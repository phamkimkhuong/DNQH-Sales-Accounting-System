using System;
using System.Collections.Generic;
using System.Diagnostics;
using DNQH_KeToanBanHang.DataAccess;
using DNQH_KeToanBanHang.Helpers;
using DNQH_KeToanBanHang.Models;

namespace DNQH_KeToanBanHang.Services
{
    public class ReportingService
    {
        private readonly BaoCaoDAL _baoCaoDAL;
        private readonly TonKhoDAL _tonKhoDAL;

        public ReportingService()
        {
            _baoCaoDAL = new BaoCaoDAL();
            _tonKhoDAL = new TonKhoDAL();
        }

        #region Authorization & Validation Helpers

        private void CheckPermission()
        {
            if (!SessionManager.IsLoggedIn || SessionManager.CurrentUser == null)
            {
                throw new UnauthorizedAccessException("Yêu cầu đăng nhập trước khi xem báo cáo kế toán.");
            }

            if (!SessionManager.IsAdmin() && !SessionManager.IsAccountant())
            {
                throw new UnauthorizedAccessException("Bạn không có quyền xem Báo cáo & Kế toán chi tiết. Chỉ Quản trị viên và Nhân viên kế toán mới có quyền này.");
            }
        }

        private void ValidateDateRange(DateTime tuNgay, DateTime denNgay)
        {
            if (tuNgay.Date > denNgay.Date)
            {
                throw new ArgumentException("Từ ngày không được lớn hơn Đến ngày! Vui lòng chọn khoảng thời gian hợp lệ.");
            }
        }

        #endregion

        #region 1. Báo cáo Doanh thu bán hàng

        public List<BaoCaoDoanhThuDTO> GetBaoCaoDoanhThu(DateTime tuNgay, DateTime denNgay, string maKH = null)
        {
            string correlationId = Guid.NewGuid().ToString("N");
            string currentUserId = SessionManager.CurrentUser != null ? SessionManager.CurrentUser.MaNV : "Unknown";
            string currentRole = SessionManager.CurrentUser != null ? SessionManager.CurrentUser.VaiTro : "Unknown";
            Stopwatch sw = Stopwatch.StartNew();

            try
            {
                CheckPermission();
                ValidateDateRange(tuNgay, denNgay);

                List<BaoCaoDoanhThuDTO> result = _baoCaoDAL.GetBaoCaoDoanhThu(tuNgay, denNgay, maKH);
                sw.Stop();

                AppLogger.Info(
                    "REPORT_REVENUE",
                    string.Format("Truy vấn báo cáo doanh thu thành công ({0} bản ghi)", result.Count),
                    correlationId,
                    currentUserId,
                    sw.ElapsedMilliseconds,
                    "REPORT",
                    string.Format("{0:yyyyMMdd}-{1:yyyyMMdd}", tuNgay, denNgay),
                    currentRole,
                    "Success");

                return result;
            }
            catch (Exception ex)
            {
                sw.Stop();
                AppLogger.Error(
                    "REPORT_REVENUE",
                    "Lỗi khi truy vấn báo cáo doanh thu: " + ex.Message,
                    ex,
                    correlationId,
                    currentUserId,
                    sw.ElapsedMilliseconds,
                    "REPORT",
                    string.Format("{0:yyyyMMdd}-{1:yyyyMMdd}", tuNgay, denNgay),
                    currentRole,
                    "Failure");
                throw;
            }
        }

        public List<BaoCaoDoanhThuDTO> GetBaoCaoDoanhThu(DateTime tuNgay, DateTime denNgay, out string errorMessage, string maKH = null)
        {
            errorMessage = string.Empty;
            try
            {
                return GetBaoCaoDoanhThu(tuNgay, denNgay, maKH);
            }
            catch (UnauthorizedAccessException ex)
            {
                errorMessage = ex.Message;
                return new List<BaoCaoDoanhThuDTO>();
            }
            catch (ArgumentException ex)
            {
                errorMessage = ex.Message;
                return new List<BaoCaoDoanhThuDTO>();
            }
            catch (Exception)
            {
                errorMessage = "Đã xảy ra lỗi trong quá trình tổng hợp báo cáo doanh thu. Vui lòng thử lại hoặc liên hệ quản trị hệ thống.";
                return new List<BaoCaoDoanhThuDTO>();
            }
        }

