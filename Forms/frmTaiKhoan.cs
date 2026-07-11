using System;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;
using DNQH_KeToanBanHang.Constants;
using DNQH_KeToanBanHang.DataAccess;
using DNQH_KeToanBanHang.Helpers;
using DNQH_KeToanBanHang.Models;

namespace DNQH_KeToanBanHang.Forms
{
    public partial class frmTaiKhoan : Form
    {
        private readonly TaiKhoanDAL taiKhoanDAL;
        private readonly NhanVienDAL nhanVienDAL;
        private readonly Label lblPageStatus;
        private readonly ErrorProvider _validationErrors;
        private readonly UiGridQueryController _gridQuery;
        private int _currentVersion = 1;

        public frmTaiKhoan()
        {
            InitializeComponent();
            _validationErrors = UiStyler.CreateErrorProvider(this);
            lblPageStatus = BuildFoundationLayout();
            ApplyFoundationDesign();
            taiKhoanDAL = new TaiKhoanDAL();
            nhanVienDAL = new NhanVienDAL();
            _gridQuery = new UiGridQueryController(
                this, txtTimKiem, dgvTaiKhoan, lblPageStatus,
                delegate(string keyword) { return delegate { return taiKhoanDAL.Search(keyword); }; },
                delegate(int count) { return string.Format("Đang hiển thị {0:N0} tài khoản.", count); },
                "Đang tải danh sách tài khoản...",
                "Không tìm thấy tài khoản phù hợp.",
                delegate(Exception ex) { UiErrorHandler.Show(this, "FRMTAIKHOAN_UI_ERROR", "Lỗi khi tải danh sách tài khoản.", ex); });
        }

        private Label BuildFoundationLayout()
        {
            TableLayoutPanel fields = UiLayoutBuilder.CreateStructuredGridLayout(
                3, 100,
                UiLayoutBuilder.Field(lblMaTK, txtMaTK, UiFieldSize.Code),
                UiLayoutBuilder.Field(lblNhanVien, cboNhanVien, UiFieldSize.Wide),
                UiLayoutBuilder.Field(lblTenDangNhap, txtTenDangNhap, UiFieldSize.Medium),
                UiLayoutBuilder.Field(lblVaiTro, cboVaiTro, UiFieldSize.Selection),
                UiLayoutBuilder.Field(lblMatKhau, txtMatKhau, UiFieldSize.Medium),
                UiLayoutBuilder.Field(lblTrangThai, cboTrangThai, UiFieldSize.Selection));
            UiLayoutBuilder.AddFullWidthNote(fields, lblMatKhauHint);

            Button btnXuatCsv = CsvExporter.CreateExportButton(dgvTaiKhoan, "DanhSachTaiKhoan", "DANH SÁCH TÀI KHOẢN");
            return UiLayoutBuilder.BuildCrudPage(
                this, lblTitle, "Phân quyền tài khoản và kiểm soát trạng thái truy cập",
                grpThongTin, fields,
                new Control[] { btnThem, btnSua, btnXoa, btnLamMoi, lblTimKiem, txtTimKiem, btnTimKiem, btnXuatCsv },
                dgvTaiKhoan);
        }

        private void ApplyFoundationDesign()
        {
            dgvTaiKhoan.AutoGenerateColumns = false;
            UiLayoutBuilder.ApplyCrudStyle(this, btnThem, btnSua, btnXoa, btnLamMoi, btnTimKiem, dgvTaiKhoan, lblPageStatus);
            CsvExporter.AttachExportContextMenu(dgvTaiKhoan, "DanhSachTaiKhoan", "DANH SÁCH TÀI KHOẢN");
            UiStyler.SetAccessibleText(txtMaTK, "Mã tài khoản", "Mã định danh tài khoản.");
            UiStyler.SetAccessibleText(cboNhanVien, "Nhân viên liên kết", "Chọn nhân viên sở hữu tài khoản.");
            UiStyler.SetAccessibleText(txtTenDangNhap, "Tên đăng nhập", "Tên đăng nhập duy nhất.");
            UiStyler.SetAccessibleText(txtMatKhau, "Mật khẩu", "Nhập mật khẩu mới hoặc để trống khi không thay đổi.");
            UiStyler.SetAccessibleText(cboVaiTro, "Vai trò", "Chọn quyền sử dụng ứng dụng.");
            UiStyler.SetAccessibleText(dgvTaiKhoan, "Danh sách tài khoản", "Chọn một dòng để sửa hoặc xóa.");
            ConfigureGridColumns();
            dgvTaiKhoan.CellFormatting += dgvTaiKhoan_CellFormatting;
        }

