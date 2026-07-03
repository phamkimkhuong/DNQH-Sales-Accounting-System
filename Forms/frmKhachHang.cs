using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DNQH_KeToanBanHang.DataAccess;
using DNQH_KeToanBanHang.Helpers;
using DNQH_KeToanBanHang.Models;

namespace DNQH_KeToanBanHang.Forms
{
    public partial class frmKhachHang : Form
    {
        private readonly KhachHangDAL khachHangDAL;
        private readonly ErrorProvider validationErrors;
        private readonly Label lblPageStatus;
        private readonly UiGridQueryController _gridQuery;
        private int _currentVersion = 1;

        public frmKhachHang()
        {
            InitializeComponent();
            validationErrors = UiStyler.CreateErrorProvider(this);
            lblPageStatus = CreateStatusLabel();
            BuildResponsiveLayout();
            ApplyFoundationDesign();
            khachHangDAL = new KhachHangDAL();
            _gridQuery = new UiGridQueryController(
                this, txtTimKiem, dgvKhachHang, lblPageStatus,
                delegate(string keyword)
                {
                    return delegate { return string.IsNullOrEmpty(keyword) ? khachHangDAL.GetAll() : khachHangDAL.Search(keyword); };
                },
                delegate(int count) { return string.Format("Đang hiển thị {0:N0} khách hàng.", count); },
                "Đang tải danh sách khách hàng...",
                "Không tìm thấy khách hàng phù hợp.",
                delegate(Exception ex) { UiErrorHandler.Show(this, "FRMKHACHHANG_UI_ERROR", "Lỗi khi tải danh sách khách hàng.", ex); });
        }

        private Label CreateStatusLabel()
        {
            return new Label
            {
                Name = "lblPageStatus",
                AutoEllipsis = true,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                Margin = new Padding(0, 4, 0, 0)
            };
        }

        private void BuildResponsiveLayout()
        {
            SuspendLayout();
            Controls.Clear();

            MinimumSize = new Size(720, 520);
            Padding = new Padding(UiTheme.PagePadding);

            TableLayoutPanel root = new TableLayoutPanel
            {
                Name = "tlpKhachHangRoot",
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 5,
                BackColor = UiTheme.Canvas
            };
            root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            root.RowStyles.Add(new RowStyle(SizeType.Absolute, 58F));
            root.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            root.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            root.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            root.RowStyles.Add(new RowStyle(SizeType.Absolute, 28F));

            Panel heading = new Panel { Dock = DockStyle.Fill };
            lblTitle.Location = new Point(0, 0);
            lblTitle.AutoSize = true;
            Label subtitle = new Label
            {
                AutoSize = true,
                Location = new Point(2, 34),
                Text = "Cập nhật hồ sơ và tìm nhanh khách hàng",
                ForeColor = UiTheme.TextSecondary
            };
            heading.Controls.Add(lblTitle);
            heading.Controls.Add(subtitle);

            BuildInformationLayout();
            FlowLayoutPanel toolbar = BuildToolbar();

            grpThongTin.Dock = DockStyle.Fill;
            grpThongTin.Margin = new Padding(0, 0, 0, UiTheme.SectionGap);
            dgvKhachHang.Dock = DockStyle.Fill;
            dgvKhachHang.Margin = new Padding(0, UiTheme.SectionGap, 0, 0);

            root.Controls.Add(heading, 0, 0);
            root.Controls.Add(grpThongTin, 0, 1);
            root.Controls.Add(toolbar, 0, 2);
            root.Controls.Add(dgvKhachHang, 0, 3);
            root.Controls.Add(lblPageStatus, 0, 4);
            Controls.Add(root);
            ResumeLayout(true);
        }

