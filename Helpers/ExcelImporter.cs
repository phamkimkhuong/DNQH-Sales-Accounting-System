using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;
using DNQH_KeToanBanHang.Constants;
using DNQH_KeToanBanHang.DataAccess;
using DNQH_KeToanBanHang.Models;

namespace DNQH_KeToanBanHang.Helpers
{
    /// <summary>
    /// Tiện ích đọc, kiểm tra hợp lệ, tạo mẫu và nhập dữ liệu hàng loạt từ Excel (.xlsx, .xls, .xml) và CSV.
    /// - Không phụ thuộc Microsoft Office Interop (hoạt động độc lập, không cần cài Office).
    /// - Không phụ thuộc thư viện bên ngoài (sử dụng 100% BCL .NET Framework 4.8: System.IO.Compression + System.Xml.Linq).
    /// - Hỗ trợ nhập danh mục Khách hàng, Sản phẩm, Nhà cung cấp với cơ chế kiểm tra toàn vẹn và Transaction an toàn.
    /// </summary>
    public static class ExcelImporter
    {
        public const string TemplateCompanyName = "CÔNG TY CỔ PHẦN THƯƠNG MẠI & DỊCH VỤ DNQH";

        #region 1. File Readers (XLSX, XLS/SpreadsheetML, CSV)

        /// <summary>
        /// Đọc nội dung tệp bảng tính (hỗ trợ .xlsx, .xls, .xml, .csv) thành danh sách các dòng và cột chuỗi.
        /// </summary>
        public static List<List<string>> ReadFile(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath) || !File.Exists(filePath))
                throw new FileNotFoundException("Không tìm thấy tệp dữ liệu cần nhập.", filePath);

            string ext = Path.GetExtension(filePath).ToLowerInvariant();

            if (ext == ".xlsx")
            {
                return ReadOpenXmlFile(filePath);
            }
            else if (ext == ".csv" || ext == ".txt")
            {
                return ReadCsvFile(filePath);
            }
            else if (ext == ".xls" || ext == ".xml")
            {
                // Kiểm tra xem tệp có phải là nhị phân BIFF8 hay SpreadsheetML XML / CSV
                byte[] header = new byte[8];
                using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    fs.Read(header, 0, Math.Min((int)fs.Length, 8));
                }

                // BIFF8 OLE2 Compound File Header: D0 CF 11 E0 A1 B1 1A E1
                if (header.Length >= 4 && header[0] == 0xD0 && header[1] == 0xCF && header[2] == 0x11 && header[3] == 0xE0)
                {
                    throw new NotSupportedException(
                        "Tệp đang ở định dạng Excel nhị phân cũ (.xls BIFF8).\n\n" +
                        "Để đảm bảo an toàn và tương thích 100%, quý khách vui lòng mở tệp trong Excel và chọn 'Save As' sang định dạng " +
                        "'.xlsx' hoặc '.csv', hoặc nhấn nút 'Tải tệp mẫu' trên phần mềm để điền dữ liệu.");
                }

                // Kiểm tra nếu là tệp ZIP (đôi khi người dùng đổi đuôi .xlsx thành .xls)
                if (header.Length >= 4 && header[0] == 0x50 && header[1] == 0x4B && header[2] == 0x03 && header[3] == 0x04)
                {
                    return ReadOpenXmlFile(filePath);
                }

