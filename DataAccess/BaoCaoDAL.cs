using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DNQH_KeToanBanHang.Helpers;
using DNQH_KeToanBanHang.Models;

namespace DNQH_KeToanBanHang.DataAccess
{
    public class BaoCaoDAL
    {
        /// <summary>
        /// Ràng buộc phân quyền dữ liệu tầng DataAccess: Chỉ Admin và Kế toán được phép đọc báo cáo.
        /// </summary>
        public static void CheckReadPermission()
        {
            if (!SessionManager.IsLoggedIn || SessionManager.CurrentUser == null)
            {
                throw new UnauthorizedAccessException("Yêu cầu đăng nhập trước khi xem báo cáo kế toán.");
            }

            if (!SessionManager.IsAdmin() && !SessionManager.IsAccountant())
            {
                throw new UnauthorizedAccessException("Bạn không có quyền xem Báo cáo & Kế toán chi tiết. Chỉ Quản trị viên và Nhân viên kế toán mới có quyền này.");
            }
        }

        /// <summary>
        /// Báo cáo doanh thu bán hàng theo kỳ
        /// </summary>
        public List<BaoCaoDoanhThuDTO> GetBaoCaoDoanhThu(DateTime tuNgay, DateTime denNgay, string maKH = null)
        {
            CheckReadPermission();

            DateTime dtTu = tuNgay.Date;
            DateTime dtDen = denNgay.Date.AddDays(1); // Exclusive end date

            string sql = @"
                SELECT 
                    h.MaHDB,
                    h.NgayLap,
                    h.MaKH,
                    ISNULL(kh.TenKH, N'Khách vãng lai') AS TenKH,
                    h.MaNV,
                    ISNULL(nv.HoTen, N'') AS TenNV,
                    h.TongTien,
                    ISNULL(h.TrangThai, N'Chưa thanh toán') AS TrangThai,
                    h.GhiChu,
                    (SELECT COUNT(*) FROM CHITIETHOADONBAN ct WHERE ct.MaHDB = h.MaHDB) AS SoMatHang
                FROM HOADONBAN h
                LEFT JOIN KHACHHANG kh ON h.MaKH = kh.MaKH
                LEFT JOIN NHANVIEN nv ON h.MaNV = nv.MaNV
                WHERE h.NgayLap >= @TuNgay AND h.NgayLap < @DenNgay
                  AND (h.TrangThai IS NULL OR (h.TrangThai <> N'Hủy' AND h.TrangThai <> N'Đã hủy' AND h.TrangThai NOT LIKE N'%Hủy%' AND h.TrangThai NOT LIKE N'%hủy%'))
                  AND (@MaKH IS NULL OR @MaKH = '' OR h.MaKH = @MaKH)
                ORDER BY h.NgayLap DESC, h.MaHDB DESC";

            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@TuNgay", SqlDbType.DateTime) { Value = dtTu },
                new SqlParameter("@DenNgay", SqlDbType.DateTime) { Value = dtDen },
                new SqlParameter("@MaKH", SqlDbType.Char, 10) { Value = (object)maKH ?? DBNull.Value }
            };

            DataTable dt = Database.ExecuteQuery(sql, parameters.ToArray());
            List<BaoCaoDoanhThuDTO> list = new List<BaoCaoDoanhThuDTO>();

            foreach (DataRow row in dt.Rows)
            {
                list.Add(new BaoCaoDoanhThuDTO
                {
                    MaHDB = row["MaHDB"].ToString().Trim(),
                    NgayLap = Convert.ToDateTime(row["NgayLap"]),
                    MaKH = row["MaKH"] != DBNull.Value ? row["MaKH"].ToString().Trim() : "",
                    TenKH = row["TenKH"].ToString().Trim(),
                    MaNV = row["MaNV"] != DBNull.Value ? row["MaNV"].ToString().Trim() : "",
                    TenNV = row["TenNV"].ToString().Trim(),
                    TongTien = Convert.ToDecimal(row["TongTien"]),
                    TrangThai = row["TrangThai"].ToString().Trim(),
                    GhiChu = row["GhiChu"] != DBNull.Value ? row["GhiChu"].ToString().Trim() : "",
                    SoMatHang = Convert.ToInt32(row["SoMatHang"])
                });
            }

            return list;
        }

        /// <summary>
        /// Báo cáo tổng hợp Thu - Chi (Sổ quỹ) theo kỳ
        /// </summary>
        public List<BaoCaoThuChiDTO> GetBaoCaoThuChi(DateTime tuNgay, DateTime denNgay)
        {
            CheckReadPermission();

            DateTime dtTu = tuNgay.Date;
            DateTime dtDen = denNgay.Date.AddDays(1);

            string sql = @"
                SELECT 
                    pt.NgayThu AS NgayGiaoDich,
                    N'Thu tiền' AS LoaiGiaoDich,
                    pt.MaPT AS MaChungTu,
                    pt.NguoiNop AS NguoiGiaoDich,
                    pt.SoTien AS SoTienThu,
                    CAST(0 AS DECIMAL(18,0)) AS SoTienChi,
                    pt.HinhThuc,
                    pt.LyDoThu AS LyDo,
                    pt.MaNV,
                    ISNULL(nv.HoTen, N'') AS TenNV,
                    pt.MaHDB AS MaHDBLienKet
                FROM PHIEUTHU pt
                LEFT JOIN NHANVIEN nv ON pt.MaNV = nv.MaNV
                WHERE pt.NgayThu >= @TuNgay AND pt.NgayThu < @DenNgay

                UNION ALL

                SELECT 
                    pc.NgayChi AS NgayGiaoDich,
                    N'Chi tiền' AS LoaiGiaoDich,
                    pc.MaPC AS MaChungTu,
                    pc.NguoiNhan AS NguoiGiaoDich,
                    CAST(0 AS DECIMAL(18,0)) AS SoTienThu,
                    pc.SoTien AS SoTienChi,
                    pc.HinhThuc,
                    pc.LyDoChi AS LyDo,
                    pc.MaNV,
                    ISNULL(nv.HoTen, N'') AS TenNV,
                    NULL AS MaHDBLienKet
                FROM PHIEUCHI pc
                LEFT JOIN NHANVIEN nv ON pc.MaNV = nv.MaNV
                WHERE pc.NgayChi >= @TuNgay AND pc.NgayChi < @DenNgay

                ORDER BY NgayGiaoDich DESC, MaChungTu DESC";

            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@TuNgay", SqlDbType.DateTime) { Value = dtTu },
                new SqlParameter("@DenNgay", SqlDbType.DateTime) { Value = dtDen }
            };