        #endregion

        #region 2. Báo cáo Tổng hợp Thu - Chi (Sổ quỹ)

        public List<BaoCaoThuChiDTO> GetBaoCaoThuChi(DateTime tuNgay, DateTime denNgay)
        {
            string correlationId = Guid.NewGuid().ToString("N");
            string currentUserId = SessionManager.CurrentUser != null ? SessionManager.CurrentUser.MaNV : "Unknown";
            string currentRole = SessionManager.CurrentUser != null ? SessionManager.CurrentUser.VaiTro : "Unknown";
            Stopwatch sw = Stopwatch.StartNew();

            try
            {
                CheckPermission();
                ValidateDateRange(tuNgay, denNgay);

                List<BaoCaoThuChiDTO> result = _baoCaoDAL.GetBaoCaoThuChi(tuNgay, denNgay);
                sw.Stop();

                AppLogger.Info(
                    "REPORT_CASH_FLOW",
                    string.Format("Truy vấn báo cáo thu chi thành công ({0} giao dịch)", result.Count),
                    correlationId,
                    currentUserId,
                    sw.ElapsedMilliseconds,
                    "REPORT",
                    string.Format("{0:yyyyMMdd}-{1:yyyyMMdd}", tuNgay, denNgay),
                    currentRole,
                    "Success");

                return result;
            }
            catch (Exception ex)
            {
                sw.Stop();
                AppLogger.Error(
                    "REPORT_CASH_FLOW",
                    "Lỗi khi truy vấn báo cáo thu chi: " + ex.Message,
                    ex,
                    correlationId,
                    currentUserId,
                    sw.ElapsedMilliseconds,
                    "REPORT",
                    string.Format("{0:yyyyMMdd}-{1:yyyyMMdd}", tuNgay, denNgay),
                    currentRole,
                    "Failure");
                throw;
            }
        }

        public List<BaoCaoThuChiDTO> GetBaoCaoThuChi(DateTime tuNgay, DateTime denNgay, out string errorMessage)
        {
            errorMessage = string.Empty;
            try
            {
                return GetBaoCaoThuChi(tuNgay, denNgay);
            }
            catch (UnauthorizedAccessException ex)
            {
                errorMessage = ex.Message;
                return new List<BaoCaoThuChiDTO>();
            }
            catch (ArgumentException ex)
            {
                errorMessage = ex.Message;
                return new List<BaoCaoThuChiDTO>();
            }
            catch (Exception)
            {
                errorMessage = "Đã xảy ra lỗi trong quá trình tổng hợp báo cáo thu - chi. Vui lòng thử lại.";
                return new List<BaoCaoThuChiDTO>();
            }
        }

        #endregion

        #region 3. Báo cáo Tổng hợp Tồn kho

        public List<BaoCaoTonKhoDTO> GetBaoCaoTonKho(string maKho = null)
        {
            string correlationId = Guid.NewGuid().ToString("N");
            string currentUserId = SessionManager.CurrentUser != null ? SessionManager.CurrentUser.MaNV : "Unknown";
            string currentRole = SessionManager.CurrentUser != null ? SessionManager.CurrentUser.VaiTro : "Unknown";
            Stopwatch sw = Stopwatch.StartNew();

            try
            {
                CheckPermission();

                List<BaoCaoTonKhoDTO> result = _baoCaoDAL.GetBaoCaoTonKho(maKho);
                sw.Stop();

                AppLogger.Info(
                    "REPORT_INVENTORY",
                    string.Format("Truy vấn báo cáo tồn kho thành công ({0} mặt hàng)", result.Count),
                    correlationId,
                    currentUserId,
                    sw.ElapsedMilliseconds,
                    "REPORT",
                    maKho ?? "ALL_WAREHOUSES",
                    currentRole,
                    "Success");

                return result;
            }
            catch (Exception ex)
            {
                sw.Stop();
                AppLogger.Error(
                    "REPORT_INVENTORY",
                    "Lỗi khi truy vấn báo cáo tồn kho: " + ex.Message,
                    ex,
                    correlationId,
                    currentUserId,
                    sw.ElapsedMilliseconds,
                    "REPORT",
                    maKho ?? "ALL_WAREHOUSES",
                    currentRole,
                    "Failure");
                throw;
            }
        }

