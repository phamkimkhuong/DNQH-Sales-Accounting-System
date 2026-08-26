 using System;

namespace DNQH_KeToanBanHang.Models
{
    /// <summary>
    /// Đối tượng truyền tải dữ liệu đại diện cho 1 bản ghi nhật ký vận hành (Audit Trail / Activity Log).
    /// </summary>
    public class LogEntryDTO
    {
        public int STT { get; set; }
        public DateTime ThoiGian { get; set; }
        public string CapDo { get; set; }
        public string HanhDong { get; set; }
        public string CorrelationId { get; set; }
        public string NguoiDung { get; set; }
        public string VaiTro { get; set; }
        public string ThucThe { get; set; }
        public string KetQua { get; set; }
        public long ThoiGianXuLyMs { get; set; }
        public string ThongDiep { get; set; }
        public string ChiTietLoi { get; set; }
        public string TenFileNguon { get; set; }

        public LogEntryDTO()
        {
            CapDo = "INFO";
            KetQua = "Success";
        }
    }
}
