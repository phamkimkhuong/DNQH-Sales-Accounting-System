using System;
using System.Security.Cryptography;
using System.Text;

namespace DNQH_KeToanBanHang.Helpers
{
    /// <summary>
    /// Tiện ích băm và xác thực mật khẩu.
    /// Hash mới dùng PBKDF2-HMAC-SHA256; các hash PBKDF2-HMAC-SHA1/S1 cũ chỉ
    /// được giữ để đăng nhập và tự nâng cấp, không chấp nhận mật khẩu plaintext.
    /// </summary>
    public static class SecurityHelper
    {
        private const string CurrentPrefix = "PBKDF2-SHA256:";
        private const string LegacyPbkdf2Prefix = "PBKDF2:";
        private const string LegacySha256Prefix = "S1:";
        private const int CurrentIterations = 100000;
        private const int SaltSize = 16;
        private const int HashSize = 32;

        public static string HashPassword(string plainPassword)
        {
            if (string.IsNullOrEmpty(plainPassword))
                return string.Empty;

            byte[] saltBytes = new byte[SaltSize];
            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(saltBytes);
            }

            byte[] hashBytes;
            using (Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(
                plainPassword, saltBytes, CurrentIterations, HashAlgorithmName.SHA256))
            {
                hashBytes = pbkdf2.GetBytes(HashSize);
            }

            return string.Format(
                "{0}{1}:{2}:{3}",
                CurrentPrefix,
                CurrentIterations,
                Convert.ToBase64String(saltBytes),
                Convert.ToBase64String(hashBytes));
        }

        public static bool VerifyPassword(string inputPassword, string storedPassword)
        {
            if (string.IsNullOrEmpty(inputPassword) || string.IsNullOrEmpty(storedPassword))
                return false;

            try
            {
                if (storedPassword.StartsWith(CurrentPrefix, StringComparison.Ordinal))
                    return VerifyPbkdf2(inputPassword, storedPassword, CurrentPrefix, HashAlgorithmName.SHA256);

                // Định dạng Phase 3/4 cũ dùng overload mặc định HMAC-SHA1.
                if (storedPassword.StartsWith(LegacyPbkdf2Prefix, StringComparison.Ordinal))
                    return VerifyPbkdf2(inputPassword, storedPassword, LegacyPbkdf2Prefix, null);

                if (storedPassword.StartsWith(LegacySha256Prefix, StringComparison.Ordinal))
                {
                    string[] parts = storedPassword.Split(':');
                    if (parts.Length != 3)
                        return false;

                    string actualHash = ComputeLegacySha256(inputPassword + parts[1]);
                    return FixedTimeEquals(parts[2], actualHash);
                }
            }
            catch (FormatException)
            {
                return false;
            }
            catch (ArgumentException)
            {
                return false;
            }
            catch (CryptographicException)
            {
                return false;
            }

            // Chuỗi không có định dạng hash hợp lệ luôn bị từ chối.
            return false;
        }

        public static bool IsHashed(string storedPassword)
        {
            return !string.IsNullOrEmpty(storedPassword) &&
                   (storedPassword.StartsWith(CurrentPrefix, StringComparison.Ordinal) ||
                    storedPassword.StartsWith(LegacyPbkdf2Prefix, StringComparison.Ordinal) ||
                    storedPassword.StartsWith(LegacySha256Prefix, StringComparison.Ordinal));
        }

        public static bool IsCurrentHash(string storedPassword)
        {
            return !string.IsNullOrEmpty(storedPassword) &&
                   storedPassword.StartsWith(
                       CurrentPrefix + CurrentIterations + ":",
                       StringComparison.Ordinal);
        }

        private static bool VerifyPbkdf2(
            string inputPassword,
            string storedPassword,
            string prefix,
            HashAlgorithmName? algorithm)
        {
            string[] parts = storedPassword.Substring(prefix.Length).Split(':');
            int iterations;
            if (parts.Length != 3 || !int.TryParse(parts[0], out iterations) ||
                iterations <= 0 || iterations > 1000000)
                return false;

            byte[] saltBytes = Convert.FromBase64String(parts[1]);
            byte[] expectedHash = Convert.FromBase64String(parts[2]);
            if (saltBytes.Length < 8 || expectedHash.Length < 16)
                return false;

            byte[] actualHash;
            if (algorithm.HasValue)
            {
                using (Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(
                    inputPassword, saltBytes, iterations, algorithm.Value))
                {
                    actualHash = pbkdf2.GetBytes(expectedHash.Length);
                }
            }
            else
            {
                using (Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(
                    inputPassword, saltBytes, iterations))
                {
                    actualHash = pbkdf2.GetBytes(expectedHash.Length);
                }
            }

            return SlowEquals(expectedHash, actualHash);
        }

        private static bool SlowEquals(byte[] a, byte[] b)
        {
            if (a == null || b == null || a.Length != b.Length)
                return false;

            int diff = 0;
            for (int i = 0; i < a.Length; i++)
                diff |= a[i] ^ b[i];

            return diff == 0;
        }

        private static bool FixedTimeEquals(string a, string b)
        {
            if (a == null || b == null)
                return false;

            return SlowEquals(Encoding.UTF8.GetBytes(a), Encoding.UTF8.GetBytes(b));
        }

        private static string ComputeLegacySha256(string rawData)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(rawData));
                StringBuilder sb = new StringBuilder(bytes.Length * 2);
                for (int i = 0; i < bytes.Length; i++)
                    sb.Append(bytes[i].ToString("x2"));

                return sb.ToString();
            }
        }
    }
}
