using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;
using DNQH_KeToanBanHang.Constants;
using DNQH_KeToanBanHang.DataAccess;
using DNQH_KeToanBanHang.Helpers;
using DNQH_KeToanBanHang.Models;

namespace DNQH_KeToanBanHang.Forms
{
    public partial class frmSanPham : Form
    {
        private readonly SanPhamDAL sanPhamDAL;
        private readonly LoaiSanPhamDAL loaiSanPhamDAL;
        private readonly NhaCungCapDAL nhaCungCapDAL;
        private readonly Label lblPageStatus;
        private readonly ErrorProvider _validationErrors;
        private readonly UiGridQueryController _gridQuery;
        private bool _isBindingLookups;
        private int _currentVersion = 1;

        public frmSanPham()
        {
            InitializeComponent();
            _validationErrors = UiStyler.CreateErrorProvider(this);
            lblPageStatus = BuildFoundationLayout();
            ApplyFoundationDesign();
            sanPhamDAL = new SanPhamDAL();
            loaiSanPhamDAL = new LoaiSanPhamDAL();
            nhaCungCapDAL = new NhaCungCapDAL();
            _gridQuery = new UiGridQueryController(
                this, txtTimKiem, dgvSanPham, lblPageStatus,
                delegate(string keyword)
                {
                    string selectedMaLoai = cboLocLoai.SelectedValue != null ? cboLocLoai.SelectedValue.ToString().Trim() : null;
                    if (string.IsNullOrEmpty(selectedMaLoai)) selectedMaLoai = null;
                    return delegate { return sanPhamDAL.Search(keyword, selectedMaLoai); };
                },
                delegate(int count) { return string.Format("Đang hiển thị {0:N0} sản phẩm.", count); },
                "Đang tải danh sách sản phẩm...",
                "Không tìm thấy sản phẩm phù hợp.",
                delegate(Exception ex) { UiErrorHandler.Show(this, "FRMSANPHAM_UI_ERROR", "Lỗi khi tải danh sách sản phẩm.", ex); });
        }

        private Label BuildFoundationLayout()
        {
            TableLayoutPanel fields = UiLayoutBuilder.CreateStructuredGridLayout(
                3, 95,
                UiLayoutBuilder.Field(lblMaSP, txtMaSP, UiFieldSize.Code),
                UiLayoutBuilder.Field(lblTenSP, txtTenSP, UiFieldSize.Wide),
                UiLayoutBuilder.Field(lblLoaiSP, cboLoaiSP, UiFieldSize.Medium),
                UiLayoutBuilder.Field(lblNhaCungCap, cboNhaCungCap, UiFieldSize.Wide),
                UiLayoutBuilder.Field(lblDonViTinh, txtDonViTinh, UiFieldSize.ShortText),
                UiLayoutBuilder.Field(lblDonGiaBan, txtDonGiaBan, UiFieldSize.Money),
                UiLayoutBuilder.Field(lblTrangThai, cboTrangThai, UiFieldSize.Selection));

            Label searchLabel = new Label { AutoSize = true, Text = "Tìm kiếm:" };
            Button btnXuatCsv = CsvExporter.CreateExportButton(dgvSanPham, "DanhSachSanPham", "DANH SÁCH SẢN PHẨM");
            Button btnNhapExcel = ExcelImporter.CreateImportButton(this, ImportEntityType.SanPham, () => _gridQuery.RefreshAsync(null));
            return UiLayoutBuilder.BuildCrudPage(
                this, lblTitle, "Cập nhật thông tin bán hàng và phân loại sản phẩm",
                grpThongTin, fields,
                new Control[] { btnThem, btnSua, btnXoa, btnLamMoi, lblLocLoai, cboLocLoai, searchLabel, txtTimKiem, btnTimKiem, btnXuatCsv, btnNhapExcel },
                dgvSanPham);
        }

