using System;
using System.Data;
using System.Data.SqlClient;
using DNQH_KeToanBanHang.Helpers;
using DNQH_KeToanBanHang.Models;

namespace DNQH_KeToanBanHang.DataAccess
{
    public class NhanVienDAL
    {
        public DataTable GetAll()
        {
            string query = "SELECT MaNV, HoTen, NgaySinh, GioiTinh, SoDienThoai, DiaChi, ChucVu, TrangThai, ISNULL(Version, 1) AS Version " +
                           "FROM NHANVIEN ORDER BY MaNV";
            return Database.ExecuteQuery(query);
        }

        public NhanVien GetByMaNV(string maNV)
        {
            if (string.IsNullOrWhiteSpace(maNV))
                return null;

            string query = "SELECT MaNV, HoTen, NgaySinh, GioiTinh, SoDienThoai, DiaChi, ChucVu, TrangThai, ISNULL(Version, 1) AS Version " +
                           "FROM NHANVIEN WHERE MaNV = @MaNV";

            SqlParameter param = new SqlParameter("@MaNV", SqlDbType.Char, 10)
            {
                Value = maNV.Trim()
            };

            DataTable dt = Database.ExecuteQuery(query, param);
            if (dt.Rows.Count == 0)
                return null;

            DataRow row = dt.Rows[0];
            DateTime? ngaySinh = null;
            if (row["NgaySinh"] != DBNull.Value)
            {
                DateTime dtVal;
                if (DateTime.TryParse(row["NgaySinh"].ToString(), out dtVal))
                    ngaySinh = dtVal;
            }

            int version = 1;
            if (row.Table.Columns.Contains("Version") && row["Version"] != DBNull.Value)
                int.TryParse(row["Version"].ToString(), out version);

            return new NhanVien
            {
                MaNV = row["MaNV"].ToString().Trim(),
                HoTen = row["HoTen"] != DBNull.Value ? row["HoTen"].ToString().Trim() : string.Empty,
                NgaySinh = ngaySinh,
                GioiTinh = row["GioiTinh"] != DBNull.Value ? row["GioiTinh"].ToString().Trim() : string.Empty,
                SoDienThoai = row["SoDienThoai"] != DBNull.Value ? row["SoDienThoai"].ToString().Trim() : string.Empty,
                DiaChi = row["DiaChi"] != DBNull.Value ? row["DiaChi"].ToString().Trim() : string.Empty,
                ChucVu = row["ChucVu"] != DBNull.Value ? row["ChucVu"].ToString().Trim() : string.Empty,
                TrangThai = row["TrangThai"] != DBNull.Value ? row["TrangThai"].ToString().Trim() : string.Empty,
                Version = version > 0 ? version : 1
            };
        }

        public bool Exists(string maNV)
        {
            if (string.IsNullOrWhiteSpace(maNV))
                return false;

            string query = "SELECT COUNT(*) FROM NHANVIEN WHERE MaNV = @MaNV";
            SqlParameter param = new SqlParameter("@MaNV", SqlDbType.Char, 10) { Value = maNV.Trim() };
            object result = Database.ExecuteScalar(query, param);
            return result != null && Convert.ToInt32(result) > 0;
        }

        private void CheckWritePermission()
        {
            if (!SessionManager.IsLoggedIn)
                throw new UnauthorizedAccessException("Yêu cầu phiên đăng nhập hợp lệ để thực hiện thao tác này.");

            if (!SessionManager.IsAdmin())
                throw new UnauthorizedAccessException("Chỉ Quản trị viên mới có quyền thực hiện thao tác quản lý nhân viên.");
        }

