using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DNQH_KeToanBanHang.Constants;
using DNQH_KeToanBanHang.Helpers;
using DNQH_KeToanBanHang.Models;

namespace DNQH_KeToanBanHang.DataAccess
{
    public class ChungTuDAL
    {
        public static void CheckWritePermission()
        {
            if (!SessionManager.IsLoggedIn || SessionManager.CurrentUser == null)
            {
                throw new UnauthorizedAccessException("Yêu cầu đăng nhập trước khi thực hiện thao tác lập chứng từ.");
            }

            if (!SessionManager.IsAdmin() && !SessionManager.IsAccountant())
            {
                throw new UnauthorizedAccessException("Bạn không có quyền lập Chứng từ. Chỉ Quản trị viên và Nhân viên kế toán mới có quyền này.");
            }
        }

        public string GetNextMaCT(SqlTransaction trans = null)
        {
            string sql = "SELECT TOP 1 MaCT FROM CHUNGTU WITH (TABLOCKX, HOLDLOCK) ORDER BY MaCT DESC";
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
                if (maxCode.StartsWith("CT") && maxCode.Length == 12)
                {
                    string numPart = maxCode.Substring(2);
                    long currentNum;
                    if (long.TryParse(numPart, out currentNum))
                    {
                        return string.Format("CT{0:D10}", currentNum + 1);
                    }
                }
            }

            return "CT0000000001";
        }

        public bool Insert(ChungTu ct, SqlTransaction trans = null)
        {
            CheckWritePermission();

            string sql = @"
                INSERT INTO CHUNGTU (MaCT, MaNV, MaHDB, NgayCT, LoaiCT, DienGiai)
                VALUES (@MaCT, @MaNV, @MaHDB, @NgayCT, @LoaiCT, @DienGiai)";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaCT", SqlDbType.Char, 12) { Value = ct.MaCT },
                new SqlParameter("@MaNV", SqlDbType.Char, 10) { Value = ct.MaNV },
                new SqlParameter("@MaHDB", SqlDbType.Char, 12) { Value = ct.MaHDB },
                new SqlParameter("@NgayCT", SqlDbType.DateTime) { Value = (object)ct.NgayCT ?? DateTime.Now },
                new SqlParameter("@LoaiCT", SqlDbType.NVarChar, 50) { Value = (object)ct.LoaiCT ?? DocumentTypeConstants.ChungTuBanHang },
                new SqlParameter("@DienGiai", SqlDbType.NVarChar, 200) { Value = (object)ct.DienGiai ?? DBNull.Value }
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

        public bool InsertChiTiet(ChiTietChungTu ctd, SqlTransaction trans = null)
        {
            CheckWritePermission();

            string sql = @"
                INSERT INTO CHITIETCHUNGTU (MaCT, STT, TaiKhoanNo, TaiKhoanCo, SoTien, DienGiai)
                VALUES (@MaCT, @STT, @TaiKhoanNo, @TaiKhoanCo, @SoTien, @DienGiai)";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaCT", SqlDbType.Char, 12) { Value = ctd.MaCT },
                new SqlParameter("@STT", SqlDbType.Int) { Value = ctd.STT },
                new SqlParameter("@TaiKhoanNo", SqlDbType.VarChar, 20) { Value = (object)ctd.TaiKhoanNo ?? DBNull.Value },
                new SqlParameter("@TaiKhoanCo", SqlDbType.VarChar, 20) { Value = (object)ctd.TaiKhoanCo ?? DBNull.Value },
                new SqlParameter("@SoTien", SqlDbType.Decimal) { Value = ctd.SoTien },
                new SqlParameter("@DienGiai", SqlDbType.NVarChar, 200) { Value = (object)ctd.DienGiai ?? DBNull.Value }
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
                SELECT ct.MaCT, ct.NgayCT, ct.LoaiCT, ct.MaHDB, ct.DienGiai,
                       ct.MaNV, nv.HoTen AS TenNV, hdb.MaKH, kh.TenKH, hdb.TongTien AS TongTienHDB,
                       ISNULL((SELECT SUM(c.SoTien) FROM CHITIETCHUNGTU c WHERE c.MaCT = ct.MaCT), 0) AS TongTienHachToan
                FROM CHUNGTU ct
                INNER JOIN HOADONBAN hdb ON ct.MaHDB = hdb.MaHDB
                INNER JOIN KHACHHANG kh ON hdb.MaKH = kh.MaKH
                INNER JOIN NHANVIEN nv ON ct.MaNV = nv.MaNV
                ORDER BY ct.NgayCT DESC, ct.MaCT DESC";

