using System;
using System.Threading;
using System.Windows.Forms;
using DNQH_KeToanBanHang.Forms;
using DNQH_KeToanBanHang.Helpers;
using DNQH_KeToanBanHang.Models;

namespace DNQH_KeToanBanHang
{
    static class Program
    {
        /// <summary>
        /// Điểm bắt đầu chính cho ứng dụng.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
#if DEBUG
            if (args != null && args.Length > 0)
            {
                if (args[0] == "--dev")
                {
                    Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
                    Application.ThreadException += new ThreadExceptionEventHandler(OnThreadException);
                    AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(OnUnhandledException);

                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);

                    TaiKhoan devAccount = new TaiKhoan
                    {
                        MaTK = "TK001",
                        TenDangNhap = "admin",
                        VaiTro = "Quản trị viên",
                        TrangThai = "Hoạt động"
                    };
                    NhanVien devEmployee = new NhanVien
                    {
                        MaNV = "NV001",
                        HoTen = "Nguyễn Văn Quản Trị"
                    };
                    SessionManager.SetSession(devAccount, devEmployee);

                    AppLogger.Info("DevStartup", "Khởi động chế độ phát triển nhanh (--dev bypass login).");
                    Application.Run(new frmMain());
                    return;
                }
            }
#endif

            // Thiết lập chế độ bắt ngoại lệ toàn cục cho UI thread
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ThreadException += new ThreadExceptionEventHandler(OnThreadException);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(OnUnhandledException);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Tự động dọn dẹp các file log cục bộ quá hạn (Log Retention 30 ngày)
            AppLogger.CleanOldLogs(30);

            AppLogger.Info("ApplicationStartup", "Hệ thống DNQH Kế Toán Bán Hàng đang khởi động.");

            try
            {
                Application.Run(new frmDangNhap());
            }
            catch (Exception ex)
            {
                AppLogger.Error("FatalError", "Lỗi nghiêm trọng không thể phục hồi tại Program.Main", ex);
                MessageBox.Show("Đã xảy ra lỗi nghiêm trọng. Chi tiết đã được ghi vào nhật ký hệ thống.",
                                "Lỗi Hệ Thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                AppLogger.Info("ApplicationShutdown", "Ứng dụng kết thúc phiên làm việc.");
            }
        }

        private static void OnThreadException(object sender, ThreadExceptionEventArgs e)
        {
            string correlationId = Guid.NewGuid().ToString("N");
            string userId = SessionManager.CurrentUser != null ? SessionManager.CurrentUser.MaNV : "System";
            string role = SessionManager.CurrentUser != null ? SessionManager.CurrentUser.VaiTro : "None";
            AppLogger.Error("UIThreadException", "Ngoại lệ chưa được xử lý trên UI Thread: " + e.Exception.Message, e.Exception, correlationId, userId, 0, null, null, role, "Failed");

            string userMsg;
            if (e.Exception is System.Data.SqlClient.SqlException)
            {
                userMsg = "Đã xảy ra lỗi khi giao tiếp với cơ sở dữ liệu. Vui lòng kiểm tra kết nối mạng hoặc liên hệ quản trị viên.";
            }
            else
            {
                userMsg = "Ứng dụng không thể hoàn tất thao tác. Vui lòng thử lại hoặc liên hệ quản trị viên.";
            }

            MessageBox.Show(
                string.Format("Đã phát sinh sự cố trong quá trình thực thi (Mã tra cứu: {0}):\n{1}\n\nChi tiết kỹ thuật đã được ghi lại trong nhật ký hệ thống.", correlationId, userMsg),
                "Lỗi Ứng Dụng",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }

        private static void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = e.ExceptionObject as Exception;
            string correlationId = Guid.NewGuid().ToString("N");
            string userId = SessionManager.CurrentUser != null ? SessionManager.CurrentUser.MaNV : "System";
            string role = SessionManager.CurrentUser != null ? SessionManager.CurrentUser.VaiTro : "None";
            string msg = ex != null ? ex.Message : "Lỗi không xác định";
            AppLogger.Error("AppDomainUnhandledException", "Ngoại lệ chưa được xử lý tại AppDomain: " + msg, ex, correlationId, userId, 0, null, null, role, "Failed");

            MessageBox.Show(
                string.Format("Đã phát sinh sự cố nghiêm trọng (Mã tra cứu: {0}).\nỨng dụng sẽ đóng để bảo đảm an toàn dữ liệu.\nVui lòng liên hệ quản trị viên.", correlationId),
                "Lỗi Nghiêm Trọng",
                MessageBoxButtons.OK,
                MessageBoxIcon.Stop);
        }
    }
}
