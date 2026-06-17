using System;

namespace DNQH_KeToanBanHang.Constants
{
    public static class InvoiceStatusConstants
    {
        public const string ChuaThanhToan = "Chưa thanh toán";
        public const string ThanhToanMotPhan = "Thanh toán một phần";
        public const string DaThanhToan = "Đã thanh toán";
        public const string DaHuy = "Đã hủy";

        public static readonly string[] All = new string[]
        {
            ChuaThanhToan,
            ThanhToanMotPhan,
            DaThanhToan,
            DaHuy
        };

        public static readonly string[] FilterList = new string[]
        {
            "-- Tất cả --",
            ChuaThanhToan,
            ThanhToanMotPhan,
            DaThanhToan,
            DaHuy
        };

        public static bool IsFullyPaid(string status)
        {
            return string.Equals(status != null ? status.Trim() : string.Empty, DaThanhToan, StringComparison.OrdinalIgnoreCase);
        }

        public static bool IsPartiallyPaid(string status)
        {
            return string.Equals(status != null ? status.Trim() : string.Empty, ThanhToanMotPhan, StringComparison.OrdinalIgnoreCase);
        }

        public static bool IsUnpaid(string status)
        {
            return string.Equals(status != null ? status.Trim() : string.Empty, ChuaThanhToan, StringComparison.OrdinalIgnoreCase);
        }

        public static bool IsCancelled(string status)
        {
            if (string.IsNullOrWhiteSpace(status)) return false;
            string s = status.Trim();
            return string.Equals(s, DaHuy, StringComparison.OrdinalIgnoreCase) ||
                   string.Equals(s, "Hủy", StringComparison.OrdinalIgnoreCase);
        }
    }
}
