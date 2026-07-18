using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using DNQH_KeToanBanHang.Helpers;
using DNQH_KeToanBanHang.Models;

namespace DNQH_KeToanBanHang.DataAccess
{
    public class DonDatHangDAL
    {
        private void CheckWritePermission()
        {
            if (!SessionManager.IsLoggedIn)
            {
                throw new UnauthorizedAccessException("Yêu cầu đăng nhập trước khi thực hiện thao tác đơn đặt hàng.");
            }

            if (!SessionManager.IsAdmin() && !SessionManager.IsSales())
            {
                throw new UnauthorizedAccessException("Bạn không có quyền thực hiện thao tác lập hoặc chỉnh sửa Đơn đặt hàng.");
            }
        }

        public string GetNextMaDDH(SqlTransaction trans = null)
        {
            string sql = trans != null
                ? "SELECT TOP 1 MaDDH FROM DONDATHANG WITH (TABLOCKX, HOLDLOCK) WHERE MaDDH LIKE 'DDH%' ORDER BY MaDDH DESC"
                : "SELECT TOP 1 MaDDH FROM DONDATHANG WHERE MaDDH LIKE 'DDH%' ORDER BY MaDDH DESC";

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
                if (maxCode.Length >= 4 && maxCode.StartsWith("DDH"))
                {
                    string numPart = maxCode.Substring(3);
                    int currentNum;
                    if (int.TryParse(numPart, out currentNum))
                    {
                        return string.Format("DDH{0:D7}", currentNum + 1);
                    }
                }
            }

            return "DDH0000001";
        }

        public DataTable GetAll()
        {
            string sql = @"
                SELECT ddh.MaDDH, ddh.NgayDat, ddh.NgayGiaoDuKien, ddh.MaKH, kh.TenKH, 
                       ddh.MaNV, nv.HoTen AS TenNV, ddh.TongTien, ddh.TrangThai, ddh.GhiChu,
                       ISNULL(ddh.Version, 1) AS Version
                FROM DONDATHANG ddh
                INNER JOIN KHACHHANG kh ON ddh.MaKH = kh.MaKH
                INNER JOIN NHANVIEN nv ON ddh.MaNV = nv.MaNV
                ORDER BY ddh.NgayDat DESC, ddh.MaDDH DESC";

            return Database.ExecuteQuery(sql);
        }

        public DataTable Search(string keyword, DateTime? fromDate, DateTime? toDate, string trangThai = null)
        {
            StringBuilder sb = new StringBuilder(@"
                SELECT ddh.MaDDH, ddh.NgayDat, ddh.NgayGiaoDuKien, ddh.MaKH, kh.TenKH, 
                       ddh.MaNV, nv.HoTen AS TenNV, ddh.TongTien, ddh.TrangThai, ddh.GhiChu,
                       ISNULL(ddh.Version, 1) AS Version
                FROM DONDATHANG ddh
                INNER JOIN KHACHHANG kh ON ddh.MaKH = kh.MaKH
                INNER JOIN NHANVIEN nv ON ddh.MaNV = nv.MaNV
                WHERE 1=1 ");

            List<SqlParameter> parameters = new List<SqlParameter>();

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                sb.Append(" AND (ddh.MaDDH LIKE @Keyword OR kh.TenKH LIKE @Keyword OR kh.SoDienThoai LIKE @Keyword OR nv.HoTen LIKE @Keyword OR ddh.GhiChu LIKE @Keyword) ");
                parameters.Add(new SqlParameter("@Keyword", SqlDbType.NVarChar, 100) { Value = "%" + keyword.Trim() + "%" });
            }

            if (fromDate.HasValue)
            {
                sb.Append(" AND ddh.NgayDat >= @FromDate ");
                parameters.Add(new SqlParameter("@FromDate", SqlDbType.DateTime) { Value = fromDate.Value.Date });
            }

            if (toDate.HasValue)
            {
                sb.Append(" AND ddh.NgayDat < @ToDate ");
                parameters.Add(new SqlParameter("@ToDate", SqlDbType.DateTime) { Value = toDate.Value.Date.AddDays(1) });
            }

            if (!string.IsNullOrWhiteSpace(trangThai) && !trangThai.Equals("-- Tất cả --", StringComparison.OrdinalIgnoreCase))
            {
                sb.Append(" AND ddh.TrangThai = @TrangThai ");
                parameters.Add(new SqlParameter("@TrangThai", SqlDbType.NVarChar, 50) { Value = trangThai.Trim() });
            }

            sb.Append(" ORDER BY ddh.NgayDat DESC, ddh.MaDDH DESC");

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
                FROM DONDATHANG ddh
                INNER JOIN KHACHHANG kh ON ddh.MaKH = kh.MaKH
                INNER JOIN NHANVIEN nv ON ddh.MaNV = nv.MaNV
                WHERE 1=1 ");

            List<SqlParameter> countParams = new List<SqlParameter>();
            List<SqlParameter> dataParams = new List<SqlParameter>();

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                whereBuilder.Append(" AND (ddh.MaDDH LIKE @Keyword OR kh.TenKH LIKE @Keyword OR kh.SoDienThoai LIKE @Keyword OR nv.HoTen LIKE @Keyword OR ddh.GhiChu LIKE @Keyword) ");
                countParams.Add(new SqlParameter("@Keyword", SqlDbType.NVarChar, 100) { Value = "%" + keyword.Trim() + "%" });
                dataParams.Add(new SqlParameter("@Keyword", SqlDbType.NVarChar, 100) { Value = "%" + keyword.Trim() + "%" });
            }

