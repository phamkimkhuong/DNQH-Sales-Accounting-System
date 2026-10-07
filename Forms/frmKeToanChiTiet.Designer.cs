namespace DNQH_KeToanBanHang.Forms
{
    partial class frmKeToanChiTiet
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
            System.Windows.Forms.DataGridViewCellStyle dgvHeaderStyle = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dgvRowStyle = new System.Windows.Forms.DataGridViewCellStyle();

            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSubTitle = new System.Windows.Forms.Label();
            this.tabControlMain = new System.Windows.Forms.TabControl();
            
            // Tab 1: Khach Hang
            this.tabKhachHang = new System.Windows.Forms.TabPage();
            this.pnlFilterKH = new System.Windows.Forms.Panel();
            this.lblChonKH = new System.Windows.Forms.Label();
            this.cboKhachHang = new System.Windows.Forms.ComboBox();
            this.lblTuNgayKH = new System.Windows.Forms.Label();
            this.dtpTuNgayKH = new System.Windows.Forms.DateTimePicker();
            this.lblDenNgayKH = new System.Windows.Forms.Label();
            this.dtpDenNgayKH = new System.Windows.Forms.DateTimePicker();
            this.btnXemKH = new System.Windows.Forms.Button();
            this.btnXuatCsvKH = new System.Windows.Forms.Button();
            this.btnInKH = new System.Windows.Forms.Button();
            this.dgvKhachHang = new System.Windows.Forms.DataGridView();
            this.pnlSummaryKH = new System.Windows.Forms.Panel();
            this.lblKHTongPhatSinhNo = new System.Windows.Forms.Label();
            this.lblKHTongPhatSinhCo = new System.Windows.Forms.Label();
            this.lblKHSoDuCuoiKy = new System.Windows.Forms.Label();

            // Tab 2: San Pham
            this.tabSanPham = new System.Windows.Forms.TabPage();
            this.pnlFilterSP = new System.Windows.Forms.Panel();
            this.lblChonSP = new System.Windows.Forms.Label();
            this.cboSanPham = new System.Windows.Forms.ComboBox();
            this.lblTuNgaySP = new System.Windows.Forms.Label();
            this.dtpTuNgaySP = new System.Windows.Forms.DateTimePicker();
            this.lblDenNgaySP = new System.Windows.Forms.Label();
            this.dtpDenNgaySP = new System.Windows.Forms.DateTimePicker();
            this.btnXemSP = new System.Windows.Forms.Button();
            this.btnXuatCsvSP = new System.Windows.Forms.Button();
            this.btnInSP = new System.Windows.Forms.Button();
            this.dgvSanPham = new System.Windows.Forms.DataGridView();
            this.pnlSummarySP = new System.Windows.Forms.Panel();
            this.lblSPTongSoLuong = new System.Windows.Forms.Label();
            this.lblSPTongDoanhThu = new System.Windows.Forms.Label();

            this.pnlHeader.SuspendLayout();
            this.tabControlMain.SuspendLayout();
            this.tabKhachHang.SuspendLayout();
            this.pnlFilterKH.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvKhachHang)).BeginInit();
            this.pnlSummaryKH.SuspendLayout();
            this.tabSanPham.SuspendLayout();
            this.pnlFilterSP.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSanPham)).BeginInit();
            this.pnlSummarySP.SuspendLayout();
            this.SuspendLayout();

            // 
            // dgv common styling
            // 
            dgvHeaderStyle.BackColor = System.Drawing.Color.FromArgb(41, 128, 185);
            dgvHeaderStyle.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            dgvHeaderStyle.ForeColor = System.Drawing.Color.White;
            dgvHeaderStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;

            dgvRowStyle.Font = new System.Drawing.Font("Segoe UI", 9F);
            dgvRowStyle.SelectionBackColor = System.Drawing.Color.FromArgb(214, 234, 248);
            dgvRowStyle.SelectionForeColor = System.Drawing.Color.Black;

            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.pnlHeader.Controls.Add(this.lblSubTitle);
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1080, 60);
            this.pnlHeader.TabIndex = 0;

            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 13.5F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(64)))), ((int)(((byte)(175)))));
            this.lblTitle.Location = new System.Drawing.Point(16, 8);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(325, 25);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "SỔ KẾ TOÁN CHI TIẾT BÁN HÀNG";

            // 
            // lblSubTitle
            // 
            this.lblSubTitle.AutoSize = true;
            this.lblSubTitle.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblSubTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblSubTitle.Location = new System.Drawing.Point(17, 34);
            this.lblSubTitle.Name = "lblSubTitle";
            this.lblSubTitle.Size = new System.Drawing.Size(430, 15);
            this.lblSubTitle.TabIndex = 1;
            this.lblSubTitle.Text = "Tra cứu chi tiết công nợ khách hàng và doanh số bán hàng sản phẩm";

            // 
            // tabControlMain
            // 
            this.tabControlMain.Controls.Add(this.tabKhachHang);
            this.tabControlMain.Controls.Add(this.tabSanPham);
            this.tabControlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlMain.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular);
            this.tabControlMain.Location = new System.Drawing.Point(0, 60);
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.SelectedIndex = 0;
            this.tabControlMain.Size = new System.Drawing.Size(1080, 640);
            this.tabControlMain.TabIndex = 1;

            // 
            // tabKhachHang
            // 
            this.tabKhachHang.Controls.Add(this.dgvKhachHang);
            this.tabKhachHang.Controls.Add(this.pnlSummaryKH);
            this.tabKhachHang.Controls.Add(this.pnlFilterKH);
            this.tabKhachHang.Location = new System.Drawing.Point(4, 26);
            this.tabKhachHang.Name = "tabKhachHang";
            this.tabKhachHang.Padding = new System.Windows.Forms.Padding(8);
            this.tabKhachHang.Size = new System.Drawing.Size(1072, 610);
            this.tabKhachHang.TabIndex = 0;
            this.tabKhachHang.Text = "1. Sổ Chi Tiết Khách Hàng";
            this.tabKhachHang.UseVisualStyleBackColor = true;

            // pnlFilterKH
            this.pnlFilterKH.BackColor = System.Drawing.Color.FromArgb(245, 248, 252);
            this.pnlFilterKH.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlFilterKH.Controls.Add(this.btnInKH);
            this.pnlFilterKH.Controls.Add(this.btnXuatCsvKH);
            this.pnlFilterKH.Controls.Add(this.btnXemKH);
            this.pnlFilterKH.Controls.Add(this.dtpDenNgayKH);
            this.pnlFilterKH.Controls.Add(this.lblDenNgayKH);
            this.pnlFilterKH.Controls.Add(this.dtpTuNgayKH);
            this.pnlFilterKH.Controls.Add(this.lblTuNgayKH);
            this.pnlFilterKH.Controls.Add(this.cboKhachHang);
            this.pnlFilterKH.Controls.Add(this.lblChonKH);
            this.pnlFilterKH.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFilterKH.Location = new System.Drawing.Point(8, 8);
            this.pnlFilterKH.Name = "pnlFilterKH";
            this.pnlFilterKH.Size = new System.Drawing.Size(1056, 50);
            this.pnlFilterKH.TabIndex = 0;

            this.lblChonKH.AutoSize = true;
            this.lblChonKH.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblChonKH.Location = new System.Drawing.Point(12, 16);
            this.lblChonKH.Name = "lblChonKH";
            this.lblChonKH.Size = new System.Drawing.Size(74, 15);
            this.lblChonKH.Text = "Khách Hàng:";

            this.cboKhachHang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboKhachHang.FormattingEnabled = true;
            this.cboKhachHang.Location = new System.Drawing.Point(92, 12);
            this.cboKhachHang.Name = "cboKhachHang";
            this.cboKhachHang.Size = new System.Drawing.Size(260, 25);
            this.cboKhachHang.TabIndex = 1;

            this.lblTuNgayKH.AutoSize = true;
            this.lblTuNgayKH.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblTuNgayKH.Location = new System.Drawing.Point(375, 16);
            this.lblTuNgayKH.Name = "lblTuNgayKH";
            this.lblTuNgayKH.Size = new System.Drawing.Size(53, 15);
            this.lblTuNgayKH.Text = "Từ Ngày:";

            this.dtpTuNgayKH.CustomFormat = "dd/MM/yyyy";
            this.dtpTuNgayKH.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTuNgayKH.Location = new System.Drawing.Point(434, 12);
            this.dtpTuNgayKH.Name = "dtpTuNgayKH";
            this.dtpTuNgayKH.Size = new System.Drawing.Size(120, 24);
            this.dtpTuNgayKH.TabIndex = 2;

            this.lblDenNgayKH.AutoSize = true;
            this.lblDenNgayKH.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblDenNgayKH.Location = new System.Drawing.Point(575, 16);
            this.lblDenNgayKH.Name = "lblDenNgayKH";
            this.lblDenNgayKH.Size = new System.Drawing.Size(62, 15);
            this.lblDenNgayKH.Text = "Đến Ngày:";

            this.dtpDenNgayKH.CustomFormat = "dd/MM/yyyy";
            this.dtpDenNgayKH.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDenNgayKH.Location = new System.Drawing.Point(643, 12);
            this.dtpDenNgayKH.Name = "dtpDenNgayKH";
            this.dtpDenNgayKH.Size = new System.Drawing.Size(120, 24);
            this.dtpDenNgayKH.TabIndex = 3;

            this.btnXemKH.BackColor = System.Drawing.Color.FromArgb(30, 64, 175);
            this.btnXemKH.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXemKH.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.btnXemKH.ForeColor = System.Drawing.Color.White;
            this.btnXemKH.Location = new System.Drawing.Point(785, 9);
            this.btnXemKH.Name = "btnXemKH";
            this.btnXemKH.Size = new System.Drawing.Size(110, 30);
            this.btnXemKH.TabIndex = 4;
            this.btnXemKH.Text = "Tra Cứu";
            this.btnXemKH.UseVisualStyleBackColor = false;
            this.btnXemKH.Click += new System.EventHandler(this.btnXemKH_Click);

            // btnXuatCsvKH
            this.btnXuatCsvKH.BackColor = System.Drawing.Color.FromArgb(39, 174, 96);
            this.btnXuatCsvKH.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXuatCsvKH.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.btnXuatCsvKH.ForeColor = System.Drawing.Color.White;
            this.btnXuatCsvKH.Location = new System.Drawing.Point(905, 9);
            this.btnXuatCsvKH.Name = "btnXuatCsvKH";
            this.btnXuatCsvKH.Size = new System.Drawing.Size(110, 30);
            this.btnXuatCsvKH.TabIndex = 5;
            this.btnXuatCsvKH.Text = "Xuất CSV";
            this.btnXuatCsvKH.UseVisualStyleBackColor = false;
            this.btnXuatCsvKH.Click += new System.EventHandler(this.btnXuatCsvKH_Click);

            // btnInKH
            this.btnInKH.BackColor = System.Drawing.Color.FromArgb(52, 152, 219);
            this.btnInKH.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInKH.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.btnInKH.ForeColor = System.Drawing.Color.White;
            this.btnInKH.Location = new System.Drawing.Point(1025, 9);
            this.btnInKH.Name = "btnInKH";
            this.btnInKH.Size = new System.Drawing.Size(120, 30);
            this.btnInKH.TabIndex = 6;
            this.btnInKH.Text = "🖨️ In Sổ Chi Tiết";
            this.btnInKH.UseVisualStyleBackColor = false;
            this.btnInKH.Click += new System.EventHandler(this.btnInKH_Click);

            // dgvKhachHang
            this.dgvKhachHang.AllowUserToAddRows = false;
            this.dgvKhachHang.AllowUserToDeleteRows = false;
            this.dgvKhachHang.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvKhachHang.BackgroundColor = System.Drawing.Color.White;
            this.dgvKhachHang.ColumnHeadersDefaultCellStyle = dgvHeaderStyle;
            this.dgvKhachHang.ColumnHeadersHeight = 30;
            this.dgvKhachHang.DefaultCellStyle = dgvRowStyle;
            this.dgvKhachHang.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvKhachHang.Location = new System.Drawing.Point(8, 58);
            this.dgvKhachHang.Name = "dgvKhachHang";
            this.dgvKhachHang.ReadOnly = true;
            this.dgvKhachHang.RowHeadersVisible = false;
            this.dgvKhachHang.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvKhachHang.Size = new System.Drawing.Size(1056, 509);
            this.dgvKhachHang.TabIndex = 1;

            // pnlSummaryKH
            this.pnlSummaryKH.BackColor = System.Drawing.Color.FromArgb(235, 245, 255);
            this.pnlSummaryKH.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlSummaryKH.Controls.Add(this.lblKHSoDuCuoiKy);
            this.pnlSummaryKH.Controls.Add(this.lblKHTongPhatSinhCo);
            this.pnlSummaryKH.Controls.Add(this.lblKHTongPhatSinhNo);
            this.pnlSummaryKH.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlSummaryKH.Location = new System.Drawing.Point(8, 567);
            this.pnlSummaryKH.Name = "pnlSummaryKH";
            this.pnlSummaryKH.Size = new System.Drawing.Size(1056, 35);
            this.pnlSummaryKH.TabIndex = 2;

            this.lblKHTongPhatSinhNo.AutoSize = true;
            this.lblKHTongPhatSinhNo.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblKHTongPhatSinhNo.Location = new System.Drawing.Point(12, 9);
            this.lblKHTongPhatSinhNo.Name = "lblKHTongPhatSinhNo";
            this.lblKHTongPhatSinhNo.Size = new System.Drawing.Size(180, 15);
            this.lblKHTongPhatSinhNo.Text = "Tổng phát sinh nợ: 0 VNĐ";

            this.lblKHTongPhatSinhCo.AutoSize = true;
            this.lblKHTongPhatSinhCo.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblKHTongPhatSinhCo.Location = new System.Drawing.Point(350, 9);
            this.lblKHTongPhatSinhCo.Name = "lblKHTongPhatSinhCo";
            this.lblKHTongPhatSinhCo.Size = new System.Drawing.Size(178, 15);
            this.lblKHTongPhatSinhCo.Text = "Tổng đã thanh toán: 0 VNĐ";

            this.lblKHSoDuCuoiKy.AutoSize = true;
            this.lblKHSoDuCuoiKy.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblKHSoDuCuoiKy.ForeColor = System.Drawing.Color.FromArgb(192, 57, 43);
            this.lblKHSoDuCuoiKy.Location = new System.Drawing.Point(700, 8);
            this.lblKHSoDuCuoiKy.Name = "lblKHSoDuCuoiKy";
            this.lblKHSoDuCuoiKy.Size = new System.Drawing.Size(185, 17);
            this.lblKHSoDuCuoiKy.Text = "Công nợ còn lại: 0 VNĐ";

            // 
            // tabSanPham
            // 
            this.tabSanPham.Controls.Add(this.dgvSanPham);
            this.tabSanPham.Controls.Add(this.pnlSummarySP);
            this.tabSanPham.Controls.Add(this.pnlFilterSP);
            this.tabSanPham.Location = new System.Drawing.Point(4, 26);
            this.tabSanPham.Name = "tabSanPham";
            this.tabSanPham.Padding = new System.Windows.Forms.Padding(8);
            this.tabSanPham.Size = new System.Drawing.Size(1072, 610);
            this.tabSanPham.TabIndex = 1;
            this.tabSanPham.Text = "2. Sổ Chi Tiết Bán Hàng Sản Phẩm";
            this.tabSanPham.UseVisualStyleBackColor = true;

            // pnlFilterSP
            this.pnlFilterSP.BackColor = System.Drawing.Color.FromArgb(245, 248, 252);
            this.pnlFilterSP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlFilterSP.Controls.Add(this.btnInSP);
            this.pnlFilterSP.Controls.Add(this.btnXuatCsvSP);
            this.pnlFilterSP.Controls.Add(this.btnXemSP);
            this.pnlFilterSP.Controls.Add(this.dtpDenNgaySP);
            this.pnlFilterSP.Controls.Add(this.lblDenNgaySP);
            this.pnlFilterSP.Controls.Add(this.dtpTuNgaySP);
            this.pnlFilterSP.Controls.Add(this.lblTuNgaySP);
            this.pnlFilterSP.Controls.Add(this.cboSanPham);
            this.pnlFilterSP.Controls.Add(this.lblChonSP);
            this.pnlFilterSP.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFilterSP.Location = new System.Drawing.Point(8, 8);
            this.pnlFilterSP.Name = "pnlFilterSP";
            this.pnlFilterSP.Size = new System.Drawing.Size(1056, 50);
            this.pnlFilterSP.TabIndex = 0;

            this.lblChonSP.AutoSize = true;
            this.lblChonSP.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblChonSP.Location = new System.Drawing.Point(12, 16);
            this.lblChonSP.Name = "lblChonSP";
            this.lblChonSP.Size = new System.Drawing.Size(64, 15);
            this.lblChonSP.Text = "Sản Phẩm:";

            this.cboSanPham.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSanPham.FormattingEnabled = true;
            this.cboSanPham.Location = new System.Drawing.Point(85, 12);
            this.cboSanPham.Name = "cboSanPham";
            this.cboSanPham.Size = new System.Drawing.Size(265, 25);
            this.cboSanPham.TabIndex = 1;

            this.lblTuNgaySP.AutoSize = true;
            this.lblTuNgaySP.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblTuNgaySP.Location = new System.Drawing.Point(375, 16);
            this.lblTuNgaySP.Name = "lblTuNgaySP";
            this.lblTuNgaySP.Size = new System.Drawing.Size(53, 15);
            this.lblTuNgaySP.Text = "Từ Ngày:";

            this.dtpTuNgaySP.CustomFormat = "dd/MM/yyyy";
            this.dtpTuNgaySP.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTuNgaySP.Location = new System.Drawing.Point(434, 12);
            this.dtpTuNgaySP.Name = "dtpTuNgaySP";
            this.dtpTuNgaySP.Size = new System.Drawing.Size(120, 24);
            this.dtpTuNgaySP.TabIndex = 2;

            this.lblDenNgaySP.AutoSize = true;
            this.lblDenNgaySP.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblDenNgaySP.Location = new System.Drawing.Point(575, 16);
            this.lblDenNgaySP.Name = "lblDenNgaySP";
            this.lblDenNgaySP.Size = new System.Drawing.Size(62, 15);
            this.lblDenNgaySP.Text = "Đến Ngày:";

            this.dtpDenNgaySP.CustomFormat = "dd/MM/yyyy";
            this.dtpDenNgaySP.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDenNgaySP.Location = new System.Drawing.Point(643, 12);
            this.dtpDenNgaySP.Name = "dtpDenNgaySP";
            this.dtpDenNgaySP.Size = new System.Drawing.Size(120, 24);
            this.dtpDenNgaySP.TabIndex = 3;

            this.btnXemSP.BackColor = System.Drawing.Color.FromArgb(30, 64, 175);
            this.btnXemSP.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXemSP.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.btnXemSP.ForeColor = System.Drawing.Color.White;
            this.btnXemSP.Location = new System.Drawing.Point(785, 9);
            this.btnXemSP.Name = "btnXemSP";
            this.btnXemSP.Size = new System.Drawing.Size(110, 30);
            this.btnXemSP.TabIndex = 4;
            this.btnXemSP.Text = "Tra Cứu";
            this.btnXemSP.UseVisualStyleBackColor = false;
            this.btnXemSP.Click += new System.EventHandler(this.btnXemSP_Click);

            // btnXuatCsvSP
            this.btnXuatCsvSP.BackColor = System.Drawing.Color.FromArgb(39, 174, 96);
            this.btnXuatCsvSP.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXuatCsvSP.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.btnXuatCsvSP.ForeColor = System.Drawing.Color.White;
            this.btnXuatCsvSP.Location = new System.Drawing.Point(905, 9);
            this.btnXuatCsvSP.Name = "btnXuatCsvSP";
            this.btnXuatCsvSP.Size = new System.Drawing.Size(110, 30);
            this.btnXuatCsvSP.TabIndex = 5;
            this.btnXuatCsvSP.Text = "Xuất CSV";
            this.btnXuatCsvSP.UseVisualStyleBackColor = false;
            this.btnXuatCsvSP.Click += new System.EventHandler(this.btnXuatCsvSP_Click);

            // btnInSP
            this.btnInSP.BackColor = System.Drawing.Color.FromArgb(52, 152, 219);
            this.btnInSP.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInSP.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.btnInSP.ForeColor = System.Drawing.Color.White;
            this.btnInSP.Location = new System.Drawing.Point(1025, 9);
            this.btnInSP.Name = "btnInSP";
            this.btnInSP.Size = new System.Drawing.Size(120, 30);
            this.btnInSP.TabIndex = 6;
            this.btnInSP.Text = "🖨️ In Sổ Bán Hàng";
            this.btnInSP.UseVisualStyleBackColor = false;
            this.btnInSP.Click += new System.EventHandler(this.btnInSP_Click);

            // dgvSanPham
            this.dgvSanPham.AllowUserToAddRows = false;
            this.dgvSanPham.AllowUserToDeleteRows = false;
            this.dgvSanPham.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSanPham.BackgroundColor = System.Drawing.Color.White;
            this.dgvSanPham.ColumnHeadersDefaultCellStyle = dgvHeaderStyle;
            this.dgvSanPham.ColumnHeadersHeight = 30;
            this.dgvSanPham.DefaultCellStyle = dgvRowStyle;
            this.dgvSanPham.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSanPham.Location = new System.Drawing.Point(8, 58);
            this.dgvSanPham.Name = "dgvSanPham";
            this.dgvSanPham.ReadOnly = true;
            this.dgvSanPham.RowHeadersVisible = false;
            this.dgvSanPham.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSanPham.Size = new System.Drawing.Size(1056, 509);
            this.dgvSanPham.TabIndex = 1;

            // pnlSummarySP
            this.pnlSummarySP.BackColor = System.Drawing.Color.FromArgb(235, 245, 255);
            this.pnlSummarySP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlSummarySP.Controls.Add(this.lblSPTongDoanhThu);
            this.pnlSummarySP.Controls.Add(this.lblSPTongSoLuong);
            this.pnlSummarySP.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlSummarySP.Location = new System.Drawing.Point(8, 567);
            this.pnlSummarySP.Name = "pnlSummarySP";
            this.pnlSummarySP.Size = new System.Drawing.Size(1056, 35);
            this.pnlSummarySP.TabIndex = 2;

            this.lblSPTongSoLuong.AutoSize = true;
            this.lblSPTongSoLuong.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblSPTongSoLuong.Location = new System.Drawing.Point(12, 9);
            this.lblSPTongSoLuong.Name = "lblSPTongSoLuong";
            this.lblSPTongSoLuong.Size = new System.Drawing.Size(148, 15);
            this.lblSPTongSoLuong.Text = "Tổng số lượng bán: 0 SP";

            this.lblSPTongDoanhThu.AutoSize = true;
            this.lblSPTongDoanhThu.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblSPTongDoanhThu.ForeColor = System.Drawing.Color.FromArgb(39, 174, 96);
            this.lblSPTongDoanhThu.Location = new System.Drawing.Point(350, 8);
            this.lblSPTongDoanhThu.Name = "lblSPTongDoanhThu";
            this.lblSPTongDoanhThu.Size = new System.Drawing.Size(188, 17);
            this.lblSPTongDoanhThu.Text = "Tổng doanh số bán: 0 VNĐ";

            // 
            // frmKeToanChiTiet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1080, 700);
            this.Controls.Add(this.tabControlMain);
            this.Controls.Add(this.pnlHeader);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "frmKeToanChiTiet";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sổ Kế Toán Chi Tiết Bán Hàng";
            this.Load += new System.EventHandler(this.frmKeToanChiTiet_Load);

            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.tabControlMain.ResumeLayout(false);
            this.tabKhachHang.ResumeLayout(false);
            this.pnlFilterKH.ResumeLayout(false);
            this.pnlFilterKH.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvKhachHang)).EndInit();
            this.pnlSummaryKH.ResumeLayout(false);
            this.pnlSummaryKH.PerformLayout();
            this.tabSanPham.ResumeLayout(false);
            this.pnlFilterSP.ResumeLayout(false);
            this.pnlFilterSP.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSanPham)).EndInit();
            this.pnlSummarySP.ResumeLayout(false);
            this.pnlSummarySP.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubTitle;
        private System.Windows.Forms.TabControl tabControlMain;
        
        // Tab KH
        private System.Windows.Forms.TabPage tabKhachHang;
        private System.Windows.Forms.Panel pnlFilterKH;
        private System.Windows.Forms.Label lblChonKH;
        private System.Windows.Forms.ComboBox cboKhachHang;
        private System.Windows.Forms.Label lblTuNgayKH;
        private System.Windows.Forms.DateTimePicker dtpTuNgayKH;
        private System.Windows.Forms.Label lblDenNgayKH;
        private System.Windows.Forms.DateTimePicker dtpDenNgayKH;
        private System.Windows.Forms.Button btnXemKH;
        private System.Windows.Forms.Button btnXuatCsvKH;
        private System.Windows.Forms.Button btnInKH;
        private System.Windows.Forms.DataGridView dgvKhachHang;
        private System.Windows.Forms.Panel pnlSummaryKH;
        private System.Windows.Forms.Label lblKHTongPhatSinhNo;
        private System.Windows.Forms.Label lblKHTongPhatSinhCo;
        private System.Windows.Forms.Label lblKHSoDuCuoiKy;

        // Tab SP
        private System.Windows.Forms.TabPage tabSanPham;
        private System.Windows.Forms.Panel pnlFilterSP;
        private System.Windows.Forms.Label lblChonSP;
        private System.Windows.Forms.ComboBox cboSanPham;
        private System.Windows.Forms.Label lblTuNgaySP;
        private System.Windows.Forms.DateTimePicker dtpTuNgaySP;
        private System.Windows.Forms.Label lblDenNgaySP;
        private System.Windows.Forms.DateTimePicker dtpDenNgaySP;
        private System.Windows.Forms.Button btnXemSP;
        private System.Windows.Forms.Button btnXuatCsvSP;
        private System.Windows.Forms.Button btnInSP;
        private System.Windows.Forms.DataGridView dgvSanPham;
        private System.Windows.Forms.Panel pnlSummarySP;
        private System.Windows.Forms.Label lblSPTongSoLuong;
        private System.Windows.Forms.Label lblSPTongDoanhThu;
    }
}