        public bool Insert(NhanVien nv)
        {
            CheckWritePermission();

            string query = "INSERT INTO NHANVIEN (MaNV, HoTen, NgaySinh, GioiTinh, SoDienThoai, DiaChi, ChucVu, TrangThai, Version) " +
                           "VALUES (@MaNV, @HoTen, @NgaySinh, @GioiTinh, @SoDienThoai, @DiaChi, @ChucVu, @TrangThai, 1)";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaNV", SqlDbType.Char, 10) { Value = nv.MaNV.Trim() },
                new SqlParameter("@HoTen", SqlDbType.NVarChar, 100) { Value = (object)nv.HoTen ?? DBNull.Value },
                new SqlParameter("@NgaySinh", SqlDbType.Date) { Value = nv.NgaySinh.HasValue ? (object)nv.NgaySinh.Value : DBNull.Value },
                new SqlParameter("@GioiTinh", SqlDbType.NVarChar, 10) { Value = (object)nv.GioiTinh ?? DBNull.Value },
                new SqlParameter("@SoDienThoai", SqlDbType.VarChar, 15) { Value = (object)nv.SoDienThoai ?? DBNull.Value },
                new SqlParameter("@DiaChi", SqlDbType.NVarChar, 200) { Value = (object)nv.DiaChi ?? DBNull.Value },
                new SqlParameter("@ChucVu", SqlDbType.NVarChar, 50) { Value = (object)nv.ChucVu ?? DBNull.Value },
                new SqlParameter("@TrangThai", SqlDbType.NVarChar, 30) { Value = (object)nv.TrangThai ?? DBNull.Value }
            };

            return Database.ExecuteNonQuery(query, parameters) > 0;
        }

        public ConcurrencyUpdateResult UpdateWithResult(NhanVien nv)
        {
            CheckWritePermission();

            int currentVersion = nv.Version > 0 ? nv.Version : 1;

            string query = "UPDATE NHANVIEN SET HoTen = @HoTen, NgaySinh = @NgaySinh, GioiTinh = @GioiTinh, " +
                           "SoDienThoai = @SoDienThoai, DiaChi = @DiaChi, ChucVu = @ChucVu, TrangThai = @TrangThai, " +
                           "Version = ISNULL(Version, 1) + 1 " +
                           "WHERE MaNV = @MaNV AND ISNULL(Version, 1) = @Version";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaNV", SqlDbType.Char, 10) { Value = nv.MaNV.Trim() },
                new SqlParameter("@HoTen", SqlDbType.NVarChar, 100) { Value = (object)nv.HoTen ?? DBNull.Value },
                new SqlParameter("@NgaySinh", SqlDbType.Date) { Value = nv.NgaySinh.HasValue ? (object)nv.NgaySinh.Value : DBNull.Value },
                new SqlParameter("@GioiTinh", SqlDbType.NVarChar, 10) { Value = (object)nv.GioiTinh ?? DBNull.Value },
                new SqlParameter("@SoDienThoai", SqlDbType.VarChar, 15) { Value = (object)nv.SoDienThoai ?? DBNull.Value },
                new SqlParameter("@DiaChi", SqlDbType.NVarChar, 200) { Value = (object)nv.DiaChi ?? DBNull.Value },
                new SqlParameter("@ChucVu", SqlDbType.NVarChar, 50) { Value = (object)nv.ChucVu ?? DBNull.Value },
                new SqlParameter("@TrangThai", SqlDbType.NVarChar, 30) { Value = (object)nv.TrangThai ?? DBNull.Value },
                new SqlParameter("@Version", SqlDbType.Int) { Value = currentVersion }
            };

            int rowsAffected = Database.ExecuteNonQuery(query, parameters);
            if (rowsAffected > 0)
            {
                nv.Version = currentVersion + 1;
                return ConcurrencyUpdateResult.Success;
            }

            if (Exists(nv.MaNV))
            {
                return ConcurrencyUpdateResult.ConcurrencyConflict;
            }

            return ConcurrencyUpdateResult.NotFoundOrFailed;
        }

        public bool Update(NhanVien nv)
        {
            return UpdateWithResult(nv) == ConcurrencyUpdateResult.Success;
        }

        public bool Delete(string maNV)
        {
            CheckWritePermission();
            string query = "DELETE FROM NHANVIEN WHERE MaNV = @MaNV";
            SqlParameter param = new SqlParameter("@MaNV", SqlDbType.Char, 10) { Value = maNV.Trim() };
            return Database.ExecuteNonQuery(query, param) > 0;
        }

        public DataTable Search(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return GetAll();

            string query = "SELECT MaNV, HoTen, NgaySinh, GioiTinh, SoDienThoai, DiaChi, ChucVu, TrangThai, ISNULL(Version, 1) AS Version " +
                           "FROM NHANVIEN WHERE MaNV LIKE @Keyword OR HoTen LIKE @Keyword OR SoDienThoai LIKE @Keyword " +
                           "ORDER BY MaNV";

            SqlParameter param = new SqlParameter("@Keyword", SqlDbType.NVarChar, 100)
            {
                Value = "%" + keyword.Trim() + "%"
            };

            return Database.ExecuteQuery(query, param);
        }
    }
}
