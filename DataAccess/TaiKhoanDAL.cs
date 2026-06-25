using System;
using System.Data;
using System.Data.SqlClient;
using DNQH_KeToanBanHang.Helpers;
using DNQH_KeToanBanHang.Models;

namespace DNQH_KeToanBanHang.DataAccess
{
    public class TaiKhoanDAL
    {
        public DataTable GetAll()
        {
            string query = "SELECT t.MaTK, t.MaNV, nv.HoTen, t.TenDangNhap, t.VaiTro, t.TrangThai, ISNULL(t.Version, 1) AS Version " +
                           "FROM TAIKHOAN t " +
                           "LEFT JOIN NHANVIEN nv ON t.MaNV = nv.MaNV " +
                           "ORDER BY t.MaTK";

            return Database.ExecuteQuery(query);
        }

        public TaiKhoan GetByTenDangNhap(string tenDangNhap)
        {
            if (string.IsNullOrWhiteSpace(tenDangNhap))
                return null;

            string query = "SELECT MaTK, MaNV, TenDangNhap, MatKhau, VaiTro, TrangThai, ISNULL(Version, 1) AS Version " +
                           "FROM TAIKHOAN WHERE TenDangNhap = @TenDangNhap";

            SqlParameter param = new SqlParameter("@TenDangNhap", SqlDbType.VarChar, 50)
            {
                Value = tenDangNhap.Trim()
            };

            DataTable dt = Database.ExecuteQuery(query, param);
            if (dt.Rows.Count == 0)
                return null;

            DataRow row = dt.Rows[0];
            int version = 1;
            if (row.Table.Columns.Contains("Version") && row["Version"] != DBNull.Value)
                int.TryParse(row["Version"].ToString(), out version);

            return new TaiKhoan
            {
                MaTK = row["MaTK"] != DBNull.Value ? row["MaTK"].ToString().Trim() : string.Empty,
                MaNV = row["MaNV"] != DBNull.Value ? row["MaNV"].ToString().Trim() : string.Empty,
                TenDangNhap = row["TenDangNhap"] != DBNull.Value ? row["TenDangNhap"].ToString().Trim() : string.Empty,
                MatKhau = row["MatKhau"] != DBNull.Value ? row["MatKhau"].ToString().Trim() : string.Empty,
                VaiTro = row["VaiTro"] != DBNull.Value ? row["VaiTro"].ToString().Trim() : string.Empty,
                TrangThai = row["TrangThai"] != DBNull.Value ? row["TrangThai"].ToString().Trim() : string.Empty,
                Version = version > 0 ? version : 1
            };
        }

        public TaiKhoan GetById(string maTK)
        {
            if (string.IsNullOrWhiteSpace(maTK))
                return null;

            string query = "SELECT MaTK, MaNV, TenDangNhap, MatKhau, VaiTro, TrangThai, ISNULL(Version, 1) AS Version " +
                           "FROM TAIKHOAN WHERE MaTK = @MaTK";

            SqlParameter param = new SqlParameter("@MaTK", SqlDbType.Char, 10)
            {
                Value = maTK.Trim()
            };

            DataTable dt = Database.ExecuteQuery(query, param);
            if (dt.Rows.Count == 0)
                return null;

            DataRow row = dt.Rows[0];
            int version = 1;
            if (row.Table.Columns.Contains("Version") && row["Version"] != DBNull.Value)
                int.TryParse(row["Version"].ToString(), out version);

            return new TaiKhoan
            {
                MaTK = row["MaTK"] != DBNull.Value ? row["MaTK"].ToString().Trim() : string.Empty,
                MaNV = row["MaNV"] != DBNull.Value ? row["MaNV"].ToString().Trim() : string.Empty,
                TenDangNhap = row["TenDangNhap"] != DBNull.Value ? row["TenDangNhap"].ToString().Trim() : string.Empty,
                MatKhau = row["MatKhau"] != DBNull.Value ? row["MatKhau"].ToString().Trim() : string.Empty,
                VaiTro = row["VaiTro"] != DBNull.Value ? row["VaiTro"].ToString().Trim() : string.Empty,
                TrangThai = row["TrangThai"] != DBNull.Value ? row["TrangThai"].ToString().Trim() : string.Empty,
                Version = version > 0 ? version : 1
            };
        }

        public bool Exists(string maTK)
        {
            if (string.IsNullOrWhiteSpace(maTK))
                return false;

            string query = "SELECT COUNT(*) FROM TAIKHOAN WHERE MaTK = @MaTK";
            SqlParameter param = new SqlParameter("@MaTK", SqlDbType.Char, 10) { Value = maTK.Trim() };
            object result = Database.ExecuteScalar(query, param);
            return result != null && Convert.ToInt32(result) > 0;
        }

