using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DNQH_KeToanBanHang.Helpers;
using DNQH_KeToanBanHang.Models;

namespace DNQH_KeToanBanHang.DataAccess
{
    public class PhieuThuDAL
    {
        public static void CheckWritePermission()
        {
            if (!SessionManager.IsLoggedIn || SessionManager.CurrentUser == null)
            {
                throw new UnauthorizedAccessException("Yêu cầu đăng nhập trước khi thực hiện thao tác lập phiếu thu.");
            }

            if (!SessionManager.IsAdmin() && !SessionManager.IsAccountant())
            {
                throw new UnauthorizedAccessException("Bạn không có quyền lập Phiếu thu. Chỉ Quản trị viên và Nhân viên kế toán mới có quyền này.");
            }
        }

        public string GetNextMaPT(SqlTransaction trans = null)
        {
            string sql = "SELECT TOP 1 MaPT FROM PHIEUTHU WITH (TABLOCKX, HOLDLOCK) ORDER BY MaPT DESC";
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
                if (maxCode.StartsWith("PT") && maxCode.Length == 10)
                {
                    string numPart = maxCode.Substring(2);
                    int currentNum;
                    if (int.TryParse(numPart, out currentNum))
                    {
                        return string.Format("PT{0:D8}", currentNum + 1);
                    }
                }
            }

            return "PT00000001";
        }

        public decimal GetTongTienDaThu(string maHDB, SqlTransaction trans = null)
        {
            string sql = "SELECT ISNULL(SUM(SoTien), 0) FROM PHIEUTHU WHERE MaHDB = @MaHDB";

            if (trans != null)
            {
                using (SqlCommand cmd = new SqlCommand(sql, trans.Connection, trans))
                {
                    cmd.Parameters.Add(new SqlParameter("@MaHDB", SqlDbType.Char, 12) { Value = maHDB });
                    object res = cmd.ExecuteScalar();
                    return (res != null && res != DBNull.Value) ? Convert.ToDecimal(res) : 0m;
                }
            }

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaHDB", SqlDbType.Char, 12) { Value = maHDB }
            };

