using System;
using DNQH_KeToanBanHang.Constants;

namespace DNQH_KeToanBanHang.Models
{
    public class NhanVien
    {
        public string MaNV { get; set; }
        public string HoTen { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string GioiTinh { get; set; }
        public string SoDienThoai { get; set; }
        public string DiaChi { get; set; }
        public string ChucVu { get; set; }
        public string TrangThai { get; set; }
        public int Version { get; set; }

        public NhanVien()
        {
            TrangThai = EntityStatusConstants.Employee.Active;
            Version = 1;
        }

        public NhanVien(string maNV, string hoTen, DateTime? ngaySinh, string gioiTinh, 
                        string soDienThoai, string diaChi, string chucVu, string trangThai, int version = 1)
        {
            MaNV = maNV;
            HoTen = hoTen;
            NgaySinh = ngaySinh;
            GioiTinh = gioiTinh;
            SoDienThoai = soDienThoai;
            DiaChi = diaChi;
            ChucVu = chucVu;
            TrangThai = string.IsNullOrEmpty(trangThai) ? EntityStatusConstants.Employee.Active : trangThai;
            Version = version > 0 ? version : 1;
        }

        public bool IsActive
        {
            get
            {
                return EntityStatusConstants.Employee.IsActive(TrangThai);
            }
        }
    }
}
