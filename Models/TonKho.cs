using System;

namespace DNQH_KeToanBanHang.Models
{
    public class TonKho
    {
        public string MaKho { get; set; }
        public string MaSP { get; set; }
        public int SoLuongTon { get; set; }
        public DateTime? NgayCapNhat { get; set; }

        // Navigation / Display Helpers
        public string TenKho { get; set; }
        public string TenSP { get; set; }
        public string DonViTinh { get; set; }
        public decimal? DonGiaBan { get; set; }

        public TonKho()
        {
            SoLuongTon = 0;
            NgayCapNhat = DateTime.Now;
        }
    }
}
