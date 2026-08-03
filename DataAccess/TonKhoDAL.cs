using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DNQH_KeToanBanHang.Helpers;
using DNQH_KeToanBanHang.Models;

namespace DNQH_KeToanBanHang.DataAccess
{
    public class TonKhoDAL
    {
        public DataTable GetAll()
        {
            string sql = @"
                SELECT tk.MaKho, k.TenKho, tk.MaSP, sp.TenSP, sp.DonViTinh, sp.DonGiaBan, tk.SoLuongTon, tk.NgayCapNhat
                FROM TONKHO tk
                INNER JOIN KHO k ON tk.MaKho = k.MaKho
                INNER JOIN SANPHAM sp ON tk.MaSP = sp.MaSP
                ORDER BY k.TenKho, sp.TenSP";

            return Database.ExecuteQuery(sql);
        }

        public DataTable GetByKho(string maKho)
        {
            string sql = @"
                SELECT tk.MaKho, k.TenKho, tk.MaSP, sp.TenSP, sp.DonViTinh, sp.DonGiaBan, tk.SoLuongTon, tk.NgayCapNhat
                FROM TONKHO tk
                INNER JOIN KHO k ON tk.MaKho = k.MaKho
                INNER JOIN SANPHAM sp ON tk.MaSP = sp.MaSP
                WHERE tk.MaKho = @MaKho
                ORDER BY sp.TenSP";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaKho", SqlDbType.Char, 10) { Value = maKho }
            };

