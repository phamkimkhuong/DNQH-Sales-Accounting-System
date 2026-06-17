using System;

namespace DNQH_KeToanBanHang.Constants
{
    public static class OrderStatusConstants
    {
        public const string ChoXuLy = "Chờ xử lý";
        public const string DaDuyet = "Đã duyệt";
        public const string DaLapHoaDon = "Đã lập hóa đơn";
        public const string DaXuatKho = "Đã xuất kho";
        public const string DaHoanThanh = "Đã hoàn thành";
        public const string DaHuy = "Đã hủy";

        // Aliases hỗ trợ tương thích ngược (ví dụ form cũ gõ "Chờ duyệt")
        public const string AliasChoDuyet = "Chờ duyệt";

        public static readonly string[] All = new string[]
        {
            ChoXuLy,
            DaDuyet,
            DaLapHoaDon,
            DaXuatKho,
            DaHoanThanh,
            DaHuy
        };

        public static readonly string[] FilterList = new string[]
        {
            "-- Tất cả --",
            ChoXuLy,
            DaDuyet,
            DaLapHoaDon,
            DaXuatKho,
            DaHoanThanh,
            DaHuy
        };

        public static bool IsPending(string status)
        {
            if (string.IsNullOrWhiteSpace(status)) return false;
            string s = status.Trim();
            return string.Equals(s, ChoXuLy, StringComparison.OrdinalIgnoreCase) ||
                   string.Equals(s, AliasChoDuyet, StringComparison.OrdinalIgnoreCase);
        }

        public static bool IsBilled(string status)
        {
            return string.Equals(status != null ? status.Trim() : string.Empty, DaLapHoaDon, StringComparison.OrdinalIgnoreCase);
        }

        public static bool IsCancelled(string status)
        {
            return string.Equals(status != null ? status.Trim() : string.Empty, DaHuy, StringComparison.OrdinalIgnoreCase);
        }

        public static string Normalize(string status)
        {
            if (string.IsNullOrWhiteSpace(status)) return ChoXuLy;
            string s = status.Trim();
            if (string.Equals(s, AliasChoDuyet, StringComparison.OrdinalIgnoreCase)) return ChoXuLy;
            return s;
        }
    }
}
