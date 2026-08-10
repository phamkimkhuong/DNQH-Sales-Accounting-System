using System;
using System.Collections.Generic;
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
    public partial class frmChungTu : Form
    {
        private readonly ChungTuDAL _chungTuDAL;
        private readonly AccountingService _accountingService;
        private readonly ErrorProvider _validationErrors;
        private readonly UiDebouncer _searchDebouncer;
        private readonly UiDebouncer _detailDebouncer;
        private readonly UiPaginationControl _pagerChungTu;
        private DataTable _dtHoaDon;
        private bool _isBinding = false;
        private bool _isBindingHistory;
        private Label _entryStatus;
        private Button btnInChungTuTab1;
        private Button btnInChungTuDS;
        private ContextMenuStrip cmsChungTu;
        private string _lastCreatedMaCT = null;

        public frmChungTu()
        {
            InitializeComponent();
            _pagerChungTu = new UiPaginationControl();
            _pagerChungTu.PageChanged += async delegate(object sender, PageChangedEventArgs e)
            {
                await _searchDebouncer.RunNowAsync(
                    delegate(int version) { return LoadDanhSachChungTuAsync(version, null); });
            };
            _validationErrors = UiStyler.CreateErrorProvider(this);
            BuildResponsiveLayout();
            ApplyFoundationDesign();
            _chungTuDAL = new ChungTuDAL();
            _accountingService = new AccountingService();
            _searchDebouncer = new UiDebouncer(350);
            _detailDebouncer = new UiDebouncer(100);
            txtTimKiem.TextChanged += txtTimKiem_TextChanged;
            Disposed += delegate
            {
                _searchDebouncer.Dispose();
                _detailDebouncer.Dispose();
            };
        }

        public void SelectTab(int index)
        {
            if (index >= 0 && index < tabControlChungTu.TabPages.Count)
            {
                tabControlChungTu.SelectedIndex = index;
                if (index == 1 && txtTimKiem != null && txtTimKiem.CanFocus)
                {
                    txtTimKiem.Focus();
                }
            }
        }

        private void BuildResponsiveLayout()
        {
            SuspendLayout();
            MinimumSize = new Size(900, 650);
            BuildDocumentHeaderLayout();
            BuildAccountingDetailLayout();

            btnInChungTuTab1 = UiStyler.CreateButton("🖨️ In chứng từ", Color.FromArgb(70, 80, 95));
            btnInChungTuTab1.Click += delegate
            {
                if (!string.IsNullOrEmpty(_lastCreatedMaCT))
                {
                    InChungTu(_lastCreatedMaCT);
                }
                else
                {
                    MessageBox.Show("Chưa có chứng từ nào vừa tạo. Vui lòng lập chứng từ hoặc chọn từ danh sách để in.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            };

            _entryStatus = UiLayoutBuilder.BuildCommandBar(
                pnlActions,
                new Button[] { btnLuuChungTu, btnInChungTuTab1, btnLamMoi },
                "Chọn hóa đơn và kiểm tra các dòng định khoản trước khi lưu.");

            pnlLapChungTu.Controls.Clear();
            TableLayoutPanel entryRoot = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 3,
                BackColor = UiTheme.Canvas
            };
            entryRoot.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            entryRoot.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            entryRoot.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            entryRoot.RowStyles.Add(new RowStyle(SizeType.Absolute, 64F));
            groupBoxThongTinChungTu.Dock = DockStyle.Fill;
            groupBoxThongTinChungTu.Margin = new Padding(0, 0, 0, 10);
            groupBoxChiTiet.Dock = DockStyle.Fill;
            groupBoxChiTiet.Margin = new Padding(0);
            pnlActions.Margin = new Padding(0, 8, 0, 0);
            entryRoot.Controls.Add(groupBoxThongTinChungTu, 0, 0);
            entryRoot.Controls.Add(groupBoxChiTiet, 0, 1);
            entryRoot.Controls.Add(pnlActions, 0, 2);
            pnlLapChungTu.Controls.Add(entryRoot);

            txtTimKiem.MinimumSize = new Size(190, UiTheme.ControlHeight);
            dtpFromDate.MinimumSize = new Size(130, UiTheme.ControlHeight);
            dtpToDate.MinimumSize = new Size(130, UiTheme.ControlHeight);
            groupBoxXemChiTiet.Text = "Chi tiết định khoản của chứng từ đang chọn";
            splitContainerDS.Panel1MinSize = 170;
            splitContainerDS.Panel2MinSize = 130;
            splitContainerDS.Resize += splitContainerDS_Resize;
            Button btnXuatCsv = CsvExporter.CreateExportButton(dgvDanhSachChungTu, "DanhSachChungTu", "DANH SÁCH CHỨNG TỪ KẾ TOÁN");

            btnInChungTuDS = UiStyler.CreateButton("🖨️ In chứng từ", UiTheme.Secondary);
            btnInChungTuDS.Click += delegate { InChungTuDangChon(); };

            cmsChungTu = new ContextMenuStrip();
            ToolStripMenuItem miIn = new ToolStripMenuItem("🖨️ In chứng từ kế toán (Mẫu 01-ĐK)...");
            miIn.Click += delegate { InChungTuDangChon(); };
            cmsChungTu.Items.Add(miIn);
            dgvDanhSachChungTu.ContextMenuStrip = cmsChungTu;
            dgvDanhSachChungTu.CellDoubleClick += delegate(object sender, DataGridViewCellEventArgs e)
            {
                if (e.RowIndex >= 0)
                {
                    InChungTuDangChon();
                }
            };

            UiLayoutBuilder.BuildHistoryPage(
                tabDanhSach,
                pnlFilter,
                new Control[] { lblTimKiem, txtTimKiem, lblFromDate, dtpFromDate, lblToDate, dtpToDate, btnTimKiem, btnLamMoiDS, btnInChungTuDS, btnXuatCsv },
                splitContainerDS,
                _pagerChungTu,
                38);
            ResumeLayout(true);
            AdjustHistorySplit();
        }

        private void BuildDocumentHeaderLayout()
        {
            groupBoxThongTinChungTu.Text = "Thông tin chứng từ";
            groupBoxThongTinChungTu.Controls.Clear();
            groupBoxThongTinChungTu.AutoSize = true;
            groupBoxThongTinChungTu.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            groupBoxThongTinChungTu.Padding = new Padding(12, 10, 12, 10);
            TableLayoutPanel fields = UiLayoutBuilder.CreateStructuredGridLayout(
                3, 95,
                UiLayoutBuilder.Field(lblMaCT, txtMaCT, UiFieldSize.Code),
                UiLayoutBuilder.Field(lblNgayCT, dtpNgayCT, UiFieldSize.DateTime),
                UiLayoutBuilder.Field(lblLoaiCT, cboLoaiCT, UiFieldSize.Selection),
                UiLayoutBuilder.Field(lblHoaDon, cboHoaDon, UiFieldSize.Wide),
                UiLayoutBuilder.SpannedField(new Label { Text = "Thông tin HDB:", AutoSize = false }, lblThongTinHDB, 2),
                UiLayoutBuilder.FullWidthField(lblDienGiai, txtDienGiai));
            groupBoxThongTinChungTu.Controls.Add(fields);
        }

        private void BuildAccountingDetailLayout()
        {
            groupBoxChiTiet.Text = "Chi tiết định khoản kế toán";
            groupBoxChiTiet.Controls.Clear();
            groupBoxChiTiet.Padding = new Padding(10, 8, 10, 10);

            pnlChiTietToolbar.Controls.Clear();
            pnlChiTietToolbar.Dock = DockStyle.Fill;
            TableLayoutPanel toolbar = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 2,
                Padding = new Padding(0, 2, 0, 2)
            };
            toolbar.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            toolbar.RowStyles.Add(new RowStyle(SizeType.Absolute, 38F));
            toolbar.RowStyles.Add(new RowStyle(SizeType.Absolute, 34F));

            FlowLayoutPanel actions = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                WrapContents = false,
                Margin = new Padding(0)
            };
            Button[] detailButtons = { btnThemDong, btnXoaDong, btnGoiY };
            for (int i = 0; i < detailButtons.Length; i++)
            {
                detailButtons[i].Margin = new Padding(0, 0, i == detailButtons.Length - 1 ? 0 : 8, 0);
                actions.Controls.Add(detailButtons[i]);
            }

            FlowLayoutPanel totals = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                WrapContents = false,
                Margin = new Padding(0),
                Padding = new Padding(0, 5, 0, 0)
            };
            Label[] totalLabels = { lblTongNo, lblTongCo, lblCanDoi };
            for (int i = 0; i < totalLabels.Length; i++)
            {
                totalLabels[i].AutoSize = true;
                totalLabels[i].Margin = new Padding(0, 0, i == totalLabels.Length - 1 ? 0 : 24, 0);
                totals.Controls.Add(totalLabels[i]);
            }
            toolbar.Controls.Add(actions, 0, 0);
            toolbar.Controls.Add(totals, 0, 1);
            pnlChiTietToolbar.Controls.Add(toolbar);

            TableLayoutPanel detailRoot = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 2
            };
            detailRoot.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            detailRoot.RowStyles.Add(new RowStyle(SizeType.Absolute, 76F));
            detailRoot.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            dgvChiTiet.Dock = DockStyle.Fill;
            dgvChiTiet.Margin = new Padding(0, 6, 0, 0);
            detailRoot.Controls.Add(pnlChiTietToolbar, 0, 0);
            detailRoot.Controls.Add(dgvChiTiet, 0, 1);
            groupBoxChiTiet.Controls.Add(detailRoot);
        }

        private void ApplyFoundationDesign()
        {
            UiStyler.Apply(this);
            if (!SystemInformation.HighContrast)
            {
                tabLapChungTu.BackColor = UiTheme.Canvas;
                tabDanhSach.BackColor = UiTheme.Canvas;
                pnlActions.BackColor = UiTheme.SurfaceMuted;
                pnlFilter.BackColor = UiTheme.Surface;
                lblThongTinHDB.ForeColor = UiTheme.Information;
                lblTongNo.ForeColor = UiTheme.Information;
                lblTongCo.ForeColor = UiTheme.Information;
                lblCanDoi.ForeColor = UiTheme.Success;
            }
            UiStyler.StyleButton(btnLuuChungTu, UiButtonRole.Success);
            UiStyler.StyleButton(btnLamMoi, UiButtonRole.Secondary);
            UiStyler.StyleButton(btnThemDong, UiButtonRole.Primary);
            UiStyler.StyleButton(btnXoaDong, UiButtonRole.Danger);
            UiStyler.StyleButton(btnGoiY, UiButtonRole.Information);
            UiStyler.StyleButton(btnTimKiem, UiButtonRole.Primary);
            UiStyler.StyleButton(btnLamMoiDS, UiButtonRole.Secondary);
            CsvExporter.AttachExportContextMenu(dgvDanhSachChungTu, "DanhSachChungTu", "DANH SÁCH CHỨNG TỪ KẾ TOÁN");
            CsvExporter.AttachExportContextMenu(dgvXemChiTiet, "ChiTietDinhKhoanChungTu", "CHI TIẾT ĐỊNH KHOẢN CHỨNG TỪ");
            UiStyler.StyleStatusLabel(_entryStatus, UiStatusKind.Neutral, "Chọn hóa đơn và kiểm tra các dòng định khoản trước khi lưu.");
            UiStyler.StyleStatusLabel(lblDSStatus, UiStatusKind.Neutral, "Chưa có dữ liệu chứng từ.");

            UiStyler.SetAccessibleText(cboHoaDon, "Hóa đơn bán", "Chọn hóa đơn cần lập chứng từ kế toán.");
            UiStyler.SetAccessibleText(txtDienGiai, "Diễn giải chứng từ", "Nhập nội dung của chứng từ kế toán.");
            UiStyler.SetAccessibleText(dgvChiTiet, "Chi tiết định khoản", "Nhập tài khoản Nợ, tài khoản Có và số tiền.");
            UiStyler.SetAccessibleText(dgvDanhSachChungTu, "Danh sách chứng từ", "Danh sách các chứng từ kế toán đã lập.");
            UiStyler.SetAccessibleText(dgvXemChiTiet, "Chi tiết chứng từ đã chọn", "Các dòng định khoản của chứng từ đang chọn.");
            ConfigureGridColumns();
        }

        private void ConfigureGridColumns()
        {
            // === Lưới Danh Sách Chứng Từ ===
            dgvDanhSachChungTu.AutoGenerateColumns = false;
            dgvDanhSachChungTu.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            colMaCT_DS.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colMaCT_DS.Width = 100;
            colMaCT_DS.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            colNgayCT_DS.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colNgayCT_DS.Width = 140;
            colNgayCT_DS.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colNgayCT_DS.DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";

            colLoaiCT_DS.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colLoaiCT_DS.Width = 150;
            colLoaiCT_DS.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            colMaHDB_DS.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colMaHDB_DS.Width = 110;
            colMaHDB_DS.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            colTenKH_DS.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colTenKH_DS.FillWeight = 150F;
            colTenKH_DS.MinimumWidth = 170;
            colTenKH_DS.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            colTongTien_DS.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colTongTien_DS.Width = 135;
            colTongTien_DS.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colTongTien_DS.DefaultCellStyle.Format = "N0";

            colTenNV_DS.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colTenNV_DS.Width = 140;
            colTenNV_DS.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            colDienGiai_DS.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colDienGiai_DS.FillWeight = 180F;
            colDienGiai_DS.MinimumWidth = 180;
            colDienGiai_DS.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            // === Lưới Chi Tiết Lập Định Khoản ===
            dgvChiTiet.AutoGenerateColumns = false;
            dgvChiTiet.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            colSTT.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colSTT.Width = 45;
            colSTT.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            colTaiKhoanNo.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colTaiKhoanNo.Width = 85;
            colTaiKhoanNo.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colTaiKhoanNo.DefaultCellStyle.Font = new Font(dgvChiTiet.Font, FontStyle.Bold);

            colTaiKhoanCo.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colTaiKhoanCo.Width = 85;
            colTaiKhoanCo.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colTaiKhoanCo.DefaultCellStyle.Font = new Font(dgvChiTiet.Font, FontStyle.Bold);

            colSoTienChiTiet.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colSoTienChiTiet.Width = 135;
            colSoTienChiTiet.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colSoTienChiTiet.DefaultCellStyle.Format = "N0";

            colDienGiaiChiTiet.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colDienGiaiChiTiet.FillWeight = 200F;
            colDienGiaiChiTiet.MinimumWidth = 200;
            colDienGiaiChiTiet.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            // === Lưới Xem Chi Tiết Định Khoản Đã Lập ===
            dgvXemChiTiet.AutoGenerateColumns = false;
            dgvXemChiTiet.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            colXemSTT.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colXemSTT.Width = 45;
            colXemSTT.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            colXemNo.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colXemNo.Width = 85;
            colXemNo.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colXemNo.DefaultCellStyle.Font = new Font(dgvXemChiTiet.Font, FontStyle.Bold);

            colXemCo.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colXemCo.Width = 85;
            colXemCo.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colXemCo.DefaultCellStyle.Font = new Font(dgvXemChiTiet.Font, FontStyle.Bold);

            colXemSoTien.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colXemSoTien.Width = 135;
            colXemSoTien.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colXemSoTien.DefaultCellStyle.Format = "N0";

            colXemDienGiai.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colXemDienGiai.FillWeight = 200F;
            colXemDienGiai.MinimumWidth = 200;
            colXemDienGiai.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
        }

        private void splitContainerDS_Resize(object sender, EventArgs e)
        {
            AdjustHistorySplit();
        }

        private void AdjustHistorySplit()
        {
            int available = splitContainerDS.ClientSize.Height - splitContainerDS.SplitterWidth;
            if (available < splitContainerDS.Panel1MinSize + splitContainerDS.Panel2MinSize)
                return;

            int desired = (int)(available * 0.6F);
            int maximum = available - splitContainerDS.Panel2MinSize;
            splitContainerDS.SplitterDistance = Math.Min(maximum, Math.Max(splitContainerDS.Panel1MinSize, desired));
        }

        private async void frmChungTu_Load(object sender, EventArgs e)
        {
            this.ApplyResponsiveUI();
            if (!SessionManager.IsAdmin() && !SessionManager.IsAccountant())
            {
                MessageBox.Show("Bạn không có quyền truy cập vào chức năng Quản lý Chứng từ kế toán.\nChỉ Quản trị viên và Kế toán mới có quyền này.",
                    "Từ chối truy cập", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
                return;
            }
            ApplyAuthorization();
            if (cboLoaiCT.Items.Count > 0)
                cboLoaiCT.SelectedIndex = 0;

            dtpFromDate.Value = DateTime.Today.AddDays(-30);
            dtpToDate.Value = DateTime.Today;

            ResetForm();
            await LoadHoaDonComboAsync();
            await _searchDebouncer.RunNowAsync(
                delegate(int version) { return LoadDanhSachChungTuAsync(version, null); });
        }

        private void ApplyAuthorization()
        {
            bool canWrite = SessionManager.IsAdmin() || SessionManager.IsAccountant();
            btnLuuChungTu.Enabled = canWrite;
            btnThemDong.Enabled = canWrite;
            btnXoaDong.Enabled = canWrite;
            btnGoiY.Enabled = canWrite;

            if (!canWrite)
            {
                tabControlChungTu.SelectedTab = tabDanhSach;
                tabLapChungTu.Text = "Lập Chứng Từ (Chỉ xem)";
                btnLuuChungTu.Visible = false;
                btnThemDong.Visible = false;
                btnXoaDong.Visible = false;
                btnGoiY.Visible = false;
            }
        }

        private void ResetForm()
        {
            txtMaCT.Text = _accountingService.GenerateNewDocumentId();
            dtpNgayCT.Value = DateTime.Now;
            if (cboLoaiCT.Items.Count > 0)
                cboLoaiCT.SelectedIndex = 0;

            txtDienGiai.Text = "Ghi nhận doanh thu bán hàng theo hóa đơn";
            dgvChiTiet.Rows.Clear();
            RecalculateTotals();
            btnLuuChungTu.Enabled = SessionManager.IsAdmin() || SessionManager.IsAccountant();
            _validationErrors.Clear();
            UiStyler.StyleStatusLabel(_entryStatus, UiStatusKind.Neutral, "Chọn hóa đơn và kiểm tra các dòng định khoản trước khi lưu.");
        }

        private async Task LoadHoaDonComboAsync()
        {
            try
            {
                _isBinding = true;
                DataTable invoices = null;
                UiStyler.StyleStatusLabel(_entryStatus, UiStatusKind.Information, "Đang tải hóa đơn có thể lập chứng từ...");
                await UiFeedbackHelper.RunBusyAsync(
                    this,
                    null,
                    null,
                    delegate
                    {
                        invoices = _chungTuDAL.GetHoaDonSanSangLapChungTu();
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
                    decimal tongTien = Convert.ToDecimal(row["TongTien"]);
                    row["DisplayText"] = string.Format("{0} - {1} ({2:N0} VNĐ)", maHDB, tenKH, tongTien);
                }

                cboHoaDon.DataSource = comboSource;
                cboHoaDon.DisplayMember = "DisplayText";
                cboHoaDon.ValueMember = "MaHDB";

                if (comboSource.Rows.Count > 0)
                {
                    cboHoaDon.SelectedIndex = 0;
                    UpdateHoaDonSelection();
                    UiStyler.StyleStatusLabel(_entryStatus, UiStatusKind.Neutral, "Chọn hóa đơn và kiểm tra các dòng định khoản trước khi lưu.");
                }
                else
                {
                    lblThongTinHDB.Text = "Không có hóa đơn khả dụng để lập chứng từ";
                    btnLuuChungTu.Enabled = false;
                    UiStyler.StyleStatusLabel(_entryStatus, UiStatusKind.Warning, "Không có hóa đơn khả dụng để lập chứng từ.");
                }
            }
            catch (Exception ex)
            {
                AppLogger.Error("LoadHoaDonCombo", "Lỗi tải danh sách hóa đơn khả dụng: " + ex.Message, ex);
                UiStyler.StyleStatusLabel(_entryStatus, UiStatusKind.Error, "Không thể tải hóa đơn có thể lập chứng từ.");
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
            string selectedMaHDB = cboHoaDon.SelectedValue.ToString();

            DataRow foundRow = null;
            if (_dtHoaDon != null)
            {
                foreach (DataRow r in _dtHoaDon.Rows)
                {
                    if (r["MaHDB"].ToString().Trim() == selectedMaHDB.Trim())
                    {
                        foundRow = r;
                        break;
                    }
                }
            }

            if (foundRow != null)
            {
                string tenKH = foundRow["TenKH"].ToString().Trim();
                decimal tongTien = Convert.ToDecimal(foundRow["TongTien"]);
                DateTime ngayLap = Convert.ToDateTime(foundRow["NgayLap"]);

                lblThongTinHDB.Text = string.Format("Khách hàng: {0} | Tổng tiền: {1:N0} VNĐ | Ngày HĐ: {2:dd/MM/yyyy}",
                    tenKH, tongTien, ngayLap);

                txtDienGiai.Text = string.Format("Ghi nhận doanh thu bán hàng theo hóa đơn {0} ({1})", selectedMaHDB, tenKH);

                if (dgvChiTiet.Rows.Count == 0)
                {
                    GenerateDefaultAccountingEntries(tongTien, selectedMaHDB);
                }
                _validationErrors.Clear();
                UiStyler.StyleStatusLabel(_entryStatus, UiStatusKind.Neutral, "Đã nạp hóa đơn. Kiểm tra định khoản trước khi lưu.");
            }
        }

        private void GenerateDefaultAccountingEntries(decimal tongTien, string maHDB)
        {
            dgvChiTiet.Rows.Clear();
            dgvChiTiet.Rows.Add(
                1,
                "131",
                "511",
                tongTien.ToString("N0"),
                string.Format("Phải thu KH / Doanh thu bán hàng HĐ {0}", maHDB)
            );
            RecalculateTotals();
        }

        private void btnGoiY_Click(object sender, EventArgs e)
        {
            if (cboHoaDon.SelectedValue == null)
            {
                ShowEntryValidation(cboHoaDon, "Vui lòng chọn hóa đơn trước khi gợi ý định khoản.");
                return;
            }

            string selectedMaHDB = cboHoaDon.SelectedValue.ToString();
            DataRow foundRow = null;
            if (_dtHoaDon != null)
            {
                foreach (DataRow r in _dtHoaDon.Rows)
                {
                    if (r["MaHDB"].ToString().Trim() == selectedMaHDB.Trim())
                    {
                        foundRow = r;
                        break;
                    }
                }
            }

            if (foundRow != null)
            {
                decimal tongTien = Convert.ToDecimal(foundRow["TongTien"]);
                GenerateDefaultAccountingEntries(tongTien, selectedMaHDB);
            }
        }

        private void btnThemDong_Click(object sender, EventArgs e)
        {
            int nextStt = dgvChiTiet.Rows.Count + 1;
            string maHDB = cboHoaDon.SelectedValue != null ? cboHoaDon.SelectedValue.ToString() : "";
            dgvChiTiet.Rows.Add(nextStt, "131", "511", "0", string.Format("Định khoản chi tiết HĐ {0}", maHDB));
            RecalculateTotals();
        }

        private void btnXoaDong_Click(object sender, EventArgs e)
        {
            if (dgvChiTiet.CurrentRow != null && !dgvChiTiet.CurrentRow.IsNewRow)
            {
                dgvChiTiet.Rows.Remove(dgvChiTiet.CurrentRow);
                RenumberSTT();
                RecalculateTotals();
            }
            else
            {
                ShowEntryValidation(dgvChiTiet, "Vui lòng chọn dòng định khoản cần xóa.");
            }
        }

        private void RenumberSTT()
        {
            for (int i = 0; i < dgvChiTiet.Rows.Count; i++)
            {
                dgvChiTiet.Rows[i].Cells["colSTT"].Value = i + 1;
            }
        }

        private void dgvChiTiet_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                RecalculateTotals();
            }
        }

        private void dgvChiTiet_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            RecalculateTotals();
        }

        private void RecalculateTotals()
        {
            decimal tongSoTien = 0;
            foreach (DataGridViewRow row in dgvChiTiet.Rows)
            {
                if (row.IsNewRow) continue;
                object valObj = row.Cells["colSoTienChiTiet"].Value;
                if (valObj != null)
                {
                    string raw = valObj.ToString().Replace(",", "").Replace(".", "").Trim();
                    decimal val;
                    if (decimal.TryParse(raw, NumberStyles.Any, CultureInfo.InvariantCulture, out val))
                    {
                        tongSoTien += val;
                    }
                }
            }

            lblTongNo.Text = string.Format("Tổng Nợ: {0:N0} VNĐ", tongSoTien);
            lblTongCo.Text = string.Format("Tổng Có: {0:N0} VNĐ", tongSoTien);
            lblCanDoi.Text = "Cân đối Nợ/Có: Hợp lệ (100%)";
            if (!SystemInformation.HighContrast)
                lblCanDoi.ForeColor = UiTheme.Success;
        }

        private async void btnLuuChungTu_Click(object sender, EventArgs e)
        {
            if (!SessionManager.IsAdmin() && !SessionManager.IsAccountant())
            {
                MessageBox.Show("Bạn không có quyền lập chứng từ kế toán!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cboHoaDon.SelectedValue == null)
            {
                ShowEntryValidation(cboHoaDon, "Vui lòng chọn hóa đơn bán hàng để lập chứng từ.");
                return;
            }

            string maHDB = cboHoaDon.SelectedValue.ToString().Trim();
            if (string.IsNullOrEmpty(maHDB))
            {
                ShowEntryValidation(cboHoaDon, "Mã hóa đơn không hợp lệ.");
                return;
            }

            if (dgvChiTiet.Rows.Count == 0)
            {
                ShowEntryValidation(dgvChiTiet, "Vui lòng thêm ít nhất một dòng định khoản chi tiết.");
                return;
            }

            List<ChiTietChungTu> chiTietList = new List<ChiTietChungTu>();
            decimal tongTienDinhKhoan = 0;

            for (int i = 0; i < dgvChiTiet.Rows.Count; i++)
            {
                DataGridViewRow row = dgvChiTiet.Rows[i];
                if (row.IsNewRow) continue;

                string tkNo = row.Cells["colTaiKhoanNo"].Value != null ? row.Cells["colTaiKhoanNo"].Value.ToString().Trim() : "";
                string tkCo = row.Cells["colTaiKhoanCo"].Value != null ? row.Cells["colTaiKhoanCo"].Value.ToString().Trim() : "";
                string dienGiaiCT = row.Cells["colDienGiaiChiTiet"].Value != null ? row.Cells["colDienGiaiChiTiet"].Value.ToString().Trim() : "";
                string soTienStr = row.Cells["colSoTienChiTiet"].Value != null ? row.Cells["colSoTienChiTiet"].Value.ToString().Replace(",", "").Replace(".", "").Trim() : "0";

                if (string.IsNullOrEmpty(tkNo))
                {
                    ShowGridValidation(i, "colTaiKhoanNo", string.Format("Dòng {0}: Tài khoản Nợ không được để trống.", i + 1));
                    return;
                }

                if (string.IsNullOrEmpty(tkCo))
                {
                    ShowGridValidation(i, "colTaiKhoanCo", string.Format("Dòng {0}: Tài khoản Có không được để trống.", i + 1));
                    return;
                }

                decimal soTien;
                if (!decimal.TryParse(soTienStr, NumberStyles.Any, CultureInfo.InvariantCulture, out soTien) || soTien <= 0)
                {
                    ShowGridValidation(i, "colSoTienChiTiet", string.Format("Dòng {0}: Số tiền phải lớn hơn 0 VNĐ.", i + 1));
                    return;
                }

                tongTienDinhKhoan += soTien;

                chiTietList.Add(new ChiTietChungTu
                {
                    STT = i + 1,
                    TaiKhoanNo = tkNo,
                    TaiKhoanCo = tkCo,
                    SoTien = soTien,
                    DienGiai = dienGiaiCT
                });
            }

            DialogResult dr = MessageBox.Show(
                string.Format("Xác nhận lưu chứng từ kế toán cho hóa đơn {0} với tổng tiền hạch toán {1:N0} VNĐ ({2} dòng định khoản)?",
                    maHDB, tongTienDinhKhoan, chiTietList.Count),
                "Xác nhận lưu chứng từ",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (dr != DialogResult.Yes)
                return;

            try
            {
                ChungTu chungTu = new ChungTu
                {
                    MaHDB = maHDB,
                    NgayCT = dtpNgayCT.Value,
                    LoaiCT = cboLoaiCT.SelectedItem != null ? cboLoaiCT.SelectedItem.ToString() : "Chứng từ bán hàng",
                    DienGiai = txtDienGiai.Text.Trim()
                };

                string errorMessage = string.Empty;
                UiStyler.StyleStatusLabel(_entryStatus, UiStatusKind.Information, "Đang kiểm tra cân đối và ghi nhận chứng từ...");
                bool ok = await UiFeedbackHelper.RunBusyAsync(
                    this,
                    btnLuuChungTu,
                    "ĐANG GHI NHẬN...",
                    delegate { return _accountingService.CreateChungTu(chungTu, chiTietList, out errorMessage); });
                if (IsDisposed)
                {
                    return;
                }

                if (ok)
                {
                    _lastCreatedMaCT = chungTu.MaCT;
                    DialogResult printPrompt = MessageBox.Show(
                        string.Format("Lập chứng từ kế toán thành công!\nMã chứng từ: {0}\nHóa đơn: {1}\nTổng tiền: {2:N0} VNĐ\n\nBạn có muốn in chứng từ kế toán ngay không?",
                            chungTu.MaCT, maHDB, tongTienDinhKhoan),
                        "Lập chứng từ thành công",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Information);

                    if (printPrompt == DialogResult.Yes)
                    {
                        InChungTu(chungTu.MaCT);
                    }

                    ResetForm();
                    await LoadHoaDonComboAsync();
                    await _searchDebouncer.RunNowAsync(
                        delegate(int version) { return LoadDanhSachChungTuAsync(version, null); });
                }
                else
                {
                    MessageBox.Show(errorMessage, "Lỗi lập chứng từ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                AppLogger.Error("btnLuuChungTu_Click", "Lỗi lưu chứng từ kế toán: " + ex.Message, ex);
                MessageBox.Show("Đã xảy ra sự cố không mong muốn trong quá trình lập chứng từ. Vui lòng thử lại sau.", "Lỗi Hệ Thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnLamMoi_Click(object sender, EventArgs e)
        {
            ResetForm();
            await LoadHoaDonComboAsync();
        }

        private async Task LoadDanhSachChungTuAsync(int requestVersion, Button actionButton)
        {
            DateTime from = dtpFromDate.Value.Date;
            DateTime to = dtpToDate.Value.Date.AddDays(1).AddSeconds(-1);
            string keyword = txtTimKiem.Text.Trim();
            UiStyler.SetGridLoading(dgvDanhSachChungTu, "Đang tải danh sách chứng từ...");
            try
            {
                PagedDataTable paged = null;
                await UiFeedbackHelper.RunBusyAsync(
                    this,
                    actionButton,
                    "ĐANG TẢI...",
                    delegate
                    {
                        paged = _chungTuDAL.SearchPhanTrang(
                            keyword, from, to,
                            _pagerChungTu.CurrentPage, _pagerChungTu.PageSize);
                        return true;
                    });
                if (IsDisposed || !_searchDebouncer.IsCurrent(requestVersion))
                {
                    return;
                }

                DataTable dt = paged != null ? paged.Table : null;
                _isBindingHistory = true;
                dgvDanhSachChungTu.AutoGenerateColumns = false;
                dgvDanhSachChungTu.DataSource = dt;
                _isBindingHistory = false;

                colMaCT_DS.DataPropertyName = "MaCT";
                colNgayCT_DS.DataPropertyName = "NgayCT";
                colLoaiCT_DS.DataPropertyName = "LoaiCT";
                colMaHDB_DS.DataPropertyName = "MaHDB";
                colTenKH_DS.DataPropertyName = "TenKH";
                colTongTien_DS.DataPropertyName = "TongTienHDB";
                colTenNV_DS.DataPropertyName = "TenNV";
                colDienGiai_DS.DataPropertyName = "DienGiai";

                int total = paged != null ? paged.TotalRecords : 0;
                _pagerChungTu.UpdateState(_pagerChungTu.CurrentPage, _pagerChungTu.PageSize, total);

                UiStyler.ClearGridState(dgvDanhSachChungTu);
                UiStyler.UpdateGridEmptyState(dgvDanhSachChungTu, "Không tìm thấy chứng từ phù hợp.");

                if (dt != null && dt.Rows.Count > 0)
                {
                    string firstMaCT = dt.Rows[0]["MaCT"].ToString();
                    await _detailDebouncer.RunNowAsync(
                        delegate(int version) { return LoadChiTietChungTuAsync(version, firstMaCT); });
                }
                else
                {
                    dgvXemChiTiet.DataSource = null;
                    UiStyler.UpdateGridEmptyState(dgvXemChiTiet, "Chưa có chi tiết chứng từ để hiển thị.");
                }
            }
            catch (Exception ex)
            {
                _isBindingHistory = false;
                if (!_searchDebouncer.IsCurrent(requestVersion))
                {
                    return;
                }
                UiStyler.SetGridError(dgvDanhSachChungTu, "Không thể tải danh sách chứng từ.");
                AppLogger.Error("LoadDanhSachChungTu", "Lỗi nạp danh sách chứng từ: " + ex.Message, ex);
                UiErrorHandler.Show(this, "FRMCHUNGTU_UI_ERROR", "Không thể tải danh sách chứng từ.", ex);
            }
        }

        private async void dgvDanhSachChungTu_SelectionChanged(object sender, EventArgs e)
        {
            if (!_isBindingHistory && dgvDanhSachChungTu.CurrentRow != null && dgvDanhSachChungTu.CurrentRow.Index >= 0)
            {
                object maCTVal = dgvDanhSachChungTu.CurrentRow.Cells["colMaCT_DS"].Value;
                if (maCTVal != null && maCTVal != DBNull.Value)
                {
                    string maCT = maCTVal.ToString();
                    await _detailDebouncer.RunNowAsync(
                        delegate(int version) { return LoadChiTietChungTuAsync(version, maCT); });
                }
            }
        }

        private async Task LoadChiTietChungTuAsync(int requestVersion, string maCT)
        {
            UiStyler.SetGridLoading(dgvXemChiTiet, "Đang tải chi tiết chứng từ...");
            try
            {
                DataTable dtCT = null;
                await UiFeedbackHelper.RunBusyAsync(
                    this,
                    null,
                    null,
                    delegate
                    {
                        dtCT = _chungTuDAL.GetChiTietDataTable(maCT);
                        return true;
                    });
                if (IsDisposed || !_detailDebouncer.IsCurrent(requestVersion))
                {
                    return;
                }
                dgvXemChiTiet.AutoGenerateColumns = false;
                dgvXemChiTiet.DataSource = dtCT;

                colXemSTT.DataPropertyName = "STT";
                colXemNo.DataPropertyName = "TaiKhoanNo";
                colXemCo.DataPropertyName = "TaiKhoanCo";
                colXemSoTien.DataPropertyName = "SoTien";
                colXemDienGiai.DataPropertyName = "DienGiai";
                UiStyler.ClearGridState(dgvXemChiTiet);
                UiStyler.UpdateGridEmptyState(dgvXemChiTiet, "Chứng từ không có dòng định khoản.");
            }
            catch (Exception ex)
            {
                if (!_detailDebouncer.IsCurrent(requestVersion))
                {
                    return;
                }
                UiStyler.SetGridError(dgvXemChiTiet, "Không thể tải chi tiết chứng từ.");
                AppLogger.Error("LoadChiTietChungTu", "Lỗi nạp chi tiết chứng từ " + maCT + ": " + ex.Message, ex);
            }
        }

        private async void btnTimKiem_Click(object sender, EventArgs e)
        {
            _pagerChungTu.Reset(_pagerChungTu.PageSize);
            await _searchDebouncer.RunNowAsync(
                delegate(int version) { return LoadDanhSachChungTuAsync(version, btnTimKiem); });
        }

        private async void btnLamMoiDS_Click(object sender, EventArgs e)
        {
            txtTimKiem.Clear();
            dtpFromDate.Value = DateTime.Today.AddDays(-30);
            dtpToDate.Value = DateTime.Today;
            _pagerChungTu.Reset(_pagerChungTu.PageSize);
            await _searchDebouncer.RunNowAsync(
                delegate(int version) { return LoadDanhSachChungTuAsync(version, btnLamMoiDS); });
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            _searchDebouncer.Restart(
                delegate(int version)
                {
                    _pagerChungTu.Reset(_pagerChungTu.PageSize);
                    return LoadDanhSachChungTuAsync(version, null);
                });
        }

        private void dgvDanhSachChungTu_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            string colName = dgvDanhSachChungTu.Columns[e.ColumnIndex].Name;
            if (colName == "colTongTien_DS" && e.Value != null && e.Value != DBNull.Value)
            {
                decimal val;
                if (decimal.TryParse(e.Value.ToString(), out val))
                {
                    e.Value = string.Format("{0:N0} VNĐ", val);
                    e.FormattingApplied = true;
                }
                e.CellStyle.Font = new Font(dgvDanhSachChungTu.Font, FontStyle.Bold);
            }
            else if (colName == "colNgayCT_DS" && e.Value != null && e.Value != DBNull.Value)
            {
                DateTime dt;
                if (DateTime.TryParse(e.Value.ToString(), out dt))
                {
                    e.Value = dt.ToString("dd/MM/yyyy HH:mm");
                    e.FormattingApplied = true;
                }
            }
        }

        private void dgvXemChiTiet_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            string colName = dgvXemChiTiet.Columns[e.ColumnIndex].Name;
            if (colName == "colXemSoTien" && e.Value != null && e.Value != DBNull.Value)
            {
                decimal val;
                if (decimal.TryParse(e.Value.ToString(), out val))
                {
                    e.Value = string.Format("{0:N0} VNĐ", val);
                    e.FormattingApplied = true;
                }
                e.CellStyle.Font = new Font(dgvXemChiTiet.Font, FontStyle.Bold);
                e.CellStyle.ForeColor = Color.FromArgb(0, 100, 180);
            }
        }

        private void ShowEntryValidation(Control control, string message)
        {
            UiInteractionHelper.ShowValidationError(_validationErrors, control, message);
            UiStyler.StyleStatusLabel(_entryStatus, UiStatusKind.Warning, message);
        }

        private void ShowGridValidation(int rowIndex, string columnName, string message)
        {
            _validationErrors.Clear();
            _validationErrors.SetError(dgvChiTiet, message);
            UiStyler.StyleStatusLabel(_entryStatus, UiStatusKind.Warning, message);
            dgvChiTiet.CurrentCell = dgvChiTiet.Rows[rowIndex].Cells[columnName];
            dgvChiTiet.BeginEdit(true);
        }

        private void InChungTuDangChon()
        {
            if (dgvDanhSachChungTu.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn một chứng từ kế toán cần in!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string maCT = dgvDanhSachChungTu.CurrentRow.Cells["colMaCT_DS"].Value != null
                ? dgvDanhSachChungTu.CurrentRow.Cells["colMaCT_DS"].Value.ToString().Trim()
                : string.Empty;

            InChungTu(maCT);
        }

        private void InChungTu(string maCT)
        {
            if (string.IsNullOrEmpty(maCT))
            {
                MessageBox.Show("Vui lòng chỉ định mã chứng từ cần in!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                ChungTu ct = _chungTuDAL.GetById(maCT);
                if (ct == null)
                {
                    MessageBox.Show("Không tìm thấy dữ liệu chứng từ " + maCT, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DataTable dtChiTiet = _chungTuDAL.GetChiTietDataTable(maCT);
                string html = VoucherPrintHelper.GenerateChungTuHtml(ct, dtChiTiet);
                frmInChungTu.ShowVoucher(this, "Chứng Từ Kế Toán - " + maCT, html, "ChungTu_" + maCT);
            }
            catch (Exception ex)
            {
                AppLogger.Error("PRINT_CT_ERROR", "Lỗi khi xuất mẫu in chứng từ: " + ex.Message, ex);
                MessageBox.Show("Lỗi khi mở mẫu in chứng từ kế toán: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
