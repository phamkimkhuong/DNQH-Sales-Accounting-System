using System;
using System.Data;
using System.Windows.Forms;
using DNQH_KeToanBanHang.DataAccess;
using DNQH_KeToanBanHang.Helpers;
using DNQH_KeToanBanHang.Models;

namespace DNQH_KeToanBanHang.Forms
{
    public partial class frmLoaiSanPham : Form
    {
        private readonly LoaiSanPhamDAL loaiSanPhamDAL;
        private readonly Label lblPageStatus;
        private readonly ErrorProvider _validationErrors;
        private readonly UiGridQueryController _gridQuery;
        private int _currentVersion = 1;

        public frmLoaiSanPham()
        {
            InitializeComponent();
            _validationErrors = UiStyler.CreateErrorProvider(this);
            lblPageStatus = BuildFoundationLayout();
            ApplyFoundationDesign();
            loaiSanPhamDAL = new LoaiSanPhamDAL();
            _gridQuery = new UiGridQueryController(
                this, txtTimKiem, dgvLoaiSanPham, lblPageStatus,
                delegate(string keyword)
                {
                    return delegate { return string.IsNullOrEmpty(keyword) ? loaiSanPhamDAL.GetAll() : loaiSanPhamDAL.Search(keyword); };
                },
                delegate(int count) { return string.Format("Đang hiển thị {0:N0} loại sản phẩm.", count); },
                "Đang tải danh sách loại sản phẩm...",
                "Không tìm thấy loại sản phẩm phù hợp.",
                delegate(Exception ex) { UiErrorHandler.Show(this, "FRMLOAISANPHAM_UI_ERROR", "Lỗi khi tải danh sách loại sản phẩm.", ex); });
        }

        private Label BuildFoundationLayout()
        {
            TableLayoutPanel fields = UiLayoutBuilder.CreateStructuredGridLayout(
                2, 95,
                UiLayoutBuilder.Field(lblMaLoai, txtMaLoai, UiFieldSize.Code),
                UiLayoutBuilder.Field(lblTenLoai, txtTenLoai, UiFieldSize.Wide),
                UiLayoutBuilder.FullWidthField(lblMoTa, txtMoTa));

            Button btnXuatCsv = CsvExporter.CreateExportButton(dgvLoaiSanPham, "DanhSachLoaiSanPham", "DANH SÁCH LOẠI SẢN PHẨM");
            return UiLayoutBuilder.BuildCrudPage(
                this, lblTitle, "Sắp xếp sản phẩm theo nhóm để tra cứu và báo cáo",
                grpThongTin, fields,
                new Control[] { btnThem, btnSua, btnXoa, btnLamMoi, lblTimKiem, txtTimKiem, btnTimKiem, btnXuatCsv },
                dgvLoaiSanPham);
        }

        private void ApplyFoundationDesign()
        {
            dgvLoaiSanPham.AutoGenerateColumns = false;
            UiLayoutBuilder.ApplyCrudStyle(this, btnThem, btnSua, btnXoa, btnLamMoi, btnTimKiem, dgvLoaiSanPham, lblPageStatus);
            CsvExporter.AttachExportContextMenu(dgvLoaiSanPham, "DanhSachLoaiSanPham", "DANH SÁCH LOẠI SẢN PHẨM");
            UiStyler.SetAccessibleText(txtMaLoai, "Mã loại sản phẩm", "Mã định danh loại sản phẩm.");
            UiStyler.SetAccessibleText(txtTenLoai, "Tên loại sản phẩm", "Tên loại sản phẩm bắt buộc.");
            UiStyler.SetAccessibleText(txtMoTa, "Mô tả loại sản phẩm", "Mô tả ngắn cho loại sản phẩm.");
            UiStyler.SetAccessibleText(dgvLoaiSanPham, "Danh sách loại sản phẩm", "Chọn một dòng để sửa hoặc xóa.");
            ConfigureGridColumns();
        }

