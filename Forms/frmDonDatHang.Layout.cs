using System;
using System.Drawing;
using System.Windows.Forms;
using DNQH_KeToanBanHang.Constants;
using DNQH_KeToanBanHang.Helpers;

namespace DNQH_KeToanBanHang.Forms
{
    public partial class frmDonDatHang
    {
        private void BuildResponsiveLayout()
        {
            SuspendLayout();
            MinimumSize = new Size(760, 640);
            tpLapDon.AutoScroll = true;
            BuildOrderInformationLayout(true);
            BuildProductEntryLayout(true);
            grpThongTinChung.SizeChanged += grpThongTinChung_SizeChanged;
            grpSanPham.SizeChanged += grpSanPham_SizeChanged;

            txtTimKiem.MinimumSize = new Size(180, UiTheme.ControlHeight);
            dtpFromDate.MinimumSize = new Size(115, UiTheme.ControlHeight);
            dtpToDate.MinimumSize = new Size(115, UiTheme.ControlHeight);
            cboTrangThaiLoc.MinimumSize = new Size(130, UiTheme.ControlHeight);
            Button btnXuatCsv = CsvExporter.CreateExportButton(dgvDanhSachDon, "DanhSachDonDatHang", "DANH SÁCH ĐƠN ĐẶT HÀNG");

            btnInDonHangDS = UiStyler.CreateButton("🖨️ In đơn hàng", UiTheme.Secondary);
            btnInDonHangDS.Click += delegate { InDonHangDangChon(); };

            cmsDonHang = new ContextMenuStrip();
            ToolStripMenuItem miIn = new ToolStripMenuItem("🖨️ In đơn đặt hàng...");
            miIn.Click += delegate { InDonHangDangChon(); };
            cmsDonHang.Items.Add(miIn);
            dgvDanhSachDon.ContextMenuStrip = cmsDonHang;

            UiLayoutBuilder.BuildHistoryPage(
                tpDanhSach,
                pnlFilterDanhSach,
                new Control[] { lblTimKiem, txtTimKiem, lblFromDate, dtpFromDate, lblToDate, dtpToDate, lblTrangThaiLoc, cboTrangThaiLoc, btnTimKiem, btnLamMoiDanhSach, btnInDonHangDS, btnLapHoaDonTuDon, btnXuatCsv },
                dgvDanhSachDon,
                _pagerDonDatHang,
                38);

            ResumeLayout(true);
        }

        private void grpThongTinChung_SizeChanged(object sender, EventArgs e)
        {
            BuildOrderInformationLayout(false);
        }

        private void grpSanPham_SizeChanged(object sender, EventArgs e)
        {
            BuildProductEntryLayout(false);
        }

        private void BuildOrderInformationLayout(bool force)
        {
            bool isWide = grpThongTinChung.ClientSize.Width >= WideLayoutBreakpoint;
            if (!force && _isWideOrderLayout.HasValue && _isWideOrderLayout.Value == isWide)
            {
                return;
            }

            if (lblCanhBaoCongNo == null)
            {
                lblCanhBaoCongNo = new Label
                {
                    Name = "lblCanhBaoCongNo",
                    AutoSize = false,
                    Dock = DockStyle.Fill,
                    Height = 26,
                    TextAlign = ContentAlignment.MiddleLeft,
                    Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                    Padding = new Padding(8, 2, 8, 2),
                    Margin = new Padding(0, 2, 0, 4),
                    Visible = false
                };
            }

            _isWideOrderLayout = isWide;
            grpThongTinChung.SuspendLayout();
            grpThongTinChung.Controls.Clear();
            grpThongTinChung.Height = isWide ? 176 : 220;
            grpThongTinChung.Padding = new Padding(12, 10, 12, 10);

            TableLayoutPanel layout = CreateVerticalLayout(isWide ? 4 : 5);
            if (isWide)
            {
                layout.Controls.Add(BuildWideOrderIdentityRow(), 0, 0);
                layout.Controls.Add(BuildWideOrderScheduleRow(), 0, 1);
                layout.Controls.Add(lblCanhBaoCongNo, 0, 2);
                layout.Controls.Add(BuildFullWidthRow(lblGhiChu, txtGhiChu, 78), 0, 3);
            }
            else
            {
                layout.Controls.Add(BuildCompactOrderIdentityRow(), 0, 0);
                layout.Controls.Add(BuildCompactCustomerRow(), 0, 1);
                layout.Controls.Add(lblCanhBaoCongNo, 0, 2);
                layout.Controls.Add(BuildCompactScheduleRow(), 0, 3);
                layout.Controls.Add(BuildFullWidthRow(lblGhiChu, txtGhiChu, 90), 0, 4);
            }

            grpThongTinChung.Controls.Add(layout);
            grpThongTinChung.ResumeLayout(true);
        }

