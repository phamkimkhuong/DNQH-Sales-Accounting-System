using System;
using System.Collections.Generic;
using System.Text;

namespace DNQH_KeToanBanHang.Helpers
{
    /// <summary>
    /// Lớp tiện ích chuyển đổi số tiền và chữ số sang chuỗi tiếng Việt chuẩn quy chuẩn tài chính kế toán.
    /// Hỗ trợ đọc số âm, số 0, hàng trăm, chục, đơn vị (lẻ/linh, mốt, lăm), nghìn, triệu, tỷ.
    /// </summary>
    public static class VietnameseNumberReader
    {
        private static readonly string[] Digits =
        {
            "không", "một", "hai", "ba", "bốn", "năm", "sáu", "bảy", "tám", "chín"
        };

        private static readonly string[] ScaleUnits =
        {
            "", "nghìn", "triệu", "tỷ", "nghìn tỷ", "triệu tỷ", "tỷ tỷ"
        };

        /// <summary>
        /// Chuyển đổi số tiền sang chuỗi đọc bằng chữ tiếng Việt có đơn vị tiền tệ (mặc định "đồng chẵn.").
        /// Ví dụ: 1250000 -> "Một triệu hai trăm năm mươi nghìn đồng chẵn."
        /// </summary>
        /// <param name="amount">Số tiền cần đọc</param>
        /// <param name="currencyUnit">Đơn vị tiền tệ (mặc định: "đồng")</param>
        /// <param name="appendChan">Nếu true, thêm "chẵn" khi không có số thập phân lẻ</param>
        /// <returns>Chuỗi số tiền viết bằng chữ, viết hoa chữ cái đầu tiên</returns>
        public static string ReadMoney(decimal amount, string currencyUnit = "đồng", bool appendChan = true)
        {
            if (amount == 0)
            {
                string zeroSuffix = appendChan ? " chẵn." : ".";
                return CapitalizeFirst("không " + currencyUnit + zeroSuffix);
            }

            bool isNegative = amount < 0;
            decimal absAmount = Math.Abs(amount);
            long integerPart = (long)Math.Floor(absAmount);
            decimal fractionalPart = absAmount - integerPart;

            string words = ReadNumber(integerPart);

            StringBuilder sb = new StringBuilder();
            if (isNegative)
            {
                sb.Append("âm ");
            }

            sb.Append(words);
            sb.Append(" ");
            sb.Append(currencyUnit);

            // Xử lý phần thập phân nếu có (ít gặp trong VND nhưng hỗ trợ đầy đủ)
            if (fractionalPart > 0)
            {
                // Làm tròn lấy 2 chữ số thập phân nếu có
                int fracVal = (int)Math.Round(fractionalPart * 100);
                if (fracVal > 0)
                {
                    sb.Append(" và ");
                    sb.Append(ReadNumber(fracVal));
                    sb.Append(" xu");
                }
                sb.Append(".");
            }
            else
            {
                if (appendChan)
                {
                    sb.Append(" chẵn.");
                }
                else
                {
                    sb.Append(".");
                }
            }

            return CapitalizeFirst(sb.ToString().Trim());
        }

        /// <summary>
        /// Đọc một số nguyên dương sang chuỗi tiếng Việt.
        /// </summary>
        /// <param name="number">Số nguyên cần đọc</param>
        /// <returns>Chuỗi chữ thường tiếng Việt</returns>
        public static string ReadNumber(long number)
        {
            if (number == 0)
            {
                return "không";
            }

            if (number < 0)
            {
                return "âm " + ReadNumber(Math.Abs(number));
            }

            List<int> groups = new List<int>();
            long temp = number;
            while (temp > 0)
            {
                groups.Add((int)(temp % 1000));
                temp /= 1000;
            }

            List<string> resultParts = new List<string>();
            bool hasHigherNonZero = false;

            // Duyệt từ nhóm cao nhất xuống nhóm thấp nhất
            for (int i = groups.Count - 1; i >= 0; i--)
            {
                int groupVal = groups[i];
                if (groupVal > 0)
                {
                    string groupText = ReadThreeDigits(groupVal, hasHigherNonZero);
                    string scale = i < ScaleUnits.Length ? ScaleUnits[i] : "";

                    string part = string.IsNullOrEmpty(scale)
                        ? groupText
                        : groupText + " " + scale;

                    resultParts.Add(part.Trim());
                    hasHigherNonZero = true;
                }
            }

            return string.Join(" ", resultParts.ToArray()).Trim();
        }

        /// <summary>
        /// Đọc nhóm 3 chữ số (0 - 999).
        /// </summary>
        private static string ReadThreeDigits(int number, bool hasHigherGroup)
        {
            int tram = number / 100;
            int chuc = (number % 100) / 10;
            int donvi = number % 10;

            List<string> parts = new List<string>();

            // Hàng trăm
            if (tram > 0)
            {
                parts.Add(Digits[tram] + " trăm");
            }
            else if (hasHigherGroup)
            {
                // Có nhóm lớn hơn trước đó thì đọc "không trăm"
                parts.Add("không trăm");
            }

            // Hàng chục
            if (chuc > 1)
            {
                parts.Add(Digits[chuc] + " mươi");
            }
            else if (chuc == 1)
            {
                parts.Add("mười");
            }
            else if (chuc == 0)
            {
                if (parts.Count > 0 && donvi > 0)
                {
                    parts.Add("lẻ");
                }
            }

            // Hàng đơn vị
            if (donvi > 0)
            {
                if (donvi == 1)
                {
                    if (chuc >= 2)
                    {
                        parts.Add("mốt");
                    }
                    else
                    {
                        parts.Add("một");
                    }
                }
                else if (donvi == 5)
                {
                    if (chuc >= 1)
                    {
                        parts.Add("lăm");
                    }
                    else
                    {
                        parts.Add("năm");
                    }
                }
                else
                {
                    parts.Add(Digits[donvi]);
                }
            }

            return string.Join(" ", parts.ToArray()).Trim();
        }

        /// <summary>
        /// Viết hoa chữ cái đầu tiên của chuỗi.
        /// </summary>
        public static string CapitalizeFirst(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return string.Empty;
            }

            text = text.Trim();
            if (text.Length == 1)
            {
                return text.ToUpper();
            }

            return char.ToUpper(text[0]) + text.Substring(1);
        }
    }
}
