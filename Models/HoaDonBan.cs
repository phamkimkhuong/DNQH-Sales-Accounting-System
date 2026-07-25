using System;
using DNQH_KeToanBanHang.Constants;

namespace DNQH_KeToanBanHang.Models
{
    public class HoaDonBan
    {
        public string MaHDB { get; set; }
        public string MaNV { get; set; }
        public string MaDDH { get; set; }
        public string MaKH { get; set; }
        public DateTime NgayLap { get; set; }
        public decimal TongTien { get; set; }
        public string GhiChu { get; set; }
        public string TrangThai { get; set; }

        // Navigation / Display Helpers
        public string TenNV { get; set; }
        public string TenKH { get; set; }

        public HoaDonBan()
        {
            NgayLap = DateTime.Now;
            TongTien = 0;
            TrangThai = InvoiceStatusConstants.ChuaThanhToan;
        }
    }
}
