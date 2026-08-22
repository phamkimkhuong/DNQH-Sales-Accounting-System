using System;
using System.IO;
using System.Text;

namespace DNQH_KeToanBanHang.Helpers
{
    /// <summary>
    /// Tiện ích lưu trữ và tải tùy chọn người dùng trên máy cục bộ (User Preferences).
    /// Áp dụng cho tính năng "Ghi nhớ tên đăng nhập" an toàn (chỉ lưu Username, tuyệt đối không lưu mật khẩu).
    /// </summary>
    public static class UserPreferenceHelper
    {
        private static readonly object _lock = new object();
        private static readonly string ConfigDir;
        private static readonly string ConfigFile;

        static UserPreferenceHelper()
        {
            try
            {
                string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                if (string.IsNullOrEmpty(appData))
                {
                    appData = AppDomain.CurrentDomain.BaseDirectory;
                }
                ConfigDir = Path.Combine(appData, "DNQH_KeToanBanHang");
                ConfigFile = Path.Combine(ConfigDir, "user_preferences.ini");
            }
            catch
            {
                ConfigDir = AppDomain.CurrentDomain.BaseDirectory;
                ConfigFile = Path.Combine(ConfigDir, "user_preferences.ini");
            }
        }

        /// <summary>
        /// Lấy trạng thái tùy chọn ghi nhớ tên đăng nhập.
        /// </summary>
        public static bool GetRememberUsername()
        {
            lock (_lock)
            {
                try
                {
                    if (!File.Exists(ConfigFile)) return false;

                    string[] lines = File.ReadAllLines(ConfigFile, Encoding.UTF8);
                    foreach (string line in lines)
                    {
                        string trimmed = line.Trim();
                        if (trimmed.StartsWith("RememberUsername=", StringComparison.OrdinalIgnoreCase))
                        {
                            string val = trimmed.Substring("RememberUsername=".Length).Trim();
                            bool result;
                            if (bool.TryParse(val, out result))
                            {
                                return result;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    AppLogger.Warn("UserPreferenceHelper", "Không thể đọc tùy chọn RememberUsername: " + ex.Message);
                }
                return false;
            }
        }

        /// <summary>
        /// Lấy tên đăng nhập đã được ghi nhớ từ phiên làm việc trước.
        /// </summary>
        public static string GetSavedUsername()
        {
            lock (_lock)
            {
                try
                {
                    if (!File.Exists(ConfigFile)) return string.Empty;

                    string[] lines = File.ReadAllLines(ConfigFile, Encoding.UTF8);
                    foreach (string line in lines)
                    {
                        string trimmed = line.Trim();
                        if (trimmed.StartsWith("SavedUsername=", StringComparison.OrdinalIgnoreCase))
                        {
                            return trimmed.Substring("SavedUsername=".Length).Trim();
                        }
                    }
                }
                catch (Exception ex)
                {
                    AppLogger.Warn("UserPreferenceHelper", "Không thể đọc tên đăng nhập đã lưu: " + ex.Message);
                }
                return string.Empty;
            }
        }

        /// <summary>
        /// Lưu tùy chọn ghi nhớ tên đăng nhập.
        /// Nếu rememberUsername = false, tự động xóa tên đã lưu để bảo đảm riêng tư.
        /// </summary>
        public static void SaveLoginPreference(bool rememberUsername, string username)
        {
            lock (_lock)
            {
                try
                {
                    if (!Directory.Exists(ConfigDir))
                    {
                        Directory.CreateDirectory(ConfigDir);
                    }

                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("[LoginPreferences]");
                    sb.AppendLine(string.Format("RememberUsername={0}", rememberUsername));
                    sb.AppendLine(string.Format("SavedUsername={0}", rememberUsername ? (username ?? string.Empty).Trim() : string.Empty));
                    sb.AppendLine(string.Format("LastUpdated={0:yyyy-MM-dd HH:mm:ss}", DateTime.Now));

                    File.WriteAllText(ConfigFile, sb.ToString(), Encoding.UTF8);
                }
                catch (Exception ex)
                {
                    AppLogger.Warn("UserPreferenceHelper", "Không thể ghi tùy chọn đăng nhập: " + ex.Message);
                }
            }
        }
    }
}
