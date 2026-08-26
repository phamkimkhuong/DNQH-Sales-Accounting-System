namespace DNQH_KeToanBanHang.Forms
{
    partial class frmNhatKyHoatDong
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

        private void InitializeComponent()
        {
            this.pnlTopHeader = new System.Windows.Forms.Panel();
            this.lblSubTitle = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnDonDepLog = new System.Windows.Forms.Button();
            this.btnLamMoi = new System.Windows.Forms.Button();
            this.pnlFilter = new System.Windows.Forms.Panel();
            this.lblKhoangNgay = new System.Windows.Forms.Label();
            this.cboKhoangThoiGian = new System.Windows.Forms.ComboBox();
            this.lblTuNgay = new System.Windows.Forms.Label();
            this.dtpTuNgay = new System.Windows.Forms.DateTimePicker();
            this.lblDenNgay = new System.Windows.Forms.Label();
            this.dtpDenNgay = new System.Windows.Forms.DateTimePicker();
            this.lblNhanVien = new System.Windows.Forms.Label();
            this.cboNhanVien = new System.Windows.Forms.ComboBox();
            this.lblCapDo = new System.Windows.Forms.Label();
            this.cboCapDo = new System.Windows.Forms.ComboBox();
            this.lblTuKhoa = new System.Windows.Forms.Label();
            this.txtTuKhoa = new System.Windows.Forms.TextBox();
            this.btnTraCuu = new System.Windows.Forms.Button();
            this.dgvNhatKy = new System.Windows.Forms.DataGridView();
            this.colSTT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colThoiGian = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCapDo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNhanVien = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colVaiTro = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTenMay = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colHanhDong = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMaDoiTuong = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colKetQua = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colThoiGianXuLy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCorrelationId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNoiDung = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlDetail = new System.Windows.Forms.Panel();
            this.lblDetailHeader = new System.Windows.Forms.Label();
            this.lblDetailContext = new System.Windows.Forms.Label();
            this.btnSaoChepCorrelationId = new System.Windows.Forms.Button();
            this.txtChiTietNoiDung = new System.Windows.Forms.TextBox();
            this.pnlBottomStatus = new System.Windows.Forms.Panel();
            this.lblStatusSummary = new System.Windows.Forms.Label();
            this.pnlTopHeader.SuspendLayout();
            this.pnlFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNhatKy)).BeginInit();
            this.pnlDetail.SuspendLayout();
            this.pnlBottomStatus.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTopHeader
            // 
            this.pnlTopHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.pnlTopHeader.Controls.Add(this.lblSubTitle);
            this.pnlTopHeader.Controls.Add(this.lblTitle);
            this.pnlTopHeader.Controls.Add(this.btnDonDepLog);
            this.pnlTopHeader.Controls.Add(this.btnLamMoi);
            this.pnlTopHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTopHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlTopHeader.Name = "pnlTopHeader";
            this.pnlTopHeader.Size = new System.Drawing.Size(1200, 68);
            this.pnlTopHeader.TabIndex = 0;
            // 
            // lblSubTitle
            // 
            this.lblSubTitle.AutoSize = true;
            this.lblSubTitle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSubTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.lblSubTitle.Location = new System.Drawing.Point(20, 38);
            this.lblSubTitle.Name = "lblSubTitle";
            this.lblSubTitle.Size = new System.Drawing.Size(536, 15);
            this.lblSubTitle.TabIndex = 1;
            this.lblSubTitle.Text = "Theo dõi, tra cứu tập trung hoạt động của tất cả các máy trạm theo thời gian thực";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 13.5F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(18, 11);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(515, 25);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "📋 NHẬT KÝ HOẠT ĐỘNG DOANH NGHIỆP (AUDIT TRAIL)";
            // 
            // btnDonDepLog
            // 
            this.btnDonDepLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDonDepLog.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.btnDonDepLog.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDonDepLog.FlatAppearance.BorderSize = 0;
            this.btnDonDepLog.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDonDepLog.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnDonDepLog.ForeColor = System.Drawing.Color.White;
            this.btnDonDepLog.Location = new System.Drawing.Point(920, 17);
            this.btnDonDepLog.Name = "btnDonDepLog";
            this.btnDonDepLog.Size = new System.Drawing.Size(155, 34);
            this.btnDonDepLog.TabIndex = 2;
            this.btnDonDepLog.Text = "🧹 Dọn dẹp log quá hạn";
            this.btnDonDepLog.UseVisualStyleBackColor = false;
            this.btnDonDepLog.Click += new System.EventHandler(this.btnDonDepLog_Click);
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLamMoi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(70)))), ((int)(((byte)(229)))));
            this.btnLamMoi.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLamMoi.FlatAppearance.BorderSize = 0;
            this.btnLamMoi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLamMoi.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnLamMoi.ForeColor = System.Drawing.Color.White;
            this.btnLamMoi.Location = new System.Drawing.Point(1085, 17);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(95, 34);
            this.btnLamMoi.TabIndex = 3;
            this.btnLamMoi.Text = "🔄 Làm mới";
            this.btnLamMoi.UseVisualStyleBackColor = false;
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);
            // 
            // pnlFilter
            // 
            this.pnlFilter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.pnlFilter.Controls.Add(this.lblKhoangNgay);
            this.pnlFilter.Controls.Add(this.cboKhoangThoiGian);
            this.pnlFilter.Controls.Add(this.lblTuNgay);
            this.pnlFilter.Controls.Add(this.dtpTuNgay);
            this.pnlFilter.Controls.Add(this.lblDenNgay);
            this.pnlFilter.Controls.Add(this.dtpDenNgay);
            this.pnlFilter.Controls.Add(this.lblNhanVien);
            this.pnlFilter.Controls.Add(this.cboNhanVien);
            this.pnlFilter.Controls.Add(this.lblCapDo);
            this.pnlFilter.Controls.Add(this.cboCapDo);
            this.pnlFilter.Controls.Add(this.lblTuKhoa);
            this.pnlFilter.Controls.Add(this.txtTuKhoa);
            this.pnlFilter.Controls.Add(this.btnTraCuu);
            this.pnlFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFilter.Location = new System.Drawing.Point(0, 68);
            this.pnlFilter.Name = "pnlFilter";
            this.pnlFilter.Padding = new System.Windows.Forms.Padding(16, 12, 16, 12);
            this.pnlFilter.Size = new System.Drawing.Size(1200, 76);
            this.pnlFilter.TabIndex = 1;
            // 
            // lblKhoangNgay
            // 
            this.lblKhoangNgay.AutoSize = true;
            this.lblKhoangNgay.Font = new System.Drawing.Font("Segoe UI Semibold", 8.5F);
            this.lblKhoangNgay.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblKhoangNgay.Location = new System.Drawing.Point(16, 12);
            this.lblKhoangNgay.Name = "lblKhoangNgay";
            this.lblKhoangNgay.Size = new System.Drawing.Size(76, 15);
            this.lblKhoangNgay.TabIndex = 0;
            this.lblKhoangNgay.Text = "Khoảng ngày:";
            // 
            // cboKhoangThoiGian
            // 
            this.cboKhoangThoiGian.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboKhoangThoiGian.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cboKhoangThoiGian.FormattingEnabled = true;
            this.cboKhoangThoiGian.Items.AddRange(new object[] {
            "Hôm nay",
            "Hôm qua",
            "7 ngày gần nhất",
            "30 ngày gần nhất",
            "Tháng này",
            "Tùy chọn..."});
            this.cboKhoangThoiGian.Location = new System.Drawing.Point(19, 32);
            this.cboKhoangThoiGian.Name = "cboKhoangThoiGian";
            this.cboKhoangThoiGian.Size = new System.Drawing.Size(125, 23);
            this.cboKhoangThoiGian.TabIndex = 1;
            this.cboKhoangThoiGian.SelectedIndexChanged += new System.EventHandler(this.cboKhoangThoiGian_SelectedIndexChanged);
            // 
            // lblTuNgay
            // 
            this.lblTuNgay.AutoSize = true;
            this.lblTuNgay.Font = new System.Drawing.Font("Segoe UI Semibold", 8.5F);
            this.lblTuNgay.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblTuNgay.Location = new System.Drawing.Point(155, 12);
            this.lblTuNgay.Name = "lblTuNgay";
            this.lblTuNgay.Size = new System.Drawing.Size(52, 15);
            this.lblTuNgay.TabIndex = 2;
            this.lblTuNgay.Text = "Từ ngày:";
            // 
            // dtpTuNgay
            // 
            this.dtpTuNgay.CustomFormat = "dd/MM/yyyy";
            this.dtpTuNgay.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpTuNgay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTuNgay.Location = new System.Drawing.Point(158, 32);
            this.dtpTuNgay.Name = "dtpTuNgay";
            this.dtpTuNgay.Size = new System.Drawing.Size(100, 23);
            this.dtpTuNgay.TabIndex = 3;
            // 
            // lblDenNgay
            // 
            this.lblDenNgay.AutoSize = true;
            this.lblDenNgay.Font = new System.Drawing.Font("Segoe UI Semibold", 8.5F);
            this.lblDenNgay.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblDenNgay.Location = new System.Drawing.Point(269, 12);
            this.lblDenNgay.Name = "lblDenNgay";
            this.lblDenNgay.Size = new System.Drawing.Size(60, 15);
            this.lblDenNgay.TabIndex = 4;
            this.lblDenNgay.Text = "Đến ngày:";
            // 
            // dtpDenNgay
            // 
            this.dtpDenNgay.CustomFormat = "dd/MM/yyyy";
            this.dtpDenNgay.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpDenNgay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDenNgay.Location = new System.Drawing.Point(272, 32);
            this.dtpDenNgay.Name = "dtpDenNgay";
            this.dtpDenNgay.Size = new System.Drawing.Size(100, 23);
            this.dtpDenNgay.TabIndex = 5;
            // 
            // lblNhanVien
            // 
            this.lblNhanVien.AutoSize = true;
            this.lblNhanVien.Font = new System.Drawing.Font("Segoe UI Semibold", 8.5F);
            this.lblNhanVien.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblNhanVien.Location = new System.Drawing.Point(385, 12);
            this.lblNhanVien.Name = "lblNhanVien";
            this.lblNhanVien.Size = new System.Drawing.Size(64, 15);
            this.lblNhanVien.TabIndex = 6;
            this.lblNhanVien.Text = "Nhân viên:";
            // 
            // cboNhanVien
            // 
            this.cboNhanVien.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboNhanVien.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cboNhanVien.FormattingEnabled = true;
            this.cboNhanVien.Location = new System.Drawing.Point(388, 32);
            this.cboNhanVien.Name = "cboNhanVien";
            this.cboNhanVien.Size = new System.Drawing.Size(150, 23);
            this.cboNhanVien.TabIndex = 7;
            // 
            // lblCapDo
            // 
            this.lblCapDo.AutoSize = true;
            this.lblCapDo.Font = new System.Drawing.Font("Segoe UI Semibold", 8.5F);
            this.lblCapDo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblCapDo.Location = new System.Drawing.Point(549, 12);
            this.lblCapDo.Name = "lblCapDo";
            this.lblCapDo.Size = new System.Drawing.Size(49, 15);
            this.lblCapDo.TabIndex = 8;
            this.lblCapDo.Text = "Cấp độ:";
            // 
            // cboCapDo
            // 
            this.cboCapDo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCapDo.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cboCapDo.FormattingEnabled = true;
            this.cboCapDo.Items.AddRange(new object[] {
            "-- Tất cả --",
            "INFO (Thông tin)",
            "WARN (Cảnh báo)",
            "ERROR (Lỗi)"});
            this.cboCapDo.Location = new System.Drawing.Point(552, 32);
            this.cboCapDo.Name = "cboCapDo";
            this.cboCapDo.Size = new System.Drawing.Size(125, 23);
            this.cboCapDo.TabIndex = 9;
            // 
            // lblTuKhoa
            // 
            this.lblTuKhoa.AutoSize = true;
            this.lblTuKhoa.Font = new System.Drawing.Font("Segoe UI Semibold", 8.5F);
            this.lblTuKhoa.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblTuKhoa.Location = new System.Drawing.Point(688, 12);
            this.lblTuKhoa.Name = "lblTuKhoa";
            this.lblTuKhoa.Size = new System.Drawing.Size(53, 15);
            this.lblTuKhoa.TabIndex = 10;
            this.lblTuKhoa.Text = "Từ khóa:";
            // 
            // txtTuKhoa
            // 
            this.txtTuKhoa.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTuKhoa.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtTuKhoa.Location = new System.Drawing.Point(691, 32);
            this.txtTuKhoa.Name = "txtTuKhoa";
            this.txtTuKhoa.Size = new System.Drawing.Size(375, 23);
            this.txtTuKhoa.TabIndex = 11;
            this.txtTuKhoa.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTuKhoa_KeyDown);
            // 
            // btnTraCuu
            // 
            this.btnTraCuu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTraCuu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(165)))), ((int)(((byte)(233)))));
            this.btnTraCuu.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTraCuu.FlatAppearance.BorderSize = 0;
            this.btnTraCuu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTraCuu.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnTraCuu.ForeColor = System.Drawing.Color.White;
            this.btnTraCuu.Location = new System.Drawing.Point(1085, 27);
            this.btnTraCuu.Name = "btnTraCuu";
            this.btnTraCuu.Size = new System.Drawing.Size(95, 32);
            this.btnTraCuu.TabIndex = 12;
            this.btnTraCuu.Text = "🔍 Tra cứu";
            this.btnTraCuu.UseVisualStyleBackColor = false;
            this.btnTraCuu.Click += new System.EventHandler(this.btnTraCuu_Click);
            // 
            // dgvNhatKy
            // 
            this.dgvNhatKy.AllowUserToAddRows = false;
            this.dgvNhatKy.AllowUserToDeleteRows = false;
            this.dgvNhatKy.AllowUserToResizeRows = false;
            this.dgvNhatKy.AutoGenerateColumns = false;
            this.dgvNhatKy.BackgroundColor = System.Drawing.Color.White;
            this.dgvNhatKy.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvNhatKy.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvNhatKy.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colSTT,
            this.colThoiGian,
            this.colCapDo,
            this.colNhanVien,
            this.colVaiTro,
            this.colTenMay,
            this.colHanhDong,
            this.colMaDoiTuong,
            this.colKetQua,
            this.colThoiGianXuLy,
            this.colCorrelationId,
            this.colNoiDung});
            this.dgvNhatKy.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvNhatKy.Location = new System.Drawing.Point(0, 144);
            this.dgvNhatKy.MultiSelect = false;
            this.dgvNhatKy.Name = "dgvNhatKy";
            this.dgvNhatKy.ReadOnly = true;
            this.dgvNhatKy.RowHeadersVisible = false;
            this.dgvNhatKy.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvNhatKy.Size = new System.Drawing.Size(1200, 396);
            this.dgvNhatKy.TabIndex = 2;
            this.dgvNhatKy.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvNhatKy_CellFormatting);
            this.dgvNhatKy.SelectionChanged += new System.EventHandler(this.dgvNhatKy_SelectionChanged);
            // 
            // colSTT
            // 
            this.colSTT.DataPropertyName = "STT";
            this.colSTT.HeaderText = "STT";
            this.colSTT.Name = "colSTT";
            this.colSTT.ReadOnly = true;
            this.colSTT.Width = 45;
            // 
            // colThoiGian
            // 
            this.colThoiGian.DataPropertyName = "ThoiGian";
            this.colThoiGian.HeaderText = "Thời Gian";
            this.colThoiGian.Name = "colThoiGian";
            this.colThoiGian.ReadOnly = true;
            this.colThoiGian.Width = 145;
            // 
            // colCapDo
            // 
            this.colCapDo.DataPropertyName = "CapDo";
            this.colCapDo.HeaderText = "Cấp Độ";
            this.colCapDo.Name = "colCapDo";
            this.colCapDo.ReadOnly = true;
            this.colCapDo.Width = 70;
            // 
            // colNhanVien
            // 
            this.colNhanVien.DataPropertyName = "TenNV";
            this.colNhanVien.HeaderText = "Nhân Viên";
            this.colNhanVien.Name = "colNhanVien";
            this.colNhanVien.ReadOnly = true;
            this.colNhanVien.Width = 150;
            // 
            // colVaiTro
            // 
            this.colVaiTro.DataPropertyName = "VaiTro";
            this.colVaiTro.HeaderText = "Vai Trò";
            this.colVaiTro.Name = "colVaiTro";
            this.colVaiTro.ReadOnly = true;
            this.colVaiTro.Width = 120;
            // 
            // colTenMay
            // 
            this.colTenMay.DataPropertyName = "TenMay";
            this.colTenMay.HeaderText = "Máy Trạm (LAN)";
            this.colTenMay.Name = "colTenMay";
            this.colTenMay.ReadOnly = true;
            this.colTenMay.Width = 125;
            // 
            // colHanhDong
            // 
            this.colHanhDong.DataPropertyName = "HanhDong";
            this.colHanhDong.HeaderText = "Hành Động";
            this.colHanhDong.Name = "colHanhDong";
            this.colHanhDong.ReadOnly = true;
            this.colHanhDong.Width = 135;
            // 
            // colMaDoiTuong
            // 
            this.colMaDoiTuong.DataPropertyName = "MaDoiTuong";
            this.colMaDoiTuong.HeaderText = "Đối Tượng";
            this.colMaDoiTuong.Name = "colMaDoiTuong";
            this.colMaDoiTuong.ReadOnly = true;
            this.colMaDoiTuong.Width = 100;
            // 
            // colKetQua
            // 
            this.colKetQua.DataPropertyName = "KetQua";
            this.colKetQua.HeaderText = "Kết Quả";
            this.colKetQua.Name = "colKetQua";
            this.colKetQua.ReadOnly = true;
            this.colKetQua.Width = 90;
            // 
            // colThoiGianXuLy
            // 
            this.colThoiGianXuLy.DataPropertyName = "ThoiGianXuLyMs";
            this.colThoiGianXuLy.HeaderText = "Xử Lý (ms)";
            this.colThoiGianXuLy.Name = "colThoiGianXuLy";
            this.colThoiGianXuLy.ReadOnly = true;
            this.colThoiGianXuLy.Width = 85;
            // 
            // colCorrelationId
            // 
            this.colCorrelationId.DataPropertyName = "CorrelationId";
            this.colCorrelationId.HeaderText = "Mã Truy Vết";
            this.colCorrelationId.Name = "colCorrelationId";
            this.colCorrelationId.ReadOnly = true;
            this.colCorrelationId.Width = 130;
            // 
            // colNoiDung
            // 
            this.colNoiDung.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colNoiDung.DataPropertyName = "NoiDung";
            this.colNoiDung.HeaderText = "Nội Dung Giao Dịch";
            this.colNoiDung.MinimumWidth = 250;
            this.colNoiDung.Name = "colNoiDung";
            this.colNoiDung.ReadOnly = true;
            // 
            // pnlDetail
            // 
            this.pnlDetail.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.pnlDetail.Controls.Add(this.lblDetailHeader);
            this.pnlDetail.Controls.Add(this.lblDetailContext);
            this.pnlDetail.Controls.Add(this.btnSaoChepCorrelationId);
            this.pnlDetail.Controls.Add(this.txtChiTietNoiDung);
            this.pnlDetail.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlDetail.Location = new System.Drawing.Point(0, 540);
            this.pnlDetail.Name = "pnlDetail";
            this.pnlDetail.Padding = new System.Windows.Forms.Padding(16, 8, 16, 8);
            this.pnlDetail.Size = new System.Drawing.Size(1200, 140);
            this.pnlDetail.TabIndex = 3;
            // 
            // lblDetailHeader
            // 
            this.lblDetailHeader.AutoSize = true;
            this.lblDetailHeader.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblDetailHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblDetailHeader.Location = new System.Drawing.Point(16, 9);
            this.lblDetailHeader.Name = "lblDetailHeader";
            this.lblDetailHeader.Size = new System.Drawing.Size(147, 15);
            this.lblDetailHeader.TabIndex = 0;
            this.lblDetailHeader.Text = "CHI TIẾT NHẬT KÝ ĐÃ CHỌN:";
            // 
            // lblDetailContext
            // 
            this.lblDetailContext.AutoSize = true;
            this.lblDetailContext.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblDetailContext.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblDetailContext.Location = new System.Drawing.Point(175, 9);
            this.lblDetailContext.Name = "lblDetailContext";
            this.lblDetailContext.Size = new System.Drawing.Size(325, 15);
            this.lblDetailContext.TabIndex = 1;
            this.lblDetailContext.Text = "Chưa chọn bản ghi nào. Click vào 1 dòng phía trên để xem chi tiết.";
            // 
            // btnSaoChepCorrelationId
            // 
            this.btnSaoChepCorrelationId.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaoChepCorrelationId.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.btnSaoChepCorrelationId.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSaoChepCorrelationId.FlatAppearance.BorderSize = 0;
            this.btnSaoChepCorrelationId.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaoChepCorrelationId.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.btnSaoChepCorrelationId.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.btnSaoChepCorrelationId.Location = new System.Drawing.Point(1040, 5);
            this.btnSaoChepCorrelationId.Name = "btnSaoChepCorrelationId";
            this.btnSaoChepCorrelationId.Size = new System.Drawing.Size(140, 24);
            this.btnSaoChepCorrelationId.TabIndex = 2;
            this.btnSaoChepCorrelationId.Text = "📋 Sao chép mã truy vết";
            this.btnSaoChepCorrelationId.UseVisualStyleBackColor = false;
            this.btnSaoChepCorrelationId.Click += new System.EventHandler(this.btnSaoChepCorrelationId_Click);
            // 
            // txtChiTietNoiDung
            // 
            this.txtChiTietNoiDung.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtChiTietNoiDung.BackColor = System.Drawing.Color.White;
            this.txtChiTietNoiDung.Font = new System.Drawing.Font("Consolas", 9F);
            this.txtChiTietNoiDung.Location = new System.Drawing.Point(19, 32);
            this.txtChiTietNoiDung.Multiline = true;
            this.txtChiTietNoiDung.Name = "txtChiTietNoiDung";
            this.txtChiTietNoiDung.ReadOnly = true;
            this.txtChiTietNoiDung.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtChiTietNoiDung.Size = new System.Drawing.Size(1161, 96);
            this.txtChiTietNoiDung.TabIndex = 3;
            // 
            // pnlBottomStatus
            // 
            this.pnlBottomStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.pnlBottomStatus.Controls.Add(this.lblStatusSummary);
            this.pnlBottomStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottomStatus.Location = new System.Drawing.Point(0, 680);
            this.pnlBottomStatus.Name = "pnlBottomStatus";
            this.pnlBottomStatus.Padding = new System.Windows.Forms.Padding(16, 6, 16, 6);
            this.pnlBottomStatus.Size = new System.Drawing.Size(1200, 30);
            this.pnlBottomStatus.TabIndex = 4;
            // 
            // lblStatusSummary
            // 
            this.lblStatusSummary.AutoSize = true;
            this.lblStatusSummary.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblStatusSummary.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblStatusSummary.Location = new System.Drawing.Point(16, 7);
            this.lblStatusSummary.Name = "lblStatusSummary";
            this.lblStatusSummary.Size = new System.Drawing.Size(155, 15);
            this.lblStatusSummary.TabIndex = 0;
            this.lblStatusSummary.Text = "Đang tải dữ liệu nhật ký...";
            // 
            // frmNhatKyHoatDong
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1200, 710);
            this.Controls.Add(this.dgvNhatKy);
            this.Controls.Add(this.pnlDetail);
            this.Controls.Add(this.pnlBottomStatus);
            this.Controls.Add(this.pnlFilter);
            this.Controls.Add(this.pnlTopHeader);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.MinimumSize = new System.Drawing.Size(1000, 620);
            this.Name = "frmNhatKyHoatDong";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Nhật Ký Hoạt Động Doanh Nghiệp (Audit Trail)";
            this.Load += new System.EventHandler(this.frmNhatKyHoatDong_Load);
            this.pnlTopHeader.ResumeLayout(false);
            this.pnlTopHeader.PerformLayout();
            this.pnlFilter.ResumeLayout(false);
            this.pnlFilter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNhatKy)).EndInit();
            this.pnlDetail.ResumeLayout(false);
            this.pnlDetail.PerformLayout();
            this.pnlBottomStatus.ResumeLayout(false);
            this.pnlBottomStatus.PerformLayout();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Panel pnlTopHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubTitle;
        private System.Windows.Forms.Button btnDonDepLog;
        private System.Windows.Forms.Button btnLamMoi;
        private System.Windows.Forms.Panel pnlFilter;
        private System.Windows.Forms.Label lblKhoangNgay;
        private System.Windows.Forms.ComboBox cboKhoangThoiGian;
        private System.Windows.Forms.Label lblTuNgay;
        private System.Windows.Forms.DateTimePicker dtpTuNgay;
        private System.Windows.Forms.Label lblDenNgay;
        private System.Windows.Forms.DateTimePicker dtpDenNgay;
        private System.Windows.Forms.Label lblNhanVien;
        private System.Windows.Forms.ComboBox cboNhanVien;
        private System.Windows.Forms.Label lblCapDo;
        private System.Windows.Forms.ComboBox cboCapDo;
        private System.Windows.Forms.Label lblTuKhoa;
        private System.Windows.Forms.TextBox txtTuKhoa;
        private System.Windows.Forms.Button btnTraCuu;
        private System.Windows.Forms.DataGridView dgvNhatKy;
        private System.Windows.Forms.Panel pnlDetail;
        private System.Windows.Forms.Label lblDetailHeader;
        private System.Windows.Forms.Label lblDetailContext;
        private System.Windows.Forms.Button btnSaoChepCorrelationId;
        private System.Windows.Forms.TextBox txtChiTietNoiDung;
        private System.Windows.Forms.Panel pnlBottomStatus;
        private System.Windows.Forms.Label lblStatusSummary;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSTT;
        private System.Windows.Forms.DataGridViewTextBoxColumn colThoiGian;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCapDo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNhanVien;
        private System.Windows.Forms.DataGridViewTextBoxColumn colVaiTro;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTenMay;
        private System.Windows.Forms.DataGridViewTextBoxColumn colHanhDong;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMaDoiTuong;
        private System.Windows.Forms.DataGridViewTextBoxColumn colKetQua;
        private System.Windows.Forms.DataGridViewTextBoxColumn colThoiGianXuLy;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCorrelationId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNoiDung;
    }
}
