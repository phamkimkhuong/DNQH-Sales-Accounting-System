using System;
using System.Drawing;
using System.Windows.Forms;
using DNQH_KeToanBanHang.Helpers;

namespace DNQH_KeToanBanHang.Forms
{
    public partial class frmBaoCaoTongHop
    {
        private void BuildResponsiveLayout()
        {
            SuspendLayout();
            MinimumSize = new Size(920, 680);
            BuildPageHeader();
            BuildKpiBanner();

            DateTimePicker[] dates = { dtpTuNgayDT, dtpDenNgayDT, dtpTuNgayTC, dtpDenNgayTC };
            foreach (DateTimePicker picker in dates)
                picker.MinimumSize = new Size(130, UiTheme.ControlHeight);
            cboKho.MinimumSize = new Size(240, UiTheme.ControlHeight);

            _doanhThuStatus = UiLayoutBuilder.BuildReportPage(
                tabDoanhThu,
                pnlFilterDT,
                new Control[] { lblTuNgayDT, dtpTuNgayDT, lblDenNgayDT, dtpDenNgayDT, btnXemDoanhThu, btnXuatCsvDoanhThu, btnInDoanhThu },
                dgvDoanhThu,
                pnlSummaryDT,
                new Control[] { lblDTTongDoanhThu, lblDTSoHoaDon },
                "Chọn khoảng thời gian để xem doanh thu bán hàng.");

            _thuChiStatus = UiLayoutBuilder.BuildReportPage(
                tabThuChi,
                pnlFilterTC,
                new Control[] { lblTuNgayTC, dtpTuNgayTC, lblDenNgayTC, dtpDenNgayTC, btnXemThuChi, btnXuatCsvThuChi, btnInThuChi },
                dgvThuChi,
                pnlSummaryTC,
                new Control[] { lblTCTongThu, lblTCTongChi, lblTCChenhLech },
                "Chọn khoảng thời gian để xem thu, chi và chênh lệch.");

            _tonKhoStatus = UiLayoutBuilder.BuildReportPage(
                tabTonKho,
                pnlFilterTK,
                new Control[] { lblChonKho, cboKho, btnXemTonKho, btnXuatCsvTonKho, btnInTonKho },
                dgvTonKho,
                pnlSummaryTK,
                new Control[] { lblTKTongTon, lblTKTongGiaTri },
                "Chọn kho hoặc xem tồn kho trên toàn hệ thống.");

            Controls.Clear();
            TableLayoutPanel root = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 3,
                BackColor = UiTheme.Canvas,
                Padding = new Padding(UiTheme.PagePadding)
            };
            root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            root.RowStyles.Add(new RowStyle(SizeType.Absolute, 66F));
            root.RowStyles.Add(new RowStyle(SizeType.Absolute, 86F));
            root.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            pnlHeader.Dock = DockStyle.Fill;
            pnlHeader.Margin = new Padding(0, 0, 0, 8);
            pnlKpiBanner.Dock = DockStyle.Fill;
            pnlKpiBanner.Margin = new Padding(0, 0, 0, 10);
            tabControlMain.Dock = DockStyle.Fill;
            tabControlMain.Margin = new Padding(0);
            root.Controls.Add(pnlHeader, 0, 0);
            root.Controls.Add(pnlKpiBanner, 0, 1);
            root.Controls.Add(tabControlMain, 0, 2);
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

        private void BuildKpiBanner()
        {
            pnlKpiBanner.Controls.Clear();
            pnlKpiBanner.Padding = new Padding(0);
            TableLayoutPanel cards = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 5,
                RowCount = 1
            };
            for (int i = 0; i < 5; i++)
                cards.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            cards.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            Panel[] panels = { pnlKpi1, pnlKpi2, pnlKpi3, pnlKpi4, pnlKpi5 };
            Label[] titles = { lblKpi1Title, lblKpi2Title, lblKpi3Title, lblKpi4Title, lblKpi5Title };
            Label[] values = { lblKpi1Value, lblKpi2Value, lblKpi3Value, lblKpi4Value, lblKpi5Value };
            for (int i = 0; i < panels.Length; i++)
            {
                ConfigureKpiCard(panels[i], titles[i], values[i]);
                panels[i].Margin = new Padding(i == 0 ? 0 : 4, 0, i == panels.Length - 1 ? 0 : 4, 0);
                cards.Controls.Add(panels[i], i, 0);
            }
            pnlKpiBanner.Controls.Add(cards);
        }

