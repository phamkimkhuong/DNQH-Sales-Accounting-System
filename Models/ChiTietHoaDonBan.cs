using System;

namespace DNQH_KeToanBanHang.Models
{
    public class ChiTietHoaDonBan
    {
        public string MaHDB { get; set; }
        public string MaSP { get; set; }
        public int SoLuong { get; set; }
        public decimal DonGia { get; set; }
        public decimal GiamGia { get; set; }
        public decimal ThanhTien { get; set; }

        // Navigation / Display Helpers
        public string TenSP { get; set; }
        public string DonViTinh { get; set; }

        public ChiTietHoaDonBan()
        {
            SoLuong = 1;
            DonGia = 0;
            GiamGia = 0;
            ThanhTien = 0;
        }

        public void TinhThanhTien()
        {
            decimal rate = 1m - (GiamGia / 100m);
            if (rate < 0) rate = 0;
            ThanhTien = Math.Round(SoLuong * DonGia * rate, 2);
        }
    }
}
