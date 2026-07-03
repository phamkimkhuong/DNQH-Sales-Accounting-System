using System;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using DNQH_KeToanBanHang.DataAccess;

namespace DNQH_KeToanBanHang.Helpers
{
    /// <summary>
    /// Helper cung cấp cơ chế tự động đề xuất mã định danh (Business Code)
    /// cho các thực thể danh mục gốc và giao dịch trong hệ thống.
    /// </summary>
    public static class AutoCodeHelper
    {
        private static readonly Regex ValidIdentifierRegex = new Regex(@"^[a-zA-Z0-9_]+$", RegexOptions.Compiled);

        /// <summary>
        /// Thuật toán thuần tính toán mã tiếp theo từ mã lớn nhất hiện có.
        /// Đảm bảo zero-padded đúng định dạng và an toàn cho kiểm thử đơn vị.
        /// </summary>
        /// <param name="currentMaxCode">Mã lớn nhất hiện tại (ví dụ: SP004, KHO02, null/rỗng nếu bảng chưa có dữ liệu)</param>
        /// <param name="prefix">Tiền tố mã (ví dụ: SP, KH, NCC, KHO...)</param>
        /// <param name="defaultPadding">Số lượng chữ số mặc định (ví dụ: 3 cho SP001, 2 cho KHO01)</param>
        /// <returns>Mã tiếp theo khả dụng</returns>
        public static string CalculateNextCode(string currentMaxCode, string prefix, int defaultPadding = 3)
        {
            if (string.IsNullOrEmpty(prefix))
                prefix = string.Empty;

            if (defaultPadding < 1)
                defaultPadding = 1;

            if (string.IsNullOrWhiteSpace(currentMaxCode))
            {
                return string.Format("{0}{1:D" + defaultPadding + "}", prefix, 1);
            }

            string trimmedCode = currentMaxCode.Trim();

            if (trimmedCode.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
            {
                string numPart = trimmedCode.Substring(prefix.Length).Trim();
                int currentNum;
                if (int.TryParse(numPart, out currentNum))
                {
                    int nextNum = currentNum + 1;
                    int padding = Math.Max(defaultPadding, numPart.Length);
                    return string.Format("{0}{1:D" + padding + "}", prefix, nextNum);
                }
            }

            // Trường hợp dữ liệu cũ không theo đúng tiền tố chuẩn, fallback về mã đầu tiên
            return string.Format("{0}{1:D" + defaultPadding + "}", prefix, 1);
        }

        /// <summary>
        /// Truy vấn mã lớn nhất từ CSDL và sinh mã tiếp theo tương ứng.
        /// </summary>
        public static string GetNextCode(string tableName, string columnName, string prefix, int defaultPadding = 3, SqlTransaction trans = null)
        {
            if (string.IsNullOrEmpty(tableName) || !ValidIdentifierRegex.IsMatch(tableName))
                throw new ArgumentException("Tên bảng không hợp lệ.", "tableName");

            if (string.IsNullOrEmpty(columnName) || !ValidIdentifierRegex.IsMatch(columnName))
                throw new ArgumentException("Tên cột không hợp lệ.", "columnName");

            try
            {
                string sql = string.Format(
                    "SELECT TOP 1 RTRIM({0}) FROM {1} WHERE {0} LIKE @prefix ORDER BY LEN(RTRIM({0})) DESC, RTRIM({0}) DESC",
                    columnName,
                    tableName);

                object result;
                if (trans != null)
                {
                    using (SqlCommand cmd = new SqlCommand(sql, trans.Connection, trans))
                    {
                        cmd.Parameters.Add(new SqlParameter("@prefix", SqlDbType.VarChar, 20) { Value = prefix + "%" });
                        result = cmd.ExecuteScalar();
                    }
                }
                else
                {
                    result = Database.ExecuteScalar(sql, new SqlParameter("@prefix", SqlDbType.VarChar, 20) { Value = prefix + "%" });
                }

                string maxCode = (result != null && result != DBNull.Value) ? result.ToString() : null;
                return CalculateNextCode(maxCode, prefix, defaultPadding);
            }
            catch (Exception ex)
            {
                AppLogger.Error("AutoCodeHelper", string.Format("Lỗi sinh mã tự động cho {0}.{1}", tableName, columnName), ex);
                return string.Format("{0}{1:D" + defaultPadding + "}", prefix, 1);
            }
        }

        // ==========================================
        // CÁC HÀM TIỆN ÍCH CHO TỪNG DANH MỤC GỐC
        // ==========================================

        public static string GetNextMaSP(SqlTransaction trans = null)
        {
            return GetNextCode("SANPHAM", "MaSP", "SP", 3, trans);
        }

        public static string GetNextMaKH(SqlTransaction trans = null)
        {
            return GetNextCode("KHACHHANG", "MaKH", "KH", 3, trans);
        }

        public static string GetNextMaNCC(SqlTransaction trans = null)
        {
            return GetNextCode("NHACUNGCAP", "MaNCC", "NCC", 3, trans);
        }

        public static string GetNextMaNV(SqlTransaction trans = null)
        {
            return GetNextCode("NHANVIEN", "MaNV", "NV", 3, trans);
        }

        public static string GetNextMaKho(SqlTransaction trans = null)
        {
            return GetNextCode("KHO", "MaKho", "KHO", 2, trans);
        }

        public static string GetNextMaLoai(SqlTransaction trans = null)
        {
            return GetNextCode("LOAISANPHAM", "MaLoai", "LSP", 3, trans);
        }

        public static string GetNextMaTK(SqlTransaction trans = null)
        {
            return GetNextCode("TAIKHOAN", "MaTK", "TK", 3, trans);
        }
    }
}
