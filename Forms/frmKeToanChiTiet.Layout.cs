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
            cboHoaDon.MinimumSize = new Size(300, UiTheme.ControlHeight);
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

            BuildInvoiceDetailPage();
            InitializeTuoiNoComponents();

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

        private void BuildInvoiceDetailPage()
        {
            UiLayoutBuilder.BuildFilterBar(
                pnlFilterHDB,
                new Control[] { lblChonHDB, cboHoaDon, btnXemHDB, btnXuatCsvHDB, btnInHDB });

            pnlHDBInfo.Controls.Clear();
            pnlHDBInfo.Dock = DockStyle.Fill;
            pnlHDBInfo.AutoSize = true;
            pnlHDBInfo.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            pnlHDBInfo.Padding = new Padding(12, 8, 8, 4);
            FlowLayoutPanel invoiceSummary = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                WrapContents = true,
                Margin = new Padding(0)
            };
            Label[] invoiceLabels = { lblHDBMa, lblHDBNgay, lblHDBKhachHang, lblHDBTongTien, lblHDBDaThu, lblHDBConLai, lblHDBTrangThai };
            for (int i = 0; i < invoiceLabels.Length; i++)
            {
                invoiceLabels[i].AutoSize = true;
                invoiceLabels[i].Margin = new Padding(0, 0, i == invoiceLabels.Length - 1 ? 0 : 24, 5);
                invoiceSummary.Controls.Add(invoiceLabels[i]);
            }
            pnlHDBInfo.Controls.Add(invoiceSummary);

            _hoaDonStatus = new Label
            {
                Name = "lblHoaDonReportStatus",
                Dock = DockStyle.Fill,
                AutoEllipsis = true,
                Text = "Chọn hóa đơn để xem mặt hàng, phiếu thu và định khoản.",
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(8, 0, 0, 0),
                Margin = new Padding(0)
            };

            tabHoaDon.Controls.Clear();
            TableLayoutPanel root = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 5,
                BackColor = UiTheme.Canvas
            };
            root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            root.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            root.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            root.RowStyles.Add(new RowStyle(SizeType.Percent, 45F));
            root.RowStyles.Add(new RowStyle(SizeType.Percent, 55F));
            root.RowStyles.Add(new RowStyle(SizeType.Absolute, 28F));
            pnlFilterHDB.Margin = new Padding(0, 0, 0, 8);
            pnlHDBInfo.Margin = new Padding(0, 0, 0, 8);
            grpMatHang.Dock = DockStyle.Fill;
            grpMatHang.Margin = new Padding(0, 0, 0, 8);
            grpKetoanLienKet.Dock = DockStyle.Fill;
            grpKetoanLienKet.Margin = new Padding(0);
            dgvMatHang.Dock = DockStyle.Fill;
            tabSubDetails.Dock = DockStyle.Fill;
            root.Controls.Add(pnlFilterHDB, 0, 0);
            root.Controls.Add(pnlHDBInfo, 0, 1);
            root.Controls.Add(grpMatHang, 0, 2);
            root.Controls.Add(grpKetoanLienKet, 0, 3);
            root.Controls.Add(_hoaDonStatus, 0, 4);
            tabHoaDon.Controls.Add(root);
        }

        private void ApplyFoundationDesign()
        {
            UiStyler.Apply(this);
            if (!SystemInformation.HighContrast)
            {
                pnlHeader.BackColor = UiTheme.Canvas;
                pnlFilterKH.BackColor = UiTheme.Surface;
                pnlFilterSP.BackColor = UiTheme.Surface;
                pnlFilterHDB.BackColor = UiTheme.Surface;
                pnlSummaryKH.BackColor = UiTheme.SurfaceMuted;
                pnlSummarySP.BackColor = UiTheme.SurfaceMuted;
                pnlHDBInfo.BackColor = UiTheme.SurfaceMuted;
                lblKHTongPhatSinhNo.ForeColor = UiTheme.Information;
                lblKHTongPhatSinhCo.ForeColor = UiTheme.Success;
                lblKHSoDuCuoiKy.ForeColor = UiTheme.Danger;
                lblSPTongDoanhThu.ForeColor = UiTheme.Success;
                lblHDBTongTien.ForeColor = UiTheme.Information;
                lblHDBDaThu.ForeColor = UiTheme.Success;
                lblHDBConLai.ForeColor = UiTheme.Danger;
            }
            Button[] viewButtons = { btnXemKH, btnXemSP, btnXemHDB };
            foreach (Button button in viewButtons)
                UiStyler.StyleButton(button, UiButtonRole.Primary);
            Button[] exportButtons = { btnXuatCsvKH, btnXuatCsvSP, btnXuatCsvHDB };
            foreach (Button button in exportButtons)
                UiStyler.StyleButton(button, UiButtonRole.Success);
            Button[] printButtons = { btnInKH, btnInSP, btnInHDB };
            foreach (Button button in printButtons)
                UiStyler.StyleButton(button, UiButtonRole.Secondary);
            UiStyler.StyleStatusLabel(_khachHangStatus, UiStatusKind.Neutral, "Chọn khách hàng và khoảng thời gian để xem công nợ.");
            UiStyler.StyleStatusLabel(_sanPhamStatus, UiStatusKind.Neutral, "Chọn sản phẩm và khoảng thời gian để xem doanh số.");
            UiStyler.StyleStatusLabel(_hoaDonStatus, UiStatusKind.Neutral, "Chọn hóa đơn để xem mặt hàng, phiếu thu và định khoản.");

            UiStyler.SetAccessibleText(cboKhachHang, "Khách hàng", "Chọn khách hàng cần xem công nợ.");
            UiStyler.SetAccessibleText(cboSanPham, "Sản phẩm", "Chọn một sản phẩm hoặc tất cả sản phẩm.");
            UiStyler.SetAccessibleText(cboHoaDon, "Hóa đơn bán", "Chọn hóa đơn cần xem chi tiết.");
            UiStyler.SetAccessibleText(dgvKhachHang, "Sổ chi tiết khách hàng", "Các phát sinh công nợ của khách hàng.");
            UiStyler.SetAccessibleText(dgvSanPham, "Sổ chi tiết sản phẩm", "Doanh số bán theo sản phẩm.");
            UiStyler.SetAccessibleText(dgvMatHang, "Mặt hàng hóa đơn", "Danh sách mặt hàng của hóa đơn.");
            UiStyler.SetAccessibleText(dgvPhieuThu, "Phiếu thu liên kết", "Các phiếu thu liên kết với hóa đơn.");
            UiStyler.SetAccessibleText(dgvDinhKhoan, "Định khoản liên kết", "Các bút toán liên kết với hóa đơn.");

            ApplyTuoiNoStyles();
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

            // Grid MatHang
            dgvMatHang.Columns.Clear();
            dgvMatHang.AutoGenerateColumns = false;
            dgvMatHang.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgvMatHang.Columns.Add(new DataGridViewTextBoxColumn { Name = "MaSP", HeaderText = "Mã SP", AutoSizeMode = DataGridViewAutoSizeColumnMode.None, Width = 95, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter } });
            dgvMatHang.Columns.Add(new DataGridViewTextBoxColumn { Name = "TenSP", HeaderText = "Tên Sản Phẩm", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill, FillWeight = 180F, MinimumWidth = 180, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleLeft } });
            dgvMatHang.Columns.Add(new DataGridViewTextBoxColumn { Name = "DonViTinh", HeaderText = "ĐVT", AutoSizeMode = DataGridViewAutoSizeColumnMode.None, Width = 65, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter } });
            dgvMatHang.Columns.Add(new DataGridViewTextBoxColumn { Name = "SoLuong", HeaderText = "Số Lượng", AutoSizeMode = DataGridViewAutoSizeColumnMode.None, Width = 90, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N0" } });
            dgvMatHang.Columns.Add(new DataGridViewTextBoxColumn { Name = "DonGia", HeaderText = "Đơn Giá", AutoSizeMode = DataGridViewAutoSizeColumnMode.None, Width = 120, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N0" } });
            dgvMatHang.Columns.Add(new DataGridViewTextBoxColumn { Name = "GiamGia", HeaderText = "Giảm Giá (%)", AutoSizeMode = DataGridViewAutoSizeColumnMode.None, Width = 80, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N0" } });
            dgvMatHang.Columns.Add(new DataGridViewTextBoxColumn { Name = "ThanhTien", HeaderText = "Thành Tiền", AutoSizeMode = DataGridViewAutoSizeColumnMode.None, Width = 135, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N0", Font = new Font("Segoe UI", 9F, FontStyle.Bold) } });

            // Grid PhieuThu
            dgvPhieuThu.Columns.Clear();
            dgvPhieuThu.AutoGenerateColumns = false;
            dgvPhieuThu.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgvPhieuThu.Columns.Add(new DataGridViewTextBoxColumn { Name = "MaPT", HeaderText = "Mã Phiếu Thu", AutoSizeMode = DataGridViewAutoSizeColumnMode.None, Width = 100, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter } });
            dgvPhieuThu.Columns.Add(new DataGridViewTextBoxColumn { Name = "NgayThu", HeaderText = "Ngày Thu", AutoSizeMode = DataGridViewAutoSizeColumnMode.None, Width = 140, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter, Format = "dd/MM/yyyy HH:mm" } });
            dgvPhieuThu.Columns.Add(new DataGridViewTextBoxColumn { Name = "NguoiNop", HeaderText = "Người Nộp", AutoSizeMode = DataGridViewAutoSizeColumnMode.None, Width = 140, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleLeft } });
            dgvPhieuThu.Columns.Add(new DataGridViewTextBoxColumn { Name = "SoTien", HeaderText = "Số Tiền Đã Thu", AutoSizeMode = DataGridViewAutoSizeColumnMode.None, Width = 135, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N0", ForeColor = Color.FromArgb(39, 174, 96), Font = new Font("Segoe UI", 9F, FontStyle.Bold) } });
            dgvPhieuThu.Columns.Add(new DataGridViewTextBoxColumn { Name = "HinhThuc", HeaderText = "Hình Thức", AutoSizeMode = DataGridViewAutoSizeColumnMode.None, Width = 100, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter } });
            dgvPhieuThu.Columns.Add(new DataGridViewTextBoxColumn { Name = "LyDoThu", HeaderText = "Lý Do Thu", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill, FillWeight = 180F, MinimumWidth = 160, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleLeft } });

            // Grid DinhKhoan
            dgvDinhKhoan.Columns.Clear();
            dgvDinhKhoan.AutoGenerateColumns = false;
            dgvDinhKhoan.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgvDinhKhoan.Columns.Add(new DataGridViewTextBoxColumn { Name = "MaCT", HeaderText = "Mã Chứng Từ", AutoSizeMode = DataGridViewAutoSizeColumnMode.None, Width = 100, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter } });
            dgvDinhKhoan.Columns.Add(new DataGridViewTextBoxColumn { Name = "NgayLap", HeaderText = "Ngày Hạch Toán", AutoSizeMode = DataGridViewAutoSizeColumnMode.None, Width = 140, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter, Format = "dd/MM/yyyy HH:mm" } });
            dgvDinhKhoan.Columns.Add(new DataGridViewTextBoxColumn { Name = "STT", HeaderText = "STT", AutoSizeMode = DataGridViewAutoSizeColumnMode.None, Width = 45, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter } });
            dgvDinhKhoan.Columns.Add(new DataGridViewTextBoxColumn { Name = "TaiKhoanNo", HeaderText = "TK Nợ", AutoSizeMode = DataGridViewAutoSizeColumnMode.None, Width = 85, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter, Font = new Font("Segoe UI", 9F, FontStyle.Bold) } });
            dgvDinhKhoan.Columns.Add(new DataGridViewTextBoxColumn { Name = "TaiKhoanCo", HeaderText = "TK Có", AutoSizeMode = DataGridViewAutoSizeColumnMode.None, Width = 85, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter, Font = new Font("Segoe UI", 9F, FontStyle.Bold) } });
            dgvDinhKhoan.Columns.Add(new DataGridViewTextBoxColumn { Name = "SoTien", HeaderText = "Số Tiền", AutoSizeMode = DataGridViewAutoSizeColumnMode.None, Width = 135, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N0" } });
            dgvDinhKhoan.Columns.Add(new DataGridViewTextBoxColumn { Name = "DienGiai", HeaderText = "Nội Dung Diễn Giải", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill, FillWeight = 180F, MinimumWidth = 180, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleLeft } });

            InitTuoiNoGridColumns();
        }
    }
}
