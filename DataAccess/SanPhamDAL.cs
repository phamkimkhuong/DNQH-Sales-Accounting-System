using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DNQH_KeToanBanHang.Constants;
using DNQH_KeToanBanHang.Helpers;
using DNQH_KeToanBanHang.Models;

namespace DNQH_KeToanBanHang.DataAccess
{
    public enum UpdateProductResult
    {
        Success,
        ConcurrencyConflict,
        NotFoundOrFailed
    }

    public struct SanPhamUsageStats
    {
        public int OrderCount;
        public int InvoiceCount;
        public int IssueCount;
        public int StockRowCount;
        public int TotalStockQty;

        public bool HasHistoricalTransactions
        {
            get { return OrderCount > 0 || InvoiceCount > 0 || IssueCount > 0; }
        }

        public bool HasStock
        {
            get { return TotalStockQty > 0; }
        }

        public bool HasAnyReference
        {
            get { return OrderCount > 0 || InvoiceCount > 0 || IssueCount > 0 || StockRowCount > 0; }
        }
    }

    public class SanPhamDAL
    {
        public DataTable GetAll()
        {
            string query = "SELECT sp.MaSP, sp.TenSP, sp.MaLoai, lsp.TenLoai, sp.MaNCC, ncc.TenNCC, " +
                           "sp.DonViTinh, sp.DonGiaBan, sp.TrangThai, ISNULL(sp.Version, 1) AS Version " +
                           "FROM SANPHAM sp " +
                           "LEFT JOIN LOAISANPHAM lsp ON sp.MaLoai = lsp.MaLoai " +
                           "LEFT JOIN NHACUNGCAP ncc ON sp.MaNCC = ncc.MaNCC " +
                           "ORDER BY sp.MaSP";

            return Database.ExecuteQuery(query);
        }

        public DataTable GetActiveProducts()
        {
            string query = "SELECT sp.MaSP, sp.TenSP, sp.MaLoai, lsp.TenLoai, sp.MaNCC, ncc.TenNCC, " +
                           "sp.DonViTinh, sp.DonGiaBan, sp.TrangThai, ISNULL(sp.Version, 1) AS Version " +
                           "FROM SANPHAM sp " +
                           "LEFT JOIN LOAISANPHAM lsp ON sp.MaLoai = lsp.MaLoai " +
                           "LEFT JOIN NHACUNGCAP ncc ON sp.MaNCC = ncc.MaNCC " +
                           "WHERE sp.TrangThai = N'Đang kinh doanh' OR sp.TrangThai = N'Hoạt động' OR sp.TrangThai IS NULL " +
                           "ORDER BY sp.MaSP";

            return Database.ExecuteQuery(query);
        }

        public SanPham GetById(string maSP)
        {
            if (string.IsNullOrWhiteSpace(maSP))
                return null;

            string query = "SELECT MaSP, MaNCC, MaLoai, TenSP, DonViTinh, DonGiaBan, TrangThai, ISNULL(Version, 1) AS Version " +
                           "FROM SANPHAM WHERE MaSP = @MaSP";

            SqlParameter param = new SqlParameter("@MaSP", SqlDbType.Char, 10) { Value = maSP.Trim() };

            DataTable dt = Database.ExecuteQuery(query, param);
            if (dt.Rows.Count == 0)
                return null;

            DataRow row = dt.Rows[0];
            decimal giaBan = 0;
            if (row["DonGiaBan"] != DBNull.Value)
                decimal.TryParse(row["DonGiaBan"].ToString(), out giaBan);

            int version = 1;
            if (row.Table.Columns.Contains("Version") && row["Version"] != DBNull.Value)
                int.TryParse(row["Version"].ToString(), out version);

            return new SanPham
            {
                MaSP = row["MaSP"].ToString().Trim(),
                MaNCC = row["MaNCC"] != DBNull.Value ? row["MaNCC"].ToString().Trim() : string.Empty,
                MaLoai = row["MaLoai"] != DBNull.Value ? row["MaLoai"].ToString().Trim() : string.Empty,
                TenSP = row["TenSP"] != DBNull.Value ? row["TenSP"].ToString().Trim() : string.Empty,
                DonViTinh = row["DonViTinh"] != DBNull.Value ? row["DonViTinh"].ToString().Trim() : string.Empty,
                DonGiaBan = giaBan,
                TrangThai = row["TrangThai"] != DBNull.Value ? row["TrangThai"].ToString().Trim() : string.Empty,
                Version = version > 0 ? version : 1
            };
        }

