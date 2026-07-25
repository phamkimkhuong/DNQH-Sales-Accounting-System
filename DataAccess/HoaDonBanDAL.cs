using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using DNQH_KeToanBanHang.Helpers;
using DNQH_KeToanBanHang.Models;

namespace DNQH_KeToanBanHang.DataAccess
{
    public class HoaDonBanDAL
    {
        private void CheckWritePermission()
        {
            if (!SessionManager.IsLoggedIn)
            {
                throw new UnauthorizedAccessException("Yêu cầu đăng nhập trước khi thực hiện thao tác hóa đơn bán.");
            }

            if (!SessionManager.IsAdmin() && !SessionManager.IsSales())
            {
                throw new UnauthorizedAccessException("Bạn không có quyền thực hiện thao tác lập hoặc chỉnh sửa Hóa đơn bán (Kế toán chỉ có quyền tra cứu).");
            }
        }

        public string GetNextMaHDB(SqlTransaction trans = null)
        {
            string sql = trans != null
                ? "SELECT TOP 1 MaHDB FROM HOADONBAN WITH (TABLOCKX, HOLDLOCK) WHERE MaHDB LIKE 'HDB%' ORDER BY MaHDB DESC"
                : "SELECT TOP 1 MaHDB FROM HOADONBAN WHERE MaHDB LIKE 'HDB%' ORDER BY MaHDB DESC";

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
                if (maxCode.Length >= 4 && maxCode.StartsWith("HDB"))
                {
                    string numPart = maxCode.Substring(3);
                    int currentNum;
                    if (int.TryParse(numPart, out currentNum))
                    {
                        return string.Format("HDB{0:D9}", currentNum + 1);
                    }
                }
            }

            return "HDB000000001";
        }

        public bool HasInvoiceForOrder(string maDDH, SqlTransaction trans = null)
        {
            string sql = "SELECT COUNT(1) FROM HOADONBAN WHERE MaDDH = @MaDDH";
            if (trans != null)
            {
                using (SqlCommand cmd = new SqlCommand(sql, trans.Connection, trans))
                {
                    cmd.Parameters.Add(new SqlParameter("@MaDDH", SqlDbType.Char, 10) { Value = maDDH });
                    object res = cmd.ExecuteScalar();
                    return res != null && res != DBNull.Value && Convert.ToInt32(res) > 0;
                }
            }

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaDDH", SqlDbType.Char, 10) { Value = maDDH }
            };

