using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DNQH_KeToanBanHang.Constants;
using DNQH_KeToanBanHang.Helpers;
using DNQH_KeToanBanHang.Models;

namespace DNQH_KeToanBanHang.DataAccess
{
    public class PhieuXuatKhoDAL
    {
        private void CheckWritePermission()
        {
            if (!SessionManager.IsLoggedIn)
            {
                throw new UnauthorizedAccessException("Yêu cầu đăng nhập trước khi thực hiện thao tác phiếu xuất kho.");
            }

            if (!SessionManager.IsAdmin() && !SessionManager.IsWarehouse())
            {
                throw new UnauthorizedAccessException("Bạn không có quyền thực hiện thao tác lập phiếu xuất kho. Chỉ Quản trị viên và Nhân viên kho mới có quyền này.");
            }
        }

        public string GetNextMaPXK(SqlTransaction trans = null)
        {
            string sql = trans != null
                ? "SELECT TOP 1 MaPXK FROM PHIEUXUATKHO WITH (TABLOCKX, HOLDLOCK) WHERE MaPXK LIKE 'PXK%' ORDER BY MaPXK DESC"
                : "SELECT TOP 1 MaPXK FROM PHIEUXUATKHO WHERE MaPXK LIKE 'PXK%' ORDER BY MaPXK DESC";

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
                if (maxCode.Length >= 4 && maxCode.StartsWith("PXK"))
                {
                    string numPart = maxCode.Substring(3);
                    int currentNum;
                    if (int.TryParse(numPart, out currentNum))
                    {
                        return string.Format("PXK{0:D7}", currentNum + 1);
                    }
                }
            }

            return "PXK0000001";
        }

        public DataTable GetHoaDonSanSangXuatKho()
        {
            string sql = @"
                SELECT hdb.MaHDB, hdb.NgayLap, hdb.MaDDH, hdb.MaKH, kh.TenKH, 
                       hdb.MaNV, nv.HoTen AS TenNV, hdb.TongTien, hdb.TrangThai, hdb.GhiChu
                FROM HOADONBAN hdb
                INNER JOIN KHACHHANG kh ON hdb.MaKH = kh.MaKH
                INNER JOIN NHANVIEN nv ON hdb.MaNV = nv.MaNV
                WHERE (hdb.TrangThai IS NULL OR (hdb.TrangThai <> N'Hủy' AND hdb.TrangThai <> N'Đã hủy'))
                  AND EXISTS (
                      SELECT 1 
                      FROM CHITIETHOADONBAN ct
                      WHERE ct.MaHDB = hdb.MaHDB
                        AND ct.SoLuong > ISNULL((
                            SELECT SUM(ctx.SoLuongXuat)
                            FROM CHITIETPHIEUXUATKHO ctx
                            INNER JOIN PHIEUXUATKHO px ON ctx.MaPXK = px.MaPXK
                            WHERE px.MaHDB = ct.MaHDB AND ctx.MaSP = ct.MaSP
                        ), 0)
                  )
                ORDER BY hdb.NgayLap DESC, hdb.MaHDB DESC";

            return Database.ExecuteQuery(sql);
        }

