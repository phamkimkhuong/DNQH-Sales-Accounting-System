using System;
using DNQH_KeToanBanHang.Constants;
using DNQH_KeToanBanHang.DataAccess;
using DNQH_KeToanBanHang.Helpers;
using DNQH_KeToanBanHang.Models;

namespace DNQH_KeToanBanHang.Services
{
    public class LoginResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public TaiKhoan Account { get; set; }
        public NhanVien Employee { get; set; }

        public static LoginResult Ok(TaiKhoan account, NhanVien employee)
        {
            return new LoginResult
            {
                Success = true,
                Message = "Đăng nhập thành công.",
                Account = account,
                Employee = employee
            };
        }

        public static LoginResult Fail(string message)
        {
            return new LoginResult
            {
                Success = false,
                Message = message
            };
        }
    }

    public class AuthService
    {
        private readonly TaiKhoanDAL taiKhoanDAL;
        private readonly NhanVienDAL nhanVienDAL;

        public AuthService()
        {
            taiKhoanDAL = new TaiKhoanDAL();
            nhanVienDAL = new NhanVienDAL();
        }

        public static bool IsValidRole(string role)
        {
            return RoleConstants.IsValidRole(role);
        }

        public LoginResult Login(string tenDangNhap, string matKhau)
        {
            // Vấn đề 4: Luôn dọn sạch phiên làm việc cũ trước khi bắt đầu bất kỳ lượt đăng nhập mới nào
            SessionManager.ClearSession();

            if (string.IsNullOrWhiteSpace(tenDangNhap))
                return LoginResult.Fail("Vui lòng nhập tên đăng nhập.");

            if (string.IsNullOrWhiteSpace(matKhau))
                return LoginResult.Fail("Vui lòng nhập mật khẩu.");

            tenDangNhap = tenDangNhap.Trim();

            TaiKhoan account = taiKhoanDAL.GetByTenDangNhap(tenDangNhap);
            if (account == null)
                return LoginResult.Fail("Tên đăng nhập hoặc mật khẩu không chính xác.");

            // Chỉ chấp nhận hash hợp lệ; plaintext trong CSDL bị từ chối.
            if (!SecurityHelper.VerifyPassword(matKhau, account.MatKhau))
                return LoginResult.Fail("Tên đăng nhập hoặc mật khẩu không chính xác.");

            // Kiểm tra trạng thái tài khoản
            if (!account.IsActive)
                return LoginResult.Fail("Tài khoản đã bị khóa hoặc ngừng hoạt động. Vui lòng liên hệ Quản trị viên.");

            // Vấn đề 1: Từ chối đăng nhập nếu tài khoản có vai trò rỗng hoặc không thuộc 4 vai trò chuẩn
            if (!IsValidRole(account.VaiTro))
                return LoginResult.Fail("Tài khoản chưa được phân vai trò hợp lệ. Vui lòng liên hệ Quản trị viên.");

            // Nếu mật khẩu trong database chưa được băm chuẩn PBKDF2 -> tự động nâng cấp sang băm an toàn PBKDF2
            if (!SecurityHelper.IsCurrentHash(account.MatKhau))
            {
                string hashed = SecurityHelper.HashPassword(matKhau);
                taiKhoanDAL.DoiMatKhau(account.MaTK, hashed);
                account.MatKhau = hashed;
            }

            // Lấy thông tin nhân viên liên kết
            NhanVien employee = null;
            if (!string.IsNullOrEmpty(account.MaNV))
            {
                employee = nhanVienDAL.GetByMaNV(account.MaNV);
            }

            // Lưu phiên làm việc
            SessionManager.SetSession(account, employee);

            AppLogger.Info(
                "LOGIN",
                string.Format("Đăng nhập thành công vào hệ thống: Tài khoản '{0}' ({1}), Nhân viên: {2}", account.TenDangNhap, account.VaiTro, employee != null ? employee.HoTen : "Không gắn NV"),
                null,
                account.MaNV,
                0,
                "TAIKHOAN",
                account.MaTK,
                account.VaiTro,
                "Success");

            return LoginResult.Ok(account, employee);
        }

        public void Logout()
        {
            if (SessionManager.CurrentUser != null)
            {
                AppLogger.Info(
                    "LOGOUT",
                    string.Format("Đăng xuất khỏi hệ thống: Tài khoản '{0}', Nhân viên: {1}", SessionManager.CurrentUser.TenDangNhap, SessionManager.CurrentUser.HoTen),
                    null,
                    SessionManager.CurrentUser.MaNV,
                    0,
                    "TAIKHOAN",
                    SessionManager.CurrentUser.MaTK,
                    SessionManager.CurrentUser.VaiTro,
                    "Success");
            }
            SessionManager.ClearSession();
        }

        public bool DoiMatKhau(string maTK, string matKhauCu, string matKhauMoi, out string message)
        {
            if (string.IsNullOrWhiteSpace(matKhauCu))
            {
                message = "Vui lòng nhập mật khẩu hiện tại.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(matKhauMoi))
            {
                message = "Vui lòng nhập mật khẩu mới.";
                return false;
            }

            if (matKhauMoi.Length < 4)
            {
                message = "Mật khẩu mới phải có ít nhất 4 ký tự.";
                return false;
            }

            if (SessionManager.CurrentUser == null || SessionManager.CurrentUser.MaTK != maTK)
            {
                message = "Phiên làm việc không hợp lệ hoặc đã hết hạn.";
                return false;
            }

            TaiKhoan account = taiKhoanDAL.GetById(maTK);
            if (account == null || !SecurityHelper.VerifyPassword(matKhauCu, account.MatKhau))
            {
                message = "Mật khẩu hiện tại không chính xác.";
                return false;
            }

            // Băm mật khẩu mới có Salt trước khi ghi xuống CSDL
            string hashedNewPass = SecurityHelper.HashPassword(matKhauMoi);
            bool result = taiKhoanDAL.DoiMatKhau(maTK, hashedNewPass);
            if (result)
            {
                AppLogger.Info(
                    "DOIMATKHAU",
                    string.Format("Đổi mật khẩu thành công cho tài khoản '{0}'", account.TenDangNhap),
                    null,
                    SessionManager.CurrentUser.MaNV,
                    0,
                    "TAIKHOAN",
                    maTK,
                    SessionManager.CurrentUser.VaiTro,
                    "Success");

                message = "Đổi mật khẩu thành công.";
                return true;
            }
            else
            {
                message = "Có lỗi xảy ra khi cập nhật mật khẩu.";
                return false;
            }
        }
    }
}
