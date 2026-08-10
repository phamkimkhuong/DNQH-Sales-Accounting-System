using System;

namespace DNQH_KeToanBanHang.Models
{
    public class PhieuChi
    {
        public string MaPC { get; set; }
        public string MaNV { get; set; }
        public DateTime? NgayChi { get; set; }
        public string NguoiNhan { get; set; }
        public string LyDoChi { get; set; }
        public decimal SoTien { get; set; }
        public string HinhThuc { get; set; }
        public string GhiChu { get; set; }

        // Thuộc tính mở rộng phục vụ hiển thị
        public string TenNV { get; set; }

        public PhieuChi()
        {
            NgayChi = DateTime.Now;
            HinhThuc = "Tiền mặt";
            SoTien = 0;
        }
    }
}
