using System;
using System.Data;
using System.Data.SqlClient;
using DNQH_KeToanBanHang.Helpers;
using DNQH_KeToanBanHang.Models;

namespace DNQH_KeToanBanHang.DataAccess
{
    public class LoaiSanPhamDAL
    {
        public DataTable GetAll()
        {
            string query = "SELECT MaLoai, TenLoai, MoTa, ISNULL(Version, 1) AS Version FROM LOAISANPHAM ORDER BY MaLoai";
            return Database.ExecuteQuery(query);
        }

        public LoaiSanPham GetById(string maLoai)
        {
            if (string.IsNullOrWhiteSpace(maLoai))
                return null;

            string query = "SELECT MaLoai, TenLoai, MoTa, ISNULL(Version, 1) AS Version FROM LOAISANPHAM WHERE MaLoai = @MaLoai";
            SqlParameter param = new SqlParameter("@MaLoai", SqlDbType.Char, 10) { Value = maLoai.Trim() };

            DataTable dt = Database.ExecuteQuery(query, param);
            if (dt.Rows.Count == 0)
                return null;

            DataRow row = dt.Rows[0];
            int version = 1;
            if (row.Table.Columns.Contains("Version") && row["Version"] != DBNull.Value)
                int.TryParse(row["Version"].ToString(), out version);

            return new LoaiSanPham
            {
                MaLoai = row["MaLoai"].ToString().Trim(),
                TenLoai = row["TenLoai"] != DBNull.Value ? row["TenLoai"].ToString().Trim() : string.Empty,
                MoTa = row["MoTa"] != DBNull.Value ? row["MoTa"].ToString().Trim() : string.Empty,
                Version = version > 0 ? version : 1
            };
        }

        public bool Exists(string maLoai)
        {
            if (string.IsNullOrWhiteSpace(maLoai))
                return false;

            string query = "SELECT COUNT(*) FROM LOAISANPHAM WHERE MaLoai = @MaLoai";
            SqlParameter param = new SqlParameter("@MaLoai", SqlDbType.Char, 10) { Value = maLoai.Trim() };
            object result = Database.ExecuteScalar(query, param);
            return result != null && Convert.ToInt32(result) > 0;
        }

        private void CheckWritePermission()
        {
            if (!SessionManager.IsLoggedIn)
                throw new UnauthorizedAccessException("Yêu cầu phiên đăng nhập hợp lệ để thực hiện thao tác này.");

            if (!SessionManager.IsAdmin() && !SessionManager.IsSales())
                throw new UnauthorizedAccessException("Chỉ Quản trị viên hoặc Nhân viên bán hàng mới có quyền cập nhật danh mục loại sản phẩm.");
        }

        public bool Insert(LoaiSanPham loai)
        {
            CheckWritePermission();

            string query = "INSERT INTO LOAISANPHAM (MaLoai, TenLoai, MoTa, Version) VALUES (@MaLoai, @TenLoai, @MoTa, 1)";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaLoai", SqlDbType.Char, 10) { Value = loai.MaLoai.Trim() },
                new SqlParameter("@TenLoai", SqlDbType.NVarChar, 100) { Value = (object)loai.TenLoai ?? DBNull.Value },
                new SqlParameter("@MoTa", SqlDbType.NVarChar, 200) { Value = (object)loai.MoTa ?? DBNull.Value }
            };

            return Database.ExecuteNonQuery(query, parameters) > 0;
        }

        public ConcurrencyUpdateResult UpdateWithResult(LoaiSanPham loai)
        {
            CheckWritePermission();

            int currentVersion = loai.Version > 0 ? loai.Version : 1;

            string query = "UPDATE LOAISANPHAM SET TenLoai = @TenLoai, MoTa = @MoTa, " +
                           "Version = ISNULL(Version, 1) + 1 " +
                           "WHERE MaLoai = @MaLoai AND ISNULL(Version, 1) = @Version";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaLoai", SqlDbType.Char, 10) { Value = loai.MaLoai.Trim() },
                new SqlParameter("@TenLoai", SqlDbType.NVarChar, 100) { Value = (object)loai.TenLoai ?? DBNull.Value },
                new SqlParameter("@MoTa", SqlDbType.NVarChar, 200) { Value = (object)loai.MoTa ?? DBNull.Value },
                new SqlParameter("@Version", SqlDbType.Int) { Value = currentVersion }
            };

            int rowsAffected = Database.ExecuteNonQuery(query, parameters);
            if (rowsAffected > 0)
            {
                loai.Version = currentVersion + 1;
                return ConcurrencyUpdateResult.Success;
            }

            if (Exists(loai.MaLoai))
            {
                return ConcurrencyUpdateResult.ConcurrencyConflict;
            }

            return ConcurrencyUpdateResult.NotFoundOrFailed;
        }

        public bool Update(LoaiSanPham loai)
        {
            return UpdateWithResult(loai) == ConcurrencyUpdateResult.Success;
        }

        public bool Delete(string maLoai)
        {
            CheckWritePermission();
            string query = "DELETE FROM LOAISANPHAM WHERE MaLoai = @MaLoai";
            SqlParameter param = new SqlParameter("@MaLoai", SqlDbType.Char, 10) { Value = maLoai.Trim() };
            return Database.ExecuteNonQuery(query, param) > 0;
        }

        public int GetSanPhamCount(string maLoai)
        {
            if (string.IsNullOrWhiteSpace(maLoai))
                return 0;

            string query = "SELECT COUNT(*) FROM SANPHAM WHERE MaLoai = @MaLoai";
            SqlParameter param = new SqlParameter("@MaLoai", SqlDbType.Char, 10) { Value = maLoai.Trim() };
            object result = Database.ExecuteScalar(query, param);
            return result != null && result != DBNull.Value ? Convert.ToInt32(result) : 0;
        }

        public DataTable Search(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return GetAll();

            string query = "SELECT MaLoai, TenLoai, MoTa, ISNULL(Version, 1) AS Version FROM LOAISANPHAM " +
                           "WHERE MaLoai LIKE @Keyword OR TenLoai LIKE @Keyword " +
                           "ORDER BY MaLoai";

            SqlParameter param = new SqlParameter("@Keyword", SqlDbType.NVarChar, 100)
            {
                Value = "%" + keyword.Trim() + "%"
            };

            return Database.ExecuteQuery(query, param);
        }
    }
}
