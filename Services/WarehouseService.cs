using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using DNQH_KeToanBanHang.Constants;
using DNQH_KeToanBanHang.DataAccess;
using DNQH_KeToanBanHang.Helpers;
using DNQH_KeToanBanHang.Models;

namespace DNQH_KeToanBanHang.Services
{
    public class WarehouseService
    {
        private readonly PhieuXuatKhoDAL _phieuXuatKhoDAL;
        private readonly TonKhoDAL _tonKhoDAL;
        private readonly HoaDonBanDAL _hoaDonBanDAL;
        private readonly KhoDAL _khoDAL;

        public WarehouseService()
        {
            _phieuXuatKhoDAL = new PhieuXuatKhoDAL();
            _tonKhoDAL = new TonKhoDAL();
            _hoaDonBanDAL = new HoaDonBanDAL();
            _khoDAL = new KhoDAL();
        }

        public string GenerateNewIssueId()
        {
            return _phieuXuatKhoDAL.GetNextMaPXK();
        }

        public bool CreatePhieuXuatKho(PhieuXuatKho pxk, List<ChiTietPhieuXuatKho> details, out string errorMessage)
        {
            errorMessage = string.Empty;
            string correlationId = Guid.NewGuid().ToString("N");
            string currentUserId = (SessionManager.CurrentUser != null) ? SessionManager.CurrentUser.MaNV : "Unknown";
            string currentRole = (SessionManager.CurrentUser != null) ? SessionManager.CurrentUser.VaiTro : "Unknown";
            Stopwatch sw = Stopwatch.StartNew();

            try
            {
                // 1. Phân quyền: Chỉ Quản trị viên và Nhân viên kho có quyền xuất kho
                if (!SessionManager.IsLoggedIn || SessionManager.CurrentUser == null)
                {
                    errorMessage = "Yêu cầu đăng nhập trước khi thực hiện xuất kho.";
                    return false;
                }

                if (!SessionManager.IsAdmin() && !SessionManager.IsWarehouse())
                {
                    errorMessage = "Bạn không có quyền lập Phiếu xuất kho. Chỉ Quản trị viên và Nhân viên kho mới có quyền này.";
                    return false;
                }

                // 2. Validate dữ liệu đầu vào
                if (pxk == null)
                {
                    errorMessage = "Thông tin phiếu xuất kho không hợp lệ.";
                    return false;
                }

                if (string.IsNullOrWhiteSpace(pxk.MaHDB))
                {
                    errorMessage = "Vui lòng chọn Hóa đơn bán cần xuất kho.";
                    return false;
                }

                if (string.IsNullOrWhiteSpace(pxk.MaKho))
                {
                    errorMessage = "Vui lòng chọn Kho xuất hàng.";
                    return false;
                }

                // Luôn gán MaNV từ phiên đăng nhập hiện tại để chống giả mạo danh tính
                pxk.MaNV = SessionManager.CurrentUser.MaNV;

                // 3. Kiểm tra Hóa đơn bán
                HoaDonBan hdb = _hoaDonBanDAL.GetById(pxk.MaHDB);
                if (hdb == null)
                {
                    errorMessage = string.Format("Không tìm thấy Hóa đơn bán [{0}] trong hệ thống.", pxk.MaHDB);
                    return false;
                }

                if (hdb.TrangThai != null && (hdb.TrangThai.Trim().Equals("Hủy", StringComparison.OrdinalIgnoreCase) ||
                                              hdb.TrangThai.Trim().Equals("Đã hủy", StringComparison.OrdinalIgnoreCase)))
                {
                    errorMessage = string.Format("Hóa đơn bán [{0}] đã bị hủy, không thể xuất kho!", pxk.MaHDB);
                    return false;
                }

                // 4. Kiểm tra Kho
                Kho kho = _khoDAL.GetById(pxk.MaKho);
                if (kho == null)
                {
                    errorMessage = string.Format("Kho hàng [{0}] không tồn tại trong hệ thống.", pxk.MaKho);
                    return false;
                }

                // 5. Kiểm tra danh sách chi tiết xuất
                if (details == null || details.Count == 0)
                {
                    errorMessage = "Phiếu xuất kho phải có ít nhất một mặt hàng cần xuất.";
                    return false;
                }

                // Lấy chi tiết gốc của Hóa đơn bán để kiểm soát tính hợp lệ của sản phẩm
                List<ChiTietHoaDonBan> hdbDetails = _hoaDonBanDAL.GetChiTietList(pxk.MaHDB);
                if (hdbDetails == null || hdbDetails.Count == 0)
                {
                    errorMessage = string.Format("Hóa đơn bán [{0}] không tồn tại hoặc không có chi tiết mặt hàng.", pxk.MaHDB);
                    return false;
                }

                Dictionary<string, int> hdbQuantities = new Dictionary<string, int>();
                for (int i = 0; i < hdbDetails.Count; i++)
                {
                    hdbQuantities[hdbDetails[i].MaSP.Trim()] = hdbDetails[i].SoLuong;
                }

                for (int i = 0; i < details.Count; i++)
                {
                    ChiTietPhieuXuatKho ct = details[i];
                    if (string.IsNullOrWhiteSpace(ct.MaSP))
                    {
                        errorMessage = string.Format("Dòng {0}: Chưa chọn sản phẩm xuất.", i + 1);
                        return false;
                    }

                    string maSP = ct.MaSP.Trim();
                    if (!hdbQuantities.ContainsKey(maSP))
                    {
                        errorMessage = string.Format("Sản phẩm [{0}] không nằm trong Hóa đơn bán [{1}].", maSP, pxk.MaHDB);
                        return false;
                    }

                    if (ct.SoLuongXuat <= 0)
                    {
                        errorMessage = string.Format("Số lượng xuất của sản phẩm [{0}] phải lớn hơn 0.", ct.TenSP ?? maSP);
                        return false;
                    }
                }

                if (string.IsNullOrWhiteSpace(pxk.TrangThai))
                {
                    pxk.TrangThai = EntityStatusConstants.Export.DaXuat;
                }
                if (pxk.NgayXuat == null)
                {
                    pxk.NgayXuat = DateTime.Now;
                }

                // 6. Thực hiện Atomic Transaction:
                // Khóa Hóa đơn (UPDLOCK, HOLDLOCK) -> Kiểm tra lại số lượng còn lại trong transaction ->
                // Sinh mã mới an toàn -> Trừ tồn kho có điều kiện (chống tồn âm) -> Insert Header & Chi tiết
                Database.ExecuteTransaction(delegate(SqlTransaction trans)
                {
                    // 6.1: Khóa dòng Hóa đơn bán và kiểm tra trạng thái hủy (chống 2 phiên cùng xuất đồng thời)
                    bool lockSuccess = _phieuXuatKhoDAL.LockHoaDonForUpdate(pxk.MaHDB, trans);
                    if (!lockSuccess)
                    {
                        throw new InvalidOperationException(
                            string.Format("Không tìm thấy Hóa đơn bán [{0}] trong hệ thống.", pxk.MaHDB));
                    }

                    // 6.2: Kiểm tra số lượng đã xuất và số lượng còn lại ngay trong transaction có khóa
                    for (int i = 0; i < details.Count; i++)
                    {
                        ChiTietPhieuXuatKho ct = details[i];
                        string maSP = ct.MaSP.Trim();

                        int daXuat = _phieuXuatKhoDAL.GetTongSoLuongDaXuat(pxk.MaHDB, maSP, trans);
                        int conLai = hdbQuantities[maSP] - daXuat;

                        if (ct.SoLuongXuat > conLai)
                        {
                            throw new InvalidOperationException(string.Format(
                                "Sản phẩm [{0}] chỉ còn {1} chiếc cần xuất theo hóa đơn [{2}], không thể xuất vượt quá ({3} chiếc)!",
                                ct.TenSP ?? maSP, conLai, pxk.MaHDB, ct.SoLuongXuat));
                        }
                    }

                    // 6.3: Luôn tự sinh mã trong transaction với khóa TABLOCKX, HOLDLOCK (loại trừ xung đột PK)
                    pxk.MaPXK = _phieuXuatKhoDAL.GetNextMaPXK(trans);

                    // 6.4: Insert Header PHIEUXUATKHO
                    _phieuXuatKhoDAL.Insert(pxk, trans);

                    // 6.5: Xử lý từng dòng chi tiết: Trừ tồn kho atomic + Insert Chi tiết
                    for (int i = 0; i < details.Count; i++)
                    {
                        ChiTietPhieuXuatKho ct = details[i];
                        ct.MaPXK = pxk.MaPXK;

                        // Trừ tồn kho có điều kiện SoLuongTon >= @SoLuongXuat (chống tồn âm & concurrency race)
                        bool truThanhCong = _tonKhoDAL.TruTonKho(pxk.MaKho, ct.MaSP, ct.SoLuongXuat, trans);
                        if (!truThanhCong)
                        {
                            int tonHienTai = _tonKhoDAL.GetTonKho(pxk.MaKho, ct.MaSP, trans);
                            throw new InvalidOperationException(
                                string.Format("Không thể xuất kho: Kho [{0}] hiện chỉ còn {1} sản phẩm [{2}], không đủ để xuất {3} sản phẩm!",
                                    pxk.MaKho, tonHienTai, ct.TenSP ?? ct.MaSP, ct.SoLuongXuat));
                        }

                        // Insert CHITIETPHIEUXUATKHO
                        _phieuXuatKhoDAL.InsertChiTiet(ct, trans);
                    }
                });

                sw.Stop();
                AppLogger.Info(
                    "CreatePhieuXuatKho",
                    string.Format("Xuất kho thành công: MaPXK={0}, MaHDB={1}, MaKho={2}, SoMatHang={3}",
                        pxk.MaPXK, pxk.MaHDB, pxk.MaKho, details.Count),
                    correlationId,
                    currentUserId,
                    sw.ElapsedMilliseconds,
                    "PHIEUXUATKHO",
                    pxk.MaPXK,
                    currentRole,
                    "Success");

                return true;
            }
            catch (Exception ex)
            {
                sw.Stop();
                if (ex is InvalidOperationException || ex is ArgumentException)
                {
                    // Thông điệp nghiệp vụ an toàn được hiển thị rõ ràng
                    errorMessage = ex.Message;
                }
                else if (ex is SqlException)
                {
                    // Lỗi SQL kỹ thuật: che giấu chi tiết nhạy cảm và trả về mã tra cứu
                    errorMessage = string.Format(
                        "Đã xảy ra lỗi trong quá trình lưu dữ liệu xuất kho. Vui lòng thử lại hoặc liên hệ quản trị viên (Mã tra cứu: {0}).",
                        correlationId);
                }
                else
                {
                    errorMessage = string.Format(
                        "Đã xảy ra lỗi hệ thống không mong muốn (Mã tra cứu: {0}).",
                        correlationId);
                }

                AppLogger.Error(
                    "CreatePhieuXuatKho",
                    "Thất bại khi lập phiếu xuất kho: " + ex.Message,
                    ex,
                    correlationId,
                    currentUserId,
                    sw.ElapsedMilliseconds,
                    "PHIEUXUATKHO",
                    (pxk != null) ? pxk.MaPXK : null,
                    currentRole,
                    "Failed");

                return false;
            }
        }
    }
}
