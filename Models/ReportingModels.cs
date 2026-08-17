using System;
using System.Collections.Generic;

namespace DNQH_KeToanBanHang.Models
{
    /// <summary>
    /// DTO đại diện cho dòng dữ liệu trong Báo cáo Doanh thu bán hàng
    /// </summary>
    public class BaoCaoDoanhThuDTO
    {
        public string MaHDB { get; set; }
        public DateTime NgayLap { get; set; }
        public string MaKH { get; set; }
        public string TenKH { get; set; }
        public string MaNV { get; set; }
        public string TenNV { get; set; }
        public decimal TongTien { get; set; }
        public string TrangThai { get; set; }
        public string GhiChu { get; set; }
        public int SoMatHang { get; set; }
    }

    /// <summary>
    /// DTO đại diện cho dòng giao dịch trong Báo cáo Tổng hợp Thu - Chi (Sổ quỹ tiền mặt/ngân hàng)
    /// </summary>
    public class BaoCaoThuChiDTO
    {
        public DateTime NgayGiaoDich { get; set; }
        public string LoaiGiaoDich { get; set; } // "Thu" hoặc "Chi"
        public string MaChungTu { get; set; }    // MaPT hoặc MaPC
        public string NguoiGiaoDich { get; set; } // NguoiNop hoặc NguoiNhan
        public decimal SoTienThu { get; set; }
        public decimal SoTienChi { get; set; }
        public string HinhThuc { get; set; }
        public string LyDo { get; set; }
        public string MaNV { get; set; }
        public string TenNV { get; set; }
        public string MaHDBLienKet { get; set; }
    }

    /// <summary>
    /// DTO đại diện cho dòng dữ liệu trong Báo cáo Tổng hợp Tồn kho
    /// </summary>
    public class BaoCaoTonKhoDTO
    {
        public string MaKho { get; set; }
        public string TenKho { get; set; }
        public string MaSP { get; set; }
        public string TenSP { get; set; }
        public string TenLoaiSP { get; set; }
        public string DonViTinh { get; set; }
        public int SoLuongTon { get; set; }
        public decimal DonGia { get; set; }
        public decimal GiaTriTon { get; set; }
        public DateTime? NgayCapNhat { get; set; }
    }

    /// <summary>
    /// DTO dòng lịch sử công nợ và thanh toán trong Sổ Chi Tiết Khách Hàng
    /// </summary>
    public class SoChiTietKhachHangDTO
    {
        public DateTime NgayGiaoDich { get; set; }
        public string LoaiNghiepVu { get; set; } // "Hóa đơn bán" hoặc "Thu tiền"
        public string SoChungTu { get; set; }    // MaHDB hoặc MaPT
        public string DienGiai { get; set; }
        public decimal PhatSinhNo { get; set; }   // Số tiền mua hàng phải trả
        public decimal PhatSinhCo { get; set; }   // Số tiền khách hàng đã thanh toán
        public decimal SoDuCuoiKy { get; set; }   // Công nợ còn lại
    }

    /// <summary>
    /// DTO dòng phân tích doanh số và số lượng trong Sổ Chi Tiết Sản Phẩm
    /// </summary>
    public class SoChiTietSanPhamDTO
    {
        public string MaSP { get; set; }
        public string TenSP { get; set; }
        public string TenLoaiSP { get; set; }
        public string DonViTinh { get; set; }
        public int TongSoLuongBan { get; set; }
        public decimal TongDoanhThu { get; set; }
        public decimal DonGiaTrungBinh { get; set; }
        public int SoHoaDonPhatSinh { get; set; }
    }

    /// <summary>
    /// DTO thông tin tổng hợp cho Sổ Chi Tiết Hóa Đơn & Chứng Từ
    /// </summary>
    public class SoChiTietHoaDonDTO
    {
        public string MaHDB { get; set; }
        public DateTime NgayLap { get; set; }
        public string MaKH { get; set; }
        public string TenKH { get; set; }
        public string DienThoai { get; set; }
        public string DiaChi { get; set; }
        public string MaNV { get; set; }
        public string TenNV { get; set; }
        public decimal TongTien { get; set; }
        public string TrangThai { get; set; }
        public decimal DaThu { get; set; }
        public decimal ConLai { get; set; }
        public string GhiChu { get; set; }
        public List<ChiTietHoaDonBanItemDTO> DanhSachMatHang { get; set; }
        public List<PhieuThuItemDTO> DanhSachPhieuThu { get; set; }
        public List<ChiTietDinhKhoanItemDTO> DanhSachDinhKhoan { get; set; }

        public SoChiTietHoaDonDTO()
        {
            DanhSachMatHang = new List<ChiTietHoaDonBanItemDTO>();
            DanhSachPhieuThu = new List<PhieuThuItemDTO>();
            DanhSachDinhKhoan = new List<ChiTietDinhKhoanItemDTO>();
        }
    }

    public class ChiTietHoaDonBanItemDTO
    {
        public string MaSP { get; set; }
        public string TenSP { get; set; }
        public string DonViTinh { get; set; }
        public int SoLuong { get; set; }
        public decimal DonGia { get; set; }
        public decimal GiamGia { get; set; }
        public decimal ThanhTien { get; set; }
    }

    public class PhieuThuItemDTO
    {
        public string MaPT { get; set; }
        public DateTime? NgayThu { get; set; }
        public string NguoiNop { get; set; }
        public decimal SoTien { get; set; }
        public string HinhThuc { get; set; }
        public string LyDoThu { get; set; }
    }

    public class ChiTietDinhKhoanItemDTO
    {
        public string MaCT { get; set; }
        public DateTime? NgayLap { get; set; }
        public int STT { get; set; }
        public string TaiKhoanNo { get; set; }
        public string TaiKhoanCo { get; set; }
        public decimal SoTien { get; set; }
        public string DienGiai { get; set; }
    }

    /// <summary>
    /// DTO tóm tắt các chỉ số tài chính - kế toán tổng quan
    /// </summary>
    public class TongHopKpiDTO
    {
        public decimal TongDoanhThu { get; set; }
        public int SoHoaDon { get; set; }
        public decimal TongThu { get; set; }
        public decimal TongChi { get; set; }
        public decimal ChenhLechThuChi { get; set; }
        public decimal TongSoLuongTon { get; set; }
        public decimal TongGiaTriTonKho { get; set; }
    }

    /// <summary>
    /// DTO thống kê cơ cấu doanh thu theo loại sản phẩm phục vụ vẽ biểu đồ tròn/donut
    /// </summary>
    public class DoanhThuTheoLoaiSPDTO
    {
        public string MaLoaiSP { get; set; }
        public string TenLoaiSP { get; set; }
        public decimal TongDoanhThu { get; set; }
        public int TongSoLuongBan { get; set; }
        public double TyLePhanTram { get; set; }
    }

    /// <summary>
    /// DTO thống kê doanh thu gom nhóm theo thời gian (ngày) phục vụ vẽ biểu đồ cột
    /// </summary>
    public class DoanhThuTheoNgayDTO
    {
        public DateTime Ngay { get; set; }
        public string NhanNgay { get; set; }
        public decimal DoanhThu { get; set; }
        public int SoHoaDon { get; set; }
    }

    /// <summary>
    /// DTO cân đối dòng tiền Thu - Chi theo thời gian phục vụ vẽ biểu đồ đường kép Spline
    /// </summary>
    public class ThuChiTheoNgayDTO
    {
        public DateTime Ngay { get; set; }
        public string NhanNgay { get; set; }
        public decimal TongThu { get; set; }
        public decimal TongChi { get; set; }
        public decimal ChenhLech { get; set; }
    }
}