        public bool Exists(string maSP)
        {
            if (string.IsNullOrWhiteSpace(maSP))
                return false;

            string query = "SELECT COUNT(*) FROM SANPHAM WHERE MaSP = @MaSP";
            SqlParameter param = new SqlParameter("@MaSP", SqlDbType.Char, 10) { Value = maSP.Trim() };
            object result = Database.ExecuteScalar(query, param);
            return result != null && Convert.ToInt32(result) > 0;
        }

        private void CheckWritePermission()
        {
            if (!SessionManager.IsLoggedIn)
                throw new UnauthorizedAccessException("Yêu cầu phiên đăng nhập hợp lệ để thực hiện thao tác này.");

            if (!SessionManager.IsAdmin() && !SessionManager.IsSales())
                throw new UnauthorizedAccessException("Chỉ Quản trị viên hoặc Nhân viên bán hàng mới có quyền cập nhật danh mục sản phẩm.");
        }

        public HashSet<string> GetAllMaSP()
        {
            HashSet<string> set = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            string query = "SELECT RTRIM(MaSP) AS MaSP FROM SANPHAM";
            DataTable dt = Database.ExecuteQuery(query);
            foreach (DataRow row in dt.Rows)
            {
                if (row["MaSP"] != DBNull.Value)
                    set.Add(row["MaSP"].ToString().Trim());
            }
            return set;
        }

        public HashSet<string> GetAllMaLoai()
        {
            HashSet<string> set = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            string query = "SELECT RTRIM(MaLoai) AS MaLoai FROM LOAISANPHAM";
            DataTable dt = Database.ExecuteQuery(query);
            foreach (DataRow row in dt.Rows)
            {
                if (row["MaLoai"] != DBNull.Value)
                    set.Add(row["MaLoai"].ToString().Trim());
            }
            return set;
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

        public bool Insert(SanPham sp)
        {
            CheckWritePermission();

            string query = "INSERT INTO SANPHAM (MaSP, MaNCC, MaLoai, TenSP, DonViTinh, DonGiaBan, TrangThai, Version) " +
                           "VALUES (@MaSP, @MaNCC, @MaLoai, @TenSP, @DonViTinh, @DonGiaBan, @TrangThai, 1)";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaSP", SqlDbType.Char, 10) { Value = sp.MaSP.Trim() },
                new SqlParameter("@MaNCC", SqlDbType.Char, 10) { Value = sp.MaNCC.Trim() },
                new SqlParameter("@MaLoai", SqlDbType.Char, 10) { Value = sp.MaLoai.Trim() },
                new SqlParameter("@TenSP", SqlDbType.NVarChar, 150) { Value = (object)sp.TenSP ?? DBNull.Value },
                new SqlParameter("@DonViTinh", SqlDbType.NVarChar, 30) { Value = (object)sp.DonViTinh ?? DBNull.Value },
                new SqlParameter("@DonGiaBan", SqlDbType.Decimal) { Value = sp.DonGiaBan, Precision = 18, Scale = 2 },
                new SqlParameter("@TrangThai", SqlDbType.NVarChar, 30) { Value = (object)sp.TrangThai ?? DBNull.Value }
            };

            return Database.ExecuteNonQuery(query, parameters) > 0;
        }

