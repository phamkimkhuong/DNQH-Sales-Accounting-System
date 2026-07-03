using System;
using System.Data;
using System.Windows.Forms;
using DNQH_KeToanBanHang.DataAccess;
using DNQH_KeToanBanHang.Helpers;
using DNQH_KeToanBanHang.Models;

namespace DNQH_KeToanBanHang.Forms
{
    public partial class frmNhaCungCap : Form
    {
        private readonly NhaCungCapDAL nhaCungCapDAL;
        private readonly Label lblPageStatus;
        private readonly ErrorProvider _validationErrors;
        private readonly UiGridQueryController _gridQuery;
        private int _currentVersion = 1;

        public frmNhaCungCap()
        {
            InitializeComponent();
            _validationErrors = UiStyler.CreateErrorProvider(this);
            lblPageStatus = BuildFoundationLayout();
            ApplyFoundationDesign();
            nhaCungCapDAL = new NhaCungCapDAL();
            _gridQuery = new UiGridQueryController(
                this, txtTimKiem, dgvNhaCungCap, lblPageStatus,
                delegate(string keyword)
                {
                    return delegate { return string.IsNullOrEmpty(keyword) ? nhaCungCapDAL.GetAll() : nhaCungCapDAL.Search(keyword); };
                },
                delegate(int count) { return string.Format("Đang hiển thị {0:N0} nhà cung cấp.", count); },
                "Đang tải danh sách nhà cung cấp...",
                "Không tìm thấy nhà cung cấp phù hợp.",
                delegate(Exception ex) { UiErrorHandler.Show(this, "FRMNHACUNGCAP_UI_ERROR", "Lỗi khi tải danh sách nhà cung cấp.", ex); });
        }

        private Label BuildFoundationLayout()
        {
            TableLayoutPanel fields = UiLayoutBuilder.CreateStructuredGridLayout(
                2, 95,
                UiLayoutBuilder.Field(lblMaNCC, txtMaNCC, UiFieldSize.Code),
                UiLayoutBuilder.Field(lblTenNCC, txtTenNCC, UiFieldSize.Wide),
                UiLayoutBuilder.Field(lblSoDienThoai, txtSoDienThoai, UiFieldSize.ShortText),
                UiLayoutBuilder.Field(lblEmail, txtEmail, UiFieldSize.Medium),
                UiLayoutBuilder.FullWidthField(lblDiaChi, txtDiaChi));

            Button btnXuatCsv = CsvExporter.CreateExportButton(dgvNhaCungCap, "DanhSachNhaCungCap", "DANH SÁCH NHÀ CUNG CẤP");
            Button btnNhapExcel = ExcelImporter.CreateImportButton(this, ImportEntityType.NhaCungCap, () => _gridQuery.RefreshAsync(null));
            return UiLayoutBuilder.BuildCrudPage(
                this, lblTitle, "Cập nhật hồ sơ và thông tin liên hệ nhà cung cấp",
                grpThongTin, fields,
                new Control[] { btnThem, btnSua, btnXoa, btnLamMoi, lblTimKiem, txtTimKiem, btnTimKiem, btnXuatCsv, btnNhapExcel },
                dgvNhaCungCap);
        }

        private void ApplyFoundationDesign()
        {
            dgvNhaCungCap.AutoGenerateColumns = false;
            UiLayoutBuilder.ApplyCrudStyle(this, btnThem, btnSua, btnXoa, btnLamMoi, btnTimKiem, dgvNhaCungCap, lblPageStatus);
            CsvExporter.AttachExportContextMenu(dgvNhaCungCap, "DanhSachNhaCungCap", "DANH SÁCH NHÀ CUNG CẤP");
            ExcelImporter.AttachImportContextMenu(this, dgvNhaCungCap, ImportEntityType.NhaCungCap, () => _gridQuery.RefreshAsync(null));
            UiStyler.SetAccessibleText(txtMaNCC, "Mã nhà cung cấp", "Mã định danh nhà cung cấp.");
            UiStyler.SetAccessibleText(txtTenNCC, "Tên nhà cung cấp", "Tên nhà cung cấp bắt buộc.");
            UiStyler.SetAccessibleText(txtTimKiem, "Từ khóa tìm kiếm", "Tìm nhà cung cấp theo mã, tên hoặc liên hệ.");
            UiStyler.SetAccessibleText(dgvNhaCungCap, "Danh sách nhà cung cấp", "Chọn một dòng để sửa hoặc xóa.");
            ConfigureGridColumns();
        }

