namespace DNQH_KeToanBanHang.Forms
{
    partial class frmPhieuChi
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
            this.tabControlPhieuChi = new System.Windows.Forms.TabControl();
            this.tabLapPhieu = new System.Windows.Forms.TabPage();
            this.pnlLapPhieu = new System.Windows.Forms.Panel();
            this.groupBoxPhieuChi = new System.Windows.Forms.GroupBox();
            this.lblMaPC = new System.Windows.Forms.Label();
            this.txtMaPC = new System.Windows.Forms.TextBox();
            this.lblNgayChi = new System.Windows.Forms.Label();
            this.dtpNgayChi = new System.Windows.Forms.DateTimePicker();
            this.lblNguoiNhan = new System.Windows.Forms.Label();
            this.txtNguoiNhan = new System.Windows.Forms.TextBox();
            this.lblSoTien = new System.Windows.Forms.Label();
            this.txtSoTien = new System.Windows.Forms.TextBox();
            this.lblHinhThuc = new System.Windows.Forms.Label();
            this.cboHinhThuc = new System.Windows.Forms.ComboBox();
            this.lblLyDoChi = new System.Windows.Forms.Label();
            this.txtLyDoChi = new System.Windows.Forms.TextBox();
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
            this.dgvDanhSachPhieuChi = new System.Windows.Forms.DataGridView();
            this.colMaPC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNgayChi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNguoiNhan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSoTien = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colHinhThuc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLyDoChi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTenNV = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colGhiChu = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblStatus = new System.Windows.Forms.Label();
            this.tabControlPhieuChi.SuspendLayout();
            this.tabLapPhieu.SuspendLayout();
            this.pnlLapPhieu.SuspendLayout();
            this.groupBoxPhieuChi.SuspendLayout();
            this.pnlButtons.SuspendLayout();
            this.tabDanhSach.SuspendLayout();
            this.pnlFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDanhSachPhieuChi)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControlPhieuChi
            // 
            this.tabControlPhieuChi.Controls.Add(this.tabLapPhieu);
            this.tabControlPhieuChi.Controls.Add(this.tabDanhSach);
            this.tabControlPhieuChi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlPhieuChi.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.tabControlPhieuChi.Location = new System.Drawing.Point(0, 0);
            this.tabControlPhieuChi.Name = "tabControlPhieuChi";
            this.tabControlPhieuChi.SelectedIndex = 0;
            this.tabControlPhieuChi.Size = new System.Drawing.Size(984, 611);
            this.tabControlPhieuChi.TabIndex = 0;
            // 
            // tabLapPhieu
            // 
            this.tabLapPhieu.Controls.Add(this.pnlLapPhieu);
            this.tabLapPhieu.Location = new System.Drawing.Point(4, 30);
            this.tabLapPhieu.Name = "tabLapPhieu";
            this.tabLapPhieu.Padding = new System.Windows.Forms.Padding(10);
            this.tabLapPhieu.Size = new System.Drawing.Size(976, 577);
            this.tabLapPhieu.TabIndex = 0;
            this.tabLapPhieu.Text = "Lập Phiếu Chi Tiền";
            this.tabLapPhieu.UseVisualStyleBackColor = true;
            // 
            // pnlLapPhieu
            // 
            this.pnlLapPhieu.AutoScroll = true;
            this.pnlLapPhieu.Controls.Add(this.pnlButtons);
            this.pnlLapPhieu.Controls.Add(this.groupBoxPhieuChi);
            this.pnlLapPhieu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLapPhieu.Location = new System.Drawing.Point(10, 10);
            this.pnlLapPhieu.Name = "pnlLapPhieu";
            this.pnlLapPhieu.Size = new System.Drawing.Size(956, 557);
            this.pnlLapPhieu.TabIndex = 0;
            // 
            // groupBoxPhieuChi
            // 
            this.groupBoxPhieuChi.Controls.Add(this.txtGhiChu);
            this.groupBoxPhieuChi.Controls.Add(this.lblGhiChu);
            this.groupBoxPhieuChi.Controls.Add(this.txtLyDoChi);
            this.groupBoxPhieuChi.Controls.Add(this.lblLyDoChi);
            this.groupBoxPhieuChi.Controls.Add(this.cboHinhThuc);
            this.groupBoxPhieuChi.Controls.Add(this.lblHinhThuc);
            this.groupBoxPhieuChi.Controls.Add(this.txtSoTien);
            this.groupBoxPhieuChi.Controls.Add(this.lblSoTien);
            this.groupBoxPhieuChi.Controls.Add(this.txtNguoiNhan);
            this.groupBoxPhieuChi.Controls.Add(this.lblNguoiNhan);
            this.groupBoxPhieuChi.Controls.Add(this.dtpNgayChi);
            this.groupBoxPhieuChi.Controls.Add(this.lblNgayChi);
            this.groupBoxPhieuChi.Controls.Add(this.txtMaPC);
            this.groupBoxPhieuChi.Controls.Add(this.lblMaPC);
            this.groupBoxPhieuChi.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxPhieuChi.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.groupBoxPhieuChi.Location = new System.Drawing.Point(0, 0);
            this.groupBoxPhieuChi.Name = "groupBoxPhieuChi";
            this.groupBoxPhieuChi.Size = new System.Drawing.Size(956, 360);
            this.groupBoxPhieuChi.TabIndex = 0;
            this.groupBoxPhieuChi.TabStop = false;
            this.groupBoxPhieuChi.Text = "Thông Tin Phiếu Chi Tiền (Chi Độc Lập)";
            // 
            // lblMaPC
            // 
            this.lblMaPC.AutoSize = true;
            this.lblMaPC.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblMaPC.Location = new System.Drawing.Point(20, 40);
            this.lblMaPC.Name = "lblMaPC";
            this.lblMaPC.Size = new System.Drawing.Size(105, 21);
            this.lblMaPC.TabIndex = 0;
            this.lblMaPC.Text = "Mã Phiếu Chi:";
            // 
            // txtMaPC
            // 
            this.txtMaPC.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtMaPC.Location = new System.Drawing.Point(140, 37);
            this.txtMaPC.Name = "txtMaPC";
            this.txtMaPC.ReadOnly = true;
            this.txtMaPC.Size = new System.Drawing.Size(250, 29);
            this.txtMaPC.TabIndex = 1;
            // 
            // lblNgayChi
            // 
            this.lblNgayChi.AutoSize = true;
            this.lblNgayChi.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblNgayChi.Location = new System.Drawing.Point(430, 40);
            this.lblNgayChi.Name = "lblNgayChi";
            this.lblNgayChi.Size = new System.Drawing.Size(77, 21);
            this.lblNgayChi.TabIndex = 2;
            this.lblNgayChi.Text = "Ngày Chi:";
            // 
            // dtpNgayChi
            // 
            this.dtpNgayChi.CustomFormat = "dd/MM/yyyy HH:mm";
            this.dtpNgayChi.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.dtpNgayChi.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpNgayChi.Location = new System.Drawing.Point(550, 37);
            this.dtpNgayChi.Name = "dtpNgayChi";
            this.dtpNgayChi.Size = new System.Drawing.Size(370, 29);
            this.dtpNgayChi.TabIndex = 3;
            // 
            // lblNguoiNhan
            // 
            this.lblNguoiNhan.AutoSize = true;
            this.lblNguoiNhan.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblNguoiNhan.Location = new System.Drawing.Point(20, 85);
            this.lblNguoiNhan.Name = "lblNguoiNhan";
            this.lblNguoiNhan.Size = new System.Drawing.Size(100, 21);
            this.lblNguoiNhan.TabIndex = 4;
            this.lblNguoiNhan.Text = "Người Nhận:";
            // 
            // txtNguoiNhan
            // 
            this.txtNguoiNhan.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtNguoiNhan.Location = new System.Drawing.Point(140, 82);
            this.txtNguoiNhan.Name = "txtNguoiNhan";
            this.txtNguoiNhan.Size = new System.Drawing.Size(250, 29);
            this.txtNguoiNhan.TabIndex = 5;
            // 
            // lblSoTien
            // 
            this.lblSoTien.AutoSize = true;
            this.lblSoTien.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblSoTien.Location = new System.Drawing.Point(430, 85);
            this.lblSoTien.Name = "lblSoTien";
            this.lblSoTien.Size = new System.Drawing.Size(93, 21);
            this.lblSoTien.TabIndex = 6;
            this.lblSoTien.Text = "Số Tiền Chi:";
            // 
            // txtSoTien
            // 
            this.txtSoTien.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSoTien.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.txtSoTien.ForeColor = System.Drawing.Color.DarkRed;
            this.txtSoTien.Location = new System.Drawing.Point(550, 82);
            this.txtSoTien.Name = "txtSoTien";
            this.txtSoTien.Size = new System.Drawing.Size(370, 29);
            this.txtSoTien.TabIndex = 7;
            this.txtSoTien.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblHinhThuc
            // 
            this.lblHinhThuc.AutoSize = true;
            this.lblHinhThuc.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblHinhThuc.Location = new System.Drawing.Point(20, 130);
            this.lblHinhThuc.Name = "lblHinhThuc";
            this.lblHinhThuc.Size = new System.Drawing.Size(83, 21);
            this.lblHinhThuc.TabIndex = 8;
            this.lblHinhThuc.Text = "Hình Thức:";
            // 
            // cboHinhThuc
            // 
            this.cboHinhThuc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboHinhThuc.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.cboHinhThuc.FormattingEnabled = true;
            this.cboHinhThuc.Items.AddRange(new object[] {
            "Tiền mặt",
            "Chuyển khoản"});
            this.cboHinhThuc.Location = new System.Drawing.Point(140, 125);
            this.cboHinhThuc.Name = "cboHinhThuc";
            this.cboHinhThuc.Size = new System.Drawing.Size(250, 29);
            this.cboHinhThuc.TabIndex = 9;
            // 
            // lblLyDoChi
            // 
            this.lblLyDoChi.AutoSize = true;
            this.lblLyDoChi.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblLyDoChi.Location = new System.Drawing.Point(20, 175);
            this.lblLyDoChi.Name = "lblLyDoChi";
            this.lblLyDoChi.Size = new System.Drawing.Size(79, 21);
            this.lblLyDoChi.TabIndex = 10;
            this.lblLyDoChi.Text = "Lý Do Chi:";
            // 
            // txtLyDoChi
            // 
            this.txtLyDoChi.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLyDoChi.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtLyDoChi.Location = new System.Drawing.Point(140, 172);
            this.txtLyDoChi.Multiline = true;
            this.txtLyDoChi.Name = "txtLyDoChi";
            this.txtLyDoChi.Size = new System.Drawing.Size(780, 60);
            this.txtLyDoChi.TabIndex = 11;
            // 
            // lblGhiChu
            // 
            this.lblGhiChu.AutoSize = true;
            this.lblGhiChu.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblGhiChu.Location = new System.Drawing.Point(20, 250);
            this.lblGhiChu.Name = "lblGhiChu";
            this.lblGhiChu.Size = new System.Drawing.Size(68, 21);
            this.lblGhiChu.TabIndex = 12;
            this.lblGhiChu.Text = "Ghi Chú:";
            // 
            // txtGhiChu
            // 
            this.txtGhiChu.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtGhiChu.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtGhiChu.Location = new System.Drawing.Point(140, 247);
            this.txtGhiChu.Multiline = true;
            this.txtGhiChu.Name = "txtGhiChu";
            this.txtGhiChu.Size = new System.Drawing.Size(780, 60);
            this.txtGhiChu.TabIndex = 13;
            // 
            // pnlButtons
            // 
            this.pnlButtons.Controls.Add(this.btnLamMoi);
            this.pnlButtons.Controls.Add(this.btnLuuPhieu);
            this.pnlButtons.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlButtons.Location = new System.Drawing.Point(0, 360);
            this.pnlButtons.Name = "pnlButtons";
            this.pnlButtons.Size = new System.Drawing.Size(956, 60);
            this.pnlButtons.TabIndex = 1;
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
            this.btnLuuPhieu.BackColor = System.Drawing.Color.FromArgb(244, 63, 94);
            this.btnLuuPhieu.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnLuuPhieu.ForeColor = System.Drawing.Color.White;
            this.btnLuuPhieu.Location = new System.Drawing.Point(140, 10);
            this.btnLuuPhieu.Name = "btnLuuPhieu";
            this.btnLuuPhieu.Size = new System.Drawing.Size(180, 40);
            this.btnLuuPhieu.TabIndex = 0;
            this.btnLuuPhieu.Text = "Lưu Phiếu Chi";
            this.btnLuuPhieu.UseVisualStyleBackColor = false;
            this.btnLuuPhieu.Click += new System.EventHandler(this.btnLuuPhieu_Click);
            // 
            // tabDanhSach
            // 
            this.tabDanhSach.Controls.Add(this.dgvDanhSachPhieuChi);
            this.tabDanhSach.Controls.Add(this.lblStatus);
            this.tabDanhSach.Controls.Add(this.pnlFilter);
            this.tabDanhSach.Location = new System.Drawing.Point(4, 30);
            this.tabDanhSach.Name = "tabDanhSach";
            this.tabDanhSach.Padding = new System.Windows.Forms.Padding(10);
            this.tabDanhSach.Size = new System.Drawing.Size(976, 577);
            this.tabDanhSach.TabIndex = 1;
            this.tabDanhSach.Text = "Danh Sách Phiếu Chi";
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
            // dgvDanhSachPhieuChi
            // 
            this.dgvDanhSachPhieuChi.AllowUserToAddRows = false;
            this.dgvDanhSachPhieuChi.AllowUserToDeleteRows = false;
            this.dgvDanhSachPhieuChi.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDanhSachPhieuChi.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDanhSachPhieuChi.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colMaPC,
            this.colNgayChi,
            this.colNguoiNhan,
            this.colSoTien,
            this.colHinhThuc,
            this.colLyDoChi,
            this.colTenNV,
            this.colGhiChu});
            this.dgvDanhSachPhieuChi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDanhSachPhieuChi.Location = new System.Drawing.Point(10, 70);
            this.dgvDanhSachPhieuChi.MultiSelect = false;
            this.dgvDanhSachPhieuChi.Name = "dgvDanhSachPhieuChi";
            this.dgvDanhSachPhieuChi.ReadOnly = true;
            this.dgvDanhSachPhieuChi.RowHeadersWidth = 30;
            this.dgvDanhSachPhieuChi.RowTemplate.Height = 28;
            this.dgvDanhSachPhieuChi.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDanhSachPhieuChi.Size = new System.Drawing.Size(956, 467);
            this.dgvDanhSachPhieuChi.TabIndex = 1;
            // 
            // colMaPC
            // 
            this.colMaPC.DataPropertyName = "MaPC";
            this.colMaPC.FillWeight = 85F;
            this.colMaPC.HeaderText = "Mã PC";
            this.colMaPC.MinimumWidth = 80;
            this.colMaPC.Name = "colMaPC";
            this.colMaPC.ReadOnly = true;
            // 
            // colNgayChi
            // 
            this.colNgayChi.DataPropertyName = "NgayChi";
            this.colNgayChi.FillWeight = 90F;
            this.colNgayChi.HeaderText = "Ngày Chi";
            this.colNgayChi.MinimumWidth = 85;
            this.colNgayChi.Name = "colNgayChi";
            this.colNgayChi.ReadOnly = true;
            // 
            // colNguoiNhan
            // 
            this.colNguoiNhan.DataPropertyName = "NguoiNhan";
            this.colNguoiNhan.FillWeight = 110F;
            this.colNguoiNhan.HeaderText = "Người Nhận";
            this.colNguoiNhan.MinimumWidth = 100;
            this.colNguoiNhan.Name = "colNguoiNhan";
            this.colNguoiNhan.ReadOnly = true;
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
            // colLyDoChi
            // 
            this.colLyDoChi.DataPropertyName = "LyDoChi";
            this.colLyDoChi.FillWeight = 130F;
            this.colLyDoChi.HeaderText = "Lý Do Chi";
            this.colLyDoChi.MinimumWidth = 120;
            this.colLyDoChi.Name = "colLyDoChi";
            this.colLyDoChi.ReadOnly = true;
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
            this.lblStatus.Text = "Tổng số phiếu chi: 0 phiếu";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // frmPhieuChi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 611);
            this.Controls.Add(this.tabControlPhieuChi);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "frmPhieuChi";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Quản Lý Phiếu Chi Tiền";
            this.Load += new System.EventHandler(this.frmPhieuChi_Load);
            this.tabControlPhieuChi.ResumeLayout(false);
            this.tabLapPhieu.ResumeLayout(false);
            this.pnlLapPhieu.ResumeLayout(false);
            this.groupBoxPhieuChi.ResumeLayout(false);
            this.groupBoxPhieuChi.PerformLayout();
            this.pnlButtons.ResumeLayout(false);
            this.tabDanhSach.ResumeLayout(false);
            this.pnlFilter.ResumeLayout(false);
            this.pnlFilter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDanhSachPhieuChi)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControlPhieuChi;
        private System.Windows.Forms.TabPage tabLapPhieu;
        private System.Windows.Forms.Panel pnlLapPhieu;
        private System.Windows.Forms.GroupBox groupBoxPhieuChi;
        private System.Windows.Forms.Label lblMaPC;
        private System.Windows.Forms.TextBox txtMaPC;
        private System.Windows.Forms.Label lblNgayChi;
        private System.Windows.Forms.DateTimePicker dtpNgayChi;
        private System.Windows.Forms.Label lblNguoiNhan;
        private System.Windows.Forms.TextBox txtNguoiNhan;
        private System.Windows.Forms.Label lblSoTien;
        private System.Windows.Forms.TextBox txtSoTien;
        private System.Windows.Forms.Label lblHinhThuc;
        private System.Windows.Forms.ComboBox cboHinhThuc;
        private System.Windows.Forms.Label lblLyDoChi;
        private System.Windows.Forms.TextBox txtLyDoChi;
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
        private System.Windows.Forms.DataGridView dgvDanhSachPhieuChi;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMaPC;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNgayChi;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNguoiNhan;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSoTien;
        private System.Windows.Forms.DataGridViewTextBoxColumn colHinhThuc;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLyDoChi;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTenNV;
        private System.Windows.Forms.DataGridViewTextBoxColumn colGhiChu;
        private System.Windows.Forms.Label lblStatus;
    }
}
