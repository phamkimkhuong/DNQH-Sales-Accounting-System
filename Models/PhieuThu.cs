using System;

namespace DNQH_KeToanBanHang.Models
{
    public class PhieuThu
    {
        public string MaPT { get; set; }
        public string MaNV { get; set; }
        public string MaHDB { get; set; }
        public DateTime? NgayThu { get; set; }
        public string NguoiNop { get; set; }
        public string LyDoThu { get; set; }
        public decimal SoTien { get; set; }
        public string HinhThuc { get; set; }
        public string GhiChu { get; set; }

        // Các thuộc tính mở rộng phục vụ hiển thị
        public string TenNV { get; set; }
        public string TenKH { get; set; }
        public decimal TongTienHDB { get; set; }
        public decimal DaThu { get; set; }
        public decimal ConLai { get; set; }

        public PhieuThu()
        {
            NgayThu = DateTime.Now;
            HinhThuc = "Tiền mặt";
            SoTien = 0;
        }
    }
}
