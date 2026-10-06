using System;
using System.Data;
using System.Data.SqlClient;
using DNQH_KeToanBanHang.Helpers;
using DNQH_KeToanBanHang.Models;

namespace DNQH_KeToanBanHang.DataAccess
{
    public class KhoDAL
    {
        public DataTable GetAll()
        {
            string query = "SELECT MaKho, TenKho, DiaChi, TrangThai, ISNULL(Version, 1) AS Version FROM KHO ORDER BY MaKho";
            return Database.ExecuteQuery(query);
        }

        public Kho GetById(string maKho)
        {
            if (string.IsNullOrWhiteSpace(maKho))
                return null;

            string query = "SELECT MaKho, TenKho, DiaChi, TrangThai, ISNULL(Version, 1) AS Version FROM KHO WHERE MaKho = @MaKho";
            SqlParameter param = new SqlParameter("@MaKho", SqlDbType.Char, 10) { Value = maKho.Trim() };

            DataTable dt = Database.ExecuteQuery(query, param);
            if (dt.Rows.Count == 0)
                return null;

            DataRow row = dt.Rows[0];
            int version = 1;
            if (row.Table.Columns.Contains("Version") && row["Version"] != DBNull.Value)
                int.TryParse(row["Version"].ToString(), out version);

            return new Kho
            {
                MaKho = row["MaKho"].ToString().Trim(),
                TenKho = row["TenKho"] != DBNull.Value ? row["TenKho"].ToString().Trim() : string.Empty,
                DiaChi = row["DiaChi"] != DBNull.Value ? row["DiaChi"].ToString().Trim() : string.Empty,
                TrangThai = row["TrangThai"] != DBNull.Value ? row["TrangThai"].ToString().Trim() : string.Empty,
                Version = version > 0 ? version : 1
            };
        }

        public bool Exists(string maKho)
        {
            if (string.IsNullOrWhiteSpace(maKho))
                return false;

            string query = "SELECT COUNT(*) FROM KHO WHERE MaKho = @MaKho";
            SqlParameter param = new SqlParameter("@MaKho", SqlDbType.Char, 10) { Value = maKho.Trim() };
            object result = Database.ExecuteScalar(query, param);
            return result != null && Convert.ToInt32(result) > 0;
        }

        private void CheckWritePermission()
        {
            if (!SessionManager.IsLoggedIn)
                throw new UnauthorizedAccessException("Yêu cầu phiên đăng nhập hợp lệ để thực hiện thao tác này.");

            if (!SessionManager.IsAdmin() && !SessionManager.IsWarehouse())
                throw new UnauthorizedAccessException("Chỉ Quản trị viên hoặc Nhân viên kho mới có quyền cập nhật danh mục kho.");
        }

        public bool Insert(Kho k)
        {
            CheckWritePermission();

            string query = "INSERT INTO KHO (MaKho, TenKho, DiaChi, TrangThai, Version) VALUES (@MaKho, @TenKho, @DiaChi, @TrangThai, 1)";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaKho", SqlDbType.Char, 10) { Value = k.MaKho.Trim() },
                new SqlParameter("@TenKho", SqlDbType.NVarChar, 100) { Value = (object)k.TenKho ?? DBNull.Value },
                new SqlParameter("@DiaChi", SqlDbType.NVarChar, 200) { Value = (object)k.DiaChi ?? DBNull.Value },
                new SqlParameter("@TrangThai", SqlDbType.NVarChar, 30) { Value = (object)k.TrangThai ?? DBNull.Value }
            };

            return Database.ExecuteNonQuery(query, parameters) > 0;
        }

        public ConcurrencyUpdateResult UpdateWithResult(Kho k)
        {
            CheckWritePermission();

            int currentVersion = k.Version > 0 ? k.Version : 1;

            string query = "UPDATE KHO SET TenKho = @TenKho, DiaChi = @DiaChi, TrangThai = @TrangThai, " +
                           "Version = ISNULL(Version, 1) + 1 " +
                           "WHERE MaKho = @MaKho AND ISNULL(Version, 1) = @Version";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaKho", SqlDbType.Char, 10) { Value = k.MaKho.Trim() },
                new SqlParameter("@TenKho", SqlDbType.NVarChar, 100) { Value = (object)k.TenKho ?? DBNull.Value },
                new SqlParameter("@DiaChi", SqlDbType.NVarChar, 200) { Value = (object)k.DiaChi ?? DBNull.Value },
                new SqlParameter("@TrangThai", SqlDbType.NVarChar, 30) { Value = (object)k.TrangThai ?? DBNull.Value },
                new SqlParameter("@Version", SqlDbType.Int) { Value = currentVersion }
            };

            int rowsAffected = Database.ExecuteNonQuery(query, parameters);
            if (rowsAffected > 0)
            {
                k.Version = currentVersion + 1;
                return ConcurrencyUpdateResult.Success;
            }

            if (Exists(k.MaKho))
            {
                return ConcurrencyUpdateResult.ConcurrencyConflict;
            }

            return ConcurrencyUpdateResult.NotFoundOrFailed;
        }

        public bool Update(Kho k)
        {
            return UpdateWithResult(k) == ConcurrencyUpdateResult.Success;
        }

        public bool Delete(string maKho)
        {
            CheckWritePermission();
            string query = "DELETE FROM KHO WHERE MaKho = @MaKho";
            SqlParameter param = new SqlParameter("@MaKho", SqlDbType.Char, 10) { Value = maKho.Trim() };
            return Database.ExecuteNonQuery(query, param) > 0;
        }

        public KhoUsageStats GetUsageStatistics(string maKho)
        {
            KhoUsageStats stats = new KhoUsageStats();
            if (string.IsNullOrWhiteSpace(maKho))
                return stats;

            string query = @"
                SELECT 
                    (SELECT COUNT(*) FROM TONKHO WHERE MaKho = @MaKho) AS TonKhoCount,
                    (SELECT COUNT(*) FROM PHIEUXUATKHO WHERE MaKho = @MaKho) AS PhieuXuatCount;
            ";

            SqlParameter param = new SqlParameter("@MaKho", SqlDbType.Char, 10) { Value = maKho.Trim() };
            DataTable dt = Database.ExecuteQuery(query, param);
            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                stats.TonKhoCount = row["TonKhoCount"] != DBNull.Value ? Convert.ToInt32(row["TonKhoCount"]) : 0;
                stats.PhieuXuatCount = row["PhieuXuatCount"] != DBNull.Value ? Convert.ToInt32(row["PhieuXuatCount"]) : 0;
            }

            return stats;
        }

        public DataTable Search(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return GetAll();

            string query = "SELECT MaKho, TenKho, DiaChi, TrangThai, ISNULL(Version, 1) AS Version FROM KHO " +
                           "WHERE MaKho LIKE @Keyword OR TenKho LIKE @Keyword OR DiaChi LIKE @Keyword " +
                           "ORDER BY MaKho";

            SqlParameter param = new SqlParameter("@Keyword", SqlDbType.NVarChar, 100)
            {
                Value = "%" + keyword.Trim() + "%"
            };

            return Database.ExecuteQuery(query, param);
        }
    }

    public struct KhoUsageStats
    {
        public int TonKhoCount;
        public int PhieuXuatCount;

        public bool HasUsage
        {
            get { return TonKhoCount > 0 || PhieuXuatCount > 0; }
        }
    }
}
