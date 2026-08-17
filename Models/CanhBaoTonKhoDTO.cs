using System;

namespace DNQH_KeToanBanHang.Models
{
    /// <summary>
    /// DTO thông tin cảnh báo tồn kho an toàn (hết hàng hoặc dưới mức tối thiểu).
    /// </summary>
    public class CanhBaoTonKhoDTO
    {
        public string MaSP { get; set; }
        public string TenSP { get; set; }
        public string TenLoaiSP { get; set; }
        public string DonViTinh { get; set; }
        public int TongTon { get; set; }
        public int TonThapNhatKho { get; set; }
        public int SoKhoThieu { get; set; }
        public decimal DonGiaBan { get; set; }
        public string MucDoCanhBao { get; set; }
        public string ChiTietKho { get; set; }

        public CanhBaoTonKhoDTO()
        {
            MaSP = string.Empty;
            TenSP = string.Empty;
            TenLoaiSP = string.Empty;
            DonViTinh = string.Empty;
            TongTon = 0;
            TonThapNhatKho = 0;
            SoKhoThieu = 0;
            DonGiaBan = 0;
            MucDoCanhBao = string.Empty;
            ChiTietKho = string.Empty;
        }

        public static string XacDinhMucDoCanhBao(int tongTon, int safetyThreshold = 10)
        {
            return XacDinhMucDoCanhBao(tongTon, safetyThreshold, int.MaxValue);
        }

        public static string XacDinhMucDoCanhBao(int tongTon, int safetyThreshold, int tonThapNhatKho)
        {
            if (tongTon <= 0)
            {
                return "Hết hàng";
            }
            if (tongTon <= safetyThreshold)
            {
                return "Cần nhập hàng mới";
            }
            if (tonThapNhatKho <= safetyThreshold)
            {
                return "Cần điều chuyển kho";
            }
            return "An toàn";
        }
    }
}
