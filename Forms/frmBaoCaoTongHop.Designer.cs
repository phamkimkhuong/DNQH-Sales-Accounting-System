namespace DNQH_KeToanBanHang.Forms
{
    partial class frmBaoCaoTongHop
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
            this.lblSubTitle = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlKpiBanner = new System.Windows.Forms.Panel();
            this.pnlKpi1 = new System.Windows.Forms.Panel();
            this.lblKpi1Value = new System.Windows.Forms.Label();
            this.lblKpi1Title = new System.Windows.Forms.Label();
            this.pnlKpi2 = new System.Windows.Forms.Panel();
            this.lblKpi2Value = new System.Windows.Forms.Label();
            this.lblKpi2Title = new System.Windows.Forms.Label();
            this.pnlKpi3 = new System.Windows.Forms.Panel();
            this.lblKpi3Value = new System.Windows.Forms.Label();
            this.lblKpi3Title = new System.Windows.Forms.Label();
            this.pnlKpi4 = new System.Windows.Forms.Panel();
            this.lblKpi4Value = new System.Windows.Forms.Label();
            this.lblKpi4Title = new System.Windows.Forms.Label();
            this.pnlKpi5 = new System.Windows.Forms.Panel();
            this.lblKpi5Value = new System.Windows.Forms.Label();
            this.lblKpi5Title = new System.Windows.Forms.Label();

            this.tabControlMain = new System.Windows.Forms.TabControl();

            // Tab 1: Doanh Thu
            this.tabDoanhThu = new System.Windows.Forms.TabPage();
            this.pnlFilterDT = new System.Windows.Forms.Panel();
            this.lblTuNgayDT = new System.Windows.Forms.Label();
            this.dtpTuNgayDT = new System.Windows.Forms.DateTimePicker();
            this.lblDenNgayDT = new System.Windows.Forms.Label();
            this.dtpDenNgayDT = new System.Windows.Forms.DateTimePicker();
            this.btnXemDoanhThu = new System.Windows.Forms.Button();
            this.btnXuatCsvDoanhThu = new System.Windows.Forms.Button();
            this.btnInDoanhThu = new System.Windows.Forms.Button();
            this.dgvDoanhThu = new System.Windows.Forms.DataGridView();
            this.pnlSummaryDT = new System.Windows.Forms.Panel();
            this.lblDTTongDoanhThu = new System.Windows.Forms.Label();
            this.lblDTSoHoaDon = new System.Windows.Forms.Label();

            // Tab 2: Thu Chi
            this.tabThuChi = new System.Windows.Forms.TabPage();
            this.pnlFilterTC = new System.Windows.Forms.Panel();
            this.lblTuNgayTC = new System.Windows.Forms.Label();
            this.dtpTuNgayTC = new System.Windows.Forms.DateTimePicker();
            this.lblDenNgayTC = new System.Windows.Forms.Label();
            this.dtpDenNgayTC = new System.Windows.Forms.DateTimePicker();
            this.btnXemThuChi = new System.Windows.Forms.Button();
            this.btnXuatCsvThuChi = new System.Windows.Forms.Button();
            this.btnInThuChi = new System.Windows.Forms.Button();
            this.dgvThuChi = new System.Windows.Forms.DataGridView();
            this.pnlSummaryTC = new System.Windows.Forms.Panel();
            this.lblTCTongThu = new System.Windows.Forms.Label();
            this.lblTCTongChi = new System.Windows.Forms.Label();
            this.lblTCChenhLech = new System.Windows.Forms.Label();

            // Tab 3: Ton Kho
            this.tabTonKho = new System.Windows.Forms.TabPage();
            this.pnlFilterTK = new System.Windows.Forms.Panel();
            this.lblChonKho = new System.Windows.Forms.Label();
            this.cboKho = new System.Windows.Forms.ComboBox();
            this.btnXemTonKho = new System.Windows.Forms.Button();
            this.btnXuatCsvTonKho = new System.Windows.Forms.Button();
            this.btnInTonKho = new System.Windows.Forms.Button();
            this.dgvTonKho = new System.Windows.Forms.DataGridView();
            this.pnlSummaryTK = new System.Windows.Forms.Panel();
            this.lblTKTongTon = new System.Windows.Forms.Label();
            this.lblTKTongGiaTri = new System.Windows.Forms.Label();

            this.pnlHeader.SuspendLayout();
            this.pnlKpiBanner.SuspendLayout();
            this.pnlKpi1.SuspendLayout();
            this.pnlKpi2.SuspendLayout();
            this.pnlKpi3.SuspendLayout();
            this.pnlKpi4.SuspendLayout();
            this.pnlKpi5.SuspendLayout();
            this.tabControlMain.SuspendLayout();
            this.tabDoanhThu.SuspendLayout();
            this.pnlFilterDT.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDoanhThu)).BeginInit();
            this.pnlSummaryDT.SuspendLayout();
            this.tabThuChi.SuspendLayout();
            this.pnlFilterTC.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvThuChi)).BeginInit();
            this.pnlSummaryTC.SuspendLayout();
            this.tabTonKho.SuspendLayout();
            this.pnlFilterTK.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTonKho)).BeginInit();
            this.pnlSummaryTK.SuspendLayout();
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
            this.pnlHeader.Size = new System.Drawing.Size(1100, 60);
            this.pnlHeader.TabIndex = 0;

            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 13.5F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(64)))), ((int)(((byte)(175)))));
            this.lblTitle.Location = new System.Drawing.Point(16, 8);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(430, 25);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "BÁO CÁO TỔNG HỢP & THỐNG KÊ TÀI CHÍNH - KHO";

            // 
            // lblSubTitle
            // 
            this.lblSubTitle.AutoSize = true;
            this.lblSubTitle.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblSubTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblSubTitle.Location = new System.Drawing.Point(17, 34);
            this.lblSubTitle.Name = "lblSubTitle";
            this.lblSubTitle.Size = new System.Drawing.Size(435, 15);
            this.lblSubTitle.TabIndex = 1;
            this.lblSubTitle.Text = "Tổng hợp doanh số bán hàng, dòng tiền thu - chi và quản trị giá trị tồn kho theo kỳ";

            // 
            // pnlKpiBanner
            // 
            this.pnlKpiBanner.BackColor = System.Drawing.Color.FromArgb(241, 245, 249);
            this.pnlKpiBanner.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlKpiBanner.Controls.Add(this.pnlKpi5);
            this.pnlKpiBanner.Controls.Add(this.pnlKpi4);
            this.pnlKpiBanner.Controls.Add(this.pnlKpi3);
            this.pnlKpiBanner.Controls.Add(this.pnlKpi2);
            this.pnlKpiBanner.Controls.Add(this.pnlKpi1);
            this.pnlKpiBanner.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlKpiBanner.Location = new System.Drawing.Point(0, 60);
            this.pnlKpiBanner.Name = "pnlKpiBanner";
            this.pnlKpiBanner.Padding = new System.Windows.Forms.Padding(6);
            this.pnlKpiBanner.Size = new System.Drawing.Size(1100, 68);
            this.pnlKpiBanner.TabIndex = 1;

            // pnlKpi1 (Doanh Thu)
            this.pnlKpi1.BackColor = System.Drawing.Color.White;
            this.pnlKpi1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlKpi1.Controls.Add(this.lblKpi1Value);
            this.pnlKpi1.Controls.Add(this.lblKpi1Title);
            this.pnlKpi1.Location = new System.Drawing.Point(8, 6);
            this.pnlKpi1.Name = "pnlKpi1";
            this.pnlKpi1.Size = new System.Drawing.Size(205, 54);
            this.pnlKpi1.TabIndex = 0;

            this.lblKpi1Title.AutoSize = true;
            this.lblKpi1Title.Font = new System.Drawing.Font("Segoe UI Semibold", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblKpi1Title.ForeColor = System.Drawing.Color.FromArgb(127, 140, 141);
            this.lblKpi1Title.Location = new System.Drawing.Point(8, 6);
            this.lblKpi1Title.Name = "lblKpi1Title";
            this.lblKpi1Title.Size = new System.Drawing.Size(107, 15);
            this.lblKpi1Title.TabIndex = 0;
            this.lblKpi1Title.Text = "TỔNG DOANH THU";

            this.lblKpi1Value.AutoSize = true;
            this.lblKpi1Value.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblKpi1Value.ForeColor = System.Drawing.Color.FromArgb(41, 128, 185);
            this.lblKpi1Value.Location = new System.Drawing.Point(8, 26);
            this.lblKpi1Value.Name = "lblKpi1Value";
            this.lblKpi1Value.Size = new System.Drawing.Size(54, 20);
            this.lblKpi1Value.TabIndex = 1;
            this.lblKpi1Value.Text = "0 VNĐ";

            // pnlKpi2 (Tong Thu)
            this.pnlKpi2.BackColor = System.Drawing.Color.White;
            this.pnlKpi2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlKpi2.Controls.Add(this.lblKpi2Value);
            this.pnlKpi2.Controls.Add(this.lblKpi2Title);
            this.pnlKpi2.Location = new System.Drawing.Point(220, 6);
            this.pnlKpi2.Name = "pnlKpi2";
            this.pnlKpi2.Size = new System.Drawing.Size(205, 54);
            this.pnlKpi2.TabIndex = 1;

            this.lblKpi2Title.AutoSize = true;
            this.lblKpi2Title.Font = new System.Drawing.Font("Segoe UI Semibold", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblKpi2Title.ForeColor = System.Drawing.Color.FromArgb(127, 140, 141);
            this.lblKpi2Title.Location = new System.Drawing.Point(8, 6);
            this.lblKpi2Title.Name = "lblKpi2Title";
            this.lblKpi2Title.Size = new System.Drawing.Size(69, 15);
            this.lblKpi2Title.TabIndex = 0;
            this.lblKpi2Title.Text = "TỔNG THU";

            this.lblKpi2Value.AutoSize = true;
            this.lblKpi2Value.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblKpi2Value.ForeColor = System.Drawing.Color.FromArgb(39, 174, 96);
            this.lblKpi2Value.Location = new System.Drawing.Point(8, 26);
            this.lblKpi2Value.Name = "lblKpi2Value";
            this.lblKpi2Value.Size = new System.Drawing.Size(54, 20);
            this.lblKpi2Value.TabIndex = 1;
            this.lblKpi2Value.Text = "0 VNĐ";

            // pnlKpi3 (Tong Chi)
            this.pnlKpi3.BackColor = System.Drawing.Color.White;
            this.pnlKpi3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlKpi3.Controls.Add(this.lblKpi3Value);
            this.pnlKpi3.Controls.Add(this.lblKpi3Title);
            this.pnlKpi3.Location = new System.Drawing.Point(432, 6);
            this.pnlKpi3.Name = "pnlKpi3";
            this.pnlKpi3.Size = new System.Drawing.Size(205, 54);
            this.pnlKpi3.TabIndex = 2;

            this.lblKpi3Title.AutoSize = true;
            this.lblKpi3Title.Font = new System.Drawing.Font("Segoe UI Semibold", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblKpi3Title.ForeColor = System.Drawing.Color.FromArgb(127, 140, 141);
            this.lblKpi3Title.Location = new System.Drawing.Point(8, 6);
            this.lblKpi3Title.Name = "lblKpi3Title";
            this.lblKpi3Title.Size = new System.Drawing.Size(65, 15);
            this.lblKpi3Title.TabIndex = 0;
            this.lblKpi3Title.Text = "TỔNG CHI";

            this.lblKpi3Value.AutoSize = true;
            this.lblKpi3Value.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblKpi3Value.ForeColor = System.Drawing.Color.FromArgb(192, 57, 43);
            this.lblKpi3Value.Location = new System.Drawing.Point(8, 26);
            this.lblKpi3Value.Name = "lblKpi3Value";
            this.lblKpi3Value.Size = new System.Drawing.Size(54, 20);
            this.lblKpi3Value.TabIndex = 1;
            this.lblKpi3Value.Text = "0 VNĐ";

            // pnlKpi4 (Ton Quy)
            this.pnlKpi4.BackColor = System.Drawing.Color.White;
            this.pnlKpi4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlKpi4.Controls.Add(this.lblKpi4Value);
            this.pnlKpi4.Controls.Add(this.lblKpi4Title);
            this.pnlKpi4.Location = new System.Drawing.Point(644, 6);
            this.pnlKpi4.Name = "pnlKpi4";
            this.pnlKpi4.Size = new System.Drawing.Size(215, 54);
            this.pnlKpi4.TabIndex = 3;

            this.lblKpi4Title.AutoSize = true;
            this.lblKpi4Title.Font = new System.Drawing.Font("Segoe UI Semibold", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblKpi4Title.ForeColor = System.Drawing.Color.FromArgb(127, 140, 141);
            this.lblKpi4Title.Location = new System.Drawing.Point(8, 6);
            this.lblKpi4Title.Name = "lblKpi4Title";
            this.lblKpi4Title.Size = new System.Drawing.Size(150, 15);
            this.lblKpi4Title.TabIndex = 0;
            this.lblKpi4Title.Text = "CHÊNH LỆCH THU - CHI (KỲ)";

            this.lblKpi4Value.AutoSize = true;
            this.lblKpi4Value.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblKpi4Value.ForeColor = System.Drawing.Color.FromArgb(142, 68, 173);
            this.lblKpi4Value.Location = new System.Drawing.Point(8, 26);
            this.lblKpi4Value.Name = "lblKpi4Value";
            this.lblKpi4Value.Size = new System.Drawing.Size(54, 20);
            this.lblKpi4Value.TabIndex = 1;
            this.lblKpi4Value.Text = "0 VNĐ";

            // pnlKpi5 (Ton Kho)
            this.pnlKpi5.BackColor = System.Drawing.Color.White;
            this.pnlKpi5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlKpi5.Controls.Add(this.lblKpi5Value);
            this.pnlKpi5.Controls.Add(this.lblKpi5Title);
            this.pnlKpi5.Location = new System.Drawing.Point(866, 6);
            this.pnlKpi5.Name = "pnlKpi5";
            this.pnlKpi5.Size = new System.Drawing.Size(220, 54);
            this.pnlKpi5.TabIndex = 4;

            this.lblKpi5Title.AutoSize = true;
            this.lblKpi5Title.Font = new System.Drawing.Font("Segoe UI Semibold", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblKpi5Title.ForeColor = System.Drawing.Color.FromArgb(127, 140, 141);
            this.lblKpi5Title.Location = new System.Drawing.Point(8, 6);
            this.lblKpi5Title.Name = "lblKpi5Title";
            this.lblKpi5Title.Size = new System.Drawing.Size(107, 15);
            this.lblKpi5Title.TabIndex = 0;
            this.lblKpi5Title.Text = "GIÁ TRỊ TỒN KHO";

            this.lblKpi5Value.AutoSize = true;
            this.lblKpi5Value.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblKpi5Value.ForeColor = System.Drawing.Color.FromArgb(211, 84, 0);
            this.lblKpi5Value.Location = new System.Drawing.Point(8, 26);
            this.lblKpi5Value.Name = "lblKpi5Value";
            this.lblKpi5Value.Size = new System.Drawing.Size(54, 20);
            this.lblKpi5Value.TabIndex = 1;
            this.lblKpi5Value.Text = "0 VNĐ";

            // 
            // tabControlMain
            // 
            this.tabControlMain.Controls.Add(this.tabDoanhThu);
            this.tabControlMain.Controls.Add(this.tabThuChi);
            this.tabControlMain.Controls.Add(this.tabTonKho);
            this.tabControlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlMain.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular);
            this.tabControlMain.Location = new System.Drawing.Point(0, 128);
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.SelectedIndex = 0;
            this.tabControlMain.Size = new System.Drawing.Size(1100, 572);
            this.tabControlMain.TabIndex = 2;

            // 
            // tabDoanhThu
            // 
            this.tabDoanhThu.Controls.Add(this.dgvDoanhThu);
            this.tabDoanhThu.Controls.Add(this.pnlSummaryDT);
            this.tabDoanhThu.Controls.Add(this.pnlFilterDT);
            this.tabDoanhThu.Location = new System.Drawing.Point(4, 26);
            this.tabDoanhThu.Name = "tabDoanhThu";
            this.tabDoanhThu.Padding = new System.Windows.Forms.Padding(8);
            this.tabDoanhThu.Size = new System.Drawing.Size(1092, 542);
            this.tabDoanhThu.TabIndex = 0;
            this.tabDoanhThu.Text = "1. Báo Cáo Doanh Thu Bán Hàng";
            this.tabDoanhThu.UseVisualStyleBackColor = true;

            // pnlFilterDT
            this.pnlFilterDT.BackColor = System.Drawing.Color.FromArgb(245, 248, 252);
            this.pnlFilterDT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlFilterDT.Controls.Add(this.btnInDoanhThu);
            this.pnlFilterDT.Controls.Add(this.btnXuatCsvDoanhThu);
            this.pnlFilterDT.Controls.Add(this.btnXemDoanhThu);
            this.pnlFilterDT.Controls.Add(this.dtpDenNgayDT);
            this.pnlFilterDT.Controls.Add(this.lblDenNgayDT);
            this.pnlFilterDT.Controls.Add(this.dtpTuNgayDT);
            this.pnlFilterDT.Controls.Add(this.lblTuNgayDT);
            this.pnlFilterDT.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFilterDT.Location = new System.Drawing.Point(8, 8);
            this.pnlFilterDT.Name = "pnlFilterDT";
            this.pnlFilterDT.Size = new System.Drawing.Size(1076, 50);
            this.pnlFilterDT.TabIndex = 0;

            this.lblTuNgayDT.AutoSize = true;
            this.lblTuNgayDT.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblTuNgayDT.Location = new System.Drawing.Point(12, 16);
            this.lblTuNgayDT.Name = "lblTuNgayDT";
            this.lblTuNgayDT.Size = new System.Drawing.Size(53, 15);
            this.lblTuNgayDT.TabIndex = 0;
            this.lblTuNgayDT.Text = "Từ Ngày:";

            this.dtpTuNgayDT.CustomFormat = "dd/MM/yyyy";
            this.dtpTuNgayDT.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTuNgayDT.Location = new System.Drawing.Point(71, 12);
            this.dtpTuNgayDT.Name = "dtpTuNgayDT";
            this.dtpTuNgayDT.Size = new System.Drawing.Size(120, 24);
            this.dtpTuNgayDT.TabIndex = 1;

            this.lblDenNgayDT.AutoSize = true;
            this.lblDenNgayDT.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblDenNgayDT.Location = new System.Drawing.Point(210, 16);
            this.lblDenNgayDT.Name = "lblDenNgayDT";
            this.lblDenNgayDT.Size = new System.Drawing.Size(62, 15);
            this.lblDenNgayDT.TabIndex = 2;
            this.lblDenNgayDT.Text = "Đến Ngày:";

            this.dtpDenNgayDT.CustomFormat = "dd/MM/yyyy";
            this.dtpDenNgayDT.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDenNgayDT.Location = new System.Drawing.Point(278, 12);
            this.dtpDenNgayDT.Name = "dtpDenNgayDT";
            this.dtpDenNgayDT.Size = new System.Drawing.Size(120, 24);
            this.dtpDenNgayDT.TabIndex = 3;

            this.btnXemDoanhThu.BackColor = System.Drawing.Color.FromArgb(30, 64, 175);
            this.btnXemDoanhThu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXemDoanhThu.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.btnXemDoanhThu.ForeColor = System.Drawing.Color.White;
            this.btnXemDoanhThu.Location = new System.Drawing.Point(420, 9);
            this.btnXemDoanhThu.Name = "btnXemDoanhThu";
            this.btnXemDoanhThu.Size = new System.Drawing.Size(110, 30);
            this.btnXemDoanhThu.TabIndex = 4;
            this.btnXemDoanhThu.Text = "Xem Báo Cáo";
            this.btnXemDoanhThu.UseVisualStyleBackColor = false;
            this.btnXemDoanhThu.Click += new System.EventHandler(this.btnXemDoanhThu_Click);

            // btnXuatCsvDoanhThu
            this.btnXuatCsvDoanhThu.BackColor = System.Drawing.Color.FromArgb(39, 174, 96);
            this.btnXuatCsvDoanhThu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXuatCsvDoanhThu.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.btnXuatCsvDoanhThu.ForeColor = System.Drawing.Color.White;
            this.btnXuatCsvDoanhThu.Location = new System.Drawing.Point(540, 9);
            this.btnXuatCsvDoanhThu.Name = "btnXuatCsvDoanhThu";
            this.btnXuatCsvDoanhThu.Size = new System.Drawing.Size(110, 30);
            this.btnXuatCsvDoanhThu.TabIndex = 5;
            this.btnXuatCsvDoanhThu.Text = "Xuất CSV";
            this.btnXuatCsvDoanhThu.UseVisualStyleBackColor = false;
            this.btnXuatCsvDoanhThu.Click += new System.EventHandler(this.btnXuatCsvDoanhThu_Click);

            // btnInDoanhThu
            this.btnInDoanhThu.BackColor = System.Drawing.Color.FromArgb(70, 80, 95);
            this.btnInDoanhThu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInDoanhThu.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.btnInDoanhThu.ForeColor = System.Drawing.Color.White;
            this.btnInDoanhThu.Location = new System.Drawing.Point(660, 9);
            this.btnInDoanhThu.Name = "btnInDoanhThu";
            this.btnInDoanhThu.Size = new System.Drawing.Size(110, 30);
            this.btnInDoanhThu.TabIndex = 6;
            this.btnInDoanhThu.Text = "🖨️ In Báo Cáo";
            this.btnInDoanhThu.UseVisualStyleBackColor = false;
            this.btnInDoanhThu.Click += new System.EventHandler(this.btnInDoanhThu_Click);

            // dgvDoanhThu
            this.dgvDoanhThu.AllowUserToAddRows = false;
            this.dgvDoanhThu.AllowUserToDeleteRows = false;
            this.dgvDoanhThu.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDoanhThu.BackgroundColor = System.Drawing.Color.White;
            this.dgvDoanhThu.ColumnHeadersDefaultCellStyle = dgvHeaderStyle;
            this.dgvDoanhThu.ColumnHeadersHeight = 30;
            this.dgvDoanhThu.DefaultCellStyle = dgvRowStyle;
            this.dgvDoanhThu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDoanhThu.Location = new System.Drawing.Point(8, 58);
            this.dgvDoanhThu.Name = "dgvDoanhThu";
            this.dgvDoanhThu.ReadOnly = true;
            this.dgvDoanhThu.RowHeadersVisible = false;
            this.dgvDoanhThu.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDoanhThu.Size = new System.Drawing.Size(1076, 441);
            this.dgvDoanhThu.TabIndex = 1;

            // pnlSummaryDT
            this.pnlSummaryDT.BackColor = System.Drawing.Color.FromArgb(235, 245, 255);
            this.pnlSummaryDT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlSummaryDT.Controls.Add(this.lblDTSoHoaDon);
            this.pnlSummaryDT.Controls.Add(this.lblDTTongDoanhThu);
            this.pnlSummaryDT.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlSummaryDT.Location = new System.Drawing.Point(8, 499);
            this.pnlSummaryDT.Name = "pnlSummaryDT";
            this.pnlSummaryDT.Size = new System.Drawing.Size(1076, 35);
            this.pnlSummaryDT.TabIndex = 2;

            this.lblDTTongDoanhThu.AutoSize = true;
            this.lblDTTongDoanhThu.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblDTTongDoanhThu.ForeColor = System.Drawing.Color.FromArgb(39, 174, 96);
            this.lblDTTongDoanhThu.Location = new System.Drawing.Point(12, 8);
            this.lblDTTongDoanhThu.Name = "lblDTTongDoanhThu";
            this.lblDTTongDoanhThu.Size = new System.Drawing.Size(175, 17);
            this.lblDTTongDoanhThu.Text = "Tổng Doanh Thu: 0 VNĐ";

            this.lblDTSoHoaDon.AutoSize = true;
            this.lblDTSoHoaDon.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblDTSoHoaDon.Location = new System.Drawing.Point(350, 9);
            this.lblDTSoHoaDon.Name = "lblDTSoHoaDon";
            this.lblDTSoHoaDon.Size = new System.Drawing.Size(130, 15);
            this.lblDTSoHoaDon.TabIndex = 1;
            this.lblDTSoHoaDon.Text = "Số lượng hóa đơn: 0 HĐ";

            // 
            // tabThuChi
            // 
            this.tabThuChi.Controls.Add(this.dgvThuChi);
            this.tabThuChi.Controls.Add(this.pnlSummaryTC);
            this.tabThuChi.Controls.Add(this.pnlFilterTC);
            this.tabThuChi.Location = new System.Drawing.Point(4, 26);
            this.tabThuChi.Name = "tabThuChi";
            this.tabThuChi.Padding = new System.Windows.Forms.Padding(8);
            this.tabThuChi.Size = new System.Drawing.Size(1092, 542);
            this.tabThuChi.TabIndex = 1;
            this.tabThuChi.Text = "2. Báo Cáo Tổng Hợp Thu - Chi (Sổ Quỹ)";
            this.tabThuChi.UseVisualStyleBackColor = true;

            // pnlFilterTC
            this.pnlFilterTC.BackColor = System.Drawing.Color.FromArgb(245, 248, 252);
            this.pnlFilterTC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlFilterTC.Controls.Add(this.btnInThuChi);
            this.pnlFilterTC.Controls.Add(this.btnXuatCsvThuChi);
            this.pnlFilterTC.Controls.Add(this.btnXemThuChi);
            this.pnlFilterTC.Controls.Add(this.dtpDenNgayTC);
            this.pnlFilterTC.Controls.Add(this.lblDenNgayTC);
            this.pnlFilterTC.Controls.Add(this.dtpTuNgayTC);
            this.pnlFilterTC.Controls.Add(this.lblTuNgayTC);
            this.pnlFilterTC.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFilterTC.Location = new System.Drawing.Point(8, 8);
            this.pnlFilterTC.Name = "pnlFilterTC";
            this.pnlFilterTC.Size = new System.Drawing.Size(1076, 50);
            this.pnlFilterTC.TabIndex = 0;

            this.lblTuNgayTC.AutoSize = true;
            this.lblTuNgayTC.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblTuNgayTC.Location = new System.Drawing.Point(12, 16);
            this.lblTuNgayTC.Name = "lblTuNgayTC";
            this.lblTuNgayTC.Size = new System.Drawing.Size(53, 15);
            this.lblTuNgayTC.TabIndex = 0;
            this.lblTuNgayTC.Text = "Từ Ngày:";

            this.dtpTuNgayTC.CustomFormat = "dd/MM/yyyy";
            this.dtpTuNgayTC.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTuNgayTC.Location = new System.Drawing.Point(71, 12);
            this.dtpTuNgayTC.Name = "dtpTuNgayTC";
            this.dtpTuNgayTC.Size = new System.Drawing.Size(120, 24);
            this.dtpTuNgayTC.TabIndex = 1;

            this.lblDenNgayTC.AutoSize = true;
            this.lblDenNgayTC.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblDenNgayTC.Location = new System.Drawing.Point(210, 16);
            this.lblDenNgayTC.Name = "lblDenNgayTC";
            this.lblDenNgayTC.Size = new System.Drawing.Size(62, 15);
            this.lblDenNgayTC.TabIndex = 2;
            this.lblDenNgayTC.Text = "Đến Ngày:";

            this.dtpDenNgayTC.CustomFormat = "dd/MM/yyyy";
            this.dtpDenNgayTC.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDenNgayTC.Location = new System.Drawing.Point(278, 12);
            this.dtpDenNgayTC.Name = "dtpDenNgayTC";
            this.dtpDenNgayTC.Size = new System.Drawing.Size(120, 24);
            this.dtpDenNgayTC.TabIndex = 3;

            this.btnXemThuChi.BackColor = System.Drawing.Color.FromArgb(30, 64, 175);
            this.btnXemThuChi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXemThuChi.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.btnXemThuChi.ForeColor = System.Drawing.Color.White;
            this.btnXemThuChi.Location = new System.Drawing.Point(420, 9);
            this.btnXemThuChi.Name = "btnXemThuChi";
            this.btnXemThuChi.Size = new System.Drawing.Size(110, 30);
            this.btnXemThuChi.TabIndex = 4;
            this.btnXemThuChi.Text = "Xem Báo Cáo";
            this.btnXemThuChi.UseVisualStyleBackColor = false;
            this.btnXemThuChi.Click += new System.EventHandler(this.btnXemThuChi_Click);

            // btnXuatCsvThuChi
            this.btnXuatCsvThuChi.BackColor = System.Drawing.Color.FromArgb(39, 174, 96);
            this.btnXuatCsvThuChi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXuatCsvThuChi.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.btnXuatCsvThuChi.ForeColor = System.Drawing.Color.White;
            this.btnXuatCsvThuChi.Location = new System.Drawing.Point(540, 9);
            this.btnXuatCsvThuChi.Name = "btnXuatCsvThuChi";
            this.btnXuatCsvThuChi.Size = new System.Drawing.Size(110, 30);
            this.btnXuatCsvThuChi.TabIndex = 5;
            this.btnXuatCsvThuChi.Text = "Xuất CSV";
            this.btnXuatCsvThuChi.UseVisualStyleBackColor = false;
            this.btnXuatCsvThuChi.Click += new System.EventHandler(this.btnXuatCsvThuChi_Click);

            // btnInThuChi
            this.btnInThuChi.BackColor = System.Drawing.Color.FromArgb(70, 80, 95);
            this.btnInThuChi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInThuChi.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.btnInThuChi.ForeColor = System.Drawing.Color.White;
            this.btnInThuChi.Location = new System.Drawing.Point(660, 9);
            this.btnInThuChi.Name = "btnInThuChi";
            this.btnInThuChi.Size = new System.Drawing.Size(110, 30);
            this.btnInThuChi.TabIndex = 6;
            this.btnInThuChi.Text = "🖨️ In Báo Cáo";
            this.btnInThuChi.UseVisualStyleBackColor = false;
            this.btnInThuChi.Click += new System.EventHandler(this.btnInThuChi_Click);

            // dgvThuChi
            this.dgvThuChi.AllowUserToAddRows = false;
            this.dgvThuChi.AllowUserToDeleteRows = false;
            this.dgvThuChi.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvThuChi.BackgroundColor = System.Drawing.Color.White;
            this.dgvThuChi.ColumnHeadersDefaultCellStyle = dgvHeaderStyle;
            this.dgvThuChi.ColumnHeadersHeight = 30;
            this.dgvThuChi.DefaultCellStyle = dgvRowStyle;
            this.dgvThuChi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvThuChi.Location = new System.Drawing.Point(8, 58);
            this.dgvThuChi.Name = "dgvThuChi";
            this.dgvThuChi.ReadOnly = true;
            this.dgvThuChi.RowHeadersVisible = false;
            this.dgvThuChi.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvThuChi.Size = new System.Drawing.Size(1076, 441);
            this.dgvThuChi.TabIndex = 1;

            // pnlSummaryTC
            this.pnlSummaryTC.BackColor = System.Drawing.Color.FromArgb(235, 245, 255);
            this.pnlSummaryTC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlSummaryTC.Controls.Add(this.lblTCChenhLech);
            this.pnlSummaryTC.Controls.Add(this.lblTCTongChi);
            this.pnlSummaryTC.Controls.Add(this.lblTCTongThu);
            this.pnlSummaryTC.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlSummaryTC.Location = new System.Drawing.Point(8, 499);
            this.pnlSummaryTC.Name = "pnlSummaryTC";
            this.pnlSummaryTC.Size = new System.Drawing.Size(1076, 35);
            this.pnlSummaryTC.TabIndex = 2;

            this.lblTCTongThu.AutoSize = true;
            this.lblTCTongThu.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblTCTongThu.ForeColor = System.Drawing.Color.FromArgb(39, 174, 96);
            this.lblTCTongThu.Location = new System.Drawing.Point(12, 9);
            this.lblTCTongThu.Name = "lblTCTongThu";
            this.lblTCTongThu.Size = new System.Drawing.Size(127, 15);
            this.lblTCTongThu.TabIndex = 0;
            this.lblTCTongThu.Text = "Tổng Thu Kỳ: 0 VNĐ";

            this.lblTCTongChi.AutoSize = true;
            this.lblTCTongChi.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblTCTongChi.ForeColor = System.Drawing.Color.FromArgb(192, 57, 43);
            this.lblTCTongChi.Location = new System.Drawing.Point(280, 9);
            this.lblTCTongChi.Name = "lblTCTongChi";
            this.lblTCTongChi.Size = new System.Drawing.Size(124, 15);
            this.lblTCTongChi.TabIndex = 1;
            this.lblTCTongChi.Text = "Tổng Chi Kỳ: 0 VNĐ";

            this.lblTCChenhLech.AutoSize = true;
            this.lblTCChenhLech.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblTCChenhLech.ForeColor = System.Drawing.Color.FromArgb(142, 68, 173);
            this.lblTCChenhLech.Location = new System.Drawing.Point(560, 8);
            this.lblTCChenhLech.Name = "lblTCChenhLech";
            this.lblTCChenhLech.Size = new System.Drawing.Size(176, 17);
            this.lblTCChenhLech.TabIndex = 2;
            this.lblTCChenhLech.Text = "Chênh Lệch Thu - Chi: 0 VNĐ";

            // 
            // tabTonKho
            // 
            this.tabTonKho.Controls.Add(this.dgvTonKho);
            this.tabTonKho.Controls.Add(this.pnlSummaryTK);
            this.tabTonKho.Controls.Add(this.pnlFilterTK);
            this.tabTonKho.Location = new System.Drawing.Point(4, 26);
            this.tabTonKho.Name = "tabTonKho";
            this.tabTonKho.Padding = new System.Windows.Forms.Padding(8);
            this.tabTonKho.Size = new System.Drawing.Size(1092, 542);
            this.tabTonKho.TabIndex = 2;
            this.tabTonKho.Text = "3. Báo Cáo Tổng Hợp Tồn Kho";
            this.tabTonKho.UseVisualStyleBackColor = true;

            // pnlFilterTK
            this.pnlFilterTK.BackColor = System.Drawing.Color.FromArgb(245, 248, 252);
            this.pnlFilterTK.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlFilterTK.Controls.Add(this.btnXuatCsvTonKho);
            this.pnlFilterTK.Controls.Add(this.btnXemTonKho);
            this.pnlFilterTK.Controls.Add(this.cboKho);
            this.pnlFilterTK.Controls.Add(this.lblChonKho);
            this.pnlFilterTK.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFilterTK.Location = new System.Drawing.Point(8, 8);
            this.pnlFilterTK.Name = "pnlFilterTK";
            this.pnlFilterTK.Size = new System.Drawing.Size(1076, 50);
            this.pnlFilterTK.TabIndex = 0;

            this.lblChonKho.AutoSize = true;
            this.lblChonKho.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblChonKho.Location = new System.Drawing.Point(12, 16);
            this.lblChonKho.Name = "lblChonKho";
            this.lblChonKho.Size = new System.Drawing.Size(61, 15);
            this.lblChonKho.TabIndex = 0;
            this.lblChonKho.Text = "Chọn Kho:";

            this.cboKho.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboKho.FormattingEnabled = true;
            this.cboKho.Location = new System.Drawing.Point(80, 12);
            this.cboKho.Name = "cboKho";
            this.cboKho.Size = new System.Drawing.Size(250, 25);
            this.cboKho.TabIndex = 1;

            this.btnXemTonKho.BackColor = System.Drawing.Color.FromArgb(30, 64, 175);
            this.btnXemTonKho.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXemTonKho.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.btnXemTonKho.ForeColor = System.Drawing.Color.White;
            this.btnXemTonKho.Location = new System.Drawing.Point(350, 9);
            this.btnXemTonKho.Name = "btnXemTonKho";
            this.btnXemTonKho.Size = new System.Drawing.Size(110, 30);
            this.btnXemTonKho.TabIndex = 2;
            this.btnXemTonKho.Text = "Xem Báo Cáo";
            this.btnXemTonKho.UseVisualStyleBackColor = false;
            this.btnXemTonKho.Click += new System.EventHandler(this.btnXemTonKho_Click);

            // btnXuatCsvTonKho
            this.btnXuatCsvTonKho.BackColor = System.Drawing.Color.FromArgb(39, 174, 96);
            this.btnXuatCsvTonKho.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXuatCsvTonKho.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.btnXuatCsvTonKho.ForeColor = System.Drawing.Color.White;
            this.btnXuatCsvTonKho.Location = new System.Drawing.Point(470, 9);
            this.btnXuatCsvTonKho.Name = "btnXuatCsvTonKho";
            this.btnXuatCsvTonKho.Size = new System.Drawing.Size(110, 30);
            this.btnXuatCsvTonKho.TabIndex = 3;
            this.btnXuatCsvTonKho.Text = "Xuất CSV";
            this.btnXuatCsvTonKho.UseVisualStyleBackColor = false;
            this.btnXuatCsvTonKho.Click += new System.EventHandler(this.btnXuatCsvTonKho_Click);

            // btnInTonKho
            this.btnInTonKho.BackColor = System.Drawing.Color.FromArgb(70, 80, 95);
            this.btnInTonKho.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInTonKho.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.btnInTonKho.ForeColor = System.Drawing.Color.White;
            this.btnInTonKho.Location = new System.Drawing.Point(590, 9);
            this.btnInTonKho.Name = "btnInTonKho";
            this.btnInTonKho.Size = new System.Drawing.Size(110, 30);
            this.btnInTonKho.TabIndex = 4;
            this.btnInTonKho.Text = "🖨️ In Báo Cáo";
            this.btnInTonKho.UseVisualStyleBackColor = false;
            this.btnInTonKho.Click += new System.EventHandler(this.btnInTonKho_Click);

            // dgvTonKho
            this.dgvTonKho.AllowUserToAddRows = false;
            this.dgvTonKho.AllowUserToDeleteRows = false;
            this.dgvTonKho.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTonKho.BackgroundColor = System.Drawing.Color.White;
            this.dgvTonKho.ColumnHeadersDefaultCellStyle = dgvHeaderStyle;
            this.dgvTonKho.ColumnHeadersHeight = 30;
            this.dgvTonKho.DefaultCellStyle = dgvRowStyle;
            this.dgvTonKho.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTonKho.Location = new System.Drawing.Point(8, 58);
            this.dgvTonKho.Name = "dgvTonKho";
            this.dgvTonKho.ReadOnly = true;
            this.dgvTonKho.RowHeadersVisible = false;
            this.dgvTonKho.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTonKho.Size = new System.Drawing.Size(1076, 441);
            this.dgvTonKho.TabIndex = 1;

            // pnlSummaryTK
            this.pnlSummaryTK.BackColor = System.Drawing.Color.FromArgb(235, 245, 255);
            this.pnlSummaryTK.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlSummaryTK.Controls.Add(this.lblTKTongGiaTri);
            this.pnlSummaryTK.Controls.Add(this.lblTKTongTon);
            this.pnlSummaryTK.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlSummaryTK.Location = new System.Drawing.Point(8, 499);
            this.pnlSummaryTK.Name = "pnlSummaryTK";
            this.pnlSummaryTK.Size = new System.Drawing.Size(1076, 35);
            this.pnlSummaryTK.TabIndex = 2;

            this.lblTKTongTon.AutoSize = true;
            this.lblTKTongTon.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblTKTongTon.Location = new System.Drawing.Point(12, 9);
            this.lblTKTongTon.Name = "lblTKTongTon";
            this.lblTKTongTon.Size = new System.Drawing.Size(170, 15);
            this.lblTKTongTon.TabIndex = 0;
            this.lblTKTongTon.Text = "Tổng số lượng tồn: 0 sản phẩm";

            this.lblTKTongGiaTri.AutoSize = true;
            this.lblTKTongGiaTri.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblTKTongGiaTri.ForeColor = System.Drawing.Color.FromArgb(211, 84, 0);
            this.lblTKTongGiaTri.Location = new System.Drawing.Point(350, 8);
            this.lblTKTongGiaTri.Name = "lblTKTongGiaTri";
            this.lblTKTongGiaTri.Size = new System.Drawing.Size(193, 17);
            this.lblTKTongGiaTri.TabIndex = 1;
            this.lblTKTongGiaTri.Text = "Tổng giá trị tồn kho: 0 VNĐ";

            // 
            // frmBaoCaoTongHop
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1100, 700);
            this.Controls.Add(this.tabControlMain);
            this.Controls.Add(this.pnlKpiBanner);
            this.Controls.Add(this.pnlHeader);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "frmBaoCaoTongHop";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Báo Cáo Tổng Hợp & Thống Kê Tài Chính - Kho";
            this.Load += new System.EventHandler(this.frmBaoCaoTongHop_Load);

            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlKpiBanner.ResumeLayout(false);
            this.pnlKpi1.ResumeLayout(false);
            this.pnlKpi1.PerformLayout();
            this.pnlKpi2.ResumeLayout(false);
            this.pnlKpi2.PerformLayout();
            this.pnlKpi3.ResumeLayout(false);
            this.pnlKpi3.PerformLayout();
            this.pnlKpi4.ResumeLayout(false);
            this.pnlKpi4.PerformLayout();
            this.pnlKpi5.ResumeLayout(false);
            this.pnlKpi5.PerformLayout();
            this.tabControlMain.ResumeLayout(false);
            this.tabDoanhThu.ResumeLayout(false);
            this.pnlFilterDT.ResumeLayout(false);
            this.pnlFilterDT.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDoanhThu)).EndInit();
            this.pnlSummaryDT.ResumeLayout(false);
            this.pnlSummaryDT.PerformLayout();
            this.tabThuChi.ResumeLayout(false);
            this.pnlFilterTC.ResumeLayout(false);
            this.pnlFilterTC.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvThuChi)).EndInit();
            this.pnlSummaryTC.ResumeLayout(false);
            this.pnlSummaryTC.PerformLayout();
            this.tabTonKho.ResumeLayout(false);
            this.pnlFilterTK.ResumeLayout(false);
            this.pnlFilterTK.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTonKho)).EndInit();
            this.pnlSummaryTK.ResumeLayout(false);
            this.pnlSummaryTK.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubTitle;
        private System.Windows.Forms.Panel pnlKpiBanner;
        private System.Windows.Forms.Panel pnlKpi1;
        private System.Windows.Forms.Label lblKpi1Value;
        private System.Windows.Forms.Label lblKpi1Title;
        private System.Windows.Forms.Panel pnlKpi2;
        private System.Windows.Forms.Label lblKpi2Value;
        private System.Windows.Forms.Label lblKpi2Title;
        private System.Windows.Forms.Panel pnlKpi3;
        private System.Windows.Forms.Label lblKpi3Value;
        private System.Windows.Forms.Label lblKpi3Title;
        private System.Windows.Forms.Panel pnlKpi4;
        private System.Windows.Forms.Label lblKpi4Value;
        private System.Windows.Forms.Label lblKpi4Title;
        private System.Windows.Forms.Panel pnlKpi5;
        private System.Windows.Forms.Label lblKpi5Value;
        private System.Windows.Forms.Label lblKpi5Title;

        private System.Windows.Forms.TabControl tabControlMain;

        // Tab DT
        private System.Windows.Forms.TabPage tabDoanhThu;
        private System.Windows.Forms.Panel pnlFilterDT;
        private System.Windows.Forms.Label lblTuNgayDT;
        private System.Windows.Forms.DateTimePicker dtpTuNgayDT;
        private System.Windows.Forms.Label lblDenNgayDT;
        private System.Windows.Forms.DateTimePicker dtpDenNgayDT;
        private System.Windows.Forms.Button btnXemDoanhThu;
        private System.Windows.Forms.Button btnXuatCsvDoanhThu;
        private System.Windows.Forms.Button btnInDoanhThu;
        private System.Windows.Forms.DataGridView dgvDoanhThu;
        private System.Windows.Forms.Panel pnlSummaryDT;
        private System.Windows.Forms.Label lblDTTongDoanhThu;
        private System.Windows.Forms.Label lblDTSoHoaDon;

        // Tab TC
        private System.Windows.Forms.TabPage tabThuChi;
        private System.Windows.Forms.Panel pnlFilterTC;
        private System.Windows.Forms.Label lblTuNgayTC;
        private System.Windows.Forms.DateTimePicker dtpTuNgayTC;
        private System.Windows.Forms.Label lblDenNgayTC;
        private System.Windows.Forms.DateTimePicker dtpDenNgayTC;
        private System.Windows.Forms.Button btnXemThuChi;
        private System.Windows.Forms.Button btnXuatCsvThuChi;
        private System.Windows.Forms.Button btnInThuChi;
        private System.Windows.Forms.DataGridView dgvThuChi;
        private System.Windows.Forms.Panel pnlSummaryTC;
        private System.Windows.Forms.Label lblTCTongThu;
        private System.Windows.Forms.Label lblTCTongChi;
        private System.Windows.Forms.Label lblTCChenhLech;

        // Tab TK
        private System.Windows.Forms.TabPage tabTonKho;
        private System.Windows.Forms.Panel pnlFilterTK;
        private System.Windows.Forms.Label lblChonKho;
        private System.Windows.Forms.ComboBox cboKho;
        private System.Windows.Forms.Button btnXemTonKho;
        private System.Windows.Forms.Button btnXuatCsvTonKho;
        private System.Windows.Forms.Button btnInTonKho;
        private System.Windows.Forms.DataGridView dgvTonKho;
        private System.Windows.Forms.Panel pnlSummaryTK;
        private System.Windows.Forms.Label lblTKTongTon;
        private System.Windows.Forms.Label lblTKTongGiaTri;
    }
    }
