using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DNQH_KeToanBanHang.Helpers;
using DNQH_KeToanBanHang.Models;

namespace DNQH_KeToanBanHang.DataAccess
{
    public class NhaCungCapDAL
    {
        public DataTable GetAll()
        {
            string query = "SELECT MaNCC, TenNCC, DiaChi, SoDienThoai, Email, ISNULL(Version, 1) AS Version FROM NHACUNGCAP ORDER BY MaNCC";
            return Database.ExecuteQuery(query);
        }

        public NhaCungCap GetById(string maNCC)
        {
            if (string.IsNullOrWhiteSpace(maNCC))
                return null;

            string query = "SELECT MaNCC, TenNCC, DiaChi, SoDienThoai, Email, ISNULL(Version, 1) AS Version FROM NHACUNGCAP WHERE MaNCC = @MaNCC";
            SqlParameter param = new SqlParameter("@MaNCC", SqlDbType.Char, 10) { Value = maNCC.Trim() };

            DataTable dt = Database.ExecuteQuery(query, param);
            if (dt.Rows.Count == 0)
                return null;

            DataRow row = dt.Rows[0];
            int version = 1;
            if (row.Table.Columns.Contains("Version") && row["Version"] != DBNull.Value)
                int.TryParse(row["Version"].ToString(), out version);

            return new NhaCungCap
            {
                MaNCC = row["MaNCC"].ToString().Trim(),
                TenNCC = row["TenNCC"] != DBNull.Value ? row["TenNCC"].ToString().Trim() : string.Empty,
                DiaChi = row["DiaChi"] != DBNull.Value ? row["DiaChi"].ToString().Trim() : string.Empty,
                SoDienThoai = row["SoDienThoai"] != DBNull.Value ? row["SoDienThoai"].ToString().Trim() : string.Empty,
                Email = row["Email"] != DBNull.Value ? row["Email"].ToString().Trim() : string.Empty,
                Version = version > 0 ? version : 1
            };
        }

        public bool Exists(string maNCC)
        {
            if (string.IsNullOrWhiteSpace(maNCC))
                return false;

            string query = "SELECT COUNT(*) FROM NHACUNGCAP WHERE MaNCC = @MaNCC";
            SqlParameter param = new SqlParameter("@MaNCC", SqlDbType.Char, 10) { Value = maNCC.Trim() };
            object result = Database.ExecuteScalar(query, param);
            return result != null && Convert.ToInt32(result) > 0;
        }

        private void CheckWritePermission()
        {
            if (!SessionManager.IsLoggedIn)
                throw new UnauthorizedAccessException("Yêu cầu phiên đăng nhập hợp lệ để thực hiện thao tác này.");

            if (!SessionManager.IsAdmin() && !SessionManager.IsSales())
                throw new UnauthorizedAccessException("Chỉ Quản trị viên hoặc Nhân viên bán hàng mới có quyền cập nhật danh mục nhà cung cấp.");
        }

        public HashSet<string> GetAllMaNCC()
        {
            HashSet<string> set = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            string query = "SELECT RTRIM(MaNCC) AS MaNCC FROM NHACUNGCAP";
            DataTable dt = Database.ExecuteQuery(query);
            foreach (DataRow row in dt.Rows)
            {
                if (row["MaNCC"] != DBNull.Value)
                    set.Add(row["MaNCC"].ToString().Trim());
            }
            return set;
        }

        public bool Insert(NhaCungCap ncc)
        {
            CheckWritePermission();

            string query = "INSERT INTO NHACUNGCAP (MaNCC, TenNCC, DiaChi, SoDienThoai, Email, Version) " +
                           "VALUES (@MaNCC, @TenNCC, @DiaChi, @SoDienThoai, @Email, 1)";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaNCC", SqlDbType.Char, 10) { Value = ncc.MaNCC.Trim() },
                new SqlParameter("@TenNCC", SqlDbType.NVarChar, 100) { Value = (object)ncc.TenNCC ?? DBNull.Value },
                new SqlParameter("@DiaChi", SqlDbType.NVarChar, 200) { Value = (object)ncc.DiaChi ?? DBNull.Value },
                new SqlParameter("@SoDienThoai", SqlDbType.VarChar, 15) { Value = (object)ncc.SoDienThoai ?? DBNull.Value },
                new SqlParameter("@Email", SqlDbType.VarChar, 100) { Value = (object)ncc.Email ?? DBNull.Value }
            };

            return Database.ExecuteNonQuery(query, parameters) > 0;
        }

