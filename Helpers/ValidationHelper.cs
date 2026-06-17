using System;
using System.Text.RegularExpressions;

namespace DNQH_KeToanBanHang.Helpers
{
    public static class ValidationHelper
    {
        public static bool IsNotEmpty(string value)
        {
            return !string.IsNullOrWhiteSpace(value);
        }

        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return true; // Email không bắt buộc nếu để trống

            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email.Trim(), pattern, RegexOptions.IgnoreCase);
        }

        public static bool IsValidPhone(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone))
                return true; // Số điện thoại không bắt buộc nếu để trống

            string pattern = @"^[0-9\+\-\.\s]{8,15}$";
            return Regex.IsMatch(phone.Trim(), pattern);
        }

        public static bool IsValidPhoneNumber(string phone)
        {
            return IsValidPhone(phone);
        }

        public static bool TryParseDecimal(string value, out decimal result)
        {
            return decimal.TryParse(value, out result);
        }

        public static bool IsValidDecimal(string value, out decimal result)
        {
            return decimal.TryParse(value, out result);
        }

        public static bool TryParseInt(string value, out int result)
        {
            return int.TryParse(value, out result);
        }
    }
}