        private void ConfigureGridColumns()
        {
            dgvTaiKhoan.AutoGenerateColumns = false;
            dgvTaiKhoan.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            colMaTK.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colMaTK.Width = 85;
            colMaTK.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            colMaNV.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colMaNV.Width = 85;
            colMaNV.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            colHoTen.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colHoTen.FillWeight = 160F;
            colHoTen.MinimumWidth = 180;
            colHoTen.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            colTenDangNhap.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colTenDangNhap.Width = 140;
            colTenDangNhap.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            colVaiTro.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colVaiTro.Width = 110;
            colVaiTro.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            colTrangThai.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colTrangThai.Width = 115;
            colTrangThai.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        private void dgvTaiKhoan_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            string colName = dgvTaiKhoan.Columns[e.ColumnIndex].Name;
            if (colName == "colTrangThai" && e.Value != null)
            {
                string status = e.Value.ToString().Trim();
                if (EntityStatusConstants.Account.IsActive(status))
                {
                    e.CellStyle.ForeColor = System.Drawing.Color.FromArgb(16, 120, 60);
                    e.CellStyle.Font = new System.Drawing.Font(dgvTaiKhoan.Font, System.Drawing.FontStyle.Bold);
                }
                else if (EntityStatusConstants.Account.IsLocked(status))
                {
                    e.CellStyle.ForeColor = System.Drawing.Color.FromArgb(180, 20, 20);
                    e.CellStyle.Font = new System.Drawing.Font(dgvTaiKhoan.Font, System.Drawing.FontStyle.Bold);
                }
            }
        }

