using System;
using DNQH_KeToanBanHang.Constants;

namespace DNQH_KeToanBanHang.Models
{
    public class SanPham
    {
        public string MaSP { get; set; }
        public string MaNCC { get; set; }
        public string MaLoai { get; set; }
        public string TenSP { get; set; }
        public string DonViTinh { get; set; }
        public decimal DonGiaBan { get; set; }
        public string TrangThai { get; set; }
        public int Version { get; set; }

        public SanPham()
        {
            TrangThai = EntityStatusConstants.Product.Active;
            Version = 1;
        }

        public SanPham(string maSP, string maNCC, string maLoai, string tenSP, string donViTinh, decimal donGiaBan, string trangThai, int version = 1)
        {
            MaSP = maSP;
            MaNCC = maNCC;
            MaLoai = maLoai;
            TenSP = tenSP;
            DonViTinh = donViTinh;
            DonGiaBan = donGiaBan;
            TrangThai = string.IsNullOrEmpty(trangThai) ? EntityStatusConstants.Product.Active : trangThai;
            Version = version > 0 ? version : 1;
        }

        public bool IsActive
        {
            get
            {
                return EntityStatusConstants.Product.IsActive(TrangThai);
            }
        }
    }
}