        private void BuildInformationLayout()
        {
            grpThongTin.Controls.Clear();
            grpThongTin.Padding = new Padding(12, 10, 12, 12);
            grpThongTin.AutoSize = true;
            grpThongTin.AutoSizeMode = AutoSizeMode.GrowAndShrink;

            TableLayoutPanel fields = UiLayoutBuilder.CreateStructuredGridLayout(
                2, 95,
                UiLayoutBuilder.Field(lblMaKH, txtMaKH, UiFieldSize.Code),
                UiLayoutBuilder.Field(lblTenKH, txtTenKH, UiFieldSize.Wide),
                UiLayoutBuilder.Field(lblSoDienThoai, txtSoDienThoai, UiFieldSize.ShortText),
                UiLayoutBuilder.Field(lblEmail, txtEmail, UiFieldSize.Medium),
                UiLayoutBuilder.FullWidthField(lblDiaChi, txtDiaChi));

            grpThongTin.Controls.Add(fields);
        }

        private FlowLayoutPanel BuildToolbar()
        {
            FlowLayoutPanel toolbar = new FlowLayoutPanel
            {
                Name = "flpKhachHangActions",
                Dock = DockStyle.Fill,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                WrapContents = true,
                Padding = new Padding(0),
                Margin = new Padding(0)
            };

            Button btnXuatCsv = CsvExporter.CreateExportButton(dgvKhachHang, "DanhSachKhachHang", "DANH SÁCH KHÁCH HÀNG");
            Button btnNhapExcel = ExcelImporter.CreateImportButton(this, ImportEntityType.KhachHang, () => _gridQuery.RefreshAsync(null));
            Control[] controls = { btnThem, btnSua, btnXoa, btnLamMoi, lblTimKiem, txtTimKiem, btnTimKiem, btnXuatCsv, btnNhapExcel };
            foreach (Control control in controls)
            {
                control.Anchor = AnchorStyles.None;
                control.Margin = new Padding(0, 0, 10, 8);
                toolbar.Controls.Add(control);
            }
            lblTimKiem.Margin = new Padding(12, 10, 6, 8);
            txtTimKiem.MinimumSize = new Size(210, UiTheme.ControlHeight);
            txtTimKiem.Width = 240;
            return toolbar;
        }

        private void ApplyFoundationDesign()
        {
            dgvKhachHang.AutoGenerateColumns = false;
            UiStyler.Apply(this);
            UiStyler.StyleButton(btnThem, UiButtonRole.Primary);
            UiStyler.StyleButton(btnSua, UiButtonRole.Information);
            UiStyler.StyleButton(btnXoa, UiButtonRole.Danger);
            UiStyler.StyleButton(btnLamMoi, UiButtonRole.Secondary);
            UiStyler.StyleButton(btnTimKiem, UiButtonRole.Secondary);
            UiStyler.StyleStatusLabel(lblPageStatus, UiStatusKind.Neutral, "Sẵn sàng.");
            CsvExporter.AttachExportContextMenu(dgvKhachHang, "DanhSachKhachHang", "DANH SÁCH KHÁCH HÀNG");
            ExcelImporter.AttachImportContextMenu(this, dgvKhachHang, ImportEntityType.KhachHang, () => _gridQuery.RefreshAsync(null));

            UiStyler.SetAccessibleText(txtMaKH, "Mã khách hàng", "Mã định danh khách hàng.");
            UiStyler.SetAccessibleText(txtTenKH, "Tên khách hàng", "Tên khách hàng bắt buộc.");
            UiStyler.SetAccessibleText(txtSoDienThoai, "Số điện thoại", "Số điện thoại liên hệ.");
            UiStyler.SetAccessibleText(txtDiaChi, "Địa chỉ", "Địa chỉ khách hàng.");
            UiStyler.SetAccessibleText(txtEmail, "Email", "Địa chỉ email khách hàng.");
            UiStyler.SetAccessibleText(txtTimKiem, "Từ khóa tìm kiếm", "Tìm theo mã, tên hoặc thông tin liên hệ.");
            UiStyler.SetAccessibleText(dgvKhachHang, "Danh sách khách hàng", "Chọn một dòng để sửa hoặc xóa.");
            ConfigureGridColumns();
        }

