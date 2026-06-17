using System;

namespace DNQH_KeToanBanHang.Models
{
    public class KhachHang
    {
        public string MaKH { get; set; }
        public string TenKH { get; set; }
        public string SoDienThoai { get; set; }
        public string DiaChi { get; set; }
        public string Email { get; set; }
        public int Version { get; set; }

        public KhachHang()
        {
            Version = 1;
        }

        public KhachHang(string maKH, string tenKH, string soDienThoai, string diaChi, string email, int version = 1)
        {
            MaKH = maKH;
            TenKH = tenKH;
            SoDienThoai = soDienThoai;
            DiaChi = diaChi;
            Email = email;
            Version = version > 0 ? version : 1;
        }
    }
}
