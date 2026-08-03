using System;
using System.Collections.Generic;
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
    public partial class frmPhieuXuatKho : Form
    {
        private readonly WarehouseService _warehouseService;
        private readonly PhieuXuatKhoDAL _phieuXuatKhoDAL;
        private readonly HoaDonBanDAL _hoaDonBanDAL;
        private readonly KhoDAL _khoDAL;
        private readonly TonKhoDAL _tonKhoDAL;
        private readonly ErrorProvider _validationErrors;
        private readonly UiDebouncer _searchDebouncer;
        private readonly UiDebouncer _entryDebouncer;
        private readonly UiDebouncer _historyDetailDebouncer;

        private DataTable _dtChiTiet;
        private bool _isBinding = false;
        private bool _isCorrectingQuantity = false;
        private Label _issueStatus;
        private readonly string _initialMaHDB;
        private Button btnInPhieuTab1;
        private Button btnInPhieuDS;
        private ContextMenuStrip cmsPhieuXuat;
        private string _lastCreatedMaPXK = null;

        public frmPhieuXuatKho() : this(null)
        {
        }

        public frmPhieuXuatKho(string initialMaHDB)
        {
            _initialMaHDB = initialMaHDB;
            InitializeComponent();
            if (!string.IsNullOrEmpty(initialMaHDB))
            {
                this.StartPosition = FormStartPosition.CenterParent;
                this.Text = string.Format("Lập Phiếu Xuất Kho - Hóa đơn [{0}]", initialMaHDB);
                this.MaximizeBox = false;
                this.MinimizeBox = false;
                this.ShowInTaskbar = false;
                this.Size = new Size(1000, 700);
                lblTitle.Text = "LẬP PHIẾU XUẤT KHO";
                lblSubTitle.Text = string.Format("Xác nhận và xuất kho hàng hóa theo hóa đơn bán [{0}]", initialMaHDB);

                if (tcPhieuXuat.TabPages.Contains(tpDanhSach))
                {
                    tcPhieuXuat.TabPages.Remove(tpDanhSach);
                }
                tcPhieuXuat.Appearance = TabAppearance.FlatButtons;
                tcPhieuXuat.ItemSize = new Size(0, 1);
                tcPhieuXuat.SizeMode = TabSizeMode.Fixed;

                btnLamMoi.Visible = false;
            }
            _validationErrors = UiStyler.CreateErrorProvider(this);
            BuildResponsiveLayout();
            ApplyFoundationDesign();
            _warehouseService = new WarehouseService();
            _phieuXuatKhoDAL = new PhieuXuatKhoDAL();
            _hoaDonBanDAL = new HoaDonBanDAL();
            _khoDAL = new KhoDAL();
            _tonKhoDAL = new TonKhoDAL();
            _searchDebouncer = new UiDebouncer(350);
            _entryDebouncer = new UiDebouncer(100);
            _historyDetailDebouncer = new UiDebouncer(100);
            txtTimKiem.TextChanged += txtTimKiem_TextChanged;
            Disposed += delegate
            {
                _searchDebouncer.Dispose();
                _entryDebouncer.Dispose();
                _historyDetailDebouncer.Dispose();
            };

            InitChiTietTable();
        }

        public void SelectTab(int index)
        {
            if (index >= 0 && index < tcPhieuXuat.TabPages.Count)
            {
                tcPhieuXuat.SelectedIndex = index;
                if (index == 1 && txtTimKiem != null && txtTimKiem.CanFocus)
                {
                    txtTimKiem.Focus();
                }
            }
        }

        private void BuildResponsiveLayout()
        {
            SuspendLayout();
            MinimumSize = new Size(800, 640);
            BuildIssueInformationLayout();
            BuildIssueFooterLayout();
            BuildHistoryFilterLayout();
            ResumeLayout(true);
        }

        private void BuildIssueInformationLayout()
        {
            grpThongTin.Controls.Clear();
            grpThongTin.AutoSize = true;
            grpThongTin.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            grpThongTin.Padding = new Padding(12, 10, 12, 10);

            TableLayoutPanel fields = UiLayoutBuilder.CreateStructuredGridLayout(
                3, 95,
                UiLayoutBuilder.Field(lblMaPXK, txtMaPXK, UiFieldSize.Code),
                UiLayoutBuilder.Field(lblNgayXuat, dtpNgayXuat, UiFieldSize.DateTime),
                UiLayoutBuilder.Field(lblKho, cboKho, UiFieldSize.Selection),
                UiLayoutBuilder.Field(lblHoaDon, cboHoaDon, UiFieldSize.Wide),
                UiLayoutBuilder.Field(lblKHTag, lblKhachHangInfo, UiFieldSize.Wide),
                UiLayoutBuilder.Field(lblNVTag, lblNhanVienLap, UiFieldSize.Wide),
                UiLayoutBuilder.FullWidthField(lblLyDoXuat, txtLyDoXuat));

            lblKhachHangInfo.TextAlign = ContentAlignment.MiddleLeft;
            lblKhachHangInfo.AutoEllipsis = true;
            lblNhanVienLap.TextAlign = ContentAlignment.MiddleLeft;
            lblNhanVienLap.AutoEllipsis = true;
            grpThongTin.Controls.Add(fields);
        }

        private void BuildIssueFooterLayout()
        {
            pnlBottom.Controls.Clear();
            pnlBottom.Height = 68;
            pnlBottom.Padding = new Padding(12, 5, 10, 5);

            TableLayoutPanel layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 2
            };
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 28F));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            lblTongSoLuongXuat.Dock = DockStyle.Fill;
            lblTongSoLuongXuat.TextAlign = ContentAlignment.MiddleLeft;
            lblTongSoLuongXuat.Margin = new Padding(0);
            _issueStatus = new Label
            {
                Name = "lblIssueStatus",
                Dock = DockStyle.Fill,
                AutoEllipsis = true,
                TextAlign = ContentAlignment.MiddleLeft,
                Margin = new Padding(0)
            };

            FlowLayoutPanel actions = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoSize = true,
                WrapContents = false,
                Padding = new Padding(8, 0, 0, 0)
            };
            btnInPhieuTab1 = UiStyler.CreateButton("🖨️ In phiếu", Color.FromArgb(70, 80, 95));
            btnInPhieuTab1.Margin = new Padding(0, 0, 8, 0);
            btnInPhieuTab1.Click += delegate
            {
                string maPXK = !string.IsNullOrEmpty(txtMaPXK.Text.Trim())
                    ? txtMaPXK.Text.Trim()
                    : _lastCreatedMaPXK;
                if (!string.IsNullOrEmpty(maPXK))
                {
                    InPhieuXuat(maPXK);
                }
                else
                {
                    MessageBox.Show("Chưa có phiếu xuất nào được chọn hoặc vừa tạo. Vui lòng lập phiếu hoặc chọn từ danh sách phiếu xuất để in.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            };

            btnXuatKho.Margin = new Padding(0, 0, 8, 0);
            btnLamMoi.Margin = new Padding(0, 0, 8, 0);
            btnDong.Margin = new Padding(0);
            actions.Controls.Add(btnXuatKho);
            actions.Controls.Add(btnInPhieuTab1);
            actions.Controls.Add(btnLamMoi);
            actions.Controls.Add(btnDong);

            layout.Controls.Add(lblTongSoLuongXuat, 0, 0);
            layout.Controls.Add(_issueStatus, 0, 1);
            layout.Controls.Add(actions, 1, 0);
            layout.SetRowSpan(actions, 2);
            pnlBottom.Controls.Add(layout);
        }

        private void BuildHistoryFilterLayout()
        {
            pnlFilter.Controls.Clear();
            pnlFilter.AutoSize = true;
            pnlFilter.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            FlowLayoutPanel filters = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                WrapContents = true,
                Padding = new Padding(12, 7, 8, 3)
            };
            Button btnXuatCsv = CsvExporter.CreateExportButton(dgvDanhSachPhieuXuat, "DanhSachPhieuXuatKho", "DANH SÁCH PHIẾU XUẤT KHO");

            btnInPhieuDS = UiStyler.CreateButton("🖨️ In phiếu xuất", UiTheme.Secondary);
            btnInPhieuDS.Click += delegate { InPhieuXuatDangChon(); };

            cmsPhieuXuat = new ContextMenuStrip();
            ToolStripMenuItem miInPhieu = new ToolStripMenuItem("🖨️ In phiếu xuất kho (Mẫu 02-VT)...");
            miInPhieu.Click += delegate { InPhieuXuatDangChon(); };
            cmsPhieuXuat.Items.Add(miInPhieu);
            dgvDanhSachPhieuXuat.ContextMenuStrip = cmsPhieuXuat;

            Control[] controls = { lblFilterKho, cboFilterKho, lblTimKiem, txtTimKiem, btnTimKiem, btnLamMoiDanhSach, btnInPhieuDS, btnXuatCsv, lblSoPhieu };
            foreach (Control control in controls)
            {
                control.Anchor = AnchorStyles.None;
                control.Margin = control is Label
                    ? new Padding(0, 9, 6, 6)
                    : new Padding(0, 0, 10, 6);
                filters.Controls.Add(control);
            }
            cboFilterKho.MinimumSize = new Size(180, UiTheme.ControlHeight);
            txtTimKiem.MinimumSize = new Size(210, UiTheme.ControlHeight);
            pnlFilter.Controls.Add(filters);
        }

        private void ApplyFoundationDesign()
        {
            lblSubTitle.Text = "Xuất hàng theo hóa đơn, kiểm tra số lượng còn lại và tồn thực tế tại kho";
            UiStyler.Apply(this);

            if (!SystemInformation.HighContrast)
            {
                pnlHeader.BackColor = UiTheme.Primary;
                lblTitle.ForeColor = Color.White;
                lblSubTitle.ForeColor = Color.FromArgb(237, 233, 254);
                tpLapPhieu.BackColor = UiTheme.Canvas;
                tpDanhSach.BackColor = UiTheme.Canvas;
                pnlBottom.BackColor = UiTheme.SurfaceMuted;
                pnlFilter.BackColor = UiTheme.Surface;
                lblTongSoLuongXuat.ForeColor = UiTheme.Information;
                lblKhachHangInfo.ForeColor = UiTheme.Information;
                lblNhanVienLap.ForeColor = UiTheme.Success;
                colSoLuongXuat.DefaultCellStyle.BackColor = Color.FromArgb(254, 249, 195);
                colSoLuongXuat.DefaultCellStyle.SelectionBackColor = Color.FromArgb(253, 230, 138);
                colSoLuongXuat.DefaultCellStyle.SelectionForeColor = UiTheme.TextPrimary;
            }

            dgvChiTiet.SelectionMode = DataGridViewSelectionMode.CellSelect;
            UiStyler.StyleButton(btnXuatKho, UiButtonRole.Success);
            UiStyler.StyleButton(btnLamMoi, UiButtonRole.Secondary);
            UiStyler.StyleButton(btnDong, UiButtonRole.Secondary);
            UiStyler.StyleButton(btnTimKiem, UiButtonRole.Primary);
            UiStyler.StyleButton(btnLamMoiDanhSach, UiButtonRole.Secondary);
            CsvExporter.AttachExportContextMenu(dgvDanhSachPhieuXuat, "DanhSachPhieuXuatKho", "DANH SÁCH PHIẾU XUẤT KHO");
            UiStyler.StyleStatusLabel(_issueStatus, UiStatusKind.Neutral, "Nhập số lượng cần xuất tại cột được tô màu.");
            UiStyler.StyleStatusLabel(lblSoPhieu, UiStatusKind.Neutral, "Chưa có dữ liệu phiếu xuất.");

            UiStyler.SetAccessibleText(cboHoaDon, "Hóa đơn bán", "Chọn hóa đơn cần xuất hàng.");
            UiStyler.SetAccessibleText(cboKho, "Kho xuất", "Chọn kho thực hiện xuất hàng.");
            UiStyler.SetAccessibleText(txtLyDoXuat, "Lý do xuất", "Nhập lý do hoặc nội dung giao hàng.");
            UiStyler.SetAccessibleText(dgvChiTiet, "Chi tiết xuất kho", "Nhập số lượng xuất lần này và đối chiếu tồn kho.");
            UiStyler.SetAccessibleText(dgvDanhSachPhieuXuat, "Danh sách phiếu xuất kho", "Nhấp đúp một phiếu để xem chi tiết.");
            dgvChiTiet.CellFormatting += dgvChiTiet_CellFormatting;
            dgvDanhSachPhieuXuat.CellFormatting += dgvDanhSachPhieuXuat_CellFormatting;
            ConfigureGridColumns();
        }

        private void ConfigureGridColumns()
        {
            // === Lưới Danh Sách Phiếu Xuất Kho ===
            dgvDanhSachPhieuXuat.AutoGenerateColumns = false;
            dgvDanhSachPhieuXuat.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDanhSachPhieuXuat.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;

            colDSMaPXK.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colDSMaPXK.Width = 110;
            colDSMaPXK.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            colDSNgayXuat.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colDSNgayXuat.Width = 140;
            colDSNgayXuat.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colDSNgayXuat.DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";

            colDSMaHDB.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colDSMaHDB.Width = 110;
            colDSMaHDB.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            colDSKho.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colDSKho.Width = 150;
            colDSKho.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            colDSKhachHang.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colDSKhachHang.FillWeight = 160F;
            colDSKhachHang.MinimumWidth = 180;
            colDSKhachHang.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            colDSNhanVien.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colDSNhanVien.Width = 140;
            colDSNhanVien.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            colDSLyDo.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colDSLyDo.FillWeight = 180F;
            colDSLyDo.MinimumWidth = 180;
            colDSLyDo.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            colDSTrangThai.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colDSTrangThai.Width = 120;
            colDSTrangThai.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // === Lưới Chi Tiết Mặt Hàng Xuất Kho ===
            dgvChiTiet.AutoGenerateColumns = false;
            dgvChiTiet.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvChiTiet.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;

            colMaSP.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colMaSP.Width = 90;
            colMaSP.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colMaSP.HeaderCell.Style.WrapMode = DataGridViewTriState.False;

            colTenSP.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colTenSP.FillWeight = 180F;
            colTenSP.MinimumWidth = 180;
            colTenSP.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            colTenSP.HeaderCell.Style.WrapMode = DataGridViewTriState.False;

            colDonViTinh.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colDonViTinh.Width = 65;
            colDonViTinh.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colDonViTinh.HeaderCell.Style.WrapMode = DataGridViewTriState.False;

            colSoLuongHDB.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colSoLuongHDB.Width = 105;
            colSoLuongHDB.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colSoLuongHDB.DefaultCellStyle.Format = "N0";
            colSoLuongHDB.HeaderCell.Style.WrapMode = DataGridViewTriState.False;

            colSoLuongDaXuat.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colSoLuongDaXuat.Width = 90;
            colSoLuongDaXuat.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colSoLuongDaXuat.DefaultCellStyle.Format = "N0";
            colSoLuongDaXuat.HeaderCell.Style.WrapMode = DataGridViewTriState.False;

            colSoLuongConLai.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colSoLuongConLai.Width = 90;
            colSoLuongConLai.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colSoLuongConLai.DefaultCellStyle.Format = "N0";
            colSoLuongConLai.HeaderCell.Style.WrapMode = DataGridViewTriState.False;

            colTonKho.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colTonKho.Width = 95;
            colTonKho.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colTonKho.DefaultCellStyle.Format = "N0";
            colTonKho.HeaderCell.Style.WrapMode = DataGridViewTriState.False;

            colSoLuongXuat.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colSoLuongXuat.Width = 100;
            colSoLuongXuat.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colSoLuongXuat.DefaultCellStyle.Format = "N0";
            colSoLuongXuat.DefaultCellStyle.Font = new Font(dgvChiTiet.Font, FontStyle.Bold);
            colSoLuongXuat.HeaderCell.Style.WrapMode = DataGridViewTriState.False;
        }

        private void InitChiTietTable()
        {
            _dtChiTiet = new DataTable();
            _dtChiTiet.Columns.Add("MaSP", typeof(string));
            _dtChiTiet.Columns.Add("TenSP", typeof(string));
            _dtChiTiet.Columns.Add("DonViTinh", typeof(string));
            _dtChiTiet.Columns.Add("SoLuongHoaDon", typeof(int));
            _dtChiTiet.Columns.Add("SoLuongDaXuat", typeof(int));
            _dtChiTiet.Columns.Add("SoLuongConLai", typeof(int));
            _dtChiTiet.Columns.Add("TonKhoHienTai", typeof(int));
            _dtChiTiet.Columns.Add("SoLuongXuat", typeof(int));
        }

        private async void frmPhieuXuatKho_Load(object sender, EventArgs e)
        {
            this.ApplyResponsiveUI();

            // 1. Phân quyền truy cập
            if (!SessionManager.IsAdmin() && !SessionManager.IsWarehouse())
            {
                MessageBox.Show("Bạn không có quyền truy cập vào chức năng Quản lý Phiếu xuất kho.\nChỉ Quản trị viên và Nhân viên kho mới có quyền này.",
                    "Từ chối truy cập", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
                return;
            }

            if (SessionManager.CurrentUser != null)
            {
                lblNhanVienLap.Text = string.Format("{0} ({1})", SessionManager.CurrentUser.HoTen, SessionManager.CurrentUser.MaNV);
            }

            await LoadCombosAsync();
            _isBinding = true;
            ResetForm();
            _isBinding = false;
            await _entryDebouncer.RunNowAsync(
                delegate(int version) { return UpdateInvoiceSelectionAsync(version); });

            if (!string.IsNullOrEmpty(_initialMaHDB))
            {
                cboHoaDon.Enabled = false;
            }
            else
            {
                await _searchDebouncer.RunNowAsync(
                    delegate(int version) { return LoadDanhSachPhieuXuatAsync(version, null); });
            }
        }

        private async Task LoadCombosAsync()
        {
            try
            {
                _isBinding = true;

                DataTable dtKho = null;
                DataTable dtHDB = null;
                SetIssueStatus(UiStatusKind.Information, "Đang tải danh mục kho và hóa đơn chờ xuất...");
                await UiFeedbackHelper.RunBusyAsync(
                    this,
                    null,
                    null,
                    delegate
                    {
                        dtKho = _khoDAL.GetAll();
                        dtHDB = _phieuXuatKhoDAL.GetHoaDonSanSangXuatKho();
                        return true;
                    });
                if (IsDisposed)
                {
                    return;
                }

                cboKho.DisplayMember = "TenKho";
                cboKho.ValueMember = "MaKho";
                cboKho.DataSource = dtKho.Copy();

                // Load danh sách kho cho bộ lọc ở Tab 2
                DataTable dtFilterKho = dtKho.Copy();
                DataRow allRow = dtFilterKho.NewRow();
                allRow["MaKho"] = "";
                allRow["TenKho"] = "(Tất cả kho)";
                dtFilterKho.Rows.InsertAt(allRow, 0);

                cboFilterKho.DisplayMember = "TenKho";
                cboFilterKho.ValueMember = "MaKho";
                cboFilterKho.DataSource = dtFilterKho;

                BindHoaDonCombo(dtHDB);
                SetIssueStatus(UiStatusKind.Neutral, "Nhập số lượng cần xuất tại cột được tô màu.");
            }
            catch (Exception ex)
            {
                UiErrorHandler.Show(this, "FRMPHIEUXUATKHO_UI_ERROR", "Lỗi khi tải danh mục kho.", ex);
            }
            finally
            {
                _isBinding = false;
            }
        }

        private async Task LoadHoaDonComboAsync()
        {
            try
            {
                DataTable dtHDB = null;
                SetIssueStatus(UiStatusKind.Information, "Đang cập nhật hóa đơn chờ xuất...");
                await UiFeedbackHelper.RunBusyAsync(
                    this,
                    null,
                    null,
                    delegate
                    {
                        dtHDB = _phieuXuatKhoDAL.GetHoaDonSanSangXuatKho();
                        return true;
                    });
                if (IsDisposed)
                {
                    return;
                }

                _isBinding = true;
                BindHoaDonCombo(dtHDB);
                SetIssueStatus(UiStatusKind.Neutral, "Danh sách hóa đơn chờ xuất đã được cập nhật.");
            }
            catch (Exception ex)
            {
                SetIssueStatus(UiStatusKind.Error, "Không thể cập nhật hóa đơn chờ xuất.");
                UiErrorHandler.Show(this, "FRMPHIEUXUATKHO_UI_ERROR", "Lỗi khi tải hóa đơn chờ xuất.", ex);
            }
            finally
            {
                _isBinding = false;
            }
        }

        private void BindHoaDonCombo(DataTable dtHDB)
        {
            DataTable dtCombo = dtHDB.Clone();
            dtCombo.Columns.Add("DisplayText", typeof(string));

            foreach (DataRow r in dtHDB.Rows)
            {
                DataRow newR = dtCombo.NewRow();
                newR.ItemArray = r.ItemArray;
                newR["DisplayText"] = string.Format("{0} - {1} ({2:N0} VNĐ)", r["MaHDB"], r["TenKH"], r["TongTien"]);
                dtCombo.Rows.Add(newR);
            }

            cboHoaDon.DisplayMember = "DisplayText";
            cboHoaDon.ValueMember = "MaHDB";
            cboHoaDon.DataSource = dtCombo;

            if (!string.IsNullOrEmpty(_initialMaHDB))
            {
                string target = _initialMaHDB.Trim();
                int foundIndex = -1;
                for (int i = 0; i < dtCombo.Rows.Count; i++)
                {
                    if (string.Equals(dtCombo.Rows[i]["MaHDB"].ToString().Trim(), target, StringComparison.OrdinalIgnoreCase))
                    {
                        foundIndex = i;
                        break;
                    }
                }

                if (foundIndex >= 0)
                {
                    cboHoaDon.SelectedIndex = foundIndex;
                    btnXuatKho.Enabled = true;
                }
                else if (cboHoaDon.Items.Count > 0)
                {
                    btnXuatKho.Enabled = true;
                    cboHoaDon.SelectedIndex = 0;
                }
            }
            else if (cboHoaDon.Items.Count > 0)
            {
                btnXuatKho.Enabled = true;
                cboHoaDon.SelectedIndex = 0;
            }
            else
            {
                btnXuatKho.Enabled = false;
                lblKhachHangInfo.Text = "(Không có hóa đơn nào sẵn sàng xuất kho)";
                lblKhachHangInfo.ForeColor = UiTheme.TextSecondary;
                _dtChiTiet.Clear();
                UpdateGrandTotal();
            }
        }

        private void ResetForm()
        {
            txtMaPXK.Text = _warehouseService.GenerateNewIssueId();
            dtpNgayXuat.Value = DateTime.Now;
            txtLyDoXuat.Text = "Xuất kho giao hàng theo hóa đơn bán";
            btnXuatKho.Enabled = (cboHoaDon.Items != null && cboHoaDon.Items.Count > 0);

            if (cboKho.Items.Count > 0)
            {
                cboKho.SelectedIndex = 0;
            }

        }

        private async Task UpdateInvoiceSelectionAsync(int requestVersion)
        {
            if (_isBinding || cboHoaDon.SelectedValue == null)
            {
                return;
            }

            string maHDB = cboHoaDon.SelectedValue.ToString().Trim();
            if (string.IsNullOrEmpty(maHDB))
            {
                return;
            }
            string selectedKho = (cboKho.SelectedValue != null) ? cboKho.SelectedValue.ToString().Trim() : "";
            DataTable preparedDetails = _dtChiTiet.Clone();
            UiStyler.SetGridLoading(dgvChiTiet, "Đang tải chi tiết hóa đơn và tồn kho...");
            SetIssueStatus(UiStatusKind.Information, "Đang đối chiếu số lượng hóa đơn với tồn kho...");

            try
            {
                HoaDonBan hdb = null;
                await UiFeedbackHelper.RunBusyAsync(
                    this,
                    null,
                    null,
                    delegate
                    {
                        hdb = _hoaDonBanDAL.GetById(maHDB);
                        if (hdb == null)
                        {
                            return true;
                        }

                        DataTable dtCT = _phieuXuatKhoDAL.GetChiTietHoaDonKemTienDoXuat(maHDB);
                        foreach (DataRow r in dtCT.Rows)
                        {
                            string maSP = r["MaSP"].ToString().Trim();
                            int conLai = Convert.ToInt32(r["SoLuongConLai"]);
                            int tonKho = !string.IsNullOrEmpty(selectedKho) ? _tonKhoDAL.GetTonKho(selectedKho, maSP) : 0;
                            int slXuat = conLai > 0 ? (conLai <= tonKho ? conLai : tonKho) : 0;

                            DataRow newRow = preparedDetails.NewRow();
                            newRow["MaSP"] = maSP;
                            newRow["TenSP"] = r["TenSP"].ToString().Trim();
                            newRow["DonViTinh"] = r["DonViTinh"] != DBNull.Value ? r["DonViTinh"].ToString().Trim() : "";
                            newRow["SoLuongHoaDon"] = Convert.ToInt32(r["SoLuongHoaDon"]);
                            newRow["SoLuongDaXuat"] = Convert.ToInt32(r["SoLuongDaXuat"]);
                            newRow["SoLuongConLai"] = conLai;
                            newRow["TonKhoHienTai"] = tonKho;
                            newRow["SoLuongXuat"] = slXuat;
                            preparedDetails.Rows.Add(newRow);
                        }
                        return true;
                    });

                if (IsDisposed || !_entryDebouncer.IsCurrent(requestVersion))
                {
                    return;
                }

                if (hdb != null)
                {
                    lblKhachHangInfo.Text = string.Format("{0} (Mã KH: {1})", hdb.TenKH, hdb.MaKH);
                    lblKhachHangInfo.ForeColor = UiTheme.Information;
                    txtLyDoXuat.Text = string.Format("Xuất kho giao hàng cho hóa đơn {0} ({1})", hdb.MaHDB, hdb.TenKH);
                    _dtChiTiet = preparedDetails;
                    dgvChiTiet.AutoGenerateColumns = false;
                    dgvChiTiet.DataSource = _dtChiTiet;
                    UiStyler.ClearGridState(dgvChiTiet);
                    UpdateGrandTotal();
                    SetIssueStatus(
                        preparedDetails.Rows.Count == 0 ? UiStatusKind.Warning : UiStatusKind.Neutral,
                        preparedDetails.Rows.Count == 0 ? "Hóa đơn không còn mặt hàng cần xuất." : "Đã đối chiếu số lượng hóa đơn với tồn kho hiện tại.");
                }
            }
            catch (Exception ex)
            {
                if (!_entryDebouncer.IsCurrent(requestVersion))
                {
                    return;
                }
                UiStyler.SetGridError(dgvChiTiet, "Không thể tải chi tiết hóa đơn và tồn kho.");
                SetIssueStatus(UiStatusKind.Error, "Không thể tải chi tiết hóa đơn và tồn kho.");
                UiErrorHandler.Show(this, "FRMPHIEUXUATKHO_UI_ERROR", "Lỗi khi tải chi tiết hóa đơn.", ex);
            }
        }

        private void UpdateGrandTotal()
        {
            int tongSL = 0;
            foreach (DataRow row in _dtChiTiet.Rows)
            {
                if (row["SoLuongXuat"] != DBNull.Value)
                {
                    tongSL += Convert.ToInt32(row["SoLuongXuat"]);
                }
            }

            lblTongSoLuongXuat.Text = string.Format("TỔNG SỐ LƯỢNG XUẤT: {0:N0}", tongSL);
        }

        private async void cboHoaDon_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_isBinding)
            {
                _historyDetailDebouncer.Cancel();
                await _entryDebouncer.RunNowAsync(
                    delegate(int version) { return UpdateInvoiceSelectionAsync(version); });
            }
        }

        private async void cboKho_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_isBinding)
            {
                _historyDetailDebouncer.Cancel();
                await _entryDebouncer.RunNowAsync(
                    delegate(int version) { return UpdateInvoiceSelectionAsync(version); });
            }
        }

        private void dgvChiTiet_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (_isCorrectingQuantity || e.RowIndex < 0 || _dtChiTiet == null || e.RowIndex >= _dtChiTiet.Rows.Count)
            {
                return;
            }

            // Nếu thay đổi cột Số lượng xuất
            if (dgvChiTiet.Columns[e.ColumnIndex].Name == "colSoLuongXuat")
            {
                DataRow row = _dtChiTiet.Rows[e.RowIndex];
                row.RowError = string.Empty;
                int slXuat = 0;
                int.TryParse(row["SoLuongXuat"].ToString(), out slXuat);

                int conLai = Convert.ToInt32(row["SoLuongConLai"]);
                int tonKho = Convert.ToInt32(row["TonKhoHienTai"]);
                int maximumAllowed = Math.Min(conLai, tonKho);

                if (slXuat < 0)
                {
                    SetIssueStatus(UiStatusKind.Warning, "Số lượng xuất không được âm; hệ thống đã đưa về 0.");
                    row.RowError = "Số lượng xuất không được âm.";
                    CorrectQuantity(row, 0);
                }
                else if (slXuat > maximumAllowed)
                {
                    string limitName = conLai <= tonKho ? "phần còn lại của hóa đơn" : "tồn tại kho";
                    SetIssueStatus(UiStatusKind.Warning, string.Format("Số lượng xuất vượt {0}; đã điều chỉnh về {1:N0}.", limitName, maximumAllowed));
                    row.RowError = string.Format("Số lượng xuất vượt {0}.", limitName);
                    CorrectQuantity(row, maximumAllowed);
                }
                else
                {
                    SetIssueStatus(UiStatusKind.Neutral, "Số lượng xuất hợp lệ.");
                }

                UpdateGrandTotal();
            }
        }

        private async void btnXuatKho_Click(object sender, EventArgs e)
        {
            if (cboHoaDon.SelectedValue == null || string.IsNullOrWhiteSpace(cboHoaDon.SelectedValue.ToString()))
            {
                ShowValidation(cboHoaDon, "Vui lòng chọn hóa đơn bán cần xuất kho.");
                return;
            }

            if (cboKho.SelectedValue == null || string.IsNullOrWhiteSpace(cboKho.SelectedValue.ToString()))
            {
                ShowValidation(cboKho, "Vui lòng chọn kho xuất hàng.");
                return;
            }

            if (_dtChiTiet.Rows.Count == 0)
            {
                ShowValidation(dgvChiTiet, "Hóa đơn được chọn không có mặt hàng nào để xuất.");
                return;
            }

            // Thu thập các mặt hàng có số lượng xuất > 0
            List<ChiTietPhieuXuatKho> details = new List<ChiTietPhieuXuatKho>();
            for (int i = 0; i < _dtChiTiet.Rows.Count; i++)
            {
                DataRow r = _dtChiTiet.Rows[i];
                int slXuat = Convert.ToInt32(r["SoLuongXuat"]);
                if (slXuat > 0)
                {
                    int tonKho = Convert.ToInt32(r["TonKhoHienTai"]);
                    if (slXuat > tonKho)
                    {
                        ShowValidation(
                            dgvChiTiet,
                            string.Format("Sản phẩm [{0}] yêu cầu xuất {1} nhưng tồn kho chỉ còn {2}.",
                                r["TenSP"], slXuat, tonKho));
                        dgvChiTiet.CurrentCell = dgvChiTiet.Rows[i].Cells[colSoLuongXuat.Index];
                        return;
                    }

                    details.Add(new ChiTietPhieuXuatKho
                    {
                        MaSP = r["MaSP"].ToString().Trim(),
                        TenSP = r["TenSP"].ToString().Trim(),
                        SoLuongXuat = slXuat
                    });
                }
            }

            if (details.Count == 0)
            {
                ShowValidation(dgvChiTiet, "Vui lòng nhập số lượng xuất lớn hơn 0 cho ít nhất một mặt hàng.");
                return;
            }

            string maPXK = txtMaPXK.Text.Trim();
            string maHDB = cboHoaDon.SelectedValue.ToString().Trim();
            string maKho = cboKho.SelectedValue.ToString().Trim();
            string lyDo = txtLyDoXuat.Text.Trim();

            PhieuXuatKho pxk = new PhieuXuatKho
            {
                MaPXK = maPXK,
                MaHDB = maHDB,
                MaKho = maKho,
                NgayXuat = dtpNgayXuat.Value,
                LyDoXuat = lyDo,
                TrangThai = "Đã xuất"
            };

            string error = string.Empty;
            SetIssueStatus(UiStatusKind.Information, "Đang kiểm tra và ghi nhận xuất kho...");
            bool success = await UiFeedbackHelper.RunBusyAsync(
                this,
                btnXuatKho,
                "ĐANG XUẤT KHO...",
                delegate { return _warehouseService.CreatePhieuXuatKho(pxk, details, out error); });
            if (IsDisposed)
            {
                return;
            }

            if (success)
            {
                _lastCreatedMaPXK = pxk.MaPXK;
                DialogResult dr = MessageBox.Show(
                    string.Format("Lập phiếu xuất kho [{0}] thành công!\nKho xuất: {1}\nHóa đơn: {2}\nTổng mặt hàng xuất: {3} mặt hàng.\n\nBạn có muốn in phiếu xuất kho ngay không?",
                        pxk.MaPXK, cboKho.Text, maHDB, details.Count),
                    "Lập phiếu xuất kho thành công",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Information);

                if (dr == DialogResult.Yes)
                {
                    InPhieuXuat(pxk.MaPXK);
                }

                if (this.Modal)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                    return;
                }

                await LoadHoaDonComboAsync();
                _isBinding = true;
                ResetForm();
                _isBinding = false;
                await _entryDebouncer.RunNowAsync(
                    delegate(int version) { return UpdateInvoiceSelectionAsync(version); });
                await _searchDebouncer.RunNowAsync(
                    delegate(int version) { return LoadDanhSachPhieuXuatAsync(version, null); });
            }
            else
            {
                MessageBox.Show(error, "Lỗi xuất kho", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task LoadDanhSachPhieuXuatAsync(int requestVersion, Button actionButton)
        {
            string selectedFilterKho = (cboFilterKho.SelectedValue != null) ? cboFilterKho.SelectedValue.ToString().Trim() : "";
            string keyword = txtTimKiem.Text.Trim();
            UiStyler.SetGridLoading(dgvDanhSachPhieuXuat, "Đang tải danh sách phiếu xuất...");
            UiStyler.StyleStatusLabel(lblSoPhieu, UiStatusKind.Information, "Đang tra cứu phiếu xuất kho...");
            try
            {
                DataTable dt = null;
                await UiFeedbackHelper.RunBusyAsync(
                    this,
                    actionButton,
                    "ĐANG TẢI...",
                    delegate
                    {
                        dt = string.IsNullOrEmpty(selectedFilterKho) && string.IsNullOrEmpty(keyword)
                            ? _phieuXuatKhoDAL.GetAll()
                            : _phieuXuatKhoDAL.Search(keyword, string.IsNullOrEmpty(selectedFilterKho) ? null : selectedFilterKho);
                        return true;
                    });
                if (IsDisposed || !_searchDebouncer.IsCurrent(requestVersion))
                {
                    return;
                }

                dgvDanhSachPhieuXuat.AutoGenerateColumns = false;
                dgvDanhSachPhieuXuat.DataSource = dt;
                lblSoPhieu.Text = string.Format("Tổng số phiếu: {0} phiếu", dt.Rows.Count);
                UiStyler.StyleStatusLabel(
                    lblSoPhieu,
                    dt.Rows.Count == 0 ? UiStatusKind.Warning : UiStatusKind.Neutral,
                    dt.Rows.Count == 0 ? "Không tìm thấy phiếu xuất phù hợp." : string.Format("Đang hiển thị {0:N0} phiếu xuất.", dt.Rows.Count));
                UiStyler.ClearGridState(dgvDanhSachPhieuXuat);
                UiStyler.UpdateGridEmptyState(dgvDanhSachPhieuXuat, "Không tìm thấy phiếu xuất phù hợp.");
            }
            catch (Exception ex)
            {
                if (!_searchDebouncer.IsCurrent(requestVersion))
                {
                    return;
                }
                UiStyler.SetGridError(dgvDanhSachPhieuXuat, "Không thể tải danh sách phiếu xuất.");
                AppLogger.Error("LoadDanhSachPhieuXuat", "Lỗi nạp danh sách phiếu xuất: " + ex.Message, ex);
            }
        }

        private async void btnTimKiem_Click(object sender, EventArgs e)
        {
            await _searchDebouncer.RunNowAsync(
                delegate(int version) { return LoadDanhSachPhieuXuatAsync(version, btnTimKiem); });
        }

        private async void cboFilterKho_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_isBinding)
            {
                await _searchDebouncer.RunNowAsync(
                    delegate(int version) { return LoadDanhSachPhieuXuatAsync(version, null); });
            }
        }

        private async void btnLamMoiDanhSach_Click(object sender, EventArgs e)
        {
            _isBinding = true;
            txtTimKiem.Clear();
            if (cboFilterKho.Items.Count > 0)
            {
                cboFilterKho.SelectedIndex = 0;
            }
            _isBinding = false;
            await _searchDebouncer.RunNowAsync(
                delegate(int version) { return LoadDanhSachPhieuXuatAsync(version, btnLamMoiDanhSach); });
        }

        private async void btnLamMoi_Click(object sender, EventArgs e)
        {
            _historyDetailDebouncer.Cancel();
            await LoadHoaDonComboAsync();
            _isBinding = true;
            ResetForm();
            _isBinding = false;
            await _entryDebouncer.RunNowAsync(
                delegate(int version) { return UpdateInvoiceSelectionAsync(version); });
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            if (!_isBinding)
            {
                _searchDebouncer.Restart(
                    delegate(int version) { return LoadDanhSachPhieuXuatAsync(version, null); });
            }
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            if (this.Modal)
            {
                this.DialogResult = DialogResult.Cancel;
            }
            this.Close();
        }

        private async void dgvDanhSachPhieuXuat_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            string maPXK = dgvDanhSachPhieuXuat.Rows[e.RowIndex].Cells["colDSMaPXK"].Value.ToString().Trim();
            _entryDebouncer.Cancel();
            await _historyDetailDebouncer.RunNowAsync(
                delegate(int version) { return LoadIssuedDocumentAsync(version, maPXK); });
        }

        private async Task LoadIssuedDocumentAsync(int requestVersion, string maPXK)
        {
            DataTable preparedDetails = _dtChiTiet.Clone();
            PhieuXuatKho pxk = null;
            UiStyler.SetGridLoading(dgvChiTiet, "Đang tải chi tiết phiếu xuất...");
            SetIssueStatus(UiStatusKind.Information, "Đang tải lại phiếu xuất " + maPXK + "...");

            try
            {
                await UiFeedbackHelper.RunBusyAsync(
                    this,
                    null,
                    null,
                    delegate
                    {
                        pxk = _phieuXuatKhoDAL.GetById(maPXK);
                        if (pxk == null)
                        {
                            return true;
                        }

                        DataTable dtCT = _phieuXuatKhoDAL.GetChiTietDataTable(maPXK);
                        foreach (DataRow r in dtCT.Rows)
                        {
                            string maSP = r["MaSP"].ToString().Trim();
                            int quantity = Convert.ToInt32(r["SoLuongXuat"]);
                            DataRow newRow = preparedDetails.NewRow();
                            newRow["MaSP"] = maSP;
                            newRow["TenSP"] = r["TenSP"].ToString().Trim();
                            newRow["DonViTinh"] = r["DonViTinh"] != DBNull.Value ? r["DonViTinh"].ToString().Trim() : "";
                            newRow["SoLuongHoaDon"] = quantity;
                            newRow["SoLuongDaXuat"] = quantity;
                            newRow["SoLuongConLai"] = 0;
                            newRow["TonKhoHienTai"] = _tonKhoDAL.GetTonKho(pxk.MaKho, maSP);
                            newRow["SoLuongXuat"] = quantity;
                            preparedDetails.Rows.Add(newRow);
                        }
                        return true;
                    });

                if (IsDisposed || !_historyDetailDebouncer.IsCurrent(requestVersion))
                {
                    return;
                }

                if (pxk == null)
                {
                    UiStyler.SetGridError(dgvChiTiet, "Không tìm thấy phiếu xuất " + maPXK + ".");
                    SetIssueStatus(UiStatusKind.Warning, "Không tìm thấy phiếu xuất đã chọn.");
                    return;
                }

                _isBinding = true;
                txtMaPXK.Text = pxk.MaPXK;
                if (pxk.NgayXuat.HasValue)
                {
                    dtpNgayXuat.Value = pxk.NgayXuat.Value;
                }
                cboKho.SelectedValue = pxk.MaKho;
                cboHoaDon.SelectedValue = pxk.MaHDB;
                txtLyDoXuat.Text = pxk.LyDoXuat;
                lblKhachHangInfo.Text = string.Format("{0}", pxk.TenKH);
                lblNhanVienLap.Text = string.Format("{0} ({1})", pxk.TenNV, pxk.MaNV);
                _isBinding = false;

                _dtChiTiet = preparedDetails;
                dgvChiTiet.AutoGenerateColumns = false;
                dgvChiTiet.DataSource = _dtChiTiet;
                UiStyler.ClearGridState(dgvChiTiet);
                btnXuatKho.Enabled = false;

                UpdateGrandTotal();
                tcPhieuXuat.SelectedIndex = 0;
                SetIssueStatus(UiStatusKind.Neutral, "Đang xem lại phiếu xuất " + maPXK + ".");
            }
            catch (Exception ex)
            {
                _isBinding = false;
                if (!_historyDetailDebouncer.IsCurrent(requestVersion))
                {
                    return;
                }
                UiStyler.SetGridError(dgvChiTiet, "Không thể tải chi tiết phiếu xuất.");
                SetIssueStatus(UiStatusKind.Error, "Không thể tải chi tiết phiếu xuất.");
                UiErrorHandler.Show(this, "FRMPHIEUXUATKHO_UI_ERROR", "Lỗi khi tải chi tiết phiếu xuất.", ex);
            }
        }

        private void SetIssueStatus(UiStatusKind kind, string message)
        {
            UiStyler.StyleStatusLabel(_issueStatus, kind, message);
        }

        private void CorrectQuantity(DataRow row, int quantity)
        {
            _isCorrectingQuantity = true;
            try
            {
                row["SoLuongXuat"] = quantity;
            }
            finally
            {
                _isCorrectingQuantity = false;
            }
        }

        private void dgvChiTiet_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (SystemInformation.HighContrast || _dtChiTiet == null || e.RowIndex < 0 || e.RowIndex >= _dtChiTiet.Rows.Count || e.Value == null)
            {
                return;
            }

            int quantity;
            if (!int.TryParse(e.Value.ToString(), out quantity))
            {
                return;
            }

            if (e.ColumnIndex == colSoLuongConLai.Index)
            {
                e.CellStyle.ForeColor = quantity > 0 ? UiTheme.Information : UiTheme.TextSecondary;
            }
            else if (e.ColumnIndex == colTonKho.Index)
            {
                int remaining = Convert.ToInt32(_dtChiTiet.Rows[e.RowIndex]["SoLuongConLai"]);
                if (quantity <= 0)
                {
                    e.CellStyle.ForeColor = UiTheme.Danger;
                }
                else if (quantity < remaining)
                {
                    e.CellStyle.ForeColor = UiTheme.Warning;
                }
            }
            else if (e.ColumnIndex == colSoLuongXuat.Index && quantity > 0)
            {
                e.CellStyle.ForeColor = UiTheme.Success;
            }
        }

        private void dgvDanhSachPhieuXuat_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            string colName = dgvDanhSachPhieuXuat.Columns[e.ColumnIndex].Name;
            if (colName == "colDSTrangThai" && e.Value != null)
            {
                string status = e.Value.ToString().Trim();
                if (string.Equals(status, "Đã xuất kho", StringComparison.OrdinalIgnoreCase))
                {
                    e.CellStyle.ForeColor = Color.FromArgb(16, 120, 60);
                    e.CellStyle.Font = new Font(dgvDanhSachPhieuXuat.Font, FontStyle.Bold);
                }
                else if (string.Equals(status, "Đã hủy", StringComparison.OrdinalIgnoreCase))
                {
                    e.CellStyle.ForeColor = Color.FromArgb(180, 20, 20);
                    e.CellStyle.Font = new Font(dgvDanhSachPhieuXuat.Font, FontStyle.Bold);
                }
            }
        }

        private void ShowValidation(Control control, string message)
        {
            UiInteractionHelper.ShowValidationError(_validationErrors, control, message);
            SetIssueStatus(UiStatusKind.Warning, message);
        }

        private void InPhieuXuatDangChon()
        {
            if (dgvDanhSachPhieuXuat.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn một phiếu xuất kho cần in!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string maPXK = dgvDanhSachPhieuXuat.CurrentRow.Cells["colDSMaPXK"].Value != null
                ? dgvDanhSachPhieuXuat.CurrentRow.Cells["colDSMaPXK"].Value.ToString().Trim()
                : string.Empty;

            InPhieuXuat(maPXK);
        }

        private void InPhieuXuat(string maPXK)
        {
            if (string.IsNullOrEmpty(maPXK))
            {
                MessageBox.Show("Vui lòng chỉ định mã phiếu xuất kho cần in!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                PhieuXuatKho pxk = _phieuXuatKhoDAL.GetById(maPXK);
                if (pxk == null)
                {
                    MessageBox.Show("Không tìm thấy dữ liệu phiếu xuất " + maPXK, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DataTable dtChiTiet = _phieuXuatKhoDAL.GetChiTietDataTable(maPXK);
                string html = VoucherPrintHelper.GeneratePhieuXuatKhoHtml(pxk, dtChiTiet);
                frmInChungTu.ShowVoucher(this, "Phiếu Xuất Kho - " + maPXK, html, "PhieuXuatKho_" + maPXK);
            }
            catch (Exception ex)
            {
                AppLogger.Error("PRINT_PXK_ERROR", "Lỗi khi xuất mẫu in phiếu xuất: " + ex.Message, ex);
                MessageBox.Show("Lỗi khi mở mẫu in phiếu xuất kho: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