        private void ConfigureGridColumns()
        {
            dgvLoaiSanPham.AutoGenerateColumns = false;
            dgvLoaiSanPham.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            colMaLoai.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colMaLoai.Width = 95;
            colMaLoai.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            colTenLoai.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colTenLoai.Width = 200;
            colTenLoai.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            colMoTa.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colMoTa.FillWeight = 200F;
            colMoTa.MinimumWidth = 200;
            colMoTa.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
        }

        private async void frmLoaiSanPham_Load(object sender, EventArgs e)
        {
            this.ApplyResponsiveUI();
            if (!SessionManager.IsAdmin() && !SessionManager.IsSales())
            {
                MessageBox.Show("Bạn không có quyền truy cập vào chức năng Quản lý Loại sản phẩm.\nChỉ Quản trị viên và Nhân viên bán hàng mới có quyền này.",
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
            txtMaLoai.Text = AutoCodeHelper.GetNextMaLoai();
            txtTenLoai.Clear();
            txtMoTa.Clear();
            txtMaLoai.ReadOnly = false;
            txtTenLoai.Focus();
        }

        private void dgvLoaiSanPham_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvLoaiSanPham.Rows.Count)
            {
                DataGridViewRow row = dgvLoaiSanPham.Rows[e.RowIndex];
                string maLoai = row.Cells["colMaLoai"].Value != null ? row.Cells["colMaLoai"].Value.ToString().Trim() : string.Empty;

                try
                {
                    LoaiSanPham loai = loaiSanPhamDAL.GetById(maLoai);
                    if (loai != null)
                    {
                        _currentVersion = loai.Version;
                        txtMaLoai.Text = loai.MaLoai;
                        txtTenLoai.Text = loai.TenLoai;
                        txtMoTa.Text = loai.MoTa;
                    }
                    else
                    {
                        txtMaLoai.Text = maLoai;
                        txtTenLoai.Text = row.Cells["colTenLoai"].Value != null ? row.Cells["colTenLoai"].Value.ToString().Trim() : string.Empty;
                        txtMoTa.Text = row.Cells["colMoTa"].Value != null ? row.Cells["colMoTa"].Value.ToString().Trim() : string.Empty;
                    }
                    txtMaLoai.ReadOnly = true;
                }
                catch (Exception ex)
                {
                    UiErrorHandler.Show(this, "FRMLOAISANPHAM_UI_ERROR", "Lỗi khi đọc chi tiết loại sản phẩm.", ex);
                }
            }
        }

