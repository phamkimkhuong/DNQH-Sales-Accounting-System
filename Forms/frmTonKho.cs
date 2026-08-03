using System;
using System.Data;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using DNQH_KeToanBanHang.DataAccess;
using DNQH_KeToanBanHang.Helpers;

namespace DNQH_KeToanBanHang.Forms
{
    public partial class frmTonKho : Form
    {
        private readonly TonKhoDAL _tonKhoDAL;
        private readonly KhoDAL _khoDAL;
        private readonly UiDebouncer _searchDebouncer;
        private bool _isInitializing;
        private CheckBox chkSapHetHang;

        public frmTonKho()
        {
            InitializeComponent();
            BuildResponsiveLayout();
            ApplyFoundationDesign();
            _tonKhoDAL = new TonKhoDAL();
            _khoDAL = new KhoDAL();
            _searchDebouncer = new UiDebouncer(350);
            txtTimKiem.TextChanged += txtTimKiem_TextChanged;
            Disposed += delegate { _searchDebouncer.Dispose(); };
        }

        private void BuildResponsiveLayout()
        {
            SuspendLayout();
            Controls.Clear();
            MinimumSize = new Size(720, 520);

            TableLayoutPanel root = new TableLayoutPanel
            {
                Name = "tlpTonKhoRoot",
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 4,
                BackColor = UiTheme.Canvas
            };
            root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            root.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
            root.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            root.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            root.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));

            pnlFilter.Controls.Clear();
            pnlFilter.Dock = DockStyle.Fill;
            pnlFilter.AutoSize = true;
            pnlFilter.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            FlowLayoutPanel filters = new FlowLayoutPanel
            {
                Name = "flpTonKhoFilters",
                Dock = DockStyle.Fill,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                WrapContents = true,
                Padding = new Padding(16, 9, 8, 5)
            };
            chkSapHetHang = new CheckBox
            {
                Name = "chkSapHetHang",
                Text = "⚠️ Sắp hết (≤ 10)",
                AutoSize = true,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = Color.FromArgb(194, 65, 12),
                Cursor = Cursors.Hand
            };
            chkSapHetHang.CheckedChanged += chkSapHetHang_CheckedChanged;

            Button btnXuatCsv = CsvExporter.CreateExportButton(dgvTonKho, "BaoCaoTonKho", "BÁO CÁO TỒN KHO");
            Control[] filterControls = { lblKho, cboKho, lblTimKiem, txtTimKiem, chkSapHetHang, btnTimKiem, btnLamMoi, btnXuatCsv, btnDong };
            foreach (Control control in filterControls)
            {
                control.Anchor = AnchorStyles.None;
                control.Margin = control is Label
                    ? new Padding(0, 9, 6, 6)
                    : (control is CheckBox ? new Padding(0, 8, 10, 6) : new Padding(0, 0, 10, 6));
                filters.Controls.Add(control);
            }
            cboKho.MinimumSize = new Size(220, UiTheme.ControlHeight);
            txtTimKiem.MinimumSize = new Size(220, UiTheme.ControlHeight);
            pnlFilter.Controls.Add(filters);

            pnlHeader.Dock = DockStyle.Fill;
            pnlHeader.Margin = new Padding(0);
            dgvTonKho.Dock = DockStyle.Fill;
            dgvTonKho.Margin = new Padding(0);
            pnlFooter.Dock = DockStyle.Fill;
            pnlFooter.Margin = new Padding(0);
            pnlFilter.Margin = new Padding(0);
            lblThongKe.Dock = DockStyle.Fill;
            lblThongKe.Padding = new Padding(16, 0, 8, 0);
            lblThongKe.TextAlign = ContentAlignment.MiddleLeft;