            DataTable dt = Database.ExecuteQuery(sql, parameters.ToArray());
            List<BaoCaoThuChiDTO> list = new List<BaoCaoThuChiDTO>();

            foreach (DataRow row in dt.Rows)
            {
                list.Add(new BaoCaoThuChiDTO
                {
                    NgayGiaoDich = Convert.ToDateTime(row["NgayGiaoDich"]),
                    LoaiGiaoDich = row["LoaiGiaoDich"].ToString().Trim(),
                    MaChungTu = row["MaChungTu"].ToString().Trim(),
                    NguoiGiaoDich = row["NguoiGiaoDich"] != DBNull.Value ? row["NguoiGiaoDich"].ToString().Trim() : "",
                    SoTienThu = Convert.ToDecimal(row["SoTienThu"]),
                    SoTienChi = Convert.ToDecimal(row["SoTienChi"]),
                    HinhThuc = row["HinhThuc"] != DBNull.Value ? row["HinhThuc"].ToString().Trim() : "",
                    LyDo = row["LyDo"] != DBNull.Value ? row["LyDo"].ToString().Trim() : "",
                    MaNV = row["MaNV"] != DBNull.Value ? row["MaNV"].ToString().Trim() : "",
                    TenNV = row["TenNV"].ToString().Trim(),
                    MaHDBLienKet = row["MaHDBLienKet"] != DBNull.Value ? row["MaHDBLienKet"].ToString().Trim() : ""
                });
            }

