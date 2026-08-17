using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using DNQH_KeToanBanHang.Helpers;
using DNQH_KeToanBanHang.Models;

namespace DNQH_KeToanBanHang.Forms
{
    public partial class frmKeToanChiTiet
    {
        private TabPage tabTuoiNo;
        private Panel pnlFilterTuoiNo;
        private Label lblNgayChot;
        private DateTimePicker dtpNgayChot;
        private Label lblKhachHangTuoiNo;
        private ComboBox cboKhachHangTuoiNo;
        private Label lblCheDoXem;
        private ComboBox cboCheDoXem;
        private Label lblLocRuiRo;
        private ComboBox cboLocRuiRo;
        private Button btnXemTuoiNo;
        private bool _isLoadingTuoiNo;
        private Button btnXuatCsvTuoiNo;
        private Button btnInTuoiNo;
        private DataGridView dgvTuoiNo;
        private Panel pnlSummaryTuoiNo;
        private Label _tuoiNoStatus;
        private Label lblTongNoPhaiThu;
        private Label lblTrongHan;
        private Label lblQuaHan3160;
        private Label lblQuaHan6190;
        private Label lblKhoDoi;

        private List<BaoCaoTuoiNoTongHopDTO> _currentTuoiNoTongHop;
        private List<BaoCaoTuoiNoChiTietDTO> _currentTuoiNoChiTiet;

        private void InitializeTuoiNoComponents()
        {
            tabTuoiNo = new TabPage { Text = "4. Tuổi nợ & Quá hạn", Padding = new Padding(4) };
            pnlFilterTuoiNo = new Panel { Dock = DockStyle.Top, Height = 48, Padding = new Padding(8, 6, 8, 6) };

            lblNgayChot = new Label { Text = "Ngày chốt:", AutoSize = true, Margin = new Padding(0, 7, 4, 0) };
            dtpNgayChot = new DateTimePicker { Format = DateTimePickerFormat.Short, Value = DateTime.Today, Width = 110 };

            lblKhachHangTuoiNo = new Label { Text = "Khách hàng:", AutoSize = true, Margin = new Padding(8, 7, 4, 0) };
            cboKhachHangTuoiNo = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Width = 190 };

            lblCheDoXem = new Label { Text = "Chế độ xem:", AutoSize = true, Margin = new Padding(8, 7, 4, 0) };
            cboCheDoXem = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Width = 160 };
            cboCheDoXem.Items.AddRange(new object[] { "Tổng hợp theo Khách", "Chi tiết theo Hóa đơn" });
            cboCheDoXem.SelectedIndex = 0;
            cboCheDoXem.SelectedIndexChanged += delegate { InitTuoiNoGridColumns(); };