        private void ConfigureKpiCard(Panel panel, Label title, Label value)
        {
            panel.Controls.Clear();
            panel.Dock = DockStyle.Fill;
            panel.BorderStyle = BorderStyle.FixedSingle;
            panel.Padding = new Padding(10, 7, 8, 6);
            TableLayoutPanel content = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 2
            };
            content.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            content.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
            content.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            title.Dock = DockStyle.Fill;
            title.AutoEllipsis = true;
            title.TextAlign = ContentAlignment.MiddleLeft;
            value.Dock = DockStyle.Fill;
            value.AutoEllipsis = true;
            value.TextAlign = ContentAlignment.MiddleLeft;
            content.Controls.Add(title, 0, 0);
            content.Controls.Add(value, 0, 1);
            panel.Controls.Add(content);
        }

        private void ApplyFoundationDesign()
        {
            UiStyler.Apply(this);
            if (!SystemInformation.HighContrast)
            {
                pnlHeader.BackColor = UiTheme.Canvas;
                lblTitle.ForeColor = UiTheme.Primary;
                lblSubTitle.ForeColor = UiTheme.TextSecondary;
                pnlKpiBanner.BackColor = UiTheme.Canvas;
                Panel[] cards = { pnlKpi1, pnlKpi2, pnlKpi3, pnlKpi4, pnlKpi5 };
                foreach (Panel card in cards)
                    card.BackColor = UiTheme.Surface;
                pnlFilterDT.BackColor = UiTheme.Surface;
                pnlFilterTC.BackColor = UiTheme.Surface;
                pnlFilterTK.BackColor = UiTheme.Surface;
                pnlSummaryDT.BackColor = UiTheme.SurfaceMuted;
                pnlSummaryTC.BackColor = UiTheme.SurfaceMuted;
                pnlSummaryTK.BackColor = UiTheme.SurfaceMuted;
                lblKpi1Value.ForeColor = UiTheme.Information;
                lblKpi2Value.ForeColor = UiTheme.Success;
                lblKpi3Value.ForeColor = UiTheme.Danger;
                lblKpi4Value.ForeColor = UiTheme.Primary;
                lblKpi5Value.ForeColor = UiTheme.Warning;
                lblDTTongDoanhThu.ForeColor = UiTheme.Success;
                lblTCTongThu.ForeColor = UiTheme.Success;
                lblTCTongChi.ForeColor = UiTheme.Danger;
                lblTKTongGiaTri.ForeColor = UiTheme.Warning;
            }
            Button[] viewButtons = { btnXemDoanhThu, btnXemThuChi, btnXemTonKho };
            foreach (Button button in viewButtons)
                UiStyler.StyleButton(button, UiButtonRole.Primary);
            Button[] exportButtons = { btnXuatCsvDoanhThu, btnXuatCsvThuChi, btnXuatCsvTonKho };
            foreach (Button button in exportButtons)
                UiStyler.StyleButton(button, UiButtonRole.Success);
            Button[] printButtons = { btnInDoanhThu, btnInThuChi, btnInTonKho };
            foreach (Button button in printButtons)
                UiStyler.StyleButton(button, UiButtonRole.Secondary);
            UiStyler.StyleStatusLabel(_doanhThuStatus, UiStatusKind.Neutral, "Chọn khoảng thời gian để xem doanh thu bán hàng.");
            UiStyler.StyleStatusLabel(_thuChiStatus, UiStatusKind.Neutral, "Chọn khoảng thời gian để xem thu, chi và chênh lệch.");
            UiStyler.StyleStatusLabel(_tonKhoStatus, UiStatusKind.Neutral, "Chọn kho hoặc xem tồn kho trên toàn hệ thống.");

            UiStyler.SetAccessibleText(dgvDoanhThu, "Báo cáo doanh thu", "Danh sách hóa đơn và doanh thu bán hàng.");
            UiStyler.SetAccessibleText(dgvThuChi, "Báo cáo thu chi", "Danh sách các giao dịch thu và chi.");
            UiStyler.SetAccessibleText(cboKho, "Kho hàng", "Chọn kho cần xem báo cáo tồn kho.");
            UiStyler.SetAccessibleText(dgvTonKho, "Báo cáo tồn kho", "Số lượng và giá trị tồn kho theo sản phẩm.");
        }

        private void InitGridColumns()
        {
            // Grid Doanh Thu
            dgvDoanhThu.Columns.Clear();
            dgvDoanhThu.AutoGenerateColumns = false;
            dgvDoanhThu.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgvDoanhThu.Columns.Add(new DataGridViewTextBoxColumn { Name = "MaHDB", HeaderText = "Mã Hóa Đơn", AutoSizeMode = DataGridViewAutoSizeColumnMode.None, Width = 110, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter } });
            dgvDoanhThu.Columns.Add(new DataGridViewTextBoxColumn { Name = "NgayLap", HeaderText = "Ngày Lập HĐ", AutoSizeMode = DataGridViewAutoSizeColumnMode.None, Width = 140, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter, Format = "dd/MM/yyyy HH:mm" } });
            dgvDoanhThu.Columns.Add(new DataGridViewTextBoxColumn { Name = "TenKH", HeaderText = "Khách Hàng", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill, FillWeight = 160F, MinimumWidth = 180, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleLeft } });
            dgvDoanhThu.Columns.Add(new DataGridViewTextBoxColumn { Name = "TenNV", HeaderText = "Nhân Viên Lập", AutoSizeMode = DataGridViewAutoSizeColumnMode.None, Width = 140, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleLeft } });
            dgvDoanhThu.Columns.Add(new DataGridViewTextBoxColumn { Name = "TongTien", HeaderText = "Tổng Tiền (VNĐ)", AutoSizeMode = DataGridViewAutoSizeColumnMode.None, Width = 135, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N0", ForeColor = Color.FromArgb(39, 174, 96), Font = new Font("Segoe UI", 9F, FontStyle.Bold) } });
            dgvDoanhThu.Columns.Add(new DataGridViewTextBoxColumn { Name = "TrangThai", HeaderText = "Trạng Thái", AutoSizeMode = DataGridViewAutoSizeColumnMode.None, Width = 120, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter } });
            dgvDoanhThu.Columns.Add(new DataGridViewTextBoxColumn { Name = "SoMatHang", HeaderText = "Số Mặt Hàng", AutoSizeMode = DataGridViewAutoSizeColumnMode.None, Width = 90, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N0" } });
            dgvDoanhThu.Columns.Add(new DataGridViewTextBoxColumn { Name = "GhiChu", HeaderText = "Ghi Chú", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill, FillWeight = 180F, MinimumWidth = 160, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleLeft } });

            // Grid Thu Chi
            dgvThuChi.Columns.Clear();
            dgvThuChi.AutoGenerateColumns = false;
            dgvThuChi.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgvThuChi.Columns.Add(new DataGridViewTextBoxColumn { Name = "NgayGiaoDich", HeaderText = "Ngày GD", AutoSizeMode = DataGridViewAutoSizeColumnMode.None, Width = 140, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter, Format = "dd/MM/yyyy HH:mm" } });
            dgvThuChi.Columns.Add(new DataGridViewTextBoxColumn { Name = "LoaiGiaoDich", HeaderText = "Loại Thu/Chi", AutoSizeMode = DataGridViewAutoSizeColumnMode.None, Width = 110, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter, Font = new Font("Segoe UI", 9F, FontStyle.Bold) } });
            dgvThuChi.Columns.Add(new DataGridViewTextBoxColumn { Name = "MaChungTu", HeaderText = "Mã Phiếu", AutoSizeMode = DataGridViewAutoSizeColumnMode.None, Width = 110, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter } });
            dgvThuChi.Columns.Add(new DataGridViewTextBoxColumn { Name = "NguoiGiaoDich", HeaderText = "Người Nộp / Nhận", AutoSizeMode = DataGridViewAutoSizeColumnMode.None, Width = 140, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleLeft } });
            dgvThuChi.Columns.Add(new DataGridViewTextBoxColumn { Name = "SoTienThu", HeaderText = "Số Tiền Thu", AutoSizeMode = DataGridViewAutoSizeColumnMode.None, Width = 130, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N0", ForeColor = Color.FromArgb(39, 174, 96), Font = new Font("Segoe UI", 9F, FontStyle.Bold) } });
            dgvThuChi.Columns.Add(new DataGridViewTextBoxColumn { Name = "SoTienChi", HeaderText = "Số Tiền Chi", AutoSizeMode = DataGridViewAutoSizeColumnMode.None, Width = 130, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N0", ForeColor = Color.FromArgb(192, 57, 43), Font = new Font("Segoe UI", 9F, FontStyle.Bold) } });
            dgvThuChi.Columns.Add(new DataGridViewTextBoxColumn { Name = "HinhThuc", HeaderText = "Hình Thức", AutoSizeMode = DataGridViewAutoSizeColumnMode.None, Width = 100, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter } });
            dgvThuChi.Columns.Add(new DataGridViewTextBoxColumn { Name = "LyDo", HeaderText = "Lý Do Thu/Chi", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill, FillWeight = 180F, MinimumWidth = 180, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleLeft } });
            dgvThuChi.Columns.Add(new DataGridViewTextBoxColumn { Name = "TenNV", HeaderText = "Nhân Viên", AutoSizeMode = DataGridViewAutoSizeColumnMode.None, Width = 140, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleLeft } });
            dgvThuChi.Columns.Add(new DataGridViewTextBoxColumn { Name = "MaHDBLienKet", HeaderText = "Mã HĐ Liên Kết", AutoSizeMode = DataGridViewAutoSizeColumnMode.None, Width = 110, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter } });

            // Grid Ton Kho
            dgvTonKho.Columns.Clear();
            dgvTonKho.AutoGenerateColumns = false;
            dgvTonKho.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgvTonKho.Columns.Add(new DataGridViewTextBoxColumn { Name = "TenKho", HeaderText = "Kho Hàng", AutoSizeMode = DataGridViewAutoSizeColumnMode.None, Width = 150, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleLeft } });
            dgvTonKho.Columns.Add(new DataGridViewTextBoxColumn { Name = "MaSP", HeaderText = "Mã Sản Phẩm", AutoSizeMode = DataGridViewAutoSizeColumnMode.None, Width = 95, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter } });
            dgvTonKho.Columns.Add(new DataGridViewTextBoxColumn { Name = "TenSP", HeaderText = "Tên Sản Phẩm", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill, FillWeight = 180F, MinimumWidth = 180, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleLeft } });
            dgvTonKho.Columns.Add(new DataGridViewTextBoxColumn { Name = "TenLoaiSP", HeaderText = "Loại Sản Phẩm", AutoSizeMode = DataGridViewAutoSizeColumnMode.None, Width = 140, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleLeft } });
            dgvTonKho.Columns.Add(new DataGridViewTextBoxColumn { Name = "DonViTinh", HeaderText = "ĐVT", AutoSizeMode = DataGridViewAutoSizeColumnMode.None, Width = 65, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter } });
            dgvTonKho.Columns.Add(new DataGridViewTextBoxColumn { Name = "SoLuongTon", HeaderText = "Số Lượng Tồn", AutoSizeMode = DataGridViewAutoSizeColumnMode.None, Width = 95, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N0", Font = new Font("Segoe UI", 9F, FontStyle.Bold) } });
            dgvTonKho.Columns.Add(new DataGridViewTextBoxColumn { Name = "DonGia", HeaderText = "Đơn Giá Bán", AutoSizeMode = DataGridViewAutoSizeColumnMode.None, Width = 120, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N0" } });
            dgvTonKho.Columns.Add(new DataGridViewTextBoxColumn { Name = "GiaTriTon", HeaderText = "Ước Tính Giá Trị", AutoSizeMode = DataGridViewAutoSizeColumnMode.None, Width = 135, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N0", ForeColor = Color.FromArgb(211, 84, 0), Font = new Font("Segoe UI", 9F, FontStyle.Bold) } });
            dgvTonKho.Columns.Add(new DataGridViewTextBoxColumn { Name = "NgayCapNhat", HeaderText = "Ngày Cập Nhật", AutoSizeMode = DataGridViewAutoSizeColumnMode.None, Width = 140, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter, Format = "dd/MM/yyyy HH:mm" } });
        }
    }
}