            return list;
        }

        /// <summary>
        /// Báo cáo tổng hợp tồn kho hiện tại
        /// </summary>
        public List<BaoCaoTonKhoDTO> GetBaoCaoTonKho(string maKho = null)
        {
            CheckReadPermission();

            string sql = @"
                SELECT 
                    tk.MaKho,
                    k.TenKho,
                    tk.MaSP,
                    sp.TenSP,
                    ISNULL(lsp.TenLoai, N'') AS TenLoaiSP,
                    ISNULL(sp.DonViTinh, N'') AS DonViTinh,
                    ISNULL(tk.SoLuongTon, 0) AS SoLuongTon,
                    ISNULL(sp.DonGiaBan, 0) AS DonGia,
                    (ISNULL(tk.SoLuongTon, 0) * ISNULL(sp.DonGiaBan, 0)) AS GiaTriTon,
                    tk.NgayCapNhat
                FROM TONKHO tk
                INNER JOIN KHO k ON tk.MaKho = k.MaKho
                INNER JOIN SANPHAM sp ON tk.MaSP = sp.MaSP
                LEFT JOIN LOAISANPHAM lsp ON sp.MaLoai = lsp.MaLoai
                WHERE (@MaKho IS NULL OR @MaKho = '' OR tk.MaKho = @MaKho)
                ORDER BY k.TenKho, sp.TenSP";

            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@MaKho", SqlDbType.Char, 10) { Value = (object)maKho ?? DBNull.Value }
            };

            DataTable dt = Database.ExecuteQuery(sql, parameters.ToArray());
            List<BaoCaoTonKhoDTO> list = new List<BaoCaoTonKhoDTO>();

            foreach (DataRow row in dt.Rows)
            {
                list.Add(new BaoCaoTonKhoDTO
                {
                    MaKho = row["MaKho"].ToString().Trim(),
                    TenKho = row["TenKho"].ToString().Trim(),
                    MaSP = row["MaSP"].ToString().Trim(),
                    TenSP = row["TenSP"].ToString().Trim(),
                    TenLoaiSP = row["TenLoaiSP"].ToString().Trim(),
                    DonViTinh = row["DonViTinh"].ToString().Trim(),
                    SoLuongTon = row["SoLuongTon"] != DBNull.Value ? Convert.ToInt32(row["SoLuongTon"]) : 0,
                    DonGia = row["DonGia"] != DBNull.Value ? Convert.ToDecimal(row["DonGia"]) : 0,
                    GiaTriTon = row["GiaTriTon"] != DBNull.Value ? Convert.ToDecimal(row["GiaTriTon"]) : 0,
                    NgayCapNhat = row["NgayCapNhat"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(row["NgayCapNhat"]) : null
                });
            }

            return list;
        }

        /// <summary>
        /// Sổ chi tiết công nợ và thanh toán của khách hàng
        /// </summary>
        public List<SoChiTietKhachHangDTO> GetSoChiTietKhachHang(string maKH, DateTime tuNgay, DateTime denNgay)
        {
            CheckReadPermission();

            DateTime dtTu = tuNgay.Date;
            DateTime dtDen = denNgay.Date.AddDays(1);

            // 1. Tính số dư nợ đầu kỳ lũy kế trước thời điểm @TuNgay (loại trừ hóa đơn đã hủy)
            string sqlSoDuDauKy = @"
                SELECT 
                    (
                        ISNULL((
                            SELECT SUM(h.TongTien) 
                            FROM HOADONBAN h 
                            WHERE h.MaKH = @MaKH 
                              AND h.NgayLap < @TuNgay 
                              AND (h.TrangThai IS NULL OR (h.TrangThai <> N'Hủy' AND h.TrangThai <> N'Đã hủy' AND h.TrangThai NOT LIKE N'%Hủy%' AND h.TrangThai NOT LIKE N'%hủy%'))
                        ), 0)
                        -
                        ISNULL((
                            SELECT SUM(pt.SoTien) 
                            FROM PHIEUTHU pt 
                            INNER JOIN HOADONBAN h ON pt.MaHDB = h.MaHDB 
                            WHERE h.MaKH = @MaKH 
                              AND pt.NgayThu < @TuNgay
                        ), 0)
                    ) AS SoDuDauKy";

            object objSoDuDauKy = Database.ExecuteScalar(sqlSoDuDauKy,
                new SqlParameter("@MaKH", SqlDbType.Char, 10) { Value = (object)maKH ?? DBNull.Value },
                new SqlParameter("@TuNgay", SqlDbType.DateTime) { Value = dtTu });

            decimal soDuDauKy = (objSoDuDauKy != null && objSoDuDauKy != DBNull.Value) ? Convert.ToDecimal(objSoDuDauKy) : 0;

            // 2. Lấy các nghiệp vụ phát sinh trong khoảng thời gian [@TuNgay, @DenNgay)
            string sql = @"
                SELECT 
                    h.NgayLap AS NgayGiaoDich,
                    N'Hóa đơn bán' AS LoaiNghiepVu,
                    h.MaHDB AS SoChungTu,
                    ISNULL(h.GhiChu, N'Xuất bán hàng hóa') AS DienGiai,
                    h.TongTien AS PhatSinhNo,
                    CAST(0 AS DECIMAL(18,0)) AS PhatSinhCo
                FROM HOADONBAN h
                WHERE h.MaKH = @MaKH AND h.NgayLap >= @TuNgay AND h.NgayLap < @DenNgay
                  AND (h.TrangThai IS NULL OR (h.TrangThai <> N'Hủy' AND h.TrangThai <> N'Đã hủy' AND h.TrangThai NOT LIKE N'%Hủy%' AND h.TrangThai NOT LIKE N'%hủy%'))

                UNION ALL

                SELECT 
                    pt.NgayThu AS NgayGiaoDich,
                    N'Phiếu thu' AS LoaiNghiepVu,
                    pt.MaPT AS SoChungTu,
                    ISNULL(pt.LyDoThu, N'Thu tiền bán hàng') AS DienGiai,
                    CAST(0 AS DECIMAL(18,0)) AS PhatSinhNo,
                    pt.SoTien AS PhatSinhCo
                FROM PHIEUTHU pt
                INNER JOIN HOADONBAN h ON pt.MaHDB = h.MaHDB
                WHERE h.MaKH = @MaKH AND pt.NgayThu >= @TuNgay AND pt.NgayThu < @DenNgay

                ORDER BY NgayGiaoDich ASC, SoChungTu ASC";

            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@MaKH", SqlDbType.Char, 10) { Value = (object)maKH ?? DBNull.Value },
                new SqlParameter("@TuNgay", SqlDbType.DateTime) { Value = dtTu },
                new SqlParameter("@DenNgay", SqlDbType.DateTime) { Value = dtDen }
            };

            DataTable dt = Database.ExecuteQuery(sql, parameters.ToArray());
            List<SoChiTietKhachHangDTO> list = new List<SoChiTietKhachHangDTO>();

            // Dòng Số dư đầu kỳ luôn hiển thị ở đầu sổ kế toán để bảo đảm tính chuẩn xác của công nợ
            list.Add(new SoChiTietKhachHangDTO
            {
                NgayGiaoDich = dtTu,
                LoaiNghiepVu = "Số dư đầu kỳ",
                SoChungTu = "-",
                DienGiai = "Số dư công nợ lũy kế trước ngày " + dtTu.ToString("dd/MM/yyyy"),
                PhatSinhNo = 0,
                PhatSinhCo = 0,
                SoDuCuoiKy = soDuDauKy
            });

            decimal soDu = soDuDauKy;

            foreach (DataRow row in dt.Rows)
            {
                decimal phatSinhNo = Convert.ToDecimal(row["PhatSinhNo"]);
                decimal phatSinhCo = Convert.ToDecimal(row["PhatSinhCo"]);
                soDu += (phatSinhNo - phatSinhCo);

                list.Add(new SoChiTietKhachHangDTO
                {
                    NgayGiaoDich = Convert.ToDateTime(row["NgayGiaoDich"]),
                    LoaiNghiepVu = row["LoaiNghiepVu"].ToString().Trim(),
                    SoChungTu = row["SoChungTu"].ToString().Trim(),
                    DienGiai = row["DienGiai"].ToString().Trim(),
                    PhatSinhNo = phatSinhNo,
                    PhatSinhCo = phatSinhCo,
                    SoDuCuoiKy = soDu
                });
            }

            return list;
        }

        /// <summary>
        /// Sổ chi tiết sản phẩm: Phân tích số lượng và doanh thu từng sản phẩm bán ra
        /// </summary>
        public List<SoChiTietSanPhamDTO> GetSoChiTietSanPham(string maSP, DateTime tuNgay, DateTime denNgay)
        {
            CheckReadPermission();

            DateTime dtTu = tuNgay.Date;
            DateTime dtDen = denNgay.Date.AddDays(1);

            string sql = @"
                SELECT 
                    sp.MaSP,
                    sp.TenSP,
                    ISNULL(lsp.TenLoai, N'') AS TenLoaiSP,
                    ISNULL(sp.DonViTinh, N'') AS DonViTinh,
                    ISNULL(SUM(ct.SoLuong), 0) AS TongSoLuongBan,
                    ISNULL(SUM(ct.ThanhTien), 0) AS TongDoanhThu,
                    COUNT(DISTINCT h.MaHDB) AS SoHoaDonPhatSinh,
                    CASE 
                        WHEN ISNULL(SUM(ct.SoLuong), 0) > 0 
                        THEN ISNULL(SUM(ct.ThanhTien), 0) / SUM(ct.SoLuong) 
                        ELSE 0 
                    END AS DonGiaTrungBinh
                FROM SANPHAM sp
                LEFT JOIN LOAISANPHAM lsp ON sp.MaLoai = lsp.MaLoai
                INNER JOIN CHITIETHOADONBAN ct ON sp.MaSP = ct.MaSP
                INNER JOIN HOADONBAN h ON ct.MaHDB = h.MaHDB
                WHERE h.NgayLap >= @TuNgay AND h.NgayLap < @DenNgay
                  AND (h.TrangThai IS NULL OR (h.TrangThai <> N'Hủy' AND h.TrangThai <> N'Đã hủy' AND h.TrangThai NOT LIKE N'%Hủy%' AND h.TrangThai NOT LIKE N'%hủy%'))
                  AND (@MaSP IS NULL OR @MaSP = '' OR sp.MaSP = @MaSP)
                GROUP BY sp.MaSP, sp.TenSP, lsp.TenLoai, sp.DonViTinh
                ORDER BY TongDoanhThu DESC, TongSoLuongBan DESC";

            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@TuNgay", SqlDbType.DateTime) { Value = dtTu },
                new SqlParameter("@DenNgay", SqlDbType.DateTime) { Value = dtDen },
                new SqlParameter("@MaSP", SqlDbType.Char, 10) { Value = (object)maSP ?? DBNull.Value }
            };

            DataTable dt = Database.ExecuteQuery(sql, parameters.ToArray());
            List<SoChiTietSanPhamDTO> list = new List<SoChiTietSanPhamDTO>();

            foreach (DataRow row in dt.Rows)
            {
                list.Add(new SoChiTietSanPhamDTO
                {
                    MaSP = row["MaSP"].ToString().Trim(),
                    TenSP = row["TenSP"].ToString().Trim(),
                    TenLoaiSP = row["TenLoaiSP"].ToString().Trim(),
                    DonViTinh = row["DonViTinh"].ToString().Trim(),
                    TongSoLuongBan = Convert.ToInt32(row["TongSoLuongBan"]),
                    TongDoanhThu = Convert.ToDecimal(row["TongDoanhThu"]),
                    DonGiaTrungBinh = Convert.ToDecimal(row["DonGiaTrungBinh"]),
                    SoHoaDonPhatSinh = Convert.ToInt32(row["SoHoaDonPhatSinh"])
                });
            }

            return list;
        }

        /// <summary>
        /// Sổ chi tiết một Hóa đơn kèm chi tiết mặt hàng, phiếu thu và chứng từ kế toán
        /// </summary>
        public SoChiTietHoaDonDTO GetSoChiTietHoaDon(string maHDB)
        {
            CheckReadPermission();

            if (string.IsNullOrWhiteSpace(maHDB)) return null;

            string sqlHDB = @"
                SELECT 
                    h.MaHDB,
                    h.NgayLap,
                    h.MaKH,
                    ISNULL(kh.TenKH, N'Khách vãng lai') AS TenKH,
                    ISNULL(kh.SoDienThoai, N'') AS DienThoai,
                    ISNULL(kh.DiaChi, N'') AS DiaChi,
                    h.MaNV,
                    ISNULL(nv.HoTen, N'') AS TenNV,
                    h.TongTien,
                    ISNULL(h.TrangThai, N'Chưa thanh toán') AS TrangThai,
                    h.GhiChu,
                    ISNULL((SELECT SUM(SoTien) FROM PHIEUTHU WHERE MaHDB = h.MaHDB), 0) AS DaThu
                FROM HOADONBAN h
                LEFT JOIN KHACHHANG kh ON h.MaKH = kh.MaKH
                LEFT JOIN NHANVIEN nv ON h.MaNV = nv.MaNV
                WHERE h.MaHDB = @MaHDB";

            DataTable dtHDB = Database.ExecuteQuery(sqlHDB, new SqlParameter("@MaHDB", SqlDbType.Char, 12) { Value = maHDB });
            if (dtHDB.Rows.Count == 0) return null;

            DataRow rowHDB = dtHDB.Rows[0];
            decimal tongTien = Convert.ToDecimal(rowHDB["TongTien"]);
            decimal daThu = Convert.ToDecimal(rowHDB["DaThu"]);

            SoChiTietHoaDonDTO result = new SoChiTietHoaDonDTO
            {
                MaHDB = rowHDB["MaHDB"].ToString().Trim(),
                NgayLap = Convert.ToDateTime(rowHDB["NgayLap"]),
                MaKH = rowHDB["MaKH"] != DBNull.Value ? rowHDB["MaKH"].ToString().Trim() : "",
                TenKH = rowHDB["TenKH"].ToString().Trim(),
                DienThoai = rowHDB["DienThoai"].ToString().Trim(),
                DiaChi = rowHDB["DiaChi"].ToString().Trim(),
                MaNV = rowHDB["MaNV"] != DBNull.Value ? rowHDB["MaNV"].ToString().Trim() : "",
                TenNV = rowHDB["TenNV"].ToString().Trim(),
                TongTien = tongTien,
                TrangThai = rowHDB["TrangThai"].ToString().Trim(),
                DaThu = daThu,
                ConLai = Math.Max(0, tongTien - daThu),
                GhiChu = rowHDB["GhiChu"] != DBNull.Value ? rowHDB["GhiChu"].ToString().Trim() : ""
            };

            // 1. Chi tiết mặt hàng
            string sqlCT = @"
                SELECT 
                    ct.MaSP,
                    sp.TenSP,
                    ISNULL(sp.DonViTinh, N'') AS DonViTinh,
                    ct.SoLuong,
                    ct.DonGia,
                    ct.GiamGia,
                    ct.ThanhTien
                FROM CHITIETHOADONBAN ct
                INNER JOIN SANPHAM sp ON ct.MaSP = sp.MaSP
                WHERE ct.MaHDB = @MaHDB
                ORDER BY ct.MaSP";

            DataTable dtCT = Database.ExecuteQuery(sqlCT, new SqlParameter("@MaHDB", SqlDbType.Char, 12) { Value = maHDB });
            foreach (DataRow r in dtCT.Rows)
            {
                result.DanhSachMatHang.Add(new ChiTietHoaDonBanItemDTO
                {
                    MaSP = r["MaSP"].ToString().Trim(),
                    TenSP = r["TenSP"].ToString().Trim(),
                    DonViTinh = r["DonViTinh"].ToString().Trim(),
                    SoLuong = Convert.ToInt32(r["SoLuong"]),
                    DonGia = Convert.ToDecimal(r["DonGia"]),
                    GiamGia = Convert.ToDecimal(r["GiamGia"]),
                    ThanhTien = Convert.ToDecimal(r["ThanhTien"])
                });
            }

            // 2. Danh sách Phiếu thu liên kết
            string sqlPT = @"
                SELECT MaPT, NgayThu, NguoiNop, SoTien, HinhThuc, LyDoThu
                FROM PHIEUTHU
                WHERE MaHDB = @MaHDB
                ORDER BY NgayThu ASC, MaPT ASC";

            DataTable dtPT = Database.ExecuteQuery(sqlPT, new SqlParameter("@MaHDB", SqlDbType.Char, 12) { Value = maHDB });
            foreach (DataRow r in dtPT.Rows)
            {
                result.DanhSachPhieuThu.Add(new PhieuThuItemDTO
                {
                    MaPT = r["MaPT"].ToString().Trim(),
                    NgayThu = r["NgayThu"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(r["NgayThu"]) : null,
                    NguoiNop = r["NguoiNop"] != DBNull.Value ? r["NguoiNop"].ToString().Trim() : "",
                    SoTien = Convert.ToDecimal(r["SoTien"]),
                    HinhThuc = r["HinhThuc"] != DBNull.Value ? r["HinhThuc"].ToString().Trim() : "",
                    LyDoThu = r["LyDoThu"] != DBNull.Value ? r["LyDoThu"].ToString().Trim() : ""
                });
            }

            // 3. Danh sách định khoản kế toán (CHUNGTU & CHITIETCHUNGTU)
            string sqlDK = @"
                SELECT 
                    ct.MaCT,
                    ct.NgayCT AS NgayLap,
                    dk.STT,
                    dk.TaiKhoanNo,
                    dk.TaiKhoanCo,
                    dk.SoTien,
                    dk.DienGiai
                FROM CHUNGTU ct
                INNER JOIN CHITIETCHUNGTU dk ON ct.MaCT = dk.MaCT
                WHERE ct.MaHDB = @MaHDB
                ORDER BY ct.MaCT, dk.STT";

            DataTable dtDK = Database.ExecuteQuery(sqlDK, new SqlParameter("@MaHDB", SqlDbType.Char, 12) { Value = maHDB });
            foreach (DataRow r in dtDK.Rows)
            {
                result.DanhSachDinhKhoan.Add(new ChiTietDinhKhoanItemDTO
                {
                    MaCT = r["MaCT"].ToString().Trim(),
                    NgayLap = r["NgayLap"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(r["NgayLap"]) : null,
                    STT = Convert.ToInt32(r["STT"]),
                    TaiKhoanNo = r["TaiKhoanNo"].ToString().Trim(),
                    TaiKhoanCo = r["TaiKhoanCo"].ToString().Trim(),
                    SoTien = Convert.ToDecimal(r["SoTien"]),
                    DienGiai = r["DienGiai"] != DBNull.Value ? r["DienGiai"].ToString().Trim() : ""
                });
            }

            return result;
        }

        /// <summary>
        /// Tổng hợp các chỉ số KPI tài chính - bán hàng - kho theo kỳ
        /// </summary>
        public TongHopKpiDTO GetTongHopKpi(DateTime tuNgay, DateTime denNgay)
        {
            CheckReadPermission();

            DateTime dtTu = tuNgay.Date;
            DateTime dtDen = denNgay.Date.AddDays(1);

            TongHopKpiDTO kpi = new TongHopKpiDTO();

            // 1. Doanh thu & số hóa đơn (loại bỏ hóa đơn hủy)
            string sqlHDB = @"
                SELECT 
                    ISNULL(SUM(TongTien), 0) AS TongDoanhThu,
                    COUNT(*) AS SoHoaDon
                FROM HOADONBAN
                WHERE NgayLap >= @TuNgay AND NgayLap < @DenNgay
                  AND (TrangThai IS NULL OR (TrangThai <> N'Hủy' AND TrangThai <> N'Đã hủy' AND TrangThai NOT LIKE N'%Hủy%' AND TrangThai NOT LIKE N'%hủy%'))";

            DataTable dtHDB = Database.ExecuteQuery(sqlHDB,
                new SqlParameter("@TuNgay", SqlDbType.DateTime) { Value = dtTu },
                new SqlParameter("@DenNgay", SqlDbType.DateTime) { Value = dtDen });

            if (dtHDB.Rows.Count > 0)
            {
                kpi.TongDoanhThu = Convert.ToDecimal(dtHDB.Rows[0]["TongDoanhThu"]);
                kpi.SoHoaDon = Convert.ToInt32(dtHDB.Rows[0]["SoHoaDon"]);
            }

            // 2. Tổng thu
            string sqlThu = @"
                SELECT ISNULL(SUM(SoTien), 0) AS TongThu
                FROM PHIEUTHU
                WHERE NgayThu >= @TuNgay AND NgayThu < @DenNgay";

            object objThu = Database.ExecuteScalar(sqlThu,
                new SqlParameter("@TuNgay", SqlDbType.DateTime) { Value = dtTu },
                new SqlParameter("@DenNgay", SqlDbType.DateTime) { Value = dtDen });

            kpi.TongThu = objThu != null && objThu != DBNull.Value ? Convert.ToDecimal(objThu) : 0;

            // 3. Tổng chi
            string sqlChi = @"
                SELECT ISNULL(SUM(SoTien), 0) AS TongChi
                FROM PHIEUCHI
                WHERE NgayChi >= @TuNgay AND NgayChi < @DenNgay";

            object objChi = Database.ExecuteScalar(sqlChi,
                new SqlParameter("@TuNgay", SqlDbType.DateTime) { Value = dtTu },
                new SqlParameter("@DenNgay", SqlDbType.DateTime) { Value = dtDen });

            kpi.TongChi = objChi != null && objChi != DBNull.Value ? Convert.ToDecimal(objChi) : 0;

            kpi.ChenhLechThuChi = kpi.TongThu - kpi.TongChi;

            // 4. Tồn kho & giá trị tồn hiện tại (xử lý an toàn trường hợp nullable)
            string sqlTon = @"
                SELECT 
                    ISNULL(SUM(tk.SoLuongTon), 0) AS TongTon,
                    ISNULL(SUM(ISNULL(tk.SoLuongTon, 0) * ISNULL(sp.DonGiaBan, 0)), 0) AS TongGiaTri
                FROM TONKHO tk
                INNER JOIN SANPHAM sp ON tk.MaSP = sp.MaSP";

            DataTable dtTon = Database.ExecuteQuery(sqlTon);
            if (dtTon.Rows.Count > 0)
            {
                kpi.TongSoLuongTon = Convert.ToInt32(dtTon.Rows[0]["TongTon"]);
                kpi.TongGiaTriTonKho = Convert.ToDecimal(dtTon.Rows[0]["TongGiaTri"]);
            }

            return kpi;
        }

        #region Báo cáo Tuổi nợ Khách hàng

        /// <summary>
        /// Báo cáo Tuổi nợ Khách hàng - Tổng hợp theo Khách hàng tại thời điểm ngày chốt
        /// </summary>
        /// <param name="ngayChot">Thời điểm tính tuổi nợ (mặc định DateTime.Today)</param>
        /// <param name="maKH">Lọc theo mã khách hàng (tùy chọn)</param>
        /// <param name="nhomTuoiNoFilter">0: Tất cả, 1: Có nợ quá hạn (>30 ngày), 2: Chỉ nợ khó đòi (>90 ngày)</param>
        public List<BaoCaoTuoiNoTongHopDTO> GetBaoCaoTuoiNoTongHop(DateTime ngayChot, string maKH = null, int? nhomTuoiNoFilter = null)
        {
            CheckReadPermission();

            DateTime dtChot = ngayChot.Date;
            DateTime dtChotKetThuc = dtChot.AddDays(1);

            string havingClause = "";
            if (nhomTuoiNoFilter.HasValue)
            {
                if (nhomTuoiNoFilter.Value == 1)
                {
                    havingClause = "HAVING SUM(CASE WHEN d.SoNgayNo > 30 THEN d.ConNo ELSE 0 END) > 0";
                }
                else if (nhomTuoiNoFilter.Value == 2)
                {
                    havingClause = "HAVING SUM(CASE WHEN d.SoNgayNo > 90 THEN d.ConNo ELSE 0 END) > 0";
                }
            }

            string sql = string.Format(@"
                WITH DebtInvoices AS (
                    SELECT 
                        h.MaHDB,
                        h.NgayLap,
                        h.MaKH,
                        ISNULL(kh.TenKH, N'Khách vãng lai') AS TenKH,
                        ISNULL(kh.SoDienThoai, N'') AS DienThoai,
                        h.TongTien,
                        ISNULL((
                            SELECT SUM(pt.SoTien) 
                            FROM PHIEUTHU pt 
                            WHERE pt.MaHDB = h.MaHDB 
                              AND pt.NgayThu < @NgayChotKetThuc
                        ), 0) AS DaThu,
                        (
                            h.TongTien - ISNULL((
                                SELECT SUM(pt.SoTien) 
                                FROM PHIEUTHU pt 
                                WHERE pt.MaHDB = h.MaHDB 
                                  AND pt.NgayThu < @NgayChotKetThuc
                            ), 0)
                        ) AS ConNo,
                        CASE 
                            WHEN DATEDIFF(day, h.NgayLap, @NgayChot) < 0 THEN 0 
                            ELSE DATEDIFF(day, h.NgayLap, @NgayChot) 
                        END AS SoNgayNo
                    FROM HOADONBAN h
                    LEFT JOIN KHACHHANG kh ON h.MaKH = kh.MaKH
                    WHERE h.NgayLap < @NgayChotKetThuc
                      AND (h.TrangThai IS NULL OR (h.TrangThai <> N'Hủy' AND h.TrangThai <> N'Đã hủy' AND h.TrangThai NOT LIKE N'%Hủy%' AND h.TrangThai NOT LIKE N'%hủy%'))
                      AND (@MaKH IS NULL OR @MaKH = '' OR h.MaKH = @MaKH)
                )
                SELECT 
                    d.MaKH,
                    d.TenKH,
                    d.DienThoai,
                    SUM(d.ConNo) AS TongNo,
                    SUM(CASE WHEN d.SoNgayNo <= 30 THEN d.ConNo ELSE 0 END) AS TrongHan_0_30,
                    SUM(CASE WHEN d.SoNgayNo BETWEEN 31 AND 60 THEN d.ConNo ELSE 0 END) AS QuaHanNhe_31_60,
                    SUM(CASE WHEN d.SoNgayNo BETWEEN 61 AND 90 THEN d.ConNo ELSE 0 END) AS QuaHanTB_61_90,
                    SUM(CASE WHEN d.SoNgayNo > 90 THEN d.ConNo ELSE 0 END) AS KhoDoi_Tren90,
                    COUNT(d.MaHDB) AS SoHoaDonNo,
                    CASE 
                        WHEN SUM(CASE WHEN d.SoNgayNo > 90 THEN d.ConNo ELSE 0 END) > 0 THEN 3
                        WHEN SUM(CASE WHEN d.SoNgayNo BETWEEN 61 AND 90 THEN d.ConNo ELSE 0 END) > 0 THEN 2
                        WHEN SUM(CASE WHEN d.SoNgayNo BETWEEN 31 AND 60 THEN d.ConNo ELSE 0 END) > 0 THEN 1
                        ELSE 0
                    END AS MucDoRuiRo
                FROM DebtInvoices d
                WHERE d.ConNo > 0
                GROUP BY d.MaKH, d.TenKH, d.DienThoai
                {0}
                ORDER BY MucDoRuiRo DESC, TongNo DESC", havingClause);

            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@NgayChot", SqlDbType.DateTime) { Value = dtChot },
                new SqlParameter("@NgayChotKetThuc", SqlDbType.DateTime) { Value = dtChotKetThuc },
                new SqlParameter("@MaKH", SqlDbType.Char, 10) { Value = (object)maKH ?? DBNull.Value }
            };

            DataTable dt = Database.ExecuteQuery(sql, parameters.ToArray());
            List<BaoCaoTuoiNoTongHopDTO> list = new List<BaoCaoTuoiNoTongHopDTO>();

            foreach (DataRow row in dt.Rows)
            {
                list.Add(new BaoCaoTuoiNoTongHopDTO
                {
                    MaKH = row["MaKH"] != DBNull.Value ? row["MaKH"].ToString().Trim() : "",
                    TenKH = row["TenKH"].ToString().Trim(),
                    DienThoai = row["DienThoai"].ToString().Trim(),
                    TongNo = Convert.ToDecimal(row["TongNo"]),
                    TrongHan_0_30 = Convert.ToDecimal(row["TrongHan_0_30"]),
                    QuaHanNhe_31_60 = Convert.ToDecimal(row["QuaHanNhe_31_60"]),
                    QuaHanTB_61_90 = Convert.ToDecimal(row["QuaHanTB_61_90"]),
                    KhoDoi_Tren90 = Convert.ToDecimal(row["KhoDoi_Tren90"]),
                    SoHoaDonNo = Convert.ToInt32(row["SoHoaDonNo"]),
                    MucDoRuiRo = Convert.ToInt32(row["MucDoRuiRo"])
                });
            }

            return list;
        }

        /// <summary>
        /// Báo cáo Tuổi nợ Khách hàng - Chi tiết theo từng Hóa đơn bán còn nợ
        /// </summary>
        public List<BaoCaoTuoiNoChiTietDTO> GetBaoCaoTuoiNoChiTiet(DateTime ngayChot, string maKH = null, int? nhomTuoiNoFilter = null)
        {
            CheckReadPermission();

            DateTime dtChot = ngayChot.Date;
            DateTime dtChotKetThuc = dtChot.AddDays(1);

            string filterClause = "";
            if (nhomTuoiNoFilter.HasValue)
            {
                if (nhomTuoiNoFilter.Value == 1)
                {
                    filterClause = "AND d.SoNgayNo > 30";
                }
                else if (nhomTuoiNoFilter.Value == 2)
                {
                    filterClause = "AND d.SoNgayNo > 90";
                }
                else if (nhomTuoiNoFilter.Value == 3) // Trong hạn <= 30
                {
                    filterClause = "AND d.SoNgayNo <= 30";
                }
            }

            string sql = string.Format(@"
                WITH DebtInvoices AS (
                    SELECT 
                        h.MaHDB,
                        h.NgayLap,
                        h.MaKH,
                        ISNULL(kh.TenKH, N'Khách vãng lai') AS TenKH,
                        ISNULL(kh.SoDienThoai, N'') AS DienThoai,
                        h.TongTien,
                        ISNULL((
                            SELECT SUM(pt.SoTien) 
                            FROM PHIEUTHU pt 
                            WHERE pt.MaHDB = h.MaHDB 
                              AND pt.NgayThu < @NgayChotKetThuc
                        ), 0) AS DaThu,
                        (
                            h.TongTien - ISNULL((
                                SELECT SUM(pt.SoTien) 
                                FROM PHIEUTHU pt 
                                WHERE pt.MaHDB = h.MaHDB 
                                  AND pt.NgayThu < @NgayChotKetThuc
                            ), 0)
                        ) AS ConNo,
                        CASE 
                            WHEN DATEDIFF(day, h.NgayLap, @NgayChot) < 0 THEN 0 
                            ELSE DATEDIFF(day, h.NgayLap, @NgayChot) 
                        END AS SoNgayNo
                    FROM HOADONBAN h
                    LEFT JOIN KHACHHANG kh ON h.MaKH = kh.MaKH
                    WHERE h.NgayLap < @NgayChotKetThuc
                      AND (h.TrangThai IS NULL OR (h.TrangThai <> N'Hủy' AND h.TrangThai <> N'Đã hủy' AND h.TrangThai NOT LIKE N'%Hủy%' AND h.TrangThai NOT LIKE N'%hủy%'))
                      AND (@MaKH IS NULL OR @MaKH = '' OR h.MaKH = @MaKH)
                )
                SELECT 
                    d.MaHDB,
                    d.NgayLap,
                    d.MaKH,
                    d.TenKH,
                    d.DienThoai,
                    d.TongTien,
                    d.DaThu,
                    d.ConNo,
                    d.SoNgayNo,
                    CASE 
                        WHEN d.SoNgayNo > 90 THEN 3
                        WHEN d.SoNgayNo BETWEEN 61 AND 90 THEN 2
                        WHEN d.SoNgayNo BETWEEN 31 AND 60 THEN 1
                        ELSE 0
                    END AS MaNhomTuoiNo
                FROM DebtInvoices d
                WHERE d.ConNo > 0
                {0}
                ORDER BY MaNhomTuoiNo DESC, d.SoNgayNo DESC, d.NgayLap ASC", filterClause);

            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@NgayChot", SqlDbType.DateTime) { Value = dtChot },
                new SqlParameter("@NgayChotKetThuc", SqlDbType.DateTime) { Value = dtChotKetThuc },
                new SqlParameter("@MaKH", SqlDbType.Char, 10) { Value = (object)maKH ?? DBNull.Value }
            };

            DataTable dt = Database.ExecuteQuery(sql, parameters.ToArray());
            List<BaoCaoTuoiNoChiTietDTO> list = new List<BaoCaoTuoiNoChiTietDTO>();

            foreach (DataRow row in dt.Rows)
            {
                list.Add(new BaoCaoTuoiNoChiTietDTO
                {
                    MaHDB = row["MaHDB"].ToString().Trim(),
                    NgayLap = Convert.ToDateTime(row["NgayLap"]),
                    MaKH = row["MaKH"] != DBNull.Value ? row["MaKH"].ToString().Trim() : "",
                    TenKH = row["TenKH"].ToString().Trim(),
                    DienThoai = row["DienThoai"].ToString().Trim(),
                    TongTien = Convert.ToDecimal(row["TongTien"]),
                    DaThu = Convert.ToDecimal(row["DaThu"]),
                    ConNo = Convert.ToDecimal(row["ConNo"]),
                    SoNgayNo = Convert.ToInt32(row["SoNgayNo"]),
                    MaNhomTuoiNo = Convert.ToInt32(row["MaNhomTuoiNo"])
                });
            }

            return list;
        }

        /// <summary>
        /// Tra cứu nhanh thông tin phân loại nợ của một khách hàng phục vụ kiểm soát bán hàng và chặn lập đơn
        /// </summary>
        public ThongTinCongNoKhachHangDTO GetThongTinCongNoKhachHang(string maKH, DateTime? ngayChot = null)
        {
            if (string.IsNullOrWhiteSpace(maKH))
            {
                return new ThongTinCongNoKhachHangDTO();
            }

            DateTime dtChot = ngayChot.HasValue ? ngayChot.Value.Date : DateTime.Today;
            DateTime dtChotKetThuc = dtChot.AddDays(1);

            string sql = @"
                WITH DebtInvoices AS (
                    SELECT 
                        h.MaHDB,
                        h.NgayLap,
                        h.MaKH,
                        ISNULL(kh.TenKH, N'') AS TenKH,
                        (
                            h.TongTien - ISNULL((
                                SELECT SUM(pt.SoTien) 
                                FROM PHIEUTHU pt 
                                WHERE pt.MaHDB = h.MaHDB 
                                  AND pt.NgayThu < @NgayChotKetThuc
                            ), 0)
                        ) AS ConNo,
                        CASE 
                            WHEN DATEDIFF(day, h.NgayLap, @NgayChot) < 0 THEN 0 
                            ELSE DATEDIFF(day, h.NgayLap, @NgayChot) 
                        END AS SoNgayNo
                    FROM HOADONBAN h
                    LEFT JOIN KHACHHANG kh ON h.MaKH = kh.MaKH
                    WHERE h.MaKH = @MaKH
                      AND h.NgayLap < @NgayChotKetThuc
                      AND (h.TrangThai IS NULL OR (h.TrangThai <> N'Hủy' AND h.TrangThai <> N'Đã hủy' AND h.TrangThai NOT LIKE N'%Hủy%' AND h.TrangThai NOT LIKE N'%hủy%'))
                )
                SELECT 
                    @MaKH AS MaKH,
                    ISNULL((SELECT TOP 1 TenKH FROM KHACHHANG WHERE MaKH = @MaKH), N'') AS TenKH,
                    ISNULL(SUM(d.ConNo), 0) AS TongNo,
                    ISNULL(SUM(CASE WHEN d.SoNgayNo <= 30 THEN d.ConNo ELSE 0 END), 0) AS TrongHan_0_30,
                    ISNULL(SUM(CASE WHEN d.SoNgayNo BETWEEN 31 AND 60 THEN d.ConNo ELSE 0 END), 0) AS QuaHanNhe_31_60,
                    ISNULL(SUM(CASE WHEN d.SoNgayNo BETWEEN 61 AND 90 THEN d.ConNo ELSE 0 END), 0) AS QuaHanTB_61_90,
                    ISNULL(SUM(CASE WHEN d.SoNgayNo > 90 THEN d.ConNo ELSE 0 END), 0) AS KhoDoi_Tren90,
                    ISNULL(COUNT(CASE WHEN d.ConNo > 0 THEN d.MaHDB END), 0) AS SoHoaDonNo
                FROM DebtInvoices d
                WHERE d.ConNo > 0";

            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@MaKH", SqlDbType.Char, 10) { Value = maKH.Trim() },
                new SqlParameter("@NgayChot", SqlDbType.DateTime) { Value = dtChot },
                new SqlParameter("@NgayChotKetThuc", SqlDbType.DateTime) { Value = dtChotKetThuc }
            };

            DataTable dt = Database.ExecuteQuery(sql, parameters.ToArray());
            if (dt.Rows.Count > 0)
            {
                DataRow r = dt.Rows[0];
                return new ThongTinCongNoKhachHangDTO
                {
                    MaKH = maKH.Trim(),
                    TenKH = r["TenKH"].ToString().Trim(),
                    TongNo = Convert.ToDecimal(r["TongNo"]),
                    TrongHan_0_30 = Convert.ToDecimal(r["TrongHan_0_30"]),
                    QuaHanNhe_31_60 = Convert.ToDecimal(r["QuaHanNhe_31_60"]),
                    QuaHanTB_61_90 = Convert.ToDecimal(r["QuaHanTB_61_90"]),
                    KhoDoi_Tren90 = Convert.ToDecimal(r["KhoDoi_Tren90"]),
                    SoHoaDonNo = Convert.ToInt32(r["SoHoaDonNo"])
                };
            }

            return new ThongTinCongNoKhachHangDTO { MaKH = maKH.Trim() };
        }

        /// <summary>
        /// Thống kê cơ cấu doanh thu theo loại sản phẩm phục vụ vẽ biểu đồ Doughnut
        /// </summary>
        public List<DoanhThuTheoLoaiSPDTO> GetCoCauDoanhThuTheoLoaiSP(DateTime tuNgay, DateTime denNgay)
        {
            CheckReadPermission();

            DateTime dtTu = tuNgay.Date;
            DateTime dtDen = denNgay.Date.AddDays(1);

            string sql = @"
                SELECT 
                    lsp.MaLoai,
                    ISNULL(lsp.TenLoai, N'Chưa phân loại') AS TenLoai,
                    ISNULL(SUM(ct.ThanhTien), 0) AS TongDoanhThu,
                    ISNULL(SUM(ct.SoLuong), 0) AS TongSoLuongBan
                FROM CHITIETHOADONBAN ct
                INNER JOIN HOADONBAN h ON ct.MaHDB = h.MaHDB
                INNER JOIN SANPHAM sp ON ct.MaSP = sp.MaSP
                INNER JOIN LOAISANPHAM lsp ON sp.MaLoai = lsp.MaLoai
                WHERE h.NgayLap >= @TuNgay AND h.NgayLap < @DenNgay
                  AND (h.TrangThai IS NULL OR (h.TrangThai NOT LIKE N'%Hủy%' AND h.TrangThai NOT LIKE N'%hủy%'))
                GROUP BY lsp.MaLoai, lsp.TenLoai
                ORDER BY TongDoanhThu DESC";

            DataTable dt = Database.ExecuteQuery(sql,
                new SqlParameter("@TuNgay", SqlDbType.DateTime) { Value = dtTu },
                new SqlParameter("@DenNgay", SqlDbType.DateTime) { Value = dtDen });

            List<DoanhThuTheoLoaiSPDTO> list = new List<DoanhThuTheoLoaiSPDTO>();
            decimal tongDoanhThuTatCa = 0;

            foreach (DataRow row in dt.Rows)
            {
                decimal dtVal = Convert.ToDecimal(row["TongDoanhThu"]);
                tongDoanhThuTatCa += dtVal;
                list.Add(new DoanhThuTheoLoaiSPDTO
                {
                    MaLoaiSP = row["MaLoai"].ToString().Trim(),
                    TenLoaiSP = row["TenLoai"].ToString().Trim(),
                    TongDoanhThu = dtVal,
                    TongSoLuongBan = Convert.ToInt32(row["TongSoLuongBan"])
                });
            }

            if (tongDoanhThuTatCa > 0)
            {
                foreach (var item in list)
                {
                    item.TyLePhanTram = Math.Round((double)(item.TongDoanhThu / tongDoanhThuTatCa * 100), 1);
                }
            }

            return list;
        }

        #endregion
    }
}