        public bool ExistsUsername(string username, string excludeMaTK = null)
        {
            if (string.IsNullOrWhiteSpace(username))
                return false;

            string query = "SELECT COUNT(*) FROM TAIKHOAN WHERE TenDangNhap = @TenDangNhap";
            if (!string.IsNullOrEmpty(excludeMaTK))
                query += " AND MaTK <> @ExcludeMaTK";

            SqlParameter[] parameters;
            if (!string.IsNullOrEmpty(excludeMaTK))
            {
                parameters = new SqlParameter[]
                {
                    new SqlParameter("@TenDangNhap", SqlDbType.VarChar, 50) { Value = username.Trim() },
                    new SqlParameter("@ExcludeMaTK", SqlDbType.Char, 10) { Value = excludeMaTK.Trim() }
                };
            }
            else
            {
                parameters = new SqlParameter[]
                {
                    new SqlParameter("@TenDangNhap", SqlDbType.VarChar, 50) { Value = username.Trim() }
                };
            }

            object result = Database.ExecuteScalar(query, parameters);
            return result != null && Convert.ToInt32(result) > 0;
        }

        public bool ExistsMaNV(string maNV, string excludeMaTK = null)
        {
            if (string.IsNullOrWhiteSpace(maNV))
                return false;

            string query = "SELECT COUNT(*) FROM TAIKHOAN WHERE MaNV = @MaNV";
            if (!string.IsNullOrEmpty(excludeMaTK))
                query += " AND MaTK <> @ExcludeMaTK";

            SqlParameter[] parameters;
            if (!string.IsNullOrEmpty(excludeMaTK))
            {
                parameters = new SqlParameter[]
                {
                    new SqlParameter("@MaNV", SqlDbType.Char, 10) { Value = maNV.Trim() },
                    new SqlParameter("@ExcludeMaTK", SqlDbType.Char, 10) { Value = excludeMaTK.Trim() }
                };
            }
            else
            {
                parameters = new SqlParameter[]
                {
                    new SqlParameter("@MaNV", SqlDbType.Char, 10) { Value = maNV.Trim() }
                };
            }

            object result = Database.ExecuteScalar(query, parameters);
            return result != null && Convert.ToInt32(result) > 0;
        }

        private void CheckWritePermission()
        {
            if (!SessionManager.IsLoggedIn)
                throw new UnauthorizedAccessException("Yêu cầu phiên đăng nhập hợp lệ để thực hiện thao tác này.");

            if (!SessionManager.IsAdmin())
                throw new UnauthorizedAccessException("Chỉ Quản trị viên mới có quyền thực hiện thao tác quản lý tài khoản.");
        }

        public bool Insert(TaiKhoan tk)
        {
            CheckWritePermission();

            string query = "INSERT INTO TAIKHOAN (MaTK, MaNV, TenDangNhap, MatKhau, VaiTro, TrangThai, Version) " +
                           "VALUES (@MaTK, @MaNV, @TenDangNhap, @MatKhau, @VaiTro, @TrangThai, 1)";

            string passToSave = SecurityHelper.HashPassword(tk.MatKhau);

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaTK", SqlDbType.Char, 10) { Value = tk.MaTK.Trim() },
                new SqlParameter("@MaNV", SqlDbType.Char, 10) { Value = tk.MaNV.Trim() },
                new SqlParameter("@TenDangNhap", SqlDbType.VarChar, 50) { Value = tk.TenDangNhap.Trim() },
                new SqlParameter("@MatKhau", SqlDbType.VarChar, 100) { Value = passToSave },
                new SqlParameter("@VaiTro", SqlDbType.NVarChar, 20) { Value = (object)tk.VaiTro ?? DBNull.Value },
                new SqlParameter("@TrangThai", SqlDbType.NVarChar, 30) { Value = (object)tk.TrangThai ?? DBNull.Value }
            };

            return Database.ExecuteNonQuery(query, parameters) > 0;
        }