        private void ConfigureGridColumns()
        {
            dgvNhaCungCap.AutoGenerateColumns = false;
            dgvNhaCungCap.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            colMaNCC.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colMaNCC.Width = 95;
            colMaNCC.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            colTenNCC.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colTenNCC.FillWeight = 160F;
            colTenNCC.MinimumWidth = 180;
            colTenNCC.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

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

        private async void frmNhaCungCap_Load(object sender, EventArgs e)
        {
            this.ApplyResponsiveUI();
            if (!SessionManager.IsAdmin() && !SessionManager.IsSales())
            {
                MessageBox.Show("Bạn không có quyền truy cập vào chức năng Quản lý Nhà cung cấp.\nChỉ Quản trị viên và Nhân viên bán hàng mới có quyền này.",
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
            txtMaNCC.Text = AutoCodeHelper.GetNextMaNCC();
            txtTenNCC.Clear();
            txtSoDienThoai.Clear();
            txtDiaChi.Clear();
            txtEmail.Clear();
            txtMaNCC.ReadOnly = false;
            txtTenNCC.Focus();
        }

        private void dgvNhaCungCap_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvNhaCungCap.Rows.Count)
            {
                DataGridViewRow row = dgvNhaCungCap.Rows[e.RowIndex];
                string maNCC = row.Cells["colMaNCC"].Value != null ? row.Cells["colMaNCC"].Value.ToString().Trim() : string.Empty;

                try
                {
                    NhaCungCap ncc = nhaCungCapDAL.GetById(maNCC);
                    if (ncc != null)
                    {
                        _currentVersion = ncc.Version;
                        txtMaNCC.Text = ncc.MaNCC;
                        txtTenNCC.Text = ncc.TenNCC;
                        txtSoDienThoai.Text = ncc.SoDienThoai;
                        txtDiaChi.Text = ncc.DiaChi;
                        txtEmail.Text = ncc.Email;
                    }
                    else
                    {
                        txtMaNCC.Text = maNCC;
                        txtTenNCC.Text = row.Cells["colTenNCC"].Value != null ? row.Cells["colTenNCC"].Value.ToString().Trim() : string.Empty;
                        txtSoDienThoai.Text = row.Cells["colSoDienThoai"].Value != null ? row.Cells["colSoDienThoai"].Value.ToString().Trim() : string.Empty;
                        txtDiaChi.Text = row.Cells["colDiaChi"].Value != null ? row.Cells["colDiaChi"].Value.ToString().Trim() : string.Empty;
                        txtEmail.Text = row.Cells["colEmail"].Value != null ? row.Cells["colEmail"].Value.ToString().Trim() : string.Empty;
                    }
                    txtMaNCC.ReadOnly = true;
                }
                catch (Exception ex)
                {
                    UiErrorHandler.Show(this, "FRMNHACUNGCAP_UI_ERROR", "Lỗi khi đọc chi tiết nhà cung cấp.", ex);
                }
            }
        }

        private async void btnThem_Click(object sender, EventArgs e)
        {
            string maNCC = txtMaNCC.Text.Trim();
            if (string.IsNullOrEmpty(maNCC))
            {
                maNCC = AutoCodeHelper.GetNextMaNCC();
                txtMaNCC.Text = maNCC;
            }

            string tenNCC = txtTenNCC.Text.Trim();
            string sdt = txtSoDienThoai.Text.Trim();
            string diaChi = txtDiaChi.Text.Trim();
            string email = txtEmail.Text.Trim();

            if (!ValidationHelper.IsNotEmpty(tenNCC))
            {
                ShowValidation(txtTenNCC, "Vui lòng nhập tên nhà cung cấp.");
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
                if (nhaCungCapDAL.Exists(maNCC))
                {
                    string suggestMa = AutoCodeHelper.GetNextMaNCC();
                    DialogResult dr = MessageBox.Show(
                        string.Format("Mã nhà cung cấp [{0}] đã tồn tại trong hệ thống.\nBạn có muốn tự động sử dụng mã mới [{1}] không?", maNCC, suggestMa),
                        "Trùng mã nhà cung cấp",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    if (dr == DialogResult.Yes)
                    {
                        maNCC = suggestMa;
                        txtMaNCC.Text = suggestMa;
                    }
                    else
                    {
                        txtMaNCC.Focus();
                        return;
                    }
                }

                NhaCungCap ncc = new NhaCungCap(maNCC, tenNCC, diaChi, sdt, email);
                if (nhaCungCapDAL.Insert(ncc))
                {
                    UiFeedbackHelper.ShowSuccess(this, "Đã thêm nhà cung cấp thành công.");
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
                UiErrorHandler.Show(this, "FRMNHACUNGCAP_UI_ERROR", "Lỗi khi thêm nhà cung cấp.", ex);
            }
        }

        private async void btnSua_Click(object sender, EventArgs e)
        {
            string maNCC = txtMaNCC.Text.Trim();
            string tenNCC = txtTenNCC.Text.Trim();
            string sdt = txtSoDienThoai.Text.Trim();
            string diaChi = txtDiaChi.Text.Trim();
            string email = txtEmail.Text.Trim();

            if (!ValidationHelper.IsNotEmpty(maNCC))
            {
                ShowValidation(txtMaNCC, "Vui lòng chọn nhà cung cấp cần sửa.");
                return;
            }

            if (!ValidationHelper.IsNotEmpty(tenNCC))
            {
                ShowValidation(txtTenNCC, "Vui lòng nhập tên nhà cung cấp.");
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
                if (!nhaCungCapDAL.Exists(maNCC))
                {
                    MessageBox.Show(string.Format("Nhà cung cấp [{0}] không tồn tại.", maNCC), "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                NhaCungCap ncc = new NhaCungCap(maNCC, tenNCC, diaChi, sdt, email, _currentVersion);
                ConcurrencyUpdateResult result = nhaCungCapDAL.UpdateWithResult(ncc);
                if (result == ConcurrencyUpdateResult.Success)
                {
                    _currentVersion = ncc.Version;
                    UiFeedbackHelper.ShowSuccess(this, "Đã cập nhật nhà cung cấp thành công.");
                    await _gridQuery.RefreshAsync(null);
                    SelectSupplierRow(maNCC);
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
                    SelectSupplierRow(maNCC);
                }
                else
                {
                    MessageBox.Show("Cập nhật thất bại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                UiErrorHandler.Show(this, "FRMNHACUNGCAP_UI_ERROR", "Lỗi khi cập nhật nhà cung cấp.", ex);
            }
        }

        private async void btnXoa_Click(object sender, EventArgs e)
        {
            string maNCC = txtMaNCC.Text.Trim();
            string tenNCC = txtTenNCC.Text.Trim();
            if (!ValidationHelper.IsNotEmpty(maNCC))
            {
                ShowValidation(txtMaNCC, "Vui lòng chọn nhà cung cấp cần xóa.");
                return;
            }

            try
            {
                int productCount = nhaCungCapDAL.GetSanPhamCount(maNCC);
                if (productCount > 0)
                {
                    MessageBox.Show(
                        string.Format("Không thể xóa nhà cung cấp [{0}] vì đang có {1} sản phẩm liên kết.", maNCC, productCount),
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                DialogResult confirm = MessageBox.Show(
                    string.Format("Bạn có chắc chắn muốn xóa nhà cung cấp [{0}] không?", maNCC),
                    "Xác nhận xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (confirm == DialogResult.Yes)
                {
                    if (nhaCungCapDAL.Delete(maNCC))
                    {
                        UiFeedbackHelper.ShowSuccess(this, string.Format("Đã xóa nhà cung cấp [{0}] thành công.", maNCC));
                        await _gridQuery.RefreshAsync(null);
                        ClearInputs();
                    }
                    else
                    {
                        MessageBox.Show("Không thể xóa nhà cung cấp này.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                UiErrorHandler.Show(this, "FRMNHACUNGCAP_UI_ERROR", "Lỗi khi xóa nhà cung cấp.", ex);
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


        private void SelectSupplierRow(string maNCC)
        {
            if (string.IsNullOrEmpty(maNCC) || dgvNhaCungCap.Rows.Count == 0)
                return;

            for (int i = 0; i < dgvNhaCungCap.Rows.Count; i++)
            {
                DataGridViewRow row = dgvNhaCungCap.Rows[i];
                if (row.Cells["colMaNCC"].Value != null &&
                    string.Equals(row.Cells["colMaNCC"].Value.ToString().Trim(), maNCC.Trim(), StringComparison.OrdinalIgnoreCase))
                {
                    dgvNhaCungCap.ClearSelection();
                    row.Selected = true;
                    if (dgvNhaCungCap.Columns.Contains("colTenNCC"))
                    {
                        dgvNhaCungCap.CurrentCell = row.Cells["colTenNCC"];
                    }
                    dgvNhaCungCap_CellClick(dgvNhaCungCap, new DataGridViewCellEventArgs(0, i));
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
