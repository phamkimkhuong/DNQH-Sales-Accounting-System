namespace DNQH_KeToanBanHang.Forms
{
    partial class frmPhieuXuatKho
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
            this.tcPhieuXuat = new System.Windows.Forms.TabControl();
            this.tpLapPhieu = new System.Windows.Forms.TabPage();
            this.dgvChiTiet = new System.Windows.Forms.DataGridView();
            this.colMaSP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTenSP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDonViTinh = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSoLuongHDB = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSoLuongDaXuat = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSoLuongConLai = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTonKho = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSoLuongXuat = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.lblTongSoLuongXuat = new System.Windows.Forms.Label();
            this.btnDong = new System.Windows.Forms.Button();
            this.btnLamMoi = new System.Windows.Forms.Button();
            this.btnXuatKho = new System.Windows.Forms.Button();
            this.grpThongTin = new System.Windows.Forms.GroupBox();
            this.lblKhachHangInfo = new System.Windows.Forms.Label();
            this.lblKHTag = new System.Windows.Forms.Label();
            this.lblNhanVienLap = new System.Windows.Forms.Label();
            this.lblNVTag = new System.Windows.Forms.Label();
            this.txtLyDoXuat = new System.Windows.Forms.TextBox();
            this.lblLyDoXuat = new System.Windows.Forms.Label();
            this.dtpNgayXuat = new System.Windows.Forms.DateTimePicker();
            this.lblNgayXuat = new System.Windows.Forms.Label();
            this.cboKho = new System.Windows.Forms.ComboBox();
            this.lblKho = new System.Windows.Forms.Label();
            this.cboHoaDon = new System.Windows.Forms.ComboBox();
            this.lblHoaDon = new System.Windows.Forms.Label();
            this.txtMaPXK = new System.Windows.Forms.TextBox();
            this.lblMaPXK = new System.Windows.Forms.Label();
            this.tpDanhSach = new System.Windows.Forms.TabPage();
            this.dgvDanhSachPhieuXuat = new System.Windows.Forms.DataGridView();
            this.colDSMaPXK = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDSNgayXuat = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDSMaHDB = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDSKho = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDSKhachHang = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDSNhanVien = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDSLyDo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDSTrangThai = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlFilter = new System.Windows.Forms.Panel();
            this.btnLamMoiDanhSach = new System.Windows.Forms.Button();
            this.btnTimKiem = new System.Windows.Forms.Button();
            this.txtTimKiem = new System.Windows.Forms.TextBox();
            this.lblTimKiem = new System.Windows.Forms.Label();
            this.cboFilterKho = new System.Windows.Forms.ComboBox();
            this.lblFilterKho = new System.Windows.Forms.Label();
            this.lblSoPhieu = new System.Windows.Forms.Label();
            this.pnlHeader.SuspendLayout();
            this.tcPhieuXuat.SuspendLayout();
            this.tpLapPhieu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTiet)).BeginInit();
            this.pnlBottom.SuspendLayout();
            this.grpThongTin.SuspendLayout();
            this.tpDanhSach.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDanhSachPhieuXuat)).BeginInit();
            this.pnlFilter.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.pnlHeader.Controls.Add(this.lblSubTitle);
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1084, 60);
            this.pnlHeader.TabIndex = 0;
            // 
            // lblSubTitle
            // 
            this.lblSubTitle.AutoSize = true;
            this.lblSubTitle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSubTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.lblSubTitle.Location = new System.Drawing.Point(20, 34);
            this.lblSubTitle.Name = "lblSubTitle";
            this.lblSubTitle.Size = new System.Drawing.Size(512, 15);
            this.lblSubTitle.TabIndex = 1;
            this.lblSubTitle.Text = "Xuất kho hàng hóa theo Hóa đơn bán, cập nhật trừ tồn kho atomic và theo dõi lịch sử xuất hàng";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 13.5F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(18, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(262, 25);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "QUẢN LÝ PHIẾU XUẤT KHO";
            // 
            // tcPhieuXuat
            // 
            this.tcPhieuXuat.Controls.Add(this.tpLapPhieu);
            this.tcPhieuXuat.Controls.Add(this.tpDanhSach);
            this.tcPhieuXuat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcPhieuXuat.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.tcPhieuXuat.Location = new System.Drawing.Point(0, 60);
            this.tcPhieuXuat.Name = "tcPhieuXuat";
            this.tcPhieuXuat.SelectedIndex = 0;
            this.tcPhieuXuat.Size = new System.Drawing.Size(1084, 601);
            this.tcPhieuXuat.TabIndex = 1;
            // 
            // tpLapPhieu
            // 
            this.tpLapPhieu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.tpLapPhieu.Controls.Add(this.dgvChiTiet);
            this.tpLapPhieu.Controls.Add(this.pnlBottom);
            this.tpLapPhieu.Controls.Add(this.grpThongTin);
            this.tpLapPhieu.Location = new System.Drawing.Point(4, 26);
            this.tpLapPhieu.Name = "tpLapPhieu";
            this.tpLapPhieu.Padding = new System.Windows.Forms.Padding(12);
            this.tpLapPhieu.Size = new System.Drawing.Size(1076, 571);
            this.tpLapPhieu.TabIndex = 0;
            this.tpLapPhieu.Text = "  Lập Phiếu Xuất Kho  ";
            // 
            // dgvChiTiet
            // 
            this.dgvChiTiet.AllowUserToAddRows = false;
            this.dgvChiTiet.AllowUserToDeleteRows = false;
            this.dgvChiTiet.BackgroundColor = System.Drawing.Color.White;
            this.dgvChiTiet.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(245)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(37)))), ((int)(((byte)(41)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvChiTiet.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvChiTiet.ColumnHeadersHeight = 35;
            this.dgvChiTiet.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colMaSP,
            this.colTenSP,
            this.colDonViTinh,
            this.colSoLuongHDB,
            this.colSoLuongDaXuat,
            this.colSoLuongConLai,
            this.colTonKho,
            this.colSoLuongXuat});
            this.dgvChiTiet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvChiTiet.EnableHeadersVisualStyles = false;
            this.dgvChiTiet.Location = new System.Drawing.Point(12, 172);
            this.dgvChiTiet.Name = "dgvChiTiet";
            this.dgvChiTiet.RowHeadersWidth = 35;
            this.dgvChiTiet.RowTemplate.Height = 28;
            this.dgvChiTiet.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvChiTiet.Size = new System.Drawing.Size(1052, 337);
            this.dgvChiTiet.TabIndex = 1;
            this.dgvChiTiet.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvChiTiet_CellValueChanged);
            // 
            // colMaSP
            // 
            this.colMaSP.DataPropertyName = "MaSP";
            this.colMaSP.HeaderText = "Mã SP";
            this.colMaSP.Name = "colMaSP";
            this.colMaSP.ReadOnly = true;
            this.colMaSP.Width = 110;
            // 
            // colTenSP
            // 
            this.colTenSP.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colTenSP.DataPropertyName = "TenSP";
            this.colTenSP.HeaderText = "Tên Sản Phẩm";
            this.colTenSP.Name = "colTenSP";
            this.colTenSP.ReadOnly = true;
            // 
            // colDonViTinh
            // 
            this.colDonViTinh.DataPropertyName = "DonViTinh";
            this.colDonViTinh.HeaderText = "ĐVT";
            this.colDonViTinh.Name = "colDonViTinh";
            this.colDonViTinh.ReadOnly = true;
            this.colDonViTinh.Width = 70;
            // 
            // colSoLuongHDB
            // 
            this.colSoLuongHDB.DataPropertyName = "SoLuongHoaDon";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.colSoLuongHDB.DefaultCellStyle = dataGridViewCellStyle2;
            this.colSoLuongHDB.HeaderText = "SL Hóa Đơn";
            this.colSoLuongHDB.Name = "colSoLuongHDB";
            this.colSoLuongHDB.ReadOnly = true;
            this.colSoLuongHDB.Width = 100;
            // 
            // colSoLuongDaXuat
            // 
            this.colSoLuongDaXuat.DataPropertyName = "SoLuongDaXuat";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.colSoLuongDaXuat.DefaultCellStyle = dataGridViewCellStyle3;
            this.colSoLuongDaXuat.HeaderText = "Đã Xuất";
            this.colSoLuongDaXuat.Name = "colSoLuongDaXuat";
            this.colSoLuongDaXuat.ReadOnly = true;
            this.colSoLuongDaXuat.Width = 90;
            // 
            // colSoLuongConLai
            // 
            this.colSoLuongConLai.DataPropertyName = "SoLuongConLai";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.colSoLuongConLai.DefaultCellStyle = dataGridViewCellStyle4;
            this.colSoLuongConLai.HeaderText = "Cần Xuất";
            this.colSoLuongConLai.Name = "colSoLuongConLai";
            this.colSoLuongConLai.ReadOnly = true;
            this.colSoLuongConLai.Width = 90;
            // 
            // colTonKho
            // 
            this.colTonKho.DataPropertyName = "TonKhoHienTai";
            this.colTonKho.HeaderText = "Tồn Kho";
            this.colTonKho.Name = "colTonKho";
            this.colTonKho.ReadOnly = true;
            this.colTonKho.Width = 110;
            // 
            // colSoLuongXuat
            // 
            this.colSoLuongXuat.DataPropertyName = "SoLuongXuat";
            this.colSoLuongXuat.HeaderText = "SL Xuất";
            this.colSoLuongXuat.Name = "colSoLuongXuat";
            this.colSoLuongXuat.Width = 130;
            // 
            // pnlBottom
            // 
            this.pnlBottom.BackColor = System.Drawing.Color.White;
            this.pnlBottom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlBottom.Controls.Add(this.lblTongSoLuongXuat);
            this.pnlBottom.Controls.Add(this.btnDong);
            this.pnlBottom.Controls.Add(this.btnLamMoi);
            this.pnlBottom.Controls.Add(this.btnXuatKho);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(12, 509);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(1052, 50);
            this.pnlBottom.TabIndex = 2;
            // 
            // lblTongSoLuongXuat
            // 
            this.lblTongSoLuongXuat.AutoSize = true;
            this.lblTongSoLuongXuat.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTongSoLuongXuat.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(110)))), ((int)(((byte)(253)))));
            this.lblTongSoLuongXuat.Location = new System.Drawing.Point(16, 14);
            this.lblTongSoLuongXuat.Name = "lblTongSoLuongXuat";
            this.lblTongSoLuongXuat.Size = new System.Drawing.Size(182, 20);
            this.lblTongSoLuongXuat.TabIndex = 0;
            this.lblTongSoLuongXuat.Text = "TỔNG SỐ LƯỢNG XUẤT: 0";
            // 
            // btnDong
            // 
            this.btnDong.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDong.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.btnDong.FlatAppearance.BorderSize = 0;
            this.btnDong.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDong.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnDong.ForeColor = System.Drawing.Color.White;
            this.btnDong.Location = new System.Drawing.Point(945, 8);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(95, 34);
            this.btnDong.TabIndex = 3;
            this.btnDong.Text = "Đóng";
            this.btnDong.UseVisualStyleBackColor = false;
            this.btnDong.Click += new System.EventHandler(this.btnDong_Click);
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLamMoi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(110)))), ((int)(((byte)(253)))));
            this.btnLamMoi.FlatAppearance.BorderSize = 0;
            this.btnLamMoi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLamMoi.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnLamMoi.ForeColor = System.Drawing.Color.White;
            this.btnLamMoi.Location = new System.Drawing.Point(835, 8);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(100, 34);
            this.btnLamMoi.TabIndex = 2;
            this.btnLamMoi.Text = "Làm Mới";
            this.btnLamMoi.UseVisualStyleBackColor = false;
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);
            // 
            // btnXuatKho
            // 
            this.btnXuatKho.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnXuatKho.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(135)))), ((int)(((byte)(84)))));
            this.btnXuatKho.FlatAppearance.BorderSize = 0;
            this.btnXuatKho.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXuatKho.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnXuatKho.ForeColor = System.Drawing.Color.White;
            this.btnXuatKho.Location = new System.Drawing.Point(685, 8);
            this.btnXuatKho.Name = "btnXuatKho";
            this.btnXuatKho.Size = new System.Drawing.Size(140, 34);
            this.btnXuatKho.TabIndex = 1;
            this.btnXuatKho.Text = "XÁC NHẬN XUẤT";
            this.btnXuatKho.UseVisualStyleBackColor = false;
            this.btnXuatKho.Click += new System.EventHandler(this.btnXuatKho_Click);
            // 
            // grpThongTin
            // 
            this.grpThongTin.Controls.Add(this.lblKhachHangInfo);
            this.grpThongTin.Controls.Add(this.lblKHTag);
            this.grpThongTin.Controls.Add(this.lblNhanVienLap);
            this.grpThongTin.Controls.Add(this.lblNVTag);
            this.grpThongTin.Controls.Add(this.txtLyDoXuat);
            this.grpThongTin.Controls.Add(this.lblLyDoXuat);
            this.grpThongTin.Controls.Add(this.dtpNgayXuat);
            this.grpThongTin.Controls.Add(this.lblNgayXuat);
            this.grpThongTin.Controls.Add(this.cboKho);
            this.grpThongTin.Controls.Add(this.lblKho);
            this.grpThongTin.Controls.Add(this.cboHoaDon);
            this.grpThongTin.Controls.Add(this.lblHoaDon);
            this.grpThongTin.Controls.Add(this.txtMaPXK);
            this.grpThongTin.Controls.Add(this.lblMaPXK);
            this.grpThongTin.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpThongTin.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.grpThongTin.Location = new System.Drawing.Point(12, 12);
            this.grpThongTin.Name = "grpThongTin";
            this.grpThongTin.Size = new System.Drawing.Size(1052, 175);
            this.grpThongTin.TabIndex = 0;
            this.grpThongTin.TabStop = false;
            this.grpThongTin.Text = "Thông Tin Phiếu Xuất Kho";
            // 
            // lblKhachHangInfo
            // 
            this.lblKhachHangInfo.AutoSize = true;
            this.lblKhachHangInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblKhachHangInfo.AutoEllipsis = true;
            this.lblKhachHangInfo.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblKhachHangInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(110)))), ((int)(((byte)(253)))));
            this.lblKhachHangInfo.Location = new System.Drawing.Point(490, 66);
            this.lblKhachHangInfo.Name = "lblKhachHangInfo";
            this.lblKhachHangInfo.Size = new System.Drawing.Size(450, 24);
            this.lblKhachHangInfo.TabIndex = 9;
            this.lblKhachHangInfo.Text = "(Chưa chọn hóa đơn)";
            // 
            // lblKHTag
            // 
            this.lblKHTag.AutoSize = true;
            this.lblKHTag.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblKHTag.Location = new System.Drawing.Point(385, 66);
            this.lblKHTag.Name = "lblKHTag";
            this.lblKHTag.Size = new System.Drawing.Size(84, 17);
            this.lblKHTag.TabIndex = 8;
            this.lblKHTag.Text = "Khách Hàng:";
            // 
            // lblNhanVienLap
            // 
            this.lblNhanVienLap.AutoSize = true;
            this.lblNhanVienLap.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblNhanVienLap.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(135)))), ((int)(((byte)(84)))));
            this.lblNhanVienLap.Location = new System.Drawing.Point(490, 102);
            this.lblNhanVienLap.Name = "lblNhanVienLap";
            this.lblNhanVienLap.Size = new System.Drawing.Size(117, 17);
            this.lblNhanVienLap.TabIndex = 7;
            this.lblNhanVienLap.Text = "Admin (NV001)";
            // 
            // lblNVTag
            // 
            this.lblNVTag.AutoSize = true;
            this.lblNVTag.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblNVTag.Location = new System.Drawing.Point(385, 102);
            this.lblNVTag.Name = "lblNVTag";
            this.lblNVTag.Size = new System.Drawing.Size(95, 17);
            this.lblNVTag.TabIndex = 6;
            this.lblNVTag.Text = "Thủ Kho Xuất:";
            // 
            // txtLyDoXuat
            // 
            this.txtLyDoXuat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLyDoXuat.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtLyDoXuat.Location = new System.Drawing.Point(125, 136);
            this.txtLyDoXuat.Name = "txtLyDoXuat";
            this.txtLyDoXuat.Size = new System.Drawing.Size(815, 24);
            this.txtLyDoXuat.TabIndex = 13;
            // 
            // lblLyDoXuat
            // 
            this.lblLyDoXuat.AutoSize = true;
            this.lblLyDoXuat.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblLyDoXuat.Location = new System.Drawing.Point(15, 139);
            this.lblLyDoXuat.Name = "lblLyDoXuat";
            this.lblLyDoXuat.Size = new System.Drawing.Size(74, 17);
            this.lblLyDoXuat.TabIndex = 12;
            this.lblLyDoXuat.Text = "Lý Do Xuất:";
            // 
            // dtpNgayXuat
            // 
            this.dtpNgayXuat.CustomFormat = "dd/MM/yyyy HH:mm";
            this.dtpNgayXuat.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.dtpNgayXuat.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpNgayXuat.Location = new System.Drawing.Point(125, 99);
            this.dtpNgayXuat.Name = "dtpNgayXuat";
            this.dtpNgayXuat.Size = new System.Drawing.Size(220, 24);
            this.dtpNgayXuat.TabIndex = 5;
            // 
            // lblNgayXuat
            // 
            this.lblNgayXuat.AutoSize = true;
            this.lblNgayXuat.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblNgayXuat.Location = new System.Drawing.Point(15, 102);
            this.lblNgayXuat.Name = "lblNgayXuat";
            this.lblNgayXuat.Size = new System.Drawing.Size(72, 17);
            this.lblNgayXuat.TabIndex = 4;
            this.lblNgayXuat.Text = "Ngày Xuất:";
            // 
            // cboKho
            // 
            this.cboKho.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboKho.DropDownWidth = 400;
            this.cboKho.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.cboKho.FormattingEnabled = true;
            this.cboKho.Location = new System.Drawing.Point(125, 63);
            this.cboKho.Name = "cboKho";
            this.cboKho.Size = new System.Drawing.Size(220, 24);
            this.cboKho.TabIndex = 3;
            this.cboKho.SelectedIndexChanged += new System.EventHandler(this.cboKho_SelectedIndexChanged);
            // 
            // lblKho
            // 
            this.lblKho.AutoSize = true;
            this.lblKho.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblKho.Location = new System.Drawing.Point(15, 66);
            this.lblKho.Name = "lblKho";
            this.lblKho.Size = new System.Drawing.Size(65, 17);
            this.lblKho.TabIndex = 2;
            this.lblKho.Text = "Kho Xuất:";
            // 
            // cboHoaDon
            // 
            this.cboHoaDon.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboHoaDon.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboHoaDon.DropDownWidth = 650;
            this.cboHoaDon.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.cboHoaDon.FormattingEnabled = true;
            this.cboHoaDon.Location = new System.Drawing.Point(490, 27);
            this.cboHoaDon.Name = "cboHoaDon";
            this.cboHoaDon.Size = new System.Drawing.Size(450, 24);
            this.cboHoaDon.TabIndex = 11;
            this.cboHoaDon.SelectedIndexChanged += new System.EventHandler(this.cboHoaDon_SelectedIndexChanged);
            // 
            // lblHoaDon
            // 
            this.lblHoaDon.AutoSize = true;
            this.lblHoaDon.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblHoaDon.Location = new System.Drawing.Point(385, 30);
            this.lblHoaDon.Name = "lblHoaDon";
            this.lblHoaDon.Size = new System.Drawing.Size(89, 17);
            this.lblHoaDon.TabIndex = 10;
            this.lblHoaDon.Text = "Chọn Hóa Đơn:";
            // 
            // txtMaPXK
            // 
            this.txtMaPXK.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtMaPXK.Location = new System.Drawing.Point(125, 27);
            this.txtMaPXK.Name = "txtMaPXK";
            this.txtMaPXK.ReadOnly = true;
            this.txtMaPXK.Size = new System.Drawing.Size(220, 24);
            this.txtMaPXK.TabIndex = 1;
            // 
            // lblMaPXK
            // 
            this.lblMaPXK.AutoSize = true;
            this.lblMaPXK.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblMaPXK.Location = new System.Drawing.Point(15, 30);
            this.lblMaPXK.Name = "lblMaPXK";
            this.lblMaPXK.Size = new System.Drawing.Size(94, 17);
            this.lblMaPXK.TabIndex = 0;
            this.lblMaPXK.Text = "Mã Phiếu Xuất:";
            // 
            // tpDanhSach
            // 
            this.tpDanhSach.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.tpDanhSach.Controls.Add(this.dgvDanhSachPhieuXuat);
            this.tpDanhSach.Controls.Add(this.pnlFilter);
            this.tpDanhSach.Location = new System.Drawing.Point(4, 26);
            this.tpDanhSach.Name = "tpDanhSach";
            this.tpDanhSach.Padding = new System.Windows.Forms.Padding(12);
            this.tpDanhSach.Size = new System.Drawing.Size(1076, 571);
            this.tpDanhSach.TabIndex = 1;
            this.tpDanhSach.Text = "  Danh Sách Phiếu Xuất Kho Đã Lập  ";
            // 
            // dgvDanhSachPhieuXuat
            // 
            this.dgvDanhSachPhieuXuat.AllowUserToAddRows = false;
            this.dgvDanhSachPhieuXuat.AllowUserToDeleteRows = false;
            this.dgvDanhSachPhieuXuat.BackgroundColor = System.Drawing.Color.White;
            this.dgvDanhSachPhieuXuat.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvDanhSachPhieuXuat.ColumnHeadersHeight = 35;
            this.dgvDanhSachPhieuXuat.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colDSMaPXK,
            this.colDSNgayXuat,
            this.colDSMaHDB,
            this.colDSKho,
            this.colDSKhachHang,
            this.colDSNhanVien,
            this.colDSLyDo,
            this.colDSTrangThai});
            this.dgvDanhSachPhieuXuat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDanhSachPhieuXuat.EnableHeadersVisualStyles = false;
            this.dgvDanhSachPhieuXuat.Location = new System.Drawing.Point(12, 58);
            this.dgvDanhSachPhieuXuat.Name = "dgvDanhSachPhieuXuat";
            this.dgvDanhSachPhieuXuat.ReadOnly = true;
            this.dgvDanhSachPhieuXuat.RowHeadersWidth = 35;
            this.dgvDanhSachPhieuXuat.RowTemplate.Height = 28;
            this.dgvDanhSachPhieuXuat.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDanhSachPhieuXuat.Size = new System.Drawing.Size(1052, 501);
            this.dgvDanhSachPhieuXuat.TabIndex = 1;
            this.dgvDanhSachPhieuXuat.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDanhSachPhieuXuat_CellDoubleClick);
            // 
            // colDSMaPXK
            // 
            this.colDSMaPXK.DataPropertyName = "MaPXK";
            this.colDSMaPXK.HeaderText = "Mã Phiếu Xuất";
            this.colDSMaPXK.Name = "colDSMaPXK";
            this.colDSMaPXK.ReadOnly = true;
            this.colDSMaPXK.Width = 120;
            // 
            // colDSNgayXuat
            // 
            this.colDSNgayXuat.DataPropertyName = "NgayXuat";
            this.colDSNgayXuat.HeaderText = "Ngày Xuất";
            this.colDSNgayXuat.Name = "colDSNgayXuat";
            this.colDSNgayXuat.ReadOnly = true;
            this.colDSNgayXuat.Width = 140;
            // 
            // colDSMaHDB
            // 
            this.colDSMaHDB.DataPropertyName = "MaHDB";
            this.colDSMaHDB.HeaderText = "Mã Hóa Đơn";
            this.colDSMaHDB.Name = "colDSMaHDB";
            this.colDSMaHDB.ReadOnly = true;
            this.colDSMaHDB.Width = 120;
            // 
            // colDSKho
            // 
            this.colDSKho.DataPropertyName = "TenKho";
            this.colDSKho.HeaderText = "Kho Xuất";
            this.colDSKho.Name = "colDSKho";
            this.colDSKho.ReadOnly = true;
            this.colDSKho.Width = 150;
            // 
            // colDSKhachHang
            // 
            this.colDSKhachHang.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colDSKhachHang.DataPropertyName = "TenKH";
            this.colDSKhachHang.HeaderText = "Khách Hàng";
            this.colDSKhachHang.Name = "colDSKhachHang";
            this.colDSKhachHang.ReadOnly = true;
            // 
            // colDSNhanVien
            // 
            this.colDSNhanVien.DataPropertyName = "TenNV";
            this.colDSNhanVien.HeaderText = "Thủ Kho";
            this.colDSNhanVien.Name = "colDSNhanVien";
            this.colDSNhanVien.ReadOnly = true;
            this.colDSNhanVien.Width = 130;
            // 
            // colDSLyDo
            // 
            this.colDSLyDo.DataPropertyName = "LyDoXuat";
            this.colDSLyDo.HeaderText = "Lý Do Xuất";
            this.colDSLyDo.Name = "colDSLyDo";
            this.colDSLyDo.ReadOnly = true;
            this.colDSLyDo.Width = 150;
            // 
            // colDSTrangThai
            // 
            this.colDSTrangThai.DataPropertyName = "TrangThai";
            this.colDSTrangThai.HeaderText = "Trạng Thái";
            this.colDSTrangThai.Name = "colDSTrangThai";
            this.colDSTrangThai.ReadOnly = true;
            this.colDSTrangThai.Width = 110;
            // 
            // pnlFilter
            // 
            this.pnlFilter.BackColor = System.Drawing.Color.White;
            this.pnlFilter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlFilter.Controls.Add(this.btnLamMoiDanhSach);
            this.pnlFilter.Controls.Add(this.btnTimKiem);
            this.pnlFilter.Controls.Add(this.txtTimKiem);
            this.pnlFilter.Controls.Add(this.lblTimKiem);
            this.pnlFilter.Controls.Add(this.cboFilterKho);
            this.pnlFilter.Controls.Add(this.lblFilterKho);
            this.pnlFilter.Controls.Add(this.lblSoPhieu);
            this.pnlFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFilter.Location = new System.Drawing.Point(12, 12);
            this.pnlFilter.Name = "pnlFilter";
            this.pnlFilter.Size = new System.Drawing.Size(1052, 46);
            this.pnlFilter.TabIndex = 0;
            // 
            // btnLamMoiDanhSach
            // 
            this.btnLamMoiDanhSach.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLamMoiDanhSach.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(110)))), ((int)(((byte)(253)))));
            this.btnLamMoiDanhSach.FlatAppearance.BorderSize = 0;
            this.btnLamMoiDanhSach.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLamMoiDanhSach.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnLamMoiDanhSach.ForeColor = System.Drawing.Color.White;
            this.btnLamMoiDanhSach.Location = new System.Drawing.Point(945, 6);
            this.btnLamMoiDanhSach.Name = "btnLamMoiDanhSach";
            this.btnLamMoiDanhSach.Size = new System.Drawing.Size(95, 32);
            this.btnLamMoiDanhSach.TabIndex = 6;
            this.btnLamMoiDanhSach.Text = "Làm Mới";
            this.btnLamMoiDanhSach.UseVisualStyleBackColor = false;
            this.btnLamMoiDanhSach.Click += new System.EventHandler(this.btnLamMoiDanhSach_Click);
            // 
            // btnTimKiem
            // 
            this.btnTimKiem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(64)))), ((int)(((byte)(175)))));
            this.btnTimKiem.FlatAppearance.BorderSize = 0;
            this.btnTimKiem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTimKiem.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnTimKiem.ForeColor = System.Drawing.Color.White;
            this.btnTimKiem.Location = new System.Drawing.Point(620, 6);
            this.btnTimKiem.Name = "btnTimKiem";
            this.btnTimKiem.Size = new System.Drawing.Size(90, 32);
            this.btnTimKiem.TabIndex = 5;
            this.btnTimKiem.Text = "Tìm Kiếm";
            this.btnTimKiem.UseVisualStyleBackColor = false;
            this.btnTimKiem.Click += new System.EventHandler(this.btnTimKiem_Click);
            // 
            // txtTimKiem
            // 
            this.txtTimKiem.Location = new System.Drawing.Point(400, 10);
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.Size = new System.Drawing.Size(210, 24);
            this.txtTimKiem.TabIndex = 4;
            // 
            // lblTimKiem
            // 
            this.lblTimKiem.AutoSize = true;
            this.lblTimKiem.Location = new System.Drawing.Point(330, 14);
            this.lblTimKiem.Name = "lblTimKiem";
            this.lblTimKiem.Size = new System.Drawing.Size(63, 17);
            this.lblTimKiem.TabIndex = 3;
            this.lblTimKiem.Text = "Tìm kiếm:";
            // 
            // cboFilterKho
            // 
            this.cboFilterKho.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFilterKho.FormattingEnabled = true;
            this.cboFilterKho.Location = new System.Drawing.Point(125, 10);
            this.cboFilterKho.Name = "cboFilterKho";
            this.cboFilterKho.Size = new System.Drawing.Size(180, 24);
            this.cboFilterKho.TabIndex = 2;
            this.cboFilterKho.SelectedIndexChanged += new System.EventHandler(this.cboFilterKho_SelectedIndexChanged);
            // 
            // lblFilterKho
            // 
            this.lblFilterKho.AutoSize = true;
            this.lblFilterKho.Location = new System.Drawing.Point(15, 14);
            this.lblFilterKho.Name = "lblFilterKho";
            this.lblFilterKho.Size = new System.Drawing.Size(92, 17);
            this.lblFilterKho.TabIndex = 1;
            this.lblFilterKho.Text = "Lọc theo kho:";
            // 
            // lblSoPhieu
            // 
            this.lblSoPhieu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSoPhieu.AutoSize = true;
            this.lblSoPhieu.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Italic);
            this.lblSoPhieu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.lblSoPhieu.Location = new System.Drawing.Point(750, 14);
            this.lblSoPhieu.Name = "lblSoPhieu";
            this.lblSoPhieu.Size = new System.Drawing.Size(149, 17);
            this.lblSoPhieu.TabIndex = 0;
            this.lblSoPhieu.Text = "Tổng số phiếu: 0 phiếu";
            // 
            // frmPhieuXuatKho
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1084, 661);
            this.Controls.Add(this.tcPhieuXuat);
            this.Controls.Add(this.pnlHeader);
            this.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmPhieuXuatKho";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quản Lý Xuất Kho - DNQH Kế Toán Bán Hàng";
            this.Load += new System.EventHandler(this.frmPhieuXuatKho_Load);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.tcPhieuXuat.ResumeLayout(false);
            this.tpLapPhieu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTiet)).EndInit();
            this.pnlBottom.ResumeLayout(false);
            this.pnlBottom.PerformLayout();
            this.grpThongTin.ResumeLayout(false);
            this.grpThongTin.PerformLayout();
            this.tpDanhSach.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDanhSachPhieuXuat)).EndInit();
            this.pnlFilter.ResumeLayout(false);
            this.pnlFilter.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubTitle;
        private System.Windows.Forms.TabControl tcPhieuXuat;
        private System.Windows.Forms.TabPage tpLapPhieu;
        private System.Windows.Forms.GroupBox grpThongTin;
        private System.Windows.Forms.TextBox txtMaPXK;
        private System.Windows.Forms.Label lblMaPXK;
        private System.Windows.Forms.ComboBox cboHoaDon;
        private System.Windows.Forms.Label lblHoaDon;
        private System.Windows.Forms.ComboBox cboKho;
        private System.Windows.Forms.Label lblKho;
        private System.Windows.Forms.DateTimePicker dtpNgayXuat;
        private System.Windows.Forms.Label lblNgayXuat;
        private System.Windows.Forms.TextBox txtLyDoXuat;
        private System.Windows.Forms.Label lblLyDoXuat;
        private System.Windows.Forms.Label lblNVTag;
        private System.Windows.Forms.Label lblNhanVienLap;
        private System.Windows.Forms.Label lblKHTag;
        private System.Windows.Forms.Label lblKhachHangInfo;
        private System.Windows.Forms.DataGridView dgvChiTiet;
        private System.Windows.Forms.Panel pnlBottom;
        private System.Windows.Forms.Label lblTongSoLuongXuat;
        private System.Windows.Forms.Button btnXuatKho;
        private System.Windows.Forms.Button btnLamMoi;
        private System.Windows.Forms.Button btnDong;
        private System.Windows.Forms.TabPage tpDanhSach;
        private System.Windows.Forms.Panel pnlFilter;
        private System.Windows.Forms.Label lblFilterKho;
        private System.Windows.Forms.ComboBox cboFilterKho;
        private System.Windows.Forms.Label lblTimKiem;
        private System.Windows.Forms.TextBox txtTimKiem;
        private System.Windows.Forms.Button btnTimKiem;
        private System.Windows.Forms.Button btnLamMoiDanhSach;
        private System.Windows.Forms.Label lblSoPhieu;
        private System.Windows.Forms.DataGridView dgvDanhSachPhieuXuat;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMaSP;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTenSP;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDonViTinh;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSoLuongHDB;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSoLuongDaXuat;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSoLuongConLai;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTonKho;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSoLuongXuat;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDSMaPXK;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDSNgayXuat;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDSMaHDB;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDSKho;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDSKhachHang;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDSNhanVien;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDSLyDo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDSTrangThai;
    }
}
