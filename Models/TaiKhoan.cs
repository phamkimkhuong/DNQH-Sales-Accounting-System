using System;
using DNQH_KeToanBanHang.Constants;

namespace DNQH_KeToanBanHang.Models
{
    public class TaiKhoan
    {
        public string MaTK { get; set; }
        public string MaNV { get; set; }
        public string TenDangNhap { get; set; }
        public string MatKhau { get; set; }
        public string VaiTro { get; set; }
        public string TrangThai { get; set; }
        public int Version { get; set; }

        public TaiKhoan()
        {
            TrangThai = EntityStatusConstants.Account.Active;
            Version = 1;
        }

        public TaiKhoan(string maTK, string maNV, string tenDangNhap, string matKhau, string vaiTro, string trangThai, int version = 1)
        {
            MaTK = maTK;
            MaNV = maNV;
            TenDangNhap = tenDangNhap;
            MatKhau = matKhau;
            VaiTro = vaiTro;
            TrangThai = string.IsNullOrEmpty(trangThai) ? EntityStatusConstants.Account.Active : trangThai;
            Version = version > 0 ? version : 1;
        }

        public bool IsActive
        {
            get
            {
                return EntityStatusConstants.Account.IsActive(TrangThai);
            }
        }

        public bool KiemTraQuyen(string requiredRole)
        {
            if (string.IsNullOrEmpty(requiredRole) || string.IsNullOrEmpty(VaiTro))
                return false;
            return string.Equals(VaiTro.Trim(), requiredRole.Trim(), StringComparison.OrdinalIgnoreCase);
        }
    }
}
