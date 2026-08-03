using System;

namespace DNQH_KeToanBanHang.Models
{
    public class ChiTietPhieuXuatKho
    {
        public string MaPXK { get; set; }
        public string MaSP { get; set; }
        public int SoLuongXuat { get; set; }

        // Các thuộc tính hỗ trợ hiển thị giao diện
        public string TenSP { get; set; }
        public string DonViTinh { get; set; }
        public decimal DonGiaBan { get; set; }
        public int SoLuongHoaDon { get; set; }
        public int SoLuongDaXuat { get; set; }
        public int SoLuongConLai { get; set; }
        public int TonKhoHienTai { get; set; }

        public ChiTietPhieuXuatKho()
        {
            SoLuongXuat = 0;
        }
    }
}
