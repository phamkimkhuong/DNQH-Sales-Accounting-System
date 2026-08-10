using System;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows.Forms;
using DNQH_KeToanBanHang.DataAccess;
using DNQH_KeToanBanHang.Helpers;
using DNQH_KeToanBanHang.Models;
using DNQH_KeToanBanHang.Services;

namespace DNQH_KeToanBanHang.Forms
{
    public partial class frmPhieuChi : Form
    {
        private readonly PhieuChiDAL _phieuChiDAL;
        private readonly AccountingService _accountingService;
        private readonly ErrorProvider _validationErrors;
        private readonly UiDebouncer _searchDebouncer;
        private Label _entryStatus;
        private Button btnInPhieuTab1;
        private Button btnInPhieuDS;
        private ContextMenuStrip cmsPhieuChi;
        private string _lastCreatedMaPC = null;

        public frmPhieuChi()
        {
            InitializeComponent();
            _validationErrors = UiStyler.CreateErrorProvider(this);
            BuildResponsiveLayout();
            ApplyFoundationDesign();
            _phieuChiDAL = new PhieuChiDAL();
            _accountingService = new AccountingService();
            _searchDebouncer = new UiDebouncer(350);
            txtTimKiem.TextChanged += txtTimKiem_TextChanged;
            Disposed += delegate { _searchDebouncer.Dispose(); };
        }

        public void SelectTab(int index)
        {
            if (index >= 0 && index < tabControlPhieuChi.TabPages.Count)
            {
                tabControlPhieuChi.SelectedIndex = index;
                if (index == 1 && txtTimKiem != null && txtTimKiem.CanFocus)
                {
                    txtTimKiem.Focus();
                }
            }
        }

