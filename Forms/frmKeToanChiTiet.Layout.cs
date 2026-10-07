using System;
using System.Drawing;
using System.Windows.Forms;
using DNQH_KeToanBanHang.Helpers;

namespace DNQH_KeToanBanHang.Forms
{
    public partial class frmKeToanChiTiet
    {
        private void BuildResponsiveLayout()
        {
            SuspendLayout();
            MinimumSize = new Size(920, 660);
            BuildPageHeader();

            cboKhachHang.MinimumSize = new Size(220, UiTheme.ControlHeight);
            cboSanPham.MinimumSize = new Size(220, UiTheme.ControlHeight);
            DateTimePicker[] dates = { dtpTuNgayKH, dtpDenNgayKH, dtpTuNgaySP, dtpDenNgaySP };
            foreach (DateTimePicker picker in dates)
                picker.MinimumSize = new Size(130, UiTheme.ControlHeight);

            _khachHangStatus = UiLayoutBuilder.BuildReportPage(
                tabKhachHang,
                pnlFilterKH,
                new Control[] { lblChonKH, cboKhachHang, lblTuNgayKH, dtpTuNgayKH, lblDenNgayKH, dtpDenNgayKH, btnXemKH, btnXuatCsvKH, btnInKH },
                dgvKhachHang,
                pnlSummaryKH,
                new Control[] { lblKHTongPhatSinhNo, lblKHTongPhatSinhCo, lblKHSoDuCuoiKy },
                "Chọn khách hàng và khoảng thời gian để xem công nợ.");

            _sanPhamStatus = UiLayoutBuilder.BuildReportPage(
                tabSanPham,
                pnlFilterSP,
                new Control[] { lblChonSP, cboSanPham, lblTuNgaySP, dtpTuNgaySP, lblDenNgaySP, dtpDenNgaySP, btnXemSP, btnXuatCsvSP, btnInSP },
                dgvSanPham,
                pnlSummarySP,
                new Control[] { lblSPTongSoLuong, lblSPTongDoanhThu },
                "Chọn sản phẩm và khoảng thời gian để xem doanh số.");

            Controls.Clear();
            TableLayoutPanel root = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 2,
                BackColor = UiTheme.Canvas,
                Padding = new Padding(UiTheme.PagePadding)
            };
            root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            root.RowStyles.Add(new RowStyle(SizeType.Absolute, 66F));
            root.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            pnlHeader.Dock = DockStyle.Fill;
            pnlHeader.Margin = new Padding(0, 0, 0, 8);
            tabControlMain.Dock = DockStyle.Fill;
            tabControlMain.Margin = new Padding(0);
            root.Controls.Add(pnlHeader, 0, 0);
            root.Controls.Add(tabControlMain, 0, 1);
            Controls.Add(root);
            ResumeLayout(true);
        }

