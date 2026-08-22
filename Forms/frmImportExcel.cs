using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DNQH_KeToanBanHang.Helpers;
using DNQH_KeToanBanHang.Models;

namespace DNQH_KeToanBanHang.Forms
{
    public partial class frmImportExcel : Form
    {
        private ImportEntityType _entityType;
        private readonly bool _lockEntityType;
        private ImportBatchResult _batchResult;
        private string _selectedFilePath;
        private int _insertedCount = 0;

        public int InsertedCount
        {
            get { return _insertedCount; }
        }

        public frmImportExcel(ImportEntityType defaultType = ImportEntityType.KhachHang, bool lockType = false)
        {
            InitializeComponent();
            _entityType = defaultType;
            _lockEntityType = lockType;
        }

        private void frmImportExcel_Load(object sender, EventArgs e)
        {
            // Nạp danh sách loại danh mục
            cboLoaiDanhMuc.Items.Clear();
            cboLoaiDanhMuc.Items.Add("Khách hàng");
            cboLoaiDanhMuc.Items.Add("Nhà cung cấp");
            cboLoaiDanhMuc.Items.Add("Sản phẩm");

            switch (_entityType)
            {
                case ImportEntityType.KhachHang:
                    cboLoaiDanhMuc.SelectedIndex = 0;
                    break;
                case ImportEntityType.NhaCungCap:
                    cboLoaiDanhMuc.SelectedIndex = 1;
                    break;
                case ImportEntityType.SanPham:
                    cboLoaiDanhMuc.SelectedIndex = 2;
                    break;
            }

            if (_lockEntityType)
            {
                cboLoaiDanhMuc.Enabled = false;
            }

            cboLoc.SelectedIndex = 0;
            ConfigureGridColumns();
            UpdateStatsUI();
        }

        private void cboLoaiDanhMuc_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboLoaiDanhMuc.SelectedIndex == 0)
                _entityType = ImportEntityType.KhachHang;
            else if (cboLoaiDanhMuc.SelectedIndex == 1)
                _entityType = ImportEntityType.NhaCungCap;
            else
                _entityType = ImportEntityType.SanPham;

            ConfigureGridColumns();

