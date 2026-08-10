namespace DNQH_KeToanBanHang.Forms
{
    partial class frmPhieuThu
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
            this.tabControlPhieuThu = new System.Windows.Forms.TabControl();
            this.tabLapPhieu = new System.Windows.Forms.TabPage();
            this.pnlLapPhieu = new System.Windows.Forms.Panel();
            this.groupBoxThongTinHDB = new System.Windows.Forms.GroupBox();
            this.lblKhachHang = new System.Windows.Forms.Label();
            this.lblTongTienHDB = new System.Windows.Forms.Label();
            this.lblDaThu = new System.Windows.Forms.Label();
            this.lblConLai = new System.Windows.Forms.Label();
            this.groupBoxPhieuThu = new System.Windows.Forms.GroupBox();
            this.lblMaPT = new System.Windows.Forms.Label();
            this.txtMaPT = new System.Windows.Forms.TextBox();
            this.lblHoaDon = new System.Windows.Forms.Label();
            this.cboHoaDon = new System.Windows.Forms.ComboBox();
            this.lblNgayThu = new System.Windows.Forms.Label();
            this.dtpNgayThu = new System.Windows.Forms.DateTimePicker();
            this.lblNguoiNop = new System.Windows.Forms.Label();
            this.txtNguoiNop = new System.Windows.Forms.TextBox();
            this.lblSoTien = new System.Windows.Forms.Label();
            this.txtSoTien = new System.Windows.Forms.TextBox();
            this.lblHinhThuc = new System.Windows.Forms.Label();
            this.cboHinhThuc = new System.Windows.Forms.ComboBox();
            this.lblLyDoThu = new System.Windows.Forms.Label();
            this.txtLyDoThu = new System.Windows.Forms.TextBox();
            this.lblGhiChu = new System.Windows.Forms.Label();
            this.txtGhiChu = new System.Windows.Forms.TextBox();
            this.pnlButtons = new System.Windows.Forms.Panel();
            this.btnLamMoi = new System.Windows.Forms.Button();
            this.btnLuuPhieu = new System.Windows.Forms.Button();
            this.tabDanhSach = new System.Windows.Forms.TabPage();
            this.pnlFilter = new System.Windows.Forms.Panel();
            this.lblTimKiem = new System.Windows.Forms.Label();
            this.txtTimKiem = new System.Windows.Forms.TextBox();
            this.lblFromDate = new System.Windows.Forms.Label();
            this.dtpFromDate = new System.Windows.Forms.DateTimePicker();
            this.lblToDate = new System.Windows.Forms.Label();
            this.dtpToDate = new System.Windows.Forms.DateTimePicker();
            this.btnTimKiem = new System.Windows.Forms.Button();
            this.btnLamMoiDS = new System.Windows.Forms.Button();
            this.dgvDanhSachPhieuThu = new System.Windows.Forms.DataGridView();
            this.colMaPT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNgayThu = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMaHDB = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTenKH = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSoTien = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colHinhThuc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNguoiNop = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLyDoThu = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTenNV = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colGhiChu = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblStatus = new System.Windows.Forms.Label();
            this.tabControlPhieuThu.SuspendLayout();
            this.tabLapPhieu.SuspendLayout();
            this.pnlLapPhieu.SuspendLayout();
            this.groupBoxThongTinHDB.SuspendLayout();
            this.groupBoxPhieuThu.SuspendLayout();
            this.pnlButtons.SuspendLayout();
            this.tabDanhSach.SuspendLayout();
            this.pnlFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDanhSachPhieuThu)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControlPhieuThu
            // 
            this.tabControlPhieuThu.Controls.Add(this.tabLapPhieu);
            this.tabControlPhieuThu.Controls.Add(this.tabDanhSach);
            this.tabControlPhieuThu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlPhieuThu.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.tabControlPhieuThu.Location = new System.Drawing.Point(0, 0);
            this.tabControlPhieuThu.Name = "tabControlPhieuThu";
            this.tabControlPhieuThu.SelectedIndex = 0;
            this.tabControlPhieuThu.Size = new System.Drawing.Size(984, 611);
            this.tabControlPhieuThu.TabIndex = 0;
            // 
            // tabLapPhieu
            // 
            this.tabLapPhieu.Controls.Add(this.pnlLapPhieu);
            this.tabLapPhieu.Location = new System.Drawing.Point(4, 30);
            this.tabLapPhieu.Name = "tabLapPhieu";
            this.tabLapPhieu.Padding = new System.Windows.Forms.Padding(10);
            this.tabLapPhieu.Size = new System.Drawing.Size(976, 577);
            this.tabLapPhieu.TabIndex = 0;
            this.tabLapPhieu.Text = "Lập Phiếu Thu Tiền";
            this.tabLapPhieu.UseVisualStyleBackColor = true;
            // 
            // pnlLapPhieu
            // 
            this.pnlLapPhieu.AutoScroll = true;
            this.pnlLapPhieu.Controls.Add(this.pnlButtons);
            this.pnlLapPhieu.Controls.Add(this.groupBoxPhieuThu);
            this.pnlLapPhieu.Controls.Add(this.groupBoxThongTinHDB);
            this.pnlLapPhieu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLapPhieu.Location = new System.Drawing.Point(10, 10);
            this.pnlLapPhieu.Name = "pnlLapPhieu";
            this.pnlLapPhieu.Size = new System.Drawing.Size(956, 557);
            this.pnlLapPhieu.TabIndex = 0;
            // 
            // groupBoxThongTinHDB
            // 
            this.groupBoxThongTinHDB.Controls.Add(this.lblConLai);
            this.groupBoxThongTinHDB.Controls.Add(this.lblDaThu);
            this.groupBoxThongTinHDB.Controls.Add(this.lblTongTienHDB);
            this.groupBoxThongTinHDB.Controls.Add(this.lblKhachHang);
            this.groupBoxThongTinHDB.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxThongTinHDB.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.groupBoxThongTinHDB.Location = new System.Drawing.Point(0, 0);
            this.groupBoxThongTinHDB.Name = "groupBoxThongTinHDB";
            this.groupBoxThongTinHDB.Size = new System.Drawing.Size(956, 85);
            this.groupBoxThongTinHDB.TabIndex = 0;
            this.groupBoxThongTinHDB.TabStop = false;
            this.groupBoxThongTinHDB.Text = "Thông Tin Hóa Đơn & Tiến Độ Thanh Toán";
            // 
            // lblKhachHang
            // 
            this.lblKhachHang.AutoEllipsis = true;
            this.lblKhachHang.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblKhachHang.Location = new System.Drawing.Point(20, 35);
            this.lblKhachHang.Name = "lblKhachHang";
            this.lblKhachHang.Size = new System.Drawing.Size(290, 25);
            this.lblKhachHang.TabIndex = 0;
            this.lblKhachHang.Text = "Khách hàng: (Chưa chọn)";
            // 
            // lblTongTienHDB
            // 
            this.lblTongTienHDB.AutoSize = true;
            this.lblTongTienHDB.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblTongTienHDB.Location = new System.Drawing.Point(320, 35);
            this.lblTongTienHDB.Name = "lblTongTienHDB";
            this.lblTongTienHDB.Size = new System.Drawing.Size(155, 21);
            this.lblTongTienHDB.TabIndex = 1;
            this.lblTongTienHDB.Text = "Tổng hóa đơn: 0 VNĐ";
            // 
            // lblDaThu
            // 
            this.lblDaThu.AutoSize = true;
            this.lblDaThu.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblDaThu.ForeColor = System.Drawing.Color.DarkGreen;
            this.lblDaThu.Location = new System.Drawing.Point(540, 35);
            this.lblDaThu.Name = "lblDaThu";
            this.lblDaThu.Size = new System.Drawing.Size(107, 21);
            this.lblDaThu.TabIndex = 2;
            this.lblDaThu.Text = "Đã thu: 0 VNĐ";
            // 
            // lblConLai
            // 
            this.lblConLai.AutoSize = true;
            this.lblConLai.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblConLai.ForeColor = System.Drawing.Color.DarkRed;
            this.lblConLai.Location = new System.Drawing.Point(720, 35);
            this.lblConLai.Name = "lblConLai";
            this.lblConLai.Size = new System.Drawing.Size(161, 21);
            this.lblConLai.TabIndex = 3;
            this.lblConLai.Text = "Còn phải thu: 0 VNĐ";
            // 
            // groupBoxPhieuThu
            // 
            this.groupBoxPhieuThu.Controls.Add(this.txtGhiChu);
            this.groupBoxPhieuThu.Controls.Add(this.lblGhiChu);
            this.groupBoxPhieuThu.Controls.Add(this.txtLyDoThu);
            this.groupBoxPhieuThu.Controls.Add(this.lblLyDoThu);
            this.groupBoxPhieuThu.Controls.Add(this.cboHinhThuc);
            this.groupBoxPhieuThu.Controls.Add(this.lblHinhThuc);
            this.groupBoxPhieuThu.Controls.Add(this.txtSoTien);
            this.groupBoxPhieuThu.Controls.Add(this.lblSoTien);
            this.groupBoxPhieuThu.Controls.Add(this.txtNguoiNop);
            this.groupBoxPhieuThu.Controls.Add(this.lblNguoiNop);
            this.groupBoxPhieuThu.Controls.Add(this.dtpNgayThu);
            this.groupBoxPhieuThu.Controls.Add(this.lblNgayThu);
            this.groupBoxPhieuThu.Controls.Add(this.cboHoaDon);
            this.groupBoxPhieuThu.Controls.Add(this.lblHoaDon);
            this.groupBoxPhieuThu.Controls.Add(this.txtMaPT);
            this.groupBoxPhieuThu.Controls.Add(this.lblMaPT);
            this.groupBoxPhieuThu.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxPhieuThu.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.groupBoxPhieuThu.Location = new System.Drawing.Point(0, 85);
            this.groupBoxPhieuThu.Name = "groupBoxPhieuThu";
            this.groupBoxPhieuThu.Size = new System.Drawing.Size(956, 360);
            this.groupBoxPhieuThu.TabIndex = 1;
            this.groupBoxPhieuThu.TabStop = false;
            this.groupBoxPhieuThu.Text = "Chi Tiết Phiếu Thu";
            // 
            // lblMaPT
            // 
            this.lblMaPT.AutoSize = true;
            this.lblMaPT.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblMaPT.Location = new System.Drawing.Point(20, 35);
            this.lblMaPT.Name = "lblMaPT";
            this.lblMaPT.Size = new System.Drawing.Size(109, 21);
            this.lblMaPT.TabIndex = 0;
            this.lblMaPT.Text = "Mã Phiếu Thu:";
            // 
            // txtMaPT
            // 
            this.txtMaPT.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtMaPT.Location = new System.Drawing.Point(140, 32);
            this.txtMaPT.Name = "txtMaPT";
            this.txtMaPT.ReadOnly = true;
            this.txtMaPT.Size = new System.Drawing.Size(250, 29);
            this.txtMaPT.TabIndex = 1;
            // 
            // lblHoaDon
            // 
            this.lblHoaDon.AutoSize = true;
            this.lblHoaDon.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblHoaDon.Location = new System.Drawing.Point(430, 35);
            this.lblHoaDon.Name = "lblHoaDon";
            this.lblHoaDon.Size = new System.Drawing.Size(107, 21);
            this.lblHoaDon.TabIndex = 2;
            this.lblHoaDon.Text = "Hóa Đơn Bán:";
            // 
            // cboHoaDon
            // 
            this.cboHoaDon.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboHoaDon.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboHoaDon.DropDownWidth = 650;
            this.cboHoaDon.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.cboHoaDon.FormattingEnabled = true;
            this.cboHoaDon.Location = new System.Drawing.Point(550, 32);
            this.cboHoaDon.Name = "cboHoaDon";
            this.cboHoaDon.Size = new System.Drawing.Size(370, 29);
            this.cboHoaDon.TabIndex = 3;
            this.cboHoaDon.SelectedIndexChanged += new System.EventHandler(this.cboHoaDon_SelectedIndexChanged);
            // 
            // lblNgayThu
            // 
            this.lblNgayThu.AutoSize = true;
            this.lblNgayThu.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblNgayThu.Location = new System.Drawing.Point(20, 80);
            this.lblNgayThu.Name = "lblNgayThu";
            this.lblNgayThu.Size = new System.Drawing.Size(81, 21);
            this.lblNgayThu.TabIndex = 4;
            this.lblNgayThu.Text = "Ngày Thu:";
            // 
            // dtpNgayThu
            // 
            this.dtpNgayThu.CustomFormat = "dd/MM/yyyy HH:mm";
            this.dtpNgayThu.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.dtpNgayThu.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpNgayThu.Location = new System.Drawing.Point(140, 75);
            this.dtpNgayThu.Name = "dtpNgayThu";
            this.dtpNgayThu.Size = new System.Drawing.Size(250, 29);
            this.dtpNgayThu.TabIndex = 5;
            // 
            // lblNguoiNop
            // 
            this.lblNguoiNop.AutoSize = true;
            this.lblNguoiNop.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblNguoiNop.Location = new System.Drawing.Point(430, 80);
            this.lblNguoiNop.Name = "lblNguoiNop";
            this.lblNguoiNop.Size = new System.Drawing.Size(91, 21);
            this.lblNguoiNop.TabIndex = 6;
            this.lblNguoiNop.Text = "Người Nộp:";
            // 
            // txtNguoiNop
            // 
            this.txtNguoiNop.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNguoiNop.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtNguoiNop.Location = new System.Drawing.Point(550, 75);
            this.txtNguoiNop.Name = "txtNguoiNop";
            this.txtNguoiNop.Size = new System.Drawing.Size(370, 29);
            this.txtNguoiNop.TabIndex = 7;
            // 
            // lblSoTien
            // 
            this.lblSoTien.AutoSize = true;
            this.lblSoTien.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblSoTien.Location = new System.Drawing.Point(20, 125);
            this.lblSoTien.Name = "lblSoTien";
            this.lblSoTien.Size = new System.Drawing.Size(97, 21);
            this.lblSoTien.TabIndex = 8;
            this.lblSoTien.Text = "Số Tiền Thu:";
            // 
            // txtSoTien
            // 
            this.txtSoTien.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.txtSoTien.ForeColor = System.Drawing.Color.DarkBlue;
            this.txtSoTien.Location = new System.Drawing.Point(140, 120);
            this.txtSoTien.Name = "txtSoTien";
            this.txtSoTien.Size = new System.Drawing.Size(250, 29);
            this.txtSoTien.TabIndex = 9;
            this.txtSoTien.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtSoTien.TextChanged += new System.EventHandler(this.txtSoTien_TextChanged);
            // 
            // lblHinhThuc
            // 
            this.lblHinhThuc.AutoSize = true;
            this.lblHinhThuc.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblHinhThuc.Location = new System.Drawing.Point(430, 125);
            this.lblHinhThuc.Name = "lblHinhThuc";
            this.lblHinhThuc.Size = new System.Drawing.Size(83, 21);
            this.lblHinhThuc.TabIndex = 10;
            this.lblHinhThuc.Text = "Hình Thức:";
            // 
            // cboHinhThuc
            // 
            this.cboHinhThuc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboHinhThuc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboHinhThuc.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.cboHinhThuc.FormattingEnabled = true;
            this.cboHinhThuc.Items.AddRange(new object[] {
            "Tiền mặt",
            "Chuyển khoản"});
            this.cboHinhThuc.Location = new System.Drawing.Point(550, 120);
            this.cboHinhThuc.Name = "cboHinhThuc";
            this.cboHinhThuc.Size = new System.Drawing.Size(370, 29);
            this.cboHinhThuc.TabIndex = 11;
            // 
            // lblLyDoThu
            // 
            this.lblLyDoThu.AutoSize = true;
            this.lblLyDoThu.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblLyDoThu.Location = new System.Drawing.Point(20, 170);
            this.lblLyDoThu.Name = "lblLyDoThu";
            this.lblLyDoThu.Size = new System.Drawing.Size(83, 21);
            this.lblLyDoThu.TabIndex = 12;
            this.lblLyDoThu.Text = "Lý Do Thu:";
            // 
            // txtLyDoThu
            // 
            this.txtLyDoThu.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLyDoThu.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtLyDoThu.Location = new System.Drawing.Point(140, 167);
            this.txtLyDoThu.Multiline = true;
            this.txtLyDoThu.Name = "txtLyDoThu";
            this.txtLyDoThu.Size = new System.Drawing.Size(780, 55);
            this.txtLyDoThu.TabIndex = 13;
            // 
            // lblGhiChu
            // 
            this.lblGhiChu.AutoSize = true;
            this.lblGhiChu.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblGhiChu.Location = new System.Drawing.Point(20, 240);
            this.lblGhiChu.Name = "lblGhiChu";
            this.lblGhiChu.Size = new System.Drawing.Size(68, 21);
            this.lblGhiChu.TabIndex = 14;
            this.lblGhiChu.Text = "Ghi Chú:";
            // 
            // txtGhiChu
            // 
            this.txtGhiChu.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtGhiChu.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtGhiChu.Location = new System.Drawing.Point(140, 237);
            this.txtGhiChu.Multiline = true;
            this.txtGhiChu.Name = "txtGhiChu";
            this.txtGhiChu.Size = new System.Drawing.Size(780, 55);
            this.txtGhiChu.TabIndex = 15;
            // 
            // pnlButtons
            // 
            this.pnlButtons.Controls.Add(this.btnLamMoi);
            this.pnlButtons.Controls.Add(this.btnLuuPhieu);
            this.pnlButtons.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlButtons.Location = new System.Drawing.Point(0, 445);
            this.pnlButtons.Name = "pnlButtons";
            this.pnlButtons.Size = new System.Drawing.Size(956, 60);
            this.pnlButtons.TabIndex = 2;
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.btnLamMoi.Location = new System.Drawing.Point(340, 10);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(120, 40);
            this.btnLamMoi.TabIndex = 1;
            this.btnLamMoi.Text = "Làm Mới";
            this.btnLamMoi.UseVisualStyleBackColor = true;
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);
            // 
            // btnLuuPhieu
            // 
            this.btnLuuPhieu.BackColor = System.Drawing.Color.FromArgb(16, 185, 129);
            this.btnLuuPhieu.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnLuuPhieu.ForeColor = System.Drawing.Color.White;
            this.btnLuuPhieu.Location = new System.Drawing.Point(140, 10);
            this.btnLuuPhieu.Name = "btnLuuPhieu";
            this.btnLuuPhieu.Size = new System.Drawing.Size(180, 40);
            this.btnLuuPhieu.TabIndex = 0;
            this.btnLuuPhieu.Text = "Lưu Phiếu Thu";
            this.btnLuuPhieu.UseVisualStyleBackColor = false;
            this.btnLuuPhieu.Click += new System.EventHandler(this.btnLuuPhieu_Click);
            // 
            // tabDanhSach
            // 
            this.tabDanhSach.Controls.Add(this.dgvDanhSachPhieuThu);
            this.tabDanhSach.Controls.Add(this.lblStatus);
            this.tabDanhSach.Controls.Add(this.pnlFilter);
            this.tabDanhSach.Location = new System.Drawing.Point(4, 30);
            this.tabDanhSach.Name = "tabDanhSach";
            this.tabDanhSach.Padding = new System.Windows.Forms.Padding(10);
            this.tabDanhSach.Size = new System.Drawing.Size(976, 577);
            this.tabDanhSach.TabIndex = 1;
            this.tabDanhSach.Text = "Danh Sách Phiếu Thu";
            this.tabDanhSach.UseVisualStyleBackColor = true;
            // 
            // pnlFilter
            // 
            this.pnlFilter.Controls.Add(this.btnLamMoiDS);
            this.pnlFilter.Controls.Add(this.btnTimKiem);
            this.pnlFilter.Controls.Add(this.dtpToDate);
            this.pnlFilter.Controls.Add(this.lblToDate);
            this.pnlFilter.Controls.Add(this.dtpFromDate);
            this.pnlFilter.Controls.Add(this.lblFromDate);
            this.pnlFilter.Controls.Add(this.txtTimKiem);
            this.pnlFilter.Controls.Add(this.lblTimKiem);
            this.pnlFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFilter.Location = new System.Drawing.Point(10, 10);
            this.pnlFilter.Name = "pnlFilter";
            this.pnlFilter.Size = new System.Drawing.Size(956, 60);
            this.pnlFilter.TabIndex = 0;
            // 
            // lblTimKiem
            // 
            this.lblTimKiem.AutoSize = true;
            this.lblTimKiem.Location = new System.Drawing.Point(10, 20);
            this.lblTimKiem.Name = "lblTimKiem";
            this.lblTimKiem.Size = new System.Drawing.Size(78, 21);
            this.lblTimKiem.TabIndex = 0;
            this.lblTimKiem.Text = "Tìm kiếm:";
            // 
            // txtTimKiem
            // 
            this.txtTimKiem.Location = new System.Drawing.Point(90, 17);
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.Size = new System.Drawing.Size(200, 29);
            this.txtTimKiem.TabIndex = 1;
            // 
            // lblFromDate
            // 
            this.lblFromDate.AutoSize = true;
            this.lblFromDate.Location = new System.Drawing.Point(310, 20);
            this.lblFromDate.Name = "lblFromDate";
            this.lblFromDate.Size = new System.Drawing.Size(68, 21);
            this.lblFromDate.TabIndex = 2;
            this.lblFromDate.Text = "Từ ngày:";
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.CustomFormat = "dd/MM/yyyy";
            this.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFromDate.Location = new System.Drawing.Point(380, 17);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.Size = new System.Drawing.Size(120, 29);
            this.dtpFromDate.TabIndex = 3;
            // 
            // lblToDate
            // 
            this.lblToDate.AutoSize = true;
            this.lblToDate.Location = new System.Drawing.Point(520, 20);
            this.lblToDate.Name = "lblToDate";
            this.lblToDate.Size = new System.Drawing.Size(79, 21);
            this.lblToDate.TabIndex = 4;
            this.lblToDate.Text = "Đến ngày:";
            // 
            // dtpToDate
            // 
            this.dtpToDate.CustomFormat = "dd/MM/yyyy";
            this.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpToDate.Location = new System.Drawing.Point(600, 17);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.Size = new System.Drawing.Size(120, 29);
            this.dtpToDate.TabIndex = 5;
            // 
            // btnTimKiem
            // 
            this.btnTimKiem.Location = new System.Drawing.Point(740, 13);
            this.btnTimKiem.Name = "btnTimKiem";
            this.btnTimKiem.Size = new System.Drawing.Size(95, 35);
            this.btnTimKiem.TabIndex = 6;
            this.btnTimKiem.Text = "Tìm Kiếm";
            this.btnTimKiem.UseVisualStyleBackColor = true;
            this.btnTimKiem.Click += new System.EventHandler(this.btnTimKiem_Click);
            // 
            // btnLamMoiDS
            // 
            this.btnLamMoiDS.Location = new System.Drawing.Point(845, 13);
            this.btnLamMoiDS.Name = "btnLamMoiDS";
            this.btnLamMoiDS.Size = new System.Drawing.Size(95, 35);
            this.btnLamMoiDS.TabIndex = 7;
            this.btnLamMoiDS.Text = "Tải Lại";
            this.btnLamMoiDS.UseVisualStyleBackColor = true;
            this.btnLamMoiDS.Click += new System.EventHandler(this.btnLamMoiDS_Click);
            // 
            // dgvDanhSachPhieuThu
            // 
            this.dgvDanhSachPhieuThu.AllowUserToAddRows = false;
            this.dgvDanhSachPhieuThu.AllowUserToDeleteRows = false;
            this.dgvDanhSachPhieuThu.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDanhSachPhieuThu.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDanhSachPhieuThu.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colMaPT,
            this.colNgayThu,
            this.colMaHDB,
            this.colTenKH,
            this.colSoTien,
            this.colHinhThuc,
            this.colNguoiNop,
            this.colLyDoThu,
            this.colTenNV,
            this.colGhiChu});
            this.dgvDanhSachPhieuThu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDanhSachPhieuThu.Location = new System.Drawing.Point(10, 70);
            this.dgvDanhSachPhieuThu.MultiSelect = false;
            this.dgvDanhSachPhieuThu.Name = "dgvDanhSachPhieuThu";
            this.dgvDanhSachPhieuThu.ReadOnly = true;
            this.dgvDanhSachPhieuThu.RowHeadersWidth = 30;
            this.dgvDanhSachPhieuThu.RowTemplate.Height = 28;
            this.dgvDanhSachPhieuThu.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDanhSachPhieuThu.Size = new System.Drawing.Size(956, 467);
            this.dgvDanhSachPhieuThu.TabIndex = 1;
            // 
            // colMaPT
            // 
            this.colMaPT.DataPropertyName = "MaPT";
            this.colMaPT.FillWeight = 85F;
            this.colMaPT.HeaderText = "Mã PT";
            this.colMaPT.MinimumWidth = 80;
            this.colMaPT.Name = "colMaPT";
            this.colMaPT.ReadOnly = true;
            // 
            // colNgayThu
            // 
            this.colNgayThu.DataPropertyName = "NgayThu";
            this.colNgayThu.FillWeight = 90F;
            this.colNgayThu.HeaderText = "Ngày Thu";
            this.colNgayThu.MinimumWidth = 85;
            this.colNgayThu.Name = "colNgayThu";
            this.colNgayThu.ReadOnly = true;
            // 
            // colMaHDB
            // 
            this.colMaHDB.DataPropertyName = "MaHDB";
            this.colMaHDB.FillWeight = 95F;
            this.colMaHDB.HeaderText = "Mã Hóa Đơn";
            this.colMaHDB.MinimumWidth = 90;
            this.colMaHDB.Name = "colMaHDB";
            this.colMaHDB.ReadOnly = true;
            // 
            // colTenKH
            // 
            this.colTenKH.DataPropertyName = "TenKH";
            this.colTenKH.FillWeight = 110F;
            this.colTenKH.HeaderText = "Khách Hàng";
            this.colTenKH.MinimumWidth = 100;
            this.colTenKH.Name = "colTenKH";
            this.colTenKH.ReadOnly = true;
            // 
            // colSoTien
            // 
            this.colSoTien.DataPropertyName = "SoTien";
            this.colSoTien.FillWeight = 95F;
            this.colSoTien.HeaderText = "Số Tiền (VNĐ)";
            this.colSoTien.MinimumWidth = 90;
            this.colSoTien.Name = "colSoTien";
            this.colSoTien.ReadOnly = true;
            // 
            // colHinhThuc
            // 
            this.colHinhThuc.DataPropertyName = "HinhThuc";
            this.colHinhThuc.FillWeight = 80F;
            this.colHinhThuc.HeaderText = "Hình Thức";
            this.colHinhThuc.MinimumWidth = 75;
            this.colHinhThuc.Name = "colHinhThuc";
            this.colHinhThuc.ReadOnly = true;
            // 
            // colNguoiNop
            // 
            this.colNguoiNop.DataPropertyName = "NguoiNop";
            this.colNguoiNop.FillWeight = 90F;
            this.colNguoiNop.HeaderText = "Người Nộp";
            this.colNguoiNop.MinimumWidth = 85;
            this.colNguoiNop.Name = "colNguoiNop";
            this.colNguoiNop.ReadOnly = true;
            // 
            // colLyDoThu
            // 
            this.colLyDoThu.DataPropertyName = "LyDoThu";
            this.colLyDoThu.FillWeight = 110F;
            this.colLyDoThu.HeaderText = "Lý Do Thu";
            this.colLyDoThu.MinimumWidth = 100;
            this.colLyDoThu.Name = "colLyDoThu";
            this.colLyDoThu.ReadOnly = true;
            // 
            // colTenNV
            // 
            this.colTenNV.DataPropertyName = "TenNV";
            this.colTenNV.FillWeight = 90F;
            this.colTenNV.HeaderText = "Người Lập";
            this.colTenNV.MinimumWidth = 85;
            this.colTenNV.Name = "colTenNV";
            this.colTenNV.ReadOnly = true;
            // 
            // colGhiChu
            // 
            this.colGhiChu.DataPropertyName = "GhiChu";
            this.colGhiChu.FillWeight = 80F;
            this.colGhiChu.HeaderText = "Ghi Chú";
            this.colGhiChu.MinimumWidth = 70;
            this.colGhiChu.Name = "colGhiChu";
            this.colGhiChu.ReadOnly = true;
            // 
            // lblStatus
            // 
            this.lblStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.lblStatus.Location = new System.Drawing.Point(10, 537);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(956, 30);
            this.lblStatus.TabIndex = 2;
            this.lblStatus.Text = "Tổng số phiếu thu: 0 phiếu";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // frmPhieuThu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 611);
            this.Controls.Add(this.tabControlPhieuThu);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "frmPhieuThu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Quản Lý Phiếu Thu Tiền";
            this.Load += new System.EventHandler(this.frmPhieuThu_Load);
            this.tabControlPhieuThu.ResumeLayout(false);
            this.tabLapPhieu.ResumeLayout(false);
            this.pnlLapPhieu.ResumeLayout(false);
            this.groupBoxThongTinHDB.ResumeLayout(false);
            this.groupBoxThongTinHDB.PerformLayout();
            this.groupBoxPhieuThu.ResumeLayout(false);
            this.groupBoxPhieuThu.PerformLayout();
            this.pnlButtons.ResumeLayout(false);
            this.tabDanhSach.ResumeLayout(false);
            this.pnlFilter.ResumeLayout(false);
            this.pnlFilter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDanhSachPhieuThu)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControlPhieuThu;
        private System.Windows.Forms.TabPage tabLapPhieu;
        private System.Windows.Forms.Panel pnlLapPhieu;
        private System.Windows.Forms.GroupBox groupBoxThongTinHDB;
        private System.Windows.Forms.Label lblKhachHang;
        private System.Windows.Forms.Label lblTongTienHDB;
        private System.Windows.Forms.Label lblDaThu;
        private System.Windows.Forms.Label lblConLai;
        private System.Windows.Forms.GroupBox groupBoxPhieuThu;
        private System.Windows.Forms.Label lblMaPT;
        private System.Windows.Forms.TextBox txtMaPT;
        private System.Windows.Forms.Label lblHoaDon;
        private System.Windows.Forms.ComboBox cboHoaDon;
        private System.Windows.Forms.Label lblNgayThu;
        private System.Windows.Forms.DateTimePicker dtpNgayThu;
        private System.Windows.Forms.Label lblNguoiNop;
        private System.Windows.Forms.TextBox txtNguoiNop;
        private System.Windows.Forms.Label lblSoTien;
        private System.Windows.Forms.TextBox txtSoTien;
        private System.Windows.Forms.Label lblHinhThuc;
        private System.Windows.Forms.ComboBox cboHinhThuc;
        private System.Windows.Forms.Label lblLyDoThu;
        private System.Windows.Forms.TextBox txtLyDoThu;
        private System.Windows.Forms.Label lblGhiChu;
        private System.Windows.Forms.TextBox txtGhiChu;
        private System.Windows.Forms.Panel pnlButtons;
        private System.Windows.Forms.Button btnLamMoi;
        private System.Windows.Forms.Button btnLuuPhieu;
        private System.Windows.Forms.TabPage tabDanhSach;
        private System.Windows.Forms.Panel pnlFilter;
        private System.Windows.Forms.Label lblTimKiem;
        private System.Windows.Forms.TextBox txtTimKiem;
        private System.Windows.Forms.Label lblFromDate;
        private System.Windows.Forms.DateTimePicker dtpFromDate;
        private System.Windows.Forms.Label lblToDate;
        private System.Windows.Forms.DateTimePicker dtpToDate;
        private System.Windows.Forms.Button btnTimKiem;
        private System.Windows.Forms.Button btnLamMoiDS;
        private System.Windows.Forms.DataGridView dgvDanhSachPhieuThu;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMaPT;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNgayThu;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMaHDB;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTenKH;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSoTien;
        private System.Windows.Forms.DataGridViewTextBoxColumn colHinhThuc;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNguoiNop;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLyDoThu;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTenNV;
        private System.Windows.Forms.DataGridViewTextBoxColumn colGhiChu;
        private System.Windows.Forms.Label lblStatus;
    }
}