        private void ApplyFoundationDesign()
        {
            dgvSanPham.AutoGenerateColumns = false;
            UiLayoutBuilder.ApplyCrudStyle(this, btnThem, btnSua, btnXoa, btnLamMoi, btnTimKiem, dgvSanPham, lblPageStatus);
            CsvExporter.AttachExportContextMenu(dgvSanPham, "DanhSachSanPham", "DANH SÁCH SẢN PHẨM");
            ExcelImporter.AttachImportContextMenu(this, dgvSanPham, ImportEntityType.SanPham, () => _gridQuery.RefreshAsync(null));
            UiStyler.SetAccessibleText(txtMaSP, "Mã sản phẩm", "Mã định danh sản phẩm.");
            UiStyler.SetAccessibleText(txtTenSP, "Tên sản phẩm", "Tên sản phẩm bắt buộc.");
            UiStyler.SetAccessibleText(cboLoaiSP, "Loại sản phẩm", "Chọn loại sản phẩm.");
            UiStyler.SetAccessibleText(cboNhaCungCap, "Nhà cung cấp", "Chọn nhà cung cấp sản phẩm.");
            UiStyler.SetAccessibleText(txtDonGiaBan, "Đơn giá bán", "Nhập đơn giá bán bằng đồng Việt Nam.");
            UiStyler.SetAccessibleText(cboLocLoai, "Lọc theo loại", "Lọc danh sách theo loại sản phẩm.");
            UiStyler.SetAccessibleText(dgvSanPham, "Danh sách sản phẩm", "Chọn một dòng để sửa hoặc xóa.");
            ConfigureGridColumns();
            dgvSanPham.CellFormatting += dgvSanPham_CellFormatting;
        }

        private void ConfigureGridColumns()
        {
            dgvSanPham.AutoGenerateColumns = false;
            dgvSanPham.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            colMaSP.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colMaSP.Width = 95;
            colMaSP.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            colTenSP.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colTenSP.FillWeight = 180F;
            colTenSP.MinimumWidth = 180;
            colTenSP.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            colTenLoai.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colTenLoai.Width = 140;
            colTenLoai.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            colTenNCC.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colTenNCC.Width = 160;
            colTenNCC.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            colDonViTinh.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colDonViTinh.Width = 65;
            colDonViTinh.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            colDonGiaBan.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colDonGiaBan.Width = 125;
            colDonGiaBan.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colDonGiaBan.DefaultCellStyle.Format = "N0";
            colDonGiaBan.DefaultCellStyle.Font = new System.Drawing.Font(dgvSanPham.Font, System.Drawing.FontStyle.Bold);

            colTrangThai.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colTrangThai.Width = 120;
            colTrangThai.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        private void dgvSanPham_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            string colName = dgvSanPham.Columns[e.ColumnIndex].Name;
            if (colName == "colTrangThai" && e.Value != null)
            {
                string status = e.Value.ToString().Trim();
                if (EntityStatusConstants.Product.IsActive(status))
                {
                    e.CellStyle.ForeColor = System.Drawing.Color.FromArgb(16, 120, 60);
                    e.CellStyle.Font = new System.Drawing.Font(dgvSanPham.Font, System.Drawing.FontStyle.Bold);
                }
                else if (EntityStatusConstants.Product.IsDiscontinued(status))
                {
                    e.CellStyle.ForeColor = System.Drawing.Color.FromArgb(180, 20, 20);
                    e.CellStyle.Font = new System.Drawing.Font(dgvSanPham.Font, System.Drawing.FontStyle.Bold);
                }
            }
        }