            if (fromDate.HasValue)
            {
                whereBuilder.Append(" AND ddh.NgayDat >= @FromDate ");
                countParams.Add(new SqlParameter("@FromDate", SqlDbType.DateTime) { Value = fromDate.Value.Date });
                dataParams.Add(new SqlParameter("@FromDate", SqlDbType.DateTime) { Value = fromDate.Value.Date });
            }

            if (toDate.HasValue)
            {
                whereBuilder.Append(" AND ddh.NgayDat < @ToDate ");
                countParams.Add(new SqlParameter("@ToDate", SqlDbType.DateTime) { Value = toDate.Value.Date.AddDays(1) });
                dataParams.Add(new SqlParameter("@ToDate", SqlDbType.DateTime) { Value = toDate.Value.Date.AddDays(1) });
            }

            if (!string.IsNullOrWhiteSpace(trangThai) && !trangThai.Equals("-- Tất cả --", StringComparison.OrdinalIgnoreCase))
            {
                whereBuilder.Append(" AND ddh.TrangThai = @TrangThai ");
                countParams.Add(new SqlParameter("@TrangThai", SqlDbType.NVarChar, 50) { Value = trangThai.Trim() });
                dataParams.Add(new SqlParameter("@TrangThai", SqlDbType.NVarChar, 50) { Value = trangThai.Trim() });
            }

            string countSql = "SELECT COUNT(1) " + whereBuilder.ToString();
            object countObj = Database.ExecuteScalar(countSql, countParams.ToArray());
            int totalRecords = countObj != null && countObj != DBNull.Value ? Convert.ToInt32(countObj) : 0;

            string dataSql = @"
                SELECT ddh.MaDDH, ddh.NgayDat, ddh.NgayGiaoDuKien, ddh.MaKH, kh.TenKH, 
                       ddh.MaNV, nv.HoTen AS TenNV, ddh.TongTien, ddh.TrangThai, ddh.GhiChu,
                       ISNULL(ddh.Version, 1) AS Version " +
                whereBuilder.ToString() + @"
                ORDER BY ddh.NgayDat DESC, ddh.MaDDH DESC
                OFFSET @Offset ROWS
                FETCH NEXT @PageSize ROWS ONLY;";

            dataParams.Add(new SqlParameter("@Offset", SqlDbType.Int) { Value = offset });
            dataParams.Add(new SqlParameter("@PageSize", SqlDbType.Int) { Value = pageSize });

