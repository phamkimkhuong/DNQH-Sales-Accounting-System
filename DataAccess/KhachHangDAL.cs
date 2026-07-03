using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DNQH_KeToanBanHang.Helpers;
using DNQH_KeToanBanHang.Models;

namespace DNQH_KeToanBanHang.DataAccess
{
    public struct KhachHangUsageStats
    {
        public int OrderCount;
        public int InvoiceCount;

        public bool HasUsage
        {
            get { return OrderCount > 0 || InvoiceCount > 0; }
        }
    }

    public class KhachHangDAL
    {
        public DataTable GetAll()
        {
            string query = "SELECT MaKH, TenKH, SoDienThoai, DiaChi, Email, ISNULL(Version, 1) AS Version FROM KHACHHANG ORDER BY MaKH";
            return Database.ExecuteQuery(query);
        }

        public KhachHang GetById(string maKH)
        {
            if (string.IsNullOrWhiteSpace(maKH))
                return null;

            string query = "SELECT MaKH, TenKH, SoDienThoai, DiaChi, Email, ISNULL(Version, 1) AS Version FROM KHACHHANG WHERE MaKH = @MaKH";
            SqlParameter param = new SqlParameter("@MaKH", SqlDbType.Char, 10) { Value = maKH.Trim() };

            DataTable dt = Database.ExecuteQuery(query, param);
            if (dt.Rows.Count == 0)
                return null;

            DataRow row = dt.Rows[0];
            int version = 1;
            if (row.Table.Columns.Contains("Version") && row["Version"] != DBNull.Value)
                int.TryParse(row["Version"].ToString(), out version);

            return new KhachHang
            {
                MaKH = row["MaKH"].ToString().Trim(),
                TenKH = row["TenKH"] != DBNull.Value ? row["TenKH"].ToString().Trim() : string.Empty,
                SoDienThoai = row["SoDienThoai"] != DBNull.Value ? row["SoDienThoai"].ToString().Trim() : string.Empty,
                DiaChi = row["DiaChi"] != DBNull.Value ? row["DiaChi"].ToString().Trim() : string.Empty,
                Email = row["Email"] != DBNull.Value ? row["Email"].ToString().Trim() : string.Empty,
                Version = version > 0 ? version : 1
            };
        }

        public bool Exists(string maKH)
        {
            if (string.IsNullOrWhiteSpace(maKH))
                return false;

            string query = "SELECT COUNT(*) FROM KHACHHANG WHERE MaKH = @MaKH";
            SqlParameter param = new SqlParameter("@MaKH", SqlDbType.Char, 10) { Value = maKH.Trim() };
            object result = Database.ExecuteScalar(query, param);
            return result != null && Convert.ToInt32(result) > 0;
        }

        private void CheckWritePermission()
        {
            if (!SessionManager.IsLoggedIn)
                throw new UnauthorizedAccessException("Yêu cầu phiên đăng nhập hợp lệ để thực hiện thao tác này.");

            if (!SessionManager.IsAdmin() && !SessionManager.IsSales())
                throw new UnauthorizedAccessException("Chỉ Quản trị viên hoặc Nhân viên bán hàng mới có quyền cập nhật danh mục khách hàng.");
        }

        public HashSet<string> GetAllMaKH()
        {
            HashSet<string> set = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            string query = "SELECT RTRIM(MaKH) AS MaKH FROM KHACHHANG";
            DataTable dt = Database.ExecuteQuery(query);
            foreach (DataRow row in dt.Rows)
            {
                if (row["MaKH"] != DBNull.Value)
                    set.Add(row["MaKH"].ToString().Trim());
            }
            return set;
        }

        public bool Insert(KhachHang kh)
        {
            CheckWritePermission();

            string query = "INSERT INTO KHACHHANG (MaKH, TenKH, SoDienThoai, DiaChi, Email, Version) " +
                           "VALUES (@MaKH, @TenKH, @SoDienThoai, @DiaChi, @Email, 1)";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaKH", SqlDbType.Char, 10) { Value = kh.MaKH.Trim() },
                new SqlParameter("@TenKH", SqlDbType.NVarChar, 100) { Value = (object)kh.TenKH ?? DBNull.Value },
                new SqlParameter("@SoDienThoai", SqlDbType.VarChar, 15) { Value = (object)kh.SoDienThoai ?? DBNull.Value },
                new SqlParameter("@DiaChi", SqlDbType.NVarChar, 200) { Value = (object)kh.DiaChi ?? DBNull.Value },
                new SqlParameter("@Email", SqlDbType.VarChar, 100) { Value = (object)kh.Email ?? DBNull.Value }
            };

            return Database.ExecuteNonQuery(query, parameters) > 0;
        }

