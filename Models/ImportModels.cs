using System;
using System.Collections.Generic;

namespace DNQH_KeToanBanHang.Models
{
    /// <summary>
    /// Loại thực thể danh mục hỗ trợ nhập hàng loạt từ Excel/CSV.
    /// </summary>
    public enum ImportEntityType
    {
        KhachHang,
        SanPham,
        NhaCungCap
    }

    /// <summary>
    /// Kết quả phân tích và kiểm tra tính hợp lệ của từng dòng dữ liệu trong tệp Excel/CSV.
    /// </summary>
    public class ImportRowResult
    {
        public int RowNumber { get; set; }
        public int DataIndex { get; set; }
        public bool IsValid { get; set; }
        public List<string> ErrorMessages { get; set; }
        public Dictionary<string, string> RawValues { get; set; }
        public object ParsedEntity { get; set; }

        public ImportRowResult()
        {
            ErrorMessages = new List<string>();
            RawValues = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            IsValid = true;
        }

        public string StatusDisplay
        {
            get { return IsValid ? "Hợp lệ" : string.Format("Lỗi ({0})", ErrorMessages.Count); }
        }

        public string ErrorDisplay
        {
            get { return ErrorMessages != null && ErrorMessages.Count > 0 ? string.Join("; ", ErrorMessages) : string.Empty; }
        }
    }

    /// <summary>
    /// Kết quả tổng hợp của toàn bộ lượt nhập dữ liệu (Batch Import).
    /// </summary>
    public class ImportBatchResult
    {
        public ImportEntityType EntityType { get; set; }
        public int TotalRows { get; set; }
        public int ValidCount { get; set; }
        public int ErrorCount { get; set; }
        public int InsertedCount { get; set; }
        public List<ImportRowResult> Rows { get; set; }

        public ImportBatchResult()
        {
            Rows = new List<ImportRowResult>();
        }

        public bool HasErrors
        {
            get { return ErrorCount > 0; }
        }
    }
}