        private void ConfigureGridColumns()
        {
            dgvKhachHang.AutoGenerateColumns = false;
            dgvKhachHang.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            colMaKH.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colMaKH.Width = 95;
            colMaKH.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            colTenKH.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colTenKH.FillWeight = 160F;
            colTenKH.MinimumWidth = 180;
            colTenKH.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            colSoDienThoai.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colSoDienThoai.Width = 115;
            colSoDienThoai.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            colDiaChi.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colDiaChi.FillWeight = 200F;
            colDiaChi.MinimumWidth = 200;
            colDiaChi.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            colEmail.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colEmail.Width = 160;
            colEmail.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
        }

        private async void frmKhachHang_Load(object sender, EventArgs e)
        {
            this.ApplyResponsiveUI();
            if (!SessionManager.IsAdmin() && !SessionManager.IsSales())
            {
                MessageBox.Show("Bạn không có quyền truy cập vào chức năng Quản lý Khách hàng.\nChỉ Quản trị viên và Nhân viên bán hàng mới có quyền này.",
                    "Từ chối truy cập", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
                return;
            }
            ClearInputs();
            await _gridQuery.RefreshAsync(null);
        }

        private void ClearInputs()
        {
            _currentVersion = 1;
            txtMaKH.Text = AutoCodeHelper.GetNextMaKH();
            txtTenKH.Clear();
            txtSoDienThoai.Clear();
            txtDiaChi.Clear();
            txtEmail.Clear();
            txtMaKH.ReadOnly = false;
            validationErrors.Clear();
            txtTenKH.Focus();
        }

        private void dgvKhachHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvKhachHang.Rows.Count)
            {
                DataGridViewRow row = dgvKhachHang.Rows[e.RowIndex];
                string maKH = row.Cells["colMaKH"].Value != null ? row.Cells["colMaKH"].Value.ToString().Trim() : string.Empty;

                try
                {
                    KhachHang kh = khachHangDAL.GetById(maKH);
                    if (kh != null)
                    {
                        _currentVersion = kh.Version;
                        txtMaKH.Text = kh.MaKH;
                        txtTenKH.Text = kh.TenKH;
                        txtSoDienThoai.Text = kh.SoDienThoai;
                        txtDiaChi.Text = kh.DiaChi;
                        txtEmail.Text = kh.Email;

                        txtMaKH.ReadOnly = true; // Không sửa khóa chính
                        SetStatus(UiStatusKind.Information, string.Format("Đang chỉnh sửa khách hàng {0}.", txtMaKH.Text));
                    }
                }
                catch (Exception ex)
                {
                    UiErrorHandler.Show(this, "FRMKHACHHANG_UI_ERROR", "Lỗi khi đọc chi tiết khách hàng.", ex);
                }
            }
        }