        public int BulkInsert(IEnumerable<NhaCungCap> items, SqlTransaction trans = null)
        {
            CheckWritePermission();
            if (items == null) return 0;

            string query = "INSERT INTO NHACUNGCAP (MaNCC, TenNCC, DiaChi, SoDienThoai, Email, Version) " +
                           "VALUES (@MaNCC, @TenNCC, @DiaChi, @SoDienThoai, @Email, 1)";

            if (trans != null)
            {
                int count = 0;
                foreach (NhaCungCap ncc in items)
                {
                    using (SqlCommand cmd = new SqlCommand(query, trans.Connection, trans))
                    {
                        cmd.Parameters.Add(new SqlParameter("@MaNCC", SqlDbType.Char, 10) { Value = ncc.MaNCC.Trim() });
                        cmd.Parameters.Add(new SqlParameter("@TenNCC", SqlDbType.NVarChar, 100) { Value = (object)ncc.TenNCC ?? DBNull.Value });
                        cmd.Parameters.Add(new SqlParameter("@DiaChi", SqlDbType.NVarChar, 200) { Value = (object)ncc.DiaChi ?? DBNull.Value });
                        cmd.Parameters.Add(new SqlParameter("@SoDienThoai", SqlDbType.VarChar, 15) { Value = (object)ncc.SoDienThoai ?? DBNull.Value });
                        cmd.Parameters.Add(new SqlParameter("@Email", SqlDbType.VarChar, 100) { Value = (object)ncc.Email ?? DBNull.Value });
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

        public ConcurrencyUpdateResult UpdateWithResult(NhaCungCap ncc)
        {
            CheckWritePermission();

            int currentVersion = ncc.Version > 0 ? ncc.Version : 1;

            string query = "UPDATE NHACUNGCAP SET TenNCC = @TenNCC, DiaChi = @DiaChi, " +
                           "SoDienThoai = @SoDienThoai, Email = @Email, Version = ISNULL(Version, 1) + 1 " +
                           "WHERE MaNCC = @MaNCC AND ISNULL(Version, 1) = @Version";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaNCC", SqlDbType.Char, 10) { Value = ncc.MaNCC.Trim() },
                new SqlParameter("@TenNCC", SqlDbType.NVarChar, 100) { Value = (object)ncc.TenNCC ?? DBNull.Value },
                new SqlParameter("@DiaChi", SqlDbType.NVarChar, 200) { Value = (object)ncc.DiaChi ?? DBNull.Value },
                new SqlParameter("@SoDienThoai", SqlDbType.VarChar, 15) { Value = (object)ncc.SoDienThoai ?? DBNull.Value },
                new SqlParameter("@Email", SqlDbType.VarChar, 100) { Value = (object)ncc.Email ?? DBNull.Value },
                new SqlParameter("@Version", SqlDbType.Int) { Value = currentVersion }
            };

            int rowsAffected = Database.ExecuteNonQuery(query, parameters);
            if (rowsAffected > 0)
            {
                ncc.Version = currentVersion + 1;
                return ConcurrencyUpdateResult.Success;
            }

            if (Exists(ncc.MaNCC))
            {
                return ConcurrencyUpdateResult.ConcurrencyConflict;
            }

            return ConcurrencyUpdateResult.NotFoundOrFailed;
        }

        public bool Update(NhaCungCap ncc)
        {
            return UpdateWithResult(ncc) == ConcurrencyUpdateResult.Success;
        }

        public bool Delete(string maNCC)
        {
            CheckWritePermission();
            string query = "DELETE FROM NHACUNGCAP WHERE MaNCC = @MaNCC";
            SqlParameter param = new SqlParameter("@MaNCC", SqlDbType.Char, 10) { Value = maNCC.Trim() };
            return Database.ExecuteNonQuery(query, param) > 0;
        }

        public int GetSanPhamCount(string maNCC)
        {
            if (string.IsNullOrWhiteSpace(maNCC))
                return 0;

            string query = "SELECT COUNT(*) FROM SANPHAM WHERE MaNCC = @MaNCC";
            SqlParameter param = new SqlParameter("@MaNCC", SqlDbType.Char, 10) { Value = maNCC.Trim() };
            object result = Database.ExecuteScalar(query, param);
            return result != null && result != DBNull.Value ? Convert.ToInt32(result) : 0;
        }

        public DataTable Search(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return GetAll();

            string query = "SELECT MaNCC, TenNCC, DiaChi, SoDienThoai, Email, ISNULL(Version, 1) AS Version FROM NHACUNGCAP " +
                           "WHERE MaNCC LIKE @Keyword OR TenNCC LIKE @Keyword OR SoDienThoai LIKE @Keyword OR Email LIKE @Keyword " +
                           "ORDER BY MaNCC";

            SqlParameter param = new SqlParameter("@Keyword", SqlDbType.NVarChar, 100)
            {
                Value = "%" + keyword.Trim() + "%"
            };

            return Database.ExecuteQuery(query, param);
        }
    }
}
