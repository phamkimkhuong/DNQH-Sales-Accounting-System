using System;

namespace DNQH_KeToanBanHang.Constants
{
    public static class EntityStatusConstants
    {
        /// <summary>
        /// Trạng thái sản phẩm
        /// </summary>
        public static class Product
        {
            public const string Active = "Đang kinh doanh";
            public const string Discontinued = "Ngừng kinh doanh"; // Chuẩn hóa duy nhất chữ 'ừ'
            public const string LegacyDiscontinued = "Ngưng kinh doanh"; // Tương thích ngược chữ 'ư'

            public static readonly string[] All = new string[]
            {
                Active,
                Discontinued
            };

            public static bool IsActive(string status)
            {
                if (string.IsNullOrWhiteSpace(status)) return false;
                string s = status.Trim();
                return string.Equals(s, Active, StringComparison.OrdinalIgnoreCase) ||
                       string.Equals(s, "Hoạt động", StringComparison.OrdinalIgnoreCase) ||
                       string.Equals(s, "Active", StringComparison.OrdinalIgnoreCase);
            }

            public static bool IsDiscontinued(string status)
            {
                if (string.IsNullOrWhiteSpace(status)) return false;
                string s = status.Trim();
                return string.Equals(s, Discontinued, StringComparison.OrdinalIgnoreCase) ||
                       string.Equals(s, LegacyDiscontinued, StringComparison.OrdinalIgnoreCase);
            }

            public static string Normalize(string status)
            {
                if (string.IsNullOrWhiteSpace(status)) return Active;
                string s = status.Trim();
                if (IsDiscontinued(s)) return Discontinued;
                if (IsActive(s)) return Active;
                return s;
            }
        }

        /// <summary>
        /// Trạng thái tài khoản người dùng
        /// </summary>
        public static class Account
        {
            public const string Active = "Hoạt động";
            public const string Locked = "Bị khóa";

            public static readonly string[] All = new string[]
            {
                Active,
                Locked
            };

            public static bool IsActive(string status)
            {
                if (string.IsNullOrWhiteSpace(status)) return false;
                string s = status.Trim();
                return string.Equals(s, Active, StringComparison.OrdinalIgnoreCase) ||
                       string.Equals(s, "Hoat dong", StringComparison.OrdinalIgnoreCase) ||
                       string.Equals(s, "Active", StringComparison.OrdinalIgnoreCase);
            }

            public static bool IsLocked(string status)
            {
                if (string.IsNullOrWhiteSpace(status)) return false;
                string s = status.Trim();
                return string.Equals(s, Locked, StringComparison.OrdinalIgnoreCase) ||
                       string.Equals(s, "Khóa", StringComparison.OrdinalIgnoreCase) ||
                       string.Equals(s, "Tạm khóa", StringComparison.OrdinalIgnoreCase) ||
                       string.Equals(s, "Ngừng hoạt động", StringComparison.OrdinalIgnoreCase);
            }
        }

        /// <summary>
        /// Trạng thái kho hàng
        /// </summary>
        public static class Warehouse
        {
            public const string Active = "Hoạt động";
            public const string Inactive = "Tạm ngừng";

            public static readonly string[] All = new string[]
            {
                Active,
                Inactive
            };

            public static bool IsActive(string status)
            {
                if (string.IsNullOrWhiteSpace(status)) return false;
                string s = status.Trim();
                return string.Equals(s, Active, StringComparison.OrdinalIgnoreCase) ||
                       string.Equals(s, "Đang hoạt động", StringComparison.OrdinalIgnoreCase) ||
                       string.Equals(s, "Active", StringComparison.OrdinalIgnoreCase);
            }

            public static bool IsInactive(string status)
            {
                if (string.IsNullOrWhiteSpace(status)) return false;
                string s = status.Trim();
                return string.Equals(s, Inactive, StringComparison.OrdinalIgnoreCase) ||
                       string.Equals(s, "Tạm khóa", StringComparison.OrdinalIgnoreCase) ||
                       string.Equals(s, "Ngừng hoạt động", StringComparison.OrdinalIgnoreCase) ||
                       string.Equals(s, "Đóng cửa", StringComparison.OrdinalIgnoreCase);
            }
        }

        /// <summary>
        /// Trạng thái nhân viên
        /// </summary>
        public static class Employee
        {
            public const string Active = "Đang làm việc";
            public const string Resigned = "Đã nghỉ việc";

            public static readonly string[] All = new string[]
            {
                Active,
                Resigned
            };

            public static bool IsActive(string status)
            {
                if (string.IsNullOrWhiteSpace(status)) return false;
                string s = status.Trim();
                return string.Equals(s, Active, StringComparison.OrdinalIgnoreCase) ||
                       string.Equals(s, "Dang lam viec", StringComparison.OrdinalIgnoreCase) ||
                       string.Equals(s, "Hoạt động", StringComparison.OrdinalIgnoreCase) ||
                       string.Equals(s, "Active", StringComparison.OrdinalIgnoreCase);
            }
        }

        /// <summary>
        /// Trạng thái phiếu xuất kho
        /// </summary>
        public static class Export
        {
            public const string DaXuat = "Đã xuất";
            public const string ChoXuat = "Chờ xuất";
            public const string DaHuy = "Đã hủy";

            public static readonly string[] All = new string[]
            {
                DaXuat,
                ChoXuat,
                DaHuy
            };
        }
    }
}
