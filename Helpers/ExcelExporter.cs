using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace DNQH_KeToanBanHang.Helpers
{
    /// <summary>
    /// Tiện ích xuất dữ liệu từ DataGridView ra tệp tin Excel chuyên nghiệp chuẩn SpreadsheetML (XML Spreadsheet 2003).
    /// - Không phụ thuộc Microsoft Office Interop (hoạt động độc lập, máy không cần cài Excel).
    /// - Không phụ thuộc thư viện bên ngoài (Zero 3rd-party dependencies).
    /// - Mở trực tiếp trên Microsoft Excel (2003 đến Office 365), WPS Office, LibreOffice Calc.
    /// - Tích hợp: Tiêu đề công ty nhận diện thương hiệu, kẻ khung viền (Borders), định dạng tiền tệ (#,##0 VNĐ),
    ///   dòng tổng cộng (Total Summary) nền vàng nhạt bôi đậm, tự động tính tổng và căn chỉnh cột.
    /// </summary>
    public static class ExcelExporter
    {
        public const string DefaultCompanyName = "CÔNG TY CỔ PHẦN THƯƠNG MẠI & DỊCH VỤ DNQH";
        public const string BrandColorHex = "#1E3A8A"; // Deep Navy Blue

        /// <summary>
        /// Cờ chế độ im lặng (Silent Mode) kế thừa từ CsvExporter hoặc đặt riêng cho kiểm thử tự động.
        /// </summary>
        public static bool IsSilentMode
        {
            get { return CsvExporter.IsSilentMode; }
            set { CsvExporter.IsSilentMode = value; }
        }

        /// <summary>
        /// Xuất dữ liệu hiển thị từ DataGridView ra tệp Excel (.xls / .xml) hoặc CSV thông qua SaveFileDialog.
        /// </summary>
        /// <param name="dgv">DataGridView chứa dữ liệu</param>
        /// <param name="defaultFileName">Tên tệp gợi ý ban đầu</param>
        /// <param name="reportTitle">Tiêu đề báo cáo</param>
        /// <param name="silent">Cờ im lặng (nếu true sẽ không bật MessageBox)</param>
        /// <returns>True nếu xuất thành công, False nếu hủy hoặc lỗi</returns>
        public static bool ExportDataGridViewToExcel(DataGridView dgv, string defaultFileName, string reportTitle = null, bool? silent = null)
        {
            bool isSilent = silent.HasValue ? silent.Value : IsSilentMode;

            if (dgv == null || dgv.Rows.Count == 0)
            {
                if (!isSilent)
                {
                    MessageBox.Show("Không có dữ liệu trong bảng để xuất tệp Excel!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                return false;
            }

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "Tệp Excel (*.xls)|*.xls|Tệp Excel XML (*.xml)|*.xml|Tệp CSV (*.csv)|*.csv|Tất cả tệp (*.*)|*.*";
                sfd.FileName = string.Format("{0}_{1:yyyyMMdd_HHmmss}.xls", defaultFileName, DateTime.Now);
                sfd.Title = "Chọn vị trí lưu tệp Excel báo cáo";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    string ext = Path.GetExtension(sfd.FileName).ToLowerInvariant();
                    if (ext == ".csv")
                    {
                        // Người dùng chủ động chọn đuôi CSV
                        return ExportToCsvDirect(dgv, sfd.FileName, reportTitle, isSilent);
                    }

                    try
                    {
                        string xmlContent = GenerateSpreadsheetMl(dgv, reportTitle, DefaultCompanyName);

                        // Ghi tệp với chuẩn UTF-8 kèm BOM để Excel nhận dạng tiếng Việt có dấu hoàn hảo
                        using (StreamWriter sw = new StreamWriter(sfd.FileName, false, new UTF8Encoding(true)))
                        {
                            sw.Write(xmlContent);
                        }

                        string correlationId = Guid.NewGuid().ToString("N");
                        string userId = SessionManager.CurrentUser != null ? SessionManager.CurrentUser.MaNV : "System";
                        string role = SessionManager.CurrentUser != null ? SessionManager.CurrentUser.VaiTro : "None";

                        AppLogger.Info(
                            "EXPORT_EXCEL",
                            string.Format("Xuất tệp Excel thành công với {0} dòng", dgv.Rows.Count),
                            correlationId,
                            userId,
                            0,
                            "EXCEL",
                            Path.GetFileName(sfd.FileName),
                            role);

                        if (!isSilent)
                        {
                            MessageBox.Show(string.Format("Xuất dữ liệu Excel chuyên nghiệp thành công ra tệp:\n{0}", sfd.FileName),
                                            "Xuất Excel thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        return true;
                    }
                    catch (Exception ex)
                    {
                        AppLogger.Error("EXPORT_EXCEL", "Lỗi khi ghi tệp Excel: " + ex.Message, ex, entityType: "EXCEL", entityId: sfd.FileName);
                        if (!isSilent)
                        {
                            MessageBox.Show("Không thể xuất tệp Excel. Chi tiết đã được ghi vào nhật ký hệ thống.",
                                            "Lỗi xuất tệp", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        return false;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Tạo nội dung tài liệu SpreadsheetML XML 2003 từ DataGridView.
        /// </summary>
        public static string GenerateSpreadsheetMl(DataGridView dgv, string reportTitle = null, string companyName = DefaultCompanyName)
        {
            if (dgv == null) return string.Empty;

            // 1. Lọc các cột hiển thị (Visible)
            List<DataGridViewColumn> visibleCols = new List<DataGridViewColumn>();
            for (int i = 0; i < dgv.Columns.Count; i++)
            {
                if (dgv.Columns[i].Visible)
                {
                    visibleCols.Add(dgv.Columns[i]);
                }
            }

            if (visibleCols.Count == 0) return string.Empty;

            int colCount = visibleCols.Count;

            // 2. Xác định kiểu hiển thị cho từng cột (Tiền tệ, Số lượng, Ngày tháng, Căn giữa, Văn bản thường)
            ColumnType[] colTypes = new ColumnType[colCount];
            decimal[] colSums = new decimal[colCount];
            bool hasAnySumColumn = false;
            int[] maxCharLens = new int[colCount];

            for (int c = 0; c < colCount; c++)
            {
                DataGridViewColumn col = visibleCols[c];
                string name = (col.Name ?? "").ToLowerInvariant();
                string header = (col.HeaderText ?? "").ToLowerInvariant();

                maxCharLens[c] = Math.Max(10, (col.HeaderText ?? "").Length);

                if (IsCurrencyColumn(name, header))
                {
                    colTypes[c] = ColumnType.Currency;
                    hasAnySumColumn = true;
                }
                else if (IsQuantityColumn(name, header))
                {
                    colTypes[c] = ColumnType.Quantity;
                    hasAnySumColumn = true;
                }
                else if (IsDateColumn(col, name, header))
                {
                    colTypes[c] = ColumnType.Date;
                }
                else if (IsCenterColumn(name, header))
                {
                    colTypes[c] = ColumnType.CenterText;
                }
                else
                {
                    colTypes[c] = ColumnType.Text;
                }
            }

            // 3. Quét dữ liệu để tính tổng và độ rộng cột
            List<object[]> rowValues = new List<object[]>();
            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (row.IsNewRow) continue;

                object[] rowData = new object[colCount];
                for (int c = 0; c < colCount; c++)
                {
                    object rawVal = row.Cells[visibleCols[c].Index].Value;
                    rowData[c] = rawVal;

                    if (rawVal != null && rawVal != DBNull.Value)
                    {
                        string strVal = rawVal.ToString();
                        if (strVal.Length > maxCharLens[c])
                        {
                            maxCharLens[c] = Math.Min(50, strVal.Length);
                        }

                        // Tính tổng nếu là cột tiền hoặc số lượng
                        if (colTypes[c] == ColumnType.Currency || colTypes[c] == ColumnType.Quantity)
                        {
                            decimal num;
                            if (TryParseNumeric(rawVal, out num))
                            {
                                colSums[c] += num;
                            }
                        }
                    }
                }
                rowValues.Add(rowData);
            }

            // 4. Xây dựng tài liệu XML SpreadsheetML
            StringBuilder sb = new StringBuilder(16384);
            sb.AppendLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            sb.AppendLine("<?mso-application progid=\"Excel.Sheet\"?>");
            sb.AppendLine("<Workbook xmlns=\"urn:schemas-microsoft-com:office:spreadsheet\"");
            sb.AppendLine(" xmlns:o=\"urn:schemas-microsoft-com:office:office\"");
            sb.AppendLine(" xmlns:x=\"urn:schemas-microsoft-com:office:excel\"");
            sb.AppendLine(" xmlns:ss=\"urn:schemas-microsoft-com:office:spreadsheet\"");
            sb.AppendLine(" xmlns:html=\"http://www.w3.org/TR/REC-html40\">");

            // Document Properties
            sb.AppendLine(" <DocumentProperties xmlns=\"urn:schemas-microsoft-com:office:office\">");
            sb.AppendLine(string.Format("  <Author>{0}</Author>", EscapeXml(SessionManager.CurrentUser != null ? SessionManager.CurrentUser.HoTen : "DNQH System")));
            sb.AppendLine(string.Format("  <Company>{0}</Company>", EscapeXml(companyName)));
            sb.AppendLine(string.Format("  <Title>{0}</Title>", EscapeXml(reportTitle ?? "Báo cáo")));
            sb.AppendLine(string.Format("  <Created>{0:yyyy-MM-ddTHH:mm:ssZ}</Created>", DateTime.UtcNow));
            sb.AppendLine(" </DocumentProperties>");

            // Styles
            AppendStyles(sb);

            // Worksheet & Table
            string sheetName = EscapeXml(string.IsNullOrEmpty(reportTitle) ? "DuLieu" : TruncateSheetName(reportTitle));
            sb.AppendLine(string.Format(" <Worksheet ss:Name=\"{0}\">", sheetName));
            sb.AppendLine("  <Table x:FullColumns=\"1\" x:FullRows=\"1\">");

            // Column Widths
            for (int c = 0; c < colCount; c++)
            {
                double width = Math.Max(75, Math.Min(280, maxCharLens[c] * 8.5 + 18));
                sb.AppendLine(string.Format("   <Column ss:Width=\"{0:0.#}\"/>", width.ToString(CultureInfo.InvariantCulture)));
            }

            int mergeAcross = Math.Max(0, colCount - 1);

            // Row 1: Company Name (Nhận diện thương hiệu)
            sb.AppendLine("   <Row ss:Height=\"22\">");
            sb.AppendLine(string.Format("    <Cell ss:MergeAcross=\"{0}\" ss:StyleID=\"CompanyHeader\"><Data ss:Type=\"String\">{1}</Data></Cell>",
                mergeAcross, EscapeXml(companyName)));
            sb.AppendLine("   </Row>");

            // Row 2: Report Title
            if (!string.IsNullOrEmpty(reportTitle))
            {
                sb.AppendLine("   <Row ss:Height=\"28\">");
                sb.AppendLine(string.Format("    <Cell ss:MergeAcross=\"{0}\" ss:StyleID=\"ReportTitle\"><Data ss:Type=\"String\">{1}</Data></Cell>",
                    mergeAcross, EscapeXml(reportTitle)));
                sb.AppendLine("   </Row>");
            }

            // Row 3: Metadata (Thời điểm xuất & Người lập)
            string userDisplay = SessionManager.CurrentUser != null
                ? string.Format("{0} ({1})", SessionManager.CurrentUser.HoTen ?? SessionManager.CurrentUser.TenDangNhap, SessionManager.CurrentUser.VaiTro)
                : "Hệ thống";
            string metaInfo = string.Format("Thời điểm xuất: {0:dd/MM/yyyy HH:mm:ss} | Người lập: {1}", DateTime.Now, userDisplay);
            sb.AppendLine("   <Row ss:Height=\"18\">");
            sb.AppendLine(string.Format("    <Cell ss:MergeAcross=\"{0}\" ss:StyleID=\"MetaHeader\"><Data ss:Type=\"String\">{1}</Data></Cell>",
                mergeAcross, EscapeXml(metaInfo)));
            sb.AppendLine("   </Row>");

            // Row 4: Empty space row
            sb.AppendLine("   <Row ss:Height=\"8\"/>");

            // Row 5: Table Header Row (Kẻ khung + nền xanh thương hiệu)
            sb.AppendLine("   <Row ss:Height=\"26\">");
            for (int c = 0; c < colCount; c++)
            {
                sb.AppendLine(string.Format("    <Cell ss:StyleID=\"TableHeader\"><Data ss:Type=\"String\">{0}</Data></Cell>",
                    EscapeXml(visibleCols[c].HeaderText)));
            }
            sb.AppendLine("   </Row>");

            // Data Rows (Kẻ khung + định dạng số + xen kẽ dòng)
            for (int r = 0; r < rowValues.Count; r++)
            {
                object[] row = rowValues[r];
                bool isAlt = (r % 2 == 1);
                sb.AppendLine("   <Row ss:Height=\"20\">");

                for (int c = 0; c < colCount; c++)
                {
                    object val = row[c];
                    ColumnType cType = colTypes[c];
                    AppendDataCell(sb, val, cType, isAlt);
                }

                sb.AppendLine("   </Row>");
            }

            // Total Summary Row (Nền vàng nhạt, chữ bôi đậm, viền đôi kế toán)
            if (rowValues.Count > 0)
            {
                sb.AppendLine("   <Row ss:Height=\"24\">");
                int labelColIndex = 0;
                string summaryTitle = hasAnySumColumn ? "TỔNG CỘNG" : string.Format("TỔNG SỐ BẢN GHI: {0}", rowValues.Count);

                for (int c = 0; c < colCount; c++)
                {
                    if (c == labelColIndex)
                    {
                        sb.AppendLine(string.Format("    <Cell ss:StyleID=\"SummaryLabel\"><Data ss:Type=\"String\">{0}</Data></Cell>", summaryTitle));
                    }
                    else if (colTypes[c] == ColumnType.Currency)
                    {
                        sb.AppendLine(string.Format("    <Cell ss:StyleID=\"SummaryCurrency\"><Data ss:Type=\"Number\">{0}</Data></Cell>",
                            colSums[c].ToString(CultureInfo.InvariantCulture)));
                    }
                    else if (colTypes[c] == ColumnType.Quantity)
                    {
                        sb.AppendLine(string.Format("    <Cell ss:StyleID=\"SummaryNumber\"><Data ss:Type=\"Number\">{0}</Data></Cell>",
                            colSums[c].ToString(CultureInfo.InvariantCulture)));
                    }
                    else
                    {
                        sb.AppendLine("    <Cell ss:StyleID=\"SummaryEmpty\"><Data ss:Type=\"String\"></Data></Cell>");
                    }
                }
                sb.AppendLine("   </Row>");
            }

            sb.AppendLine("  </Table>");

            // Worksheet Options
            sb.AppendLine("  <WorksheetOptions xmlns=\"urn:schemas-microsoft-com:office:excel\">");
            sb.AppendLine("   <PageSetup>");
            sb.AppendLine("    <Layout x:Orientation=\"Landscape\"/>");
            sb.AppendLine("   </PageSetup>");
            sb.AppendLine("   <Selected/>");
            sb.AppendLine("   <DisplayGridlines/>");
            sb.AppendLine("  </WorksheetOptions>");

            sb.AppendLine(" </Worksheet>");
            sb.AppendLine("</Workbook>");

            return sb.ToString();
        }

        private static void AppendDataCell(StringBuilder sb, object val, ColumnType type, bool isAlt)
        {
            if (val == null || val == DBNull.Value)
            {
                string style = isAlt ? "DataTextAlt" : "DataText";
                sb.AppendLine(string.Format("    <Cell ss:StyleID=\"{0}\"><Data ss:Type=\"String\"></Data></Cell>", style));
                return;
            }

            switch (type)
            {
                case ColumnType.Currency:
                    decimal curVal;
                    if (TryParseNumeric(val, out curVal))
                    {
                        string style = isAlt ? "DataCurrencyAlt" : "DataCurrency";
                        sb.AppendLine(string.Format("    <Cell ss:StyleID=\"{0}\"><Data ss:Type=\"Number\">{1}</Data></Cell>",
                            style, curVal.ToString(CultureInfo.InvariantCulture)));
                    }
                    else
                    {
                        string style = isAlt ? "DataTextAlt" : "DataText";
                        sb.AppendLine(string.Format("    <Cell ss:StyleID=\"{0}\"><Data ss:Type=\"String\">{1}</Data></Cell>",
                            style, EscapeXml(val.ToString())));
                    }
                    break;

                case ColumnType.Quantity:
                    decimal qVal;
                    if (TryParseNumeric(val, out qVal))
                    {
                        string style = isAlt ? "DataNumberAlt" : "DataNumber";
                        sb.AppendLine(string.Format("    <Cell ss:StyleID=\"{0}\"><Data ss:Type=\"Number\">{1}</Data></Cell>",
                            style, qVal.ToString(CultureInfo.InvariantCulture)));
                    }
                    else
                    {
                        string style = isAlt ? "DataTextAlt" : "DataText";
                        sb.AppendLine(string.Format("    <Cell ss:StyleID=\"{0}\"><Data ss:Type=\"String\">{1}</Data></Cell>",
                            style, EscapeXml(val.ToString())));
                    }
                    break;

                case ColumnType.Date:
                    string dateStyle = isAlt ? "DataDateAlt" : "DataDate";
                    string dateText;
                    if (val is DateTime)
                    {
                        DateTime dt = (DateTime)val;
                        dateText = dt.TimeOfDay.TotalSeconds == 0 ? dt.ToString("dd/MM/yyyy") : dt.ToString("dd/MM/yyyy HH:mm");
                    }
                    else
                    {
                        DateTime dt;
                        if (DateTime.TryParse(val.ToString(), out dt))
                        {
                            dateText = dt.TimeOfDay.TotalSeconds == 0 ? dt.ToString("dd/MM/yyyy") : dt.ToString("dd/MM/yyyy HH:mm");
                        }
                        else
                        {
                            dateText = val.ToString();
                        }
                    }
                    sb.AppendLine(string.Format("    <Cell ss:StyleID=\"{0}\"><Data ss:Type=\"String\">{1}</Data></Cell>",
                        dateStyle, EscapeXml(dateText)));
                    break;

                case ColumnType.CenterText:
                    string centerStyle = isAlt ? "DataCenterAlt" : "DataCenter";
                    sb.AppendLine(string.Format("    <Cell ss:StyleID=\"{0}\"><Data ss:Type=\"String\">{1}</Data></Cell>",
                        centerStyle, EscapeXml(val.ToString())));
                    break;

                default:
                    string textStyle = isAlt ? "DataTextAlt" : "DataText";
                    sb.AppendLine(string.Format("    <Cell ss:StyleID=\"{0}\"><Data ss:Type=\"String\">{1}</Data></Cell>",
                        textStyle, EscapeXml(val.ToString())));
                    break;
            }
        }

        private static void AppendStyles(StringBuilder sb)
        {
            sb.AppendLine(" <Styles>");

            // Default
            sb.AppendLine("  <Style ss:ID=\"Default\" ss:Name=\"Normal\">");
            sb.AppendLine("   <Alignment ss:Vertical=\"Center\"/>");
            sb.AppendLine("   <Borders/>");
            sb.AppendLine("   <Font ss:FontName=\"Segoe UI\" ss:Size=\"10\" ss:Color=\"#000000\"/>");
            sb.AppendLine("  </Style>");

            // Company Header
            sb.AppendLine("  <Style ss:ID=\"CompanyHeader\">");
            sb.AppendLine("   <Alignment ss:Horizontal=\"Left\" ss:Vertical=\"Center\"/>");
            sb.AppendLine("   <Font ss:FontName=\"Segoe UI\" ss:Size=\"11\" ss:Bold=\"1\" ss:Color=\"#1E3A8A\"/>");
            sb.AppendLine("  </Style>");

            // Report Title
            sb.AppendLine("  <Style ss:ID=\"ReportTitle\">");
            sb.AppendLine("   <Alignment ss:Horizontal=\"Left\" ss:Vertical=\"Center\"/>");
            sb.AppendLine("   <Font ss:FontName=\"Segoe UI\" ss:Size=\"15\" ss:Bold=\"1\" ss:Color=\"#0D47A1\"/>");
            sb.AppendLine("  </Style>");

            // Meta Header
            sb.AppendLine("  <Style ss:ID=\"MetaHeader\">");
            sb.AppendLine("   <Alignment ss:Horizontal=\"Left\" ss:Vertical=\"Center\"/>");
            sb.AppendLine("   <Font ss:FontName=\"Segoe UI\" ss:Size=\"9\" ss:Italic=\"1\" ss:Color=\"#4B5563\"/>");
            sb.AppendLine("  </Style>");

            // Table Header (Borders 1px, navy background, white text)
            sb.AppendLine("  <Style ss:ID=\"TableHeader\">");
            sb.AppendLine("   <Alignment ss:Horizontal=\"Center\" ss:Vertical=\"Center\" ss:WrapText=\"1\"/>");
            sb.AppendLine("   <Borders>");
            sb.AppendLine("    <Border ss:Position=\"Bottom\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\" ss:Color=\"#000000\"/>");
            sb.AppendLine("    <Border ss:Position=\"Left\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\" ss:Color=\"#000000\"/>");
            sb.AppendLine("    <Border ss:Position=\"Right\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\" ss:Color=\"#000000\"/>");
            sb.AppendLine("    <Border ss:Position=\"Top\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\" ss:Color=\"#000000\"/>");
            sb.AppendLine("   </Borders>");
            sb.AppendLine("   <Font ss:FontName=\"Segoe UI\" ss:Size=\"10\" ss:Bold=\"1\" ss:Color=\"#FFFFFF\"/>");
            sb.AppendLine("   <Interior ss:Color=\"#1E3A8A\" ss:Pattern=\"Solid\"/>");
            sb.AppendLine("  </Style>");

            // DataText & DataTextAlt
            AppendBorderedStyle(sb, "DataText", "Left", false, null, null);
            AppendBorderedStyle(sb, "DataTextAlt", "Left", false, null, "#F8FAFC");

            // DataCenter & DataCenterAlt
            AppendBorderedStyle(sb, "DataCenter", "Center", false, null, null);
            AppendBorderedStyle(sb, "DataCenterAlt", "Center", false, null, "#F8FAFC");

            // DataDate & DataDateAlt
            AppendBorderedStyle(sb, "DataDate", "Center", false, null, null);
            AppendBorderedStyle(sb, "DataDateAlt", "Center", false, null, "#F8FAFC");

            // DataNumber & DataNumberAlt
            AppendBorderedStyle(sb, "DataNumber", "Right", false, "#,##0", null);
            AppendBorderedStyle(sb, "DataNumberAlt", "Right", false, "#,##0", "#F8FAFC");

            // DataCurrency & DataCurrencyAlt
            AppendBorderedStyle(sb, "DataCurrency", "Right", false, "#,##0\\ &quot;VNĐ&quot;", null);
            AppendBorderedStyle(sb, "DataCurrencyAlt", "Right", false, "#,##0\\ &quot;VNĐ&quot;", "#F8FAFC");

            // Summary Styles (Borders: Top 1px, Bottom Double 3px, Yellow background #FEF3C7)
            AppendSummaryStyle(sb, "SummaryLabel", "Center", false, null);
            AppendSummaryStyle(sb, "SummaryEmpty", "Left", false, null);
            AppendSummaryStyle(sb, "SummaryNumber", "Right", false, "#,##0");
            AppendSummaryStyle(sb, "SummaryCurrency", "Right", true, "#,##0\\ &quot;VNĐ&quot;");

            sb.AppendLine(" </Styles>");
        }

        private static void AppendBorderedStyle(StringBuilder sb, string styleId, string align, bool bold, string numberFormat, string bgColor)
        {
            sb.AppendLine(string.Format("  <Style ss:ID=\"{0}\">", styleId));
            sb.AppendLine(string.Format("   <Alignment ss:Horizontal=\"{0}\" ss:Vertical=\"Center\"/>", align));
            sb.AppendLine("   <Borders>");
            sb.AppendLine("    <Border ss:Position=\"Bottom\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\" ss:Color=\"#D1D5DB\"/>");
            sb.AppendLine("    <Border ss:Position=\"Left\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\" ss:Color=\"#D1D5DB\"/>");
            sb.AppendLine("    <Border ss:Position=\"Right\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\" ss:Color=\"#D1D5DB\"/>");
            sb.AppendLine("    <Border ss:Position=\"Top\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\" ss:Color=\"#D1D5DB\"/>");
            sb.AppendLine("   </Borders>");
            sb.AppendLine(string.Format("   <Font ss:FontName=\"Segoe UI\" ss:Size=\"10\"{0}/>", bold ? " ss:Bold=\"1\"" : ""));
            if (!string.IsNullOrEmpty(bgColor))
            {
                sb.AppendLine(string.Format("   <Interior ss:Color=\"{0}\" ss:Pattern=\"Solid\"/>", bgColor));
            }
            if (!string.IsNullOrEmpty(numberFormat))
            {
                sb.AppendLine(string.Format("   <NumberFormat ss:Format=\"{0}\"/>", numberFormat));
            }
            sb.AppendLine("  </Style>");
        }

        private static void AppendSummaryStyle(StringBuilder sb, string styleId, string align, bool isAmberText, string numberFormat)
        {
            sb.AppendLine(string.Format("  <Style ss:ID=\"{0}\">", styleId));
            sb.AppendLine(string.Format("   <Alignment ss:Horizontal=\"{0}\" ss:Vertical=\"Center\"/>", align));
            sb.AppendLine("   <Borders>");
            sb.AppendLine("    <Border ss:Position=\"Bottom\" ss:LineStyle=\"Double\" ss:Weight=\"3\" ss:Color=\"#000000\"/>");
            sb.AppendLine("    <Border ss:Position=\"Left\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\" ss:Color=\"#D1D5DB\"/>");
            sb.AppendLine("    <Border ss:Position=\"Right\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\" ss:Color=\"#D1D5DB\"/>");
            sb.AppendLine("    <Border ss:Position=\"Top\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\" ss:Color=\"#000000\"/>");
            sb.AppendLine("   </Borders>");
            string fontColor = isAmberText ? " ss:Color=\"#92400E\"" : "";
            sb.AppendLine(string.Format("   <Font ss:FontName=\"Segoe UI\" ss:Size=\"10\" ss:Bold=\"1\"{0}/>", fontColor));
            sb.AppendLine("   <Interior ss:Color=\"#FEF3C7\" ss:Pattern=\"Solid\"/>");
            if (!string.IsNullOrEmpty(numberFormat))
            {
                sb.AppendLine(string.Format("   <NumberFormat ss:Format=\"{0}\"/>", numberFormat));
            }
            sb.AppendLine("  </Style>");
        }

        private static bool IsCurrencyColumn(string name, string header)
        {
            string[] keywords = new string[]
            {
                "tien", "tiền", "gia", "giá", "no", "nợ", "thu", "chi", "vat", "thue", "thuế",
                "tri gia", "trị giá", "phi", "phí", "chietkhau", "chiết khấu", "thanhtien", "thành tiền",
                "dongia", "đơn giá", "tongtien", "tổng tiền", "dathanhtoan", "đã thanh toán",
                "conno", "còn nợ", "doanh thu", "doanhthu", "phatsinh", "phát sinh", "sotien", "số tiền",
                "tronghan", "trong hạn", "quahan", "quá hạn"
            };

            foreach (string kw in keywords)
            {
                if (name.Contains(kw) || header.Contains(kw))
                    return true;
            }
            return false;
        }

        private static bool IsQuantityColumn(string name, string header)
        {
            string[] keywords = new string[]
            {
                "soluong", "số lượng", "sl", "ton", "tồn", "tonkho", "tồn kho", "soluongton"
            };

            foreach (string kw in keywords)
            {
                if (name.Contains(kw) || header.Contains(kw))
                    return true;
            }
            return false;
        }

        private static bool IsDateColumn(DataGridViewColumn col, string name, string header)
        {
            if (col.ValueType == typeof(DateTime)) return true;

            string[] keywords = new string[] { "ngay", "ngày", "date", "time", "thoidiem", "thời điểm" };
            foreach (string kw in keywords)
            {
                if (name.Contains(kw) || header.Contains(kw))
                    return true;
            }
            return false;
        }

        private static bool IsCenterColumn(string name, string header)
        {
            string[] keywords = new string[] { "ma", "mã", "stt", "trangthai", "trạng thái", "pttt", "loai", "loại" };
            foreach (string kw in keywords)
            {
                if (name.Contains(kw) || header.Contains(kw))
                    return true;
            }
            return false;
        }

        public static bool TryParseNumeric(object val, out decimal result)
        {
            result = 0;
            if (val == null || val == DBNull.Value) return false;

            if (val is decimal) { result = (decimal)val; return true; }
            if (val is double) { result = Convert.ToDecimal((double)val); return true; }
            if (val is float) { result = Convert.ToDecimal((float)val); return true; }
            if (val is int) { result = (int)val; return true; }
            if (val is long) { result = (long)val; return true; }
            if (val is short) { result = (short)val; return true; }

            string str = val.ToString().Trim();
            if (string.IsNullOrEmpty(str)) return false;

            // Xóa tiền tệ và khoảng trắng: "1,500,000 VNĐ" -> "1500000"
            str = Regex.Replace(str, @"[^\d,.\-+]", "");

            if (decimal.TryParse(str, NumberStyles.Any, CultureInfo.InvariantCulture, out result))
                return true;

            if (decimal.TryParse(str, NumberStyles.Any, new CultureInfo("vi-VN"), out result))
                return true;

            return false;
        }

        public static string EscapeXml(string text)
        {
            if (string.IsNullOrEmpty(text)) return string.Empty;

            StringBuilder sb = new StringBuilder(text.Length + 16);
            for (int i = 0; i < text.Length; i++)
            {
                char ch = text[i];
                // Loại bỏ control characters không hợp lệ trong XML 1.0
                if (ch < 32 && ch != '\t' && ch != '\r' && ch != '\n')
                {
                    continue;
                }

                switch (ch)
                {
                    case '&': sb.Append("&amp;"); break;
                    case '<': sb.Append("&lt;"); break;
                    case '>': sb.Append("&gt;"); break;
                    case '"': sb.Append("&quot;"); break;
                    case '\'': sb.Append("&apos;"); break;
                    default: sb.Append(ch); break;
                }
            }
            return sb.ToString();
        }

        private static string TruncateSheetName(string name)
        {
            if (string.IsNullOrEmpty(name)) return "Sheet1";
            string clean = Regex.Replace(name, @"[\\/*?:\[\]]", "");
            return clean.Length > 31 ? clean.Substring(0, 31) : clean;
        }

        private static bool ExportToCsvDirect(DataGridView dgv, string filePath, string reportTitle, bool isSilent)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(filePath, false, new UTF8Encoding(true)))
                {
                    if (!string.IsNullOrEmpty(reportTitle))
                    {
                        sw.WriteLine(CsvExporter.EscapeCsv(reportTitle, true));
                        sw.WriteLine(string.Format("Thời điểm xuất: {0:dd/MM/yyyy HH:mm:ss}", DateTime.Now));
                        sw.WriteLine("");
                    }

                    List<string> headers = new List<string>();
                    for (int i = 0; i < dgv.Columns.Count; i++)
                    {
                        if (dgv.Columns[i].Visible)
                        {
                            headers.Add(CsvExporter.EscapeCsv(dgv.Columns[i].HeaderText, true));
                        }
                    }
                    sw.WriteLine(string.Join(",", headers.ToArray()));

                    foreach (DataGridViewRow row in dgv.Rows)
                    {
                        if (row.IsNewRow) continue;

                        List<string> cells = new List<string>();
                        for (int i = 0; i < dgv.Columns.Count; i++)
                        {
                            if (dgv.Columns[i].Visible)
                            {
                                object val = row.Cells[i].Value;
                                string text = "";
                                if (val != null && val != DBNull.Value)
                                {
                                    if (val is DateTime)
                                    {
                                        text = ((DateTime)val).ToString("dd/MM/yyyy HH:mm");
                                    }
                                    else if (val is decimal || val is double || val is float)
                                    {
                                        text = Convert.ToDecimal(val).ToString("0.##", CultureInfo.InvariantCulture);
                                    }
                                    else
                                    {
                                        text = val.ToString();
                                    }
                                }
                                cells.Add(CsvExporter.EscapeCsv(text, val is string || val is char));
                            }
                        }
                        sw.WriteLine(string.Join(",", cells.ToArray()));
                    }
                }

                if (!isSilent)
                {
                    MessageBox.Show(string.Format("Xuất dữ liệu thành công ra tệp CSV:\n{0}", filePath),
                                    "Xuất CSV thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                return true;
            }
            catch (Exception ex)
            {
                AppLogger.Error("EXPORT_CSV", "Lỗi khi ghi tệp CSV: " + ex.Message, ex, entityType: "CSV", entityId: filePath);
                if (!isSilent)
                {
                    MessageBox.Show("Không thể xuất tệp CSV.", "Lỗi xuất tệp", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return false;
            }
        }

        private enum ColumnType
        {
            Text,
            CenterText,
            Date,
            Quantity,
            Currency
        }
    }
}