            DataTable table = Database.ExecuteQuery(dataSql, dataParams.ToArray());
            return new PagedDataTable(table, totalRecords, pageIndex, pageSize);
        }

        public DonDatHang GetById(string maDDH)
        {
            string sql = @"
                SELECT ddh.MaDDH, ddh.NgayDat, ddh.NgayGiaoDuKien, ddh.MaKH, kh.TenKH, 
                       ddh.MaNV, nv.HoTen AS TenNV, ddh.TongTien, ddh.TrangThai, ddh.GhiChu,
                       ISNULL(ddh.Version, 1) AS Version
                FROM DONDATHANG ddh
                INNER JOIN KHACHHANG kh ON ddh.MaKH = kh.MaKH
                INNER JOIN NHANVIEN nv ON ddh.MaNV = nv.MaNV
                WHERE ddh.MaDDH = @MaDDH";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaDDH", SqlDbType.Char, 10) { Value = maDDH }
            };

            DataTable dt = Database.ExecuteQuery(sql, parameters);
            if (dt.Rows.Count == 0)
            {
                return null;
            }

            DataRow row = dt.Rows[0];
            int version = 1;
            if (row.Table.Columns.Contains("Version") && row["Version"] != DBNull.Value)
                int.TryParse(row["Version"].ToString(), out version);

            return new DonDatHang
            {
                MaDDH = row["MaDDH"].ToString().Trim(),
                NgayDat = Convert.ToDateTime(row["NgayDat"]),
                NgayGiaoDuKien = row["NgayGiaoDuKien"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(row["NgayGiaoDuKien"]) : null,
                MaKH = row["MaKH"].ToString().Trim(),
                TenKH = row["TenKH"].ToString().Trim(),
                MaNV = row["MaNV"].ToString().Trim(),
                TenNV = row["TenNV"].ToString().Trim(),
                TongTien = row["TongTien"] != DBNull.Value ? Convert.ToDecimal(row["TongTien"]) : 0,
                TrangThai = row["TrangThai"] != DBNull.Value ? row["TrangThai"].ToString().Trim() : "",
                GhiChu = row["GhiChu"] != DBNull.Value ? row["GhiChu"].ToString().Trim() : "",
                Version = version > 0 ? version : 1
            };
        }

        public bool Exists(string maDDH)
        {
            if (string.IsNullOrWhiteSpace(maDDH))
                return false;

            string query = "SELECT COUNT(*) FROM DONDATHANG WHERE MaDDH = @MaDDH";
            SqlParameter param = new SqlParameter("@MaDDH", SqlDbType.Char, 10) { Value = maDDH.Trim() };
            object result = Database.ExecuteScalar(query, param);
            return result != null && Convert.ToInt32(result) > 0;
        }

        public DataTable GetChiTietDataTable(string maDDH)
        {
            string sql = @"
                SELECT ct.MaDDH, ct.MaSP, sp.TenSP, sp.DonViTinh, ct.SoLuong, ct.DonGia, ct.GiamGia, ct.ThanhTien
                FROM CHITIETDONDATHANG ct
                INNER JOIN SANPHAM sp ON ct.MaSP = sp.MaSP
                WHERE ct.MaDDH = @MaDDH
                ORDER BY sp.TenSP";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaDDH", SqlDbType.Char, 10) { Value = maDDH }
            };

            return Database.ExecuteQuery(sql, parameters);
        }

        public List<ChiTietDonDatHang> GetChiTietList(string maDDH)
        {
            List<ChiTietDonDatHang> list = new List<ChiTietDonDatHang>();
            DataTable dt = GetChiTietDataTable(maDDH);

            foreach (DataRow row in dt.Rows)
            {
                list.Add(new ChiTietDonDatHang
                {
                    MaDDH = row["MaDDH"].ToString().Trim(),
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

        public int Insert(DonDatHang ddh, SqlTransaction trans)
        {
            CheckWritePermission();

            string sql = @"
                INSERT INTO DONDATHANG (MaDDH, MaNV, MaKH, NgayDat, NgayGiaoDuKien, TongTien, TrangThai, GhiChu, Version)
                VALUES (@MaDDH, @MaNV, @MaKH, @NgayDat, @NgayGiaoDuKien, @TongTien, @TrangThai, @GhiChu, 1)";

            using (SqlCommand cmd = new SqlCommand(sql, trans.Connection, trans))
            {
                cmd.Parameters.Add(new SqlParameter("@MaDDH", SqlDbType.Char, 10) { Value = ddh.MaDDH });
                cmd.Parameters.Add(new SqlParameter("@MaNV", SqlDbType.Char, 10) { Value = ddh.MaNV });
                cmd.Parameters.Add(new SqlParameter("@MaKH", SqlDbType.Char, 10) { Value = ddh.MaKH });
                cmd.Parameters.Add(new SqlParameter("@NgayDat", SqlDbType.DateTime) { Value = ddh.NgayDat });
                cmd.Parameters.Add(new SqlParameter("@NgayGiaoDuKien", SqlDbType.Date) { Value = (object)ddh.NgayGiaoDuKien ?? DBNull.Value });
                cmd.Parameters.Add(new SqlParameter("@TongTien", SqlDbType.Decimal) { Value = ddh.TongTien, Precision = 18, Scale = 2 });
                cmd.Parameters.Add(new SqlParameter("@TrangThai", SqlDbType.NVarChar, 30) { Value = (object)ddh.TrangThai ?? DBNull.Value });
                cmd.Parameters.Add(new SqlParameter("@GhiChu", SqlDbType.NVarChar, 200) { Value = (object)ddh.GhiChu ?? DBNull.Value });

                return cmd.ExecuteNonQuery();
            }
        }

        public int InsertChiTiet(ChiTietDonDatHang ct, SqlTransaction trans)
        {
            CheckWritePermission();

            string sql = @"
                INSERT INTO CHITIETDONDATHANG (MaDDH, MaSP, SoLuong, DonGia, GiamGia, ThanhTien)
                VALUES (@MaDDH, @MaSP, @SoLuong, @DonGia, @GiamGia, @ThanhTien)";

            using (SqlCommand cmd = new SqlCommand(sql, trans.Connection, trans))
            {
                cmd.Parameters.Add(new SqlParameter("@MaDDH", SqlDbType.Char, 10) { Value = ct.MaDDH });
                cmd.Parameters.Add(new SqlParameter("@MaSP", SqlDbType.Char, 10) { Value = ct.MaSP });
                cmd.Parameters.Add(new SqlParameter("@SoLuong", SqlDbType.Int) { Value = ct.SoLuong });
                cmd.Parameters.Add(new SqlParameter("@DonGia", SqlDbType.Decimal) { Value = ct.DonGia, Precision = 18, Scale = 2 });
                cmd.Parameters.Add(new SqlParameter("@GiamGia", SqlDbType.Decimal) { Value = ct.GiamGia, Precision = 5, Scale = 2 });
                cmd.Parameters.Add(new SqlParameter("@ThanhTien", SqlDbType.Decimal) { Value = ct.ThanhTien, Precision = 18, Scale = 2 });

                return cmd.ExecuteNonQuery();
            }
        }

        public int UpdateTongTien(string maDDH, decimal tongTien, SqlTransaction trans)
        {
            string sql = "UPDATE DONDATHANG SET TongTien = @TongTien, Version = ISNULL(Version, 1) + 1 WHERE MaDDH = @MaDDH";

            using (SqlCommand cmd = new SqlCommand(sql, trans.Connection, trans))
            {
                cmd.Parameters.Add(new SqlParameter("@TongTien", SqlDbType.Decimal) { Value = tongTien, Precision = 18, Scale = 2 });
                cmd.Parameters.Add(new SqlParameter("@MaDDH", SqlDbType.Char, 10) { Value = maDDH });

                return cmd.ExecuteNonQuery();
            }
        }

        public ConcurrencyUpdateResult UpdateTrangThaiWithResult(string maDDH, string trangThai, int version, SqlTransaction trans = null)
        {
            string sql = "UPDATE DONDATHANG SET TrangThai = @TrangThai, Version = ISNULL(Version, 1) + 1 " +
                         "WHERE MaDDH = @MaDDH AND ISNULL(Version, 1) = @Version";

            int rowsAffected;
            if (trans != null)
            {
                using (SqlCommand cmd = new SqlCommand(sql, trans.Connection, trans))
                {
                    cmd.Parameters.Add(new SqlParameter("@TrangThai", SqlDbType.NVarChar, 30) { Value = trangThai });
                    cmd.Parameters.Add(new SqlParameter("@MaDDH", SqlDbType.Char, 10) { Value = maDDH });
                    cmd.Parameters.Add(new SqlParameter("@Version", SqlDbType.Int) { Value = version });
                    rowsAffected = cmd.ExecuteNonQuery();
                }
            }
            else
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@TrangThai", SqlDbType.NVarChar, 30) { Value = trangThai },
                    new SqlParameter("@MaDDH", SqlDbType.Char, 10) { Value = maDDH },
                    new SqlParameter("@Version", SqlDbType.Int) { Value = version }
                };
                rowsAffected = Database.ExecuteNonQuery(sql, parameters);
            }

            if (rowsAffected > 0)
            {
                return ConcurrencyUpdateResult.Success;
            }

            if (Exists(maDDH))
            {
                return ConcurrencyUpdateResult.ConcurrencyConflict;
            }

            return ConcurrencyUpdateResult.NotFoundOrFailed;
        }

        public int UpdateTrangThai(string maDDH, string trangThai, SqlTransaction trans = null)
        {
            string sql = "UPDATE DONDATHANG SET TrangThai = @TrangThai, Version = ISNULL(Version, 1) + 1 WHERE MaDDH = @MaDDH";

            if (trans != null)
            {
                using (SqlCommand cmd = new SqlCommand(sql, trans.Connection, trans))
                {
                    cmd.Parameters.Add(new SqlParameter("@TrangThai", SqlDbType.NVarChar, 30) { Value = trangThai });
                    cmd.Parameters.Add(new SqlParameter("@MaDDH", SqlDbType.Char, 10) { Value = maDDH });
                    return cmd.ExecuteNonQuery();
                }
            }
            else
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@TrangThai", SqlDbType.NVarChar, 30) { Value = trangThai },
                    new SqlParameter("@MaDDH", SqlDbType.Char, 10) { Value = maDDH }
                };
                return Database.ExecuteNonQuery(sql, parameters);
            }
        }

        public ConcurrencyUpdateResult UpdateWithResult(DonDatHang ddh, SqlTransaction trans = null)
        {
            CheckWritePermission();

            int currentVersion = ddh.Version > 0 ? ddh.Version : 1;
            string sql = @"
                UPDATE DONDATHANG 
                SET MaKH = @MaKH, NgayDat = @NgayDat, NgayGiaoDuKien = @NgayGiaoDuKien, 
                    TrangThai = @TrangThai, GhiChu = @GhiChu, Version = ISNULL(Version, 1) + 1
                WHERE MaDDH = @MaDDH AND ISNULL(Version, 1) = @Version";

            int rowsAffected;
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaDDH", SqlDbType.Char, 10) { Value = ddh.MaDDH },
                new SqlParameter("@MaKH", SqlDbType.Char, 10) { Value = ddh.MaKH },
                new SqlParameter("@NgayDat", SqlDbType.DateTime) { Value = ddh.NgayDat },
                new SqlParameter("@NgayGiaoDuKien", SqlDbType.Date) { Value = (object)ddh.NgayGiaoDuKien ?? DBNull.Value },
                new SqlParameter("@TrangThai", SqlDbType.NVarChar, 30) { Value = (object)ddh.TrangThai ?? DBNull.Value },
                new SqlParameter("@GhiChu", SqlDbType.NVarChar, 200) { Value = (object)ddh.GhiChu ?? DBNull.Value },
                new SqlParameter("@Version", SqlDbType.Int) { Value = currentVersion }
            };

            if (trans != null)
            {
                using (SqlCommand cmd = new SqlCommand(sql, trans.Connection, trans))
                {
                    cmd.Parameters.AddRange(parameters);
                    rowsAffected = cmd.ExecuteNonQuery();
                }
            }
            else
            {
                rowsAffected = Database.ExecuteNonQuery(sql, parameters);
            }

            if (rowsAffected > 0)
            {
                ddh.Version = currentVersion + 1;
                return ConcurrencyUpdateResult.Success;
            }

            if (Exists(ddh.MaDDH))
            {
                return ConcurrencyUpdateResult.ConcurrencyConflict;
            }

            return ConcurrencyUpdateResult.NotFoundOrFailed;
        }
    }
}
