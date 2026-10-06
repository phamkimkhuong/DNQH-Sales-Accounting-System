using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using DNQH_KeToanBanHang.Constants;
using DNQH_KeToanBanHang.DataAccess;
using DNQH_KeToanBanHang.Helpers;
using DNQH_KeToanBanHang.Models;

namespace DNQH_KeToanBanHang.Forms
{
    public partial class frmNhanVien : Form
    {
        private readonly NhanVienDAL nhanVienDAL;
        private readonly Label lblPageStatus;
        private readonly ErrorProvider _validationErrors;
        private readonly UiGridQueryController _gridQuery;
        private int _currentVersion = 1;

        public frmNhanVien()
        {
            InitializeComponent();
            _validationErrors = UiStyler.CreateErrorProvider(this);
            lblPageStatus = BuildFoundationLayout();
            ApplyFoundationDesign();
            nhanVienDAL = new NhanVienDAL();
            _gridQuery = new UiGridQueryController(
                this, txtTimKiem, dgvNhanVien, lblPageStatus,
                delegate(string keyword) { return delegate { return nhanVienDAL.Search(keyword); }; },
                delegate(int count) { return string.Format("Đang hiển thị {0:N0} nhân viên.", count); },
                "Đang tải danh sách nhân viên...",
                "Không tìm thấy nhân viên phù hợp.",
                delegate(Exception ex) { UiErrorHandler.Show(this, "FRMNHANVIEN_UI_ERROR", "Lỗi khi tải danh sách nhân viên.", ex); });
        }

        private Label BuildFoundationLayout()
        {
            TableLayoutPanel fields = UiLayoutBuilder.CreateStructuredGridLayout(
                3, 95,
                UiLayoutBuilder.Field(lblMaNV, txtMaNV, UiFieldSize.Code),
                UiLayoutBuilder.Field(lblHoTen, txtHoTen, UiFieldSize.Medium),
                UiLayoutBuilder.Field(lblGioiTinh, cboGioiTinh, UiFieldSize.ShortText),
                UiLayoutBuilder.Field(lblNgaySinh, dtpNgaySinh, UiFieldSize.Date),
                UiLayoutBuilder.Field(lblSoDienThoai, txtSoDienThoai, UiFieldSize.ShortText),
                UiLayoutBuilder.Field(lblChucVu, cboChucVu, UiFieldSize.Selection),
                UiLayoutBuilder.Field(lblTrangThai, cboTrangThai, UiFieldSize.Selection),
                UiLayoutBuilder.SpannedField(lblDiaChi, txtDiaChi, 2));

            Button btnXuatCsv = CsvExporter.CreateExportButton(dgvNhanVien, "DanhSachNhanVien", "DANH SÁCH NHÂN VIÊN");
            return UiLayoutBuilder.BuildCrudPage(
                this, lblTitle, "Quản lý hồ sơ, chức vụ và trạng thái nhân viên",
                grpThongTin, fields,
                new Control[] { btnThem, btnSua, btnXoa, btnLamMoi, lblTimKiem, txtTimKiem, btnTimKiem, btnXuatCsv },
                dgvNhanVien);
        }

        private void ApplyFoundationDesign()
        {
            dgvNhanVien.AutoGenerateColumns = false;
            UiLayoutBuilder.ApplyCrudStyle(this, btnThem, btnSua, btnXoa, btnLamMoi, btnTimKiem, dgvNhanVien, lblPageStatus);
            CsvExporter.AttachExportContextMenu(dgvNhanVien, "DanhSachNhanVien", "DANH SÁCH NHÂN VIÊN");
            UiStyler.SetAccessibleText(txtMaNV, "Mã nhân viên", "Mã định danh nhân viên.");
            UiStyler.SetAccessibleText(txtHoTen, "Họ tên nhân viên", "Họ tên nhân viên bắt buộc.");
            UiStyler.SetAccessibleText(dtpNgaySinh, "Ngày sinh", "Chọn ngày sinh nếu có.");
            UiStyler.SetAccessibleText(cboChucVu, "Chức vụ", "Chọn chức vụ nhân viên.");
            UiStyler.SetAccessibleText(cboTrangThai, "Trạng thái nhân viên", "Chọn trạng thái làm việc.");
            UiStyler.SetAccessibleText(dgvNhanVien, "Danh sách nhân viên", "Chọn một dòng để sửa hoặc xóa.");
            ConfigureGridColumns();
            dgvNhanVien.CellFormatting += dgvNhanVien_CellFormatting;
        }

