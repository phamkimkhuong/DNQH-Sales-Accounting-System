using System;
using System.Drawing;
using System.Windows.Forms;
using DNQH_KeToanBanHang.Constants;
using DNQH_KeToanBanHang.Helpers;

namespace DNQH_KeToanBanHang.Forms
{
    public partial class frmHoaDonBan
    {
        private void BuildResponsiveLayout()
        {
            SuspendLayout();
            MinimumSize = new Size(760, 620);
            grpThongTin.Controls.Clear();
            grpThongTin.AutoSize = true;
            grpThongTin.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            grpThongTin.Padding = new Padding(12, 10, 12, 10);

            TableLayoutPanel fields = UiLayoutBuilder.CreateStructuredGridLayout(
                3, 95,
                UiLayoutBuilder.Field(lblMaHDB, txtMaHDB, UiFieldSize.Code),
                UiLayoutBuilder.Field(lblDonDatHang, cboDonDatHang, UiFieldSize.Wide),
                UiLayoutBuilder.Field(lblNgayLap, dtpNgayLap, UiFieldSize.DateTime),
                UiLayoutBuilder.Field(lblKHTag, lblKhachHangInfo, UiFieldSize.Wide),
                UiLayoutBuilder.Field(lblNVTag, lblNhanVienLap, UiFieldSize.Wide),
                UiLayoutBuilder.Field(lblTrangThai, cboTrangThai, UiFieldSize.Selection),
                UiLayoutBuilder.FullWidthField(lblGhiChu, txtGhiChu));

            lblNhanVienLap.TextAlign = ContentAlignment.MiddleLeft;
            lblNhanVienLap.AutoEllipsis = true;
            lblKhachHangInfo.TextAlign = ContentAlignment.MiddleLeft;
            lblKhachHangInfo.AutoEllipsis = true;
            grpThongTin.Controls.Add(fields);

            txtTimKiem.MinimumSize = new Size(180, UiTheme.ControlHeight);
            dtpFromDate.MinimumSize = new Size(115, UiTheme.ControlHeight);
            dtpToDate.MinimumSize = new Size(115, UiTheme.ControlHeight);
            cboTrangThaiLoc.MinimumSize = new Size(130, UiTheme.ControlHeight);

            Button btnInHoaDonDS = new Button { Text = "🖨️ In Hóa Đơn", AutoSize = true, Height = UiTheme.ButtonHeight };
            UiStyler.StyleButton(btnInHoaDonDS, UiButtonRole.Primary);
            btnInHoaDonDS.Click += delegate { InHoaDonDangChon(); };

            Button btnXuatCsv = CsvExporter.CreateExportButton(dgvDanhSachHoaDon, "DanhSachHoaDonBan", "DANH SÁCH HÓA ĐƠN BÁN");
            UiLayoutBuilder.BuildHistoryPage(
                tpDanhSach,
                pnlFilterDanhSach,
                new Control[] { lblTimKiem, txtTimKiem, lblFromDate, dtpFromDate, lblToDate, dtpToDate, lblTrangThaiLoc, cboTrangThaiLoc, btnTimKiem, btnLamMoiDanhSach, btnLapPhieuThuTuHDB, btnLapPhieuXuatTuHDB, btnInHoaDonDS, btnXuatCsv },
                dgvDanhSachHoaDon,
                _pagerHoaDon,
                38);

            // Nút in trên Tab 1
            Button btnInHoaDonTab1 = new Button
            {
                Text = "🖨️ In Hóa Đơn",
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Location = new Point(btnLapHoaDon.Left - 130, btnLapHoaDon.Top),
                Size = new Size(120, btnLapHoaDon.Height)
            };
            UiStyler.StyleButton(btnInHoaDonTab1, UiButtonRole.Information);
            btnInHoaDonTab1.Click += delegate
            {
                if (string.IsNullOrWhiteSpace(txtMaHDB.Text))
                {
                    MessageBox.Show("Chưa có thông tin hóa đơn để in!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                InHoaDon(txtMaHDB.Text.Trim());
            };
            pnlTongTien.Controls.Add(btnInHoaDonTab1);

            ResumeLayout(true);
        }

        private void ApplyFoundationDesign()
        {
            lblSubTitle.Text = "Phát hành hóa đơn từ đơn đặt hàng và theo dõi trạng thái thanh toán";
            UiStyler.Apply(this);

            if (!SystemInformation.HighContrast)
            {
                pnlHeader.BackColor = UiTheme.Sidebar;
                lblTitle.ForeColor = Color.White;
                lblSubTitle.ForeColor = Color.FromArgb(203, 213, 225);
                tpLapHoaDon.BackColor = UiTheme.Canvas;
                tpDanhSach.BackColor = UiTheme.Canvas;
                pnlTongTien.BackColor = UiTheme.SurfaceMuted;
                pnlFilterDanhSach.BackColor = UiTheme.SurfaceMuted;
                lblTongTien.ForeColor = UiTheme.Danger;
                lblKhachHangInfo.ForeColor = UiTheme.Information;
                lblNhanVienLap.ForeColor = UiTheme.Information;
            }

            UiStyler.StyleButton(btnLapHoaDon, UiButtonRole.Success);
            UiStyler.StyleButton(btnLamMoi, UiButtonRole.Secondary);
            UiStyler.StyleButton(btnDong, UiButtonRole.Secondary);
            UiStyler.StyleButton(btnTimKiem, UiButtonRole.Primary);
            UiStyler.StyleButton(btnLamMoiDanhSach, UiButtonRole.Secondary);
            UiStyler.StyleButton(btnLapPhieuThuTuHDB, UiButtonRole.Success);
            UiStyler.StyleButton(btnLapPhieuXuatTuHDB, UiButtonRole.Primary);
            CsvExporter.AttachExportContextMenu(dgvDanhSachHoaDon, "DanhSachHoaDonBan", "DANH SÁCH HÓA ĐƠN BÁN");

            if (dgvDanhSachHoaDon.ContextMenuStrip != null)
            {
                ToolStripMenuItem itemInHDB = new ToolStripMenuItem("🖨️ In hóa đơn bán (Mẫu 02-BH)...");
                itemInHDB.Click += delegate { InHoaDonDangChon(); };
                dgvDanhSachHoaDon.ContextMenuStrip.Items.Add(itemInHDB);
            }

            UiStyler.SetAccessibleText(cboDonDatHang, "Đơn đặt hàng", "Chọn đơn đặt hàng chưa phát hành hóa đơn.");
            UiStyler.SetAccessibleText(dtpNgayLap, "Ngày lập hóa đơn", "Ngày và giờ phát hành hóa đơn.");
            UiStyler.SetAccessibleText(cboTrangThai, "Trạng thái thanh toán", "Chọn trạng thái thanh toán của hóa đơn.");
            UiStyler.SetAccessibleText(txtGhiChu, "Ghi chú hóa đơn", "Nhập ghi chú bổ sung cho hóa đơn.");
            UiStyler.SetAccessibleText(dgvChiTiet, "Chi tiết hóa đơn", "Danh sách sản phẩm lấy từ đơn đặt hàng.");
            UiStyler.SetAccessibleText(dgvDanhSachHoaDon, "Danh sách hóa đơn", "Nhấp đúp một hóa đơn để xem chi tiết.");

            ConfigureGridColumns();
            dgvDanhSachHoaDon.CellFormatting += dgvDanhSachHoaDon_CellFormatting;
            dgvChiTiet.CellFormatting += dgvChiTiet_CellFormatting;
        }

        private void ConfigureGridColumns()
        {
            // === Cấu hình lưới Danh Sách Hóa Đơn Bán ===
            dgvDanhSachHoaDon.AutoGenerateColumns = false;
            dgvDanhSachHoaDon.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            colDSMaHDB.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colDSMaHDB.Width = 110;
            colDSMaHDB.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            colDSNgayLap.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colDSNgayLap.Width = 140;
            colDSNgayLap.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colDSNgayLap.DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";

            colDSMaDDH.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colDSMaDDH.Width = 110;
            colDSMaDDH.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            colDSKhachHang.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colDSKhachHang.FillWeight = 160F;
            colDSKhachHang.MinimumWidth = 220;
            colDSKhachHang.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            colDSNhanVien.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colDSNhanVien.Width = 150;
            colDSNhanVien.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            colDSTongTien.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colDSTongTien.Width = 140;
            colDSTongTien.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colDSTongTien.DefaultCellStyle.Format = "N0";

            colDSTrangThai.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colDSTrangThai.Width = 130;
            colDSTrangThai.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            colDSGhiChu.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colDSGhiChu.FillWeight = 200F;
            colDSGhiChu.MinimumWidth = 200;
            colDSGhiChu.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            // === Cấu hình lưới Chi Tiết Hóa Đơn ===
            dgvChiTiet.AutoGenerateColumns = false;
            dgvChiTiet.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvChiTiet.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;

            colMaSP.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colMaSP.Width = 100;
            colMaSP.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            colTenSP.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colTenSP.FillWeight = 180F;
            colTenSP.MinimumWidth = 180;
            colTenSP.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            colDonViTinh.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colDonViTinh.Width = 70;
            colDonViTinh.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            colSoLuong.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colSoLuong.Width = 90;
            colSoLuong.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colSoLuong.DefaultCellStyle.Format = "N0";

            colDonGia.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colDonGia.Width = 125;
            colDonGia.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colDonGia.DefaultCellStyle.Format = "N0";

            colGiamGia.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colGiamGia.Width = 80;
            colGiamGia.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colGiamGia.DefaultCellStyle.Format = "0'%'";

            colThanhTien.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colThanhTien.Width = 130;
            colThanhTien.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colThanhTien.DefaultCellStyle.Format = "N0";
        }

        private void dgvDanhSachHoaDon_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            string colName = dgvDanhSachHoaDon.Columns[e.ColumnIndex].Name;

            if (colName == "colDSTongTien" && e.Value != null)
            {
                e.CellStyle.Font = new Font(dgvDanhSachHoaDon.Font, FontStyle.Bold);
                e.CellStyle.ForeColor = Color.FromArgb(180, 20, 20);
            }
            else if (colName == "colDSTrangThai" && e.Value != null)
            {
                string status = e.Value.ToString().Trim();
                if (InvoiceStatusConstants.IsFullyPaid(status))
                {
                    e.CellStyle.ForeColor = Color.FromArgb(16, 120, 60);
                    e.CellStyle.Font = new Font(dgvDanhSachHoaDon.Font, FontStyle.Bold);
                }
                else if (InvoiceStatusConstants.IsPartiallyPaid(status))
                {
                    e.CellStyle.ForeColor = Color.FromArgb(180, 100, 10);
                    e.CellStyle.Font = new Font(dgvDanhSachHoaDon.Font, FontStyle.Bold);
                }
                else if (InvoiceStatusConstants.IsUnpaid(status))
                {
                    e.CellStyle.ForeColor = Color.FromArgb(200, 30, 30);
                    e.CellStyle.Font = new Font(dgvDanhSachHoaDon.Font, FontStyle.Bold);
                }
                else if (InvoiceStatusConstants.IsCancelled(status))
                {
                    e.CellStyle.ForeColor = Color.FromArgb(140, 140, 140);
                    e.CellStyle.Font = new Font(dgvDanhSachHoaDon.Font, FontStyle.Italic);
                }
            }
        }

        private void dgvChiTiet_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            string colName = dgvChiTiet.Columns[e.ColumnIndex].Name;
            if (colName == "colThanhTien" && e.Value != null)
            {
                e.CellStyle.Font = new Font(dgvChiTiet.Font, FontStyle.Bold);
                e.CellStyle.ForeColor = Color.FromArgb(0, 100, 180);
            }
        }
    }
}