            object result = Database.ExecuteScalar(sql, parameters);
            return (result != null && result != DBNull.Value) ? Convert.ToDecimal(result) : 0m;
        }

        public DataTable GetHoaDonChuaThuDu()
        {
            string sql = @"
                SELECT hdb.MaHDB, hdb.NgayLap, hdb.MaKH, kh.TenKH, hdb.TongTien,
                       ISNULL((SELECT SUM(pt.SoTien) FROM PHIEUTHU pt WHERE pt.MaHDB = hdb.MaHDB), 0) AS DaThu,
                       (hdb.TongTien - ISNULL((SELECT SUM(pt.SoTien) FROM PHIEUTHU pt WHERE pt.MaHDB = hdb.MaHDB), 0)) AS ConLai,
                       hdb.TrangThai
                FROM HOADONBAN hdb
                INNER JOIN KHACHHANG kh ON hdb.MaKH = kh.MaKH
                WHERE (hdb.TrangThai IS NULL OR (hdb.TrangThai <> N'Hủy' AND hdb.TrangThai <> N'Đã hủy'))
                  AND (hdb.TongTien > ISNULL((SELECT SUM(pt.SoTien) FROM PHIEUTHU pt WHERE pt.MaHDB = hdb.MaHDB), 0))
                ORDER BY hdb.NgayLap DESC, hdb.MaHDB DESC";

            return Database.ExecuteQuery(sql);
        }

        public bool Insert(PhieuThu pt, SqlTransaction trans = null)
        {
            CheckWritePermission();

            string sql = @"
                INSERT INTO PHIEUTHU (MaPT, MaNV, MaHDB, NgayThu, NguoiNop, LyDoThu, SoTien, HinhThuc, GhiChu)
                VALUES (@MaPT, @MaNV, @MaHDB, @NgayThu, @NguoiNop, @LyDoThu, @SoTien, @HinhThuc, @GhiChu)";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaPT", SqlDbType.Char, 10) { Value = pt.MaPT },
                new SqlParameter("@MaNV", SqlDbType.Char, 10) { Value = pt.MaNV },
                new SqlParameter("@MaHDB", SqlDbType.Char, 12) { Value = pt.MaHDB },
                new SqlParameter("@NgayThu", SqlDbType.DateTime) { Value = (object)pt.NgayThu ?? DateTime.Now },
                new SqlParameter("@NguoiNop", SqlDbType.NVarChar, 100) { Value = (object)pt.NguoiNop ?? DBNull.Value },
                new SqlParameter("@LyDoThu", SqlDbType.NVarChar, 200) { Value = (object)pt.LyDoThu ?? DBNull.Value },
                new SqlParameter("@SoTien", SqlDbType.Decimal) { Value = pt.SoTien },
                new SqlParameter("@HinhThuc", SqlDbType.NVarChar, 20) { Value = (object)pt.HinhThuc ?? "Tiền mặt" },
                new SqlParameter("@GhiChu", SqlDbType.NVarChar, 200) { Value = (object)pt.GhiChu ?? DBNull.Value }
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

        public DataTable GetAll()
        {
            string sql = @"
                SELECT pt.MaPT, pt.NgayThu, pt.MaHDB, pt.SoTien, pt.HinhThuc, pt.NguoiNop, pt.LyDoThu, pt.GhiChu,
                       pt.MaNV, nv.HoTen AS TenNV, hdb.MaKH, kh.TenKH, hdb.TongTien AS TongTienHDB
                FROM PHIEUTHU pt
                INNER JOIN HOADONBAN hdb ON pt.MaHDB = hdb.MaHDB
                INNER JOIN KHACHHANG kh ON hdb.MaKH = kh.MaKH
                INNER JOIN NHANVIEN nv ON pt.MaNV = nv.MaNV
                ORDER BY pt.NgayThu DESC, pt.MaPT DESC";

            return Database.ExecuteQuery(sql);
        }

        public DataTable Search(string keyword, DateTime? fromDate = null, DateTime? toDate = null)
        {
            string sql = @"
                SELECT pt.MaPT, pt.NgayThu, pt.MaHDB, pt.SoTien, pt.HinhThuc, pt.NguoiNop, pt.LyDoThu, pt.GhiChu,
                       pt.MaNV, nv.HoTen AS TenNV, hdb.MaKH, kh.TenKH, hdb.TongTien AS TongTienHDB
                FROM PHIEUTHU pt
                INNER JOIN HOADONBAN hdb ON pt.MaHDB = hdb.MaHDB
                INNER JOIN KHACHHANG kh ON hdb.MaKH = kh.MaKH
                INNER JOIN NHANVIEN nv ON pt.MaNV = nv.MaNV
                WHERE 1 = 1 ";

            List<SqlParameter> prms = new List<SqlParameter>();

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                sql += " AND (pt.MaPT LIKE @KW OR pt.MaHDB LIKE @KW OR pt.NguoiNop LIKE @KW OR kh.TenKH LIKE @KW OR nv.HoTen LIKE @KW) ";
                prms.Add(new SqlParameter("@KW", SqlDbType.NVarChar, 100) { Value = "%" + keyword.Trim() + "%" });
            }

            if (fromDate.HasValue)
            {
                sql += " AND pt.NgayThu >= @FromDate ";
                prms.Add(new SqlParameter("@FromDate", SqlDbType.DateTime) { Value = fromDate.Value.Date });
            }

            if (toDate.HasValue)
            {
                sql += " AND pt.NgayThu < @ToDate ";
                prms.Add(new SqlParameter("@ToDate", SqlDbType.DateTime) { Value = toDate.Value.Date.AddDays(1) });
            }

            sql += " ORDER BY pt.NgayThu DESC, pt.MaPT DESC";

            return Database.ExecuteQuery(sql, prms.ToArray());
        }

        public PhieuThu GetById(string maPT)
        {
            string sql = @"
                SELECT pt.MaPT, pt.MaNV, pt.MaHDB, pt.NgayThu, pt.NguoiNop, pt.LyDoThu, pt.SoTien, pt.HinhThuc, pt.GhiChu,
                       nv.HoTen AS TenNV, kh.TenKH, hdb.TongTien AS TongTienHDB
                FROM PHIEUTHU pt
                INNER JOIN HOADONBAN hdb ON pt.MaHDB = hdb.MaHDB
                INNER JOIN KHACHHANG kh ON hdb.MaKH = kh.MaKH
                INNER JOIN NHANVIEN nv ON pt.MaNV = nv.MaNV
                WHERE pt.MaPT = @MaPT";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaPT", SqlDbType.Char, 10) { Value = maPT }
            };

            DataTable dt = Database.ExecuteQuery(sql, parameters);
            if (dt.Rows.Count == 0)
            {
                return null;
            }

            DataRow r = dt.Rows[0];
            return new PhieuThu
            {
                MaPT = r["MaPT"].ToString().Trim(),
                MaNV = r["MaNV"].ToString().Trim(),
                MaHDB = r["MaHDB"].ToString().Trim(),
                NgayThu = r["NgayThu"] != DBNull.Value ? Convert.ToDateTime(r["NgayThu"]) : (DateTime?)null,
                NguoiNop = r["NguoiNop"] != DBNull.Value ? r["NguoiNop"].ToString().Trim() : "",
                LyDoThu = r["LyDoThu"] != DBNull.Value ? r["LyDoThu"].ToString().Trim() : "",
                SoTien = r["SoTien"] != DBNull.Value ? Convert.ToDecimal(r["SoTien"]) : 0m,
                HinhThuc = r["HinhThuc"] != DBNull.Value ? r["HinhThuc"].ToString().Trim() : "",
                GhiChu = r["GhiChu"] != DBNull.Value ? r["GhiChu"].ToString().Trim() : "",
                TenNV = r["TenNV"] != DBNull.Value ? r["TenNV"].ToString().Trim() : "",
                TenKH = r["TenKH"] != DBNull.Value ? r["TenKH"].ToString().Trim() : "",
                TongTienHDB = r["TongTienHDB"] != DBNull.Value ? Convert.ToDecimal(r["TongTienHDB"]) : 0m
            };
        }
    }
}
