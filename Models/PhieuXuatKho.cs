using System;
using DNQH_KeToanBanHang.Constants;

namespace DNQH_KeToanBanHang.Models
{
    public class PhieuXuatKho
    {
        public string MaPXK { get; set; }
        public string MaNV { get; set; }
        public string MaHDB { get; set; }
        public string MaKho { get; set; }
        public DateTime? NgayXuat { get; set; }
        public string LyDoXuat { get; set; }
        public string TrangThai { get; set; }

        // Các thuộc tính bổ sung hiển thị giao diện
        public string TenNV { get; set; }
        public string TenKho { get; set; }
        public string TenKH { get; set; }
        public decimal TongTienHDB { get; set; }

        public PhieuXuatKho()
        {
            NgayXuat = DateTime.Now;
            TrangThai = EntityStatusConstants.Export.DaXuat;
        }
    }
}