            return Database.ExecuteQuery(sql, parameters);
        }

        public int GetTongTonKho(string maSP)
        {
            return GetTongTonKho(maSP, null);
        }

        public int GetTongTonKho(string maSP, SqlTransaction trans)
        {
            string sql = @"
                SELECT ISNULL(SUM(SoLuongTon), 0)
                FROM TONKHO
                WHERE MaSP = @MaSP";

            if (trans != null)
            {
                using (SqlCommand cmd = new SqlCommand(sql, trans.Connection, trans))
                {
                    cmd.Parameters.Add(new SqlParameter("@MaSP", SqlDbType.Char, 10) { Value = maSP });
                    object res = cmd.ExecuteScalar();
                    if (res != null && res != DBNull.Value)
                    {
                        return Convert.ToInt32(res);
                    }
                    return 0;
                }
            }

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaSP", SqlDbType.Char, 10) { Value = maSP }
            };

            object result = Database.ExecuteScalar(sql, parameters);
            if (result != null && result != DBNull.Value)
            {
                return Convert.ToInt32(result);
            }
            return 0;
        }

        public int GetTonKho(string maKho, string maSP)
        {
            return GetTonKho(maKho, maSP, null);
        }

        public int GetTonKho(string maKho, string maSP, SqlTransaction trans)
        {
            string sql = @"
                SELECT ISNULL(SoLuongTon, 0)
                FROM TONKHO
                WHERE MaKho = @MaKho AND MaSP = @MaSP";

            if (trans != null)
            {
                using (SqlCommand cmd = new SqlCommand(sql, trans.Connection, trans))
                {
                    cmd.Parameters.Add(new SqlParameter("@MaKho", SqlDbType.Char, 10) { Value = maKho });
                    cmd.Parameters.Add(new SqlParameter("@MaSP", SqlDbType.Char, 10) { Value = maSP });
                    object res = cmd.ExecuteScalar();
                    if (res != null && res != DBNull.Value)
                    {
                        return Convert.ToInt32(res);
                    }
                    return 0;
                }
            }

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaKho", SqlDbType.Char, 10) { Value = maKho },
                new SqlParameter("@MaSP", SqlDbType.Char, 10) { Value = maSP }
            };

            object result = Database.ExecuteScalar(sql, parameters);
            if (result != null && result != DBNull.Value)
            {
                return Convert.ToInt32(result);
            }
            return 0;
        }

        public bool TruTonKho(string maKho, string maSP, int soLuong, SqlTransaction trans)
        {
            if (trans == null)
            {
                throw new ArgumentNullException("trans", "Thao tác trừ tồn kho bắt buộc phải nằm trong SqlTransaction.");
            }

            string sql = @"
                UPDATE TONKHO
                SET SoLuongTon = SoLuongTon - @SoLuong,
                    NgayCapNhat = GETDATE()
                WHERE MaKho = @MaKho
                  AND MaSP = @MaSP
                  AND SoLuongTon >= @SoLuong";

            using (SqlCommand cmd = new SqlCommand(sql, trans.Connection, trans))
            {
                cmd.Parameters.Add(new SqlParameter("@SoLuong", SqlDbType.Int) { Value = soLuong });
                cmd.Parameters.Add(new SqlParameter("@MaKho", SqlDbType.Char, 10) { Value = maKho });
                cmd.Parameters.Add(new SqlParameter("@MaSP", SqlDbType.Char, 10) { Value = maSP });

                int rows = cmd.ExecuteNonQuery();
                return rows > 0;
            }
        }

        public DataTable SearchTonKho(string keyword, string maKho = null)
        {
            string sql = @"
                SELECT tk.MaKho, k.TenKho, tk.MaSP, sp.TenSP, sp.DonViTinh, sp.DonGiaBan, tk.SoLuongTon, tk.NgayCapNhat
                FROM TONKHO tk
                INNER JOIN KHO k ON tk.MaKho = k.MaKho
                INNER JOIN SANPHAM sp ON tk.MaSP = sp.MaSP
                WHERE (tk.MaSP LIKE @Keyword OR sp.TenSP LIKE @Keyword OR k.TenKho LIKE @Keyword)";

            if (!string.IsNullOrEmpty(maKho))
            {
                sql += " AND tk.MaKho = @MaKho";
            }

            sql += " ORDER BY k.TenKho, sp.TenSP";

            SqlParameter[] parameters;
            if (!string.IsNullOrEmpty(maKho))
            {
                parameters = new SqlParameter[]
                {
                    new SqlParameter("@Keyword", SqlDbType.NVarChar, 100) { Value = "%" + (keyword ?? "") + "%" },
                    new SqlParameter("@MaKho", SqlDbType.Char, 10) { Value = maKho }
                };
            }
            else
            {
                parameters = new SqlParameter[]
                {
                    new SqlParameter("@Keyword", SqlDbType.NVarChar, 100) { Value = "%" + (keyword ?? "") + "%" }
                };
            }

            return Database.ExecuteQuery(sql, parameters);
        }

        /// <summary>
        /// Lấy danh sách sản phẩm có tồn kho dưới ngưỡng an toàn (hết hàng, sắp hết hàng toàn công ty, hoặc có kho chi nhánh thiếu hàng).
        /// </summary>
        public List<CanhBaoTonKhoDTO> GetDanhSachCanhBaoTonKho(int threshold = 10, string maKho = null)
        {
            string sql;
            if (!string.IsNullOrEmpty(maKho))
            {
                sql = @"
                    SELECT 
                        sp.MaSP,
                        sp.TenSP,
                        ISNULL(lsp.TenLoai, N'') AS TenLoaiSP,
                        ISNULL(sp.DonViTinh, N'') AS DonViTinh,
                        ISNULL(sp.DonGiaBan, 0) AS DonGiaBan,
                        ISNULL(SUM(tk.SoLuongTon), 0) AS TongTon,
                        ISNULL(SUM(tk.SoLuongTon), 0) AS TonThapNhatKho,
                        CASE WHEN ISNULL(SUM(tk.SoLuongTon), 0) <= @Threshold THEN 1 ELSE 0 END AS SoKhoThieu
                    FROM SANPHAM sp
                    LEFT JOIN LOAISANPHAM lsp ON sp.MaLoai = lsp.MaLoai
                    LEFT JOIN TONKHO tk ON sp.MaSP = tk.MaSP AND tk.MaKho = @MaKho
                    WHERE sp.TrangThai IS NULL OR (sp.TrangThai <> N'Ngừng kinh doanh' AND sp.TrangThai <> N'Ngưng kinh doanh')
                    GROUP BY sp.MaSP, sp.TenSP, lsp.TenLoai, sp.DonViTinh, sp.DonGiaBan
                    HAVING ISNULL(SUM(tk.SoLuongTon), 0) <= @Threshold
                    ORDER BY ISNULL(SUM(tk.SoLuongTon), 0) ASC, sp.TenSP ASC";
            }
            else
            {
                sql = @"
                    WITH StockAgg AS (
                        SELECT 
                            sp.MaSP,
                            sp.TenSP,
                            ISNULL(lsp.TenLoai, N'') AS TenLoaiSP,
                            ISNULL(sp.DonViTinh, N'') AS DonViTinh,
                            ISNULL(sp.DonGiaBan, 0) AS DonGiaBan,
                            ISNULL(SUM(tk.SoLuongTon), 0) AS TongTon,
                            MIN(ISNULL(tk.SoLuongTon, 0)) AS TonThapNhatKho,
                            COUNT(CASE WHEN ISNULL(tk.SoLuongTon, 0) <= @Threshold THEN 1 END) AS SoKhoThieu
                        FROM SANPHAM sp
                        LEFT JOIN LOAISANPHAM lsp ON sp.MaLoai = lsp.MaLoai
                        LEFT JOIN TONKHO tk ON sp.MaSP = tk.MaSP
                        WHERE sp.TrangThai IS NULL OR (sp.TrangThai <> N'Ngừng kinh doanh' AND sp.TrangThai <> N'Ngưng kinh doanh')
                        GROUP BY sp.MaSP, sp.TenSP, lsp.TenLoai, sp.DonViTinh, sp.DonGiaBan
                    )
                    SELECT 
                        s.MaSP, s.TenSP, s.TenLoaiSP, s.DonViTinh, s.DonGiaBan, s.TongTon, s.TonThapNhatKho, s.SoKhoThieu
                    FROM StockAgg s
                    WHERE s.TongTon <= @Threshold OR s.SoKhoThieu > 0
                    ORDER BY s.TongTon ASC, s.TonThapNhatKho ASC, s.TenSP ASC";
            }

            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@Threshold", SqlDbType.Int) { Value = threshold }
            };
            if (!string.IsNullOrEmpty(maKho))
            {
                parameters.Add(new SqlParameter("@MaKho", SqlDbType.Char, 10) { Value = maKho });
            }

            DataTable dt = Database.ExecuteQuery(sql, parameters.ToArray());
            List<CanhBaoTonKhoDTO> list = new List<CanhBaoTonKhoDTO>();

            if (dt != null && dt.Rows.Count > 0)
            {
                string sqlKhoChiTiet = @"
                    SELECT tk.MaSP, k.TenKho, tk.SoLuongTon
                    FROM TONKHO tk
                    INNER JOIN KHO k ON tk.MaKho = k.MaKho
                    ORDER BY k.TenKho";
                DataTable dtKho = Database.ExecuteQuery(sqlKhoChiTiet);

                foreach (DataRow row in dt.Rows)
                {
                    string maSP = row["MaSP"].ToString().Trim();
                    int tongTon = Convert.ToInt32(row["TongTon"]);
                    int tonThapNhatKho = row.Table.Columns.Contains("TonThapNhatKho") && row["TonThapNhatKho"] != DBNull.Value
                        ? Convert.ToInt32(row["TonThapNhatKho"]) : tongTon;
                    int soKhoThieu = row.Table.Columns.Contains("SoKhoThieu") && row["SoKhoThieu"] != DBNull.Value
                        ? Convert.ToInt32(row["SoKhoThieu"]) : 0;

                    List<string> khoItems = new List<string>();
                    if (dtKho != null)
                    {
                        foreach (DataRow rk in dtKho.Rows)
                        {
                            if (rk["MaSP"].ToString().Trim() == maSP)
                            {
                                int soLuongKho = Convert.ToInt32(rk["SoLuongTon"]);
                                string khoBadge = soLuongKho <= threshold ? string.Format("{0}: {1} (⚠️)", rk["TenKho"].ToString().Trim(), soLuongKho)
                                                                          : string.Format("{0}: {1}", rk["TenKho"].ToString().Trim(), soLuongKho);
                                khoItems.Add(khoBadge);
                            }
                        }
                    }

                    string chiTietKho = khoItems.Count > 0 ? string.Join(" | ", khoItems) : "Chưa phân bổ kho";

                    list.Add(new CanhBaoTonKhoDTO
                    {
                        MaSP = maSP,
                        TenSP = row["TenSP"].ToString().Trim(),
                        TenLoaiSP = row["TenLoaiSP"].ToString().Trim(),
                        DonViTinh = row["DonViTinh"].ToString().Trim(),
                        DonGiaBan = Convert.ToDecimal(row["DonGiaBan"]),
                        TongTon = tongTon,
                        TonThapNhatKho = tonThapNhatKho,
                        SoKhoThieu = soKhoThieu,
                        MucDoCanhBao = CanhBaoTonKhoDTO.XacDinhMucDoCanhBao(tongTon, threshold, tonThapNhatKho),
                        ChiTietKho = chiTietKho
                    });
                }
            }

            return list;
        }
    }
}
