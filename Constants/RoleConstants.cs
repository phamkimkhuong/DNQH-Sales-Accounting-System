using System;

namespace DNQH_KeToanBanHang.Constants
{
    public static class RoleConstants
    {
        public const string Admin = "Quản trị viên";
        public const string Sales = "Nhân viên bán hàng";
        public const string Warehouse = "Nhân viên kho";
        public const string Accountant = "Nhân viên kế toán";

        // Aliases hỗ trợ tương thích ngược
        public const string AliasAdmin = "Admin";
        public const string AliasSales = "BanHang";
        public const string AliasWarehouse = "Kho";
        public const string AliasAccountant = "KeToan";

        public static readonly string[] AllRoles = new string[]
        {
            Admin,
            Sales,
            Warehouse,
            Accountant
        };

        public static bool IsAdmin(string role)
        {
            if (string.IsNullOrWhiteSpace(role)) return false;
            string r = role.Trim();
            return string.Equals(r, Admin, StringComparison.OrdinalIgnoreCase) ||
                   string.Equals(r, AliasAdmin, StringComparison.OrdinalIgnoreCase);
        }

        public static bool IsSales(string role)
        {
            if (string.IsNullOrWhiteSpace(role)) return false;
            string r = role.Trim();
            return string.Equals(r, Sales, StringComparison.OrdinalIgnoreCase) ||
                   string.Equals(r, AliasSales, StringComparison.OrdinalIgnoreCase);
        }

        public static bool IsWarehouse(string role)
        {
            if (string.IsNullOrWhiteSpace(role)) return false;
            string r = role.Trim();
            return string.Equals(r, Warehouse, StringComparison.OrdinalIgnoreCase) ||
                   string.Equals(r, AliasWarehouse, StringComparison.OrdinalIgnoreCase);
        }

        public static bool IsAccountant(string role)
        {
            if (string.IsNullOrWhiteSpace(role)) return false;
            string r = role.Trim();
            return string.Equals(r, Accountant, StringComparison.OrdinalIgnoreCase) ||
                   string.Equals(r, AliasAccountant, StringComparison.OrdinalIgnoreCase);
        }

        public static bool IsValidRole(string role)
        {
            if (string.IsNullOrWhiteSpace(role)) return false;
            string r = role.Trim();
            return string.Equals(r, Admin, StringComparison.OrdinalIgnoreCase) ||
                   string.Equals(r, Sales, StringComparison.OrdinalIgnoreCase) ||
                   string.Equals(r, Warehouse, StringComparison.OrdinalIgnoreCase) ||
                   string.Equals(r, Accountant, StringComparison.OrdinalIgnoreCase);
        }

        public static string NormalizeRole(string role)
        {
            if (string.IsNullOrWhiteSpace(role)) return string.Empty;
            if (IsAdmin(role)) return Admin;
            if (IsSales(role)) return Sales;
            if (IsWarehouse(role)) return Warehouse;
            if (IsAccountant(role)) return Accountant;
            return role.Trim();
        }
    }
}