        public List<BaoCaoTonKhoDTO> GetBaoCaoTonKho(out string errorMessage, string maKho = null)
        {
            errorMessage = string.Empty;
            try
            {
                return GetBaoCaoTonKho(maKho);
            }
            catch (UnauthorizedAccessException ex)
            {
                errorMessage = ex.Message;
                return new List<BaoCaoTonKhoDTO>();
            }
            catch (Exception)
            {
                errorMessage = "Đã xảy ra lỗi trong quá trình tổng hợp báo cáo tồn kho. Vui lòng thử lại.";
                return new List<BaoCaoTonKhoDTO>();
            }
        }

        public List<CanhBaoTonKhoDTO> GetDanhSachCanhBaoTonKho(int threshold = 10, string maKho = null)
        {
            if (!SessionManager.IsLoggedIn || SessionManager.CurrentUser == null)
            {
                throw new UnauthorizedAccessException("Yêu cầu đăng nhập trước khi xem cảnh báo tồn kho.");
            }

            return _tonKhoDAL.GetDanhSachCanhBaoTonKho(threshold, maKho);
        }

        public List<CanhBaoTonKhoDTO> GetDanhSachCanhBaoTonKho(out string errorMessage, int threshold = 10, string maKho = null)
        {
            errorMessage = string.Empty;
            try
            {
                return GetDanhSachCanhBaoTonKho(threshold, maKho);
            }
            catch (UnauthorizedAccessException ex)
            {
                errorMessage = ex.Message;
                return new List<CanhBaoTonKhoDTO>();
            }
            catch (Exception ex)
            {
                errorMessage = "Đã xảy ra lỗi trong quá trình tải cảnh báo tồn kho an toàn: " + ex.Message;
                return new List<CanhBaoTonKhoDTO>();
            }
        }

        #endregion

        #region 4. Sổ Chi Tiết Khách Hàng

        public List<SoChiTietKhachHangDTO> GetSoChiTietKhachHang(string maKH, DateTime tuNgay, DateTime denNgay)
        {
            string correlationId = Guid.NewGuid().ToString("N");
            string currentUserId = SessionManager.CurrentUser != null ? SessionManager.CurrentUser.MaNV : "Unknown";
            string currentRole = SessionManager.CurrentUser != null ? SessionManager.CurrentUser.VaiTro : "Unknown";
            Stopwatch sw = Stopwatch.StartNew();

            try
            {
                CheckPermission();
                if (string.IsNullOrWhiteSpace(maKH))
                {
                    throw new ArgumentException("Vui lòng chọn khách hàng cần xem sổ chi tiết.");
                }
                ValidateDateRange(tuNgay, denNgay);

                List<SoChiTietKhachHangDTO> result = _baoCaoDAL.GetSoChiTietKhachHang(maKH, tuNgay, denNgay);
                sw.Stop();

                AppLogger.Info(
                    "LEDGER_CUSTOMER",
                    string.Format("Truy vấn sổ chi tiết khách hàng {0} thành công ({1} dòng)", maKH, result.Count),
                    correlationId,
                    currentUserId,
                    sw.ElapsedMilliseconds,
                    "REPORT",
                    maKH,
                    currentRole,
                    "Success");

                return result;
            }
            catch (Exception ex)
            {
                sw.Stop();
                AppLogger.Error(
                    "LEDGER_CUSTOMER",
                    "Lỗi khi truy vấn sổ chi tiết khách hàng: " + ex.Message,
                    ex,
                    correlationId,
                    currentUserId,
                    sw.ElapsedMilliseconds,
                    "REPORT",
                    maKH ?? "UNKNOWN",
                    currentRole,
                    "Failure");
                throw;
            }
        }

