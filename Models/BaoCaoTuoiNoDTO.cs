using System;

namespace DNQH_KeToanBanHang.Models
{
    /// <summary>
    /// Báo cáo Tuổi nợ Khách hàng - Phân tích tổng hợp theo Khách hàng
    /// </summary>
    public class BaoCaoTuoiNoTongHopDTO
    {
        public string MaKH { get; set; }
        public string TenKH { get; set; }
        public string DienThoai { get; set; }
        public decimal TongNo { get; set; }

        /// <summary>Trong hạn (0 - 30 ngày)</summary>
        public decimal TrongHan_0_30 { get; set; }

        /// <summary>Quá hạn nhẹ (31 - 60 ngày)</summary>
        public decimal QuaHanNhe_31_60 { get; set; }

        /// <summary>Quá hạn trung bình (61 - 90 ngày)</summary>
        public decimal QuaHanTB_61_90 { get; set; }

        /// <summary>Nợ khó đòi (> 90 ngày)</summary>
        public decimal KhoDoi_Tren90 { get; set; }

        /// <summary>Số lượng hóa đơn còn nợ</summary>
        public int SoHoaDonNo { get; set; }

        /// <summary>
        /// Mức độ rủi ro công nợ:
        /// 0: Bình thường (chỉ có trong hạn hoặc không nợ)
        /// 1: Cảnh báo nhẹ (quá hạn 31 - 60 ngày)
        /// 2: Cảnh báo trung bình (quá hạn 61 - 90 ngày)
        /// 3: Nợ khó đòi - Rủi ro cao (> 90 ngày)
        /// </summary>
        public int MucDoRuiRo { get; set; }

        public string TenMucDoRuiRo
        {
            get
            {
                if (KhoDoi_Tren90 > 0) return "Nợ khó đòi (>90 ngày)";
                if (QuaHanTB_61_90 > 0) return "Quá hạn TB (61-90 ngày)";
                if (QuaHanNhe_31_60 > 0) return "Quá hạn nhẹ (31-60 ngày)";
                if (TrongHan_0_30 > 0) return "Trong hạn (<=30 ngày)";
                return "Không có nợ";
            }
        }

        public bool CoNoQuaHan
        {
            get { return QuaHanNhe_31_60 > 0 || QuaHanTB_61_90 > 0 || KhoDoi_Tren90 > 0; }
        }

        public bool CoNoKhoDoi
        {
            get { return KhoDoi_Tren90 > 0; }
        }
    }

    /// <summary>
    /// Báo cáo Tuổi nợ Khách hàng - Phân tích chi tiết theo Hóa đơn bán
    /// </summary>
    public class BaoCaoTuoiNoChiTietDTO
    {
        public string MaHDB { get; set; }
        public DateTime NgayLap { get; set; }
        public string MaKH { get; set; }
        public string TenKH { get; set; }
        public string DienThoai { get; set; }
        public decimal TongTien { get; set; }
        public decimal DaThu { get; set; }
        public decimal ConNo { get; set; }
        public int SoNgayNo { get; set; }

        /// <summary>
        /// 0: Trong hạn (0-30 ngày)
        /// 1: Quá hạn nhẹ (31-60 ngày)
        /// 2: Quá hạn TB (61-90 ngày)
        /// 3: Nợ khó đòi (>90 ngày)
        /// </summary>
        public int MaNhomTuoiNo { get; set; }

        public string TenNhomTuoiNo
        {
            get
            {
                switch (MaNhomTuoiNo)
                {
                    case 3: return "Nợ khó đòi (>90 ngày)";
                    case 2: return "Quá hạn TB (61-90 ngày)";
                    case 1: return "Quá hạn nhẹ (31-60 ngày)";
                    default: return "Trong hạn (<=30 ngày)";
                }
            }
        }
    }

    /// <summary>
    /// DTO tóm tắt tình trạng nợ phục vụ cảnh báo nhanh khi chọn khách hàng hoặc lập đơn đặt hàng
    /// </summary>
    public class ThongTinCongNoKhachHangDTO
    {
        public string MaKH { get; set; }
        public string TenKH { get; set; }
        public decimal TongNo { get; set; }
        public decimal TrongHan_0_30 { get; set; }
        public decimal QuaHanNhe_31_60 { get; set; }
        public decimal QuaHanTB_61_90 { get; set; }
        public decimal KhoDoi_Tren90 { get; set; }
        public int SoHoaDonNo { get; set; }

        public bool HasBadDebt
        {
            get { return KhoDoi_Tren90 > 0; }
        }

        public bool HasOverdue
        {
            get { return QuaHanNhe_31_60 > 0 || QuaHanTB_61_90 > 0 || KhoDoi_Tren90 > 0; }
        }

        public string GetWarningSummary()
        {
            if (HasBadDebt)
            {
                return string.Format("CẢNH BÁO NỢ KHÓ ĐÒI: Khách hàng đang có nợ quá hạn >90 ngày: {0:N0} VNĐ (Tổng nợ: {1:N0} VNĐ). Hệ thống tạm khóa đặt hàng!",
                    KhoDoi_Tren90, TongNo);
            }
            if (QuaHanTB_61_90 > 0)
            {
                return string.Format("CẢNH BÁO: Khách hàng đang có nợ quá hạn 61-90 ngày: {0:N0} VNĐ (Tổng nợ: {1:N0} VNĐ). Cần xác nhận trước khi lập đơn!",
                    QuaHanTB_61_90, TongNo);
            }
            if (QuaHanNhe_31_60 > 0)
            {
                return string.Format("LƯU Ý: Khách hàng đang có nợ quá hạn 31-60 ngày: {0:N0} VNĐ (Tổng nợ: {1:N0} VNĐ).",
                    QuaHanNhe_31_60, TongNo);
            }
            if (TongNo > 0)
            {
                return string.Format("Thông tin: Khách hàng đang có nợ trong hạn: {0:N0} VNĐ.", TongNo);
            }
            return "Khách hàng không có dư nợ. Tín dụng tốt.";
        }
    }
}
