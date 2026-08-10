using System;

namespace DNQH_KeToanBanHang.Models
{
    public class ChiTietChungTu
    {
        public string MaCT { get; set; }
        public int STT { get; set; }
        public string TaiKhoanNo { get; set; }
        public string TaiKhoanCo { get; set; }
        public decimal SoTien { get; set; }
        public string DienGiai { get; set; }

        public ChiTietChungTu()
        {
            STT = 1;
            SoTien = 0;
        }
    }
}