        private void ConfigureGridColumns()
        {
            dgvNhanVien.AutoGenerateColumns = false;
            dgvNhanVien.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            colMaNV.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colMaNV.Width = 90;
            colMaNV.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            colHoTen.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colHoTen.FillWeight = 150F;
            colHoTen.MinimumWidth = 160;
            colHoTen.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            colNgaySinh.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colNgaySinh.Width = 105;
            colNgaySinh.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colNgaySinh.DefaultCellStyle.Format = "dd/MM/yyyy";

            colGioiTinh.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colGioiTinh.Width = 65;
            colGioiTinh.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            colSoDienThoai.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colSoDienThoai.Width = 115;
            colSoDienThoai.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            colChucVu.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colChucVu.Width = 120;
            colChucVu.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            colDiaChi.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colDiaChi.FillWeight = 180F;
            colDiaChi.MinimumWidth = 180;
            colDiaChi.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            colTrangThai.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colTrangThai.Width = 115;
            colTrangThai.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        private void dgvNhanVien_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            string colName = dgvNhanVien.Columns[e.ColumnIndex].Name;
            if (colName == "colTrangThai" && e.Value != null)
            {
                string status = e.Value.ToString().Trim();
                if (EntityStatusConstants.Employee.IsActive(status))
                {
                    e.CellStyle.ForeColor = System.Drawing.Color.FromArgb(16, 120, 60);
                    e.CellStyle.Font = new System.Drawing.Font(dgvNhanVien.Font, System.Drawing.FontStyle.Bold);
                }
                else
                {
                    e.CellStyle.ForeColor = System.Drawing.Color.FromArgb(180, 20, 20);
                    e.CellStyle.Font = new System.Drawing.Font(dgvNhanVien.Font, System.Drawing.FontStyle.Bold);
                }
            }
        }

        private async void frmNhanVien_Load(object sender, EventArgs e)
        {
            this.ApplyResponsiveUI();
            if (!SessionManager.IsAdmin())
            {
                MessageBox.Show("Bạn không có quyền truy cập vào chức năng Quản lý Nhân viên.\nChỉ Quản trị viên mới có quyền này.",
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
            txtMaNV.Text = AutoCodeHelper.GetNextMaNV();
            txtHoTen.Clear();
            txtSoDienThoai.Clear();
            txtDiaChi.Clear();

            dtpNgaySinh.Checked = false;
            cboGioiTinh.SelectedIndex = -1;
            cboChucVu.SelectedIndex = -1;

            UiComboBoxHelper.PopulateItems(cboTrangThai, EntityStatusConstants.Employee.All, EntityStatusConstants.Employee.Active);

            txtMaNV.ReadOnly = false;
            txtHoTen.Focus();
        }

        private void dgvNhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvNhanVien.Rows.Count)
            {
                DataGridViewRow row = dgvNhanVien.Rows[e.RowIndex];
                string maNV = row.Cells["colMaNV"].Value != null ? row.Cells["colMaNV"].Value.ToString().Trim() : string.Empty;

                try
                {
                    NhanVien nv = nhanVienDAL.GetByMaNV(maNV);
                    if (nv != null)
                    {
                        _currentVersion = nv.Version;
                        txtMaNV.Text = nv.MaNV;
                        txtHoTen.Text = nv.HoTen;
                        txtSoDienThoai.Text = nv.SoDienThoai;
                        txtDiaChi.Text = nv.DiaChi;

                        if (nv.NgaySinh.HasValue)
                        {
                            dtpNgaySinh.Checked = true;
                            dtpNgaySinh.Value = nv.NgaySinh.Value;
                        }
                        else
                        {
                            dtpNgaySinh.Checked = false;
                        }

                        if (!string.IsNullOrEmpty(nv.GioiTinh) && cboGioiTinh.Items.Contains(nv.GioiTinh))
                            cboGioiTinh.SelectedItem = nv.GioiTinh;
                        else
                            cboGioiTinh.SelectedIndex = -1;

                        if (!string.IsNullOrEmpty(nv.ChucVu) && cboChucVu.Items.Contains(nv.ChucVu))
                            cboChucVu.SelectedItem = nv.ChucVu;
                        else
                            cboChucVu.SelectedIndex = -1;

                        UiComboBoxHelper.SafeSelect(cboTrangThai, nv.TrangThai);

                        txtMaNV.ReadOnly = true;
                    }
                }
                catch (Exception ex)
                {
                    UiErrorHandler.Show(this, "FRMNHANVIEN_UI_ERROR", "Lỗi khi đọc chi tiết nhân viên.", ex);
                }
            }
        }