        private async void frmTaiKhoan_Load(object sender, EventArgs e)
        {
            this.ApplyResponsiveUI();
            if (!SessionManager.IsAdmin())
            {
                MessageBox.Show("Chỉ Quản trị viên mới có quyền truy cập chức năng này.", "Từ chối truy cập", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
                return;
            }

            await LoadNhanVienComboboxAsync();

            UiComboBoxHelper.PopulateItems(cboVaiTro, RoleConstants.AllRoles, RoleConstants.Admin);
            UiComboBoxHelper.PopulateItems(cboTrangThai, EntityStatusConstants.Account.All, EntityStatusConstants.Account.Active);

            ClearInputs();
            await _gridQuery.RefreshAsync(null);
        }

        private async Task LoadNhanVienComboboxAsync()
        {
            try
            {
                DataTable dt = null;
                UiStyler.StyleStatusLabel(lblPageStatus, UiStatusKind.Information, "Đang tải danh sách nhân viên...");
                await UiFeedbackHelper.RunBusyAsync(
                    this,
                    null,
                    null,
                    delegate
                    {
                        dt = nhanVienDAL.GetAll();
                        return true;
                    });
                if (IsDisposed)
                {
                    return;
                }

                DataTable dtCombo = dt.Clone();
                dtCombo.Columns.Add("DisplayNV", typeof(string));

                foreach (DataRow row in dt.Rows)
                {
                    DataRow newRow = dtCombo.NewRow();
                    newRow.ItemArray = row.ItemArray;
                    string maNV = row["MaNV"].ToString().Trim();
                    string hoTen = row["HoTen"].ToString().Trim();
                    newRow["DisplayNV"] = string.Format("{0} ({1})", hoTen, maNV);
                    dtCombo.Rows.Add(newRow);
                }

                cboNhanVien.DataSource = dtCombo;
                cboNhanVien.DisplayMember = "DisplayNV";
                cboNhanVien.ValueMember = "MaNV";
            }
            catch (Exception ex)
            {
                UiErrorHandler.Show(this, "FRMTAIKHOAN_UI_ERROR", "Lỗi khi tải danh sách nhân viên.", ex);
            }
        }

        private void ClearInputs()
        {
            _currentVersion = 1;
            txtMaTK.Text = AutoCodeHelper.GetNextMaTK();
            txtTenDangNhap.Clear();
            txtMatKhau.Clear();

            if (cboNhanVien.Items.Count > 0)
                cboNhanVien.SelectedIndex = 0;

            if (cboVaiTro.Items.Count > 0)
                cboVaiTro.SelectedIndex = 0;

            if (cboTrangThai.Items.Count > 0)
                cboTrangThai.SelectedIndex = 0;

            txtMaTK.ReadOnly = false;
            txtTenDangNhap.Focus();
        }

        private void dgvTaiKhoan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvTaiKhoan.Rows.Count)
            {
                DataGridViewRow row = dgvTaiKhoan.Rows[e.RowIndex];
                string maTK = row.Cells["colMaTK"].Value != null ? row.Cells["colMaTK"].Value.ToString().Trim() : string.Empty;

                try
                {
                    TaiKhoan tk = taiKhoanDAL.GetById(maTK);
                    if (tk != null)
                    {
                        _currentVersion = tk.Version;
                        txtMaTK.Text = tk.MaTK;
                        txtTenDangNhap.Text = tk.TenDangNhap;
                        txtMatKhau.Clear();

                        if (!string.IsNullOrEmpty(tk.MaNV))
                            cboNhanVien.SelectedValue = tk.MaNV;
                        else
                            cboNhanVien.SelectedIndex = -1;

                        UiComboBoxHelper.SafeSelect(cboVaiTro, tk.VaiTro);
                        UiComboBoxHelper.SafeSelect(cboTrangThai, tk.TrangThai);

                        txtMaTK.ReadOnly = true;
                    }
                }
                catch (Exception ex)
                {
                    UiErrorHandler.Show(this, "FRMTAIKHOAN_UI_ERROR", "Lỗi khi đọc chi tiết tài khoản.", ex);
                }
            }
        }