        private async void btnThem_Click(object sender, EventArgs e)
        {
            string maLoai = txtMaLoai.Text.Trim();
            if (string.IsNullOrEmpty(maLoai))
            {
                maLoai = AutoCodeHelper.GetNextMaLoai();
                txtMaLoai.Text = maLoai;
            }

            string tenLoai = txtTenLoai.Text.Trim();
            string moTa = txtMoTa.Text.Trim();

            if (!ValidationHelper.IsNotEmpty(tenLoai))
            {
                ShowValidation(txtTenLoai, "Vui lòng nhập tên loại sản phẩm.");
                return;
            }

            try
            {
                if (loaiSanPhamDAL.Exists(maLoai))
                {
                    string suggestMa = AutoCodeHelper.GetNextMaLoai();
                    DialogResult dr = MessageBox.Show(
                        string.Format("Mã loại sản phẩm [{0}] đã tồn tại trong hệ thống.\nBạn có muốn tự động sử dụng mã mới [{1}] không?", maLoai, suggestMa),
                        "Trùng mã loại sản phẩm",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    if (dr == DialogResult.Yes)
                    {
                        maLoai = suggestMa;
                        txtMaLoai.Text = suggestMa;
                    }
                    else
                    {
                        txtMaLoai.Focus();
                        return;
                    }
                }

                LoaiSanPham loai = new LoaiSanPham(maLoai, tenLoai, moTa);
                if (loaiSanPhamDAL.Insert(loai))
                {
                    UiFeedbackHelper.ShowSuccess(this, "Đã thêm loại sản phẩm thành công.");
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
                UiErrorHandler.Show(this, "FRMLOAISANPHAM_UI_ERROR", "Lỗi khi thêm loại sản phẩm.", ex);
            }
        }

        private async void btnSua_Click(object sender, EventArgs e)
        {
            string maLoai = txtMaLoai.Text.Trim();
            string tenLoai = txtTenLoai.Text.Trim();
            string moTa = txtMoTa.Text.Trim();

            if (!ValidationHelper.IsNotEmpty(maLoai))
            {
                ShowValidation(txtMaLoai, "Vui lòng chọn loại sản phẩm cần sửa.");
                return;
            }

            if (!ValidationHelper.IsNotEmpty(tenLoai))
            {
                ShowValidation(txtTenLoai, "Vui lòng nhập tên loại sản phẩm.");
                return;
            }

            try
            {
                if (!loaiSanPhamDAL.Exists(maLoai))
                {
                    MessageBox.Show(string.Format("Loại sản phẩm [{0}] không tồn tại.", maLoai), "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                LoaiSanPham loai = new LoaiSanPham(maLoai, tenLoai, moTa, _currentVersion);
                ConcurrencyUpdateResult result = loaiSanPhamDAL.UpdateWithResult(loai);
                if (result == ConcurrencyUpdateResult.Success)
                {
                    _currentVersion = loai.Version;
                    UiFeedbackHelper.ShowSuccess(this, "Đã cập nhật loại sản phẩm thành công.");
                    await _gridQuery.RefreshAsync(null);
                    SelectCategoryRow(maLoai);
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
                    SelectCategoryRow(maLoai);
                }
                else
                {
                    MessageBox.Show("Cập nhật thất bại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                UiErrorHandler.Show(this, "FRMLOAISANPHAM_UI_ERROR", "Lỗi khi cập nhật loại sản phẩm.", ex);
            }
        }

        private async void btnXoa_Click(object sender, EventArgs e)
        {
            string maLoai = txtMaLoai.Text.Trim();
            string tenLoai = txtTenLoai.Text.Trim();
            if (!ValidationHelper.IsNotEmpty(maLoai))
            {
                ShowValidation(txtMaLoai, "Vui lòng chọn loại sản phẩm cần xóa.");
                return;
            }

            try
            {
                int productCount = loaiSanPhamDAL.GetSanPhamCount(maLoai);
                if (productCount > 0)
                {
                    MessageBox.Show(
                        string.Format("Không thể xóa loại sản phẩm [{0}] vì đang có {1} sản phẩm thuộc loại này.", maLoai, productCount),
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                DialogResult confirm = MessageBox.Show(
                    string.Format("Bạn có chắc chắn muốn xóa loại sản phẩm [{0}] không?", maLoai),
                    "Xác nhận xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (confirm == DialogResult.Yes)
                {
                    if (loaiSanPhamDAL.Delete(maLoai))
                    {
                        UiFeedbackHelper.ShowSuccess(this, string.Format("Đã xóa loại sản phẩm [{0}] thành công.", maLoai));
                        await _gridQuery.RefreshAsync(null);
                        ClearInputs();
                    }
                    else
                    {
                        MessageBox.Show("Không thể xóa loại sản phẩm này.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                UiErrorHandler.Show(this, "FRMLOAISANPHAM_UI_ERROR", "Lỗi khi xóa loại sản phẩm.", ex);
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


        private void SelectCategoryRow(string maLoai)
        {
            if (string.IsNullOrEmpty(maLoai) || dgvLoaiSanPham.Rows.Count == 0)
                return;

            for (int i = 0; i < dgvLoaiSanPham.Rows.Count; i++)
            {
                DataGridViewRow row = dgvLoaiSanPham.Rows[i];
                if (row.Cells["colMaLoai"].Value != null &&
                    string.Equals(row.Cells["colMaLoai"].Value.ToString().Trim(), maLoai.Trim(), StringComparison.OrdinalIgnoreCase))
                {
                    dgvLoaiSanPham.ClearSelection();
                    row.Selected = true;
                    if (dgvLoaiSanPham.Columns.Contains("colTenLoai"))
                    {
                        dgvLoaiSanPham.CurrentCell = row.Cells["colTenLoai"];
                    }
                    dgvLoaiSanPham_CellClick(dgvLoaiSanPham, new DataGridViewCellEventArgs(0, i));
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
