using System;

namespace DNQH_KeToanBanHang.Constants
{
    public static class DocumentTypeConstants
    {
        public const string ChungTuBanHang = "Chứng từ bán hàng";
        public const string PhieuThu = "Phiếu thu";
        public const string PhieuChi = "Phiếu chi";

        public static readonly string[] All = new string[]
        {
            ChungTuBanHang,
            PhieuThu,
            PhieuChi
        };
    }
}