            root.Controls.Add(pnlHeader, 0, 0);
            root.Controls.Add(pnlFilter, 0, 1);
            root.Controls.Add(dgvTonKho, 0, 2);
            root.Controls.Add(pnlFooter, 0, 3);
            Controls.Add(root);
            ResumeLayout(true);
        }

        private void ApplyFoundationDesign()
        {
            lblSubTitle.Text = "Theo dõi số lượng thực tế theo kho và tìm nhanh mặt hàng";
            UiStyler.Apply(this);

            if (!SystemInformation.HighContrast)
            {
                pnlHeader.BackColor = UiTheme.Primary;
                lblTitle.ForeColor = Color.White;
                lblSubTitle.ForeColor = Color.FromArgb(237, 233, 254);
                pnlFilter.BackColor = UiTheme.Surface;
                pnlFooter.BackColor = UiTheme.SurfaceMuted;
            }

            UiStyler.StyleButton(btnTimKiem, UiButtonRole.Primary);
            UiStyler.StyleButton(btnLamMoi, UiButtonRole.Secondary);
            UiStyler.StyleButton(btnDong, UiButtonRole.Secondary);
            UiStyler.StyleStatusLabel(lblThongKe, UiStatusKind.Neutral, "Chưa có dữ liệu tồn kho.");
            CsvExporter.AttachExportContextMenu(dgvTonKho, "BaoCaoTonKho", "BÁO CÁO TỒN KHO");

            UiStyler.SetAccessibleText(cboKho, "Kho hàng", "Lọc tồn kho theo địa điểm kho.");
            UiStyler.SetAccessibleText(txtTimKiem, "Từ khóa sản phẩm", "Tìm theo mã hoặc tên sản phẩm.");
            UiStyler.SetAccessibleText(dgvTonKho, "Danh sách tồn kho", "Số lượng thực tế của từng sản phẩm theo kho.");
            UiStyler.SetAccessibleText(lblThongKe, "Thống kê tồn kho", "Tổng số dòng và tổng số lượng đang hiển thị.");
            dgvTonKho.CellFormatting += dgvTonKho_CellFormatting;
            ConfigureGridColumns();
        }

        private void ConfigureGridColumns()
        {
            dgvTonKho.AutoGenerateColumns = false;
            dgvTonKho.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            colMaKho.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colMaKho.Width = 80;
            colMaKho.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            colTenKho.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colTenKho.Width = 160;
            colTenKho.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            colMaSP.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colMaSP.Width = 95;
            colMaSP.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            colTenSP.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colTenSP.FillWeight = 180F;
            colTenSP.MinimumWidth = 200;
            colTenSP.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            colDonViTinh.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colDonViTinh.Width = 65;
            colDonViTinh.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            colDonGiaBan.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colDonGiaBan.Width = 120;
            colDonGiaBan.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colDonGiaBan.DefaultCellStyle.Format = "N0";

            colSoLuongTon.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colSoLuongTon.Width = 105;
            colSoLuongTon.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colSoLuongTon.DefaultCellStyle.Format = "N0";
            colSoLuongTon.DefaultCellStyle.Font = new Font(dgvTonKho.Font, FontStyle.Bold);

            colNgayCapNhat.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colNgayCapNhat.Width = 140;
            colNgayCapNhat.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colNgayCapNhat.DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
        }

        private async void frmTonKho_Load(object sender, EventArgs e)
        {
            this.ApplyResponsiveUI();
            _isInitializing = true;
            await LoadKhoComboBoxAsync();
            _isInitializing = false;
            await _searchDebouncer.RunNowAsync(
                delegate(int version) { return LoadDataAsync(version, null); });
        }

        private async Task LoadKhoComboBoxAsync()
        {
            try
            {
                DataTable dtKho = null;
                await UiFeedbackHelper.RunBusyAsync(
                    this,
                    null,
                    null,
                    delegate
                    {
                        dtKho = _khoDAL.GetAll();
                        return true;
                    });
                if (IsDisposed)
                {
                    return;
                }

                DataTable dtCombo = dtKho.Clone();

                DataRow allRow = dtCombo.NewRow();
                allRow["MaKho"] = "";
                allRow["TenKho"] = "--- Tất cả các kho ---";
                dtCombo.Rows.Add(allRow);

                foreach (DataRow row in dtKho.Rows)
                {
                    dtCombo.ImportRow(row);
                }

                cboKho.DisplayMember = "TenKho";
                cboKho.ValueMember = "MaKho";
                cboKho.DataSource = dtCombo;
                cboKho.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                UiErrorHandler.Show(this, "FRMTONKHO_UI_ERROR", "Lỗi khi tải danh sách kho.", ex);
            }
        }

        private async Task LoadDataAsync(int requestVersion, Button actionButton)
        {
            string selectedKho = "";
            if (cboKho.SelectedValue != null && !(cboKho.SelectedValue is DataRowView))
            {
                selectedKho = cboKho.SelectedValue.ToString().Trim();
            }
            string keyword = txtTimKiem.Text.Trim();

            UiStyler.SetGridLoading(dgvTonKho, "Đang tra cứu tồn kho...");
            UiStyler.StyleStatusLabel(lblThongKe, UiStatusKind.Information, "Đang tải dữ liệu tồn kho...");
            try
            {
                DataTable dt = null;
                await UiFeedbackHelper.RunBusyAsync(
                    this,
                    actionButton,
                    "ĐANG TẢI...",
                    delegate
                    {
                        if (!string.IsNullOrEmpty(keyword))
                        {
                            dt = _tonKhoDAL.SearchTonKho(keyword, string.IsNullOrEmpty(selectedKho) ? null : selectedKho);
                        }
                        else if (!string.IsNullOrEmpty(selectedKho))
                        {
                            dt = _tonKhoDAL.GetByKho(selectedKho);
                        }
                        else
                        {
                            dt = _tonKhoDAL.GetAll();
                        }

                        if (chkSapHetHang != null && chkSapHetHang.Checked && dt != null && dt.Rows.Count > 0)
                        {
                            DataView dv = dt.DefaultView;
                            dv.RowFilter = "ISNULL(SoLuongTon, 0) <= 10";
                            dt = dv.ToTable();
                        }

                        return true;
                    });

                if (IsDisposed || !_searchDebouncer.IsCurrent(requestVersion))
                {
                    return;
                }

                dgvTonKho.AutoGenerateColumns = false;
                dgvTonKho.DataSource = dt;

                // Cập nhật thống kê
                int totalItems = dt.Rows.Count;
                int totalQty = 0;
                foreach (DataRow r in dt.Rows)
                {
                    if (r["SoLuongTon"] != DBNull.Value)
                    {
                        totalQty += Convert.ToInt32(r["SoLuongTon"]);
                    }
                }

                UiStyler.StyleStatusLabel(
                    lblThongKe,
                    totalItems == 0 ? UiStatusKind.Warning : UiStatusKind.Neutral,
                    totalItems == 0
                        ? "Không có dữ liệu tồn kho phù hợp."
                        : string.Format("Tổng số mặt hàng: {0:N0} dòng | Tổng số lượng tồn: {1:N0} sản phẩm", totalItems, totalQty));
                UiStyler.ClearGridState(dgvTonKho);
                UiStyler.UpdateGridEmptyState(dgvTonKho, "Không có dữ liệu tồn kho phù hợp.");
            }
            catch (Exception ex)
            {
                if (!_searchDebouncer.IsCurrent(requestVersion))
                {
                    return;
                }
                UiStyler.SetGridError(dgvTonKho, "Không thể tải dữ liệu tồn kho.");
                UiErrorHandler.Show(this, "FRMTONKHO_UI_ERROR", "Lỗi khi tải dữ liệu tồn kho.", ex);
            }
        }

        private async void cboKho_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_isInitializing)
            {
                await _searchDebouncer.RunNowAsync(
                    delegate(int version) { return LoadDataAsync(version, null); });
            }
        }

        private async void btnTimKiem_Click(object sender, EventArgs e)
        {
            await _searchDebouncer.RunNowAsync(
                delegate(int version) { return LoadDataAsync(version, btnTimKiem); });
        }

        private async void chkSapHetHang_CheckedChanged(object sender, EventArgs e)
        {
            if (!_isInitializing)
            {
                await _searchDebouncer.RunNowAsync(
                    delegate(int version) { return LoadDataAsync(version, null); });
            }
        }

        private async void btnLamMoi_Click(object sender, EventArgs e)
        {
            _isInitializing = true;
            txtTimKiem.Clear();
            if (cboKho.Items.Count > 0)
            {
                cboKho.SelectedIndex = 0;
            }
            if (chkSapHetHang != null)
            {
                chkSapHetHang.Checked = false;
            }
            _isInitializing = false;
            await _searchDebouncer.RunNowAsync(
                delegate(int version) { return LoadDataAsync(version, btnLamMoi); });
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            if (!_isInitializing)
            {
                _searchDebouncer.Restart(
                    delegate(int version) { return LoadDataAsync(version, null); });
            }
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvTonKho_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (SystemInformation.HighContrast || e.RowIndex < 0 || e.ColumnIndex != colSoLuongTon.Index || e.Value == null)
            {
                return;
            }

            int quantity;
            if (!int.TryParse(e.Value.ToString(), out quantity))
            {
                return;
            }

            if (quantity <= 0)
            {
                e.CellStyle.ForeColor = UiTheme.Danger;
                e.CellStyle.SelectionForeColor = UiTheme.Danger;
                e.CellStyle.Font = new Font(dgvTonKho.Font, FontStyle.Bold);
            }
            else if (quantity <= 10)
            {
                e.CellStyle.ForeColor = UiTheme.Warning;
                e.CellStyle.SelectionForeColor = UiTheme.Warning;
                e.CellStyle.Font = new Font(dgvTonKho.Font, FontStyle.Bold);
            }
        }
    }
}