        private async void frmSanPham_Load(object sender, EventArgs e)
        {
            this.ApplyResponsiveUI();
            if (!SessionManager.IsAdmin() && !SessionManager.IsSales())
            {
                MessageBox.Show("Bạn không có quyền truy cập vào chức năng Quản lý Sản phẩm.\nChỉ Quản trị viên và Nhân viên bán hàng mới có quyền này.",
                    "Từ chối truy cập", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
                return;
            }

            await LoadComboboxesAsync();
            UiComboBoxHelper.PopulateItems(cboTrangThai, EntityStatusConstants.Product.All, EntityStatusConstants.Product.Active);
            ClearInputs();
            await _gridQuery.RefreshAsync(null);
        }

        private async Task LoadComboboxesAsync()
        {
            try
            {
                DataTable dtLoai = null;
                DataTable dtNCC = null;
                UiStyler.StyleStatusLabel(lblPageStatus, UiStatusKind.Information, "Đang tải loại sản phẩm và nhà cung cấp...");
                await UiFeedbackHelper.RunBusyAsync(
                    this,
                    null,
                    null,
                    delegate
                    {
                        dtLoai = loaiSanPhamDAL.GetAll();
                        dtNCC = nhaCungCapDAL.GetAll();
                        return true;
                    });
                if (IsDisposed)
                {
                    return;
                }

                _isBindingLookups = true;
                cboLoaiSP.DataSource = dtLoai;
                cboLoaiSP.DisplayMember = "TenLoai";
                cboLoaiSP.ValueMember = "MaLoai";

                // Load Loai for Filter
                DataTable dtLocLoai = dtLoai.Copy();
                DataRow emptyRow = dtLocLoai.NewRow();
                emptyRow["MaLoai"] = "";
                emptyRow["TenLoai"] = "-- Tất cả loại --";
                dtLocLoai.Rows.InsertAt(emptyRow, 0);
                cboLocLoai.DataSource = dtLocLoai;
                cboLocLoai.DisplayMember = "TenLoai";
                cboLocLoai.ValueMember = "MaLoai";
                cboLocLoai.SelectedIndex = 0;

                cboNhaCungCap.DataSource = dtNCC;
                cboNhaCungCap.DisplayMember = "TenNCC";
                cboNhaCungCap.ValueMember = "MaNCC";
                _isBindingLookups = false;
            }
            catch (Exception ex)
            {
                _isBindingLookups = false;
                UiErrorHandler.Show(this, "FRMSANPHAM_UI_ERROR", "Lỗi khi tải danh mục loại SP / nhà cung cấp.", ex);
            }
        }

        private void ClearInputs()
        {
            _currentVersion = 1;
            txtMaSP.Text = AutoCodeHelper.GetNextMaSP();
            txtTenSP.Clear();
            txtDonViTinh.Clear();
            txtDonGiaBan.Clear();

            if (cboLoaiSP.Items.Count > 0)
                cboLoaiSP.SelectedIndex = 0;

            if (cboNhaCungCap.Items.Count > 0)
                cboNhaCungCap.SelectedIndex = 0;

            if (cboTrangThai.Items.Count > 0)
                cboTrangThai.SelectedIndex = 0;

            txtMaSP.ReadOnly = false;
            txtTenSP.Focus();
        }

        private void dgvSanPham_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvSanPham.Rows.Count)
            {
                DataGridViewRow row = dgvSanPham.Rows[e.RowIndex];
                string maSP = row.Cells["colMaSP"].Value != null ? row.Cells["colMaSP"].Value.ToString().Trim() : string.Empty;

                try
                {
                    SanPham sp = sanPhamDAL.GetById(maSP);
                    if (sp != null)
                    {
                        _currentVersion = sp.Version;
                        txtMaSP.Text = sp.MaSP;
                        txtTenSP.Text = sp.TenSP;
                        txtDonViTinh.Text = sp.DonViTinh;
                        txtDonGiaBan.Text = sp.DonGiaBan.ToString("0.##");

                        if (!string.IsNullOrEmpty(sp.MaLoai))
                            cboLoaiSP.SelectedValue = sp.MaLoai;
                        else
                            cboLoaiSP.SelectedIndex = -1;

                        if (!string.IsNullOrEmpty(sp.MaNCC))
                            cboNhaCungCap.SelectedValue = sp.MaNCC;
                        else
                            cboNhaCungCap.SelectedIndex = -1;

                        UiComboBoxHelper.SafeSelect(cboTrangThai, EntityStatusConstants.Product.Normalize(sp.TrangThai));

                        txtMaSP.ReadOnly = true;
                    }
                }
                catch (Exception ex)
                {
                    UiErrorHandler.Show(this, "FRMSANPHAM_UI_ERROR", "Lỗi khi đọc chi tiết sản phẩm.", ex);
                }
            }
        }