        private async void btnThem_Click(object sender, EventArgs e)
        {
            string maNV = txtMaNV.Text.Trim();
            if (string.IsNullOrEmpty(maNV))
            {
                maNV = AutoCodeHelper.GetNextMaNV();
                txtMaNV.Text = maNV;
            }

            string hoTen = txtHoTen.Text.Trim();
            string sdt = txtSoDienThoai.Text.Trim();
            string diaChi = txtDiaChi.Text.Trim();
            string gioiTinh = cboGioiTinh.SelectedItem != null ? cboGioiTinh.SelectedItem.ToString() : null;
            string chucVu = cboChucVu.SelectedItem != null ? cboChucVu.SelectedItem.ToString() : null;
            string trangThai = cboTrangThai.SelectedItem != null ? cboTrangThai.SelectedItem.ToString() : EntityStatusConstants.Employee.Active;
            DateTime? ngaySinh = dtpNgaySinh.Checked ? (DateTime?)dtpNgaySinh.Value.Date : null;

            if (!ValidationHelper.IsNotEmpty(hoTen))
            {
                ShowValidation(txtHoTen, "Vui lòng nhập họ tên nhân viên.");
                return;
            }

            if (!string.IsNullOrEmpty(sdt) && !ValidationHelper.IsValidPhoneNumber(sdt))
            {
                ShowValidation(txtSoDienThoai, "Số điện thoại không hợp lệ (cần từ 9 - 11 chữ số).");
                return;
            }

            try
            {
                if (nhanVienDAL.Exists(maNV))
                {
                    string suggestMa = AutoCodeHelper.GetNextMaNV();
                    DialogResult dr = MessageBox.Show(
                        string.Format("Mã nhân viên [{0}] đã tồn tại trong hệ thống.\nBạn có muốn tự động sử dụng mã mới [{1}] không?", maNV, suggestMa),
                        "Trùng mã nhân viên",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    if (dr == DialogResult.Yes)
                    {
                        maNV = suggestMa;
                        txtMaNV.Text = suggestMa;
                    }
                    else
                    {
                        txtMaNV.Focus();
                        return;
                    }
                }

                NhanVien nv = new NhanVien(maNV, hoTen, ngaySinh, gioiTinh, sdt, diaChi, chucVu, trangThai);
                if (nhanVienDAL.Insert(nv))
                {
                    UiFeedbackHelper.ShowSuccess(this, "Đã thêm nhân viên thành công.");
                    await _gridQuery.RefreshAsync(null);
                    ClearInputs();
                }
                else
                {
                    MessageBox.Show("Thêm mới thất bại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                UiErrorHandler.Show(this, "FRMNHANVIEN_UI_ERROR", "Lỗi khi thêm nhân viên.", ex);
            }
        }

        private async void btnSua_Click(object sender, EventArgs e)
        {
            string maNV = txtMaNV.Text.Trim();
            string hoTen = txtHoTen.Text.Trim();
            string sdt = txtSoDienThoai.Text.Trim();
            string diaChi = txtDiaChi.Text.Trim();
            string gioiTinh = cboGioiTinh.SelectedItem != null ? cboGioiTinh.SelectedItem.ToString() : null;
            string chucVu = cboChucVu.SelectedItem != null ? cboChucVu.SelectedItem.ToString() : null;
            string trangThai = cboTrangThai.SelectedItem != null ? cboTrangThai.SelectedItem.ToString() : EntityStatusConstants.Employee.Active;
            DateTime? ngaySinh = dtpNgaySinh.Checked ? (DateTime?)dtpNgaySinh.Value.Date : null;

            if (!ValidationHelper.IsNotEmpty(maNV))
            {
                ShowValidation(txtMaNV, "Vui lòng chọn nhân viên cần sửa từ danh sách.");
                return;
            }

            if (!ValidationHelper.IsNotEmpty(hoTen))
            {
                ShowValidation(txtHoTen, "Vui lòng nhập họ tên nhân viên.");
                return;
            }

            if (!string.IsNullOrEmpty(sdt) && !ValidationHelper.IsValidPhoneNumber(sdt))
            {
                ShowValidation(txtSoDienThoai, "Số điện thoại không hợp lệ (cần từ 9 - 11 chữ số).");
                return;
            }

            try
            {
                if (!nhanVienDAL.Exists(maNV))
                {
                    MessageBox.Show(string.Format("Nhân viên [{0}] không tồn tại.", maNV), "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                NhanVien nv = new NhanVien(maNV, hoTen, ngaySinh, gioiTinh, sdt, diaChi, chucVu, trangThai, _currentVersion);
                ConcurrencyUpdateResult result = nhanVienDAL.UpdateWithResult(nv);
                if (result == ConcurrencyUpdateResult.Success)
                {
                    _currentVersion = nv.Version;
                    UiFeedbackHelper.ShowSuccess(this, "Đã cập nhật thông tin nhân viên thành công.");
                    await _gridQuery.RefreshAsync(null);
                    SelectEmployeeRow(maNV);
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
                    SelectEmployeeRow(maNV);
                }
                else
                {
                    MessageBox.Show("Cập nhật thất bại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                UiErrorHandler.Show(this, "FRMNHANVIEN_UI_ERROR", "Lỗi khi cập nhật nhân viên.", ex);
            }
        }

        private async void btnXoa_Click(object sender, EventArgs e)
        {
            string maNV = txtMaNV.Text.Trim();
            if (!ValidationHelper.IsNotEmpty(maNV))
            {
                ShowValidation(txtMaNV, "Vui lòng chọn nhân viên cần xóa từ danh sách.");
                return;
            }

            DialogResult confirm = MessageBox.Show(
                string.Format("Bạn có chắc chắn muốn xóa nhân viên [{0}] không?", maNV),
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                try
                {
                    if (nhanVienDAL.Delete(maNV))
                    {
                        UiFeedbackHelper.ShowSuccess(this, "Đã xóa nhân viên thành công.");
                        await _gridQuery.RefreshAsync(null);
                        ClearInputs();
                    }
                    else
                    {
                        MessageBox.Show("Không thể xóa nhân viên này vì có tài khoản, đơn hàng, hóa đơn hoặc chứng từ liên quan.", "Cảnh báo ràng buộc", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (SqlException sqlEx)
                {
                    if (sqlEx.Number == 547)
                    {
                        MessageBox.Show(
                            string.Format("Không thể xóa nhân viên [{0}] vì đang có tài khoản, đơn hàng, hóa đơn hoặc chứng từ liên quan trong hệ thống.", maNV),
                            "Cảnh báo ràng buộc toàn vẹn",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                    }
                    else
                    {
                        UiErrorHandler.Show(this, "FRMNHANVIEN_UI_ERROR", "Lỗi khi xóa nhân viên.", sqlEx);
                    }
                }
                catch (Exception ex)
                {
                    UiErrorHandler.Show(this, "FRMNHANVIEN_UI_ERROR", "Lỗi khi xóa nhân viên.", ex);
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

        private void SelectEmployeeRow(string maNV)
        {
            if (string.IsNullOrEmpty(maNV) || dgvNhanVien.Rows.Count == 0)
                return;

            for (int i = 0; i < dgvNhanVien.Rows.Count; i++)
            {
                DataGridViewRow row = dgvNhanVien.Rows[i];
                if (row.Cells["colMaNV"].Value != null &&
                    string.Equals(row.Cells["colMaNV"].Value.ToString().Trim(), maNV.Trim(), StringComparison.OrdinalIgnoreCase))
                {
                    dgvNhanVien.ClearSelection();
                    row.Selected = true;
                    if (dgvNhanVien.Columns.Contains("colHoTen"))
                    {
                        dgvNhanVien.CurrentCell = row.Cells["colHoTen"];
                    }
                    dgvNhanVien_CellClick(dgvNhanVien, new DataGridViewCellEventArgs(0, i));
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
