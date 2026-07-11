using System;
using System.Data;
using System.Windows.Forms;
using DNQH_KeToanBanHang.Constants;
using DNQH_KeToanBanHang.DataAccess;
using DNQH_KeToanBanHang.Helpers;
using DNQH_KeToanBanHang.Models;

namespace DNQH_KeToanBanHang.Forms
{
    public partial class frmKho : Form
    {
        private readonly KhoDAL khoDAL;
        private readonly Label lblPageStatus;
        private readonly ErrorProvider _validationErrors;
        private readonly UiGridQueryController _gridQuery;
        private int _currentVersion = 1;

        public frmKho()
        {
            InitializeComponent();
            _validationErrors = UiStyler.CreateErrorProvider(this);
            lblPageStatus = BuildFoundationLayout();
            ApplyFoundationDesign();
            khoDAL = new KhoDAL();
            _gridQuery = new UiGridQueryController(
                this, txtTimKiem, dgvKho, lblPageStatus,
                delegate(string keyword)
                {
                    return delegate { return string.IsNullOrEmpty(keyword) ? khoDAL.GetAll() : khoDAL.Search(keyword); };
                },
                delegate(int count) { return string.Format("Đang hiển thị {0:N0} kho.", count); },
                "Đang tải danh sách kho...",
                "Không tìm thấy kho phù hợp.",
                delegate(Exception ex) { UiErrorHandler.Show(this, "FRMKHO_UI_ERROR", "Lỗi khi tải danh sách kho.", ex); });
        }

        private Label BuildFoundationLayout()
        {
            TableLayoutPanel fields = UiLayoutBuilder.CreateStructuredGridLayout(
                2, 95,
                UiLayoutBuilder.Field(lblMaKho, txtMaKho, UiFieldSize.Code),
                UiLayoutBuilder.Field(lblTenKho, txtTenKho, UiFieldSize.Wide),
                UiLayoutBuilder.Field(lblDiaChi, txtDiaChi, UiFieldSize.Wide),
                UiLayoutBuilder.Field(lblTrangThai, cboTrangThai, UiFieldSize.Selection));

            Button btnXuatCsv = CsvExporter.CreateExportButton(dgvKho, "DanhSachKhoHang", "DANH SÁCH KHO HÀNG");
            return UiLayoutBuilder.BuildCrudPage(
                this, lblTitle, "Quản lý địa điểm lưu kho và trạng thái sử dụng",
                grpThongTin, fields,
                new Control[] { btnThem, btnSua, btnXoa, btnLamMoi, lblTimKiem, txtTimKiem, btnTimKiem, btnXuatCsv },
                dgvKho);
        }

        private void ApplyFoundationDesign()
        {
            dgvKho.AutoGenerateColumns = false;
            UiLayoutBuilder.ApplyCrudStyle(this, btnThem, btnSua, btnXoa, btnLamMoi, btnTimKiem, dgvKho, lblPageStatus);
            CsvExporter.AttachExportContextMenu(dgvKho, "DanhSachKhoHang", "DANH SÁCH KHO HÀNG");
            UiStyler.SetAccessibleText(txtMaKho, "Mã kho", "Mã định danh kho.");
            UiStyler.SetAccessibleText(txtTenKho, "Tên kho", "Tên kho bắt buộc.");
            UiStyler.SetAccessibleText(cboTrangThai, "Trạng thái kho", "Chọn trạng thái sử dụng của kho.");
            UiStyler.SetAccessibleText(dgvKho, "Danh sách kho", "Chọn một dòng để sửa hoặc xóa.");
            ConfigureGridColumns();
            dgvKho.CellFormatting += dgvKho_CellFormatting;
        }

        private void ConfigureGridColumns()
        {
            dgvKho.AutoGenerateColumns = false;
            dgvKho.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            colMaKho.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colMaKho.Width = 95;
            colMaKho.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            colTenKho.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colTenKho.Width = 180;
            colTenKho.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            colDiaChi.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colDiaChi.FillWeight = 200F;
            colDiaChi.MinimumWidth = 200;
            colDiaChi.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            colTrangThai.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colTrangThai.Width = 120;
            colTrangThai.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        private void dgvKho_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            string colName = dgvKho.Columns[e.ColumnIndex].Name;
            if (colName == "colTrangThai" && e.Value != null)
            {
                string status = e.Value.ToString().Trim();
                if (EntityStatusConstants.Warehouse.IsActive(status))
                {
                    e.CellStyle.ForeColor = System.Drawing.Color.FromArgb(16, 120, 60);
                    e.CellStyle.Font = new System.Drawing.Font(dgvKho.Font, System.Drawing.FontStyle.Bold);
                }
                else if (EntityStatusConstants.Warehouse.IsInactive(status))
                {
                    e.CellStyle.ForeColor = System.Drawing.Color.FromArgb(180, 20, 20);
                    e.CellStyle.Font = new System.Drawing.Font(dgvKho.Font, System.Drawing.FontStyle.Bold);
                }
            }
        }

