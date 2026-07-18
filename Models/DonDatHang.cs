using System;
using DNQH_KeToanBanHang.Constants;

namespace DNQH_KeToanBanHang.Models
{
    public class DonDatHang
    {
        public string MaDDH { get; set; }
        public string MaNV { get; set; }
        public string MaKH { get; set; }
        public DateTime NgayDat { get; set; }
        public DateTime? NgayGiaoDuKien { get; set; }
        public decimal TongTien { get; set; }
        public string TrangThai { get; set; }
        public string GhiChu { get; set; }
        public int Version { get; set; }

        // Navigation / Display Helpers
        public string TenNV { get; set; }
        public string TenKH { get; set; }

        public DonDatHang()
        {
            NgayDat = DateTime.Now;
            TongTien = 0;
            TrangThai = OrderStatusConstants.AliasChoDuyet;
            Version = 1;
        }
    }
}