        private async void btnThem_Click(object sender, EventArgs e)
        {
            string maKH = txtMaKH.Text.Trim();
            if (string.IsNullOrEmpty(maKH))
            {
                maKH = AutoCodeHelper.GetNextMaKH();
                txtMaKH.Text = maKH;
            }

            string tenKH = txtTenKH.Text.Trim();
            string sdt = txtSoDienThoai.Text.Trim();
            string diaChi = txtDiaChi.Text.Trim();
            string email = txtEmail.Text.Trim();

            if (!ValidationHelper.IsNotEmpty(tenKH))
            {
                ShowValidation(txtTenKH, "Vui lòng nhập tên khách hàng.");
                return;
            }

            if (!ValidationHelper.IsValidPhone(sdt))
            {
                ShowValidation(txtSoDienThoai, "Số điện thoại không đúng định dạng.");
                return;
            }

            if (!ValidationHelper.IsValidEmail(email))
            {
                ShowValidation(txtEmail, "Email không đúng định dạng.");
                return;
            }

            try
            {
                if (khachHangDAL.Exists(maKH))
                {
                    string suggestMa = AutoCodeHelper.GetNextMaKH();
                    DialogResult dr = MessageBox.Show(
                        string.Format("Mã khách hàng [{0}] đã tồn tại trong hệ thống.\nBạn có muốn tự động sử dụng mã mới [{1}] không?", maKH, suggestMa),
                        "Trùng mã khách hàng",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    if (dr == DialogResult.Yes)
                    {
                        maKH = suggestMa;
                        txtMaKH.Text = suggestMa;
                    }
                    else
                    {
                        txtMaKH.Focus();
                        return;
                    }
                }

                KhachHang kh = new KhachHang(maKH, tenKH, sdt, diaChi, email);
                if (khachHangDAL.Insert(kh))
                {
                    ClearInputs();
                    await _gridQuery.RefreshAsync(null);
                    SetStatus(UiStatusKind.Success, string.Format("Đã thêm khách hàng {0}.", maKH));
                    UiFeedbackHelper.ShowSuccess(this, string.Format("Đã thêm khách hàng {0} thành công.", maKH));
                }
                else
                {
                    SetStatus(UiStatusKind.Error, "Không thể thêm khách hàng. Vui lòng kiểm tra lại dữ liệu.");
                }
            }
            catch (Exception ex)
            {
                UiErrorHandler.Show(this, "FRMKHACHHANG_UI_ERROR", "Đã xảy ra lỗi khi thêm khách hàng.", ex);
            }
        }

        private async void btnSua_Click(object sender, EventArgs e)
        {
            string maKH = txtMaKH.Text.Trim();
            string tenKH = txtTenKH.Text.Trim();
            string sdt = txtSoDienThoai.Text.Trim();
            string diaChi = txtDiaChi.Text.Trim();
            string email = txtEmail.Text.Trim();

            if (!ValidationHelper.IsNotEmpty(maKH))
            {
                ShowValidation(txtMaKH, "Vui lòng chọn khách hàng cần sửa thông tin.");
                return;
            }

            if (!ValidationHelper.IsNotEmpty(tenKH))
            {
                ShowValidation(txtTenKH, "Vui lòng nhập tên khách hàng.");
                return;
            }

            if (!ValidationHelper.IsValidPhone(sdt))
            {
                ShowValidation(txtSoDienThoai, "Số điện thoại không đúng định dạng.");
                return;
            }

            if (!ValidationHelper.IsValidEmail(email))
            {
                ShowValidation(txtEmail, "Email không đúng định dạng.");
                return;
            }

            try
            {
                if (!khachHangDAL.Exists(maKH))
                {
                    SetStatus(UiStatusKind.Warning, string.Format("Khách hàng {0} không còn tồn tại trong hệ thống.", maKH));
                    return;
                }

                KhachHang kh = new KhachHang(maKH, tenKH, sdt, diaChi, email, _currentVersion);
                ConcurrencyUpdateResult result = khachHangDAL.UpdateWithResult(kh);

                if (result == ConcurrencyUpdateResult.Success)
                {
                    _currentVersion = kh.Version;
                    SetStatus(UiStatusKind.Success, string.Format("Đã cập nhật khách hàng {0}.", maKH));
                    UiFeedbackHelper.ShowSuccess(this, "Đã cập nhật thông tin khách hàng thành công.");
                    await _gridQuery.RefreshAsync(null);
                    SelectCustomerRow(maKH);
                }
                else if (result == ConcurrencyUpdateResult.ConcurrencyConflict)
                {
                    MessageBox.Show(
                        "Dữ liệu này vừa bị người khác cập nhật ở một màn hình khác!\n" +
                        "Thao tác của bạn không được chấp nhận để tránh ghi đè dữ liệu.\n" +
                        "Hệ thống sẽ tự động làm mới lại dữ liệu mới nhất.",
                        "Xung đột dữ liệu (Concurrency Conflict)",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    await _gridQuery.RefreshAsync(null);
                    SelectCustomerRow(maKH);
                }
                else
                {
                    SetStatus(UiStatusKind.Error, "Không thể cập nhật khách hàng.");
                    MessageBox.Show("Cập nhật khách hàng thất bại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                UiErrorHandler.Show(this, "FRMKHACHHANG_UI_ERROR", "Đã xảy ra lỗi khi sửa khách hàng.", ex);
            }
        }

        private async void btnXoa_Click(object sender, EventArgs e)
        {
            string maKH = txtMaKH.Text.Trim();
            string tenKH = txtTenKH.Text.Trim();
            if (!ValidationHelper.IsNotEmpty(maKH))
            {
                ShowValidation(txtMaKH, "Vui lòng chọn khách hàng cần xóa.");
                return;
            }

            try
            {
                KhachHangUsageStats stats = khachHangDAL.GetUsageStatistics(maKH);
                if (stats.HasUsage)
                {
                    List<string> refs = new List<string>();
                    if (stats.OrderCount > 0)
                        refs.Add(string.Format("{0} đơn hàng", stats.OrderCount));
                    if (stats.InvoiceCount > 0)
                        refs.Add(string.Format("{0} hóa đơn", stats.InvoiceCount));

                    string refSummary = string.Join(", ", refs.ToArray());

                    MessageBox.Show(
                        string.Format("Không thể xóa khách hàng [{0}] vì đang có dữ liệu ({1}).", maKH, refSummary),
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                DialogResult confirm = MessageBox.Show(
                    string.Format("Bạn có chắc chắn muốn xóa khách hàng [{0}] không?", maKH),
                    "Xác nhận xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (confirm == DialogResult.Yes)
                {
                    if (khachHangDAL.Delete(maKH))
                    {
                        ClearInputs();
                        await _gridQuery.RefreshAsync(null);
                        SetStatus(UiStatusKind.Success, string.Format("Đã xóa khách hàng {0}.", maKH));
                        UiFeedbackHelper.ShowSuccess(this, string.Format("Đã xóa khách hàng [{0}] thành công.", maKH));
                    }
                    else
                    {
                        SetStatus(UiStatusKind.Warning, "Không thể xóa khách hàng này.");
                        MessageBox.Show("Không thể xóa khách hàng này.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                UiErrorHandler.Show(this, "FRMKHACHHANG_UI_ERROR", "Lỗi khi xóa khách hàng.", ex);
            }
        }

        private async void btnLamMoi_Click(object sender, EventArgs e)
        {
            ClearInputs();
            await _gridQuery.RefreshAsync(btnLamMoi);
        }

        private async void btnTimKiem_Click(object sender, EventArgs e)
        {
            await _gridQuery.RefreshAsync(btnTimKiem);
        }

        private async void txtTimKiem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                await _gridQuery.RefreshAsync(btnTimKiem);
            }
        }


        private void ShowValidation(Control control, string message)
        {
            UiInteractionHelper.ShowValidationError(validationErrors, control, message);
            SetStatus(UiStatusKind.Warning, message);
        }

        private void SetStatus(UiStatusKind kind, string message)
        {
            UiStyler.StyleStatusLabel(lblPageStatus, kind, message);
        }

        private void SelectCustomerRow(string maKH)
        {
            if (string.IsNullOrEmpty(maKH) || dgvKhachHang.Rows.Count == 0)
                return;

            for (int i = 0; i < dgvKhachHang.Rows.Count; i++)
            {
                DataGridViewRow row = dgvKhachHang.Rows[i];
                if (row.Cells["colMaKH"].Value != null &&
                    string.Equals(row.Cells["colMaKH"].Value.ToString().Trim(), maKH.Trim(), StringComparison.OrdinalIgnoreCase))
                {
                    dgvKhachHang.ClearSelection();
                    row.Selected = true;
                    if (dgvKhachHang.Columns.Contains("colTenKH"))
                    {
                        dgvKhachHang.CurrentCell = row.Cells["colTenKH"];
                    }
                    dgvKhachHang_CellClick(dgvKhachHang, new DataGridViewCellEventArgs(0, i));
                    break;
                }
            }
        }
    }
}