        private async void frmKho_Load(object sender, EventArgs e)
        {
            this.ApplyResponsiveUI();
            if (!SessionManager.IsAdmin() && !SessionManager.IsWarehouse())
            {
                MessageBox.Show("Bạn không có quyền truy cập vào chức năng Quản lý Kho.\nChỉ Quản trị viên và Nhân viên kho mới có quyền này.",
                    "Từ chối truy cập", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
                return;
            }

            UiComboBoxHelper.PopulateItems(cboTrangThai, EntityStatusConstants.Warehouse.All, EntityStatusConstants.Warehouse.Active);
            ClearInputs();
            await _gridQuery.RefreshAsync(null);
        }

        private void ClearInputs()
        {
            _currentVersion = 1;
            txtMaKho.Text = AutoCodeHelper.GetNextMaKho();
            txtTenKho.Clear();
            txtDiaChi.Clear();
            if (cboTrangThai.Items.Count > 0)
            {
                cboTrangThai.SelectedIndex = 0;
            }
            txtMaKho.ReadOnly = false;
            txtTenKho.Focus();
        }

        private void dgvKho_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvKho.Rows.Count)
            {
                DataGridViewRow row = dgvKho.Rows[e.RowIndex];
                string maKho = row.Cells["colMaKho"].Value != null ? row.Cells["colMaKho"].Value.ToString().Trim() : string.Empty;

                try
                {
                    Kho k = khoDAL.GetById(maKho);
                    if (k != null)
                    {
                        _currentVersion = k.Version;
                        txtMaKho.Text = k.MaKho;
                        txtTenKho.Text = k.TenKho;
                        txtDiaChi.Text = k.DiaChi;

                        string trangThai = k.TrangThai;
                        UiComboBoxHelper.SafeSelect(cboTrangThai, trangThai);
                    }
                    else
                    {
                        txtMaKho.Text = maKho;
                        txtTenKho.Text = row.Cells["colTenKho"].Value != null ? row.Cells["colTenKho"].Value.ToString().Trim() : string.Empty;
                        txtDiaChi.Text = row.Cells["colDiaChi"].Value != null ? row.Cells["colDiaChi"].Value.ToString().Trim() : string.Empty;

                        string trangThai = row.Cells["colTrangThai"].Value != null ? row.Cells["colTrangThai"].Value.ToString().Trim() : string.Empty;
                        UiComboBoxHelper.SafeSelect(cboTrangThai, trangThai);
                    }
                    txtMaKho.ReadOnly = true;
                }
                catch (Exception ex)
                {
                    UiErrorHandler.Show(this, "FRMKHO_UI_ERROR", "Lỗi khi đọc chi tiết kho.", ex);
                }
            }
        }

