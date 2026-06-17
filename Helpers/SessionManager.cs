using System;
using DNQH_KeToanBanHang.Constants;
using DNQH_KeToanBanHang.Models;

namespace DNQH_KeToanBanHang.Helpers
{
    public class UserSession
    {
        public string MaTK { get; set; }
        public string MaNV { get; set; }
        public string TenDangNhap { get; set; }
        public string VaiTro { get; set; }
        public string HoTen { get; set; }

        public UserSession()
        {
        }

        public UserSession(string maTK, string maNV, string tenDangNhap, string vaiTro, string hoTen)
        {
            MaTK = maTK;
            MaNV = maNV;
            TenDangNhap = tenDangNhap;
            VaiTro = vaiTro;
            HoTen = hoTen;
        }
    }

    public static class SessionManager
    {
        public static UserSession CurrentUser { get; private set; }

        public static bool IsLoggedIn
        {
            get { return CurrentUser != null; }
        }

        public static void SetSession(TaiKhoan account, NhanVien employee = null)
        {
            if (account == null)
            {
                ClearSession();
                return;
            }

            string hoTen = employee != null && !string.IsNullOrEmpty(employee.HoTen)
                ? employee.HoTen
                : account.TenDangNhap;

            CurrentUser = new UserSession(
                account.MaTK,
                account.MaNV,
                account.TenDangNhap,
                account.VaiTro,
                hoTen
            );
        }

        public static void SetSession(string maTK, string maNV, string tenDangNhap, string vaiTro, string hoTen)
        {
            CurrentUser = new UserSession(maTK, maNV, tenDangNhap, vaiTro, hoTen);
        }

        public static void ClearSession()
        {
            CurrentUser = null;
        }

        public static bool HasRole(string role)
        {
            if (CurrentUser == null || string.IsNullOrEmpty(CurrentUser.VaiTro) || string.IsNullOrEmpty(role))
                return false;

            return string.Equals(CurrentUser.VaiTro.Trim(), role.Trim(), StringComparison.OrdinalIgnoreCase);
        }

        public static bool IsAdmin()
        {
            return RoleConstants.IsAdmin(CurrentUser != null ? CurrentUser.VaiTro : null);
        }

        public static bool IsSales()
        {
            return RoleConstants.IsSales(CurrentUser != null ? CurrentUser.VaiTro : null);
        }

        public static bool IsWarehouse()
        {
            return RoleConstants.IsWarehouse(CurrentUser != null ? CurrentUser.VaiTro : null);
        }

        public static bool IsAccountant()
        {
            return RoleConstants.IsAccountant(CurrentUser != null ? CurrentUser.VaiTro : null);
        }
    }
}
