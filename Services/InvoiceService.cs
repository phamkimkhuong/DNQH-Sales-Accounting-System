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
    public class InvoiceService
    {
        private readonly HoaDonBanDAL _hoaDonBanDAL;
        private readonly DonDatHangDAL _donDatHangDAL;

        public InvoiceService()
        {
            _hoaDonBanDAL = new HoaDonBanDAL();
            _donDatHangDAL = new DonDatHangDAL();
        }

        public string GenerateNewInvoiceId()
        {
            return _hoaDonBanDAL.GetNextMaHDB();
        }

        public bool CreateInvoiceFromOrder(string maDDH, string maHDB, string ghiChu, string trangThai, out string errorMessage)
        {
            string createdMaHDB;
            return CreateInvoiceFromOrder(maDDH, maHDB, ghiChu, trangThai, out errorMessage, out createdMaHDB);
        }

        public bool CreateInvoiceFromOrder(string maDDH, string maHDB, string ghiChu, string trangThai, out string errorMessage, out string createdMaHDB)
        {
            errorMessage = string.Empty;
            createdMaHDB = string.Empty;
            string correlationId = Guid.NewGuid().ToString("N");
            string currentUserId = (SessionManager.CurrentUser != null) ? SessionManager.CurrentUser.MaNV : "Unknown";
            string currentRole = (SessionManager.CurrentUser != null) ? SessionManager.CurrentUser.VaiTro : "Unknown";
            Stopwatch sw = Stopwatch.StartNew();
            HoaDonBan invoice = null;

            try
            {
                // 1. Phân quyền: Chỉ Admin và Bán hàng được lập hóa đơn; Kế toán chỉ tra cứu
                if (!SessionManager.IsLoggedIn || SessionManager.CurrentUser == null)
                {
                    errorMessage = "Yêu cầu đăng nhập trước khi lập hóa đơn bán.";
                    return false;
                }

                if (!SessionManager.IsAdmin() && !SessionManager.IsSales())
                {
                    errorMessage = "Bạn không có quyền lập Hóa đơn bán (Kế toán chỉ có quyền tra cứu/kiểm tra hóa đơn).";
                    return false;
                }

                // 2. Kiểm tra đơn đặt hàng hợp lệ
                if (string.IsNullOrWhiteSpace(maDDH))
                {
                    errorMessage = "Vui lòng chọn đơn đặt hàng cần lập hóa đơn.";
                    return false;
                }

                DonDatHang order = _donDatHangDAL.GetById(maDDH);
                if (order == null)
                {
                    errorMessage = string.Format("Không tìm thấy đơn đặt hàng mã [{0}].", maDDH);
                    return false;
                }

                // Chặn lập hóa đơn cho đơn đã hủy
                if (order.TrangThai != null && order.TrangThai.Trim().Equals("Đã hủy", StringComparison.OrdinalIgnoreCase))
                {
                    errorMessage = string.Format("Đơn đặt hàng [{0}] đã bị hủy, không thể lập hóa đơn bán!", maDDH);
                    return false;
                }

                // 3. Kiểm tra quy tắc bất biến: Mỗi đơn đặt hàng có tối đa một hóa đơn bán (UQ_HOADONBAN_MADDH)
                if (_hoaDonBanDAL.HasInvoiceForOrder(maDDH))
                {
                    errorMessage = string.Format("Đơn đặt hàng [{0}] đã được lập hóa đơn bán trước đó. Không thể lập hóa đơn lần 2!", maDDH);
                    return false;
                }

                // 4. Lấy chi tiết đơn đặt hàng gốc
                List<ChiTietDonDatHang> orderDetails = _donDatHangDAL.GetChiTietList(maDDH);
                if (orderDetails == null || orderDetails.Count == 0)
                {
                    errorMessage = string.Format("Đơn đặt hàng [{0}] không có chi tiết sản phẩm hợp lệ.", maDDH);
                    return false;
                }

                // Chuẩn bị danh sách tồn kho cần kiểm tra
                Dictionary<string, int> requestedQuantities = new Dictionary<string, int>();
                for (int i = 0; i < orderDetails.Count; i++)
                {
                    string sp = orderDetails[i].MaSP.Trim();
                    if (!requestedQuantities.ContainsKey(sp))
                    {
                        requestedQuantities[sp] = 0;
                    }
                    requestedQuantities[sp] += orderDetails[i].SoLuong;
                }

                // 5. Khởi tạo đối tượng Hóa đơn bán (gán MaNV của người dùng hiện tại)
                invoice = new HoaDonBan
                {
                    MaHDB = maHDB,
                    MaNV = SessionManager.CurrentUser.MaNV,
                    MaDDH = order.MaDDH,
                    MaKH = order.MaKH,
                    NgayLap = DateTime.Now,
                    TongTien = order.TongTien,
                    GhiChu = string.IsNullOrEmpty(ghiChu) ? string.Format("Lập từ đơn đặt hàng {0}", order.MaDDH) : ghiChu,
                    TrangThai = string.IsNullOrEmpty(trangThai) ? InvoiceStatusConstants.ChuaThanhToan : trangThai
                };

                // 6. Khởi tạo chi tiết hóa đơn từ chi tiết đơn hàng
                List<ChiTietHoaDonBan> invoiceDetails = new List<ChiTietHoaDonBan>();
                decimal tongTien = 0;

                for (int i = 0; i < orderDetails.Count; i++)
                {
                    ChiTietDonDatHang od = orderDetails[i];
                    ChiTietHoaDonBan id = new ChiTietHoaDonBan
                    {
                        MaHDB = invoice.MaHDB,
                        MaSP = od.MaSP,
                        SoLuong = od.SoLuong,
                        DonGia = od.DonGia,
                        GiamGia = od.GiamGia,
                        ThanhTien = od.ThanhTien
                    };
                    tongTien += id.ThanhTien;
                    invoiceDetails.Add(id);
                }

                invoice.TongTien = tongTien;

                // 7. Thực hiện Atomic Transaction: Khóa bảng, kiểm tra tồn kho, Insert Hóa đơn + Chi tiết + Cập nhật Đơn
                Database.ExecuteTransaction(delegate(SqlTransaction trans)
                {
                    // Chống race condition: sinh mã và kiểm tra lại sự tồn tại trong transaction
                    if (_hoaDonBanDAL.HasInvoiceForOrder(maDDH, trans))
                    {
                        throw new InvalidOperationException(
                            string.Format("Đơn đặt hàng [{0}] đã có hóa đơn bán được lập đồng thời bởi phiên khác!", maDDH));
                    }

                    // Luôn tự sinh mã trong transaction với khóa bảng TABLOCKX, HOLDLOCK (loại trừ xung đột PK)
                    invoice.MaHDB = _hoaDonBanDAL.GetNextMaHDB(trans);

                    // Kiểm tra tồn kho trước khi phát hành hóa đơn
                    TonKhoDAL tkDal = new TonKhoDAL();
                    foreach (KeyValuePair<string, int> kvp in requestedQuantities)
                    {
                        int tonHienTai = tkDal.GetTongTonKho(kvp.Key, trans);
                        if (kvp.Value > tonHienTai)
                        {
                            throw new InvalidOperationException(
                                string.Format("Sản phẩm {0} yêu cầu {1} nhưng tồn kho khả dụng hiện tại chỉ còn {2}.",
                                    kvp.Key, kvp.Value, tonHienTai));
                        }
                    }

                    // 7.1: Insert HOADONBAN
                    _hoaDonBanDAL.Insert(invoice, trans);

                    // 7.2: Insert tất cả CHITIETHOADONBAN
                    for (int i = 0; i < invoiceDetails.Count; i++)
                    {
                        invoiceDetails[i].MaHDB = invoice.MaHDB;
                        _hoaDonBanDAL.InsertChiTiet(invoiceDetails[i], trans);
                    }

                    // 7.3: Cập nhật tổng tiền chuẩn xác
                    _hoaDonBanDAL.UpdateTongTien(invoice.MaHDB, invoice.TongTien, trans);

                    // 7.4: Cập nhật trạng thái đơn đặt hàng sang "Đã lập hóa đơn" với Khóa Lạc Quan
                    ConcurrencyUpdateResult orderUpdateRes = _donDatHangDAL.UpdateTrangThaiWithResult(order.MaDDH, OrderStatusConstants.DaLapHoaDon, order.Version, trans);
                    if (orderUpdateRes == ConcurrencyUpdateResult.ConcurrencyConflict)
                    {
                        throw new InvalidOperationException("Đơn đặt hàng này vừa bị người khác cập nhật ở một màn hình khác! Thao tác của bạn không được chấp nhận để tránh ghi đè dữ liệu.");
                    }
                });

                sw.Stop();
                AppLogger.Info(
                    "CreateInvoice",
                    string.Format("Lập hóa đơn bán thành công: MaHDB={0}, MaDDH={1}, TongTien={2:N0}, SoDong={3}",
                        invoice.MaHDB, invoice.MaDDH, invoice.TongTien, invoiceDetails.Count),
                    correlationId,
                    currentUserId,
                    sw.ElapsedMilliseconds,
                    "HOADONBAN",
                    invoice.MaHDB,
                    currentRole,
                    "Success");

                createdMaHDB = invoice.MaHDB;
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
                        "Đã xảy ra lỗi trong quá trình lưu dữ liệu hóa đơn bán. Vui lòng thử lại hoặc liên hệ quản trị viên (Mã tra cứu: {0}).",
                        correlationId);
                }
                else
                {
                    errorMessage = string.Format(
                        "Đã xảy ra lỗi hệ thống không mong muốn (Mã tra cứu: {0}).",
                        correlationId);
                }

                AppLogger.Error(
                    "CreateInvoice",
                    "Thất bại khi lập hóa đơn bán: " + ex.Message,
                    ex,
                    correlationId,
                    currentUserId,
                    sw.ElapsedMilliseconds,
                    "HOADONBAN",
                    invoice != null ? invoice.MaHDB : maHDB,
                    currentRole,
                    "Failed");

                return false;
            }
        }
    }
}
