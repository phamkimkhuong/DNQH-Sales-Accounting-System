using System;
using DNQH_KeToanBanHang.Constants;

namespace DNQH_KeToanBanHang.Models
{
    public class ChungTu
    {
        public string MaCT { get; set; }
        public string MaNV { get; set; }
        public string MaHDB { get; set; }
        public DateTime? NgayCT { get; set; }
        public string LoaiCT { get; set; }
        public string DienGiai { get; set; }

        // Thuộc tính mở rộng phục vụ hiển thị
        public string TenNV { get; set; }
        public string TenKH { get; set; }
        public decimal TongTienHachToan { get; set; }

        public ChungTu()
        {
            NgayCT = DateTime.Now;
            LoaiCT = DocumentTypeConstants.ChungTuBanHang;
        }
    }
}
