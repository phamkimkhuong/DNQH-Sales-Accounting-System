namespace DNQH_KeToanBanHang.Forms
{
    partial class frmHoaDonBan
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblSubTitle = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.tcHoaDon = new System.Windows.Forms.TabControl();
            this.tpLapHoaDon = new System.Windows.Forms.TabPage();
            this.dgvChiTiet = new System.Windows.Forms.DataGridView();
            this.colMaSP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTenSP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDonViTinh = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSoLuong = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDonGia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colGiamGia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colThanhTien = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlTongTien = new System.Windows.Forms.Panel();
            this.btnDong = new System.Windows.Forms.Button();
            this.btnLamMoi = new System.Windows.Forms.Button();
            this.btnLapHoaDon = new System.Windows.Forms.Button();
            this.btnLapPhieuThuTuHDB = new System.Windows.Forms.Button();
            this.btnLapPhieuXuatTuHDB = new System.Windows.Forms.Button();
            this.lblTongTien = new System.Windows.Forms.Label();
            this.grpThongTin = new System.Windows.Forms.GroupBox();
            this.txtGhiChu = new System.Windows.Forms.TextBox();
            this.lblGhiChu = new System.Windows.Forms.Label();
            this.cboTrangThai = new System.Windows.Forms.ComboBox();
            this.lblTrangThai = new System.Windows.Forms.Label();
            this.lblKhachHangInfo = new System.Windows.Forms.Label();
            this.lblKHTag = new System.Windows.Forms.Label();
            this.lblNhanVienLap = new System.Windows.Forms.Label();
            this.lblNVTag = new System.Windows.Forms.Label();
            this.dtpNgayLap = new System.Windows.Forms.DateTimePicker();
            this.lblNgayLap = new System.Windows.Forms.Label();
            this.cboDonDatHang = new System.Windows.Forms.ComboBox();
            this.lblDonDatHang = new System.Windows.Forms.Label();
            this.txtMaHDB = new System.Windows.Forms.TextBox();
            this.lblMaHDB = new System.Windows.Forms.Label();
            this.tpDanhSach = new System.Windows.Forms.TabPage();
            this.dgvDanhSachHoaDon = new System.Windows.Forms.DataGridView();
            this.colDSMaHDB = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDSNgayLap = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDSMaDDH = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDSKhachHang = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDSNhanVien = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDSTongTien = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDSTrangThai = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDSGhiChu = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlFilterDanhSach = new System.Windows.Forms.Panel();
            this.lblTimKiem = new System.Windows.Forms.Label();
            this.txtTimKiem = new System.Windows.Forms.TextBox();
            this.lblFromDate = new System.Windows.Forms.Label();
            this.dtpFromDate = new System.Windows.Forms.DateTimePicker();
            this.lblToDate = new System.Windows.Forms.Label();
            this.dtpToDate = new System.Windows.Forms.DateTimePicker();
            this.lblTrangThaiLoc = new System.Windows.Forms.Label();
            this.cboTrangThaiLoc = new System.Windows.Forms.ComboBox();
            this.btnTimKiem = new System.Windows.Forms.Button();
            this.btnLamMoiDanhSach = new System.Windows.Forms.Button();
            this.lblSoHoaDon = new System.Windows.Forms.Label();
            this.pnlHeader.SuspendLayout();
            this.tcHoaDon.SuspendLayout();
            this.tpLapHoaDon.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTiet)).BeginInit();
            this.pnlTongTien.SuspendLayout();
            this.grpThongTin.SuspendLayout();
            this.tpDanhSach.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDanhSachHoaDon)).BeginInit();
            this.pnlFilterDanhSach.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(58)))), ((int)(((byte)(237)))));
            this.pnlHeader.Controls.Add(this.lblSubTitle);
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1080, 60);
            this.pnlHeader.TabIndex = 0;
            // 
            // lblSubTitle
            // 
            this.lblSubTitle.AutoSize = true;
            this.lblSubTitle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSubTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(213)))), ((int)(((byte)(255)))));
            this.lblSubTitle.Location = new System.Drawing.Point(20, 34);
            this.lblSubTitle.Name = "lblSubTitle";
            this.lblSubTitle.Size = new System.Drawing.Size(437, 15);
            this.lblSubTitle.TabIndex = 1;
            this.lblSubTitle.Text = "Phát hành hóa đơn bán hàng từ đơn đặt hàng hợp lệ (Ràng buộc duy nhất 1 Đơn - 1 H" +
    "óa đơn)";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(18, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(306, 25);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "QUẢN LÝ HÓA ĐƠN BÁN HÀNG";
            // 
            // tcHoaDon
            // 
            this.tcHoaDon.Controls.Add(this.tpLapHoaDon);
            this.tcHoaDon.Controls.Add(this.tpDanhSach);
            this.tcHoaDon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcHoaDon.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.tcHoaDon.Location = new System.Drawing.Point(0, 60);
            this.tcHoaDon.Name = "tcHoaDon";
            this.tcHoaDon.SelectedIndex = 0;
            this.tcHoaDon.Size = new System.Drawing.Size(1080, 640);
            this.tcHoaDon.TabIndex = 1;
            // 
            // tpLapHoaDon
            // 
            this.tpLapHoaDon.BackColor = System.Drawing.Color.White;
            this.tpLapHoaDon.Controls.Add(this.dgvChiTiet);
            this.tpLapHoaDon.Controls.Add(this.pnlTongTien);
            this.tpLapHoaDon.Controls.Add(this.grpThongTin);
            this.tpLapHoaDon.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.tpLapHoaDon.Location = new System.Drawing.Point(4, 26);
            this.tpLapHoaDon.Name = "tpLapHoaDon";
            this.tpLapHoaDon.Padding = new System.Windows.Forms.Padding(10);
            this.tpLapHoaDon.Size = new System.Drawing.Size(1072, 610);
            this.tpLapHoaDon.TabIndex = 0;
            this.tpLapHoaDon.Text = "  Lập Hóa Đơn Bán Mới  ";
            // 
            // dgvChiTiet
            // 
            this.dgvChiTiet.AllowUserToAddRows = false;
            this.dgvChiTiet.AllowUserToDeleteRows = false;
            this.dgvChiTiet.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvChiTiet.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(240)))), ((int)(((byte)(248)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(40)))), ((int)(((byte)(50)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvChiTiet.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvChiTiet.ColumnHeadersHeight = 30;
            this.dgvChiTiet.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colMaSP,
            this.colTenSP,
            this.colDonViTinh,
            this.colSoLuong,
            this.colDonGia,
            this.colGiamGia,
            this.colThanhTien});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(235)))), ((int)(((byte)(252)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvChiTiet.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvChiTiet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvChiTiet.EnableHeadersVisualStyles = false;
            this.dgvChiTiet.Location = new System.Drawing.Point(10, 160);
            this.dgvChiTiet.MultiSelect = false;
            this.dgvChiTiet.Name = "dgvChiTiet";
            this.dgvChiTiet.ReadOnly = true;
            this.dgvChiTiet.RowHeadersVisible = false;
            this.dgvChiTiet.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvChiTiet.Size = new System.Drawing.Size(1052, 395);
            this.dgvChiTiet.TabIndex = 1;
            // 
            // colMaSP
            // 
            this.colMaSP.DataPropertyName = "MaSP";
            this.colMaSP.FillWeight = 60F;
            this.colMaSP.HeaderText = "Mã SP";
            this.colMaSP.Name = "colMaSP";
            this.colMaSP.ReadOnly = true;
            // 
            // colTenSP
            // 
            this.colTenSP.DataPropertyName = "TenSP";
            this.colTenSP.FillWeight = 160F;
            this.colTenSP.HeaderText = "Tên Sản Phẩm";
            this.colTenSP.Name = "colTenSP";
            this.colTenSP.ReadOnly = true;
            // 
            // colDonViTinh
            // 
            this.colDonViTinh.DataPropertyName = "DonViTinh";
            this.colDonViTinh.FillWeight = 45F;
            this.colDonViTinh.HeaderText = "ĐVT";
            this.colDonViTinh.Name = "colDonViTinh";
            this.colDonViTinh.ReadOnly = true;
            // 
            // colSoLuong
            // 
            this.colSoLuong.DataPropertyName = "SoLuong";
            this.colSoLuong.FillWeight = 50F;
            this.colSoLuong.HeaderText = "Số Lượng";
            this.colSoLuong.Name = "colSoLuong";
            this.colSoLuong.ReadOnly = true;
            // 
            // colDonGia
            // 
            this.colDonGia.DataPropertyName = "DonGia";
            this.colDonGia.FillWeight = 75F;
            this.colDonGia.HeaderText = "Đơn Giá (VNĐ)";
            this.colDonGia.Name = "colDonGia";
            this.colDonGia.ReadOnly = true;
            // 
            // colGiamGia
            // 
            this.colGiamGia.DataPropertyName = "GiamGia";
            this.colGiamGia.FillWeight = 50F;
            this.colGiamGia.HeaderText = "Giảm (%)";
            this.colGiamGia.Name = "colGiamGia";
            this.colGiamGia.ReadOnly = true;
            // 
            // colThanhTien
            // 
            this.colThanhTien.DataPropertyName = "ThanhTien";
            this.colThanhTien.FillWeight = 85F;
            this.colThanhTien.HeaderText = "Thành Tiền (VNĐ)";
            this.colThanhTien.Name = "colThanhTien";
            this.colThanhTien.ReadOnly = true;
            // 
            // pnlTongTien
            // 
            this.pnlTongTien.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(245)))), ((int)(((byte)(252)))));
            this.pnlTongTien.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlTongTien.Controls.Add(this.btnDong);
            this.pnlTongTien.Controls.Add(this.btnLamMoi);
            this.pnlTongTien.Controls.Add(this.btnLapHoaDon);
            this.pnlTongTien.Controls.Add(this.lblTongTien);
            this.pnlTongTien.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlTongTien.Location = new System.Drawing.Point(10, 555);
            this.pnlTongTien.Name = "pnlTongTien";
            this.pnlTongTien.Size = new System.Drawing.Size(1052, 45);
            this.pnlTongTien.TabIndex = 2;
            // 
            // btnDong
            // 
            this.btnDong.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDong.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.btnDong.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDong.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDong.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnDong.ForeColor = System.Drawing.Color.White;
            this.btnDong.Location = new System.Drawing.Point(945, 6);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(95, 32);
            this.btnDong.TabIndex = 3;
            this.btnDong.Text = "Đóng";
            this.btnDong.UseVisualStyleBackColor = false;
            this.btnDong.Click += new System.EventHandler(this.btnDong_Click);
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLamMoi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(162)))), ((int)(((byte)(184)))));
            this.btnLamMoi.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLamMoi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLamMoi.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnLamMoi.ForeColor = System.Drawing.Color.White;
            this.btnLamMoi.Location = new System.Drawing.Point(825, 6);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(110, 32);
            this.btnLamMoi.TabIndex = 2;
            this.btnLamMoi.Text = "Làm Mới";
            this.btnLamMoi.UseVisualStyleBackColor = false;
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);
            // 
            // btnLapHoaDon
            // 
            this.btnLapHoaDon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLapHoaDon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnLapHoaDon.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLapHoaDon.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLapHoaDon.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnLapHoaDon.ForeColor = System.Drawing.Color.White;
            this.btnLapHoaDon.Location = new System.Drawing.Point(660, 6);
            this.btnLapHoaDon.Name = "btnLapHoaDon";
            this.btnLapHoaDon.Size = new System.Drawing.Size(155, 32);
            this.btnLapHoaDon.TabIndex = 1;
            this.btnLapHoaDon.Text = "LẬP HÓA ĐƠN";
            this.btnLapHoaDon.UseVisualStyleBackColor = false;
            this.btnLapHoaDon.Click += new System.EventHandler(this.btnLapHoaDon_Click);
            // 
            // lblTongTien
            // 
            this.lblTongTien.AutoSize = true;
            this.lblTongTien.Font = new System.Drawing.Font("Segoe UI", 11.5F, System.Drawing.FontStyle.Bold);
            this.lblTongTien.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.lblTongTien.Location = new System.Drawing.Point(15, 11);
            this.lblTongTien.Name = "lblTongTien";
            this.lblTongTien.Size = new System.Drawing.Size(203, 21);
            this.lblTongTien.TabIndex = 0;
            this.lblTongTien.Text = "TỔNG HÓA ĐƠN: 0 VNĐ";
            // 
            // grpThongTin
            // 
            this.grpThongTin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.grpThongTin.Controls.Add(this.txtGhiChu);
            this.grpThongTin.Controls.Add(this.lblGhiChu);
            this.grpThongTin.Controls.Add(this.cboTrangThai);
            this.grpThongTin.Controls.Add(this.lblTrangThai);
            this.grpThongTin.Controls.Add(this.lblKhachHangInfo);
            this.grpThongTin.Controls.Add(this.lblKHTag);
            this.grpThongTin.Controls.Add(this.lblNhanVienLap);
            this.grpThongTin.Controls.Add(this.lblNVTag);
            this.grpThongTin.Controls.Add(this.dtpNgayLap);
            this.grpThongTin.Controls.Add(this.lblNgayLap);
            this.grpThongTin.Controls.Add(this.cboDonDatHang);
            this.grpThongTin.Controls.Add(this.lblDonDatHang);
            this.grpThongTin.Controls.Add(this.txtMaHDB);
            this.grpThongTin.Controls.Add(this.lblMaHDB);
            this.grpThongTin.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpThongTin.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.grpThongTin.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(50)))), ((int)(((byte)(80)))));
            this.grpThongTin.Location = new System.Drawing.Point(10, 10);
            this.grpThongTin.Name = "grpThongTin";
            this.grpThongTin.Size = new System.Drawing.Size(1052, 150);
            this.grpThongTin.TabIndex = 0;
            this.grpThongTin.TabStop = false;
            this.grpThongTin.Text = " THÔNG TIN HÓA ĐƠN BÁN ";
            // 
            // txtGhiChu
            // 
            this.txtGhiChu.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtGhiChu.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtGhiChu.Location = new System.Drawing.Point(530, 110);
            this.txtGhiChu.Name = "txtGhiChu";
            this.txtGhiChu.Size = new System.Drawing.Size(495, 24);
            this.txtGhiChu.TabIndex = 13;
            // 
            // lblGhiChu
            // 
            this.lblGhiChu.AutoSize = true;
            this.lblGhiChu.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblGhiChu.Location = new System.Drawing.Point(460, 114);
            this.lblGhiChu.Name = "lblGhiChu";
            this.lblGhiChu.Size = new System.Drawing.Size(51, 15);
            this.lblGhiChu.TabIndex = 12;
            this.lblGhiChu.Text = "Ghi chú:";
            // 
            // cboTrangThai
            // 
            this.cboTrangThai.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTrangThai.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.cboTrangThai.FormattingEnabled = true;
            this.cboTrangThai.Items.AddRange(new object[] {
            "Chưa thanh toán",
            "Đã thanh toán"});
            this.cboTrangThai.Location = new System.Drawing.Point(120, 110);
            this.cboTrangThai.Name = "cboTrangThai";
            this.cboTrangThai.Size = new System.Drawing.Size(180, 24);
            this.cboTrangThai.TabIndex = 11;
            // 
            // lblTrangThai
            // 
            this.lblTrangThai.AutoSize = true;
            this.lblTrangThai.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblTrangThai.Location = new System.Drawing.Point(15, 114);
            this.lblTrangThai.Name = "lblTrangThai";
            this.lblTrangThai.Size = new System.Drawing.Size(64, 15);
            this.lblTrangThai.TabIndex = 10;
            this.lblTrangThai.Text = "Trạng thái:";
            // 
            // lblKhachHangInfo
            // 
            this.lblKhachHangInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblKhachHangInfo.AutoEllipsis = true;
            this.lblKhachHangInfo.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblKhachHangInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(110)))), ((int)(((byte)(253)))));
            this.lblKhachHangInfo.Location = new System.Drawing.Point(530, 71);
            this.lblKhachHangInfo.Name = "lblKhachHangInfo";
            this.lblKhachHangInfo.Size = new System.Drawing.Size(495, 24);
            this.lblKhachHangInfo.TabIndex = 9;
            this.lblKhachHangInfo.Text = "(Tự động nạp theo đơn đặt)";
            // 
            // lblKHTag
            // 
            this.lblKHTag.AutoSize = true;
            this.lblKHTag.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblKHTag.Location = new System.Drawing.Point(440, 71);
            this.lblKHTag.Name = "lblKHTag";
            this.lblKHTag.Size = new System.Drawing.Size(73, 15);
            this.lblKHTag.TabIndex = 8;
            this.lblKHTag.Text = "Khách hàng:";
            // 
            // lblNhanVienLap
            // 
            this.lblNhanVienLap.AutoSize = true;
            this.lblNhanVienLap.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblNhanVienLap.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(100)))), ((int)(((byte)(160)))));
            this.lblNhanVienLap.Location = new System.Drawing.Point(530, 29);
            this.lblNhanVienLap.Name = "lblNhanVienLap";
            this.lblNhanVienLap.Size = new System.Drawing.Size(126, 15);
            this.lblNhanVienLap.TabIndex = 5;
            this.lblNhanVienLap.Text = "Nhân viên đăng nhập";
            // 
            // lblNVTag
            // 
            this.lblNVTag.AutoSize = true;
            this.lblNVTag.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblNVTag.Location = new System.Drawing.Point(440, 29);
            this.lblNVTag.Name = "lblNVTag";
            this.lblNVTag.Size = new System.Drawing.Size(84, 15);
            this.lblNVTag.TabIndex = 4;
            this.lblNVTag.Text = "Nhân viên lập:";
            // 
            // dtpNgayLap
            // 
            this.dtpNgayLap.CustomFormat = "dd/MM/yyyy HH:mm";
            this.dtpNgayLap.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.dtpNgayLap.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpNgayLap.Location = new System.Drawing.Point(860, 25);
            this.dtpNgayLap.Name = "dtpNgayLap";
            this.dtpNgayLap.Size = new System.Drawing.Size(165, 24);
            this.dtpNgayLap.TabIndex = 7;
            // 
            // lblNgayLap
            // 
            this.lblNgayLap.AutoSize = true;
            this.lblNgayLap.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblNgayLap.Location = new System.Drawing.Point(795, 29);
            this.lblNgayLap.Name = "lblNgayLap";
            this.lblNgayLap.Size = new System.Drawing.Size(58, 15);
            this.lblNgayLap.TabIndex = 6;
            this.lblNgayLap.Text = "Ngày lập:";
            // 
            // cboDonDatHang
            // 
            this.cboDonDatHang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDonDatHang.DropDownWidth = 650;
            this.cboDonDatHang.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.cboDonDatHang.FormattingEnabled = true;
            this.cboDonDatHang.Location = new System.Drawing.Point(120, 67);
            this.cboDonDatHang.Name = "cboDonDatHang";
            this.cboDonDatHang.Size = new System.Drawing.Size(295, 24);
            this.cboDonDatHang.TabIndex = 3;
            this.cboDonDatHang.SelectedIndexChanged += new System.EventHandler(this.cboDonDatHang_SelectedIndexChanged);
            // 
            // lblDonDatHang
            // 
            this.lblDonDatHang.AutoSize = true;
            this.lblDonDatHang.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblDonDatHang.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.lblDonDatHang.Location = new System.Drawing.Point(15, 71);
            this.lblDonDatHang.Name = "lblDonDatHang";
            this.lblDonDatHang.Size = new System.Drawing.Size(87, 15);
            this.lblDonDatHang.TabIndex = 2;
            this.lblDonDatHang.Text = "Chọn đơn đặt:";
            // 
            // txtMaHDB
            // 
            this.txtMaHDB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(245)))), ((int)(((byte)(255)))));
            this.txtMaHDB.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.txtMaHDB.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(70)))), ((int)(((byte)(150)))));
            this.txtMaHDB.Location = new System.Drawing.Point(120, 25);
            this.txtMaHDB.Name = "txtMaHDB";
            this.txtMaHDB.ReadOnly = true;
            this.txtMaHDB.Size = new System.Drawing.Size(180, 24);
            this.txtMaHDB.TabIndex = 1;
            // 
            // lblMaHDB
            // 
            this.lblMaHDB.AutoSize = true;
            this.lblMaHDB.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblMaHDB.Location = new System.Drawing.Point(15, 29);
            this.lblMaHDB.Name = "lblMaHDB";
            this.lblMaHDB.Size = new System.Drawing.Size(55, 15);
            this.lblMaHDB.TabIndex = 0;
            this.lblMaHDB.Text = "Mã HDB:";
            // 
            // tpDanhSach
            // 
            this.tpDanhSach.BackColor = System.Drawing.Color.White;
            this.tpDanhSach.Controls.Add(this.dgvDanhSachHoaDon);
            this.tpDanhSach.Controls.Add(this.pnlFilterDanhSach);
            this.tpDanhSach.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.tpDanhSach.Location = new System.Drawing.Point(4, 26);
            this.tpDanhSach.Name = "tpDanhSach";
            this.tpDanhSach.Padding = new System.Windows.Forms.Padding(10);
            this.tpDanhSach.Size = new System.Drawing.Size(1072, 610);
            this.tpDanhSach.TabIndex = 1;
            this.tpDanhSach.Text = "  Danh Sách Hóa Đơn Đã Phát Hành  ";
            // 
            // dgvDanhSachHoaDon
            // 
            this.dgvDanhSachHoaDon.AllowUserToAddRows = false;
            this.dgvDanhSachHoaDon.AllowUserToDeleteRows = false;
            this.dgvDanhSachHoaDon.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDanhSachHoaDon.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(240)))), ((int)(((byte)(248)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(40)))), ((int)(((byte)(50)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvDanhSachHoaDon.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvDanhSachHoaDon.ColumnHeadersHeight = 32;
            this.dgvDanhSachHoaDon.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colDSMaHDB,
            this.colDSNgayLap,
            this.colDSMaDDH,
            this.colDSKhachHang,
            this.colDSNhanVien,
            this.colDSTongTien,
            this.colDSTrangThai,
            this.colDSGhiChu});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(235)))), ((int)(((byte)(252)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvDanhSachHoaDon.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgvDanhSachHoaDon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDanhSachHoaDon.EnableHeadersVisualStyles = false;
            this.dgvDanhSachHoaDon.Location = new System.Drawing.Point(10, 50);
            this.dgvDanhSachHoaDon.MultiSelect = false;
            this.dgvDanhSachHoaDon.Name = "dgvDanhSachHoaDon";
            this.dgvDanhSachHoaDon.ReadOnly = true;
            this.dgvDanhSachHoaDon.RowHeadersVisible = false;
            this.dgvDanhSachHoaDon.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDanhSachHoaDon.Size = new System.Drawing.Size(1052, 550);
            this.dgvDanhSachHoaDon.TabIndex = 1;
            this.dgvDanhSachHoaDon.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDanhSachHoaDon_CellDoubleClick);
            // 
            // colDSMaHDB
            // 
            this.colDSMaHDB.DataPropertyName = "MaHDB";
            this.colDSMaHDB.FillWeight = 70F;
            this.colDSMaHDB.HeaderText = "Mã HDB";
            this.colDSMaHDB.Name = "colDSMaHDB";
            this.colDSMaHDB.ReadOnly = true;
            // 
            // colDSNgayLap
            // 
            this.colDSNgayLap.DataPropertyName = "NgayLap";
            this.colDSNgayLap.FillWeight = 75F;
            this.colDSNgayLap.HeaderText = "Ngày Lập";
            this.colDSNgayLap.Name = "colDSNgayLap";
            this.colDSNgayLap.ReadOnly = true;
            // 
            // colDSMaDDH
            // 
            this.colDSMaDDH.DataPropertyName = "MaDDH";
            this.colDSMaDDH.FillWeight = 65F;
            this.colDSMaDDH.HeaderText = "Mã Đơn Đặt";
            this.colDSMaDDH.Name = "colDSMaDDH";
            this.colDSMaDDH.ReadOnly = true;
            // 
            // colDSKhachHang
            // 
            this.colDSKhachHang.DataPropertyName = "TenKH";
            this.colDSKhachHang.FillWeight = 130F;
            this.colDSKhachHang.HeaderText = "Khách Hàng";
            this.colDSKhachHang.Name = "colDSKhachHang";
            this.colDSKhachHang.ReadOnly = true;
            // 
            // colDSNhanVien
            // 
            this.colDSNhanVien.DataPropertyName = "TenNV";
            this.colDSNhanVien.FillWeight = 100F;
            this.colDSNhanVien.HeaderText = "Nhân Viên Lập";
            this.colDSNhanVien.Name = "colDSNhanVien";
            this.colDSNhanVien.ReadOnly = true;
            // 
            // colDSTongTien
            // 
            this.colDSTongTien.DataPropertyName = "TongTien";
            this.colDSTongTien.FillWeight = 85F;
            this.colDSTongTien.HeaderText = "Tổng Tiền (VNĐ)";
            this.colDSTongTien.Name = "colDSTongTien";
            this.colDSTongTien.ReadOnly = true;
            // 
            // colDSTrangThai
            // 
            this.colDSTrangThai.DataPropertyName = "TrangThai";
            this.colDSTrangThai.FillWeight = 70F;
            this.colDSTrangThai.HeaderText = "Trạng Thái";
            this.colDSTrangThai.Name = "colDSTrangThai";
            this.colDSTrangThai.ReadOnly = true;
            // 
            // colDSGhiChu
            // 
            this.colDSGhiChu.DataPropertyName = "GhiChu";
            this.colDSGhiChu.FillWeight = 110F;
            this.colDSGhiChu.HeaderText = "Ghi Chú";
            this.colDSGhiChu.Name = "colDSGhiChu";
            this.colDSGhiChu.ReadOnly = true;
            // 
            // pnlFilterDanhSach
            // 
            this.pnlFilterDanhSach.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(248)))), ((int)(((byte)(252)))));
            this.pnlFilterDanhSach.Controls.Add(this.lblTimKiem);
            this.pnlFilterDanhSach.Controls.Add(this.txtTimKiem);
            this.pnlFilterDanhSach.Controls.Add(this.lblFromDate);
            this.pnlFilterDanhSach.Controls.Add(this.dtpFromDate);
            this.pnlFilterDanhSach.Controls.Add(this.lblToDate);
            this.pnlFilterDanhSach.Controls.Add(this.dtpToDate);
            this.pnlFilterDanhSach.Controls.Add(this.lblTrangThaiLoc);
            this.pnlFilterDanhSach.Controls.Add(this.cboTrangThaiLoc);
            this.pnlFilterDanhSach.Controls.Add(this.btnTimKiem);
            this.pnlFilterDanhSach.Controls.Add(this.btnLamMoiDanhSach);
            this.pnlFilterDanhSach.Controls.Add(this.btnLapPhieuThuTuHDB);
            this.pnlFilterDanhSach.Controls.Add(this.btnLapPhieuXuatTuHDB);
            this.pnlFilterDanhSach.Controls.Add(this.lblSoHoaDon);
            this.pnlFilterDanhSach.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFilterDanhSach.Location = new System.Drawing.Point(10, 10);
            this.pnlFilterDanhSach.Name = "pnlFilterDanhSach";
            this.pnlFilterDanhSach.Size = new System.Drawing.Size(1052, 40);
            this.pnlFilterDanhSach.TabIndex = 0;
            // 
            // lblTimKiem
            // 
            this.lblTimKiem.AutoSize = true;
            this.lblTimKiem.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblTimKiem.Location = new System.Drawing.Point(0, 0);
            this.lblTimKiem.Name = "lblTimKiem";
            this.lblTimKiem.Size = new System.Drawing.Size(59, 15);
            this.lblTimKiem.TabIndex = 4;
            this.lblTimKiem.Text = "Tìm kiếm:";
            // 
            // txtTimKiem
            // 
            this.txtTimKiem.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtTimKiem.Location = new System.Drawing.Point(0, 0);
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.Size = new System.Drawing.Size(180, 23);
            this.txtTimKiem.TabIndex = 5;
            // 
            // lblFromDate
            // 
            this.lblFromDate.AutoSize = true;
            this.lblFromDate.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblFromDate.Location = new System.Drawing.Point(0, 0);
            this.lblFromDate.Name = "lblFromDate";
            this.lblFromDate.Size = new System.Drawing.Size(52, 15);
            this.lblFromDate.TabIndex = 6;
            this.lblFromDate.Text = "Từ ngày:";
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFromDate.Location = new System.Drawing.Point(0, 0);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.Size = new System.Drawing.Size(115, 23);
            this.dtpFromDate.TabIndex = 7;
            // 
            // lblToDate
            // 
            this.lblToDate.AutoSize = true;
            this.lblToDate.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblToDate.Location = new System.Drawing.Point(0, 0);
            this.lblToDate.Name = "lblToDate";
            this.lblToDate.Size = new System.Drawing.Size(60, 15);
            this.lblToDate.TabIndex = 8;
            this.lblToDate.Text = "Đến ngày:";
            // 
            // dtpToDate
            // 
            this.dtpToDate.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpToDate.Location = new System.Drawing.Point(0, 0);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.Size = new System.Drawing.Size(115, 23);
            this.dtpToDate.TabIndex = 9;
            // 
            // lblTrangThaiLoc
            // 
            this.lblTrangThaiLoc.AutoSize = true;
            this.lblTrangThaiLoc.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblTrangThaiLoc.Location = new System.Drawing.Point(0, 0);
            this.lblTrangThaiLoc.Name = "lblTrangThaiLoc";
            this.lblTrangThaiLoc.Size = new System.Drawing.Size(62, 15);
            this.lblTrangThaiLoc.TabIndex = 10;
            this.lblTrangThaiLoc.Text = "Trạng thái:";
            // 
            // cboTrangThaiLoc
            // 
            this.cboTrangThaiLoc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTrangThaiLoc.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cboTrangThaiLoc.Location = new System.Drawing.Point(0, 0);
            this.cboTrangThaiLoc.Name = "cboTrangThaiLoc";
            this.cboTrangThaiLoc.Size = new System.Drawing.Size(130, 23);
            this.cboTrangThaiLoc.TabIndex = 11;
            // 
            // btnTimKiem
            // 
            this.btnTimKiem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnTimKiem.Location = new System.Drawing.Point(0, 0);
            this.btnTimKiem.Name = "btnTimKiem";
            this.btnTimKiem.Size = new System.Drawing.Size(85, 28);
            this.btnTimKiem.TabIndex = 12;
            this.btnTimKiem.Text = "Tìm Kiếm";
            this.btnTimKiem.UseVisualStyleBackColor = true;
            // 
            // btnLapPhieuThuTuHDB
            // 
            this.btnLapPhieuThuTuHDB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLapPhieuThuTuHDB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(185)))), ((int)(((byte)(129)))));
            this.btnLapPhieuThuTuHDB.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLapPhieuThuTuHDB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLapPhieuThuTuHDB.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnLapPhieuThuTuHDB.ForeColor = System.Drawing.Color.White;
            this.btnLapPhieuThuTuHDB.Location = new System.Drawing.Point(660, 6);
            this.btnLapPhieuThuTuHDB.Name = "btnLapPhieuThuTuHDB";
            this.btnLapPhieuThuTuHDB.Size = new System.Drawing.Size(140, 28);
            this.btnLapPhieuThuTuHDB.TabIndex = 2;
            this.btnLapPhieuThuTuHDB.Text = "💰 Lập Phiếu Thu";
            this.btnLapPhieuThuTuHDB.UseVisualStyleBackColor = false;
            this.btnLapPhieuThuTuHDB.Click += new System.EventHandler(this.btnLapPhieuThuTuHDB_Click);
            // 
            // btnLapPhieuXuatTuHDB
            // 
            this.btnLapPhieuXuatTuHDB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLapPhieuXuatTuHDB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(165)))), ((int)(((byte)(233)))));
            this.btnLapPhieuXuatTuHDB.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLapPhieuXuatTuHDB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLapPhieuXuatTuHDB.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnLapPhieuXuatTuHDB.ForeColor = System.Drawing.Color.White;
            this.btnLapPhieuXuatTuHDB.Location = new System.Drawing.Point(808, 6);
            this.btnLapPhieuXuatTuHDB.Name = "btnLapPhieuXuatTuHDB";
            this.btnLapPhieuXuatTuHDB.Size = new System.Drawing.Size(134, 28);
            this.btnLapPhieuXuatTuHDB.TabIndex = 3;
            this.btnLapPhieuXuatTuHDB.Text = "📦 Xuất Kho";
            this.btnLapPhieuXuatTuHDB.UseVisualStyleBackColor = false;
            this.btnLapPhieuXuatTuHDB.Click += new System.EventHandler(this.btnLapPhieuXuatTuHDB_Click);
            // 
            // btnLamMoiDanhSach
            // 
            this.btnLamMoiDanhSach.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLamMoiDanhSach.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(110)))), ((int)(((byte)(253)))));
            this.btnLamMoiDanhSach.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLamMoiDanhSach.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLamMoiDanhSach.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnLamMoiDanhSach.ForeColor = System.Drawing.Color.White;
            this.btnLamMoiDanhSach.Location = new System.Drawing.Point(950, 6);
            this.btnLamMoiDanhSach.Name = "btnLamMoiDanhSach";
            this.btnLamMoiDanhSach.Size = new System.Drawing.Size(90, 28);
            this.btnLamMoiDanhSach.TabIndex = 1;
            this.btnLamMoiDanhSach.Text = "Tải Lại";
            this.btnLamMoiDanhSach.UseVisualStyleBackColor = false;
            this.btnLamMoiDanhSach.Click += new System.EventHandler(this.btnLamMoiDanhSach_Click);
            // 
            // lblSoHoaDon
            // 
            this.lblSoHoaDon.AutoSize = true;
            this.lblSoHoaDon.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblSoHoaDon.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(60)))), ((int)(((byte)(90)))));
            this.lblSoHoaDon.Location = new System.Drawing.Point(10, 11);
            this.lblSoHoaDon.Name = "lblSoHoaDon";
            this.lblSoHoaDon.Size = new System.Drawing.Size(188, 17);
            this.lblSoHoaDon.TabIndex = 0;
            this.lblSoHoaDon.Text = "Tổng số hóa đơn phát hành: 0";
            // 
            // frmHoaDonBan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1080, 700);
            this.Controls.Add(this.tcHoaDon);
            this.Controls.Add(this.pnlHeader);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "frmHoaDonBan";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Quản Lý Hóa Đơn Bán Hàng";
            this.Load += new System.EventHandler(this.frmHoaDonBan_Load);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.tcHoaDon.ResumeLayout(false);
            this.tpLapHoaDon.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTiet)).EndInit();
            this.pnlTongTien.ResumeLayout(false);
            this.pnlTongTien.PerformLayout();
            this.grpThongTin.ResumeLayout(false);
            this.grpThongTin.PerformLayout();
            this.tpDanhSach.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDanhSachHoaDon)).EndInit();
            this.pnlFilterDanhSach.ResumeLayout(false);
            this.pnlFilterDanhSach.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblSubTitle;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TabControl tcHoaDon;
        private System.Windows.Forms.TabPage tpLapHoaDon;
        private System.Windows.Forms.GroupBox grpThongTin;
        private System.Windows.Forms.Label lblMaHDB;
        private System.Windows.Forms.TextBox txtMaHDB;
        private System.Windows.Forms.Label lblDonDatHang;
        private System.Windows.Forms.ComboBox cboDonDatHang;
        private System.Windows.Forms.Label lblNgayLap;
        private System.Windows.Forms.DateTimePicker dtpNgayLap;
        private System.Windows.Forms.Label lblNVTag;
        private System.Windows.Forms.Label lblNhanVienLap;
        private System.Windows.Forms.Label lblKHTag;
        private System.Windows.Forms.Label lblKhachHangInfo;
        private System.Windows.Forms.Label lblTrangThai;
        private System.Windows.Forms.ComboBox cboTrangThai;
        private System.Windows.Forms.Label lblGhiChu;
        private System.Windows.Forms.TextBox txtGhiChu;
        private System.Windows.Forms.DataGridView dgvChiTiet;
        private System.Windows.Forms.Panel pnlTongTien;
        private System.Windows.Forms.Label lblTongTien;
        private System.Windows.Forms.Button btnLapHoaDon;
        private System.Windows.Forms.Button btnLamMoi;
        private System.Windows.Forms.Button btnDong;
        private System.Windows.Forms.TabPage tpDanhSach;
        private System.Windows.Forms.DataGridView dgvDanhSachHoaDon;
        private System.Windows.Forms.Panel pnlFilterDanhSach;
        private System.Windows.Forms.Label lblSoHoaDon;
        private System.Windows.Forms.Button btnLamMoiDanhSach;
        private System.Windows.Forms.Button btnLapPhieuThuTuHDB;
        private System.Windows.Forms.Button btnLapPhieuXuatTuHDB;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMaSP;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTenSP;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDonViTinh;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSoLuong;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDonGia;
        private System.Windows.Forms.DataGridViewTextBoxColumn colGiamGia;
        private System.Windows.Forms.DataGridViewTextBoxColumn colThanhTien;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDSMaHDB;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDSNgayLap;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDSMaDDH;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDSKhachHang;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDSNhanVien;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDSTongTien;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDSTrangThai;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDSGhiChu;
        private System.Windows.Forms.Label lblTimKiem;
        private System.Windows.Forms.TextBox txtTimKiem;
        private System.Windows.Forms.Label lblFromDate;
        private System.Windows.Forms.DateTimePicker dtpFromDate;
        private System.Windows.Forms.Label lblToDate;
        private System.Windows.Forms.DateTimePicker dtpToDate;
        private System.Windows.Forms.Label lblTrangThaiLoc;
        private System.Windows.Forms.ComboBox cboTrangThaiLoc;
        private System.Windows.Forms.Button btnTimKiem;
    }
}