        public bool LockHoaDonForUpdate(string maHDB, SqlTransaction trans)
        {
            string sql = @"
                SELECT TrangThai 
                FROM HOADONBAN WITH (UPDLOCK, HOLDLOCK) 
                WHERE MaHDB = @MaHDB";

            using (SqlCommand cmd = new SqlCommand(sql, trans.Connection, trans))
            {
                cmd.Parameters.Add(new SqlParameter("@MaHDB", SqlDbType.Char, 12) { Value = maHDB });
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (!reader.Read())
                    {
                        return false;
                    }

                    string trangThai = !reader.IsDBNull(0) ? reader.GetString(0).Trim() : "";
                    if (InvoiceStatusConstants.IsCancelled(trangThai))
                    {
                        throw new InvalidOperationException(string.Format("Hóa đơn bán [{0}] đã bị hủy, không thể xuất kho!", maHDB));
                    }

                    return true;
                }
            }
        }

        public DataTable GetChiTietHoaDonKemTienDoXuat(string maHDB)
        {
            string sql = @"
                SELECT ct.MaHDB, ct.MaSP, sp.TenSP, sp.DonViTinh, sp.DonGiaBan, ct.SoLuong AS SoLuongHoaDon,
                       ISNULL((
                           SELECT SUM(ctx.SoLuongXuat)
                           FROM CHITIETPHIEUXUATKHO ctx
                           INNER JOIN PHIEUXUATKHO px ON ctx.MaPXK = px.MaPXK
                           WHERE px.MaHDB = ct.MaHDB AND ctx.MaSP = ct.MaSP
                       ), 0) AS SoLuongDaXuat
                FROM CHITIETHOADONBAN ct
                INNER JOIN SANPHAM sp ON ct.MaSP = sp.MaSP
                WHERE ct.MaHDB = @MaHDB
                ORDER BY sp.TenSP";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaHDB", SqlDbType.Char, 12) { Value = maHDB }
            };

            DataTable dt = Database.ExecuteQuery(sql, parameters);
            dt.Columns.Add("SoLuongConLai", typeof(int));

            foreach (DataRow row in dt.Rows)
            {
                int slHoaDon = Convert.ToInt32(row["SoLuongHoaDon"]);
                int slDaXuat = Convert.ToInt32(row["SoLuongDaXuat"]);
                int conLai = slHoaDon - slDaXuat;
                row["SoLuongConLai"] = conLai > 0 ? conLai : 0;
            }

            return dt;
        }

        public int GetTongSoLuongDaXuat(string maHDB, string maSP, SqlTransaction trans = null)
        {
            string sql = @"
                SELECT ISNULL(SUM(ctx.SoLuongXuat), 0)
                FROM CHITIETPHIEUXUATKHO ctx
                INNER JOIN PHIEUXUATKHO px ON ctx.MaPXK = px.MaPXK
                WHERE px.MaHDB = @MaHDB AND ctx.MaSP = @MaSP";

            if (trans != null)
            {
                using (SqlCommand cmd = new SqlCommand(sql, trans.Connection, trans))
                {
                    cmd.Parameters.Add(new SqlParameter("@MaHDB", SqlDbType.Char, 12) { Value = maHDB });
                    cmd.Parameters.Add(new SqlParameter("@MaSP", SqlDbType.Char, 10) { Value = maSP });
                    object res = cmd.ExecuteScalar();
                    return (res != null && res != DBNull.Value) ? Convert.ToInt32(res) : 0;
                }
            }

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaHDB", SqlDbType.Char, 12) { Value = maHDB },
                new SqlParameter("@MaSP", SqlDbType.Char, 10) { Value = maSP }
            };

            object result = Database.ExecuteScalar(sql, parameters);
            return (result != null && result != DBNull.Value) ? Convert.ToInt32(result) : 0;
        }

        public DataTable GetAll()
        {
            string sql = @"
                SELECT pxk.MaPXK, pxk.NgayXuat, pxk.MaHDB, pxk.MaKho, k.TenKho, 
                       pxk.MaNV, nv.HoTen AS TenNV, kh.TenKH, hdb.TongTien AS TongTienHDB, 
                       pxk.LyDoXuat, pxk.TrangThai
                FROM PHIEUXUATKHO pxk
                INNER JOIN KHO k ON pxk.MaKho = k.MaKho
                INNER JOIN NHANVIEN nv ON pxk.MaNV = nv.MaNV
                INNER JOIN HOADONBAN hdb ON pxk.MaHDB = hdb.MaHDB
                INNER JOIN KHACHHANG kh ON hdb.MaKH = kh.MaKH
                ORDER BY pxk.NgayXuat DESC, pxk.MaPXK DESC";

            return Database.ExecuteQuery(sql);
        }

        public PhieuXuatKho GetById(string maPXK)
        {
            string sql = @"
                SELECT pxk.MaPXK, pxk.NgayXuat, pxk.MaHDB, pxk.MaKho, k.TenKho, 
                       pxk.MaNV, nv.HoTen AS TenNV, kh.TenKH, hdb.TongTien AS TongTienHDB, 
                       pxk.LyDoXuat, pxk.TrangThai
                FROM PHIEUXUATKHO pxk
                INNER JOIN KHO k ON pxk.MaKho = k.MaKho
                INNER JOIN NHANVIEN nv ON pxk.MaNV = nv.MaNV
                INNER JOIN HOADONBAN hdb ON pxk.MaHDB = hdb.MaHDB
                INNER JOIN KHACHHANG kh ON hdb.MaKH = kh.MaKH
                WHERE pxk.MaPXK = @MaPXK";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaPXK", SqlDbType.Char, 10) { Value = maPXK }
            };

            DataTable dt = Database.ExecuteQuery(sql, parameters);
            if (dt.Rows.Count == 0)
                return null;

            DataRow row = dt.Rows[0];
            return new PhieuXuatKho
            {
                MaPXK = row["MaPXK"].ToString().Trim(),
                NgayXuat = row["NgayXuat"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(row["NgayXuat"]) : null,
                MaHDB = row["MaHDB"].ToString().Trim(),
                MaKho = row["MaKho"].ToString().Trim(),
                TenKho = row["TenKho"].ToString().Trim(),
                MaNV = row["MaNV"].ToString().Trim(),
                TenNV = row["TenNV"].ToString().Trim(),
                TenKH = row["TenKH"].ToString().Trim(),
                TongTienHDB = row["TongTienHDB"] != DBNull.Value ? Convert.ToDecimal(row["TongTienHDB"]) : 0,
                LyDoXuat = row["LyDoXuat"] != DBNull.Value ? row["LyDoXuat"].ToString().Trim() : "",
                TrangThai = row["TrangThai"] != DBNull.Value ? row["TrangThai"].ToString().Trim() : ""
            };
        }

        public DataTable GetChiTietDataTable(string maPXK)
        {
            string sql = @"
                SELECT ctx.MaPXK, ctx.MaSP, sp.TenSP, sp.DonViTinh, ctx.SoLuongXuat, sp.DonGiaBan
                FROM CHITIETPHIEUXUATKHO ctx
                INNER JOIN SANPHAM sp ON ctx.MaSP = sp.MaSP
                WHERE ctx.MaPXK = @MaPXK
                ORDER BY sp.TenSP";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaPXK", SqlDbType.Char, 10) { Value = maPXK }
            };

            return Database.ExecuteQuery(sql, parameters);
        }

        public List<ChiTietPhieuXuatKho> GetChiTietList(string maPXK)
        {
            List<ChiTietPhieuXuatKho> list = new List<ChiTietPhieuXuatKho>();
            DataTable dt = GetChiTietDataTable(maPXK);

            foreach (DataRow row in dt.Rows)
            {
                list.Add(new ChiTietPhieuXuatKho
                {
                    MaPXK = row["MaPXK"].ToString().Trim(),
                    MaSP = row["MaSP"].ToString().Trim(),
                    TenSP = row["TenSP"].ToString().Trim(),
                    DonViTinh = row["DonViTinh"] != DBNull.Value ? row["DonViTinh"].ToString().Trim() : "",
                    SoLuongXuat = Convert.ToInt32(row["SoLuongXuat"]),
                    DonGiaBan = row["DonGiaBan"] != DBNull.Value ? Convert.ToDecimal(row["DonGiaBan"]) : 0
                });
            }

            return list;
        }

        public int Insert(PhieuXuatKho pxk, SqlTransaction trans)
        {
            CheckWritePermission();

            string sql = @"
                INSERT INTO PHIEUXUATKHO (MaPXK, MaNV, MaHDB, MaKho, NgayXuat, LyDoXuat, TrangThai)
                VALUES (@MaPXK, @MaNV, @MaHDB, @MaKho, @NgayXuat, @LyDoXuat, @TrangThai)";

            using (SqlCommand cmd = new SqlCommand(sql, trans.Connection, trans))
            {
                cmd.Parameters.Add(new SqlParameter("@MaPXK", SqlDbType.Char, 10) { Value = pxk.MaPXK });
                cmd.Parameters.Add(new SqlParameter("@MaNV", SqlDbType.Char, 10) { Value = pxk.MaNV });
                cmd.Parameters.Add(new SqlParameter("@MaHDB", SqlDbType.Char, 12) { Value = pxk.MaHDB });
                cmd.Parameters.Add(new SqlParameter("@MaKho", SqlDbType.Char, 10) { Value = pxk.MaKho });
                cmd.Parameters.Add(new SqlParameter("@NgayXuat", SqlDbType.DateTime) { Value = (object)pxk.NgayXuat ?? DBNull.Value });
                cmd.Parameters.Add(new SqlParameter("@LyDoXuat", SqlDbType.NVarChar, 200) { Value = (object)pxk.LyDoXuat ?? DBNull.Value });
                cmd.Parameters.Add(new SqlParameter("@TrangThai", SqlDbType.NVarChar, 30) { Value = (object)pxk.TrangThai ?? DBNull.Value });

                return cmd.ExecuteNonQuery();
            }
        }

        public int InsertChiTiet(ChiTietPhieuXuatKho ct, SqlTransaction trans)
        {
            CheckWritePermission();

            string sql = @"
                INSERT INTO CHITIETPHIEUXUATKHO (MaPXK, MaSP, SoLuongXuat)
                VALUES (@MaPXK, @MaSP, @SoLuongXuat)";

            using (SqlCommand cmd = new SqlCommand(sql, trans.Connection, trans))
            {
                cmd.Parameters.Add(new SqlParameter("@MaPXK", SqlDbType.Char, 10) { Value = ct.MaPXK });
                cmd.Parameters.Add(new SqlParameter("@MaSP", SqlDbType.Char, 10) { Value = ct.MaSP });
                cmd.Parameters.Add(new SqlParameter("@SoLuongXuat", SqlDbType.Int) { Value = ct.SoLuongXuat });

                return cmd.ExecuteNonQuery();
            }
        }

        public DataTable Search(string keyword, string maKho = null)
        {
            string sql = @"
                SELECT pxk.MaPXK, pxk.NgayXuat, pxk.MaHDB, pxk.MaKho, k.TenKho, 
                       pxk.MaNV, nv.HoTen AS TenNV, kh.TenKH, hdb.TongTien AS TongTienHDB, 
                       pxk.LyDoXuat, pxk.TrangThai
                FROM PHIEUXUATKHO pxk
                INNER JOIN KHO k ON pxk.MaKho = k.MaKho
                INNER JOIN NHANVIEN nv ON pxk.MaNV = nv.MaNV
                INNER JOIN HOADONBAN hdb ON pxk.MaHDB = hdb.MaHDB
                INNER JOIN KHACHHANG kh ON hdb.MaKH = kh.MaKH
                WHERE (pxk.MaPXK LIKE @Keyword OR pxk.MaHDB LIKE @Keyword OR kh.TenKH LIKE @Keyword OR nv.HoTen LIKE @Keyword)";

            if (!string.IsNullOrEmpty(maKho))
            {
                sql += " AND pxk.MaKho = @MaKho";
            }

            sql += " ORDER BY pxk.NgayXuat DESC, pxk.MaPXK DESC";

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
    }
}