        public List<SoChiTietKhachHangDTO> GetSoChiTietKhachHang(string maKH, DateTime tuNgay, DateTime denNgay, out string errorMessage)
        {
            errorMessage = string.Empty;
            try
            {
                return GetSoChiTietKhachHang(maKH, tuNgay, denNgay);
            }
            catch (UnauthorizedAccessException ex)
            {
                errorMessage = ex.Message;
                return new List<SoChiTietKhachHangDTO>();
            }
            catch (ArgumentException ex)
            {
                errorMessage = ex.Message;
                return new List<SoChiTietKhachHangDTO>();
            }
            catch (Exception)
            {
                errorMessage = "Đã xảy ra lỗi trong quá trình tra cứu sổ chi tiết khách hàng.";
                return new List<SoChiTietKhachHangDTO>();
            }
        }

        #endregion

        #region 5. Sổ Chi Tiết Sản Phẩm

        public List<SoChiTietSanPhamDTO> GetSoChiTietSanPham(string maSP, DateTime tuNgay, DateTime denNgay)
        {
            string correlationId = Guid.NewGuid().ToString("N");
            string currentUserId = SessionManager.CurrentUser != null ? SessionManager.CurrentUser.MaNV : "Unknown";
            string currentRole = SessionManager.CurrentUser != null ? SessionManager.CurrentUser.VaiTro : "Unknown";
            Stopwatch sw = Stopwatch.StartNew();

            try
            {
                CheckPermission();
                ValidateDateRange(tuNgay, denNgay);

                List<SoChiTietSanPhamDTO> result = _baoCaoDAL.GetSoChiTietSanPham(maSP, tuNgay, denNgay);
                sw.Stop();

                AppLogger.Info(
                    "LEDGER_PRODUCT",
                    string.Format("Truy vấn sổ chi tiết sản phẩm thành công ({0} sản phẩm)", result.Count),
                    correlationId,
                    currentUserId,
                    sw.ElapsedMilliseconds,
                    "REPORT",
                    maSP ?? "ALL_PRODUCTS",
                    currentRole,
                    "Success");

                return result;
            }
            catch (Exception ex)
            {
                sw.Stop();
                AppLogger.Error(
                    "LEDGER_PRODUCT",
                    "Lỗi khi truy vấn sổ chi tiết sản phẩm: " + ex.Message,
                    ex,
                    correlationId,
                    currentUserId,
                    sw.ElapsedMilliseconds,
                    "REPORT",
                    maSP ?? "ALL_PRODUCTS",
                    currentRole,
                    "Failure");
                throw;
            }
        }

        public List<SoChiTietSanPhamDTO> GetSoChiTietSanPham(string maSP, DateTime tuNgay, DateTime denNgay, out string errorMessage)
        {
            errorMessage = string.Empty;
            try
            {
                return GetSoChiTietSanPham(maSP, tuNgay, denNgay);
            }
            catch (UnauthorizedAccessException ex)
            {
                errorMessage = ex.Message;
                return new List<SoChiTietSanPhamDTO>();
            }
            catch (ArgumentException ex)
            {
                errorMessage = ex.Message;
                return new List<SoChiTietSanPhamDTO>();
            }
            catch (Exception)
            {
                errorMessage = "Đã xảy ra lỗi trong quá trình tra cứu sổ chi tiết sản phẩm.";
                return new List<SoChiTietSanPhamDTO>();
            }
        }

        #endregion

        #region 6. Sổ Chi Tiết Hóa Đơn & Chứng Từ