            // Nếu đã có tệp được chọn trước đó, phân tích lại theo danh mục mới
            if (!string.IsNullOrEmpty(_selectedFilePath) && File.Exists(_selectedFilePath))
            {
                ProcessFileAsync(_selectedFilePath);
            }
            else
            {
                _batchResult = null;
                UpdateStatsUI();
            }
        }

        private void ConfigureGridColumns()
        {
            dgvPreview.Columns.Clear();
            dgvPreview.AutoGenerateColumns = false;

            // Cột hệ thống kiểm tra
            DataGridViewTextBoxColumn colSTT = new DataGridViewTextBoxColumn
            {
                Name = "colSTT",
                HeaderText = "STT",
                Width = 50,
                ReadOnly = true,
                DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter }
            };
            DataGridViewTextBoxColumn colDongFile = new DataGridViewTextBoxColumn
            {
                Name = "colDongFile",
                HeaderText = "Dòng Excel",
                Width = 85,
                ReadOnly = true,
                DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter }
            };
            DataGridViewTextBoxColumn colTrangThai = new DataGridViewTextBoxColumn
            {
                Name = "colTrangThai",
                HeaderText = "Trạng thái",
                Width = 100,
                ReadOnly = true,
                DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter }
            };
            DataGridViewTextBoxColumn colLoi = new DataGridViewTextBoxColumn
            {
                Name = "colLoi",
                HeaderText = "Chi tiết kiểm tra / Lỗi",
                Width = 240,
                ReadOnly = true
            };

            dgvPreview.Columns.AddRange(colSTT, colDongFile, colTrangThai, colLoi);

            // Cột theo từng loại danh mục
            if (_entityType == ImportEntityType.KhachHang)
            {
                dgvPreview.Columns.Add(new DataGridViewTextBoxColumn { Name = "colMaKH", HeaderText = "Mã KH", Width = 100, ReadOnly = true });
                dgvPreview.Columns.Add(new DataGridViewTextBoxColumn { Name = "colTenKH", HeaderText = "Tên khách hàng", Width = 180, ReadOnly = true });
                dgvPreview.Columns.Add(new DataGridViewTextBoxColumn { Name = "colSoDienThoai", HeaderText = "Số điện thoại", Width = 110, ReadOnly = true });
                dgvPreview.Columns.Add(new DataGridViewTextBoxColumn { Name = "colDiaChi", HeaderText = "Địa chỉ", Width = 200, ReadOnly = true });
                dgvPreview.Columns.Add(new DataGridViewTextBoxColumn { Name = "colEmail", HeaderText = "Email", Width = 150, ReadOnly = true });
            }
            else if (_entityType == ImportEntityType.NhaCungCap)
            {
                dgvPreview.Columns.Add(new DataGridViewTextBoxColumn { Name = "colMaNCC", HeaderText = "Mã NCC", Width = 100, ReadOnly = true });
                dgvPreview.Columns.Add(new DataGridViewTextBoxColumn { Name = "colTenNCC", HeaderText = "Tên nhà cung cấp", Width = 180, ReadOnly = true });
                dgvPreview.Columns.Add(new DataGridViewTextBoxColumn { Name = "colDiaChi", HeaderText = "Địa chỉ", Width = 200, ReadOnly = true });
                dgvPreview.Columns.Add(new DataGridViewTextBoxColumn { Name = "colSoDienThoai", HeaderText = "Số điện thoại", Width = 110, ReadOnly = true });
                dgvPreview.Columns.Add(new DataGridViewTextBoxColumn { Name = "colEmail", HeaderText = "Email", Width = 150, ReadOnly = true });
            }
            else // SanPham
            {
                dgvPreview.Columns.Add(new DataGridViewTextBoxColumn { Name = "colMaSP", HeaderText = "Mã SP", Width = 95, ReadOnly = true });
                dgvPreview.Columns.Add(new DataGridViewTextBoxColumn { Name = "colTenSP", HeaderText = "Tên sản phẩm", Width = 180, ReadOnly = true });
                dgvPreview.Columns.Add(new DataGridViewTextBoxColumn { Name = "colMaLoai", HeaderText = "Mã loại", Width = 85, ReadOnly = true });
                dgvPreview.Columns.Add(new DataGridViewTextBoxColumn { Name = "colMaNCC", HeaderText = "Mã NCC", Width = 85, ReadOnly = true });
                dgvPreview.Columns.Add(new DataGridViewTextBoxColumn { Name = "colDonViTinh", HeaderText = "ĐVT", Width = 75, ReadOnly = true });
                dgvPreview.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "colDonGiaBan",
                    HeaderText = "Đơn giá bán",
                    Width = 115,
                    ReadOnly = true,
                    DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N0" }
                });
                dgvPreview.Columns.Add(new DataGridViewTextBoxColumn { Name = "colTrangThaiSP", HeaderText = "Trạng thái", Width = 120, ReadOnly = true });
            }
        }

        private void btnChonTep_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = "Chọn tệp Excel hoặc CSV chứa dữ liệu cần nhập";
                ofd.Filter = "Tệp bảng tính (*.xlsx;*.xls;*.xml;*.csv)|*.xlsx;*.xls;*.xml;*.csv|Tệp Excel OpenXML (*.xlsx)|*.xlsx|Tệp Excel (.xls, .xml)|*.xls;*.xml|Tệp CSV (*.csv)|*.csv|Tất cả tệp (*.*)|*.*";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    _selectedFilePath = ofd.FileName;
                    lblDuongDanTep.Text = string.Format("Tệp đã chọn: {0}", _selectedFilePath);
                    ProcessFileAsync(_selectedFilePath);
                }
            }
        }

        private void btnTaiMau_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                string typeName = ExcelImporter.GetEntityTypeName(_entityType).Replace(" ", "");
                sfd.Title = "Lưu tệp mẫu Excel";
                sfd.Filter = "Tệp Excel (*.xls)|*.xls|Tất cả tệp (*.*)|*.*";
                sfd.FileName = string.Format("MauNhap_{0}_{1:yyyyMMdd}.xls", typeName, DateTime.Now);

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    bool ok = ExcelImporter.SaveTemplate(_entityType, sfd.FileName);
                    if (ok)
                    {
                        var answer = MessageBox.Show(
                            string.Format("Đã tạo tệp mẫu thành công tại:\n{0}\n\nBạn có muốn mở tệp vừa tạo ngay không?", sfd.FileName),
                            "Tải tệp mẫu thành công",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Information);

                        if (answer == DialogResult.Yes)
                        {
                            try { System.Diagnostics.Process.Start(sfd.FileName); } catch { }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Có lỗi xảy ra khi lưu tệp mẫu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private async void ProcessFileAsync(string filePath)
        {
            lblStatus.Text = "Đang đọc và kiểm tra dữ liệu từ tệp...";
            btnChonTep.Enabled = false;
            btnXacNhan.Enabled = false;
            Cursor = Cursors.WaitCursor;

            try
            {
                ImportBatchResult result = await Task.Run(() =>
                {
                    List<List<string>> raw = ExcelImporter.ReadFile(filePath);
                    return ExcelImporter.ValidateBatch(_entityType, raw);
                });

                _batchResult = result;
                PopulateGrid();
                UpdateStatsUI();
                lblStatus.Text = string.Format("Đã phân tích xong tệp: {0} dòng ({1} hợp lệ, {2} lỗi).",
                    result.TotalRows, result.ValidCount, result.ErrorCount);
            }
            catch (Exception ex)
            {
                _batchResult = null;
                dgvPreview.Rows.Clear();
                UpdateStatsUI();
                lblStatus.Text = "Lỗi khi đọc tệp.";
                MessageBox.Show(ex.Message, "Lỗi đọc tệp", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                btnChonTep.Enabled = true;
                Cursor = Cursors.Default;
            }
        }

        private void PopulateGrid()
        {
            dgvPreview.Rows.Clear();
            if (_batchResult == null || _batchResult.Rows == null || _batchResult.Rows.Count == 0)
            {
                return;
            }

            int filterIndex = cboLoc.SelectedIndex; // 0: All, 1: Valid, 2: Errors

            int displayStt = 1;
            foreach (ImportRowResult r in _batchResult.Rows)
            {
                if (filterIndex == 1 && !r.IsValid) continue;
                if (filterIndex == 2 && r.IsValid) continue;

                int rowIndex = dgvPreview.Rows.Add();
                DataGridViewRow dgvRow = dgvPreview.Rows[rowIndex];
                dgvRow.Tag = r;

                dgvRow.Cells["colSTT"].Value = displayStt++;
                dgvRow.Cells["colDongFile"].Value = r.RowNumber;
                dgvRow.Cells["colTrangThai"].Value = r.StatusDisplay;
                dgvRow.Cells["colLoi"].Value = r.ErrorDisplay;

                if (_entityType == ImportEntityType.KhachHang)
                {
                    dgvRow.Cells["colMaKH"].Value = r.RawValues.ContainsKey("MaKH") ? r.RawValues["MaKH"] : "";
                    dgvRow.Cells["colTenKH"].Value = r.RawValues.ContainsKey("TenKH") ? r.RawValues["TenKH"] : "";
                    dgvRow.Cells["colSoDienThoai"].Value = r.RawValues.ContainsKey("SoDienThoai") ? r.RawValues["SoDienThoai"] : "";
                    dgvRow.Cells["colDiaChi"].Value = r.RawValues.ContainsKey("DiaChi") ? r.RawValues["DiaChi"] : "";
                    dgvRow.Cells["colEmail"].Value = r.RawValues.ContainsKey("Email") ? r.RawValues["Email"] : "";
                }
                else if (_entityType == ImportEntityType.NhaCungCap)
                {
                    dgvRow.Cells["colMaNCC"].Value = r.RawValues.ContainsKey("MaNCC") ? r.RawValues["MaNCC"] : "";
                    dgvRow.Cells["colTenNCC"].Value = r.RawValues.ContainsKey("TenNCC") ? r.RawValues["TenNCC"] : "";
                    dgvRow.Cells["colDiaChi"].Value = r.RawValues.ContainsKey("DiaChi") ? r.RawValues["DiaChi"] : "";
                    dgvRow.Cells["colSoDienThoai"].Value = r.RawValues.ContainsKey("SoDienThoai") ? r.RawValues["SoDienThoai"] : "";
                    dgvRow.Cells["colEmail"].Value = r.RawValues.ContainsKey("Email") ? r.RawValues["Email"] : "";
                }
                else // SanPham
                {
                    dgvRow.Cells["colMaSP"].Value = r.RawValues.ContainsKey("MaSP") ? r.RawValues["MaSP"] : "";
                    dgvRow.Cells["colTenSP"].Value = r.RawValues.ContainsKey("TenSP") ? r.RawValues["TenSP"] : "";
                    dgvRow.Cells["colMaLoai"].Value = r.RawValues.ContainsKey("MaLoai") ? r.RawValues["MaLoai"] : "";
                    dgvRow.Cells["colMaNCC"].Value = r.RawValues.ContainsKey("MaNCC") ? r.RawValues["MaNCC"] : "";
                    dgvRow.Cells["colDonViTinh"].Value = r.RawValues.ContainsKey("DonViTinh") ? r.RawValues["DonViTinh"] : "";
                    dgvRow.Cells["colDonGiaBan"].Value = r.RawValues.ContainsKey("DonGiaBan") ? r.RawValues["DonGiaBan"] : "";
                    dgvRow.Cells["colTrangThaiSP"].Value = r.RawValues.ContainsKey("TrangThai") ? r.RawValues["TrangThai"] : "";
                }
            }
        }

        private void cboLoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateGrid();
        }

        private void dgvPreview_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvPreview.Rows.Count) return;

            DataGridViewRow dgvRow = dgvPreview.Rows[e.RowIndex];
            ImportRowResult r = dgvRow.Tag as ImportRowResult;
            if (r == null) return;

            if (!r.IsValid)
            {
                // Dòng có lỗi: nền hồng nhạt, chữ đỏ
                dgvRow.DefaultCellStyle.BackColor = Color.FromArgb(254, 242, 242);
                dgvRow.DefaultCellStyle.SelectionBackColor = Color.FromArgb(254, 226, 226);
                dgvRow.DefaultCellStyle.SelectionForeColor = Color.FromArgb(153, 27, 27);

                if (dgvPreview.Columns[e.ColumnIndex].Name == "colTrangThai")
                {
                    e.CellStyle.ForeColor = Color.FromArgb(185, 28, 28);
                    e.CellStyle.Font = new Font(dgvPreview.Font, FontStyle.Bold);
                }
                else if (dgvPreview.Columns[e.ColumnIndex].Name == "colLoi")
                {
                    e.CellStyle.ForeColor = Color.FromArgb(185, 28, 28);
                }
            }
            else
            {
                if (dgvPreview.Columns[e.ColumnIndex].Name == "colTrangThai")
                {
                    e.CellStyle.ForeColor = Color.FromArgb(21, 128, 61);
                    e.CellStyle.Font = new Font(dgvPreview.Font, FontStyle.Bold);
                }
            }
        }

        private void UpdateStatsUI()
        {
            if (_batchResult == null)
            {
                lblTongSo.Text = "📊 Tổng cộng: 0 dòng";
                lblHopLe.Text = "✔ Hợp lệ: 0 (0%)";
                lblCoLoi.Text = "✖ Có lỗi: 0 (0%)";
                btnXacNhan.Enabled = false;
                return;
            }

            int total = _batchResult.TotalRows;
            int valid = _batchResult.ValidCount;
            int err = _batchResult.ErrorCount;

            double validPct = total > 0 ? ((double)valid / total) * 100 : 0;
            double errPct = total > 0 ? ((double)err / total) * 100 : 0;

            lblTongSo.Text = string.Format("📊 Tổng cộng: {0:N0} dòng", total);
            lblHopLe.Text = string.Format("✔ Hợp lệ: {0:N0} ({1:F1}%)", valid, validPct);
            lblCoLoi.Text = string.Format("✖ Có lỗi: {0:N0} ({1:F1}%)", err, errPct);

            bool canInsert = valid > 0;
            btnXacNhan.Enabled = canInsert;
        }

        private async void btnXacNhan_Click(object sender, EventArgs e)
        {
            if (_batchResult == null || _batchResult.TotalRows == 0)
            {
                MessageBox.Show("Chưa có dữ liệu nào để nhập.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            List<ImportRowResult> rowsToInsert;
            if (chkBoQuaLoi.Checked)
            {
                rowsToInsert = _batchResult.Rows.Where(r => r.IsValid).ToList();
            }
            else
            {
                if (_batchResult.HasErrors)
                {
                    MessageBox.Show(
                        string.Format("Tệp dữ liệu đang có {0} dòng bị lỗi.\n\n" +
                                      "Vui lòng sửa các dòng lỗi trong tệp Excel hoặc tích chọn 'Chỉ nhập các dòng hợp lệ' để tiếp tục.",
                                      _batchResult.ErrorCount),
                        "Dữ liệu chưa hoàn toàn hợp lệ",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }
                rowsToInsert = _batchResult.Rows;
            }

            if (rowsToInsert.Count == 0)
            {
                MessageBox.Show("Không có dòng hợp lệ nào để nhập vào hệ thống.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string confirmMsg = string.Format(
                "Hệ thống chuẩn bị nhập {0} bản ghi {1} vào cơ sở dữ liệu.\n\n" +
                (_batchResult.ErrorCount > 0 ? string.Format("(Đã bỏ qua {0} dòng bị lỗi)\n\n", _batchResult.ErrorCount) : "") +
                "Bạn có chắc chắn muốn thực hiện thao tác này?",
                rowsToInsert.Count,
                ExcelImporter.GetEntityTypeName(_entityType).ToLower());

            DialogResult dr = MessageBox.Show(confirmMsg, "Xác nhận nhập dữ liệu", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr != DialogResult.Yes) return;

            btnXacNhan.Enabled = false;
            btnDong.Enabled = false;
            btnChonTep.Enabled = false;
            lblStatus.Text = "Đang lưu dữ liệu vào CSDL...";
            Cursor = Cursors.WaitCursor;

            try
            {
                int count = await Task.Run(() => ExcelImporter.ExecuteImport(_entityType, rowsToInsert));
                _insertedCount = count;

                MessageBox.Show(
                    string.Format("Nhập dữ liệu thành công!\n\nĐã thêm mới {0} bản ghi {1} vào cơ sở dữ liệu.",
                    count, ExcelImporter.GetEntityTypeName(_entityType).ToLower()),
                    "Thành công",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                UiErrorHandler.Show(this, "BULK_IMPORT_EXEC_ERROR", "Lỗi khi ghi dữ liệu vào CSDL. Toàn bộ thao tác đã được hoàn tác an toàn.", ex);
                btnXacNhan.Enabled = true;
                btnDong.Enabled = true;
                btnChonTep.Enabled = true;
                lblStatus.Text = "Nhập dữ liệu thất bại.";
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
