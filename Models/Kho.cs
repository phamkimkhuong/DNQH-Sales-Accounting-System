using System;
using DNQH_KeToanBanHang.Constants;

namespace DNQH_KeToanBanHang.Models
{
    public class Kho
    {
        public string MaKho { get; set; }
        public string TenKho { get; set; }
        public string DiaChi { get; set; }
        public string TrangThai { get; set; }
        public int Version { get; set; }

        public Kho()
        {
            TrangThai = EntityStatusConstants.Warehouse.Active;
            Version = 1;
        }

        public Kho(string maKho, string tenKho, string diaChi, string trangThai, int version = 1)
        {
            MaKho = maKho;
            TenKho = tenKho;
            DiaChi = diaChi;
            TrangThai = string.IsNullOrEmpty(trangThai) ? EntityStatusConstants.Warehouse.Active : trangThai;
            Version = version > 0 ? version : 1;
        }

        public bool IsActive
        {
            get
            {
                return EntityStatusConstants.Warehouse.IsActive(TrangThai);
            }
        }
    }
}
