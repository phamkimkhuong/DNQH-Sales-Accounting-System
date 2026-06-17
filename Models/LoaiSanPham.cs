using System;

namespace DNQH_KeToanBanHang.Models
{
    public class LoaiSanPham
    {
        public string MaLoai { get; set; }
        public string TenLoai { get; set; }
        public string MoTa { get; set; }
        public int Version { get; set; }

        public LoaiSanPham()
        {
            Version = 1;
        }

        public LoaiSanPham(string maLoai, string tenLoai, string moTa, int version = 1)
        {
            MaLoai = maLoai;
            TenLoai = tenLoai;
            MoTa = moTa;
            Version = version > 0 ? version : 1;
        }
    }
}