        private async void btnThem_Click(object sender, EventArgs e)
        {
            string maSP = txtMaSP.Text.Trim();
            if (string.IsNullOrEmpty(maSP))
            {
                maSP = AutoCodeHelper.GetNextMaSP();
                txtMaSP.Text = maSP;
            }

            string tenSP = txtTenSP.Text.Trim();
            string donViTinh = txtDonViTinh.Text.Trim();
            string donGiaStr = txtDonGiaBan.Text.Trim();
            string trangThai = cboTrangThai.SelectedItem != null ? cboTrangThai.SelectedItem.ToString() : EntityStatusConstants.Product.Active;

            if (!ValidationHelper.IsNotEmpty(tenSP))
            {
                ShowValidation(txtTenSP, "Vui lòng nhập tên sản phẩm.");
                return;
            }

            if (cboLoaiSP.SelectedValue == null)
            {
                ShowValidation(cboLoaiSP, "Vui lòng chọn loại sản phẩm.");
                return;
            }

            if (cboNhaCungCap.SelectedValue == null)
            {
                ShowValidation(cboNhaCungCap, "Vui lòng chọn nhà cung cấp.");
                return;
            }

            decimal donGiaBan;
            if (!ValidationHelper.IsValidDecimal(donGiaStr, out donGiaBan) || donGiaBan < 0)
            {
                ShowValidation(txtDonGiaBan, "Đơn giá bán không hợp lệ (phải là số không âm).");
                return;
            }

            string maLoai = cboLoaiSP.SelectedValue.ToString().Trim();
            string maNCC = cboNhaCungCap.SelectedValue.ToString().Trim();

            try
            {
                if (sanPhamDAL.Exists(maSP))
                {
                    string suggestMa = AutoCodeHelper.GetNextMaSP();
                    DialogResult dr = MessageBox.Show(
                        string.Format("Mã sản phẩm [{0}] đã tồn tại trong hệ thống.\nBạn có muốn tự động sử dụng mã mới [{1}] không?", maSP, suggestMa),
                        "Trùng mã sản phẩm",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    if (dr == DialogResult.Yes)
                    {
                        maSP = suggestMa;
                        txtMaSP.Text = suggestMa;
                    }
                    else
                    {
                        txtMaSP.Focus();
                        return;
                    }
                }

                SanPham sp = new SanPham(maSP, maNCC, maLoai, tenSP, donViTinh, donGiaBan, trangThai);
                if (sanPhamDAL.Insert(sp))
                {
                    UiFeedbackHelper.ShowSuccess(this, "Đã thêm sản phẩm thành công.");
                    await _gridQuery.RefreshAsync(null);
                    ClearInputs();
                }
                else
                {
                    MessageBox.Show("Thêm sản phẩm thất bại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                UiErrorHandler.Show(this, "FRMSANPHAM_UI_ERROR", "Lỗi khi thêm sản phẩm.", ex);
            }
        }

        private async void btnSua_Click(object sender, EventArgs e)
        {
            string maSP = txtMaSP.Text.Trim();
            string tenSP = txtTenSP.Text.Trim();
            string donViTinh = txtDonViTinh.Text.Trim();
            string donGiaStr = txtDonGiaBan.Text.Trim();
            string trangThai = cboTrangThai.SelectedItem != null ? cboTrangThai.SelectedItem.ToString() : EntityStatusConstants.Product.Active;

            if (!ValidationHelper.IsNotEmpty(maSP))
            {
                ShowValidation(txtMaSP, "Vui lòng chọn sản phẩm cần sửa từ danh sách.");
                return;
            }

            if (!ValidationHelper.IsNotEmpty(tenSP))
            {
                ShowValidation(txtTenSP, "Vui lòng nhập tên sản phẩm.");
                return;
            }

            if (cboLoaiSP.SelectedValue == null)
            {
                ShowValidation(cboLoaiSP, "Vui lòng chọn loại sản phẩm.");
                return;
            }

            if (cboNhaCungCap.SelectedValue == null)
            {
                ShowValidation(cboNhaCungCap, "Vui lòng chọn nhà cung cấp.");
                return;
            }

            decimal donGiaBan;
            if (!ValidationHelper.IsValidDecimal(donGiaStr, out donGiaBan) || donGiaBan < 0)
            {
                ShowValidation(txtDonGiaBan, "Đơn giá bán không hợp lệ (phải là số không âm).");
                return;
            }

            string maLoai = cboLoaiSP.SelectedValue.ToString().Trim();
            string maNCC = cboNhaCungCap.SelectedValue.ToString().Trim();

            try
            {
                if (!sanPhamDAL.Exists(maSP))
                {
                    MessageBox.Show(string.Format("Sản phẩm [{0}] không tồn tại.", maSP), "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                SanPham sp = new SanPham(maSP, maNCC, maLoai, tenSP, donViTinh, donGiaBan, trangThai, _currentVersion);
                ConcurrencyUpdateResult result = sanPhamDAL.UpdateWithResult(sp);

                if (result == ConcurrencyUpdateResult.Success)
                {
                    _currentVersion = sp.Version;
                    UiFeedbackHelper.ShowSuccess(this, "Đã cập nhật sản phẩm thành công.");
                    await _gridQuery.RefreshAsync(null);
                    SelectProductRow(maSP);
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
                    SelectProductRow(maSP);
                }
                else
                {
                    MessageBox.Show("Cập nhật sản phẩm thất bại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                UiErrorHandler.Show(this, "FRMSANPHAM_UI_ERROR", "Lỗi khi cập nhật sản phẩm.", ex);
            }
        }

        private async void btnXoa_Click(object sender, EventArgs e)
        {
            string maSP = txtMaSP.Text.Trim();
            if (!ValidationHelper.IsNotEmpty(maSP))
            {
                ShowValidation(txtMaSP, "Vui lòng chọn sản phẩm cần xóa.");
                return;
            }

            try
            {
                SanPhamUsageStats stats = sanPhamDAL.GetUsageStatistics(maSP);
                if (stats.HasAnyReference)
                {
                    List<string> references = new List<string>();
                    if (stats.OrderCount > 0)
                        references.Add(string.Format("{0} đơn hàng", stats.OrderCount));
                    if (stats.InvoiceCount > 0)
                        references.Add(string.Format("{0} hóa đơn", stats.InvoiceCount));
                    if (stats.IssueCount > 0)
                        references.Add(string.Format("{0} phiếu xuất", stats.IssueCount));
                    if (stats.TotalStockQty > 0)
                        references.Add(string.Format("{0} tồn kho", stats.TotalStockQty));
                    else if (stats.StockRowCount > 0)
                        references.Add("dữ liệu kho");

                    string refSummary = string.Join(", ", references.ToArray());

                    string message = string.Format(
                        "Sản phẩm [{0}] đang có dữ liệu ({1}) nên không thể xóa.\n\n" +
                        "Bạn có muốn chuyển trạng thái sang 'Ngừng kinh doanh' không?",
                        maSP, refSummary);

                    DialogResult dr = MessageBox.Show(message, "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dr == DialogResult.Yes)
                    {
                        if (sanPhamDAL.Deactivate(maSP))
                        {
                            UiFeedbackHelper.ShowSuccess(this, string.Format("Đã chuyển [{0}] sang 'Ngừng kinh doanh'.", maSP));
                            await _gridQuery.RefreshAsync(null);
                            SelectProductRow(maSP);
                        }
                        else
                        {
                            MessageBox.Show("Chuyển trạng thái thất bại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    return;
                }

                DialogResult confirm = MessageBox.Show(
                    string.Format("Bạn có chắc chắn muốn xóa sản phẩm [{0}] không?", maSP),
                    "Xác nhận xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (confirm == DialogResult.Yes)
                {
                    if (sanPhamDAL.Delete(maSP))
                    {
                        UiFeedbackHelper.ShowSuccess(this, string.Format("Đã xóa sản phẩm [{0}] thành công.", maSP));
                        await _gridQuery.RefreshAsync(null);
                        ClearInputs();
                    }
                    else
                    {
                        MessageBox.Show("Không thể xóa sản phẩm này.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                UiErrorHandler.Show(this, "FRMSANPHAM_UI_ERROR", "Lỗi khi xóa sản phẩm.", ex);
            }
        }

        private async void btnLamMoi_Click(object sender, EventArgs e)
        {
            ClearInputs();
            await _gridQuery.RefreshAsync(btnLamMoi);
        }

        private async void cboLocLoai_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_isBindingLookups)
            {
                await _gridQuery.RefreshAsync(null);
            }
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

        private void SelectProductRow(string maSP)
        {
            if (string.IsNullOrEmpty(maSP) || dgvSanPham.Rows.Count == 0)
                return;

            for (int i = 0; i < dgvSanPham.Rows.Count; i++)
            {
                DataGridViewRow row = dgvSanPham.Rows[i];
                if (row.Cells["colMaSP"].Value != null &&
                    string.Equals(row.Cells["colMaSP"].Value.ToString().Trim(), maSP.Trim(), StringComparison.OrdinalIgnoreCase))
                {
                    dgvSanPham.ClearSelection();
                    row.Selected = true;
                    if (dgvSanPham.Columns.Contains("colTenSP"))
                    {
                        dgvSanPham.CurrentCell = row.Cells["colTenSP"];
                    }
                    dgvSanPham_CellClick(dgvSanPham, new DataGridViewCellEventArgs(0, i));
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