        private async void btnThem_Click(object sender, EventArgs e)
        {
            string maKho = txtMaKho.Text.Trim();
            if (string.IsNullOrEmpty(maKho))
            {
                maKho = AutoCodeHelper.GetNextMaKho();
                txtMaKho.Text = maKho;
            }

            string tenKho = txtTenKho.Text.Trim();
            string diaChi = txtDiaChi.Text.Trim();
            string trangThai = cboTrangThai.SelectedItem != null ? cboTrangThai.SelectedItem.ToString() : EntityStatusConstants.Warehouse.Active;

            if (!ValidationHelper.IsNotEmpty(tenKho))
            {
                ShowValidation(txtTenKho, "Vui lòng nhập tên kho.");
                return;
            }

            try
            {
                if (khoDAL.Exists(maKho))
                {
                    string suggestMa = AutoCodeHelper.GetNextMaKho();
                    DialogResult dr = MessageBox.Show(
                        string.Format("Mã kho [{0}] đã tồn tại trong hệ thống.\nBạn có muốn tự động sử dụng mã mới [{1}] không?", maKho, suggestMa),
                        "Trùng mã kho",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    if (dr == DialogResult.Yes)
                    {
                        maKho = suggestMa;
                        txtMaKho.Text = suggestMa;
                    }
                    else
                    {
                        txtMaKho.Focus();
                        return;
                    }
                }

                Kho k = new Kho(maKho, tenKho, diaChi, trangThai);
                if (khoDAL.Insert(k))
                {
                    UiFeedbackHelper.ShowSuccess(this, "Đã thêm kho thành công.");
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
                UiErrorHandler.Show(this, "FRMKHO_UI_ERROR", "Lỗi khi thêm kho.", ex);
            }
        }

        private async void btnSua_Click(object sender, EventArgs e)
        {
            string maKho = txtMaKho.Text.Trim();
            string tenKho = txtTenKho.Text.Trim();
            string diaChi = txtDiaChi.Text.Trim();
            string trangThai = cboTrangThai.SelectedItem != null ? cboTrangThai.SelectedItem.ToString() : EntityStatusConstants.Warehouse.Active;

            if (!ValidationHelper.IsNotEmpty(maKho))
            {
                ShowValidation(txtMaKho, "Vui lòng chọn kho cần sửa từ danh sách.");
                return;
            }

            if (!ValidationHelper.IsNotEmpty(tenKho))
            {
                ShowValidation(txtTenKho, "Vui lòng nhập tên kho.");
                return;
            }

            try
            {
                if (!khoDAL.Exists(maKho))
                {
                    MessageBox.Show(string.Format("Kho [{0}] không tồn tại.", maKho), "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                Kho k = new Kho(maKho, tenKho, diaChi, trangThai, _currentVersion);
                ConcurrencyUpdateResult result = khoDAL.UpdateWithResult(k);
                if (result == ConcurrencyUpdateResult.Success)
                {
                    _currentVersion = k.Version;
                    UiFeedbackHelper.ShowSuccess(this, "Đã cập nhật kho thành công.");
                    await _gridQuery.RefreshAsync(null);
                    SelectWarehouseRow(maKho);
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
                    SelectWarehouseRow(maKho);
                }
                else
                {
                    MessageBox.Show("Cập nhật thất bại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                UiErrorHandler.Show(this, "FRMKHO_UI_ERROR", "Lỗi khi cập nhật kho.", ex);
            }
        }

        private async void btnXoa_Click(object sender, EventArgs e)
        {
            string maKho = txtMaKho.Text.Trim();
            if (!ValidationHelper.IsNotEmpty(maKho))
            {
                ShowValidation(txtMaKho, "Vui lòng chọn kho cần xóa từ danh sách.");
                return;
            }

            DialogResult confirm = MessageBox.Show(
                string.Format("Bạn có chắc chắn muốn xóa kho [{0}] không?", maKho),
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                try
                {
                    if (khoDAL.Delete(maKho))
                    {
                        UiFeedbackHelper.ShowSuccess(this, "Đã xóa kho thành công.");
                        await _gridQuery.RefreshAsync(null);
                        ClearInputs();
                    }
                    else
                    {
                        MessageBox.Show("Không thể xóa kho này vì có dữ liệu tồn kho hoặc phiếu xuất liên quan.", "Cảnh báo ràng buộc", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    UiErrorHandler.Show(this, "FRMKHO_UI_ERROR", "Lỗi khi xóa kho.", ex);
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


        private void SelectWarehouseRow(string maKho)
        {
            if (string.IsNullOrEmpty(maKho) || dgvKho.Rows.Count == 0)
                return;

            for (int i = 0; i < dgvKho.Rows.Count; i++)
            {
                DataGridViewRow row = dgvKho.Rows[i];
                if (row.Cells["colMaKho"].Value != null &&
                    string.Equals(row.Cells["colMaKho"].Value.ToString().Trim(), maKho.Trim(), StringComparison.OrdinalIgnoreCase))
                {
                    dgvKho.ClearSelection();
                    row.Selected = true;
                    if (dgvKho.Columns.Contains("colTenKho"))
                    {
                        dgvKho.CurrentCell = row.Cells["colTenKho"];
                    }
                    dgvKho_CellClick(dgvKho, new DataGridViewCellEventArgs(0, i));
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
