using System;

namespace DNQH_KeToanBanHang.Models
{
    public class NhaCungCap
    {
        public string MaNCC { get; set; }
        public string TenNCC { get; set; }
        public string DiaChi { get; set; }
        public string SoDienThoai { get; set; }
        public string Email { get; set; }
        public int Version { get; set; }

        public NhaCungCap()
        {
            Version = 1;
        }

        public NhaCungCap(string maNCC, string tenNCC, string diaChi, string soDienThoai, string email, int version = 1)
        {
            MaNCC = maNCC;
            TenNCC = tenNCC;
            DiaChi = diaChi;
            SoDienThoai = soDienThoai;
            Email = email;
            Version = version > 0 ? version : 1;
        }
    }
}