        public SoChiTietHoaDonDTO GetSoChiTietHoaDon(string maHDB)
        {
            string correlationId = Guid.NewGuid().ToString("N");
            string currentUserId = SessionManager.CurrentUser != null ? SessionManager.CurrentUser.MaNV : "Unknown";
            string currentRole = SessionManager.CurrentUser != null ? SessionManager.CurrentUser.VaiTro : "Unknown";
            Stopwatch sw = Stopwatch.StartNew();

            try
            {
                CheckPermission();
                if (string.IsNullOrWhiteSpace(maHDB))
                {
                    throw new ArgumentException("Vui lòng chọn mã hóa đơn cần tra cứu.");
                }

                SoChiTietHoaDonDTO result = _baoCaoDAL.GetSoChiTietHoaDon(maHDB);
                sw.Stop();

                AppLogger.Info(
                    "LEDGER_INVOICE",
                    string.Format("Truy vấn chi tiết hóa đơn {0} thành công", maHDB),
                    correlationId,
                    currentUserId,
                    sw.ElapsedMilliseconds,
                    "REPORT",
                    maHDB,
                    currentRole,
                    "Success");

                return result;
            }
            catch (Exception ex)
            {
                sw.Stop();
                AppLogger.Error(
                    "LEDGER_INVOICE",
                    "Lỗi khi truy vấn chi tiết hóa đơn: " + ex.Message,
                    ex,
                    correlationId,
                    currentUserId,
                    sw.ElapsedMilliseconds,
                    "REPORT",
                    maHDB ?? "UNKNOWN",
                    currentRole,
                    "Failure");
                throw;
            }
        }

        public SoChiTietHoaDonDTO GetSoChiTietHoaDon(string maHDB, out string errorMessage)
        {
            errorMessage = string.Empty;
            try
            {
                return GetSoChiTietHoaDon(maHDB);
            }
            catch (UnauthorizedAccessException ex)
            {
                errorMessage = ex.Message;
                return null;
            }
            catch (ArgumentException ex)
            {
                errorMessage = ex.Message;
                return null;
            }
            catch (Exception)
            {
                errorMessage = "Đã xảy ra lỗi trong quá trình tra cứu chi tiết hóa đơn.";
                return null;
            }
        }

        #endregion

        #region 7. Tổng hợp KPI

        public TongHopKpiDTO GetTongHopKpi(DateTime tuNgay, DateTime denNgay)
        {
            string correlationId = Guid.NewGuid().ToString("N");
            string currentUserId = SessionManager.CurrentUser != null ? SessionManager.CurrentUser.MaNV : "Unknown";
            string currentRole = SessionManager.CurrentUser != null ? SessionManager.CurrentUser.VaiTro : "Unknown";
            Stopwatch sw = Stopwatch.StartNew();

            try
            {
                CheckPermission();
                ValidateDateRange(tuNgay, denNgay);

                TongHopKpiDTO result = _baoCaoDAL.GetTongHopKpi(tuNgay, denNgay);
                sw.Stop();

                AppLogger.Info(
                    "REPORT_KPI",
                    "Truy vấn tổng hợp chỉ số KPI thành công",
                    correlationId,
                    currentUserId,
                    sw.ElapsedMilliseconds,
                    "REPORT",
                    string.Format("{0:yyyyMMdd}-{1:yyyyMMdd}", tuNgay, denNgay),
                    currentRole,
                    "Success");

                return result;
            }
            catch (Exception ex)
            {
                sw.Stop();
                AppLogger.Error(
                    "REPORT_KPI",
                    "Lỗi khi truy vấn chỉ số KPI: " + ex.Message,
                    ex,
                    correlationId,
                    currentUserId,
                    sw.ElapsedMilliseconds,
                    "REPORT",
                    string.Format("{0:yyyyMMdd}-{1:yyyyMMdd}", tuNgay, denNgay),
                    currentRole,
                    "Failure");
                throw;
            }
        }

        public TongHopKpiDTO GetTongHopKpi(DateTime tuNgay, DateTime denNgay, out string errorMessage)
        {
            errorMessage = string.Empty;
            try
            {
                return GetTongHopKpi(tuNgay, denNgay);
            }
            catch (UnauthorizedAccessException ex)
            {
                errorMessage = ex.Message;
                return new TongHopKpiDTO();
            }
            catch (ArgumentException ex)
            {
                errorMessage = ex.Message;
                return new TongHopKpiDTO();
            }
            catch (Exception)
            {
                errorMessage = "Đã xảy ra lỗi trong quá trình tổng hợp chỉ số KPI.";
                return new TongHopKpiDTO();
            }
        }

        #endregion

        #region 8. Báo cáo Tuổi nợ Khách hàng

