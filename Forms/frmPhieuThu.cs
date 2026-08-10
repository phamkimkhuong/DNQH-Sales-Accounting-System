using System;
using System.Data;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using DNQH_KeToanBanHang.DataAccess;
using DNQH_KeToanBanHang.Helpers;
using DNQH_KeToanBanHang.Models;
using DNQH_KeToanBanHang.Services;

namespace DNQH_KeToanBanHang.Forms
{
    public partial class frmPhieuThu : Form
    {
        private readonly PhieuThuDAL _phieuThuDAL;
        private readonly AccountingService _accountingService;
        private readonly ErrorProvider _validationErrors;
        private readonly UiDebouncer _searchDebouncer;
        private DataTable _dtHoaDon;
        private bool _isBinding = false;
        private Label _entryStatus;
        private readonly string _initialMaHDB;
        private Button btnInPhieuTab1;
        private Button btnInPhieuDS;
        private ContextMenuStrip cmsPhieuThu;
        private string _lastCreatedMaPT = null;

        public frmPhieuThu() : this(null)
        {
        }

        public frmPhieuThu(string initialMaHDB)
        {
            _initialMaHDB = initialMaHDB;
            InitializeComponent();
            if (!string.IsNullOrEmpty(initialMaHDB))
            {
                this.StartPosition = FormStartPosition.CenterParent;
                this.Text = string.Format("Lập Phiếu Thu Tiền - Hóa đơn [{0}]", initialMaHDB);
                this.MaximizeBox = false;
                this.MinimizeBox = false;
                this.ShowInTaskbar = false;
                this.Size = new Size(950, 680);

                if (tabControlPhieuThu.TabPages.Contains(tabDanhSach))
                {
                    tabControlPhieuThu.TabPages.Remove(tabDanhSach);
                }
                tabControlPhieuThu.Appearance = TabAppearance.FlatButtons;
                tabControlPhieuThu.ItemSize = new Size(0, 1);
                tabControlPhieuThu.SizeMode = TabSizeMode.Fixed;

                btnLamMoi.Visible = false;
            }
            _validationErrors = UiStyler.CreateErrorProvider(this);
            BuildResponsiveLayout();
            ApplyFoundationDesign();
            _phieuThuDAL = new PhieuThuDAL();
            _accountingService = new AccountingService();
            _searchDebouncer = new UiDebouncer(350);
            txtTimKiem.TextChanged += txtTimKiem_TextChanged;
            Disposed += delegate { _searchDebouncer.Dispose(); };
        }