        private async void btnThem_Click(object sender, EventArgs e)
        {
            string maTK = txtMaTK.Text.Trim();
            if (string.IsNullOrEmpty(maTK))
            {
                maTK = AutoCodeHelper.GetNextMaTK();
                txtMaTK.Text = maTK;
            }

            string tenDangNhap = txtTenDangNhap.Text.Trim();
            string matKhau = txtMatKhau.Text.Trim();
            string vaiTro = cboVaiTro.SelectedItem != null ? cboVaiTro.SelectedItem.ToString() : string.Empty;
            string trangThai = cboTrangThai.SelectedItem != null ? cboTrangThai.SelectedItem.ToString() : EntityStatusConstants.Account.Active;

            if (cboNhanVien.SelectedValue == null)
            {
                ShowValidation(cboNhanVien, "Vui lòng chọn nhân viên.");
                return;
            }
            string maNV = cboNhanVien.SelectedValue.ToString().Trim();

            if (!ValidationHelper.IsNotEmpty(tenDangNhap))
            {
                ShowValidation(txtTenDangNhap, "Vui lòng nhập tên đăng nhập.");
                return;
            }

            if (!ValidationHelper.IsNotEmpty(matKhau))
            {
                ShowValidation(txtMatKhau, "Vui lòng nhập mật khẩu khởi tạo cho tài khoản mới.");
                return;
            }

            try
            {
                if (taiKhoanDAL.Exists(maTK))
                {
                    string suggestMa = AutoCodeHelper.GetNextMaTK();
                    DialogResult dr = MessageBox.Show(
                        string.Format("Mã tài khoản [{0}] đã tồn tại trong hệ thống.\nBạn có muốn tự động sử dụng mã mới [{1}] không?", maTK, suggestMa),
                        "Trùng mã tài khoản",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    if (dr == DialogResult.Yes)
                    {
                        maTK = suggestMa;
                        txtMaTK.Text = suggestMa;
                    }
                    else
                    {
                        txtMaTK.Focus();
                        return;
                    }
                }

                if (taiKhoanDAL.ExistsUsername(tenDangNhap))
                {
                    MessageBox.Show(string.Format("Tên đăng nhập [{0}] đã có người sử dụng.", tenDangNhap), "Trùng tên đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtTenDangNhap.Focus();
                    return;
                }

                if (taiKhoanDAL.ExistsMaNV(maNV))
                {
                    MessageBox.Show(string.Format("Nhân viên [{0}] đã được gán tài khoản khác. Mỗi nhân viên chỉ có tối đa một tài khoản.", maNV), "Ràng buộc duy nhất", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cboNhanVien.Focus();
                    return;
                }

                TaiKhoan tk = new TaiKhoan(maTK, maNV, tenDangNhap, matKhau, vaiTro, trangThai);
                if (taiKhoanDAL.Insert(tk))
                {
                    UiFeedbackHelper.ShowSuccess(this, "Đã tạo tài khoản thành công.");
                    await _gridQuery.RefreshAsync(null);
                    ClearInputs();
                }
                else
                {
                    MessageBox.Show("Thêm tài khoản thất bại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                UiErrorHandler.Show(this, "FRMTAIKHOAN_UI_ERROR", "Lỗi khi thêm tài khoản.", ex);
            }
        }

        private async void btnSua_Click(object sender, EventArgs e)
        {
            string maTK = txtMaTK.Text.Trim();
            string tenDangNhap = txtTenDangNhap.Text.Trim();
            string matKhau = txtMatKhau.Text.Trim();
            string vaiTro = cboVaiTro.SelectedItem != null ? cboVaiTro.SelectedItem.ToString() : string.Empty;
            string trangThai = cboTrangThai.SelectedItem != null ? cboTrangThai.SelectedItem.ToString() : EntityStatusConstants.Account.Active;

            if (!ValidationHelper.IsNotEmpty(maTK))
            {
                ShowValidation(txtMaTK, "Vui lòng chọn tài khoản cần sửa từ danh sách.");
                return;
            }

            if (cboNhanVien.SelectedValue == null)
            {
                ShowValidation(cboNhanVien, "Vui lòng chọn nhân viên.");
                return;
            }
            string maNV = cboNhanVien.SelectedValue.ToString().Trim();

            if (!ValidationHelper.IsNotEmpty(tenDangNhap))
            {
                ShowValidation(txtTenDangNhap, "Vui lòng nhập tên đăng nhập.");
                return;
            }

            try
            {
                if (!taiKhoanDAL.Exists(maTK))
                {
                    MessageBox.Show(string.Format("Tài khoản [{0}] không tồn tại.", maTK), "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (taiKhoanDAL.ExistsUsername(tenDangNhap, maTK))
                {
                    MessageBox.Show(string.Format("Tên đăng nhập [{0}] đã có tài khoản khác sử dụng.", tenDangNhap), "Trùng tên đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtTenDangNhap.Focus();
                    return;
                }

                if (taiKhoanDAL.ExistsMaNV(maNV, maTK))
                {
                    MessageBox.Show(string.Format("Nhân viên [{0}] đã có tài khoản khác. Mỗi nhân viên chỉ có tối đa một tài khoản.", maNV), "Ràng buộc duy nhất", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cboNhanVien.Focus();
                    return;
                }

                // If currently logged in user is editing themselves, prevent self-lockout
                if (SessionManager.CurrentUser != null && SessionManager.CurrentUser.MaTK == maTK)
                {
                    if (EntityStatusConstants.Account.IsLocked(trangThai))
                    {
                        MessageBox.Show("Bạn không thể tự khóa tài khoản của chính mình!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (!RoleConstants.IsAdmin(vaiTro))
                    {
                        MessageBox.Show("Bạn không thể tự hạ quyền Quản trị viên của chính mình!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                bool changePassword = !string.IsNullOrEmpty(matKhau);
                TaiKhoan tk = new TaiKhoan(maTK, maNV, tenDangNhap, matKhau, vaiTro, trangThai, _currentVersion);
                ConcurrencyUpdateResult result = taiKhoanDAL.UpdateWithResult(tk, changePassword);

                if (result == ConcurrencyUpdateResult.Success)
                {
                    _currentVersion = tk.Version;
                    // Đồng bộ phiên đăng nhập nếu đang sửa chính tài khoản đang đăng nhập
                    if (SessionManager.CurrentUser != null && SessionManager.CurrentUser.MaTK == maTK)
                    {
                        SessionManager.CurrentUser.TenDangNhap = tenDangNhap;
                        SessionManager.CurrentUser.VaiTro = vaiTro;
                        NhanVien emp = nhanVienDAL.GetByMaNV(maNV);
                        if (emp != null)
                        {
                            SessionManager.CurrentUser.MaNV = emp.MaNV;
                            SessionManager.CurrentUser.HoTen = emp.HoTen;
                        }
                    }

                    UiFeedbackHelper.ShowSuccess(this, "Đã cập nhật tài khoản thành công.");
                    await _gridQuery.RefreshAsync(null);
                    SelectAccountRow(maTK);
                    txtMatKhau.Clear();
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
                    SelectAccountRow(maTK);
                }
                else
                {
                    MessageBox.Show("Cập nhật thất bại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                UiErrorHandler.Show(this, "FRMTAIKHOAN_UI_ERROR", "Lỗi khi cập nhật tài khoản.", ex);
            }
        }

        private async void btnXoa_Click(object sender, EventArgs e)
        {
            string maTK = txtMaTK.Text.Trim();
            if (!ValidationHelper.IsNotEmpty(maTK))
            {
                ShowValidation(txtMaTK, "Vui lòng chọn tài khoản cần xóa từ danh sách.");
                return;
            }

            if (SessionManager.CurrentUser != null && SessionManager.CurrentUser.MaTK == maTK)
            {
                MessageBox.Show("Bạn không thể xóa tài khoản của chính bạn đang đăng nhập!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirm = MessageBox.Show(
                string.Format("Bạn có chắc chắn muốn xóa tài khoản [{0}] không?", maTK),
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                try
                {
                    if (taiKhoanDAL.Delete(maTK))
                    {
                        UiFeedbackHelper.ShowSuccess(this, "Đã xóa tài khoản thành công.");
                        await _gridQuery.RefreshAsync(null);
                        ClearInputs();
                    }
                    else
                    {
                        MessageBox.Show("Không thể xóa tài khoản này do có ràng buộc dữ liệu.", "Cảnh báo ràng buộc", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    UiErrorHandler.Show(this, "FRMTAIKHOAN_UI_ERROR", "Lỗi khi xóa tài khoản.", ex);
                }
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
                await _gridQuery.RefreshAsync(btnTimKiem);
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void SelectAccountRow(string maTK)
        {
            if (string.IsNullOrEmpty(maTK) || dgvTaiKhoan.Rows.Count == 0)
                return;

            for (int i = 0; i < dgvTaiKhoan.Rows.Count; i++)
            {
                DataGridViewRow row = dgvTaiKhoan.Rows[i];
                if (row.Cells["colMaTK"].Value != null &&
                    string.Equals(row.Cells["colMaTK"].Value.ToString().Trim(), maTK.Trim(), StringComparison.OrdinalIgnoreCase))
                {
                    dgvTaiKhoan.ClearSelection();
                    row.Selected = true;
                    if (dgvTaiKhoan.Columns.Contains("colTenDangNhap"))
                    {
                        dgvTaiKhoan.CurrentCell = row.Cells["colTenDangNhap"];
                    }
                    dgvTaiKhoan_CellClick(dgvTaiKhoan, new DataGridViewCellEventArgs(0, i));
                    break;
                }
            }
        }

        private void ShowValidation(Control control, string message)
        {
            UiInteractionHelper.ShowValidationError(_validationErrors, control, message);
            UiStyler.StyleStatusLabel(lblPageStatus, UiStatusKind.Warning, message);
        }
    }
}