        private void BuildResponsiveLayout()
        {
            SuspendLayout();
            MinimumSize = new Size(780, 620);
            groupBoxPhieuChi.Text = "Thông tin phiếu chi";
            groupBoxPhieuChi.Controls.Clear();
            groupBoxPhieuChi.Padding = new Padding(12, 10, 12, 10);

            TableLayoutPanel fields = UiLayoutBuilder.CreateStructuredGridLayout(
                3, 95,
                UiLayoutBuilder.Field(lblMaPC, txtMaPC, UiFieldSize.Code),
                UiLayoutBuilder.Field(lblNgayChi, dtpNgayChi, UiFieldSize.DateTime),
                UiLayoutBuilder.Field(lblHinhThuc, cboHinhThuc, UiFieldSize.Selection),
                UiLayoutBuilder.Field(lblNguoiNhan, txtNguoiNhan, UiFieldSize.Medium),
                UiLayoutBuilder.Field(lblSoTien, txtSoTien, UiFieldSize.Money),
                UiLayoutBuilder.FullWidthField(lblLyDoChi, txtLyDoChi),
                UiLayoutBuilder.FullWidthField(lblGhiChu, txtGhiChu));
            groupBoxPhieuChi.Controls.Add(fields);

            btnInPhieuTab1 = UiStyler.CreateButton("🖨️ In phiếu", Color.FromArgb(70, 80, 95));
            btnInPhieuTab1.Click += delegate
            {
                if (!string.IsNullOrEmpty(_lastCreatedMaPC))
                {
                    InPhieuChi(_lastCreatedMaPC);
                }
                else
                {
                    MessageBox.Show("Chưa có phiếu chi nào vừa tạo. Vui lòng lập phiếu hoặc chọn từ danh sách phiếu chi để in.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            };

            _entryStatus = UiLayoutBuilder.BuildCommandBar(
                pnlButtons,
                new Button[] { btnLuuPhieu, btnInPhieuTab1, btnLamMoi },
                "Nhập người nhận, số tiền và lý do chi.");

            pnlLapPhieu.Controls.Clear();
            TableLayoutPanel entryRoot = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 2,
                BackColor = UiTheme.Canvas
            };
            entryRoot.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            entryRoot.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            entryRoot.RowStyles.Add(new RowStyle(SizeType.Absolute, 64F));
            groupBoxPhieuChi.Dock = DockStyle.Fill;
            groupBoxPhieuChi.Margin = new Padding(0);
            pnlButtons.Margin = new Padding(0, 8, 0, 0);
            entryRoot.Controls.Add(groupBoxPhieuChi, 0, 0);
            entryRoot.Controls.Add(pnlButtons, 0, 1);
            pnlLapPhieu.Controls.Add(entryRoot);

            txtTimKiem.MinimumSize = new Size(190, UiTheme.ControlHeight);
            dtpFromDate.MinimumSize = new Size(130, UiTheme.ControlHeight);
            dtpToDate.MinimumSize = new Size(130, UiTheme.ControlHeight);
            Button btnXuatCsv = CsvExporter.CreateExportButton(dgvDanhSachPhieuChi, "DanhSachPhieuChi", "DANH SÁCH PHIẾU CHI");

            btnInPhieuDS = UiStyler.CreateButton("🖨️ In phiếu chi", UiTheme.Secondary);
            btnInPhieuDS.Click += delegate { InPhieuChiDangChon(); };

            cmsPhieuChi = new ContextMenuStrip();
            ToolStripMenuItem miInPhieu = new ToolStripMenuItem("🖨️ In phiếu chi (Mẫu 02-TT)...");
            miInPhieu.Click += delegate { InPhieuChiDangChon(); };
            cmsPhieuChi.Items.Add(miInPhieu);
            dgvDanhSachPhieuChi.ContextMenuStrip = cmsPhieuChi;
            dgvDanhSachPhieuChi.CellDoubleClick += delegate(object sender, DataGridViewCellEventArgs e)
            {
                if (e.RowIndex >= 0)
                {
                    InPhieuChiDangChon();
                }
            };

            UiLayoutBuilder.BuildHistoryPage(
                tabDanhSach,
                pnlFilter,
                new Control[] { lblTimKiem, txtTimKiem, lblFromDate, dtpFromDate, lblToDate, dtpToDate, btnTimKiem, btnLamMoiDS, btnInPhieuDS, btnXuatCsv },
                dgvDanhSachPhieuChi,
                lblStatus);
            ResumeLayout(true);
        }

        private void ApplyFoundationDesign()
        {
            UiStyler.Apply(this);
            if (!SystemInformation.HighContrast)
            {
                tabLapPhieu.BackColor = UiTheme.Canvas;
                tabDanhSach.BackColor = UiTheme.Canvas;
                pnlButtons.BackColor = UiTheme.SurfaceMuted;
                pnlFilter.BackColor = UiTheme.Surface;
                txtSoTien.ForeColor = UiTheme.Danger;
            }
            UiStyler.StyleButton(btnLuuPhieu, UiButtonRole.Danger);
            UiStyler.StyleButton(btnLamMoi, UiButtonRole.Secondary);
            UiStyler.StyleButton(btnTimKiem, UiButtonRole.Primary);
            UiStyler.StyleButton(btnLamMoiDS, UiButtonRole.Secondary);
            CsvExporter.AttachExportContextMenu(dgvDanhSachPhieuChi, "DanhSachPhieuChi", "DANH SÁCH PHIẾU CHI");
            UiStyler.StyleStatusLabel(_entryStatus, UiStatusKind.Neutral, "Nhập người nhận, số tiền và lý do chi.");
            UiStyler.StyleStatusLabel(lblStatus, UiStatusKind.Neutral, "Chưa có dữ liệu phiếu chi.");

            UiStyler.SetAccessibleText(txtNguoiNhan, "Người nhận tiền", "Nhập họ tên người nhận tiền.");
            UiStyler.SetAccessibleText(txtSoTien, "Số tiền chi", "Nhập số tiền chi bằng đồng Việt Nam.");
            UiStyler.SetAccessibleText(cboHinhThuc, "Hình thức chi", "Chọn tiền mặt hoặc chuyển khoản.");
            UiStyler.SetAccessibleText(txtLyDoChi, "Lý do chi", "Nhập nội dung chi tiền bắt buộc.");
            UiStyler.SetAccessibleText(dgvDanhSachPhieuChi, "Danh sách phiếu chi", "Danh sách các khoản chi đã ghi nhận.");
            dgvDanhSachPhieuChi.CellFormatting += dgvDanhSachPhieuChi_CellFormatting;
            ConfigureGridColumns();
        }

        private void ConfigureGridColumns()
        {
            dgvDanhSachPhieuChi.AutoGenerateColumns = false;
            dgvDanhSachPhieuChi.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            colMaPC.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colMaPC.Width = 100;
            colMaPC.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            colNgayChi.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colNgayChi.Width = 140;
            colNgayChi.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colNgayChi.DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";

            colNguoiNhan.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colNguoiNhan.Width = 140;
            colNguoiNhan.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            colSoTien.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colSoTien.Width = 135;
            colSoTien.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colSoTien.DefaultCellStyle.Format = "N0";

            colHinhThuc.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colHinhThuc.Width = 110;
            colHinhThuc.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            colLyDoChi.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colLyDoChi.FillWeight = 180F;
            colLyDoChi.MinimumWidth = 180;
            colLyDoChi.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            colTenNV.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colTenNV.Width = 140;
            colTenNV.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            colGhiChu.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colGhiChu.FillWeight = 160F;
            colGhiChu.MinimumWidth = 140;
            colGhiChu.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
        }

        private async void frmPhieuChi_Load(object sender, EventArgs e)
        {
            this.ApplyResponsiveUI();
            if (!SessionManager.IsAdmin() && !SessionManager.IsAccountant())
            {
                MessageBox.Show("Bạn không có quyền truy cập vào chức năng Quản lý Phiếu chi tiền.\nChỉ Quản trị viên và Kế toán mới có quyền này.",
                    "Từ chối truy cập", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
                return;
            }
            ApplyAuthorization();
            InitHinhThucCombo();
            ResetForm();
            await _searchDebouncer.RunNowAsync(
                delegate(int version) { return LoadDanhSachPhieuChiAsync(version, null); });
        }

        private void ApplyAuthorization()
        {
            bool canWrite = SessionManager.IsAdmin() || SessionManager.IsAccountant();
            btnLuuPhieu.Enabled = canWrite;

            if (!canWrite)
            {
                tabControlPhieuChi.SelectedTab = tabDanhSach;
                tabLapPhieu.Text = "Lập Phiếu Chi (Chỉ xem)";
                btnLuuPhieu.Visible = false;
            }
        }

        private void InitHinhThucCombo()
        {
            if (cboHinhThuc.Items.Count == 0)
            {
                cboHinhThuc.Items.Add("Tiền mặt");
                cboHinhThuc.Items.Add("Chuyển khoản");
            }
            cboHinhThuc.SelectedIndex = 0;
        }

        private void ResetForm()
        {
            txtMaPC.Text = _accountingService.GenerateNewPaymentId();
            dtpNgayChi.Value = DateTime.Now;
            if (cboHinhThuc.Items.Count > 0)
            {
                cboHinhThuc.SelectedIndex = 0;
            }
            txtNguoiNhan.Clear();
            txtSoTien.Text = "0";
            txtLyDoChi.Text = "Chi hoạt động kinh doanh";
            txtGhiChu.Clear();
            btnLuuPhieu.Enabled = SessionManager.IsAdmin() || SessionManager.IsAccountant();
            _validationErrors.Clear();
            UiStyler.StyleStatusLabel(_entryStatus, UiStatusKind.Neutral, "Nhập người nhận, số tiền và lý do chi.");
            txtNguoiNhan.Focus();
        }

        private async Task LoadDanhSachPhieuChiAsync(int requestVersion, Button actionButton)
        {
            DateTime from = dtpFromDate.Value.Date;
            DateTime to = dtpToDate.Value.Date.AddDays(1).AddSeconds(-1);
            string keyword = txtTimKiem.Text.Trim();
            UiStyler.SetGridLoading(dgvDanhSachPhieuChi, "Đang tải danh sách phiếu chi...");
            UiStyler.StyleStatusLabel(lblStatus, UiStatusKind.Information, "Đang tra cứu phiếu chi...");
            try
            {
                DataTable dt = null;
                await UiFeedbackHelper.RunBusyAsync(
                    this,
                    actionButton,
                    "ĐANG TẢI...",
                    delegate
                    {
                        dt = string.IsNullOrEmpty(keyword)
                            ? _phieuChiDAL.GetAll(from, to)
                            : _phieuChiDAL.Search(keyword, from, to);
                        return true;
                    });
                if (IsDisposed || !_searchDebouncer.IsCurrent(requestVersion))
                {
                    return;
                }

                dgvDanhSachPhieuChi.AutoGenerateColumns = false;
                dgvDanhSachPhieuChi.DataSource = dt;

                colMaPC.DataPropertyName = "MaPC";
                colNgayChi.DataPropertyName = "NgayChi";
                colNguoiNhan.DataPropertyName = "NguoiNhan";
                colSoTien.DataPropertyName = "SoTien";
                colHinhThuc.DataPropertyName = "HinhThuc";
                colLyDoChi.DataPropertyName = "LyDoChi";
                colTenNV.DataPropertyName = "TenNV";
                colGhiChu.DataPropertyName = "GhiChu";

                UiStyler.StyleStatusLabel(
                    lblStatus,
                    dt.Rows.Count == 0 ? UiStatusKind.Warning : UiStatusKind.Neutral,
                    dt.Rows.Count == 0 ? "Không tìm thấy phiếu chi phù hợp." : string.Format("Đang hiển thị {0:N0} phiếu chi.", dt.Rows.Count));
                UiStyler.ClearGridState(dgvDanhSachPhieuChi);
                UiStyler.UpdateGridEmptyState(dgvDanhSachPhieuChi, "Không tìm thấy phiếu chi phù hợp.");
            }
            catch (Exception ex)
            {
                if (!_searchDebouncer.IsCurrent(requestVersion))
                {
                    return;
                }
                UiStyler.SetGridError(dgvDanhSachPhieuChi, "Không thể tải danh sách phiếu chi.");
                AppLogger.Error("LoadDanhSachPhieuChi", "Lỗi nạp danh sách phiếu chi: " + ex.Message, ex);
                UiErrorHandler.Show(this, "FRMPHIEUCHI_UI_ERROR", "Không thể tải danh sách phiếu chi.", ex);
            }
        }

        private async void btnLuuPhieu_Click(object sender, EventArgs e)
        {
            if (!SessionManager.IsAdmin() && !SessionManager.IsAccountant())
            {
                MessageBox.Show("Bạn không có quyền lập phiếu chi tiền!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string nguoiNhan = txtNguoiNhan.Text.Trim();
            if (string.IsNullOrEmpty(nguoiNhan))
            {
                ShowEntryValidation(txtNguoiNhan, "Vui lòng nhập họ tên người nhận tiền.");
                return;
            }

            string soTienStr = txtSoTien.Text.Replace(",", "").Replace(".", "").Trim();
            decimal soTien;
            if (!decimal.TryParse(soTienStr, NumberStyles.Any, CultureInfo.InvariantCulture, out soTien) || soTien <= 0)
            {
                ShowEntryValidation(txtSoTien, "Số tiền chi phải lớn hơn 0 VNĐ.");
                return;
            }

            string lyDoChi = txtLyDoChi.Text.Trim();
            if (string.IsNullOrEmpty(lyDoChi))
            {
                ShowEntryValidation(txtLyDoChi, "Vui lòng nhập lý do chi tiền.");
                return;
            }

            string hinhThuc = cboHinhThuc.SelectedItem != null ? cboHinhThuc.SelectedItem.ToString() : "Tiền mặt";
            string ghiChu = txtGhiChu.Text.Trim();

            DialogResult dr = MessageBox.Show(
                string.Format("Xác nhận lập phiếu chi số tiền {0:N0} VNĐ cho '{1}'?", soTien, nguoiNhan),
                "Xác nhận chi tiền",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (dr != DialogResult.Yes)
                return;

            try
            {
                PhieuChi phieuChi = new PhieuChi
                {
                    NgayChi = dtpNgayChi.Value,
                    NguoiNhan = nguoiNhan,
                    LyDoChi = lyDoChi,
                    SoTien = soTien,
                    HinhThuc = hinhThuc,
                    GhiChu = ghiChu
                };

                string errorMessage = string.Empty;
                UiStyler.StyleStatusLabel(_entryStatus, UiStatusKind.Information, "Đang kiểm tra và ghi nhận phiếu chi...");
                bool ok = await UiFeedbackHelper.RunBusyAsync(
                    this,
                    btnLuuPhieu,
                    "ĐANG GHI NHẬN...",
                    delegate { return _accountingService.CreatePhieuChi(phieuChi, out errorMessage); });
                if (IsDisposed)
                {
                    return;
                }

                if (ok)
                {
                    _lastCreatedMaPC = phieuChi.MaPC;
                    DialogResult printPrompt = MessageBox.Show(
                        string.Format("Lập phiếu chi thành công!\nMã phiếu chi: {0}\nSố tiền: {1:N0} VNĐ\n\nBạn có muốn in phiếu chi ngay không?", phieuChi.MaPC, soTien),
                        "Lập phiếu chi thành công",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Information);

                    if (printPrompt == DialogResult.Yes)
                    {
                        InPhieuChi(phieuChi.MaPC);
                    }

                    ResetForm();
                    await _searchDebouncer.RunNowAsync(
                        delegate(int version) { return LoadDanhSachPhieuChiAsync(version, null); });
                }
                else
                {
                    MessageBox.Show(errorMessage, "Lỗi lập phiếu chi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                AppLogger.Error("btnLuuPhieu_Click", "Lỗi lưu phiếu chi: " + ex.Message, ex);
                MessageBox.Show("Đã xảy ra sự cố không mong muốn trong quá trình lập phiếu chi. Vui lòng thử lại sau.", "Lỗi Hệ Thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            ResetForm();
        }

        private async void btnTimKiem_Click(object sender, EventArgs e)
        {
            await _searchDebouncer.RunNowAsync(
                delegate(int version) { return LoadDanhSachPhieuChiAsync(version, btnTimKiem); });
        }

        private async void btnLamMoiDS_Click(object sender, EventArgs e)
        {
            txtTimKiem.Clear();
            dtpFromDate.Value = DateTime.Today.AddDays(-30);
            dtpToDate.Value = DateTime.Today;
            await _searchDebouncer.RunNowAsync(
                delegate(int version) { return LoadDanhSachPhieuChiAsync(version, btnLamMoiDS); });
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            _searchDebouncer.Restart(
                delegate(int version) { return LoadDanhSachPhieuChiAsync(version, null); });
        }

        private void txtSoTien_Leave(object sender, EventArgs e)
        {
            string raw = txtSoTien.Text.Replace(",", "").Replace(".", "").Trim();
            decimal val;
            if (decimal.TryParse(raw, NumberStyles.Any, CultureInfo.InvariantCulture, out val))
            {
                txtSoTien.Text = string.Format("{0:N0}", val);
            }
        }

        private void txtSoTien_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void dgvDanhSachPhieuChi_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            string colName = dgvDanhSachPhieuChi.Columns[e.ColumnIndex].Name;
            if (colName == "colSoTien" && e.Value != null && e.Value != DBNull.Value)
            {
                decimal val;
                if (decimal.TryParse(e.Value.ToString(), out val))
                {
                    e.Value = string.Format("{0:N0} VNĐ", val);
                    e.FormattingApplied = true;
                }
                e.CellStyle.Font = new Font(dgvDanhSachPhieuChi.Font, FontStyle.Bold);
                e.CellStyle.ForeColor = Color.FromArgb(192, 57, 43);
            }
            else if (colName == "colNgayChi" && e.Value != null && e.Value != DBNull.Value)
            {
                DateTime dt;
                if (DateTime.TryParse(e.Value.ToString(), out dt))
                {
                    e.Value = dt.ToString("dd/MM/yyyy HH:mm");
                    e.FormattingApplied = true;
                }
            }
        }

        private void ShowEntryValidation(Control control, string message)
        {
            UiInteractionHelper.ShowValidationError(_validationErrors, control, message);
            UiStyler.StyleStatusLabel(_entryStatus, UiStatusKind.Warning, message);
        }

        private void InPhieuChiDangChon()
        {
            if (dgvDanhSachPhieuChi.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn một phiếu chi cần in!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string maPC = dgvDanhSachPhieuChi.CurrentRow.Cells["colMaPC"].Value != null
                ? dgvDanhSachPhieuChi.CurrentRow.Cells["colMaPC"].Value.ToString().Trim()
                : string.Empty;

            InPhieuChi(maPC);
        }

        private void InPhieuChi(string maPC)
        {
            if (string.IsNullOrEmpty(maPC))
            {
                MessageBox.Show("Vui lòng chỉ định mã phiếu chi cần in!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                PhieuChi pc = _phieuChiDAL.GetById(maPC);
                if (pc == null)
                {
                    MessageBox.Show("Không tìm thấy dữ liệu phiếu chi " + maPC, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string html = VoucherPrintHelper.GeneratePhieuChiHtml(pc);
                frmInChungTu.ShowVoucher(this, "Phiếu Chi Tiền - " + maPC, html, "PhieuChi_" + maPC);
            }
            catch (Exception ex)
            {
                AppLogger.Error("PRINT_PC_ERROR", "Lỗi khi xuất mẫu in phiếu chi: " + ex.Message, ex);
                MessageBox.Show("Lỗi khi mở mẫu in phiếu chi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