        public List<BaoCaoTuoiNoTongHopDTO> GetBaoCaoTuoiNoTongHop(DateTime ngayChot, string maKH = null, int? nhomTuoiNoFilter = null)
        {
            string correlationId = Guid.NewGuid().ToString("N");
            string currentUserId = SessionManager.CurrentUser != null ? SessionManager.CurrentUser.MaNV : "Unknown";
            string currentRole = SessionManager.CurrentUser != null ? SessionManager.CurrentUser.VaiTro : "Unknown";
            Stopwatch sw = Stopwatch.StartNew();

            try
            {
                CheckPermission();

                List<BaoCaoTuoiNoTongHopDTO> result = _baoCaoDAL.GetBaoCaoTuoiNoTongHop(ngayChot, maKH, nhomTuoiNoFilter);
                sw.Stop();

                AppLogger.Info(
                    "REPORT_AGING_SUMMARY",
                    string.Format("Truy vấn báo cáo tuổi nợ tổng hợp thành công ({0} khách hàng)", result.Count),
                    correlationId,
                    currentUserId,
                    sw.ElapsedMilliseconds,
                    "REPORT",
                    string.Format("{0:yyyyMMdd}", ngayChot),
                    currentRole,
                    "Success");

                return result;
            }
            catch (Exception ex)
            {
                sw.Stop();
                AppLogger.Error(
                    "REPORT_AGING_SUMMARY",
                    "Lỗi khi truy vấn báo cáo tuổi nợ tổng hợp: " + ex.Message,
                    ex,
                    correlationId,
                    currentUserId,
                    sw.ElapsedMilliseconds,
                    "REPORT",
                    string.Format("{0:yyyyMMdd}", ngayChot),
                    currentRole,
                    "Failure");
                throw;
            }
        }

        public List<BaoCaoTuoiNoTongHopDTO> GetBaoCaoTuoiNoTongHop(DateTime ngayChot, out string errorMessage, string maKH = null, int? nhomTuoiNoFilter = null)
        {
            errorMessage = string.Empty;
            try
            {
                return GetBaoCaoTuoiNoTongHop(ngayChot, maKH, nhomTuoiNoFilter);
            }
            catch (UnauthorizedAccessException ex)
            {
                errorMessage = ex.Message;
                return new List<BaoCaoTuoiNoTongHopDTO>();
            }
            catch (Exception)
            {
                errorMessage = "Đã xảy ra lỗi trong quá trình tải báo cáo tuổi nợ tổng hợp.";
                return new List<BaoCaoTuoiNoTongHopDTO>();
            }
        }

        public List<BaoCaoTuoiNoChiTietDTO> GetBaoCaoTuoiNoChiTiet(DateTime ngayChot, string maKH = null, int? nhomTuoiNoFilter = null)
        {
            string correlationId = Guid.NewGuid().ToString("N");
            string currentUserId = SessionManager.CurrentUser != null ? SessionManager.CurrentUser.MaNV : "Unknown";
            string currentRole = SessionManager.CurrentUser != null ? SessionManager.CurrentUser.VaiTro : "Unknown";
            Stopwatch sw = Stopwatch.StartNew();

            try
            {
                CheckPermission();

                List<BaoCaoTuoiNoChiTietDTO> result = _baoCaoDAL.GetBaoCaoTuoiNoChiTiet(ngayChot, maKH, nhomTuoiNoFilter);
                sw.Stop();

                AppLogger.Info(
                    "REPORT_AGING_DETAIL",
                    string.Format("Truy vấn báo cáo tuổi nợ chi tiết thành công ({0} hóa đơn)", result.Count),
                    correlationId,
                    currentUserId,
                    sw.ElapsedMilliseconds,
                    "REPORT",
                    string.Format("{0:yyyyMMdd}", ngayChot),
                    currentRole,
                    "Success");

                return result;
            }
            catch (Exception ex)
            {
                sw.Stop();
                AppLogger.Error(
                    "REPORT_AGING_DETAIL",
                    "Lỗi khi truy vấn báo cáo tuổi nợ chi tiết: " + ex.Message,
                    ex,
                    correlationId,
                    currentUserId,
                    sw.ElapsedMilliseconds,
                    "REPORT",
                    string.Format("{0:yyyyMMdd}", ngayChot),
                    currentRole,
                    "Failure");
                throw;
            }
        }