        public ConcurrencyUpdateResult UpdateWithResult(TaiKhoan tk, bool changePassword = false)
        {
            CheckWritePermission();

            int currentVersion = tk.Version > 0 ? tk.Version : 1;
            string query;
            SqlParameter[] parameters;

            if (changePassword && !string.IsNullOrEmpty(tk.MatKhau))
            {
                string passToSave = SecurityHelper.HashPassword(tk.MatKhau);
                query = "UPDATE TAIKHOAN SET MaNV = @MaNV, TenDangNhap = @TenDangNhap, MatKhau = @MatKhau, " +
                        "VaiTro = @VaiTro, TrangThai = @TrangThai, Version = ISNULL(Version, 1) + 1 " +
                        "WHERE MaTK = @MaTK AND ISNULL(Version, 1) = @Version";

                parameters = new SqlParameter[]
                {
                    new SqlParameter("@MaTK", SqlDbType.Char, 10) { Value = tk.MaTK.Trim() },
                    new SqlParameter("@MaNV", SqlDbType.Char, 10) { Value = tk.MaNV.Trim() },
                    new SqlParameter("@TenDangNhap", SqlDbType.VarChar, 50) { Value = tk.TenDangNhap.Trim() },
                    new SqlParameter("@MatKhau", SqlDbType.VarChar, 100) { Value = passToSave },
                    new SqlParameter("@VaiTro", SqlDbType.NVarChar, 20) { Value = (object)tk.VaiTro ?? DBNull.Value },
                    new SqlParameter("@TrangThai", SqlDbType.NVarChar, 30) { Value = (object)tk.TrangThai ?? DBNull.Value },
                    new SqlParameter("@Version", SqlDbType.Int) { Value = currentVersion }
                };
            }
            else
            {
                query = "UPDATE TAIKHOAN SET MaNV = @MaNV, TenDangNhap = @TenDangNhap, " +
                        "VaiTro = @VaiTro, TrangThai = @TrangThai, Version = ISNULL(Version, 1) + 1 " +
                        "WHERE MaTK = @MaTK AND ISNULL(Version, 1) = @Version";

                parameters = new SqlParameter[]
                {
                    new SqlParameter("@MaTK", SqlDbType.Char, 10) { Value = tk.MaTK.Trim() },
                    new SqlParameter("@MaNV", SqlDbType.Char, 10) { Value = tk.MaNV.Trim() },
                    new SqlParameter("@TenDangNhap", SqlDbType.VarChar, 50) { Value = tk.TenDangNhap.Trim() },
                    new SqlParameter("@VaiTro", SqlDbType.NVarChar, 20) { Value = (object)tk.VaiTro ?? DBNull.Value },
                    new SqlParameter("@TrangThai", SqlDbType.NVarChar, 30) { Value = (object)tk.TrangThai ?? DBNull.Value },
                    new SqlParameter("@Version", SqlDbType.Int) { Value = currentVersion }
                };
            }

            int rowsAffected = Database.ExecuteNonQuery(query, parameters);
            if (rowsAffected > 0)
            {
                tk.Version = currentVersion + 1;
                return ConcurrencyUpdateResult.Success;
            }

            if (Exists(tk.MaTK))
            {
                return ConcurrencyUpdateResult.ConcurrencyConflict;
            }

            return ConcurrencyUpdateResult.NotFoundOrFailed;
        }

        public bool Update(TaiKhoan tk, bool changePassword = false)
        {
            return UpdateWithResult(tk, changePassword) == ConcurrencyUpdateResult.Success;
        }

        public bool DoiMatKhau(string maTK, string matKhauMoi)
        {
            if (string.IsNullOrWhiteSpace(maTK) || string.IsNullOrWhiteSpace(matKhauMoi))
                return false;

            string query = "UPDATE TAIKHOAN SET MatKhau = @MatKhauMoi, Version = ISNULL(Version, 1) + 1 WHERE MaTK = @MaTK";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MatKhauMoi", SqlDbType.VarChar, 100) { Value = matKhauMoi.Trim() },
                new SqlParameter("@MaTK", SqlDbType.Char, 10) { Value = maTK.Trim() }
            };

            int rows = Database.ExecuteNonQuery(query, parameters);
            return rows > 0;
        }

        public bool Delete(string maTK)
        {
            CheckWritePermission();
            string query = "DELETE FROM TAIKHOAN WHERE MaTK = @MaTK";
            SqlParameter param = new SqlParameter("@MaTK", SqlDbType.Char, 10) { Value = maTK.Trim() };
            return Database.ExecuteNonQuery(query, param) > 0;
        }

        public DataTable Search(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return GetAll();

            string query = "SELECT t.MaTK, t.MaNV, nv.HoTen, t.TenDangNhap, t.VaiTro, t.TrangThai, ISNULL(t.Version, 1) AS Version " +
                           "FROM TAIKHOAN t " +
                           "LEFT JOIN NHANVIEN nv ON t.MaNV = nv.MaNV " +
                           "WHERE t.MaTK LIKE @Keyword OR t.TenDangNhap LIKE @Keyword OR nv.HoTen LIKE @Keyword " +
                           "ORDER BY t.MaTK";

            SqlParameter param = new SqlParameter("@Keyword", SqlDbType.NVarChar, 100)
            {
                Value = "%" + keyword.Trim() + "%"
            };

            return Database.ExecuteQuery(query, param);
        }
    }
}