                // Đọc thử dưới dạng SpreadsheetML XML
                try
                {
                    return ReadSpreadsheetMlFile(filePath);
                }
                catch
                {
                    // Fallback sang CSV nếu không phải XML hợp lệ
                    return ReadCsvFile(filePath);
                }
            }
            else
            {
                throw new NotSupportedException(string.Format("Định dạng tệp '{0}' không được hỗ trợ. Vui lòng sử dụng tệp .xlsx, .xls hoặc .csv.", ext));
            }
        }

        /// <summary>
        /// Phân tích tệp Excel OpenXML (.xlsx) thông qua System.IO.Compression.ZipArchive & System.Xml.Linq.
        /// </summary>
        public static List<List<string>> ReadOpenXmlFile(string filePath)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                return ReadOpenXmlStream(fs);
            }
        }

        public static List<List<string>> ReadOpenXmlStream(Stream stream)
        {
            List<List<string>> rows = new List<List<string>>();

            using (ZipArchive zip = new ZipArchive(stream, ZipArchiveMode.Read, true))
            {
                // 1. Đọc bảng Shared Strings nếu có
                List<string> sharedStrings = new List<string>();
                ZipArchiveEntry sstEntry = zip.GetEntry("xl/sharedStrings.xml");
                if (sstEntry != null)
                {
                    using (Stream sstStream = sstEntry.Open())
                    {
                        XDocument sstDoc = XDocument.Load(sstStream);
                        XNamespace ns = sstDoc.Root.Name.Namespace;
                        foreach (XElement si in sstDoc.Descendants(ns + "si"))
                        {
                            StringBuilder sb = new StringBuilder();
                            foreach (XElement t in si.Descendants(ns + "t"))
                            {
                                sb.Append(t.Value);
                            }
                            sharedStrings.Add(sb.ToString());
                        }
                    }
                }

                // 2. Tìm worksheet đầu tiên
                ZipArchiveEntry sheetEntry = zip.GetEntry("xl/worksheets/sheet1.xml");
                if (sheetEntry == null)
                {
                    foreach (ZipArchiveEntry entry in zip.Entries)
                    {
                        if (entry.FullName.StartsWith("xl/worksheets/sheet", StringComparison.OrdinalIgnoreCase) &&
                            entry.FullName.EndsWith(".xml", StringComparison.OrdinalIgnoreCase))
                        {
                            sheetEntry = entry;
                            break;
                        }
                    }
                }

                if (sheetEntry == null)
                    throw new InvalidDataException("Không tìm thấy trang tính (worksheet) nào trong tệp Excel .xlsx.");

                // 3. Đọc dữ liệu các hàng và ô
                using (Stream sheetStream = sheetEntry.Open())
                {
                    XDocument sheetDoc = XDocument.Load(sheetStream);
                    XNamespace ns = sheetDoc.Root.Name.Namespace;
                    IEnumerable<XElement> rowElements = sheetDoc.Descendants(ns + "row");

                    foreach (XElement rowElem in rowElements)
                    {
                        List<string> cellValues = new List<string>();
                        int currentColIndex = 0;

                        foreach (XElement c in rowElem.Elements(ns + "c"))
                        {
                            string cellRef = (string)c.Attribute("r");
                            int colIndex = !string.IsNullOrEmpty(cellRef) ? ColumnNameToIndex(cellRef) : currentColIndex;

                            while (cellValues.Count < colIndex)
                            {
                                cellValues.Add(string.Empty);
                            }

                            string type = (string)c.Attribute("t");
                            string val = string.Empty;

                            if (type == "s")
                            {
                                XElement vElem = c.Element(ns + "v");
                                int sstIdx;
                                if (vElem != null && int.TryParse(vElem.Value, out sstIdx) && sstIdx >= 0 && sstIdx < sharedStrings.Count)
                                {
                                    val = sharedStrings[sstIdx];
                                }
                            }
                            else if (type == "inlineStr")
                            {
                                XElement tElem = c.Descendants(ns + "t").FirstOrDefault();
                                if (tElem != null) val = tElem.Value;
                            }
                            else
                            {
                                XElement vElem = c.Element(ns + "v");
                                if (vElem != null) val = vElem.Value;
                            }

                            cellValues.Add(val != null ? val.Trim() : string.Empty);
                            currentColIndex = cellValues.Count;
                        }

                        if (cellValues.Any(v => !string.IsNullOrWhiteSpace(v)))
                        {
                            rows.Add(cellValues);
                        }
                    }
                }
            }

            return rows;
        }

        /// <summary>
        /// Đọc tệp bảng tính định dạng SpreadsheetML (XML Spreadsheet 2003 / .xls / .xml).
        /// </summary>
        public static List<List<string>> ReadSpreadsheetMlFile(string filePath)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                return ReadSpreadsheetMlStream(fs);
            }
        }

        public static List<List<string>> ReadSpreadsheetMlStream(Stream stream)
        {
            List<List<string>> rows = new List<List<string>>();
            XDocument doc = XDocument.Load(stream);
            XNamespace ss = "urn:schemas-microsoft-com:office:spreadsheet";

            // Lấy bảng đầu tiên
            XElement tableElem = doc.Descendants(ss + "Table").FirstOrDefault();
            if (tableElem == null)
            {
                tableElem = doc.Descendants().FirstOrDefault(x => x.Name.LocalName == "Table");
            }

            if (tableElem == null)
                throw new InvalidDataException("Tệp XML không chứa thẻ <Table> bảng tính hợp lệ.");

            XNamespace tableNs = tableElem.Name.Namespace;

            foreach (XElement rowElem in tableElem.Elements(tableNs + "Row"))
            {
                List<string> cellValues = new List<string>();
                int currentCellIndex = 1;

                foreach (XElement cellElem in rowElem.Elements(tableNs + "Cell"))
                {
                    // Thuộc tính ss:Index (1-based) nếu có ô trống bị bỏ qua
                    XAttribute indexAttr = cellElem.Attribute(ss + "Index") ?? cellElem.Attributes().FirstOrDefault(a => a.Name.LocalName == "Index");
                    int explicitIndex;
                    if (indexAttr != null && int.TryParse(indexAttr.Value, out explicitIndex) && explicitIndex > currentCellIndex)
                    {
                        while (currentCellIndex < explicitIndex)
                        {
                            cellValues.Add(string.Empty);
                            currentCellIndex++;
                        }
                    }

                    XElement dataElem = cellElem.Element(tableNs + "Data") ?? cellElem.Elements().FirstOrDefault(e => e.Name.LocalName == "Data");
                    string text = dataElem != null ? dataElem.Value : string.Empty;
                    cellValues.Add(text != null ? text.Trim() : string.Empty);
                    currentCellIndex++;
                }

                if (cellValues.Any(v => !string.IsNullOrWhiteSpace(v)))
                {
                    rows.Add(cellValues);
                }
            }

            return rows;
        }

        /// <summary>
        /// Phân tích cú pháp tệp CSV theo chuẩn RFC 4180 (hỗ trợ dấu nháy kép bọc, dấu phẩy/chấm phẩy, xuống dòng trong ô).
        /// </summary>
        public static List<List<string>> ReadCsvFile(string filePath)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (StreamReader reader = new StreamReader(fs, Encoding.UTF8, true))
            {
                string text = reader.ReadToEnd();
                return ParseCsvString(text);
            }
        }

        public static List<List<string>> ParseCsvString(string csvContent)
        {
            List<List<string>> result = new List<List<string>>();
            if (string.IsNullOrEmpty(csvContent))
                return result;

            // Tự động phát hiện dấu phân cách (dấu phẩy hoặc chấm phẩy)
            string firstLine = csvContent.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).FirstOrDefault() ?? string.Empty;
            char delimiter = ',';
            if (firstLine.Count(c => c == ';') > firstLine.Count(c => c == ','))
            {
                delimiter = ';';
            }

            List<string> currentRow = new List<string>();
            StringBuilder currentCell = new StringBuilder();
            bool insideQuotes = false;

            for (int i = 0; i < csvContent.Length; i++)
            {
                char c = csvContent[i];

                if (insideQuotes)
                {
                    if (c == '"')
                    {
                        if (i + 1 < csvContent.Length && csvContent[i + 1] == '"')
                        {
                            currentCell.Append('"');
                            i++; // Bỏ qua nháy kép escape
                        }
                        else
                        {
                            insideQuotes = false;
                        }
                    }
                    else
                    {
                        currentCell.Append(c);
                    }
                }
                else
                {
                    if (c == '"')
                    {
                        insideQuotes = true;
                    }
                    else if (c == delimiter)
                    {
                        currentRow.Add(currentCell.ToString().Trim());
                        currentCell.Length = 0;
                    }
                    else if (c == '\r')
                    {
                        if (i + 1 < csvContent.Length && csvContent[i + 1] == '\n')
                        {
                            i++;
                        }
                        currentRow.Add(currentCell.ToString().Trim());
                        currentCell.Length = 0;

                        if (currentRow.Any(cell => !string.IsNullOrWhiteSpace(cell)))
                        {
                            result.Add(currentRow);
                        }
                        currentRow = new List<string>();
                    }
                    else if (c == '\n')
                    {
                        currentRow.Add(currentCell.ToString().Trim());
                        currentCell.Length = 0;

                        if (currentRow.Any(cell => !string.IsNullOrWhiteSpace(cell)))
                        {
                            result.Add(currentRow);
                        }
                        currentRow = new List<string>();
                    }
                    else
                    {
                        currentCell.Append(c);
                    }
                }
            }

            if (currentCell.Length > 0 || currentRow.Count > 0)
            {
                currentRow.Add(currentCell.ToString().Trim());
                if (currentRow.Any(cell => !string.IsNullOrWhiteSpace(cell)))
                {
                    result.Add(currentRow);
                }
            }

            return result;
        }

        public static int ColumnNameToIndex(string cellRef)
        {
            if (string.IsNullOrEmpty(cellRef)) return 0;
            int col = 0;
            foreach (char ch in cellRef)
            {
                if (char.IsLetter(ch))
                {
                    col = col * 26 + (char.ToUpperInvariant(ch) - 'A' + 1);
                }
                else
                {
                    break;
                }
            }
            return Math.Max(0, col - 1);
        }

        #endregion

        #region 2. Template Generation (SpreadsheetML)

        /// <summary>
        /// Tạo nội dung tệp mẫu Excel (.xls / SpreadsheetML) chuẩn nghiệp vụ có màu thương hiệu, hướng dẫn và dữ liệu mẫu.
        /// </summary>
        public static string GenerateTemplateSpreadsheetMl(ImportEntityType entityType, string companyName = null)
        {
            string comp = !string.IsNullOrWhiteSpace(companyName) ? companyName : TemplateCompanyName;
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
            sb.AppendLine("<?mso-application progid=\"Excel.Sheet\"?>");
            sb.AppendLine("<Workbook xmlns=\"urn:schemas-microsoft-com:office:spreadsheet\"");
            sb.AppendLine(" xmlns:o=\"urn:schemas-microsoft-com:office:office\"");
            sb.AppendLine(" xmlns:x=\"urn:schemas-microsoft-com:office:excel\"");
            sb.AppendLine(" xmlns:ss=\"urn:schemas-microsoft-com:office:spreadsheet\"");
            sb.AppendLine(" xmlns:html=\"http://www.w3.org/TR/REC-html40\">");

            // Styles
            sb.AppendLine("  <Styles>");
            sb.AppendLine("    <Style ss:ID=\"Default\" ss:Name=\"Normal\">");
            sb.AppendLine("      <Alignment ss:Vertical=\"Center\"/>");
            sb.AppendLine("      <Font ss:FontName=\"Segoe UI\" ss:Size=\"10\" ss:Color=\"#333333\"/>");
            sb.AppendLine("    </Style>");

            // Company Title Style
            sb.AppendLine("    <Style ss:ID=\"CompanyHeader\">");
            sb.AppendLine("      <Alignment ss:Horizontal=\"Left\" ss:Vertical=\"Center\"/>");
            sb.AppendLine("      <Font ss:FontName=\"Segoe UI\" ss:Size=\"11\" ss:Bold=\"1\" ss:Color=\"#1E3A8A\"/>");
            sb.AppendLine("    </Style>");

            // Report Title Style
            sb.AppendLine("    <Style ss:ID=\"ReportTitle\">");
            sb.AppendLine("      <Alignment ss:Horizontal=\"Center\" ss:Vertical=\"Center\"/>");
            sb.AppendLine("      <Font ss:FontName=\"Segoe UI\" ss:Size=\"14\" ss:Bold=\"1\" ss:Color=\"#1E3A8A\"/>");
            sb.AppendLine("    </Style>");

            // Header Column Style
            sb.AppendLine("    <Style ss:ID=\"HeaderCol\">");
            sb.AppendLine("      <Alignment ss:Horizontal=\"Center\" ss:Vertical=\"Center\" ss:WrapText=\"1\"/>");
            sb.AppendLine("      <Borders>");
            sb.AppendLine("        <Border ss:Position=\"Bottom\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\" ss:Color=\"#B0C4DE\"/>");
            sb.AppendLine("        <Border ss:Position=\"Left\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\" ss:Color=\"#B0C4DE\"/>");
            sb.AppendLine("        <Border ss:Position=\"Right\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\" ss:Color=\"#B0C4DE\"/>");
            sb.AppendLine("        <Border ss:Position=\"Top\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\" ss:Color=\"#B0C4DE\"/>");
            sb.AppendLine("      </Borders>");
            sb.AppendLine("      <Font ss:FontName=\"Segoe UI\" ss:Size=\"10\" ss:Bold=\"1\" ss:Color=\"#FFFFFF\"/>");
            sb.AppendLine("      <Interior ss:Color=\"#1E3A8A\" ss:Pattern=\"Solid\"/>");
            sb.AppendLine("    </Style>");

            // Instruction Row Style
            sb.AppendLine("    <Style ss:ID=\"InstructionRow\">");
            sb.AppendLine("      <Alignment ss:Horizontal=\"Left\" ss:Vertical=\"Center\" ss:WrapText=\"1\"/>");
            sb.AppendLine("      <Borders>");
            sb.AppendLine("        <Border ss:Position=\"Bottom\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\" ss:Color=\"#E5E7EB\"/>");
            sb.AppendLine("        <Border ss:Position=\"Left\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\" ss:Color=\"#E5E7EB\"/>");
            sb.AppendLine("        <Border ss:Position=\"Right\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\" ss:Color=\"#E5E7EB\"/>");
            sb.AppendLine("        <Border ss:Position=\"Top\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\" ss:Color=\"#E5E7EB\"/>");
            sb.AppendLine("      </Borders>");
            sb.AppendLine("      <Font ss:FontName=\"Segoe UI\" ss:Size=\"9\" ss:Italic=\"1\" ss:Color=\"#6B7280\"/>");
            sb.AppendLine("      <Interior ss:Color=\"#F3F4F6\" ss:Pattern=\"Solid\"/>");
            sb.AppendLine("    </Style>");

            // Sample Data Style
            sb.AppendLine("    <Style ss:ID=\"DataCell\">");
            sb.AppendLine("      <Alignment ss:Vertical=\"Center\"/>");
            sb.AppendLine("      <Borders>");
            sb.AppendLine("        <Border ss:Position=\"Bottom\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\" ss:Color=\"#E5E7EB\"/>");
            sb.AppendLine("        <Border ss:Position=\"Left\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\" ss:Color=\"#E5E7EB\"/>");
            sb.AppendLine("        <Border ss:Position=\"Right\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\" ss:Color=\"#E5E7EB\"/>");
            sb.AppendLine("        <Border ss:Position=\"Top\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\" ss:Color=\"#E5E7EB\"/>");
            sb.AppendLine("      </Borders>");
            sb.AppendLine("      <Font ss:FontName=\"Segoe UI\" ss:Size=\"10\" ss:Color=\"#1F2937\"/>");
            sb.AppendLine("    </Style>");

            // Currency Style
            sb.AppendLine("    <Style ss:ID=\"CurrencyCell\">");
            sb.AppendLine("      <Alignment ss:Horizontal=\"Right\" ss:Vertical=\"Center\"/>");
            sb.AppendLine("      <Borders>");
            sb.AppendLine("        <Border ss:Position=\"Bottom\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\" ss:Color=\"#E5E7EB\"/>");
            sb.AppendLine("        <Border ss:Position=\"Left\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\" ss:Color=\"#E5E7EB\"/>");
            sb.AppendLine("        <Border ss:Position=\"Right\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\" ss:Color=\"#E5E7EB\"/>");
            sb.AppendLine("        <Border ss:Position=\"Top\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\" ss:Color=\"#E5E7EB\"/>");
            sb.AppendLine("      </Borders>");
            sb.AppendLine("      <Font ss:FontName=\"Segoe UI\" ss:Size=\"10\" ss:Color=\"#1F2937\"/>");
            sb.AppendLine("      <NumberFormat ss:Format=\"#,##0\"/>");
            sb.AppendLine("    </Style>");

            sb.AppendLine("  </Styles>");

            string sheetName;
            string title;
            string[] headers;
            string[] instructions;
            List<string[]> sampleRows = new List<string[]>();
            int colCount;

            if (entityType == ImportEntityType.KhachHang)
            {
                sheetName = "KhachHang";
                title = "TỆP MẪU NHẬP DANH MỤC KHÁCH HÀNG";
                headers = new[] { "Mã KH*", "Tên khách hàng*", "Số điện thoại", "Địa chỉ", "Email" };
                instructions = new[]
                {
                    "(Bắt buộc, tối đa 10 ký tự, không trùng)",
                    "(Bắt buộc, tối đa 100 ký tự)",
                    "(Tùy chọn, 8-15 chữ số)",
                    "(Tùy chọn, tối đa 200 ký tự)",
                    "(Tùy chọn, định dạng email hợp lệ)"
                };
                sampleRows.Add(new[] { "KH_EX01", "Công ty Cổ phần Hoàng Gia", "0912345678", "123 Nguyễn Trãi, Thanh Xuân, Hà Nội", "hoanggia@gmail.com" });
                sampleRows.Add(new[] { "KH_EX02", "Đại lý Bánh kẹo Miền Trung", "0987654321", "45 Lê Lợi, TP. Đà Nẵng", "mientrung@daily.vn" });
                sampleRows.Add(new[] { "KH_EX03", "Cửa hàng Tiện lợi Tuấn Phát", "0905123987", "78 Quang Trung, Gò Vấp, TP.HCM", "" });
            }
            else if (entityType == ImportEntityType.NhaCungCap)
            {
                sheetName = "NhaCungCap";
                title = "TỆP MẪU NHẬP DANH MỤC NHÀ CUNG CẤP";
                headers = new[] { "Mã NCC*", "Tên nhà cung cấp*", "Địa chỉ", "Số điện thoại", "Email" };
                instructions = new[]
                {
                    "(Bắt buộc, tối đa 10 ký tự, không trùng)",
                    "(Bắt buộc, tối đa 100 ký tự)",
                    "(Tùy chọn, tối đa 200 ký tự)",
                    "(Tùy chọn, 8-15 chữ số)",
                    "(Tùy chọn, định dạng email hợp lệ)"
                };
                sampleRows.Add(new[] { "NCC_EX01", "Tổng Công Ty May 10", "765 Nguyễn Văn Linh, Long Biên, Hà Nội", "02438654321", "contact@may10.vn" });
                sampleRows.Add(new[] { "NCC_EX02", "Công Ty CP Sữa Vinamilk", "Số 10 Tân Trào, Tân Phú, Quận 7, TP.HCM", "02854155555", "vinamilk@vinamilk.com.vn" });
                sampleRows.Add(new[] { "NCC_EX03", "Công ty TNHH Nhựa Tiền Phong", "Số 2 An Đà, Ngô Quyền, Hải Phòng", "02253813979", "nhuatienphong@vnn.vn" });
            }
            else // SanPham
            {
                sheetName = "SanPham";
                title = "TỆP MẪU NHẬP DANH MỤC SẢN PHẨM";
                headers = new[] { "Mã SP*", "Tên sản phẩm*", "Mã loại*", "Mã NCC*", "Đơn vị tính*", "Đơn giá bán*", "Trạng thái" };
                instructions = new[]
                {
                    "(Bắt buộc, tối đa 10 ký tự, không trùng)",
                    "(Bắt buộc, tối đa 150 ký tự)",
                    "(Bắt buộc, phải tồn tại trong CSDL)",
                    "(Bắt buộc, phải tồn tại trong CSDL)",
                    "(Bắt buộc: Cái, Hộp, Bộ, Thùng, Kg...)",
                    "(Bắt buộc, số >= 0, ví dụ: 250000)",
                    "(Mặc định: Đang kinh doanh)"
                };

                // Lấy thử danh sách Mã Loại và Mã NCC thực tế từ DB để đưa vào mẫu ví dụ
                string sampleLoai = "L01";
                string sampleNCC = "NCC01";
                try
                {
                    SanPhamDAL spDal = new SanPhamDAL();
                    var loaiSet = spDal.GetAllMaLoai();
                    var nccSet = spDal.GetAllMaNCC();
                    if (loaiSet.Count > 0) sampleLoai = loaiSet.First();
                    if (nccSet.Count > 0) sampleNCC = nccSet.First();
                }
                catch { }

                sampleRows.Add(new[] { "SP_EX01", "Bộ quần áo thể thao nam Pro", sampleLoai, sampleNCC, "Bộ", "350000", "Đang kinh doanh" });
                sampleRows.Add(new[] { "SP_EX02", "Sữa đặc có đường Ông Thọ 380g", sampleLoai, sampleNCC, "Hộp", "24500", "Đang kinh doanh" });
                sampleRows.Add(new[] { "SP_EX03", "Bình giữ nhiệt Inox 500ml", sampleLoai, sampleNCC, "Cái", "180000", "Đang kinh doanh" });
            }

            colCount = headers.Length;

            sb.AppendLine(string.Format("  <Worksheet ss:Name=\"{0}\">", sheetName));
            sb.AppendLine(string.Format("    <Table ss:ExpandedColumnCount=\"{0}\" x:FullColumns=\"1\" x:FullRows=\"1\">", colCount));

            // Set column widths
            for (int i = 0; i < colCount; i++)
            {
                int width = (i == 1 || i == 3) ? 220 : 130;
                sb.AppendLine(string.Format("      <Column ss:Index=\"{0}\" ss:AutoFitWidth=\"1\" ss:Width=\"{1}\"/>", i + 1, width));
            }

            // Company Title
            sb.AppendLine("      <Row ss:Height=\"22\">");
            sb.AppendLine(string.Format("        <Cell ss:MergeAcross=\"{0}\" ss:StyleID=\"CompanyHeader\"><Data ss:Type=\"String\">{1}</Data></Cell>",
                colCount - 1, EscapeXml(comp)));
            sb.AppendLine("      </Row>");

            // Report Title
            sb.AppendLine("      <Row ss:Height=\"30\">");
            sb.AppendLine(string.Format("        <Cell ss:MergeAcross=\"{0}\" ss:StyleID=\"ReportTitle\"><Data ss:Type=\"String\">{1}</Data></Cell>",
                colCount - 1, EscapeXml(title)));
            sb.AppendLine("      </Row>");

            // Empty row
            sb.AppendLine("      <Row ss:Height=\"12\"></Row>");

            // Header Row
            sb.AppendLine("      <Row ss:Height=\"26\">");
            foreach (string header in headers)
            {
                sb.AppendLine(string.Format("        <Cell ss:StyleID=\"HeaderCol\"><Data ss:Type=\"String\">{0}</Data></Cell>", EscapeXml(header)));
            }
            sb.AppendLine("      </Row>");

            // Instruction Row
            sb.AppendLine("      <Row ss:Height=\"28\">");
            foreach (string inst in instructions)
            {
                sb.AppendLine(string.Format("        <Cell ss:StyleID=\"InstructionRow\"><Data ss:Type=\"String\">{0}</Data></Cell>", EscapeXml(inst)));
            }
            sb.AppendLine("      </Row>");

            // Sample Data Rows
            foreach (string[] row in sampleRows)
            {
                sb.AppendLine("      <Row ss:Height=\"20\">");
                for (int i = 0; i < row.Length; i++)
                {
                    string val = row[i];
                    if (entityType == ImportEntityType.SanPham && i == 5) // DonGiaBan
                    {
                        sb.AppendLine(string.Format("        <Cell ss:StyleID=\"CurrencyCell\"><Data ss:Type=\"Number\">{0}</Data></Cell>", val));
                    }
                    else
                    {
                        sb.AppendLine(string.Format("        <Cell ss:StyleID=\"DataCell\"><Data ss:Type=\"String\">{0}</Data></Cell>", EscapeXml(val)));
                    }
                }
                sb.AppendLine("      </Row>");
            }

            // Extra Reference Note for SanPham
            if (entityType == ImportEntityType.SanPham)
            {
                try
                {
                    SanPhamDAL spDal = new SanPhamDAL();
                    var loaiSet = spDal.GetAllMaLoai();
                    var nccSet = spDal.GetAllMaNCC();

                    sb.AppendLine("      <Row ss:Height=\"15\"></Row>");
                    sb.AppendLine("      <Row ss:Height=\"20\">");
                    sb.AppendLine(string.Format("        <Cell ss:MergeAcross=\"{0}\" ss:StyleID=\"InstructionRow\"><Data ss:Type=\"String\">* LƯU Ý TRA CỨU KHÓA NGOẠI: Mã loại và Mã NCC phải khớp với dữ liệu đã có trong CSDL.</Data></Cell>", colCount - 1));
                    sb.AppendLine("      </Row>");

                    if (loaiSet.Count > 0)
                    {
                        sb.AppendLine("      <Row ss:Height=\"20\">");
                        sb.AppendLine(string.Format("        <Cell ss:MergeAcross=\"{0}\" ss:StyleID=\"InstructionRow\"><Data ss:Type=\"String\">  - Danh sách Mã Loại SP hiện có: {1}</Data></Cell>",
                            colCount - 1, EscapeXml(string.Join(", ", loaiSet))));
                        sb.AppendLine("      </Row>");
                    }

                    if (nccSet.Count > 0)
                    {
                        sb.AppendLine("      <Row ss:Height=\"20\">");
                        sb.AppendLine(string.Format("        <Cell ss:MergeAcross=\"{0}\" ss:StyleID=\"InstructionRow\"><Data ss:Type=\"String\">  - Danh sách Mã NCC hiện có: {1}</Data></Cell>",
                            colCount - 1, EscapeXml(string.Join(", ", nccSet))));
                        sb.AppendLine("      </Row>");
                    }
                }
                catch { }
            }

            sb.AppendLine("    </Table>");
            sb.AppendLine("  </Worksheet>");
            sb.AppendLine("</Workbook>");

            return sb.ToString();
        }

        public static bool SaveTemplate(ImportEntityType entityType, string filePath, string companyName = null)
        {
            try
            {
                string xml = GenerateTemplateSpreadsheetMl(entityType, companyName);
                using (StreamWriter sw = new StreamWriter(filePath, false, new UTF8Encoding(true)))
                {
                    sw.Write(xml);
                }
                return true;
            }
            catch (Exception ex)
            {
                AppLogger.Error("IMPORT_TEMPLATE_ERROR", "Lỗi khi tạo tệp mẫu Excel: " + ex.Message, ex);
                return false;
            }
        }

        #endregion

        #region 3. Validation Engine

        /// <summary>
        /// Phân tích và kiểm tra tính hợp lệ toàn bộ các dòng dữ liệu đọc được từ tệp.
        /// </summary>
        public static ImportBatchResult ValidateBatch(ImportEntityType entityType, List<List<string>> rawRows)
        {
            ImportBatchResult batch = new ImportBatchResult
            {
                EntityType = entityType
            };

            if (rawRows == null || rawRows.Count == 0)
            {
                return batch;
            }

            // 1. Tìm dòng Header
            int headerRowIndex = FindHeaderRowIndex(entityType, rawRows);
            if (headerRowIndex < 0)
            {
                ImportRowResult errRow = new ImportRowResult
                {
                    RowNumber = 1,
                    IsValid = false
                };
                errRow.ErrorMessages.Add("Không nhận diện được dòng tiêu đề cột hợp lệ cho danh mục " + GetEntityTypeName(entityType) + ".");
                batch.Rows.Add(errRow);
                batch.TotalRows = 1;
                batch.ErrorCount = 1;
                return batch;
            }

            List<string> headerCells = rawRows[headerRowIndex];
            Dictionary<string, int> colMap = BuildColumnMapping(entityType, headerCells);

            // 2. Tải danh sách khóa đã tồn tại trong CSDL để đối chiếu
            HashSet<string> existingDbKeys = LoadExistingDbKeys(entityType);
            HashSet<string> existingLoaiKeys = entityType == ImportEntityType.SanPham ? LoadExistingLoaiKeys() : null;
            HashSet<string> existingNccKeys = entityType == ImportEntityType.SanPham ? LoadExistingNccKeys() : null;

            HashSet<string> seenKeysInFile = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            int dataIndex = 0;
            for (int r = headerRowIndex + 1; r < rawRows.Count; r++)
            {
                List<string> row = rawRows[r];

                // Bỏ qua dòng trống
                if (row == null || row.Count == 0 || row.All(c => string.IsNullOrWhiteSpace(c)))
                    continue;

                // Bỏ qua dòng ghi chú/hướng dẫn nếu có
                if (IsInstructionRow(row))
                    continue;

                dataIndex++;
                ImportRowResult rowResult = new ImportRowResult
                {
                    RowNumber = r + 1,
                    DataIndex = dataIndex,
                    IsValid = true
                };

                // Lưu dữ liệu thô theo tên trường
                foreach (var kv in colMap)
                {
                    string val = kv.Value < row.Count ? row[kv.Value] : string.Empty;
                    rowResult.RawValues[kv.Key] = val != null ? val.Trim() : string.Empty;
                }

                // Thực thi kiểm tra nghiệp vụ theo từng loại thực thể
                switch (entityType)
                {
                    case ImportEntityType.KhachHang:
                        ValidateKhachHangRow(rowResult, seenKeysInFile, existingDbKeys);
                        break;
                    case ImportEntityType.NhaCungCap:
                        ValidateNhaCungCapRow(rowResult, seenKeysInFile, existingDbKeys);
                        break;
                    case ImportEntityType.SanPham:
                        ValidateSanPhamRow(rowResult, seenKeysInFile, existingDbKeys, existingLoaiKeys, existingNccKeys);
                        break;
                }

                batch.Rows.Add(rowResult);
            }

            batch.TotalRows = batch.Rows.Count;
            batch.ValidCount = batch.Rows.Count(x => x.IsValid);
            batch.ErrorCount = batch.Rows.Count(x => !x.IsValid);

            return batch;
        }

        private static void ValidateKhachHangRow(ImportRowResult res, HashSet<string> seenKeys, HashSet<string> existingDbKeys)
        {
            string maKH = res.RawValues.ContainsKey("MaKH") ? res.RawValues["MaKH"] : string.Empty;
            string tenKH = res.RawValues.ContainsKey("TenKH") ? res.RawValues["TenKH"] : string.Empty;
            string sdt = res.RawValues.ContainsKey("SoDienThoai") ? res.RawValues["SoDienThoai"] : string.Empty;
            string diaChi = res.RawValues.ContainsKey("DiaChi") ? res.RawValues["DiaChi"] : string.Empty;
            string email = res.RawValues.ContainsKey("Email") ? res.RawValues["Email"] : string.Empty;

            // Kiểm tra Mã KH
            if (string.IsNullOrWhiteSpace(maKH))
            {
                res.ErrorMessages.Add("Mã khách hàng không được để trống");
                res.IsValid = false;
            }
            else
            {
                if (maKH.Length > 10)
                {
                    res.ErrorMessages.Add("Mã khách hàng vượt quá 10 ký tự (" + maKH.Length + ")");
                    res.IsValid = false;
                }

                if (seenKeys.Contains(maKH))
                {
                    res.ErrorMessages.Add(string.Format("Mã KH '{0}' bị trùng lặp trong tệp", maKH));
                    res.IsValid = false;
                }
                else
                {
                    seenKeys.Add(maKH);
                }

                if (existingDbKeys != null && existingDbKeys.Contains(maKH))
                {
                    res.ErrorMessages.Add(string.Format("Mã KH '{0}' đã tồn tại trong CSDL", maKH));
                    res.IsValid = false;
                }
            }

            // Kiểm tra Tên KH
            if (string.IsNullOrWhiteSpace(tenKH))
            {
                res.ErrorMessages.Add("Tên khách hàng không được để trống");
                res.IsValid = false;
            }
            else if (tenKH.Length > 100)
            {
                res.ErrorMessages.Add("Tên khách hàng vượt quá 100 ký tự");
                res.IsValid = false;
            }

            // Kiểm tra SĐT
            if (!string.IsNullOrWhiteSpace(sdt))
            {
                if (sdt.Length > 15)
                {
                    res.ErrorMessages.Add("Số điện thoại vượt quá 15 ký tự");
                    res.IsValid = false;
                }
                else if (!ValidationHelper.IsValidPhone(sdt))
                {
                    res.ErrorMessages.Add("Số điện thoại không đúng định dạng: " + sdt);
                    res.IsValid = false;
                }
            }

            // Kiểm tra Email
            if (!string.IsNullOrWhiteSpace(email))
            {
                if (email.Length > 100)
                {
                    res.ErrorMessages.Add("Email vượt quá 100 ký tự");
                    res.IsValid = false;
                }
                else if (!ValidationHelper.IsValidEmail(email))
                {
                    res.ErrorMessages.Add("Email không đúng định dạng: " + email);
                    res.IsValid = false;
                }
            }

            // Kiểm tra Địa chỉ
            if (!string.IsNullOrWhiteSpace(diaChi) && diaChi.Length > 200)
            {
                res.ErrorMessages.Add("Địa chỉ vượt quá 200 ký tự");
                res.IsValid = false;
            }

            if (res.IsValid)
            {
                res.ParsedEntity = new KhachHang
                {
                    MaKH = maKH.Trim(),
                    TenKH = tenKH.Trim(),
                    SoDienThoai = !string.IsNullOrWhiteSpace(sdt) ? sdt.Trim() : null,
                    DiaChi = !string.IsNullOrWhiteSpace(diaChi) ? diaChi.Trim() : null,
                    Email = !string.IsNullOrWhiteSpace(email) ? email.Trim() : null,
                    Version = 1
                };
            }
        }

        private static void ValidateNhaCungCapRow(ImportRowResult res, HashSet<string> seenKeys, HashSet<string> existingDbKeys)
        {
            string maNCC = res.RawValues.ContainsKey("MaNCC") ? res.RawValues["MaNCC"] : string.Empty;
            string tenNCC = res.RawValues.ContainsKey("TenNCC") ? res.RawValues["TenNCC"] : string.Empty;
            string diaChi = res.RawValues.ContainsKey("DiaChi") ? res.RawValues["DiaChi"] : string.Empty;
            string sdt = res.RawValues.ContainsKey("SoDienThoai") ? res.RawValues["SoDienThoai"] : string.Empty;
            string email = res.RawValues.ContainsKey("Email") ? res.RawValues["Email"] : string.Empty;

            if (string.IsNullOrWhiteSpace(maNCC))
            {
                res.ErrorMessages.Add("Mã nhà cung cấp không được để trống");
                res.IsValid = false;
            }
            else
            {
                if (maNCC.Length > 10)
                {
                    res.ErrorMessages.Add("Mã NCC vượt quá 10 ký tự (" + maNCC.Length + ")");
                    res.IsValid = false;
                }

                if (seenKeys.Contains(maNCC))
                {
                    res.ErrorMessages.Add(string.Format("Mã NCC '{0}' bị trùng lặp trong tệp", maNCC));
                    res.IsValid = false;
                }
                else
                {
                    seenKeys.Add(maNCC);
                }

                if (existingDbKeys != null && existingDbKeys.Contains(maNCC))
                {
                    res.ErrorMessages.Add(string.Format("Mã NCC '{0}' đã tồn tại trong CSDL", maNCC));
                    res.IsValid = false;
                }
            }

            if (string.IsNullOrWhiteSpace(tenNCC))
            {
                res.ErrorMessages.Add("Tên nhà cung cấp không được để trống");
                res.IsValid = false;
            }
            else if (tenNCC.Length > 100)
            {
                res.ErrorMessages.Add("Tên nhà cung cấp vượt quá 100 ký tự");
                res.IsValid = false;
            }

            if (!string.IsNullOrWhiteSpace(sdt))
            {
                if (sdt.Length > 15)
                {
                    res.ErrorMessages.Add("Số điện thoại vượt quá 15 ký tự");
                    res.IsValid = false;
                }
                else if (!ValidationHelper.IsValidPhone(sdt))
                {
                    res.ErrorMessages.Add("Số điện thoại không đúng định dạng: " + sdt);
                    res.IsValid = false;
                }
            }

            if (!string.IsNullOrWhiteSpace(email))
            {
                if (email.Length > 100)
                {
                    res.ErrorMessages.Add("Email vượt quá 100 ký tự");
                    res.IsValid = false;
                }
                else if (!ValidationHelper.IsValidEmail(email))
                {
                    res.ErrorMessages.Add("Email không đúng định dạng: " + email);
                    res.IsValid = false;
                }
            }

            if (!string.IsNullOrWhiteSpace(diaChi) && diaChi.Length > 200)
            {
                res.ErrorMessages.Add("Địa chỉ vượt quá 200 ký tự");
                res.IsValid = false;
            }

            if (res.IsValid)
            {
                res.ParsedEntity = new NhaCungCap
                {
                    MaNCC = maNCC.Trim(),
                    TenNCC = tenNCC.Trim(),
                    DiaChi = !string.IsNullOrWhiteSpace(diaChi) ? diaChi.Trim() : null,
                    SoDienThoai = !string.IsNullOrWhiteSpace(sdt) ? sdt.Trim() : null,
                    Email = !string.IsNullOrWhiteSpace(email) ? email.Trim() : null,
                    Version = 1
                };
            }
        }

        private static void ValidateSanPhamRow(ImportRowResult res, HashSet<string> seenKeys, HashSet<string> existingDbKeys,
            HashSet<string> existingLoaiKeys, HashSet<string> existingNccKeys)
        {
            string maSP = res.RawValues.ContainsKey("MaSP") ? res.RawValues["MaSP"] : string.Empty;
            string tenSP = res.RawValues.ContainsKey("TenSP") ? res.RawValues["TenSP"] : string.Empty;
            string maLoai = res.RawValues.ContainsKey("MaLoai") ? res.RawValues["MaLoai"] : string.Empty;
            string maNCC = res.RawValues.ContainsKey("MaNCC") ? res.RawValues["MaNCC"] : string.Empty;
            string dvt = res.RawValues.ContainsKey("DonViTinh") ? res.RawValues["DonViTinh"] : string.Empty;
            string giaStr = res.RawValues.ContainsKey("DonGiaBan") ? res.RawValues["DonGiaBan"] : string.Empty;
            string trangThai = res.RawValues.ContainsKey("TrangThai") ? res.RawValues["TrangThai"] : string.Empty;

            // Kiểm tra Mã SP
            if (string.IsNullOrWhiteSpace(maSP))
            {
                res.ErrorMessages.Add("Mã sản phẩm không được để trống");
                res.IsValid = false;
            }
            else
            {
                if (maSP.Length > 10)
                {
                    res.ErrorMessages.Add("Mã SP vượt quá 10 ký tự (" + maSP.Length + ")");
                    res.IsValid = false;
                }

                if (seenKeys.Contains(maSP))
                {
                    res.ErrorMessages.Add(string.Format("Mã SP '{0}' bị trùng lặp trong tệp", maSP));
                    res.IsValid = false;
                }
                else
                {
                    seenKeys.Add(maSP);
                }

                if (existingDbKeys != null && existingDbKeys.Contains(maSP))
                {
                    res.ErrorMessages.Add(string.Format("Mã SP '{0}' đã tồn tại trong CSDL", maSP));
                    res.IsValid = false;
                }
            }

            // Kiểm tra Tên SP
            if (string.IsNullOrWhiteSpace(tenSP))
            {
                res.ErrorMessages.Add("Tên sản phẩm không được để trống");
                res.IsValid = false;
            }
            else if (tenSP.Length > 150)
            {
                res.ErrorMessages.Add("Tên sản phẩm vượt quá 150 ký tự");
                res.IsValid = false;
            }

            // Kiểm tra Mã Loại (FK)
            if (string.IsNullOrWhiteSpace(maLoai))
            {
                res.ErrorMessages.Add("Mã loại sản phẩm không được để trống");
                res.IsValid = false;
            }
            else if (existingLoaiKeys != null && !existingLoaiKeys.Contains(maLoai))
            {
                res.ErrorMessages.Add(string.Format("Mã loại '{0}' không tồn tại trong CSDL", maLoai));
                res.IsValid = false;
            }

            // Kiểm tra Mã NCC (FK)
            if (string.IsNullOrWhiteSpace(maNCC))
            {
                res.ErrorMessages.Add("Mã nhà cung cấp không được để trống");
                res.IsValid = false;
            }
            else if (existingNccKeys != null && !existingNccKeys.Contains(maNCC))
            {
                res.ErrorMessages.Add(string.Format("Mã NCC '{0}' không tồn tại trong CSDL", maNCC));
                res.IsValid = false;
            }

            // Kiểm tra Đơn vị tính
            if (string.IsNullOrWhiteSpace(dvt))
            {
                res.ErrorMessages.Add("Đơn vị tính không được để trống");
                res.IsValid = false;
            }
            else if (dvt.Length > 30)
            {
                res.ErrorMessages.Add("Đơn vị tính vượt quá 30 ký tự");
                res.IsValid = false;
            }

            // Kiểm tra Đơn giá bán
            decimal donGiaBan = 0;
            if (string.IsNullOrWhiteSpace(giaStr))
            {
                res.ErrorMessages.Add("Đơn giá bán không được để trống");
                res.IsValid = false;
            }
            else
            {
                string cleanedGia = giaStr.Replace(",", "").Replace(".", "").Replace("₫", "").Replace("VND", "").Replace("VNĐ", "").Trim();
                if (!decimal.TryParse(cleanedGia, NumberStyles.Any, CultureInfo.InvariantCulture, out donGiaBan) &&
                    !decimal.TryParse(giaStr, NumberStyles.Any, CultureInfo.CurrentCulture, out donGiaBan))
                {
                    res.ErrorMessages.Add("Đơn giá bán không phải số hợp lệ: " + giaStr);
                    res.IsValid = false;
                }
                else if (donGiaBan < 0)
                {
                    res.ErrorMessages.Add("Đơn giá bán không được nhỏ hơn 0");
                    res.IsValid = false;
                }
            }

            // Trạng thái
            if (string.IsNullOrWhiteSpace(trangThai))
            {
                trangThai = EntityStatusConstants.Product.Active;
            }
            else
            {
                if (!EntityStatusConstants.Product.All.Any(s => string.Equals(s, trangThai, StringComparison.OrdinalIgnoreCase)))
                {
                    trangThai = EntityStatusConstants.Product.Active;
                }
            }

            if (res.IsValid)
            {
                res.ParsedEntity = new SanPham
                {
                    MaSP = maSP.Trim(),
                    TenSP = tenSP.Trim(),
                    MaLoai = maLoai.Trim(),
                    MaNCC = maNCC.Trim(),
                    DonViTinh = dvt.Trim(),
                    DonGiaBan = donGiaBan,
                    TrangThai = trangThai,
                    Version = 1
                };
            }
        }

        private static int FindHeaderRowIndex(ImportEntityType entityType, List<List<string>> rawRows)
        {
            for (int r = 0; r < Math.Min(rawRows.Count, 25); r++)
            {
                List<string> row = rawRows[r];
                if (row == null || row.Count < 2) continue;

                // Dòng tiêu đề cột hợp lệ phải có ít nhất 2 ô có nội dung
                int nonEmptyCount = row.Count(c => !string.IsNullOrWhiteSpace(c));
                if (nonEmptyCount < 2) continue;

                // Bỏ qua dòng hướng dẫn/chú thích
                if (IsInstructionRow(row)) continue;

                Dictionary<string, int> map = BuildColumnMapping(entityType, row);

                if (entityType == ImportEntityType.KhachHang)
                {
                    if (map.ContainsKey("MaKH") && map.ContainsKey("TenKH") && map["MaKH"] != map["TenKH"])
                    {
                        return r;
                    }
                }
                else if (entityType == ImportEntityType.NhaCungCap)
                {
                    if (map.ContainsKey("MaNCC") && map.ContainsKey("TenNCC") && map["MaNCC"] != map["TenNCC"])
                    {
                        return r;
                    }
                }
                else if (entityType == ImportEntityType.SanPham)
                {
                    if (map.ContainsKey("MaSP") && map.ContainsKey("TenSP") && map["MaSP"] != map["TenSP"])
                    {
                        return r;
                    }
                }
            }

            return -1;
        }

        private static Dictionary<string, int> BuildColumnMapping(ImportEntityType entityType, List<string> headerCells)
        {
            Dictionary<string, int> map = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

            for (int i = 0; i < headerCells.Count; i++)
            {
                string norm = NormalizeHeaderName(headerCells[i]);
                if (string.IsNullOrEmpty(norm)) continue;

                if (entityType == ImportEntityType.KhachHang)
                {
                    if (norm.Contains("makh") || norm.Contains("makhachhang") || norm.Contains("makhach") || norm.Contains("customercode") || norm.Contains("customerid") || norm == "ma" || norm == "code")
                        map["MaKH"] = i;
                    else if (norm.Contains("tenkh") || norm.Contains("tenkhachhang") || norm.Contains("tenkhach") || norm.Contains("customername") || norm.Contains("hoten") || norm == "ten" || norm == "name")
                        map["TenKH"] = i;
                    else if (norm.Contains("sdt") || norm.Contains("sodienthoai") || norm.Contains("dienthoai") || norm.Contains("phone") || norm.Contains("tel") || norm.Contains("didong"))
                        map["SoDienThoai"] = i;
                    else if (norm.Contains("diachi") || norm.Contains("address"))
                        map["DiaChi"] = i;
                    else if (norm.Contains("email") || norm.Contains("mail") || norm.Contains("thudientu"))
                        map["Email"] = i;
                }
                else if (entityType == ImportEntityType.NhaCungCap)
                {
                    if (norm.Contains("mancc") || norm.Contains("manhacungcap") || norm.Contains("suppliercode") || norm.Contains("supplierid") || norm.Contains("vendorid") || norm == "ma" || norm == "code")
                        map["MaNCC"] = i;
                    else if (norm.Contains("tenncc") || norm.Contains("tennhacungcap") || norm.Contains("nhacungcap") || norm.Contains("suppliername") || norm.Contains("vendorname") || norm == "ten" || norm == "name")
                        map["TenNCC"] = i;
                    else if (norm.Contains("diachi") || norm.Contains("address"))
                        map["DiaChi"] = i;
                    else if (norm.Contains("sdt") || norm.Contains("sodienthoai") || norm.Contains("dienthoai") || norm.Contains("phone") || norm.Contains("tel") || norm.Contains("didong"))
                        map["SoDienThoai"] = i;
                    else if (norm.Contains("email") || norm.Contains("mail") || norm.Contains("thudientu"))
                        map["Email"] = i;
                }
                else if (entityType == ImportEntityType.SanPham)
                {
                    if (norm.Contains("masp") || norm.Contains("masanpham") || norm.Contains("productcode") || norm.Contains("productid") || norm.Contains("itemcode") || norm.Contains("sku") || norm.Contains("mahang") || norm.Contains("mahh") || norm == "ma" || norm == "code")
                        map["MaSP"] = i;
                    else if (norm.Contains("tensp") || norm.Contains("tensanpham") || norm.Contains("productname") || norm.Contains("itemname") || norm.Contains("tenhang") || norm.Contains("tenhh") || norm == "ten" || norm == "name")
                        map["TenSP"] = i;
                    else if (norm.Contains("maloai") || norm.Contains("maloaisp") || norm.Contains("loaisp") || norm.Contains("category") || norm == "loai" || norm.Contains("nhomhang") || norm.Contains("manhom"))
                        map["MaLoai"] = i;
                    else if (norm.Contains("mancc") || norm.Contains("manhacungcap") || norm.Contains("nhacungcap") || norm.Contains("supplier") || norm == "ncc")
                        map["MaNCC"] = i;
                    else if (norm.Contains("donvitinh") || norm.Contains("dvt") || norm.Contains("unit"))
                        map["DonViTinh"] = i;
                    else if (norm.Contains("dongiaban") || norm.Contains("dongia") || norm.Contains("giaban") || norm.Contains("price") || norm == "gia")
                        map["DonGiaBan"] = i;
                    else if (norm.Contains("trangthai") || norm.Contains("status") || norm.Contains("tinhtrang"))
                        map["TrangThai"] = i;
                }
            }

            return map;
        }

        private static string NormalizeHeaderName(string raw)
        {
            if (string.IsNullOrEmpty(raw)) return string.Empty;
            string s = raw.ToLowerInvariant()
                .Replace(" ", "")
                .Replace("*", "")
                .Replace(":", "")
                .Replace("_", "")
                .Replace("-", "");

            // Bỏ dấu tiếng Việt
            string[] vietnameseSigns = new string[]
            {
                "aAeEoOuUiIdDyY",
                "áàạảãâấầậẩẫăắằặẳẵ",
                "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",
                "éèẹẻẽêếềệểễ",
                "ÉÈẸẺẼÊẾỀỆỂỄ",
                "óòọỏõôốồộổỗơớờợởỡ",
                "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",
                "úùụủũưứừựửữ",
                "ÚÙỤỦŨƯỨỪỰỬỮ",
                "íìịỉĩ",
                "ÍÌỊỈĨ",
                "đ",
                "Đ",
                "ýỳỵỷỹ",
                "ÝỲỴỶỸ"
            };

            for (int i = 1; i < vietnameseSigns.Length; i++)
            {
                for (int j = 0; j < vietnameseSigns[i].Length; j++)
                    s = s.Replace(vietnameseSigns[i][j], vietnameseSigns[0][i - 1]);
            }

            return s;
        }

        private static bool IsInstructionRow(List<string> row)
        {
            string first = row.FirstOrDefault(c => !string.IsNullOrWhiteSpace(c));
            if (string.IsNullOrEmpty(first)) return false;
            first = first.Trim();
            return first.StartsWith("(") || first.StartsWith("*") || first.StartsWith("-") ||
                   first.StartsWith("LƯU Ý", StringComparison.OrdinalIgnoreCase) ||
                   first.StartsWith("LUU Y", StringComparison.OrdinalIgnoreCase) ||
                   first.StartsWith("Ghi chú", StringComparison.OrdinalIgnoreCase);
        }

        private static HashSet<string> LoadExistingDbKeys(ImportEntityType entityType)
        {
            try
            {
                switch (entityType)
                {
                    case ImportEntityType.KhachHang:
                        return new KhachHangDAL().GetAllMaKH();
                    case ImportEntityType.NhaCungCap:
                        return new NhaCungCapDAL().GetAllMaNCC();
                    case ImportEntityType.SanPham:
                        return new SanPhamDAL().GetAllMaSP();
                }
            }
            catch (Exception ex)
            {
                AppLogger.Warn("IMPORT_LOAD_KEYS_FAIL", "Không thể tải danh sách khóa tồn tại: " + ex.Message);
            }
            return new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        }

        private static HashSet<string> LoadExistingLoaiKeys()
        {
            try
            {
                return new SanPhamDAL().GetAllMaLoai();
            }
            catch
            {
                return new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            }
        }

        private static HashSet<string> LoadExistingNccKeys()
        {
            try
            {
                return new SanPhamDAL().GetAllMaNCC();
            }
            catch
            {
                return new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            }
        }

        #endregion

        #region 4. Bulk Insert Execution

        /// <summary>
        /// Thực thi nhập hàng loạt các dòng hợp lệ vào CSDL qua Transaction.
        /// </summary>
        public static int ExecuteImport(ImportEntityType entityType, List<ImportRowResult> validRows, bool atomic = true)
        {
            if (validRows == null || validRows.Count == 0)
                return 0;

            string correlationId = Guid.NewGuid().ToString("N");
            string userId = SessionManager.CurrentUser != null ? SessionManager.CurrentUser.MaNV : "System";
            string role = SessionManager.CurrentUser != null ? SessionManager.CurrentUser.VaiTro : "None";
            DateTime startTime = DateTime.Now;

            int insertedCount = 0;

            try
            {
                switch (entityType)
                {
                    case ImportEntityType.KhachHang:
                        {
                            var list = validRows.Select(r => (KhachHang)r.ParsedEntity).Where(e => e != null).ToList();
                            KhachHangDAL dal = new KhachHangDAL();
                            insertedCount = dal.BulkInsert(list);
                            break;
                        }
                    case ImportEntityType.NhaCungCap:
                        {
                            var list = validRows.Select(r => (NhaCungCap)r.ParsedEntity).Where(e => e != null).ToList();
                            NhaCungCapDAL dal = new NhaCungCapDAL();
                            insertedCount = dal.BulkInsert(list);
                            break;
                        }
                    case ImportEntityType.SanPham:
                        {
                            var list = validRows.Select(r => (SanPham)r.ParsedEntity).Where(e => e != null).ToList();
                            SanPhamDAL dal = new SanPhamDAL();
                            insertedCount = dal.BulkInsert(list);
                            break;
                        }
                }

                long duration = (long)(DateTime.Now - startTime).TotalMilliseconds;
                AppLogger.Info(
                    "BULK_IMPORT_SUCCESS",
                    string.Format("Nhập thành công {0} bản ghi cho danh mục {1}", insertedCount, GetEntityTypeName(entityType)),
                    correlationId,
                    userId,
                    duration,
                    "BULK_IMPORT",
                    entityType.ToString(),
                    role);

                return insertedCount;
            }
            catch (Exception ex)
            {
                long duration = (long)(DateTime.Now - startTime).TotalMilliseconds;
                AppLogger.Error(
                    "BULK_IMPORT_FAILED",
                    string.Format("Lỗi khi nhập hàng loạt danh mục {0}: {1}", GetEntityTypeName(entityType), ex.Message),
                    ex,
                    correlationId,
                    userId,
                    duration,
                    "BULK_IMPORT",
                    entityType.ToString(),
                    role);

                throw;
            }
        }

        #endregion

        #region Helpers

        public static string GetEntityTypeName(ImportEntityType entityType)
        {
            switch (entityType)
            {
                case ImportEntityType.KhachHang: return "Khách hàng";
                case ImportEntityType.NhaCungCap: return "Nhà cung cấp";
                case ImportEntityType.SanPham: return "Sản phẩm";
                default: return entityType.ToString();
            }
        }

        public static string EscapeXml(string unescaped)
        {
            if (string.IsNullOrEmpty(unescaped)) return string.Empty;
            return unescaped
                .Replace("&", "&amp;")
                .Replace("<", "&lt;")
                .Replace(">", "&gt;")
                .Replace("\"", "&quot;")
                .Replace("'", "&apos;");
        }

        /// <summary>
        /// Tạo nút bấm "Nhập từ Excel" chuẩn giao diện WinForms, tự động mở form frmImportExcel khi click.
        /// </summary>
        public static Button CreateImportButton(Form parentForm, ImportEntityType entityType, Action onImportCompleted = null, string buttonText = "📥 Nhập Excel")
        {
            Button btn = new Button
            {
                Text = buttonText,
                AutoSize = true,
                Height = UiTheme.ButtonHeight
            };
            UiStyler.StyleButton(btn, UiButtonRole.Secondary);
            btn.Click += delegate
            {
                using (Forms.frmImportExcel frm = new Forms.frmImportExcel(entityType, true))
                {
                    if (frm.ShowDialog(parentForm) == DialogResult.OK)
                    {
                        if (onImportCompleted != null)
                        {
                            onImportCompleted();
                        }
                    }
                }
            };
            return btn;
        }

        /// <summary>
        /// Gắn thêm tùy chọn "Nhập từ Excel..." vào menu chuột phải của DataGridView.
        /// </summary>
        public static void AttachImportContextMenu(Form parentForm, DataGridView dgv, ImportEntityType entityType, Action onImportCompleted = null)
        {
            if (dgv == null) return;
            ContextMenuStrip menu = dgv.ContextMenuStrip;
            if (menu == null)
            {
                menu = new ContextMenuStrip
                {
                    Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point)
                };
                dgv.ContextMenuStrip = menu;
            }

            ToolStripMenuItem importItem = new ToolStripMenuItem("📥 Nhập dữ liệu từ Excel/CSV...");
            importItem.Click += delegate
            {
                using (Forms.frmImportExcel frm = new Forms.frmImportExcel(entityType, true))
                {
                    if (frm.ShowDialog(parentForm) == DialogResult.OK)
                    {
                        if (onImportCompleted != null)
                        {
                            onImportCompleted();
                        }
                    }
                }
            };
            menu.Items.Add(importItem);
        }

        #endregion
    }
}