            lblLocRuiRo = new Label { Text = "Lọc rủi ro:", AutoSize = true, Margin = new Padding(8, 7, 4, 0) };
            cboLocRuiRo = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Width = 160 };
            cboLocRuiRo.Items.AddRange(new object[] { "Tất cả công nợ", "Có nợ quá hạn (>30 ngày)", "Chỉ nợ khó đòi (>90 ngày)" });
            cboLocRuiRo.SelectedIndex = 0;

            btnXemTuoiNo = new Button { Text = "Tra Cứu", Width = 95 };
            btnXemTuoiNo.Click += async delegate { await LoadBaoCaoTuoiNoAsync(btnXemTuoiNo); };

            btnXuatCsvTuoiNo = new Button { Text = "📄 Xuất CSV", Width = 100 };
            btnXuatCsvTuoiNo.Click += btnXuatCsvTuoiNo_Click;

            btnInTuoiNo = new Button { Text = "🖨️ In báo cáo", Width = 105 };
            btnInTuoiNo.Click += btnInTuoiNo_Click;

            dgvTuoiNo = new DataGridView
            {
                Dock = DockStyle.Fill,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                RowHeadersVisible = false,
                EnableHeadersVisualStyles = false
            };
            dgvTuoiNo.RowPrePaint += dgvTuoiNo_RowPrePaint;

            pnlSummaryTuoiNo = new Panel { Dock = DockStyle.Bottom, Height = 44, Padding = new Padding(8, 6, 8, 6) };
            lblTongNoPhaiThu = new Label { AutoSize = true, Text = "Tổng nợ: 0 VNĐ", Margin = new Padding(0, 0, 16, 0), Font = new Font("Segoe UI", 9F, FontStyle.Bold) };
            lblTrongHan = new Label { AutoSize = true, Text = "Trong hạn (<=30): 0 VNĐ", Margin = new Padding(0, 0, 16, 0), ForeColor = Color.FromArgb(21, 128, 61) };
            lblQuaHan3160 = new Label { AutoSize = true, Text = "Quá hạn 31-60: 0 VNĐ", Margin = new Padding(0, 0, 16, 0), ForeColor = Color.FromArgb(161, 98, 7) };
            lblQuaHan6190 = new Label { AutoSize = true, Text = "Quá hạn 61-90: 0 VNĐ", Margin = new Padding(0, 0, 16, 0), ForeColor = Color.FromArgb(194, 65, 12) };
            lblKhoDoi = new Label { AutoSize = true, Text = "Khó đòi (>90): 0 VNĐ", Margin = new Padding(0), ForeColor = Color.FromArgb(185, 28, 28), Font = new Font("Segoe UI", 9F, FontStyle.Bold) };

            _tuoiNoStatus = UiLayoutBuilder.BuildReportPage(
                tabTuoiNo,
                pnlFilterTuoiNo,
                new Control[] { lblNgayChot, dtpNgayChot, lblKhachHangTuoiNo, cboKhachHangTuoiNo, lblCheDoXem, cboCheDoXem, lblLocRuiRo, cboLocRuiRo, btnXemTuoiNo, btnXuatCsvTuoiNo, btnInTuoiNo },
                dgvTuoiNo,
                pnlSummaryTuoiNo,
                new Control[] { lblTongNoPhaiThu, lblTrongHan, lblQuaHan3160, lblQuaHan6190, lblKhoDoi },
                "Chọn ngày chốt và tiêu chí để phân tích tuổi nợ khách hàng.");

            if (!tabControlMain.TabPages.Contains(tabTuoiNo))
            {
                tabControlMain.TabPages.Add(tabTuoiNo);
            }
        }

        private void ApplyTuoiNoStyles()
        {
            if (btnXemTuoiNo != null) UiStyler.StyleButton(btnXemTuoiNo, UiButtonRole.Primary);
            if (btnXuatCsvTuoiNo != null) UiStyler.StyleButton(btnXuatCsvTuoiNo, UiButtonRole.Success);
            if (btnInTuoiNo != null) UiStyler.StyleButton(btnInTuoiNo, UiButtonRole.Secondary);
            if (pnlFilterTuoiNo != null && !SystemInformation.HighContrast) pnlFilterTuoiNo.BackColor = UiTheme.Surface;
            if (pnlSummaryTuoiNo != null && !SystemInformation.HighContrast) pnlSummaryTuoiNo.BackColor = UiTheme.SurfaceMuted;
        }

        private void InitTuoiNoGridColumns()
        {
            if (dgvTuoiNo == null) return;

            dgvTuoiNo.Columns.Clear();
            dgvTuoiNo.AutoGenerateColumns = false;
            dgvTuoiNo.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            bool isTongHop = (cboCheDoXem == null || cboCheDoXem.SelectedIndex <= 0);

            if (isTongHop)
            {
                dgvTuoiNo.Columns.Add(new DataGridViewTextBoxColumn { Name = "MaKH", HeaderText = "Mã KH", AutoSizeMode = DataGridViewAutoSizeColumnMode.None, Width = 85, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter } });
                dgvTuoiNo.Columns.Add(new DataGridViewTextBoxColumn { Name = "TenKH", HeaderText = "Tên Khách Hàng", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill, FillWeight = 160F, MinimumWidth = 150 });
                dgvTuoiNo.Columns.Add(new DataGridViewTextBoxColumn { Name = "DienThoai", HeaderText = "Điện Thoại", AutoSizeMode = DataGridViewAutoSizeColumnMode.None, Width = 100, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter } });
                dgvTuoiNo.Columns.Add(new DataGridViewTextBoxColumn { Name = "TongNo", HeaderText = "Tổng Dư Nợ", AutoSizeMode = DataGridViewAutoSizeColumnMode.None, Width = 130, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N0", Font = new Font("Segoe UI", 9F, FontStyle.Bold) } });
                dgvTuoiNo.Columns.Add(new DataGridViewTextBoxColumn { Name = "TrongHan", HeaderText = "Trong Hạn (0-30)", AutoSizeMode = DataGridViewAutoSizeColumnMode.None, Width = 125, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N0", ForeColor = Color.FromArgb(21, 128, 61) } });
                dgvTuoiNo.Columns.Add(new DataGridViewTextBoxColumn { Name = "QuaHan3160", HeaderText = "Quá Hạn 31-60", AutoSizeMode = DataGridViewAutoSizeColumnMode.None, Width = 120, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N0", ForeColor = Color.FromArgb(161, 98, 7) } });
                dgvTuoiNo.Columns.Add(new DataGridViewTextBoxColumn { Name = "QuaHan6190", HeaderText = "Quá Hạn 61-90", AutoSizeMode = DataGridViewAutoSizeColumnMode.None, Width = 120, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N0", ForeColor = Color.FromArgb(194, 65, 12) } });
                dgvTuoiNo.Columns.Add(new DataGridViewTextBoxColumn { Name = "KhoDoi", HeaderText = "Khó Đòi (>90)", AutoSizeMode = DataGridViewAutoSizeColumnMode.None, Width = 125, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N0", ForeColor = Color.FromArgb(185, 28, 28), Font = new Font("Segoe UI", 9F, FontStyle.Bold) } });
                dgvTuoiNo.Columns.Add(new DataGridViewTextBoxColumn { Name = "DanhGia", HeaderText = "Đánh Giá Rủi Ro", AutoSizeMode = DataGridViewAutoSizeColumnMode.None, Width = 150, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter } });
            }
            else
            {
                dgvTuoiNo.Columns.Add(new DataGridViewTextBoxColumn { Name = "MaHDB", HeaderText = "Mã Hóa Đơn", AutoSizeMode = DataGridViewAutoSizeColumnMode.None, Width = 100, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter, Font = new Font("Segoe UI", 9F, FontStyle.Bold) } });
                dgvTuoiNo.Columns.Add(new DataGridViewTextBoxColumn { Name = "NgayLap", HeaderText = "Ngày Lập", AutoSizeMode = DataGridViewAutoSizeColumnMode.None, Width = 95, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter, Format = "dd/MM/yyyy" } });
                dgvTuoiNo.Columns.Add(new DataGridViewTextBoxColumn { Name = "MaKH", HeaderText = "Mã KH", AutoSizeMode = DataGridViewAutoSizeColumnMode.None, Width = 80, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter } });
                dgvTuoiNo.Columns.Add(new DataGridViewTextBoxColumn { Name = "TenKH", HeaderText = "Tên Khách Hàng", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill, FillWeight = 160F, MinimumWidth = 140 });
                dgvTuoiNo.Columns.Add(new DataGridViewTextBoxColumn { Name = "TongTien", HeaderText = "Tổng Tiền HĐ", AutoSizeMode = DataGridViewAutoSizeColumnMode.None, Width = 115, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N0" } });
                dgvTuoiNo.Columns.Add(new DataGridViewTextBoxColumn { Name = "DaThu", HeaderText = "Đã Thanh Toán", AutoSizeMode = DataGridViewAutoSizeColumnMode.None, Width = 115, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N0" } });
                dgvTuoiNo.Columns.Add(new DataGridViewTextBoxColumn { Name = "ConNo", HeaderText = "Còn Nợ (VND)", AutoSizeMode = DataGridViewAutoSizeColumnMode.None, Width = 125, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N0", Font = new Font("Segoe UI", 9F, FontStyle.Bold) } });
                dgvTuoiNo.Columns.Add(new DataGridViewTextBoxColumn { Name = "SoNgayNo", HeaderText = "Tuổi Nợ", AutoSizeMode = DataGridViewAutoSizeColumnMode.None, Width = 85, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter, Font = new Font("Segoe UI", 9F, FontStyle.Bold) } });
                dgvTuoiNo.Columns.Add(new DataGridViewTextBoxColumn { Name = "NhomTuoiNo", HeaderText = "Phân Nhóm Tuổi Nợ", AutoSizeMode = DataGridViewAutoSizeColumnMode.None, Width = 160, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter } });
            }
        }

        private void dgvTuoiNo_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvTuoiNo.Rows.Count) return;

            DataGridViewRow row = dgvTuoiNo.Rows[e.RowIndex];
            bool isTongHop = (cboCheDoXem == null || cboCheDoXem.SelectedIndex <= 0);

            if (isTongHop)
            {
                if (_currentTuoiNoTongHop != null && e.RowIndex < _currentTuoiNoTongHop.Count)
                {
                    var item = _currentTuoiNoTongHop[e.RowIndex];
                    if (item.KhoDoi_Tren90 > 0)
                    {
                        row.DefaultCellStyle.BackColor = Color.FromArgb(254, 226, 226); // Red light
                    }
                    else if (item.QuaHanTB_61_90 > 0)
                    {
                        row.DefaultCellStyle.BackColor = Color.FromArgb(255, 237, 213); // Orange light
                    }
                    else if (item.QuaHanNhe_31_60 > 0)
                    {
                        row.DefaultCellStyle.BackColor = Color.FromArgb(254, 249, 195); // Yellow light
                    }
                }
            }
            else
            {
                if (_currentTuoiNoChiTiet != null && e.RowIndex < _currentTuoiNoChiTiet.Count)
                {
                    var item = _currentTuoiNoChiTiet[e.RowIndex];
                    if (item.MaNhomTuoiNo == 3)
                    {
                        row.DefaultCellStyle.BackColor = Color.FromArgb(254, 226, 226);
                    }
                    else if (item.MaNhomTuoiNo == 2)
                    {
                        row.DefaultCellStyle.BackColor = Color.FromArgb(255, 237, 213);
                    }
                    else if (item.MaNhomTuoiNo == 1)
                    {
                        row.DefaultCellStyle.BackColor = Color.FromArgb(254, 249, 195);
                    }
                }
            }
        }

        private async Task<bool> LoadBaoCaoTuoiNoAsync(Button actionButton)
        {
            if (_isLoadingTuoiNo) return false;
            _isLoadingTuoiNo = true;
            try
            {
                DateTime ngayChot = dtpNgayChot.Value;
                string maKH = (cboKhachHangTuoiNo.SelectedValue != null && !string.IsNullOrEmpty(cboKhachHangTuoiNo.SelectedValue.ToString()))
                    ? cboKhachHangTuoiNo.SelectedValue.ToString() : null;

                int? filterRuiRo = null;
                if (cboLocRuiRo.SelectedIndex == 1) filterRuiRo = 1; // Quá hạn >30
                else if (cboLocRuiRo.SelectedIndex == 2) filterRuiRo = 2; // Khó đòi >90

                bool isTongHop = (cboCheDoXem.SelectedIndex <= 0);

                UiStyler.SetGridLoading(dgvTuoiNo, "Đang phân tích dữ liệu tuổi nợ...");
                UiStyler.StyleStatusLabel(_tuoiNoStatus, UiStatusKind.Information, "Đang phân tích tuổi nợ và phân nhóm rủi ro...");

                string err = string.Empty;
                InitTuoiNoGridColumns();

                if (isTongHop)
                {
                    List<BaoCaoTuoiNoTongHopDTO> list = null;
                    await UiFeedbackHelper.RunBusyAsync(
                        this,
                        actionButton,
                        "ĐANG TẢI...",
                        delegate
                        {
                            list = _reportingService.GetBaoCaoTuoiNoTongHop(ngayChot, out err, maKH, filterRuiRo);
                            return true;
                        });

                    if (IsDisposed) return false;
                    if (!string.IsNullOrEmpty(err))
                    {
                        UiStyler.StyleStatusLabel(_tuoiNoStatus, UiStatusKind.Error, err);
                        UiStyler.SetGridError(dgvTuoiNo, err);
                        return false;
                    }

                    _currentTuoiNoTongHop = list ?? new List<BaoCaoTuoiNoTongHopDTO>();
                    dgvTuoiNo.Rows.Clear();

                    decimal tongNo = 0, tongTrongHan = 0, tong3160 = 0, tong6190 = 0, tongKhoDoi = 0;
                    foreach (var item in _currentTuoiNoTongHop)
                    {
                        tongNo += item.TongNo;
                        tongTrongHan += item.TrongHan_0_30;
                        tong3160 += item.QuaHanNhe_31_60;
                        tong6190 += item.QuaHanTB_61_90;
                        tongKhoDoi += item.KhoDoi_Tren90;

                        dgvTuoiNo.Rows.Add(
                            item.MaKH,
                            item.TenKH,
                            item.DienThoai,
                            item.TongNo,
                            item.TrongHan_0_30,
                            item.QuaHanNhe_31_60,
                            item.QuaHanTB_61_90,
                            item.KhoDoi_Tren90,
                            item.TenMucDoRuiRo
                        );
                    }

                    lblTongNoPhaiThu.Text = string.Format("Tổng nợ: {0:N0} VNĐ", tongNo);
                    lblTrongHan.Text = string.Format("Trong hạn: {0:N0} VNĐ", tongTrongHan);
                    lblQuaHan3160.Text = string.Format("Quá hạn 31-60: {0:N0} VNĐ", tong3160);
                    lblQuaHan6190.Text = string.Format("Quá hạn 61-90: {0:N0} VNĐ", tong6190);
                    lblKhoDoi.Text = string.Format("Khó đòi (>90): {0:N0} VNĐ", tongKhoDoi);

                    UiStyler.StyleStatusLabel(
                        _tuoiNoStatus,
                        _currentTuoiNoTongHop.Count == 0 ? UiStatusKind.Warning : UiStatusKind.Neutral,
                        _currentTuoiNoTongHop.Count == 0 ? "Không có dữ liệu công nợ phù hợp tiêu chí đã chọn." : string.Format("Đang hiển thị {0:N0} khách hàng có công nợ.", _currentTuoiNoTongHop.Count));
                }
                else
                {
                    List<BaoCaoTuoiNoChiTietDTO> list = null;
                    await UiFeedbackHelper.RunBusyAsync(
                        this,
                        actionButton,
                        "ĐANG TẢI...",
                        delegate
                        {
                            list = _reportingService.GetBaoCaoTuoiNoChiTiet(ngayChot, out err, maKH, filterRuiRo);
                            return true;
                        });

                    if (IsDisposed) return false;
                    if (!string.IsNullOrEmpty(err))
                    {
                        UiStyler.StyleStatusLabel(_tuoiNoStatus, UiStatusKind.Error, err);
                        UiStyler.SetGridError(dgvTuoiNo, err);
                        return false;
                    }

                    _currentTuoiNoChiTiet = list ?? new List<BaoCaoTuoiNoChiTietDTO>();
                    dgvTuoiNo.Rows.Clear();

                    decimal tongNo = 0, tongTrongHan = 0, tong3160 = 0, tong6190 = 0, tongKhoDoi = 0;
                    foreach (var item in _currentTuoiNoChiTiet)
                    {
                        tongNo += item.ConNo;
                        if (item.MaNhomTuoiNo == 3) tongKhoDoi += item.ConNo;
                        else if (item.MaNhomTuoiNo == 2) tong6190 += item.ConNo;
                        else if (item.MaNhomTuoiNo == 1) tong3160 += item.ConNo;
                        else tongTrongHan += item.ConNo;

                        dgvTuoiNo.Rows.Add(
                            item.MaHDB,
                            item.NgayLap,
                            item.MaKH,
                            item.TenKH,
                            item.TongTien,
                            item.DaThu,
                            item.ConNo,
                            string.Format("{0} ngày", item.SoNgayNo),
                            item.TenNhomTuoiNo
                        );
                    }

                    lblTongNoPhaiThu.Text = string.Format("Tổng nợ HĐ: {0:N0} VNĐ", tongNo);
                    lblTrongHan.Text = string.Format("Trong hạn: {0:N0} VNĐ", tongTrongHan);
                    lblQuaHan3160.Text = string.Format("Quá hạn 31-60: {0:N0} VNĐ", tong3160);
                    lblQuaHan6190.Text = string.Format("Quá hạn 61-90: {0:N0} VNĐ", tong6190);
                    lblKhoDoi.Text = string.Format("Khó đòi (>90): {0:N0} VNĐ", tongKhoDoi);

                    UiStyler.StyleStatusLabel(
                        _tuoiNoStatus,
                        _currentTuoiNoChiTiet.Count == 0 ? UiStatusKind.Warning : UiStatusKind.Neutral,
                        _currentTuoiNoChiTiet.Count == 0 ? "Không có hóa đơn nợ phù hợp tiêu chí đã chọn." : string.Format("Đang hiển thị {0:N0} hóa đơn còn nợ.", _currentTuoiNoChiTiet.Count));
                }

                UiStyler.ClearGridState(dgvTuoiNo);
                UiStyler.UpdateGridEmptyState(dgvTuoiNo, "Không có dữ liệu công nợ thỏa mãn tiêu chí.");
                return true;
            }
            finally
            {
                _isLoadingTuoiNo = false;
                if (btnXemTuoiNo != null && !btnXemTuoiNo.IsDisposed)
                {
                    btnXemTuoiNo.Text = "Tra Cứu";
                    btnXemTuoiNo.Enabled = true;
                }
            }
        }

        private void btnXuatCsvTuoiNo_Click(object sender, EventArgs e)
        {
            bool isTongHop = (cboCheDoXem.SelectedIndex <= 0);
            string title = isTongHop ? "BÁO CÁO PHÂN TÍCH TUỔI NỢ KHÁCH HÀNG (TỔNG HỢP)" : "BÁO CÁO CHI TIẾT TUỔI NỢ THEO HÓA ĐƠN";
            string filename = isTongHop ? "BaoCaoTuoiNoTongHop" : "BaoCaoTuoiNoChiTiet";
            CsvExporter.ExportDataGridViewToExcel(dgvTuoiNo, filename, title);
        }

        private void btnInTuoiNo_Click(object sender, EventArgs e)
        {
            bool isTongHop = (cboCheDoXem.SelectedIndex <= 0);
            string tenKHFilter = (cboKhachHangTuoiNo.SelectedIndex > 0) ? cboKhachHangTuoiNo.Text : null;
            DateTime ngayChot = dtpNgayChot.Value;

            if (isTongHop)
            {
                if (_currentTuoiNoTongHop == null || _currentTuoiNoTongHop.Count == 0)
                {
                    UiFeedbackHelper.ShowToast(this, "Không có dữ liệu báo cáo tuổi nợ để in. Vui lòng bấm 'Tra cứu' trước.", UiStatusKind.Warning, 3000);
                    return;
                }
                string html = ReportPrintHelper.GenerateBaoCaoTuoiNoTongHopHtml(ngayChot, _currentTuoiNoTongHop, tenKHFilter);
                frmInChungTu.ShowVoucher(this, "Báo Cáo Phân Tích Tuổi Nợ Khách Hàng", html, string.Format("BaoCaoTuoiNo_{0:yyyyMMdd}", ngayChot));
            }
            else
            {
                if (_currentTuoiNoChiTiet == null || _currentTuoiNoChiTiet.Count == 0)
                {
                    UiFeedbackHelper.ShowToast(this, "Không có dữ liệu chi tiết hóa đơn để in. Vui lòng bấm 'Tra cứu' trước.", UiStatusKind.Warning, 3000);
                    return;
                }
                string html = ReportPrintHelper.GenerateBaoCaoTuoiNoChiTietHtml(ngayChot, _currentTuoiNoChiTiet, tenKHFilter);
                frmInChungTu.ShowVoucher(this, "Báo Cáo Chi Tiết Tuổi Nợ Hóa Đơn", html, string.Format("BaoCaoTuoiNoHDB_{0:yyyyMMdd}", ngayChot));
            }
        }
    }
}
