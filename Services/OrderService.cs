using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using DNQH_KeToanBanHang.Constants;
using DNQH_KeToanBanHang.DataAccess;
using DNQH_KeToanBanHang.Helpers;
using DNQH_KeToanBanHang.Models;

namespace DNQH_KeToanBanHang.Services
{
    public class OrderService
    {
        private readonly DonDatHangDAL _donDatHangDAL;
        private readonly TonKhoDAL _tonKhoDAL;
        private readonly BaoCaoDAL _baoCaoDAL;

        public OrderService()
        {
            _donDatHangDAL = new DonDatHangDAL();
            _tonKhoDAL = new TonKhoDAL();
            _baoCaoDAL = new BaoCaoDAL();
        }

        public string GenerateNewOrderId()
        {
            return _donDatHangDAL.GetNextMaDDH();
        }

        public bool CreateOrder(DonDatHang order, List<ChiTietDonDatHang> details, out string errorMessage)
        {
            errorMessage = string.Empty;
            string correlationId = Guid.NewGuid().ToString("N");
            string currentUserId = (SessionManager.CurrentUser != null) ? SessionManager.CurrentUser.MaNV : "Unknown";
            string currentRole = (SessionManager.CurrentUser != null) ? SessionManager.CurrentUser.VaiTro : "Unknown";
            Stopwatch sw = Stopwatch.StartNew();

            try
            {
                // 1. Phân quyền & Session check
                if (!SessionManager.IsLoggedIn || SessionManager.CurrentUser == null)
                {
                    errorMessage = "Yêu cầu đăng nhập trước khi lập đơn đặt hàng.";
                    return false;
                }

                if (!SessionManager.IsAdmin() && !SessionManager.IsSales())
                {
                    errorMessage = "Bạn không có quyền lập Đơn đặt hàng.";
                    return false;
                }

                // 2. Validation theo chuẩn 01_PROJECT_SPEC.md mục 7
                if (order == null)
                {
                    errorMessage = "Thông tin đơn đặt hàng không hợp lệ.";
                    return false;
                }

                // Luôn gán MaNV từ phiên hiện tại, chống giả mạo danh tính
                order.MaNV = SessionManager.CurrentUser.MaNV;

                if (string.IsNullOrWhiteSpace(order.MaKH))
                {
                    errorMessage = "Vui lòng chọn khách hàng cho đơn đặt hàng.";
                    return false;
                }

                // 2.1 Kiểm tra chính sách rủi ro tín dụng & tuổi nợ khách hàng
                ThongTinCongNoKhachHangDTO debtInfo = _baoCaoDAL.GetThongTinCongNoKhachHang(order.MaKH);
                if (debtInfo != null && debtInfo.HasBadDebt)
                {
                    errorMessage = string.Format(
                        "Khách hàng {0} đang có khoản nợ khó đòi quá hạn trên 90 ngày ({1:N0} VNĐ, tổng nợ: {2:N0} VNĐ). Theo quy chế tín dụng, hệ thống tạm khóa tính năng lập đơn đặt hàng mới cho khách hàng này!",
                        !string.IsNullOrEmpty(debtInfo.TenKH) ? debtInfo.TenKH : order.MaKH,
                        debtInfo.KhoDoi_Tren90,
                        debtInfo.TongNo);

                    AppLogger.Warn(
                        "ORDER_BLOCKED_BAD_DEBT",
                        errorMessage,
                        correlationId,
                        currentUserId,
                        sw.ElapsedMilliseconds,
                        "DONDATHANG",
                        order.MaKH,
                        currentRole,
                        "Blocked");

                    return false;
                }

                if (details == null || details.Count == 0)
                {
                    errorMessage = "Đơn đặt hàng phải có ít nhất một dòng chi tiết sản phẩm.";
                    return false;
                }

                // 3. Kiểm tra từng dòng chi tiết và tính tổng tiền, cộng dồn số lượng theo mã sản phẩm
                decimal tongTien = 0;
                Dictionary<string, int> requestedQuantities = new Dictionary<string, int>();

                for (int i = 0; i < details.Count; i++)
                {
                    ChiTietDonDatHang ct = details[i];
                    if (string.IsNullOrWhiteSpace(ct.MaSP))
                    {
                        errorMessage = string.Format("Dòng {0}: Chưa chọn sản phẩm.", i + 1);
                        return false;
                    }

                    string maSP = ct.MaSP.Trim();

                    if (ct.SoLuong <= 0)
                    {
                        errorMessage = string.Format("Dòng {0} ({1}): Số lượng đặt phải lớn hơn 0.", i + 1, ct.TenSP ?? maSP);
                        return false;
                    }

                    if (ct.DonGia < 0)
                    {
                        errorMessage = string.Format("Dòng {0} ({1}): Đơn giá không được âm.", i + 1, ct.TenSP ?? maSP);
                        return false;
                    }

                    if (ct.GiamGia < 0 || ct.GiamGia > 100)
                    {
                        errorMessage = string.Format("Dòng {0} ({1}): Mức giảm giá phải nằm trong khoảng từ 0% đến 100%.", i + 1, ct.TenSP ?? maSP);
                        return false;
                    }

                    // Tính thành tiền từng dòng
                    ct.TinhThanhTien();
                    tongTien += ct.ThanhTien;

                    // Cộng dồn để kiểm tra tồn
                    if (!requestedQuantities.ContainsKey(maSP))
                    {
                        requestedQuantities[maSP] = 0;
                    }
                    requestedQuantities[maSP] += ct.SoLuong;
                }

                order.TongTien = tongTien;
                if (string.IsNullOrWhiteSpace(order.TrangThai))
                {
                    order.TrangThai = OrderStatusConstants.ChoXuLy;
                }

                // 4. Thực hiện Atomic Transaction qua Database.ExecuteTransaction
                Database.ExecuteTransaction(delegate(SqlTransaction trans)
                {
                    // Luôn tự sinh mã trong transaction với khóa bảng TABLOCKX, HOLDLOCK (loại trừ xung đột PK)
                    order.MaDDH = _donDatHangDAL.GetNextMaDDH(trans);

                    // Kiểm tra tồn kho toàn bộ các sản phẩm trong transaction
                    foreach (KeyValuePair<string, int> kvp in requestedQuantities)
                    {
                        int tonHienTai = _tonKhoDAL.GetTongTonKho(kvp.Key, trans);
                        if (kvp.Value > tonHienTai)
                        {
                            throw new InvalidOperationException(
                                string.Format("Sản phẩm {0} yêu cầu tổng số lượng {1} nhưng tồn kho khả dụng chỉ còn {2}.",
                                    kvp.Key, kvp.Value, tonHienTai));
                        }
                    }

                    // Bước 4.1: Insert DONDATHANG
                    _donDatHangDAL.Insert(order, trans);

                    // Bước 4.2: Insert tất cả CHITIETDONDATHANG
                    for (int i = 0; i < details.Count; i++)
                    {
                        details[i].MaDDH = order.MaDDH;
                        _donDatHangDAL.InsertChiTiet(details[i], trans);
                    }

                    // Bước 4.3: Update Tổng tiền chuẩn xác
                    _donDatHangDAL.UpdateTongTien(order.MaDDH, order.TongTien, trans);
                });

                sw.Stop();
                AppLogger.Info(
                    "CreateOrder",
                    string.Format("Lập đơn đặt hàng thành công: MaDDH={0}, KhachHang={1}, TongTien={2:N0}, SoDong={3}",
                        order.MaDDH, order.MaKH, order.TongTien, details.Count),
                    correlationId,
                    currentUserId,
                    sw.ElapsedMilliseconds,
                    "DONDATHANG",
                    order.MaDDH,
                    currentRole,
                    "Success");

                return true;
            }
            catch (Exception ex)
            {
                sw.Stop();
                if (ex is InvalidOperationException || ex is ArgumentException)
                {
                    errorMessage = ex.Message;
                }
                else if (ex is SqlException)
                {
                    errorMessage = string.Format(
                        "Đã xảy ra lỗi trong quá trình lưu dữ liệu đơn đặt hàng. Vui lòng thử lại hoặc liên hệ quản trị viên (Mã tra cứu: {0}).",
                        correlationId);
                }
                else
                {
                    errorMessage = string.Format(
                        "Đã xảy ra lỗi hệ thống không mong muốn (Mã tra cứu: {0}).",
                        correlationId);
                }

                AppLogger.Error(
                    "CreateOrder",
                    "Thất bại khi lập đơn đặt hàng: " + ex.Message,
                    ex,
                    correlationId,
                    currentUserId,
                    sw.ElapsedMilliseconds,
                    "DONDATHANG",
                    (order != null) ? order.MaDDH : null,
                    currentRole,
                    "Failed");

                return false;
            }
        }
    }
}
