using System;

namespace DNQH_KeToanBanHang.Models
{
    /// <summary>
    /// Đối tượng ánh xạ với bảng NHATKYHOATDONG trên SQL Server (Business Audit Trail tập trung toàn hệ thống).
    /// </summary>
    public class NhatKyHoatDong
    {
        public long Id { get; set; }
        public int STT { get; set; }
        public DateTime ThoiGian { get; set; }
        public string CapDo { get; set; }
        public string HanhDong { get; set; }
        public string MaNV { get; set; }
        public string TenNV { get; set; }
        public string VaiTro { get; set; }
        public string TenMay { get; set; }
        public string LoaiDoiTuong { get; set; }
        public string MaDoiTuong { get; set; }
        public string KetQua { get; set; }
        public long ThoiGianXuLyMs { get; set; }
        public string CorrelationId { get; set; }
        public string NoiDung { get; set; }

        public NhatKyHoatDong()
        {
            ThoiGian = DateTime.Now;
            CapDo = "INFO";
            KetQua = "Thành công";
            TenMay = Environment.MachineName;
        }
    }
}