            object result = Database.ExecuteScalar(sql, parameters);
            if (result != null && result != DBNull.Value)
            {
                return Convert.ToInt32(result) > 0;
            }
            return false;
        }

        public DataTable GetDonDatHangChuaLapHoaDon()
        {
            string sql = @"
                SELECT ddh.MaDDH, ddh.NgayDat, ddh.MaKH, kh.TenKH, ddh.MaNV, nv.HoTen AS TenNV, 
                       ddh.TongTien, ddh.TrangThai, ddh.GhiChu
                FROM DONDATHANG ddh
                INNER JOIN KHACHHANG kh ON ddh.MaKH = kh.MaKH
                INNER JOIN NHANVIEN nv ON ddh.MaNV = nv.MaNV
                WHERE (ddh.TrangThai IS NULL OR ddh.TrangThai <> N'Đã hủy')
                  AND NOT EXISTS (SELECT 1 FROM HOADONBAN hdb WHERE hdb.MaDDH = ddh.MaDDH)
                ORDER BY ddh.NgayDat DESC";

            return Database.ExecuteQuery(sql);
        }

        public DataTable GetAll()
        {
            string sql = @"
                SELECT hdb.MaHDB, hdb.NgayLap, hdb.MaDDH, hdb.MaKH, kh.TenKH, 
                       hdb.MaNV, nv.HoTen AS TenNV, hdb.TongTien, hdb.TrangThai, hdb.GhiChu
                FROM HOADONBAN hdb
                INNER JOIN KHACHHANG kh ON hdb.MaKH = kh.MaKH
                INNER JOIN NHANVIEN nv ON hdb.MaNV = nv.MaNV
                ORDER BY hdb.NgayLap DESC, hdb.MaHDB DESC";

            return Database.ExecuteQuery(sql);
        }

        public DataTable Search(string keyword, DateTime? fromDate, DateTime? toDate, string trangThai = null)
        {
            StringBuilder sb = new StringBuilder(@"
                SELECT hdb.MaHDB, hdb.NgayLap, hdb.MaDDH, hdb.MaKH, kh.TenKH, 
                       hdb.MaNV, nv.HoTen AS TenNV, hdb.TongTien, hdb.TrangThai, hdb.GhiChu
                FROM HOADONBAN hdb
                INNER JOIN KHACHHANG kh ON hdb.MaKH = kh.MaKH
                INNER JOIN NHANVIEN nv ON hdb.MaNV = nv.MaNV
                WHERE 1=1 ");

            List<SqlParameter> parameters = new List<SqlParameter>();

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                sb.Append(" AND (hdb.MaHDB LIKE @Keyword OR hdb.MaDDH LIKE @Keyword OR kh.TenKH LIKE @Keyword OR kh.SoDienThoai LIKE @Keyword OR nv.HoTen LIKE @Keyword OR hdb.GhiChu LIKE @Keyword) ");
                parameters.Add(new SqlParameter("@Keyword", SqlDbType.NVarChar, 100) { Value = "%" + keyword.Trim() + "%" });
            }

            if (fromDate.HasValue)
            {
                sb.Append(" AND hdb.NgayLap >= @FromDate ");
                parameters.Add(new SqlParameter("@FromDate", SqlDbType.DateTime) { Value = fromDate.Value.Date });
            }

            if (toDate.HasValue)
            {
                sb.Append(" AND hdb.NgayLap < @ToDate ");
                parameters.Add(new SqlParameter("@ToDate", SqlDbType.DateTime) { Value = toDate.Value.Date.AddDays(1) });
            }

            if (!string.IsNullOrWhiteSpace(trangThai) && !trangThai.Equals("-- Tất cả --", StringComparison.OrdinalIgnoreCase))
            {
                sb.Append(" AND hdb.TrangThai = @TrangThai ");
                parameters.Add(new SqlParameter("@TrangThai", SqlDbType.NVarChar, 50) { Value = trangThai.Trim() });
            }

            sb.Append(" ORDER BY hdb.NgayLap DESC, hdb.MaHDB DESC");

            return Database.ExecuteQuery(sb.ToString(), parameters.ToArray());
        }

        public PagedDataTable SearchPhanTrang(
            string keyword,
            DateTime? fromDate,
            DateTime? toDate,
            string trangThai,
            int pageIndex,
            int pageSize)
        {
            if (pageIndex < 1) pageIndex = 1;
            if (pageSize < 1) pageSize = 25;
            int offset = (pageIndex - 1) * pageSize;

            StringBuilder whereBuilder = new StringBuilder(@"
                FROM HOADONBAN hdb
                INNER JOIN KHACHHANG kh ON hdb.MaKH = kh.MaKH
                INNER JOIN NHANVIEN nv ON hdb.MaNV = nv.MaNV
                WHERE 1=1 ");

            List<SqlParameter> countParams = new List<SqlParameter>();
            List<SqlParameter> dataParams = new List<SqlParameter>();

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                whereBuilder.Append(" AND (hdb.MaHDB LIKE @Keyword OR hdb.MaDDH LIKE @Keyword OR kh.TenKH LIKE @Keyword OR kh.SoDienThoai LIKE @Keyword OR nv.HoTen LIKE @Keyword OR hdb.GhiChu LIKE @Keyword) ");
                countParams.Add(new SqlParameter("@Keyword", SqlDbType.NVarChar, 100) { Value = "%" + keyword.Trim() + "%" });
                dataParams.Add(new SqlParameter("@Keyword", SqlDbType.NVarChar, 100) { Value = "%" + keyword.Trim() + "%" });
            }

            if (fromDate.HasValue)
            {
                whereBuilder.Append(" AND hdb.NgayLap >= @FromDate ");
                countParams.Add(new SqlParameter("@FromDate", SqlDbType.DateTime) { Value = fromDate.Value.Date });
                dataParams.Add(new SqlParameter("@FromDate", SqlDbType.DateTime) { Value = fromDate.Value.Date });
            }

            if (toDate.HasValue)
            {
                whereBuilder.Append(" AND hdb.NgayLap < @ToDate ");
                countParams.Add(new SqlParameter("@ToDate", SqlDbType.DateTime) { Value = toDate.Value.Date.AddDays(1) });
                dataParams.Add(new SqlParameter("@ToDate", SqlDbType.DateTime) { Value = toDate.Value.Date.AddDays(1) });
            }

            if (!string.IsNullOrWhiteSpace(trangThai) && !trangThai.Equals("-- Tất cả --", StringComparison.OrdinalIgnoreCase))
            {
                whereBuilder.Append(" AND hdb.TrangThai = @TrangThai ");
                countParams.Add(new SqlParameter("@TrangThai", SqlDbType.NVarChar, 50) { Value = trangThai.Trim() });
                dataParams.Add(new SqlParameter("@TrangThai", SqlDbType.NVarChar, 50) { Value = trangThai.Trim() });
            }

            string countSql = "SELECT COUNT(1) " + whereBuilder.ToString();
            object countObj = Database.ExecuteScalar(countSql, countParams.ToArray());
            int totalRecords = countObj != null && countObj != DBNull.Value ? Convert.ToInt32(countObj) : 0;

            string dataSql = @"
                SELECT hdb.MaHDB, hdb.NgayLap, hdb.MaDDH, hdb.MaKH, kh.TenKH, 
                       hdb.MaNV, nv.HoTen AS TenNV, hdb.TongTien, hdb.TrangThai, hdb.GhiChu " +
                whereBuilder.ToString() + @"
                ORDER BY hdb.NgayLap DESC, hdb.MaHDB DESC
                OFFSET @Offset ROWS
                FETCH NEXT @PageSize ROWS ONLY;";

            dataParams.Add(new SqlParameter("@Offset", SqlDbType.Int) { Value = offset });
            dataParams.Add(new SqlParameter("@PageSize", SqlDbType.Int) { Value = pageSize });

            DataTable table = Database.ExecuteQuery(dataSql, dataParams.ToArray());
            return new PagedDataTable(table, totalRecords, pageIndex, pageSize);
        }

        public HoaDonBan GetById(string maHDB)
        {
            string sql = @"
                SELECT hdb.MaHDB, hdb.NgayLap, hdb.MaDDH, hdb.MaKH, kh.TenKH, 
                       hdb.MaNV, nv.HoTen AS TenNV, hdb.TongTien, hdb.TrangThai, hdb.GhiChu
                FROM HOADONBAN hdb
                INNER JOIN KHACHHANG kh ON hdb.MaKH = kh.MaKH
                INNER JOIN NHANVIEN nv ON hdb.MaNV = nv.MaNV
                WHERE hdb.MaHDB = @MaHDB";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaHDB", SqlDbType.Char, 12) { Value = maHDB }
            };

            DataTable dt = Database.ExecuteQuery(sql, parameters);
            if (dt.Rows.Count == 0)
            {
                return null;
            }

            DataRow row = dt.Rows[0];
            return new HoaDonBan
            {
                MaHDB = row["MaHDB"].ToString().Trim(),
                NgayLap = Convert.ToDateTime(row["NgayLap"]),
                MaDDH = row["MaDDH"].ToString().Trim(),
                MaKH = row["MaKH"].ToString().Trim(),
                TenKH = row["TenKH"].ToString().Trim(),
                MaNV = row["MaNV"].ToString().Trim(),
                TenNV = row["TenNV"].ToString().Trim(),
                TongTien = row["TongTien"] != DBNull.Value ? Convert.ToDecimal(row["TongTien"]) : 0,
                TrangThai = row["TrangThai"] != DBNull.Value ? row["TrangThai"].ToString().Trim() : "",
                GhiChu = row["GhiChu"] != DBNull.Value ? row["GhiChu"].ToString().Trim() : ""
            };
        }

        public DataTable GetChiTietDataTable(string maHDB)
        {
            string sql = @"
                SELECT ct.MaHDB, ct.MaSP, sp.TenSP, sp.DonViTinh, ct.SoLuong, ct.DonGia, ct.GiamGia, ct.ThanhTien
                FROM CHITIETHOADONBAN ct
                INNER JOIN SANPHAM sp ON ct.MaSP = sp.MaSP
                WHERE ct.MaHDB = @MaHDB
                ORDER BY sp.TenSP";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaHDB", SqlDbType.Char, 12) { Value = maHDB }
            };

            return Database.ExecuteQuery(sql, parameters);
        }

        public List<ChiTietHoaDonBan> GetChiTietList(string maHDB)
        {
            List<ChiTietHoaDonBan> list = new List<ChiTietHoaDonBan>();
            DataTable dt = GetChiTietDataTable(maHDB);

            foreach (DataRow row in dt.Rows)
            {
                list.Add(new ChiTietHoaDonBan
                {
                    MaHDB = row["MaHDB"].ToString().Trim(),
                    MaSP = row["MaSP"].ToString().Trim(),
                    TenSP = row["TenSP"].ToString().Trim(),
                    DonViTinh = row["DonViTinh"] != DBNull.Value ? row["DonViTinh"].ToString().Trim() : "",
                    SoLuong = Convert.ToInt32(row["SoLuong"]),
                    DonGia = Convert.ToDecimal(row["DonGia"]),
                    GiamGia = row["GiamGia"] != DBNull.Value ? Convert.ToDecimal(row["GiamGia"]) : 0,
                    ThanhTien = Convert.ToDecimal(row["ThanhTien"])
                });
            }

            return list;
        }

        public int Insert(HoaDonBan hdb, SqlTransaction trans)
        {
            CheckWritePermission();

            string sql = @"
                INSERT INTO HOADONBAN (MaHDB, MaNV, MaDDH, MaKH, NgayLap, TongTien, GhiChu, TrangThai)
                VALUES (@MaHDB, @MaNV, @MaDDH, @MaKH, @NgayLap, @TongTien, @GhiChu, @TrangThai)";

            using (SqlCommand cmd = new SqlCommand(sql, trans.Connection, trans))
            {
                cmd.Parameters.Add(new SqlParameter("@MaHDB", SqlDbType.Char, 12) { Value = hdb.MaHDB });
                cmd.Parameters.Add(new SqlParameter("@MaNV", SqlDbType.Char, 10) { Value = hdb.MaNV });
                cmd.Parameters.Add(new SqlParameter("@MaDDH", SqlDbType.Char, 10) { Value = hdb.MaDDH });
                cmd.Parameters.Add(new SqlParameter("@MaKH", SqlDbType.Char, 10) { Value = hdb.MaKH });
                cmd.Parameters.Add(new SqlParameter("@NgayLap", SqlDbType.DateTime) { Value = hdb.NgayLap });
                cmd.Parameters.Add(new SqlParameter("@TongTien", SqlDbType.Decimal) { Value = hdb.TongTien, Precision = 18, Scale = 2 });
                cmd.Parameters.Add(new SqlParameter("@GhiChu", SqlDbType.NVarChar, 200) { Value = (object)hdb.GhiChu ?? DBNull.Value });
                cmd.Parameters.Add(new SqlParameter("@TrangThai", SqlDbType.NVarChar, 30) { Value = (object)hdb.TrangThai ?? DBNull.Value });

                return cmd.ExecuteNonQuery();
            }
        }

        public int InsertChiTiet(ChiTietHoaDonBan ct, SqlTransaction trans)
        {
            CheckWritePermission();

            string sql = @"
                INSERT INTO CHITIETHOADONBAN (MaHDB, MaSP, SoLuong, DonGia, GiamGia, ThanhTien)
                VALUES (@MaHDB, @MaSP, @SoLuong, @DonGia, @GiamGia, @ThanhTien)";

            using (SqlCommand cmd = new SqlCommand(sql, trans.Connection, trans))
            {
                cmd.Parameters.Add(new SqlParameter("@MaHDB", SqlDbType.Char, 12) { Value = ct.MaHDB });
                cmd.Parameters.Add(new SqlParameter("@MaSP", SqlDbType.Char, 10) { Value = ct.MaSP });
                cmd.Parameters.Add(new SqlParameter("@SoLuong", SqlDbType.Int) { Value = ct.SoLuong });
                cmd.Parameters.Add(new SqlParameter("@DonGia", SqlDbType.Decimal) { Value = ct.DonGia, Precision = 18, Scale = 2 });
                cmd.Parameters.Add(new SqlParameter("@GiamGia", SqlDbType.Decimal) { Value = ct.GiamGia, Precision = 5, Scale = 2 });
                cmd.Parameters.Add(new SqlParameter("@ThanhTien", SqlDbType.Decimal) { Value = ct.ThanhTien, Precision = 18, Scale = 2 });

                return cmd.ExecuteNonQuery();
            }
        }

        public int UpdateTongTien(string maHDB, decimal tongTien, SqlTransaction trans)
        {
            string sql = "UPDATE HOADONBAN SET TongTien = @TongTien WHERE MaHDB = @MaHDB";

            using (SqlCommand cmd = new SqlCommand(sql, trans.Connection, trans))
            {
                cmd.Parameters.Add(new SqlParameter("@TongTien", SqlDbType.Decimal) { Value = tongTien, Precision = 18, Scale = 2 });
                cmd.Parameters.Add(new SqlParameter("@MaHDB", SqlDbType.Char, 12) { Value = maHDB });

                return cmd.ExecuteNonQuery();
            }
        }
    }
}