        public void SelectTab(int index)
        {
            if (index >= 0 && index < tabControlPhieuThu.TabPages.Count)
            {
                tabControlPhieuThu.SelectedIndex = index;
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
            BuildInvoiceSummaryLayout();
            BuildReceiptFormLayout();

            btnInPhieuTab1 = UiStyler.CreateButton("🖨️ In phiếu", Color.FromArgb(70, 80, 95));
            btnInPhieuTab1.Click += delegate
            {
                if (!string.IsNullOrEmpty(_lastCreatedMaPT))
                {
                    InPhieuThu(_lastCreatedMaPT);
                }
                else
                {
                    MessageBox.Show("Chưa có phiếu thu nào vừa tạo. Vui lòng lập phiếu hoặc chọn từ danh sách phiếu thu để in.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            };

            _entryStatus = UiLayoutBuilder.BuildCommandBar(
                pnlButtons,
                new Button[] { btnLuuPhieu, btnInPhieuTab1, btnLamMoi },
                "Chọn hóa đơn cần thu và kiểm tra số tiền còn lại.");

            pnlLapPhieu.Controls.Clear();
            TableLayoutPanel entryRoot = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 3,
                BackColor = UiTheme.Canvas
            };
            entryRoot.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            entryRoot.RowStyles.Add(new RowStyle(SizeType.Absolute, 90F));
            entryRoot.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            entryRoot.RowStyles.Add(new RowStyle(SizeType.Absolute, 64F));
            groupBoxThongTinHDB.Dock = DockStyle.Fill;
            groupBoxThongTinHDB.Margin = new Padding(0, 0, 0, 10);
            groupBoxPhieuThu.Dock = DockStyle.Fill;
            groupBoxPhieuThu.Margin = new Padding(0);
            pnlButtons.Margin = new Padding(0, 8, 0, 0);
            entryRoot.Controls.Add(groupBoxThongTinHDB, 0, 0);
            entryRoot.Controls.Add(groupBoxPhieuThu, 0, 1);
            entryRoot.Controls.Add(pnlButtons, 0, 2);
            pnlLapPhieu.Controls.Add(entryRoot);

            txtTimKiem.MinimumSize = new Size(190, UiTheme.ControlHeight);
            dtpFromDate.MinimumSize = new Size(130, UiTheme.ControlHeight);
            dtpToDate.MinimumSize = new Size(130, UiTheme.ControlHeight);
            Button btnXuatCsv = CsvExporter.CreateExportButton(dgvDanhSachPhieuThu, "DanhSachPhieuThu", "DANH SÁCH PHIẾU THU");

            btnInPhieuDS = UiStyler.CreateButton("🖨️ In phiếu thu", UiTheme.Secondary);
            btnInPhieuDS.Click += delegate { InPhieuThuDangChon(); };

            cmsPhieuThu = new ContextMenuStrip();
            ToolStripMenuItem miInPhieu = new ToolStripMenuItem("🖨️ In phiếu thu (Mẫu 01-TT)...");
            miInPhieu.Click += delegate { InPhieuThuDangChon(); };
            cmsPhieuThu.Items.Add(miInPhieu);
            dgvDanhSachPhieuThu.ContextMenuStrip = cmsPhieuThu;
            dgvDanhSachPhieuThu.CellDoubleClick += delegate(object sender, DataGridViewCellEventArgs e)
            {
                if (e.RowIndex >= 0)
                {
                    InPhieuThuDangChon();
                }
            };

            UiLayoutBuilder.BuildHistoryPage(
                tabDanhSach,
                pnlFilter,
                new Control[] { lblTimKiem, txtTimKiem, lblFromDate, dtpFromDate, lblToDate, dtpToDate, btnTimKiem, btnLamMoiDS, btnInPhieuDS, btnXuatCsv },
                dgvDanhSachPhieuThu,
                lblStatus);
            ResumeLayout(true);
        }

        private void BuildInvoiceSummaryLayout()
        {
            groupBoxThongTinHDB.Controls.Clear();
            TableLayoutPanel summary = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 4,
                RowCount = 1,
                Padding = new Padding(12, 8, 12, 6)
            };
            summary.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 34F));
            summary.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 24F));
            summary.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 18F));
            summary.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 24F));
            Label[] labels = { lblKhachHang, lblTongTienHDB, lblDaThu, lblConLai };
            for (int i = 0; i < labels.Length; i++)
            {
                labels[i].Dock = DockStyle.Fill;
                labels[i].AutoEllipsis = true;
                labels[i].TextAlign = ContentAlignment.MiddleLeft;
                labels[i].Margin = new Padding(0, 0, 8, 0);
                summary.Controls.Add(labels[i], i, 0);
            }
            groupBoxThongTinHDB.Controls.Add(summary);
        }

        private void BuildReceiptFormLayout()
        {
            groupBoxPhieuThu.Controls.Clear();
            groupBoxPhieuThu.Padding = new Padding(12, 10, 12, 10);
            TableLayoutPanel fields = UiLayoutBuilder.CreateStructuredGridLayout(
                3, 95,
                UiLayoutBuilder.Field(lblMaPT, txtMaPT, UiFieldSize.Code),
                UiLayoutBuilder.Field(lblNgayThu, dtpNgayThu, UiFieldSize.DateTime),
                UiLayoutBuilder.Field(lblHinhThuc, cboHinhThuc, UiFieldSize.Selection),
                UiLayoutBuilder.Field(lblHoaDon, cboHoaDon, UiFieldSize.Wide),
                UiLayoutBuilder.Field(lblNguoiNop, txtNguoiNop, UiFieldSize.Medium),
                UiLayoutBuilder.Field(lblSoTien, txtSoTien, UiFieldSize.Money),
                UiLayoutBuilder.FullWidthField(lblLyDoThu, txtLyDoThu),
                UiLayoutBuilder.FullWidthField(lblGhiChu, txtGhiChu));
            groupBoxPhieuThu.Controls.Add(fields);
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
                txtSoTien.ForeColor = UiTheme.Success;
                lblDaThu.ForeColor = UiTheme.Success;
                lblConLai.ForeColor = UiTheme.Danger;
            }
            UiStyler.StyleButton(btnLuuPhieu, UiButtonRole.Success);
            UiStyler.StyleButton(btnLamMoi, UiButtonRole.Secondary);
            UiStyler.StyleButton(btnTimKiem, UiButtonRole.Primary);
            UiStyler.StyleButton(btnLamMoiDS, UiButtonRole.Secondary);
            CsvExporter.AttachExportContextMenu(dgvDanhSachPhieuThu, "DanhSachPhieuThu", "DANH SÁCH PHIẾU THU");
            UiStyler.StyleStatusLabel(_entryStatus, UiStatusKind.Neutral, "Chọn hóa đơn cần thu và kiểm tra số tiền còn lại.");
            UiStyler.StyleStatusLabel(lblStatus, UiStatusKind.Neutral, "Chưa có dữ liệu phiếu thu.");

            UiStyler.SetAccessibleText(cboHoaDon, "Hóa đơn bán", "Chọn hóa đơn còn số tiền phải thu.");
            UiStyler.SetAccessibleText(txtNguoiNop, "Người nộp tiền", "Nhập họ tên người nộp tiền.");
            UiStyler.SetAccessibleText(txtSoTien, "Số tiền thu", "Nhập số tiền thu bằng đồng Việt Nam.");
            UiStyler.SetAccessibleText(txtLyDoThu, "Lý do thu", "Nhập nội dung thu tiền.");
            UiStyler.SetAccessibleText(dgvDanhSachPhieuThu, "Danh sách phiếu thu", "Danh sách các khoản thu đã ghi nhận.");
            dgvDanhSachPhieuThu.CellFormatting += dgvDanhSachPhieuThu_CellFormatting;
            ConfigureGridColumns();
        }

        private void ConfigureGridColumns()
        {
            dgvDanhSachPhieuThu.AutoGenerateColumns = false;
            dgvDanhSachPhieuThu.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            colMaPT.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colMaPT.Width = 100;
            colMaPT.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            colNgayThu.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colNgayThu.Width = 140;
            colNgayThu.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colNgayThu.DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";

            colMaHDB.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colMaHDB.Width = 110;
            colMaHDB.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            colTenKH.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colTenKH.FillWeight = 150F;
            colTenKH.MinimumWidth = 180;
            colTenKH.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            colSoTien.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colSoTien.Width = 135;
            colSoTien.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colSoTien.DefaultCellStyle.Format = "N0";

            colHinhThuc.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colHinhThuc.Width = 110;
            colHinhThuc.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            colNguoiNop.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colNguoiNop.Width = 140;
            colNguoiNop.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            colLyDoThu.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colLyDoThu.FillWeight = 160F;
            colLyDoThu.MinimumWidth = 160;
            colLyDoThu.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            colTenNV.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colTenNV.Width = 140;
            colTenNV.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            colGhiChu.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colGhiChu.FillWeight = 160F;
            colGhiChu.MinimumWidth = 140;
            colGhiChu.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
        }

        private void dgvDanhSachPhieuThu_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            string colName = dgvDanhSachPhieuThu.Columns[e.ColumnIndex].Name;
            if (colName == "colSoTien" && e.Value != null)
            {
                e.CellStyle.Font = new Font(dgvDanhSachPhieuThu.Font, FontStyle.Bold);
                e.CellStyle.ForeColor = Color.FromArgb(16, 120, 60);
            }
        }

        private async void frmPhieuThu_Load(object sender, EventArgs e)
        {
            this.ApplyResponsiveUI();
            if (!SessionManager.IsAdmin() && !SessionManager.IsAccountant())
            {
                MessageBox.Show("Bạn không có quyền truy cập vào chức năng Quản lý Phiếu thu tiền.\nChỉ Quản trị viên và Kế toán mới có quyền này.",
                    "Từ chối truy cập", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
                return;
            }
            ApplyAuthorization();
            ResetForm();
            await LoadHoaDonComboAsync();

            if (!string.IsNullOrEmpty(_initialMaHDB))
            {
                cboHoaDon.Enabled = false;
            }
            else
            {
                await _searchDebouncer.RunNowAsync(
                    delegate(int version) { return LoadDanhSachPhieuThuAsync(version, null, false); });
            }
        }

        private void ApplyAuthorization()
        {
            bool canWrite = SessionManager.IsAdmin() || SessionManager.IsAccountant();
            btnLuuPhieu.Enabled = canWrite;

            if (!canWrite)
            {
                tabControlPhieuThu.SelectedTab = tabDanhSach;
                tabLapPhieu.Text = "Lập Phiếu Thu (Chỉ xem)";
                btnLuuPhieu.Visible = false;
            }
        }

        private void ResetForm()
        {
            txtMaPT.Text = _accountingService.GenerateNewReceiptId();
            dtpNgayThu.Value = DateTime.Now;
            cboHinhThuc.SelectedIndex = 0;
            txtNguoiNop.Clear();
            txtSoTien.Text = "0";
            txtLyDoThu.Text = "Thu tiền bán hàng theo hóa đơn";
            txtGhiChu.Clear();
            btnLuuPhieu.Enabled = SessionManager.IsAdmin() || SessionManager.IsAccountant();
            _validationErrors.Clear();
            UiStyler.StyleStatusLabel(_entryStatus, UiStatusKind.Neutral, "Chọn hóa đơn cần thu và kiểm tra số tiền còn lại.");
        }

        private async Task LoadHoaDonComboAsync()
        {
            try
            {
                _isBinding = true;
                DataTable invoices = null;
                UiStyler.StyleStatusLabel(_entryStatus, UiStatusKind.Information, "Đang tải hóa đơn còn phải thu...");
                await UiFeedbackHelper.RunBusyAsync(
                    this,
                    null,
                    null,
                    delegate
                    {
                        invoices = _phieuThuDAL.GetHoaDonChuaThuDu();
                        return true;
                    });
                if (IsDisposed)
                {
                    return;
                }
                _dtHoaDon = invoices;

                DataTable comboSource = _dtHoaDon.Copy();
                comboSource.Columns.Add("DisplayText", typeof(string));

                foreach (DataRow row in comboSource.Rows)
                {
                    string maHDB = row["MaHDB"].ToString().Trim();
                    string tenKH = row["TenKH"].ToString().Trim();
                    decimal conLai = Convert.ToDecimal(row["ConLai"]);
                    row["DisplayText"] = string.Format("{0} - {1} (Còn lại: {2:N0} VNĐ)", maHDB, tenKH, conLai);
                }

                cboHoaDon.DataSource = comboSource;
                cboHoaDon.DisplayMember = "DisplayText";
                cboHoaDon.ValueMember = "MaHDB";

                if (!string.IsNullOrEmpty(_initialMaHDB))
                {
                    string target = _initialMaHDB.Trim();
                    int foundIndex = -1;
                    for (int i = 0; i < comboSource.Rows.Count; i++)
                    {
                        if (string.Equals(comboSource.Rows[i]["MaHDB"].ToString().Trim(), target, StringComparison.OrdinalIgnoreCase))
                        {
                            foundIndex = i;
                            break;
                        }
                    }

                    if (foundIndex >= 0)
                    {
                        cboHoaDon.SelectedIndex = foundIndex;
                        UpdateHoaDonSelection();
                        UiStyler.StyleStatusLabel(_entryStatus, UiStatusKind.Neutral, "Đã tự động chọn hóa đơn " + target);
                    }
                    else
                    {
                        UiStyler.StyleStatusLabel(_entryStatus, UiStatusKind.Warning, "Hóa đơn " + target + " đã được thu đủ hoặc không còn nợ.");
                    }
                }
                else if (comboSource.Rows.Count > 0)
                {
                    cboHoaDon.SelectedIndex = 0;
                    UpdateHoaDonSelection();
                    UiStyler.StyleStatusLabel(_entryStatus, UiStatusKind.Neutral, "Chọn hóa đơn cần thu và kiểm tra số tiền còn lại.");
                }
                else
                {
                    lblKhachHang.Text = "Khách hàng: (Không có hóa đơn nợ cần thu)";
                    lblTongTienHDB.Text = "Tổng hóa đơn: 0 VNĐ";
                    lblDaThu.Text = "Đã thu: 0 VNĐ";
                    lblConLai.Text = "Còn phải thu: 0 VNĐ";
                    btnLuuPhieu.Enabled = false;
                    UiStyler.StyleStatusLabel(_entryStatus, UiStatusKind.Warning, "Không có hóa đơn còn số tiền phải thu.");
                }
            }
            catch (Exception ex)
            {
                UiStyler.StyleStatusLabel(_entryStatus, UiStatusKind.Error, "Không thể tải hóa đơn còn phải thu.");
                UiErrorHandler.Show(this, "FRMPHIEUTHU_UI_ERROR", "Lỗi nạp danh sách hóa đơn còn phải thu.", ex);
            }
            finally
            {
                _isBinding = false;
            }
        }

        private void cboHoaDon_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_isBinding) return;
            UpdateHoaDonSelection();
        }

        private void UpdateHoaDonSelection()
        {
            if (cboHoaDon.SelectedValue == null) return;

            string maHDB = cboHoaDon.SelectedValue.ToString().Trim();
            DataRow selectedRow = null;

            foreach (DataRow r in _dtHoaDon.Rows)
            {
                if (r["MaHDB"].ToString().Trim() == maHDB)
                {
                    selectedRow = r;
                    break;
                }
            }

            if (selectedRow != null)
            {
                string tenKH = selectedRow["TenKH"].ToString().Trim();
                decimal tongTien = Convert.ToDecimal(selectedRow["TongTien"]);
                decimal daThu = Convert.ToDecimal(selectedRow["DaThu"]);
                decimal conLai = Convert.ToDecimal(selectedRow["ConLai"]);

                lblKhachHang.Text = "Khách hàng: " + tenKH;
                lblTongTienHDB.Text = string.Format("Tổng hóa đơn: {0:N0} VNĐ", tongTien);
                lblDaThu.Text = string.Format("Đã thu: {0:N0} VNĐ", daThu);
                lblConLai.Text = string.Format("Còn phải thu: {0:N0} VNĐ", conLai);

                txtNguoiNop.Text = tenKH;
                txtLyDoThu.Text = string.Format("Thu tiền khách hàng theo hóa đơn bán [{0}]", maHDB);
                txtSoTien.Text = string.Format("{0:N0}", conLai);
            }
        }

        private void txtSoTien_TextChanged(object sender, EventArgs e)
        {
            // Cho phép người dùng nhập tự do, loại bỏ dấu phẩy để parse
        }

        private async void btnLuuPhieu_Click(object sender, EventArgs e)
        {
            if (!SessionManager.IsAdmin() && !SessionManager.IsAccountant())
            {
                MessageBox.Show("Bạn không có quyền lập phiếu thu tiền.", "Từ chối quyền", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cboHoaDon.SelectedValue == null || string.IsNullOrWhiteSpace(cboHoaDon.SelectedValue.ToString()))
            {
                ShowEntryValidation(cboHoaDon, "Vui lòng chọn hóa đơn bán cần thu tiền.");
                return;
            }

            string rawSoTien = txtSoTien.Text.Replace(",", "").Replace(".", "").Trim();
            decimal soTien;
            if (!decimal.TryParse(rawSoTien, out soTien) || soTien <= 0)
            {
                ShowEntryValidation(txtSoTien, "Số tiền thu phải lớn hơn 0 VNĐ.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtNguoiNop.Text))
            {
                ShowEntryValidation(txtNguoiNop, "Vui lòng nhập họ tên người nộp tiền.");
                return;
            }

            string maHDB = cboHoaDon.SelectedValue.ToString().Trim();

            PhieuThu pt = new PhieuThu
            {
                MaHDB = maHDB,
                NgayThu = dtpNgayThu.Value,
                NguoiNop = txtNguoiNop.Text.Trim(),
                LyDoThu = txtLyDoThu.Text.Trim(),
                SoTien = soTien,
                HinhThuc = cboHinhThuc.SelectedItem != null ? cboHinhThuc.SelectedItem.ToString() : "Tiền mặt",
                GhiChu = txtGhiChu.Text.Trim()
            };

            string error = string.Empty;
            UiStyler.StyleStatusLabel(_entryStatus, UiStatusKind.Information, "Đang kiểm tra và ghi nhận phiếu thu...");
            bool ok = await UiFeedbackHelper.RunBusyAsync(
                this,
                btnLuuPhieu,
                "ĐANG GHI NHẬN...",
                delegate { return _accountingService.CreatePhieuThu(pt, out error); });
            if (IsDisposed)
            {
                return;
            }

            if (ok)
            {
                _lastCreatedMaPT = pt.MaPT;
                DialogResult dr = MessageBox.Show(
                    string.Format("Lập phiếu thu [{0}] thành công!\nHóa đơn: {1}\nSố tiền thu: {2:N0} VNĐ\nHình thức: {3}\n\nBạn có muốn in phiếu thu ngay không?",
                        pt.MaPT, maHDB, pt.SoTien, pt.HinhThuc),
                    "Lập phiếu thu thành công",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Information);

                if (dr == DialogResult.Yes)
                {
                    InPhieuThu(pt.MaPT);
                }

                if (this.Modal)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                    return;
                }

                ResetForm();
                await LoadHoaDonComboAsync();
                await _searchDebouncer.RunNowAsync(
                    delegate(int version) { return LoadDanhSachPhieuThuAsync(version, null, false); });
            }
            else
            {
                MessageBox.Show(error, "Lỗi lập phiếu thu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnLamMoi_Click(object sender, EventArgs e)
        {
            ResetForm();
            await LoadHoaDonComboAsync();
        }

        private async Task LoadDanhSachPhieuThuAsync(int requestVersion, Button actionButton, bool useFilters)
        {
            string keyword = txtTimKiem.Text.Trim();
            DateTime from = dtpFromDate.Value.Date;
            DateTime to = dtpToDate.Value.Date;
            UiStyler.SetGridLoading(dgvDanhSachPhieuThu, "Đang tải danh sách phiếu thu...");
            UiStyler.StyleStatusLabel(lblStatus, UiStatusKind.Information, "Đang tra cứu phiếu thu...");
            try
            {
                DataTable dt = null;
                await UiFeedbackHelper.RunBusyAsync(
                    this,
                    actionButton,
                    "ĐANG TẢI...",
                    delegate
                    {
                        dt = useFilters
                            ? _phieuThuDAL.Search(keyword, from, to)
                            : _phieuThuDAL.GetAll();
                        return true;
                    });
                if (IsDisposed || !_searchDebouncer.IsCurrent(requestVersion))
                {
                    return;
                }

                dgvDanhSachPhieuThu.AutoGenerateColumns = false;
                dgvDanhSachPhieuThu.DataSource = dt;
                UiStyler.StyleStatusLabel(
                    lblStatus,
                    dt.Rows.Count == 0 ? UiStatusKind.Warning : UiStatusKind.Neutral,
                    dt.Rows.Count == 0 ? "Không tìm thấy phiếu thu phù hợp." : string.Format("Đang hiển thị {0:N0} phiếu thu.", dt.Rows.Count));

                ConfigureGridColumns();
                UiStyler.ClearGridState(dgvDanhSachPhieuThu);
                UiStyler.UpdateGridEmptyState(dgvDanhSachPhieuThu, "Không tìm thấy phiếu thu phù hợp.");
            }
            catch (Exception ex)
            {
                if (!_searchDebouncer.IsCurrent(requestVersion))
                {
                    return;
                }
                UiStyler.SetGridError(dgvDanhSachPhieuThu, "Không thể tải danh sách phiếu thu.");
                UiErrorHandler.Show(this, "FRMPHIEUTHU_UI_ERROR", "Lỗi nạp danh sách phiếu thu.", ex);
            }
        }

        private async void btnTimKiem_Click(object sender, EventArgs e)
        {
            await _searchDebouncer.RunNowAsync(
                delegate(int version) { return LoadDanhSachPhieuThuAsync(version, btnTimKiem, true); });
        }

        private async void btnLamMoiDS_Click(object sender, EventArgs e)
        {
            txtTimKiem.Clear();
            await _searchDebouncer.RunNowAsync(
                delegate(int version) { return LoadDanhSachPhieuThuAsync(version, btnLamMoiDS, false); });
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            _searchDebouncer.Restart(
                delegate(int version) { return LoadDanhSachPhieuThuAsync(version, null, true); });
        }

        private void ShowEntryValidation(Control control, string message)
        {
            UiInteractionHelper.ShowValidationError(_validationErrors, control, message);
            UiStyler.StyleStatusLabel(_entryStatus, UiStatusKind.Warning, message);
        }

        private void InPhieuThuDangChon()
        {
            if (dgvDanhSachPhieuThu.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn một phiếu thu cần in!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string maPT = dgvDanhSachPhieuThu.CurrentRow.Cells["colMaPT"].Value != null
                ? dgvDanhSachPhieuThu.CurrentRow.Cells["colMaPT"].Value.ToString().Trim()
                : string.Empty;

            InPhieuThu(maPT);
        }

        private void InPhieuThu(string maPT)
        {
            if (string.IsNullOrEmpty(maPT))
            {
                MessageBox.Show("Vui lòng chỉ định mã phiếu thu cần in!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                PhieuThu pt = _phieuThuDAL.GetById(maPT);
                if (pt == null)
                {
                    MessageBox.Show("Không tìm thấy dữ liệu phiếu thu " + maPT, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string html = VoucherPrintHelper.GeneratePhieuThuHtml(pt);
                frmInChungTu.ShowVoucher(this, "Phiếu Thu Tiền - " + maPT, html, "PhieuThu_" + maPT);
            }
            catch (Exception ex)
            {
                AppLogger.Error("PRINT_PT_ERROR", "Lỗi khi xuất mẫu in phiếu thu: " + ex.Message, ex);
                MessageBox.Show("Lỗi khi mở mẫu in phiếu thu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