        public int BulkInsert(IEnumerable<SanPham> items, SqlTransaction trans = null)
        {
            CheckWritePermission();
            if (items == null) return 0;

            string query = "INSERT INTO SANPHAM (MaSP, MaNCC, MaLoai, TenSP, DonViTinh, DonGiaBan, TrangThai, Version) " +
                           "VALUES (@MaSP, @MaNCC, @MaLoai, @TenSP, @DonViTinh, @DonGiaBan, @TrangThai, 1)";

            if (trans != null)
            {
                int count = 0;
                foreach (SanPham sp in items)
                {
                    using (SqlCommand cmd = new SqlCommand(query, trans.Connection, trans))
                    {
                        cmd.Parameters.Add(new SqlParameter("@MaSP", SqlDbType.Char, 10) { Value = sp.MaSP.Trim() });
                        cmd.Parameters.Add(new SqlParameter("@MaNCC", SqlDbType.Char, 10) { Value = sp.MaNCC.Trim() });
                        cmd.Parameters.Add(new SqlParameter("@MaLoai", SqlDbType.Char, 10) { Value = sp.MaLoai.Trim() });
                        cmd.Parameters.Add(new SqlParameter("@TenSP", SqlDbType.NVarChar, 150) { Value = (object)sp.TenSP ?? DBNull.Value });
                        cmd.Parameters.Add(new SqlParameter("@DonViTinh", SqlDbType.NVarChar, 30) { Value = (object)sp.DonViTinh ?? DBNull.Value });
                        cmd.Parameters.Add(new SqlParameter("@DonGiaBan", SqlDbType.Decimal) { Value = sp.DonGiaBan, Precision = 18, Scale = 2 });
                        cmd.Parameters.Add(new SqlParameter("@TrangThai", SqlDbType.NVarChar, 30) { Value = (object)sp.TrangThai ?? DBNull.Value });
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

        public ConcurrencyUpdateResult UpdateWithResult(SanPham sp)
        {
            CheckWritePermission();

            int currentVersion = sp.Version > 0 ? sp.Version : 1;

            string query = "UPDATE SANPHAM SET MaNCC = @MaNCC, MaLoai = @MaLoai, TenSP = @TenSP, " +
                           "DonViTinh = @DonViTinh, DonGiaBan = @DonGiaBan, TrangThai = @TrangThai, " +
                           "Version = ISNULL(Version, 1) + 1 " +
                           "WHERE MaSP = @MaSP AND ISNULL(Version, 1) = @Version";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaSP", SqlDbType.Char, 10) { Value = sp.MaSP.Trim() },
                new SqlParameter("@MaNCC", SqlDbType.Char, 10) { Value = sp.MaNCC.Trim() },
                new SqlParameter("@MaLoai", SqlDbType.Char, 10) { Value = sp.MaLoai.Trim() },
                new SqlParameter("@TenSP", SqlDbType.NVarChar, 150) { Value = (object)sp.TenSP ?? DBNull.Value },
                new SqlParameter("@DonViTinh", SqlDbType.NVarChar, 30) { Value = (object)sp.DonViTinh ?? DBNull.Value },
                new SqlParameter("@DonGiaBan", SqlDbType.Decimal) { Value = sp.DonGiaBan, Precision = 18, Scale = 2 },
                new SqlParameter("@TrangThai", SqlDbType.NVarChar, 30) { Value = (object)sp.TrangThai ?? DBNull.Value },
                new SqlParameter("@Version", SqlDbType.Int) { Value = currentVersion }
            };

            int rowsAffected = Database.ExecuteNonQuery(query, parameters);
            if (rowsAffected > 0)
            {
                sp.Version = currentVersion + 1;
                return ConcurrencyUpdateResult.Success;
            }

            if (Exists(sp.MaSP))
            {
                return ConcurrencyUpdateResult.ConcurrencyConflict;
            }

            return ConcurrencyUpdateResult.NotFoundOrFailed;
        }

        public bool Update(SanPham sp)
        {
            return UpdateWithResult(sp) == ConcurrencyUpdateResult.Success;
        }

        public bool Delete(string maSP)
        {
            CheckWritePermission();
            string query = "DELETE FROM SANPHAM WHERE MaSP = @MaSP";
            SqlParameter param = new SqlParameter("@MaSP", SqlDbType.Char, 10) { Value = maSP.Trim() };
            return Database.ExecuteNonQuery(query, param) > 0;
        }

        public bool Deactivate(string maSP)
        {
            CheckWritePermission();
            string query = "UPDATE SANPHAM SET TrangThai = @TrangThai, Version = ISNULL(Version, 1) + 1 WHERE MaSP = @MaSP";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@TrangThai", SqlDbType.NVarChar, 30) { Value = EntityStatusConstants.Product.Discontinued },
                new SqlParameter("@MaSP", SqlDbType.Char, 10) { Value = maSP.Trim() }
            };
            return Database.ExecuteNonQuery(query, parameters) > 0;
        }

        public SanPhamUsageStats GetUsageStatistics(string maSP)
        {
            SanPhamUsageStats stats = new SanPhamUsageStats();
            if (string.IsNullOrWhiteSpace(maSP))
                return stats;

            string query = @"
                SELECT 
                    (SELECT COUNT(*) FROM CHITIETDONDATHANG WHERE MaSP = @MaSP) AS OrderCount,
                    (SELECT COUNT(*) FROM CHITIETHOADONBAN WHERE MaSP = @MaSP) AS InvoiceCount,
                    (SELECT COUNT(*) FROM CHITIETPHIEUXUATKHO WHERE MaSP = @MaSP) AS IssueCount,
                    (SELECT COUNT(*) FROM TONKHO WHERE MaSP = @MaSP) AS StockRowCount,
                    (SELECT ISNULL(SUM(SoLuongTon), 0) FROM TONKHO WHERE MaSP = @MaSP) AS TotalStockQty;
            ";

            SqlParameter param = new SqlParameter("@MaSP", SqlDbType.Char, 10) { Value = maSP.Trim() };
            DataTable dt = Database.ExecuteQuery(query, param);
            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                stats.OrderCount = row["OrderCount"] != DBNull.Value ? Convert.ToInt32(row["OrderCount"]) : 0;
                stats.InvoiceCount = row["InvoiceCount"] != DBNull.Value ? Convert.ToInt32(row["InvoiceCount"]) : 0;
                stats.IssueCount = row["IssueCount"] != DBNull.Value ? Convert.ToInt32(row["IssueCount"]) : 0;
                stats.StockRowCount = row["StockRowCount"] != DBNull.Value ? Convert.ToInt32(row["StockRowCount"]) : 0;
                stats.TotalStockQty = row["TotalStockQty"] != DBNull.Value ? Convert.ToInt32(row["TotalStockQty"]) : 0;
            }

            return stats;
        }

        public DataTable Search(string keyword, string maLoai = null)
        {
            string query = "SELECT sp.MaSP, sp.TenSP, sp.MaLoai, lsp.TenLoai, sp.MaNCC, ncc.TenNCC, " +
                           "sp.DonViTinh, sp.DonGiaBan, sp.TrangThai, ISNULL(sp.Version, 1) AS Version " +
                           "FROM SANPHAM sp " +
                           "LEFT JOIN LOAISANPHAM lsp ON sp.MaLoai = lsp.MaLoai " +
                           "LEFT JOIN NHACUNGCAP ncc ON sp.MaNCC = ncc.MaNCC " +
                           "WHERE 1=1 ";

            System.Collections.Generic.List<SqlParameter> parameters = new System.Collections.Generic.List<SqlParameter>();

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                query += " AND (sp.MaSP LIKE @Keyword OR sp.TenSP LIKE @Keyword) ";
                parameters.Add(new SqlParameter("@Keyword", SqlDbType.NVarChar, 150) { Value = "%" + keyword.Trim() + "%" });
            }

            if (!string.IsNullOrWhiteSpace(maLoai))
            {
                query += " AND sp.MaLoai = @MaLoai ";
                parameters.Add(new SqlParameter("@MaLoai", SqlDbType.Char, 10) { Value = maLoai.Trim() });
            }

            query += " ORDER BY sp.MaSP";

            return Database.ExecuteQuery(query, parameters.ToArray());
        }
    }
}
