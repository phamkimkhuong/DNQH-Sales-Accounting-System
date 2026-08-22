using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace DNQH_KeToanBanHang.Helpers
{
    /// <summary>
    /// Lớp tiện ích hỗ trợ trích xuất dữ liệu từ DataGridView ra tệp tin CSV chuẩn UTF-8 (kèm BOM)
    /// Đảm bảo mở đúng tiếng Việt có dấu trên Microsoft Excel.
    /// </summary>
    public static class CsvExporter
    {
        private static bool _isSilentMode = false;

        /// <summary>
        /// Tạo một nút bấm "Xuất Excel" chuẩn style WinForms với sự kiện click tự động gọi xuất Excel chuyên nghiệp.
        /// </summary>
        public static Button CreateExportButton(DataGridView dgv, string defaultFileName, string reportTitle = null, string buttonText = "Xuất Excel")
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
                ExportDataGridViewToExcel(dgv, defaultFileName, reportTitle);
            };
            return btn;
        }

        /// <summary>
        /// Gắn ContextMenuStrip lên DataGridView để người dùng có thể nhấp chuột phải và xuất dữ liệu ra Excel (.xls) hoặc CSV (.csv).
        /// </summary>
        public static void AttachExportContextMenu(DataGridView dgv, string defaultFileName, string reportTitle = null)
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
            else if (menu.Items.Count > 0)
            {
                menu.Items.Add(new ToolStripSeparator());
            }

            ToolStripMenuItem exportExcelItem = new ToolStripMenuItem("📊 Xuất dữ liệu ra Excel (.xls)...");
            exportExcelItem.Click += delegate
            {
                ExportDataGridViewToExcel(dgv, defaultFileName, reportTitle);
            };
            menu.Items.Add(exportExcelItem);

            ToolStripMenuItem exportCsvItem = new ToolStripMenuItem("📄 Xuất dữ liệu ra CSV (.csv)...");
            exportCsvItem.Click += delegate
            {
                ExportDataGridViewToCsv(dgv, defaultFileName, reportTitle);
            };
            menu.Items.Add(exportCsvItem);
        }

        /// <summary>
        /// Xuất dữ liệu hiển thị từ DataGridView ra tệp Excel (.xls) chuẩn mở SpreadsheetML với kiểu dáng chuyên nghiệp.
        /// </summary>
        public static bool ExportDataGridViewToExcel(DataGridView dgv, string defaultFileName, string reportTitle = null, bool? silent = null)
        {
            return ExcelExporter.ExportDataGridViewToExcel(dgv, defaultFileName, reportTitle, silent);
        }

        /// <summary>
        /// Cờ chế độ im lặng (Silent Mode) dành riêng cho kiểm thử tự động (Automated Test / CI).
        /// Khi bật cờ này, CsvExporter sẽ không hiển thị bất kỳ hộp thoại MessageBox.Show nào để tránh treo tiến trình.
        /// </summary>
        public static bool IsSilentMode
        {
            get { return _isSilentMode; }
            set { _isSilentMode = value; }
        }

        /// <summary>
        /// Xuất nội dung hiển thị của DataGridView ra file CSV thông qua hộp thoại SaveFileDialog
        /// </summary>
        /// <param name="dgv">Bảng DataGridView chứa dữ liệu</param>
        /// <param name="defaultFileName">Tên file gợi ý mặc định</param>
        /// <param name="reportTitle">Tiêu đề báo cáo (nếu có)</param>
        /// <param name="silent">Nếu true, không hiển thị MessageBox.Show (mặc định lấy theo IsSilentMode)</param>
        /// <returns>True nếu xuất thành công, False nếu hủy hoặc lỗi</returns>
        public static bool ExportDataGridViewToCsv(DataGridView dgv, string defaultFileName, string reportTitle = null, bool? silent = null)
        {
            bool isSilent = silent.HasValue ? silent.Value : IsSilentMode;

            if (dgv == null || dgv.Rows.Count == 0)
            {
                if (!isSilent)
                {
                    MessageBox.Show("Không có dữ liệu trong bảng để xuất file CSV!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                return false;
            }

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "Tệp CSV (*.csv)|*.csv|Tất cả tệp (*.*)|*.*";
                sfd.FileName = string.Format("{0}_{1:yyyyMMdd_HHmmss}.csv", defaultFileName, DateTime.Now);
                sfd.Title = "Chọn vị trí lưu tệp CSV báo cáo";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        // Sử dụng UTF-8 with BOM (Byte Order Mark) để Excel nhận diện chuẩn tiếng Việt UTF-8
                        using (StreamWriter sw = new StreamWriter(sfd.FileName, false, new UTF8Encoding(true)))
                        {
                            if (!string.IsNullOrEmpty(reportTitle))
                            {
                                sw.WriteLine(EscapeCsv(reportTitle, true));
                                sw.WriteLine(string.Format("Thời điểm xuất: {0:dd/MM/yyyy HH:mm:ss}", DateTime.Now));
                                sw.WriteLine("");
                            }

                            // 1. Ghi dòng Tiêu đề cột (Headers)
                            List<string> headers = new List<string>();
                            for (int i = 0; i < dgv.Columns.Count; i++)
                            {
                                if (dgv.Columns[i].Visible)
                                {
                                    headers.Add(EscapeCsv(dgv.Columns[i].HeaderText, true));
                                }
                            }
                            sw.WriteLine(string.Join(",", headers.ToArray()));

                            // 2. Ghi từng dòng dữ liệu (Data Rows)
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
                                        cells.Add(EscapeCsv(text, val is string || val is char));
                                    }
                                }
                                sw.WriteLine(string.Join(",", cells.ToArray()));
                            }
                        }

                        string correlationId = Guid.NewGuid().ToString("N");
                        string userId = SessionManager.CurrentUser != null ? SessionManager.CurrentUser.MaNV : "System";
                        string role = SessionManager.CurrentUser != null ? SessionManager.CurrentUser.VaiTro : "None";
                        AppLogger.Info(
                            "EXPORT_CSV",
                            string.Format("Xuất tệp CSV thành công với {0} dòng", dgv.Rows.Count),
                            correlationId,
                            userId,
                            0,
                            "CSV",
                            Path.GetFileName(sfd.FileName),
                            role);
                        if (!isSilent)
                        {
                            MessageBox.Show(string.Format("Xuất dữ liệu thành công ra tệp:\n{0}", sfd.FileName),
                                            "Xuất CSV thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        return true;
                    }
                    catch (Exception ex)
                    {
                        AppLogger.Error("EXPORT_CSV", "Lỗi khi ghi tệp CSV: " + ex.Message, ex, entityType: "CSV", entityId: sfd.FileName);
                        if (!isSilent)
                        {
                            MessageBox.Show("Không thể xuất tệp CSV. Chi tiết đã được ghi vào nhật ký hệ thống.",
                                            "Lỗi xuất tệp", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        return false;
                    }
                }
            }

            return false;
        }

        internal static string EscapeCsv(string field, bool protectFormula)
        {
            if (string.IsNullOrEmpty(field)) return "\"\"";

            if (protectFormula)
            {
                string trimmed = field.TrimStart();
                if (trimmed.Length > 0 &&
                    (trimmed[0] == '=' || trimmed[0] == '+' || trimmed[0] == '-' || trimmed[0] == '@'))
                {
                    field = "'" + field;
                }
            }

            if (field.Contains(",") || field.Contains("\"") || field.Contains("\r") || field.Contains("\n"))
            {
                return "\"" + field.Replace("\"", "\"\"") + "\"";
            }
            return "\"" + field + "\"";
        }
    }
}