        public List<BaoCaoTuoiNoChiTietDTO> GetBaoCaoTuoiNoChiTiet(DateTime ngayChot, out string errorMessage, string maKH = null, int? nhomTuoiNoFilter = null)
        {
            errorMessage = string.Empty;
            try
            {
                return GetBaoCaoTuoiNoChiTiet(ngayChot, maKH, nhomTuoiNoFilter);
            }
            catch (UnauthorizedAccessException ex)
            {
                errorMessage = ex.Message;
                return new List<BaoCaoTuoiNoChiTietDTO>();
            }
            catch (Exception)
            {
                errorMessage = "Đã xảy ra lỗi trong quá trình tải báo cáo tuổi nợ chi tiết.";
                return new List<BaoCaoTuoiNoChiTietDTO>();
            }
        }

        /// <summary>
        /// Lấy tóm tắt công nợ khách hàng phục vụ kiểm soát bán hàng
        /// </summary>
        public ThongTinCongNoKhachHangDTO GetThongTinCongNoKhachHang(string maKH, DateTime? ngayChot = null)
        {
            if (string.IsNullOrWhiteSpace(maKH))
            {
                return new ThongTinCongNoKhachHangDTO();
            }

            try
            {
                return _baoCaoDAL.GetThongTinCongNoKhachHang(maKH, ngayChot);
            }
            catch (Exception ex)
            {
                AppLogger.Warn("DEBT_CHECK", "Không thể kiểm tra tình trạng công nợ khách hàng: " + ex.Message, null, "System", 0, "KHACHHANG", maKH, "System", "Error");
                return new ThongTinCongNoKhachHangDTO { MaKH = maKH };
            }
        }

        #endregion

        #region 9. Thống kê Cơ cấu Doanh thu phục vụ Biểu đồ

        public List<DoanhThuTheoLoaiSPDTO> GetCoCauDoanhThuTheoLoaiSP(DateTime tuNgay, DateTime denNgay)
        {
            string correlationId = Guid.NewGuid().ToString("N");
            string currentUserId = SessionManager.CurrentUser != null ? SessionManager.CurrentUser.MaNV : "Unknown";
            string currentRole = SessionManager.CurrentUser != null ? SessionManager.CurrentUser.VaiTro : "Unknown";
            Stopwatch sw = Stopwatch.StartNew();

            try
            {
                CheckPermission();

                List<DoanhThuTheoLoaiSPDTO> result = _baoCaoDAL.GetCoCauDoanhThuTheoLoaiSP(tuNgay, denNgay);
                sw.Stop();

                AppLogger.Info(
                    "REPORT_CATEGORY_SALES",
                    string.Format("Lấy cơ cấu doanh thu theo loại sản phẩm thành công ({0} loại)", result.Count),
                    correlationId,
                    currentUserId,
                    sw.ElapsedMilliseconds,
                    "REPORT",
                    string.Format("{0:yyyyMMdd}-{1:yyyyMMdd}", tuNgay, denNgay),
                    currentRole,
                    "Success");

                return result;
            }
            catch (Exception ex)
            {
                sw.Stop();
                AppLogger.Error(
                    "REPORT_CATEGORY_SALES",
                    "Lỗi khi lấy cơ cấu doanh thu theo loại sản phẩm: " + ex.Message,
                    ex,
                    correlationId,
                    currentUserId,
                    sw.ElapsedMilliseconds,
                    "REPORT",
                    string.Format("{0:yyyyMMdd}-{1:yyyyMMdd}", tuNgay, denNgay),
                    currentRole,
                    "Failure");
                throw;
            }
        }

        public List<DoanhThuTheoLoaiSPDTO> GetCoCauDoanhThuTheoLoaiSP(DateTime tuNgay, DateTime denNgay, out string errorMessage)
        {
            errorMessage = string.Empty;
            try
            {
                return GetCoCauDoanhThuTheoLoaiSP(tuNgay, denNgay);
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return new List<DoanhThuTheoLoaiSPDTO>();
            }
        }

        #endregion
    }
}
