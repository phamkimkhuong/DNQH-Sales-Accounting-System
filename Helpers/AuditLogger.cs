using System;
using System.Threading.Tasks;
using DNQH_KeToanBanHang.DataAccess;
using DNQH_KeToanBanHang.Models;

namespace DNQH_KeToanBanHang.Helpers
{
    /// <summary>
    /// Tiện ích ghi nhận Nhật ký hoạt động doanh nghiệp (Business Audit Trail) tập trung toàn hệ thống.
    /// Tự động thu thập bối cảnh phiên làm việc (Người dùng, Vai trò, Tên máy trạm LAN)
    /// và ghi đồng thời vào SQL Server (cho Quản trị viên giám sát) và File log cục bộ (cho IT chẩn đoán).
    /// </summary>
    public static class AuditLogger
    {
        /// <summary>
        /// Ghi nhận 1 hành động nghiệp vụ vào hệ thống kiểm toán tập trung.
        /// </summary>
        /// <param name="hanhDong">Tên hành động nghiệp vụ (Ví dụ: Đăng nhập, Lập hóa đơn, Xuất kho, Phiếu thu...)</param>
        /// <param name="noiDung">Mô tả chi tiết nội dung giao dịch</param>
        /// <param name="loaiDoiTuong">Loại đối tượng (HOADONBAN, DONDATHANG, PHIEUXUATKHO...)</param>
        /// <param name="maDoiTuong">Mã khóa chính của đối tượng (HDB000000001, DDH0000001...)</param>
        /// <param name="ketQua">Kết quả (Thành công / Thất bại / Cảnh báo)</param>
        /// <param name="correlationId">Mã liên kết chu trình giao dịch</param>
        /// <param name="durationMs">Thời gian xử lý giao dịch tính bằng miligiây</param>
        /// <param name="capDo">Cấp độ (INFO / WARN / ERROR)</param>
        public static void Log(
            string hanhDong,
            string noiDung,
            string loaiDoiTuong = null,
            string maDoiTuong = null,
            string ketQua = "Thành công",
            string correlationId = null,
            long durationMs = 0,
            string capDo = "INFO")
        {
            try
            {
                // 1. Trích xuất ngữ cảnh phiên làm việc hiện tại
                string maNV = SessionManager.CurrentUser != null ? SessionManager.CurrentUser.MaNV : "System";
                string tenNV = SessionManager.CurrentUser != null ? SessionManager.CurrentUser.HoTen : "Hệ thống";
                string vaiTro = SessionManager.CurrentUser != null ? SessionManager.CurrentUser.VaiTro : "Hệ thống";
                string tenMay = Environment.MachineName;

                // 2. Ghi ra file log cục bộ (Diagnostics)
                if (capDo == "ERROR")
                {
                    AppLogger.Error(hanhDong, noiDung, null, correlationId, maNV, durationMs, loaiDoiTuong, maDoiTuong, vaiTro, ketQua);
                }
                else if (capDo == "WARN")
                {
                    AppLogger.Warn(hanhDong, noiDung, correlationId, maNV, durationMs, loaiDoiTuong, maDoiTuong, vaiTro, ketQua);
                }
                else
                {
                    AppLogger.Info(hanhDong, noiDung, correlationId, maNV, durationMs, loaiDoiTuong, maDoiTuong, vaiTro, ketQua);
                }

                // 3. Ghi vào SQL Server tập trung (Business Audit Trail)
                NhatKyHoatDong entry = new NhatKyHoatDong
                {
                    ThoiGian = DateTime.Now,
                    CapDo = capDo ?? "INFO",
                    HanhDong = hanhDong,
                    MaNV = maNV,
                    TenNV = tenNV,
                    VaiTro = vaiTro,
                    TenMay = tenMay,
                    LoaiDoiTuong = loaiDoiTuong,
                    MaDoiTuong = maDoiTuong,
                    KetQua = ketQua ?? "Thành công",
                    ThoiGianXuLyMs = durationMs,
                    CorrelationId = correlationId,
                    NoiDung = noiDung
                };

                // Chạy ngầm trong background thread để không ảnh hưởng thời gian phản hồi UI
                Task.Run(() => NhatKyHoatDongDal.GhiNhatKy(entry));
            }
            catch (Exception ex)
            {
                Console.WriteLine("AuditLogger.Log Exception: " + ex.Message);
            }
        }

        /// <summary>
        /// Ghi nhận lỗi nghiệp vụ với thông tin Exception.
        /// </summary>
        public static void LogError(
            string hanhDong,
            string noiDung,
            Exception ex,
            string loaiDoiTuong = null,
            string maDoiTuong = null,
            string correlationId = null,
            long durationMs = 0)
        {
            string fullMsg = noiDung;
            if (ex != null)
            {
                fullMsg += string.Format(" [Lỗi: {0} - {1}]", ex.GetType().Name, ex.Message);
            }

            Log(hanhDong, fullMsg, loaiDoiTuong, maDoiTuong, "Thất bại", correlationId, durationMs, "ERROR");
        }
    }
}
