using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DNQH_KeToanBanHang.Helpers;
using DNQH_KeToanBanHang.Models;

namespace DNQH_KeToanBanHang.DataAccess
{
    public class PhieuChiDAL
    {
        public static void CheckWritePermission()
        {
            if (!SessionManager.IsLoggedIn || SessionManager.CurrentUser == null)
            {
                throw new UnauthorizedAccessException("Yêu cầu đăng nhập trước khi thực hiện thao tác lập phiếu chi.");
            }

            if (!SessionManager.IsAdmin() && !SessionManager.IsAccountant())
            {
                throw new UnauthorizedAccessException("Bạn không có quyền lập Phiếu chi. Chỉ Quản trị viên và Nhân viên kế toán mới có quyền này.");
            }
        }

        public string GetNextMaPC(SqlTransaction trans = null)
        {
            string sql = "SELECT TOP 1 MaPC FROM PHIEUCHI WITH (TABLOCKX, HOLDLOCK) ORDER BY MaPC DESC";
            object result;

            if (trans != null)
            {
                using (SqlCommand cmd = new SqlCommand(sql, trans.Connection, trans))
                {
                    result = cmd.ExecuteScalar();
                }
            }
            else
            {
                result = Database.ExecuteScalar(sql);
            }

            if (result != null && result != DBNull.Value)
            {
                string maxCode = result.ToString().Trim();
                if (maxCode.StartsWith("PC") && maxCode.Length == 10)
                {
                    string numPart = maxCode.Substring(2);
                    int currentNum;
                    if (int.TryParse(numPart, out currentNum))
                    {
                        return string.Format("PC{0:D8}", currentNum + 1);
                    }
                }
            }

            return "PC00000001";
        }

        public bool Insert(PhieuChi pc, SqlTransaction trans = null)
        {
            CheckWritePermission();

            string sql = @"
                INSERT INTO PHIEUCHI (MaPC, MaNV, NgayChi, NguoiNhan, LyDoChi, SoTien, HinhThuc, GhiChu)
                VALUES (@MaPC, @MaNV, @NgayChi, @NguoiNhan, @LyDoChi, @SoTien, @HinhThuc, @GhiChu)";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaPC", SqlDbType.Char, 10) { Value = pc.MaPC },
                new SqlParameter("@MaNV", SqlDbType.Char, 10) { Value = pc.MaNV },
                new SqlParameter("@NgayChi", SqlDbType.DateTime) { Value = (object)pc.NgayChi ?? DateTime.Now },
                new SqlParameter("@NguoiNhan", SqlDbType.NVarChar, 100) { Value = (object)pc.NguoiNhan ?? DBNull.Value },
                new SqlParameter("@LyDoChi", SqlDbType.NVarChar, 200) { Value = (object)pc.LyDoChi ?? DBNull.Value },
                new SqlParameter("@SoTien", SqlDbType.Decimal) { Value = pc.SoTien },
                new SqlParameter("@HinhThuc", SqlDbType.NVarChar, 20) { Value = (object)pc.HinhThuc ?? "Tiền mặt" },
                new SqlParameter("@GhiChu", SqlDbType.NVarChar, 200) { Value = (object)pc.GhiChu ?? DBNull.Value }
            };

            int affected;
            if (trans != null)
            {
                using (SqlCommand cmd = new SqlCommand(sql, trans.Connection, trans))
                {
                    cmd.Parameters.AddRange(parameters);
                    affected = cmd.ExecuteNonQuery();
                }
            }
            else
            {
                affected = Database.ExecuteNonQuery(sql, parameters);
            }

            return affected > 0;
        }

        public DataTable GetAll(DateTime? fromDate = null, DateTime? toDate = null)
        {
            if (fromDate.HasValue || toDate.HasValue)
            {
                return Search("", fromDate, toDate);
            }

            string sql = @"
                SELECT pc.MaPC, pc.NgayChi, pc.SoTien, pc.HinhThuc, pc.NguoiNhan, pc.LyDoChi, pc.GhiChu,
                       pc.MaNV, nv.HoTen AS TenNV
                FROM PHIEUCHI pc
                INNER JOIN NHANVIEN nv ON pc.MaNV = nv.MaNV
                ORDER BY pc.NgayChi DESC, pc.MaPC DESC";

            return Database.ExecuteQuery(sql);
        }

        public DataTable Search(string keyword, DateTime? fromDate = null, DateTime? toDate = null)
        {
            string sql = @"
                SELECT pc.MaPC, pc.NgayChi, pc.SoTien, pc.HinhThuc, pc.NguoiNhan, pc.LyDoChi, pc.GhiChu,
                       pc.MaNV, nv.HoTen AS TenNV
                FROM PHIEUCHI pc
                INNER JOIN NHANVIEN nv ON pc.MaNV = nv.MaNV
                WHERE 1 = 1 ";

            List<SqlParameter> prms = new List<SqlParameter>();

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                sql += " AND (pc.MaPC LIKE @KW OR pc.NguoiNhan LIKE @KW OR pc.LyDoChi LIKE @KW OR nv.HoTen LIKE @KW) ";
                prms.Add(new SqlParameter("@KW", SqlDbType.NVarChar, 100) { Value = "%" + keyword.Trim() + "%" });
            }

            if (fromDate.HasValue)
            {
                sql += " AND pc.NgayChi >= @FromDate ";
                prms.Add(new SqlParameter("@FromDate", SqlDbType.DateTime) { Value = fromDate.Value.Date });
            }

            if (toDate.HasValue)
            {
                sql += " AND pc.NgayChi < @ToDate ";
                prms.Add(new SqlParameter("@ToDate", SqlDbType.DateTime) { Value = toDate.Value.Date.AddDays(1) });
            }

            sql += " ORDER BY pc.NgayChi DESC, pc.MaPC DESC";

            return Database.ExecuteQuery(sql, prms.ToArray());
        }

        public PhieuChi GetById(string maPC)
        {
            string sql = @"
                SELECT pc.MaPC, pc.MaNV, pc.NgayChi, pc.NguoiNhan, pc.LyDoChi, pc.SoTien, pc.HinhThuc, pc.GhiChu,
                       nv.HoTen AS TenNV
                FROM PHIEUCHI pc
                INNER JOIN NHANVIEN nv ON pc.MaNV = nv.MaNV
                WHERE pc.MaPC = @MaPC";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaPC", SqlDbType.Char, 10) { Value = maPC }
            };

            DataTable dt = Database.ExecuteQuery(sql, parameters);
            if (dt.Rows.Count == 0)
            {
                return null;
            }

            DataRow r = dt.Rows[0];
            return new PhieuChi
            {
                MaPC = r["MaPC"].ToString().Trim(),
                MaNV = r["MaNV"].ToString().Trim(),
                NgayChi = r["NgayChi"] != DBNull.Value ? Convert.ToDateTime(r["NgayChi"]) : (DateTime?)null,
                NguoiNhan = r["NguoiNhan"] != DBNull.Value ? r["NguoiNhan"].ToString().Trim() : "",
                LyDoChi = r["LyDoChi"] != DBNull.Value ? r["LyDoChi"].ToString().Trim() : "",
                SoTien = r["SoTien"] != DBNull.Value ? Convert.ToDecimal(r["SoTien"]) : 0m,
                HinhThuc = r["HinhThuc"] != DBNull.Value ? r["HinhThuc"].ToString().Trim() : "",
                GhiChu = r["GhiChu"] != DBNull.Value ? r["GhiChu"].ToString().Trim() : "",
                TenNV = r["TenNV"] != DBNull.Value ? r["TenNV"].ToString().Trim() : ""
            };
        }
    }
}