        private void BuildPageHeader()
        {
            pnlHeader.Controls.Clear();
            pnlHeader.Padding = new Padding(4, 2, 4, 2);
            TableLayoutPanel heading = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 2
            };
            heading.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            heading.RowStyles.Add(new RowStyle(SizeType.Absolute, 34F));
            heading.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            lblTitle.Dock = DockStyle.Fill;
            lblTitle.TextAlign = ContentAlignment.MiddleLeft;
            lblSubTitle.Dock = DockStyle.Fill;
            lblSubTitle.TextAlign = ContentAlignment.MiddleLeft;
            heading.Controls.Add(lblTitle, 0, 0);
            heading.Controls.Add(lblSubTitle, 0, 1);
            pnlHeader.Controls.Add(heading);
        }

        private void ApplyFoundationDesign()
        {
            UiStyler.Apply(this);
            if (!SystemInformation.HighContrast)
            {
                pnlHeader.BackColor = UiTheme.Canvas;
                lblTitle.ForeColor = UiTheme.Primary;
                lblSubTitle.ForeColor = UiTheme.TextSecondary;
                pnlFilterKH.BackColor = UiTheme.Surface;
                pnlFilterSP.BackColor = UiTheme.Surface;
                pnlSummaryKH.BackColor = UiTheme.SurfaceMuted;
                pnlSummarySP.BackColor = UiTheme.SurfaceMuted;
                lblKHTongPhatSinhNo.ForeColor = UiTheme.Information;
                lblKHTongPhatSinhCo.ForeColor = UiTheme.Success;
                lblKHSoDuCuoiKy.ForeColor = UiTheme.Danger;
                lblSPTongDoanhThu.ForeColor = UiTheme.Success;
            }
            Button[] viewButtons = { btnXemKH, btnXemSP };
            foreach (Button button in viewButtons)
                UiStyler.StyleButton(button, UiButtonRole.Primary);
            Button[] exportButtons = { btnXuatCsvKH, btnXuatCsvSP };
            foreach (Button button in exportButtons)
                UiStyler.StyleButton(button, UiButtonRole.Success);
            Button[] printButtons = { btnInKH, btnInSP };
            foreach (Button button in printButtons)
                UiStyler.StyleButton(button, UiButtonRole.Secondary);
            UiStyler.StyleStatusLabel(_khachHangStatus, UiStatusKind.Neutral, "Chọn khách hàng và khoảng thời gian để xem công nợ.");
            UiStyler.StyleStatusLabel(_sanPhamStatus, UiStatusKind.Neutral, "Chọn sản phẩm và khoảng thời gian để xem doanh số.");

            UiStyler.SetAccessibleText(cboKhachHang, "Khách hàng", "Chọn khách hàng cần xem công nợ.");
            UiStyler.SetAccessibleText(cboSanPham, "Sản phẩm", "Chọn một sản phẩm hoặc tất cả sản phẩm.");
            UiStyler.SetAccessibleText(dgvKhachHang, "Sổ chi tiết khách hàng", "Các phát sinh công nợ của khách hàng.");
            UiStyler.SetAccessibleText(dgvSanPham, "Sổ chi tiết sản phẩm", "Doanh số bán theo sản phẩm.");
        }

        private void InitGridColumns()
        {
            // Grid KhachHang
            dgvKhachHang.Columns.Clear();
            dgvKhachHang.AutoGenerateColumns = false;
            dgvKhachHang.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgvKhachHang.Columns.Add(new DataGridViewTextBoxColumn { Name = "NgayGiaoDich", HeaderText = "Ngày GD", AutoSizeMode = DataGridViewAutoSizeColumnMode.None, Width = 140, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter, Format = "dd/MM/yyyy HH:mm" } });
            dgvKhachHang.Columns.Add(new DataGridViewTextBoxColumn { Name = "LoaiNghiepVu", HeaderText = "Loại Nghiệp Vụ", AutoSizeMode = DataGridViewAutoSizeColumnMode.None, Width = 130, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleLeft } });
            dgvKhachHang.Columns.Add(new DataGridViewTextBoxColumn { Name = "SoChungTu", HeaderText = "Số Chứng Từ", AutoSizeMode = DataGridViewAutoSizeColumnMode.None, Width = 110, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter } });
            dgvKhachHang.Columns.Add(new DataGridViewTextBoxColumn { Name = "DienGiai", HeaderText = "Diễn Giải", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill, FillWeight = 180F, MinimumWidth = 180, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleLeft } });
            dgvKhachHang.Columns.Add(new DataGridViewTextBoxColumn { Name = "PhatSinhNo", HeaderText = "Phát Sinh Nợ (Mua)", AutoSizeMode = DataGridViewAutoSizeColumnMode.None, Width = 130, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N0" } });
            dgvKhachHang.Columns.Add(new DataGridViewTextBoxColumn { Name = "PhatSinhCo", HeaderText = "Phát Sinh Có (Trả)", AutoSizeMode = DataGridViewAutoSizeColumnMode.None, Width = 130, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N0" } });
            dgvKhachHang.Columns.Add(new DataGridViewTextBoxColumn { Name = "SoDuCuoiKy", HeaderText = "Số Dư Còn Lại", AutoSizeMode = DataGridViewAutoSizeColumnMode.None, Width = 135, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N0", ForeColor = Color.FromArgb(192, 57, 43), Font = new Font("Segoe UI", 9F, FontStyle.Bold) } });

            // Grid SanPham
            dgvSanPham.Columns.Clear();
            dgvSanPham.AutoGenerateColumns = false;
            dgvSanPham.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgvSanPham.Columns.Add(new DataGridViewTextBoxColumn { Name = "MaSP", HeaderText = "Mã SP", AutoSizeMode = DataGridViewAutoSizeColumnMode.None, Width = 95, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter } });
            dgvSanPham.Columns.Add(new DataGridViewTextBoxColumn { Name = "TenSP", HeaderText = "Tên Sản Phẩm", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill, FillWeight = 180F, MinimumWidth = 180, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleLeft } });
            dgvSanPham.Columns.Add(new DataGridViewTextBoxColumn { Name = "TenLoaiSP", HeaderText = "Loại Sản Phẩm", AutoSizeMode = DataGridViewAutoSizeColumnMode.None, Width = 140, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleLeft } });
            dgvSanPham.Columns.Add(new DataGridViewTextBoxColumn { Name = "DonViTinh", HeaderText = "ĐVT", AutoSizeMode = DataGridViewAutoSizeColumnMode.None, Width = 65, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter } });
            dgvSanPham.Columns.Add(new DataGridViewTextBoxColumn { Name = "TongSoLuongBan", HeaderText = "Số Lượng Bán", AutoSizeMode = DataGridViewAutoSizeColumnMode.None, Width = 100, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N0" } });
            dgvSanPham.Columns.Add(new DataGridViewTextBoxColumn { Name = "DonGiaTrungBinh", HeaderText = "Đơn Giá B/Q", AutoSizeMode = DataGridViewAutoSizeColumnMode.None, Width = 120, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N0" } });
            dgvSanPham.Columns.Add(new DataGridViewTextBoxColumn { Name = "TongDoanhThu", HeaderText = "Tổng Doanh Thu", AutoSizeMode = DataGridViewAutoSizeColumnMode.None, Width = 135, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N0", ForeColor = Color.FromArgb(39, 174, 96), Font = new Font("Segoe UI", 9F, FontStyle.Bold) } });
            dgvSanPham.Columns.Add(new DataGridViewTextBoxColumn { Name = "SoHoaDonPhatSinh", HeaderText = "Số Lượt HĐ", AutoSizeMode = DataGridViewAutoSizeColumnMode.None, Width = 90, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter } });
        }
    }
}