            return Database.ExecuteQuery(sql);
        }

        public DataTable Search(string keyword, DateTime? fromDate = null, DateTime? toDate = null)
        {
            string sql = @"
                SELECT ct.MaCT, ct.NgayCT, ct.LoaiCT, ct.MaHDB, ct.DienGiai,
                       ct.MaNV, nv.HoTen AS TenNV, hdb.MaKH, kh.TenKH, hdb.TongTien AS TongTienHDB,
                       ISNULL((SELECT SUM(c.SoTien) FROM CHITIETCHUNGTU c WHERE c.MaCT = ct.MaCT), 0) AS TongTienHachToan
                FROM CHUNGTU ct
                INNER JOIN HOADONBAN hdb ON ct.MaHDB = hdb.MaHDB
                INNER JOIN KHACHHANG kh ON hdb.MaKH = kh.MaKH
                INNER JOIN NHANVIEN nv ON ct.MaNV = nv.MaNV
                WHERE 1 = 1 ";

            List<SqlParameter> prms = new List<SqlParameter>();

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                sql += " AND (ct.MaCT LIKE @KW OR ct.MaHDB LIKE @KW OR ct.DienGiai LIKE @KW OR kh.TenKH LIKE @KW OR nv.HoTen LIKE @KW) ";
                prms.Add(new SqlParameter("@KW", SqlDbType.NVarChar, 100) { Value = "%" + keyword.Trim() + "%" });
            }

            if (fromDate.HasValue)
            {
                sql += " AND ct.NgayCT >= @FromDate ";
                prms.Add(new SqlParameter("@FromDate", SqlDbType.DateTime) { Value = fromDate.Value.Date });
            }

            if (toDate.HasValue)
            {
                sql += " AND ct.NgayCT < @ToDate ";
                prms.Add(new SqlParameter("@ToDate", SqlDbType.DateTime) { Value = toDate.Value.Date.AddDays(1) });
            }

            sql += " ORDER BY ct.NgayCT DESC, ct.MaCT DESC";

            return Database.ExecuteQuery(sql, prms.ToArray());
        }

        public PagedDataTable SearchPhanTrang(
            string keyword,
            DateTime? fromDate,
            DateTime? toDate,
            int pageIndex,
            int pageSize)
        {
            if (pageIndex < 1) pageIndex = 1;
            if (pageSize < 1) pageSize = 25;
            int offset = (pageIndex - 1) * pageSize;

            string whereBase = @"
                FROM CHUNGTU ct
                INNER JOIN HOADONBAN hdb ON ct.MaHDB = hdb.MaHDB
                INNER JOIN KHACHHANG kh ON hdb.MaKH = kh.MaKH
                INNER JOIN NHANVIEN nv ON ct.MaNV = nv.MaNV
                WHERE 1 = 1 ";

            List<SqlParameter> countParams = new List<SqlParameter>();
            List<SqlParameter> dataParams = new List<SqlParameter>();
            string filterSql = "";

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                filterSql += " AND (ct.MaCT LIKE @KW OR ct.MaHDB LIKE @KW OR ct.DienGiai LIKE @KW OR kh.TenKH LIKE @KW OR nv.HoTen LIKE @KW) ";
                countParams.Add(new SqlParameter("@KW", SqlDbType.NVarChar, 100) { Value = "%" + keyword.Trim() + "%" });
                dataParams.Add(new SqlParameter("@KW", SqlDbType.NVarChar, 100) { Value = "%" + keyword.Trim() + "%" });
            }

            if (fromDate.HasValue)
            {
                filterSql += " AND ct.NgayCT >= @FromDate ";
                countParams.Add(new SqlParameter("@FromDate", SqlDbType.DateTime) { Value = fromDate.Value.Date });
                dataParams.Add(new SqlParameter("@FromDate", SqlDbType.DateTime) { Value = fromDate.Value.Date });
            }

            if (toDate.HasValue)
            {
                filterSql += " AND ct.NgayCT < @ToDate ";
                countParams.Add(new SqlParameter("@ToDate", SqlDbType.DateTime) { Value = toDate.Value.Date.AddDays(1) });
                dataParams.Add(new SqlParameter("@ToDate", SqlDbType.DateTime) { Value = toDate.Value.Date.AddDays(1) });
            }

            string countSql = "SELECT COUNT(1) " + whereBase + filterSql;
            object countObj = Database.ExecuteScalar(countSql, countParams.ToArray());
            int totalRecords = countObj != null && countObj != DBNull.Value ? Convert.ToInt32(countObj) : 0;

            string dataSql = @"
                SELECT ct.MaCT, ct.NgayCT, ct.LoaiCT, ct.MaHDB, ct.DienGiai,
                       ct.MaNV, nv.HoTen AS TenNV, hdb.MaKH, kh.TenKH, hdb.TongTien AS TongTienHDB,
                       ISNULL((SELECT SUM(c.SoTien) FROM CHITIETCHUNGTU c WHERE c.MaCT = ct.MaCT), 0) AS TongTienHachToan " +
                whereBase + filterSql + @"
                ORDER BY ct.NgayCT DESC, ct.MaCT DESC
                OFFSET @Offset ROWS
                FETCH NEXT @PageSize ROWS ONLY;";

            dataParams.Add(new SqlParameter("@Offset", SqlDbType.Int) { Value = offset });
            dataParams.Add(new SqlParameter("@PageSize", SqlDbType.Int) { Value = pageSize });

            DataTable table = Database.ExecuteQuery(dataSql, dataParams.ToArray());
            return new PagedDataTable(table, totalRecords, pageIndex, pageSize);
        }

        public ChungTu GetById(string maCT)
        {
            string sql = @"
                SELECT ct.MaCT, ct.MaNV, ct.MaHDB, ct.NgayCT, ct.LoaiCT, ct.DienGiai,
                       nv.HoTen AS TenNV, kh.TenKH, hdb.TongTien AS TongTienHDB,
                       ISNULL((SELECT SUM(c.SoTien) FROM CHITIETCHUNGTU c WHERE c.MaCT = ct.MaCT), 0) AS TongTienHachToan
                FROM CHUNGTU ct
                INNER JOIN HOADONBAN hdb ON ct.MaHDB = hdb.MaHDB
                INNER JOIN KHACHHANG kh ON hdb.MaKH = kh.MaKH
                INNER JOIN NHANVIEN nv ON ct.MaNV = nv.MaNV
                WHERE ct.MaCT = @MaCT";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaCT", SqlDbType.Char, 12) { Value = maCT }
            };

            DataTable dt = Database.ExecuteQuery(sql, parameters);
            if (dt.Rows.Count == 0)
            {
                return null;
            }

            DataRow r = dt.Rows[0];
            return new ChungTu
            {
                MaCT = r["MaCT"].ToString().Trim(),
                MaNV = r["MaNV"].ToString().Trim(),
                MaHDB = r["MaHDB"].ToString().Trim(),
                NgayCT = r["NgayCT"] != DBNull.Value ? Convert.ToDateTime(r["NgayCT"]) : (DateTime?)null,
                LoaiCT = r["LoaiCT"] != DBNull.Value ? r["LoaiCT"].ToString().Trim() : "",
                DienGiai = r["DienGiai"] != DBNull.Value ? r["DienGiai"].ToString().Trim() : "",
                TenNV = r["TenNV"] != DBNull.Value ? r["TenNV"].ToString().Trim() : "",
                TenKH = r["TenKH"] != DBNull.Value ? r["TenKH"].ToString().Trim() : "",
                TongTienHachToan = r["TongTienHachToan"] != DBNull.Value ? Convert.ToDecimal(r["TongTienHachToan"]) : 0m
            };
        }

        public DataTable GetChiTietDataTable(string maCT)
        {
            string sql = @"
                SELECT STT, TaiKhoanNo, TaiKhoanCo, SoTien, DienGiai
                FROM CHITIETCHUNGTU
                WHERE MaCT = @MaCT
                ORDER BY STT";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaCT", SqlDbType.Char, 12) { Value = maCT }
            };

            return Database.ExecuteQuery(sql, parameters);
        }

        public List<ChiTietChungTu> GetChiTietList(string maCT)
        {
            DataTable dt = GetChiTietDataTable(maCT);
            List<ChiTietChungTu> list = new List<ChiTietChungTu>();

            foreach (DataRow r in dt.Rows)
            {
                list.Add(new ChiTietChungTu
                {
                    MaCT = maCT,
                    STT = Convert.ToInt32(r["STT"]),
                    TaiKhoanNo = r["TaiKhoanNo"] != DBNull.Value ? r["TaiKhoanNo"].ToString().Trim() : "",
                    TaiKhoanCo = r["TaiKhoanCo"] != DBNull.Value ? r["TaiKhoanCo"].ToString().Trim() : "",
                    SoTien = r["SoTien"] != DBNull.Value ? Convert.ToDecimal(r["SoTien"]) : 0m,
                    DienGiai = r["DienGiai"] != DBNull.Value ? r["DienGiai"].ToString().Trim() : ""
                });
            }

            return list;
        }

        public DataTable GetHoaDonSanSangLapChungTu()
        {
            string sql = @"
                SELECT hdb.MaHDB, hdb.NgayLap, hdb.MaKH, kh.TenKH, hdb.TongTien, hdb.TrangThai
                FROM HOADONBAN hdb
                INNER JOIN KHACHHANG kh ON hdb.MaKH = kh.MaKH
                WHERE (hdb.TrangThai IS NULL OR (hdb.TrangThai <> N'Hủy' AND hdb.TrangThai <> N'Đã hủy'))
                ORDER BY hdb.NgayLap DESC, hdb.MaHDB DESC";

            return Database.ExecuteQuery(sql);
        }
    }
}
