using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace DNQH_KeToanBanHang.Helpers
{
    /// <summary>
    /// Tiện ích ghi log tập trung
    /// Hỗ trợ Rolling file log theo ngày và bối cảnh có cấu trúc (CorrelationId, UserId, DurationMs).
    /// </summary>
    public static class AppLogger
    {
        private static readonly object _lockObj = new object();
        private static string _logDirectory;

        static AppLogger()
        {
            try
            {
                _logDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs");
                if (!Directory.Exists(_logDirectory))
                {
                    Directory.CreateDirectory(_logDirectory);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Không thể khởi tạo thư mục log: " + ex.Message);
            }
        }

        public static string LogDirectory
        {
            get
            {
                if (string.IsNullOrEmpty(_logDirectory))
                {
                    _logDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs");
                }
                return _logDirectory;
            }
        }

        /// <summary>
        /// Tự động dọn dẹp các file log quá hạn (mặc định 30 ngày).
        /// Trả về số lượng file đã xóa thành công.
        /// </summary>
        public static int CleanOldLogs(int retainDays = 30)
        {
            if (retainDays <= 0) return 0;
            int deletedCount = 0;
            try
            {
                string logDir = LogDirectory;
                if (!Directory.Exists(logDir))
                {
                    return 0;
                }

                DateTime cutoffDate = DateTime.Today.AddDays(-retainDays);
                string[] logFiles = Directory.GetFiles(logDir, "*.log");

                foreach (string filePath in logFiles)
                {
                    try
                    {
                        string fileName = Path.GetFileNameWithoutExtension(filePath);
                        bool shouldDelete = false;

                        // Tên mẫu: app-yyyy-MM-dd hoặc error-yyyy-MM-dd
                        int dateDashIdx = fileName.IndexOf('-');
                        if (dateDashIdx > 0 && fileName.Length >= dateDashIdx + 11)
                        {
                            string datePart = fileName.Substring(dateDashIdx + 1);
                            DateTime logDate;
                            if (DateTime.TryParseExact(datePart, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out logDate))
                            {
                                if (logDate < cutoffDate)
                                {
                                    shouldDelete = true;
                                }
                            }
                        }

                        // Kiểm tra bổ sung nếu không trích xuất được từ tên
                        if (!shouldDelete)
                        {
                            FileInfo fi = new FileInfo(filePath);
                            if (fi.LastWriteTime < cutoffDate)
                            {
                                shouldDelete = true;
                            }
                        }

                        if (shouldDelete)
                        {
                            File.Delete(filePath);
                            deletedCount++;
                        }
                    }
                    catch (Exception exFile)
                    {
                        Console.WriteLine("Không thể xóa file log cũ " + filePath + ": " + exFile.Message);
                    }
                }

                if (deletedCount > 0)
                {
                    Info("LogRetention", string.Format("Đã tự động dọn dẹp {0} file nhật ký cũ hơn {1} ngày.", deletedCount, retainDays));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi trong quá trình dọn dẹp log: " + ex.Message);
            }

            return deletedCount;
        }

        public static void Info(string operation, string message, string correlationId = null, string userId = null, long durationMs = 0, string entityType = null, string entityId = null, string role = null, string result = "Success")
        {
            WriteLog("INFO", operation, message, correlationId, userId, durationMs, null, entityType, entityId, role, result);
        }

        public static void Warn(string operation, string message, string correlationId = null, string userId = null, long durationMs = 0, string entityType = null, string entityId = null, string role = null, string result = null)
        {
            WriteLog("WARN", operation, message, correlationId, userId, durationMs, null, entityType, entityId, role, result);
        }

        public static void Error(string operation, string message, Exception ex = null, string correlationId = null, string userId = null, long durationMs = 0, string entityType = null, string entityId = null, string role = null, string result = "Failed")
        {
            WriteLog("ERROR", operation, message, correlationId, userId, durationMs, ex, entityType, entityId, role, result);
        }

        private static void WriteLog(string level, string operation, string message, string correlationId, string userId, long durationMs, Exception ex, string entityType = null, string entityId = null, string role = null, string result = null)
        {
            try
            {
                DateTime now = DateTime.Now;
                lock (_lockObj)
                {
                    if (string.IsNullOrEmpty(_logDirectory))
                    {
                        _logDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs");
                        if (!Directory.Exists(_logDirectory))
                        {
                            Directory.CreateDirectory(_logDirectory);
                        }
                    }

                    string dateStr = now.ToString("yyyy-MM-dd");
                    string logFileName = level == "ERROR" ? string.Format("error-{0}.log", dateStr) : string.Format("app-{0}.log", dateStr);
                    string logFilePath = Path.Combine(_logDirectory, logFileName);

                    StringBuilder sb = new StringBuilder();
                    sb.Append(now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
                    sb.Append(" [").Append(level.PadRight(5)).Append("] ");
                    sb.Append("Operation=").Append(operation ?? "Unknown");

                    if (!string.IsNullOrEmpty(correlationId))
                    {
                        sb.Append(" CorrelationId=").Append(correlationId);
                    }

                    if (!string.IsNullOrEmpty(userId))
                    {
                        sb.Append(" UserId=").Append(userId);
                    }

                    if (!string.IsNullOrEmpty(role))
                    {
                        sb.Append(" Role=").Append(role);
                    }

                    if (!string.IsNullOrEmpty(entityType))
                    {
                        sb.Append(" EntityType=").Append(entityType);
                    }

                    if (!string.IsNullOrEmpty(entityId))
                    {
                        sb.Append(" EntityId=").Append(entityId);
                    }

                    if (!string.IsNullOrEmpty(result))
                    {
                        sb.Append(" Result=").Append(result);
                    }

                    if (durationMs > 0)
                    {
                        sb.Append(" Duration=").Append(durationMs).Append("ms");
                    }

                    sb.Append(" - ").Append(message);

                    if (ex != null)
                    {
                        sb.AppendLine();
                        sb.Append("   Exception: ").Append(ex.GetType().FullName).Append(": ").Append(ex.Message);
                        sb.AppendLine();
                        sb.Append("   StackTrace: ").Append(ex.StackTrace);
                    }

                    using (StreamWriter sw = new StreamWriter(logFilePath, true, Encoding.UTF8))
                    {
                        sw.WriteLine(sb.ToString());
                    }

                    // Nếu là ERROR, cũng đồng thời ghi một dòng tóm tắt vào app-yyyy-MM-dd.log
                    if (level == "ERROR")
                    {
                        string generalLogPath = Path.Combine(_logDirectory, string.Format("app-{0}.log", dateStr));
                        using (StreamWriter sw = new StreamWriter(generalLogPath, true, Encoding.UTF8))
                        {
                            sw.WriteLine(string.Format("{0} [ERROR] Operation={1} CorrelationId={2} UserId={3} Role={4} Entity={5}:{6} Result={7} Duration={8}ms - {9} (Chi tiết xem error-{10}.log)",
                                now.ToString("yyyy-MM-dd HH:mm:ss.fff"),
                                operation ?? "Unknown",
                                correlationId ?? "None",
                                userId ?? "None",
                                role ?? "None",
                                entityType ?? "None",
                                entityId ?? "None",
                                result ?? "Failed",
                                durationMs,
                                message,
                                dateStr));
                        }
                    }
                }
            }
            catch (Exception writeEx)
            {
                Console.WriteLine("Lỗi ghi log: " + writeEx.Message);
            }
        }
    }
}