        private void BuildProductEntryLayout(bool force)
        {
            int layoutMode = grpSanPham.ClientSize.Width >= WideLayoutBreakpoint
                ? 2
                : grpSanPham.ClientSize.Width >= 850 ? 1 : 0;
            if (!force && _productLayoutMode.HasValue && _productLayoutMode.Value == layoutMode)
            {
                return;
            }

            _productLayoutMode = layoutMode;
            bool isWide = layoutMode == 2;
            bool isNarrow = layoutMode == 0;
            grpSanPham.SuspendLayout();
            grpSanPham.Controls.Clear();
            grpSanPham.Height = isWide ? 124 : isNarrow ? 226 : 150;
            grpSanPham.Padding = new Padding(12, 10, 12, 10);

            TableLayoutPanel layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = isNarrow ? 1 : 2,
                RowCount = isNarrow ? 2 : 1,
                Padding = new Padding(0, 4, 0, 0)
            };
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            if (!isNarrow)
            {
                layout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 146F));
            }
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            if (isNarrow)
            {
                layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 46F));
            }

            TableLayoutPanel fields = CreateVerticalLayout(isWide ? 2 : isNarrow ? 4 : 3);
            if (isWide)
            {
                fields.Controls.Add(BuildWideProductSelectionRow(), 0, 0);
                fields.Controls.Add(BuildWideProductValueRow(), 0, 1);
            }
            else if (isNarrow)
            {
                fields.Controls.Add(BuildCompactProductSelectionRow(), 0, 0);
                fields.Controls.Add(BuildCompactProductPricingRow(), 0, 1);
                fields.Controls.Add(BuildNarrowProductQuantityRow(), 0, 2);
                fields.Controls.Add(BuildNarrowProductTotalRow(), 0, 3);
            }
            else
            {
                fields.Controls.Add(BuildCompactProductSelectionRow(), 0, 0);
                fields.Controls.Add(BuildCompactProductPricingRow(), 0, 1);
                fields.Controls.Add(BuildCompactProductQuantityRow(), 0, 2);
            }

            FlowLayoutPanel actions = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = isNarrow ? FlowDirection.RightToLeft : FlowDirection.TopDown,
                WrapContents = false,
                Padding = isNarrow ? new Padding(0, 4, 0, 0) : new Padding(8, 0, 0, 0)
            };
            btnThemChiTiet.Width = 132;
            btnXoaChiTiet.Width = 132;
            btnThemChiTiet.Margin = isNarrow ? new Padding(8, 0, 0, 0) : new Padding(0, 0, 0, 8);
            btnXoaChiTiet.Margin = new Padding(0);
            actions.Controls.Add(btnThemChiTiet);
            actions.Controls.Add(btnXoaChiTiet);
            layout.Controls.Add(fields, 0, 0);
            layout.Controls.Add(actions, isNarrow ? 0 : 1, isNarrow ? 1 : 0);

            grpSanPham.Controls.Add(layout);
            grpSanPham.ResumeLayout(true);
        }

        private TableLayoutPanel BuildWideOrderIdentityRow()
        {
            TableLayoutPanel row = CreateRow(
                90, UiLayoutBuilder.GetFieldWidth(UiFieldSize.Code),
                90, UiLayoutBuilder.GetFieldWidth(UiFieldSize.Wide),
                90, UiLayoutBuilder.GetFieldWidth(UiFieldSize.Selection), -1);
            AddRowField(row, lblMaDDH, txtMaDDH, 0);
            AddRowField(row, lblNVTag, lblNhanVienLap, 2);
            AddRowField(row, lblTrangThai, cboTrangThai, 4);
            return row;
        }

        private TableLayoutPanel BuildWideOrderScheduleRow()
        {
            TableLayoutPanel row = CreateRow(
                90, UiLayoutBuilder.GetFieldWidth(UiFieldSize.Wide),
                90, UiLayoutBuilder.GetFieldWidth(UiFieldSize.DateTime),
                90, UiLayoutBuilder.GetFieldWidth(UiFieldSize.Date), -1);
            AddRowField(row, lblKhachHang, cboKhachHang, 0);
            AddRowField(row, lblNgayDat, dtpNgayDat, 2);
            AddRowField(row, lblNgayGiao, dtpNgayGiao, 4);
            return row;
        }

        private TableLayoutPanel BuildCompactOrderIdentityRow()
        {
            TableLayoutPanel row = CreateRow(90, UiLayoutBuilder.GetFieldWidth(UiFieldSize.Code), 98, -1);
            AddRowField(row, lblMaDDH, txtMaDDH, 0);
            AddRowField(row, lblNVTag, lblNhanVienLap, 2);
            return row;
        }

        private TableLayoutPanel BuildCompactCustomerRow()
        {
            TableLayoutPanel row = CreateRow(90, -1, 90, UiLayoutBuilder.GetFieldWidth(UiFieldSize.Selection));
            AddRowField(row, lblKhachHang, cboKhachHang, 0);
            AddRowField(row, lblTrangThai, cboTrangThai, 2);
            return row;
        }

        private TableLayoutPanel BuildCompactScheduleRow()
        {
            TableLayoutPanel row = CreateRow(90, UiLayoutBuilder.GetFieldWidth(UiFieldSize.DateTime), 98, UiLayoutBuilder.GetFieldWidth(UiFieldSize.Date), -1);
            AddRowField(row, lblNgayDat, dtpNgayDat, 0);
            AddRowField(row, lblNgayGiao, dtpNgayGiao, 2);
            return row;
        }

        private TableLayoutPanel BuildWideProductSelectionRow()
        {
            TableLayoutPanel row = CreateRow(
                72, UiLayoutBuilder.GetFieldWidth(UiFieldSize.Wide),
                44, UiLayoutBuilder.GetFieldWidth(UiFieldSize.ShortText), 230, -1);
            AddRowField(row, lblSanPham, cboSanPham, 0);
            AddRowField(row, lblDVT, txtDonViTinh, 2);
            AddStandaloneControl(row, lblTonKhoKhaDung, 4);
            return row;
        }

        private TableLayoutPanel BuildWideProductValueRow()
        {
            TableLayoutPanel row = CreateRow(
                72, UiLayoutBuilder.GetFieldWidth(UiFieldSize.Money),
                76, UiLayoutBuilder.GetFieldWidth(UiFieldSize.Number),
                72, UiLayoutBuilder.GetFieldWidth(UiFieldSize.Number),
                88, UiLayoutBuilder.GetFieldWidth(UiFieldSize.Money), -1);
            AddRowField(row, lblDonGia, txtDonGiaBan, 0);
            AddRowField(row, lblSoLuong, nudSoLuong, 2);
            AddRowField(row, lblGiamGia, nudGiamGia, 4);
            AddRowField(row, lblThanhTienPreview, txtThanhTienPreview, 6);
            return row;
        }

        private TableLayoutPanel BuildCompactProductSelectionRow()
        {
            TableLayoutPanel row = CreateRow(72, -1, 44, UiLayoutBuilder.GetFieldWidth(UiFieldSize.ShortText));
            AddRowField(row, lblSanPham, cboSanPham, 0);
            AddRowField(row, lblDVT, txtDonViTinh, 2);
            return row;
        }

        private TableLayoutPanel BuildCompactProductPricingRow()
        {
            TableLayoutPanel row = CreateRow(72, UiLayoutBuilder.GetFieldWidth(UiFieldSize.Money), 230, -1);
            AddRowField(row, lblDonGia, txtDonGiaBan, 0);
            AddStandaloneControl(row, lblTonKhoKhaDung, 2);
            return row;
        }

        private TableLayoutPanel BuildCompactProductQuantityRow()
        {
            TableLayoutPanel row = CreateRow(
                72, UiLayoutBuilder.GetFieldWidth(UiFieldSize.Number),
                72, UiLayoutBuilder.GetFieldWidth(UiFieldSize.Number),
                88, UiLayoutBuilder.GetFieldWidth(UiFieldSize.Money), -1);
            AddRowField(row, lblSoLuong, nudSoLuong, 0);
            AddRowField(row, lblGiamGia, nudGiamGia, 2);
            AddRowField(row, lblThanhTienPreview, txtThanhTienPreview, 4);
            return row;
        }

        private TableLayoutPanel BuildNarrowProductQuantityRow()
        {
            TableLayoutPanel row = CreateRow(
                72, UiLayoutBuilder.GetFieldWidth(UiFieldSize.Number),
                72, UiLayoutBuilder.GetFieldWidth(UiFieldSize.Number), -1);
            AddRowField(row, lblSoLuong, nudSoLuong, 0);
            AddRowField(row, lblGiamGia, nudGiamGia, 2);
            return row;
        }

        private TableLayoutPanel BuildNarrowProductTotalRow()
        {
            TableLayoutPanel row = CreateRow(88, UiLayoutBuilder.GetFieldWidth(UiFieldSize.Money), -1);
            AddRowField(row, lblThanhTienPreview, txtThanhTienPreview, 0);
            return row;
        }

        private static TableLayoutPanel BuildFullWidthRow(Label label, Control field, int labelWidth)
        {
            TableLayoutPanel row = CreateRow(labelWidth, -1);
            AddRowField(row, label, field, 0);
            return row;
        }

        private static TableLayoutPanel CreateVerticalLayout(int rows)
        {
            TableLayoutPanel layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = rows,
                Margin = new Padding(0),
                Padding = new Padding(0)
            };
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            for (int row = 0; row < rows; row++)
            {
                layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F / rows));
            }
            return layout;
        }

        private static TableLayoutPanel CreateRow(params int[] widths)
        {
            TableLayoutPanel row = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = widths.Length,
                RowCount = 1,
                Margin = new Padding(0),
                Padding = new Padding(0)
            };
            row.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            for (int i = 0; i < widths.Length; i++)
            {
                row.ColumnStyles.Add(widths[i] < 0
                    ? new ColumnStyle(SizeType.Percent, 100F)
                    : new ColumnStyle(SizeType.Absolute, widths[i]));
            }
            return row;
        }

        private static void AddRowField(TableLayoutPanel row, Label label, Control field, int labelColumn)
        {
            label.AutoSize = false;
            label.Dock = DockStyle.Fill;
            label.TextAlign = ContentAlignment.MiddleRight;
            label.Margin = new Padding(0, 0, 6, 0);

            field.Dock = DockStyle.Fill;
            field.Margin = new Padding(0, 4, 14, 4);
            if (field is Label)
            {
                Label valueLabel = (Label)field;
                valueLabel.AutoSize = false;
                valueLabel.AutoEllipsis = true;
                valueLabel.TextAlign = ContentAlignment.MiddleLeft;
            }

            row.Controls.Add(label, labelColumn, 0);
            row.Controls.Add(field, labelColumn + 1, 0);
        }

        private static void AddStandaloneControl(TableLayoutPanel row, Control control, int column)
        {
            control.Anchor = AnchorStyles.Left;
            control.Margin = new Padding(0, 0, 8, 0);
            row.Controls.Add(control, column, 0);
        }

        private void ApplyFoundationDesign()
        {
            lblSubTitle.Text = "Tiếp nhận đơn hàng, kiểm tra khả dụng và theo dõi trạng thái xử lý";
            UiStyler.Apply(this);

            if (!SystemInformation.HighContrast)
            {
                pnlHeader.BackColor = UiTheme.Primary;
                lblTitle.ForeColor = Color.White;
                lblSubTitle.ForeColor = Color.FromArgb(237, 233, 254);
                tpLapDon.BackColor = UiTheme.Canvas;
                tpDanhSach.BackColor = UiTheme.Canvas;
                pnlTongTien.BackColor = UiTheme.SurfaceMuted;
                pnlFilterDanhSach.BackColor = UiTheme.SurfaceMuted;
                lblTongTien.ForeColor = UiTheme.Danger;
                lblTonKhoKhaDung.ForeColor = UiTheme.Information;
            }

            UiStyler.StyleButton(btnThemChiTiet, UiButtonRole.Primary);
            UiStyler.StyleButton(btnXoaChiTiet, UiButtonRole.Danger);
            UiStyler.StyleButton(btnLuuDon, UiButtonRole.Success);
            UiStyler.StyleButton(btnLamMoi, UiButtonRole.Secondary);
            UiStyler.StyleButton(btnDong, UiButtonRole.Secondary);

            btnInDonHangTab1 = UiStyler.CreateButton("🖨️ In đơn", Color.FromArgb(70, 80, 95));
            btnInDonHangTab1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnInDonHangTab1.Location = new Point(btnLuuDon.Left - 118, btnLuuDon.Top);
            btnInDonHangTab1.Size = new Size(110, btnLuuDon.Height);
            btnInDonHangTab1.Click += delegate
            {
                string maDDH = !string.IsNullOrEmpty(txtMaDDH.Text.Trim())
                    ? txtMaDDH.Text.Trim()
                    : _lastCreatedMaDDH;
                if (!string.IsNullOrEmpty(maDDH))
                {
                    InDonHang(maDDH);
                }
                else
                {
                    MessageBox.Show("Chưa có đơn hàng nào được chọn hoặc vừa tạo. Vui lòng lập đơn hoặc chọn từ danh sách để in.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            };
            pnlTongTien.Controls.Add(btnInDonHangTab1);

            UiStyler.StyleButton(btnTimKiem, UiButtonRole.Primary);
            UiStyler.StyleButton(btnLamMoiDanhSach, UiButtonRole.Secondary);
            UiStyler.StyleButton(btnLapHoaDonTuDon, UiButtonRole.Success);
            CsvExporter.AttachExportContextMenu(dgvDanhSachDon, "DanhSachDonDatHang", "DANH SÁCH ĐƠN ĐẶT HÀNG");

            UiStyler.SetAccessibleText(cboKhachHang, "Khách hàng đặt hàng", "Chọn khách hàng cho đơn đặt hàng.");
            UiStyler.SetAccessibleText(cboSanPham, "Sản phẩm", "Chọn sản phẩm cần thêm vào đơn.");
            UiStyler.SetAccessibleText(nudSoLuong, "Số lượng đặt", "Nhập số lượng sản phẩm cần đặt.");
            UiStyler.SetAccessibleText(nudGiamGia, "Giảm giá phần trăm", "Nhập tỷ lệ giảm giá của dòng sản phẩm.");
            UiStyler.SetAccessibleText(dgvChiTiet, "Chi tiết đơn đặt hàng", "Danh sách sản phẩm trong đơn hiện tại.");
            UiStyler.SetAccessibleText(dgvDanhSachDon, "Danh sách đơn đặt hàng", "Nhấp đúp một đơn để xem chi tiết.");

            ConfigureGridColumns();
            dgvDanhSachDon.CellFormatting += dgvDanhSachDon_CellFormatting;
            dgvChiTiet.CellFormatting += dgvChiTiet_CellFormatting;
        }

        private void ConfigureGridColumns()
        {
            // === Cấu hình lưới Danh sách Đơn Đặt Hàng ===
            dgvDanhSachDon.AutoGenerateColumns = false;
            dgvDanhSachDon.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            colDSMaDDH.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colDSMaDDH.Width = 110;
            colDSMaDDH.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            colDSNgayDat.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colDSNgayDat.Width = 140;
            colDSNgayDat.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colDSNgayDat.DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";

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

            // === Cấu hình lưới Chi Tiết Sản Phẩm Đặt ===
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

        private void dgvDanhSachDon_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            string colName = dgvDanhSachDon.Columns[e.ColumnIndex].Name;

            if (colName == "colDSTongTien" && e.Value != null)
            {
                e.CellStyle.Font = new Font(dgvDanhSachDon.Font, FontStyle.Bold);
                e.CellStyle.ForeColor = Color.FromArgb(180, 20, 20);
            }
            else if (colName == "colDSTrangThai" && e.Value != null)
            {
                string status = e.Value.ToString().Trim();
                if (string.Equals(status, OrderStatusConstants.DaLapHoaDon, StringComparison.OrdinalIgnoreCase))
                {
                    e.CellStyle.ForeColor = Color.FromArgb(16, 120, 60);
                    e.CellStyle.Font = new Font(dgvDanhSachDon.Font, FontStyle.Bold);
                }
                else if (string.Equals(status, OrderStatusConstants.DaDuyet, StringComparison.OrdinalIgnoreCase))
                {
                    e.CellStyle.ForeColor = Color.FromArgb(20, 80, 200);
                    e.CellStyle.Font = new Font(dgvDanhSachDon.Font, FontStyle.Bold);
                }
                else if (string.Equals(status, OrderStatusConstants.DaXuatKho, StringComparison.OrdinalIgnoreCase) ||
                         string.Equals(status, OrderStatusConstants.DaHoanThanh, StringComparison.OrdinalIgnoreCase))
                {
                    e.CellStyle.ForeColor = Color.FromArgb(10, 110, 140);
                    e.CellStyle.Font = new Font(dgvDanhSachDon.Font, FontStyle.Bold);
                }
                else if (OrderStatusConstants.IsPending(status))
                {
                    e.CellStyle.ForeColor = Color.FromArgb(180, 110, 10);
                    e.CellStyle.Font = new Font(dgvDanhSachDon.Font, FontStyle.Bold);
                }
                else if (OrderStatusConstants.IsCancelled(status))
                {
                    e.CellStyle.ForeColor = Color.FromArgb(200, 30, 30);
                    e.CellStyle.Font = new Font(dgvDanhSachDon.Font, FontStyle.Bold);
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