        public int BulkInsert(IEnumerable<KhachHang> items, SqlTransaction trans = null)
        {
            CheckWritePermission();
            if (items == null) return 0;

            string query = "INSERT INTO KHACHHANG (MaKH, TenKH, SoDienThoai, DiaChi, Email, Version) " +
                           "VALUES (@MaKH, @TenKH, @SoDienThoai, @DiaChi, @Email, 1)";

            if (trans != null)
            {
                int count = 0;
                foreach (KhachHang kh in items)
                {
                    using (SqlCommand cmd = new SqlCommand(query, trans.Connection, trans))
                    {
                        cmd.Parameters.Add(new SqlParameter("@MaKH", SqlDbType.Char, 10) { Value = kh.MaKH.Trim() });
                        cmd.Parameters.Add(new SqlParameter("@TenKH", SqlDbType.NVarChar, 100) { Value = (object)kh.TenKH ?? DBNull.Value });
                        cmd.Parameters.Add(new SqlParameter("@SoDienThoai", SqlDbType.VarChar, 15) { Value = (object)kh.SoDienThoai ?? DBNull.Value });
                        cmd.Parameters.Add(new SqlParameter("@DiaChi", SqlDbType.NVarChar, 200) { Value = (object)kh.DiaChi ?? DBNull.Value });
                        cmd.Parameters.Add(new SqlParameter("@Email", SqlDbType.VarChar, 100) { Value = (object)kh.Email ?? DBNull.Value });
                        count += cmd.ExecuteNonQuery();
                    }
                }
                return count;
            }
            else
            {
                return Database.ExecuteTransaction(t => BulkInsert(items, t));
            }
        }

        public ConcurrencyUpdateResult UpdateWithResult(KhachHang kh)
        {
            CheckWritePermission();

            int currentVersion = kh.Version > 0 ? kh.Version : 1;

            string query = "UPDATE KHACHHANG SET TenKH = @TenKH, SoDienThoai = @SoDienThoai, " +
                           "DiaChi = @DiaChi, Email = @Email, Version = ISNULL(Version, 1) + 1 " +
                           "WHERE MaKH = @MaKH AND ISNULL(Version, 1) = @Version";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaKH", SqlDbType.Char, 10) { Value = kh.MaKH.Trim() },
                new SqlParameter("@TenKH", SqlDbType.NVarChar, 100) { Value = (object)kh.TenKH ?? DBNull.Value },
                new SqlParameter("@SoDienThoai", SqlDbType.VarChar, 15) { Value = (object)kh.SoDienThoai ?? DBNull.Value },
                new SqlParameter("@DiaChi", SqlDbType.NVarChar, 200) { Value = (object)kh.DiaChi ?? DBNull.Value },
                new SqlParameter("@Email", SqlDbType.VarChar, 100) { Value = (object)kh.Email ?? DBNull.Value },
                new SqlParameter("@Version", SqlDbType.Int) { Value = currentVersion }
            };

            int rowsAffected = Database.ExecuteNonQuery(query, parameters);
            if (rowsAffected > 0)
            {
                kh.Version = currentVersion + 1;
                return ConcurrencyUpdateResult.Success;
            }

            if (Exists(kh.MaKH))
            {
                return ConcurrencyUpdateResult.ConcurrencyConflict;
            }

            return ConcurrencyUpdateResult.NotFoundOrFailed;
        }

        public bool Update(KhachHang kh)
        {
            return UpdateWithResult(kh) == ConcurrencyUpdateResult.Success;
        }

        public bool Delete(string maKH)
        {
            CheckWritePermission();
            string query = "DELETE FROM KHACHHANG WHERE MaKH = @MaKH";
            SqlParameter param = new SqlParameter("@MaKH", SqlDbType.Char, 10) { Value = maKH.Trim() };
            return Database.ExecuteNonQuery(query, param) > 0;
        }

        public KhachHangUsageStats GetUsageStatistics(string maKH)
        {
            KhachHangUsageStats stats = new KhachHangUsageStats();
            if (string.IsNullOrWhiteSpace(maKH))
                return stats;

            string query = @"
                SELECT 
                    (SELECT COUNT(*) FROM DONDATHANG WHERE MaKH = @MaKH) AS OrderCount,
                    (SELECT COUNT(*) FROM HOADONBAN WHERE MaKH = @MaKH) AS InvoiceCount;
            ";

            SqlParameter param = new SqlParameter("@MaKH", SqlDbType.Char, 10) { Value = maKH.Trim() };
            DataTable dt = Database.ExecuteQuery(query, param);
            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                stats.OrderCount = row["OrderCount"] != DBNull.Value ? Convert.ToInt32(row["OrderCount"]) : 0;
                stats.InvoiceCount = row["InvoiceCount"] != DBNull.Value ? Convert.ToInt32(row["InvoiceCount"]) : 0;
            }

            return stats;
        }

        public DataTable Search(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return GetAll();

            string query = "SELECT MaKH, TenKH, SoDienThoai, DiaChi, Email, ISNULL(Version, 1) AS Version FROM KHACHHANG " +
                           "WHERE MaKH LIKE @Keyword OR TenKH LIKE @Keyword OR SoDienThoai LIKE @Keyword OR Email LIKE @Keyword " +
                           "ORDER BY MaKH";

            SqlParameter param = new SqlParameter("@Keyword", SqlDbType.NVarChar, 100)
            {
                Value = "%" + keyword.Trim() + "%"
            };

            return Database.ExecuteQuery(query, param);
        }
    }
}
