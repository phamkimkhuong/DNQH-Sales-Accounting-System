namespace DNQH_KeToanBanHang.Forms
{
    partial class frmChungTu
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
            System.Windows.Forms.DataGridViewCellStyle dgvHeaderStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tabControlChungTu = new System.Windows.Forms.TabControl();
            this.tabLapChungTu = new System.Windows.Forms.TabPage();
            this.pnlLapChungTu = new System.Windows.Forms.Panel();
            this.groupBoxChiTiet = new System.Windows.Forms.GroupBox();
            this.dgvChiTiet = new System.Windows.Forms.DataGridView();
            this.colSTT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTaiKhoanNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTaiKhoanCo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSoTienChiTiet = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDienGiaiChiTiet = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlChiTietToolbar = new System.Windows.Forms.Panel();
            this.lblTongNo = new System.Windows.Forms.Label();
            this.lblTongCo = new System.Windows.Forms.Label();
            this.lblCanDoi = new System.Windows.Forms.Label();
            this.btnThemDong = new System.Windows.Forms.Button();
            this.btnXoaDong = new System.Windows.Forms.Button();
            this.btnGoiY = new System.Windows.Forms.Button();
            this.groupBoxThongTinChungTu = new System.Windows.Forms.GroupBox();
            this.lblMaCT = new System.Windows.Forms.Label();
            this.txtMaCT = new System.Windows.Forms.TextBox();
            this.lblNgayCT = new System.Windows.Forms.Label();
            this.dtpNgayCT = new System.Windows.Forms.DateTimePicker();
            this.lblLoaiCT = new System.Windows.Forms.Label();
            this.cboLoaiCT = new System.Windows.Forms.ComboBox();
            this.lblHoaDon = new System.Windows.Forms.Label();
            this.cboHoaDon = new System.Windows.Forms.ComboBox();
            this.lblThongTinHDB = new System.Windows.Forms.Label();
            this.lblDienGiai = new System.Windows.Forms.Label();
            this.txtDienGiai = new System.Windows.Forms.TextBox();
            this.pnlActions = new System.Windows.Forms.Panel();
            this.btnLamMoi = new System.Windows.Forms.Button();
            this.btnLuuChungTu = new System.Windows.Forms.Button();
            this.tabDanhSach = new System.Windows.Forms.TabPage();
            this.splitContainerDS = new System.Windows.Forms.SplitContainer();
            this.pnlFilter = new System.Windows.Forms.Panel();
            this.lblTimKiem = new System.Windows.Forms.Label();
            this.txtTimKiem = new System.Windows.Forms.TextBox();
            this.lblFromDate = new System.Windows.Forms.Label();
            this.dtpFromDate = new System.Windows.Forms.DateTimePicker();
            this.lblToDate = new System.Windows.Forms.Label();
            this.dtpToDate = new System.Windows.Forms.DateTimePicker();
            this.btnTimKiem = new System.Windows.Forms.Button();
            this.btnLamMoiDS = new System.Windows.Forms.Button();
            this.dgvDanhSachChungTu = new System.Windows.Forms.DataGridView();
            this.colMaCT_DS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNgayCT_DS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLoaiCT_DS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMaHDB_DS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTenKH_DS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTongTien_DS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTenNV_DS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDienGiai_DS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblDSStatus = new System.Windows.Forms.Label();
            this.groupBoxXemChiTiet = new System.Windows.Forms.GroupBox();
            this.dgvXemChiTiet = new System.Windows.Forms.DataGridView();
            this.colXemSTT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colXemNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colXemCo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colXemSoTien = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colXemDienGiai = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabControlChungTu.SuspendLayout();
            this.tabLapChungTu.SuspendLayout();
            this.pnlLapChungTu.SuspendLayout();
            this.groupBoxChiTiet.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTiet)).BeginInit();
            this.pnlChiTietToolbar.SuspendLayout();
            this.groupBoxThongTinChungTu.SuspendLayout();
            this.pnlActions.SuspendLayout();
            this.tabDanhSach.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerDS)).BeginInit();
            this.splitContainerDS.Panel1.SuspendLayout();
            this.splitContainerDS.Panel2.SuspendLayout();
            this.splitContainerDS.SuspendLayout();
            this.pnlFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDanhSachChungTu)).BeginInit();
            this.groupBoxXemChiTiet.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvXemChiTiet)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControlChungTu
            // 
            this.tabControlChungTu.Controls.Add(this.tabLapChungTu);
            this.tabControlChungTu.Controls.Add(this.tabDanhSach);
            this.tabControlChungTu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlChungTu.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.tabControlChungTu.Location = new System.Drawing.Point(0, 0);
            this.tabControlChungTu.Name = "tabControlChungTu";
            this.tabControlChungTu.SelectedIndex = 0;
            this.tabControlChungTu.Size = new System.Drawing.Size(1064, 681);
            this.tabControlChungTu.TabIndex = 0;
            // 
            // tabLapChungTu
            // 
            this.tabLapChungTu.Controls.Add(this.pnlLapChungTu);
            this.tabLapChungTu.Location = new System.Drawing.Point(4, 30);
            this.tabLapChungTu.Name = "tabLapChungTu";
            this.tabLapChungTu.Padding = new System.Windows.Forms.Padding(10);
            this.tabLapChungTu.Size = new System.Drawing.Size(1056, 647);
            this.tabLapChungTu.TabIndex = 0;
            this.tabLapChungTu.Text = "Lập Chứng Từ Kế Toán";
            this.tabLapChungTu.UseVisualStyleBackColor = true;
            // 
            // pnlLapChungTu
            // 
            this.pnlLapChungTu.AutoScroll = true;
            this.pnlLapChungTu.Controls.Add(this.groupBoxChiTiet);
            this.pnlLapChungTu.Controls.Add(this.groupBoxThongTinChungTu);
            this.pnlLapChungTu.Controls.Add(this.pnlActions);
            this.pnlLapChungTu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLapChungTu.Location = new System.Drawing.Point(10, 10);
            this.pnlLapChungTu.Name = "pnlLapChungTu";
            this.pnlLapChungTu.Size = new System.Drawing.Size(1036, 627);
            this.pnlLapChungTu.TabIndex = 0;
            // 
            // groupBoxThongTinChungTu
            // 
            this.groupBoxThongTinChungTu.Controls.Add(this.lblMaCT);
            this.groupBoxThongTinChungTu.Controls.Add(this.txtMaCT);
            this.groupBoxThongTinChungTu.Controls.Add(this.lblNgayCT);
            this.groupBoxThongTinChungTu.Controls.Add(this.dtpNgayCT);
            this.groupBoxThongTinChungTu.Controls.Add(this.lblLoaiCT);
            this.groupBoxThongTinChungTu.Controls.Add(this.cboLoaiCT);
            this.groupBoxThongTinChungTu.Controls.Add(this.lblHoaDon);
            this.groupBoxThongTinChungTu.Controls.Add(this.cboHoaDon);
            this.groupBoxThongTinChungTu.Controls.Add(this.lblThongTinHDB);
            this.groupBoxThongTinChungTu.Controls.Add(this.lblDienGiai);
            this.groupBoxThongTinChungTu.Controls.Add(this.txtDienGiai);
            this.groupBoxThongTinChungTu.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxThongTinChungTu.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.groupBoxThongTinChungTu.Location = new System.Drawing.Point(0, 0);
            this.groupBoxThongTinChungTu.Name = "groupBoxThongTinChungTu";
            this.groupBoxThongTinChungTu.Size = new System.Drawing.Size(1036, 175);
            this.groupBoxThongTinChungTu.TabIndex = 0;
            this.groupBoxThongTinChungTu.TabStop = false;
            this.groupBoxThongTinChungTu.Text = "Thông Tin Chứng Từ";
            // 
            // lblMaCT
            // 
            this.lblMaCT.AutoSize = true;
            this.lblMaCT.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblMaCT.Location = new System.Drawing.Point(20, 30);
            this.lblMaCT.Name = "lblMaCT";
            this.lblMaCT.Size = new System.Drawing.Size(102, 21);
            this.lblMaCT.TabIndex = 0;
            this.lblMaCT.Text = "Mã chứng từ:";
            // 
            // txtMaCT
            // 
            this.txtMaCT.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.txtMaCT.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtMaCT.Location = new System.Drawing.Point(135, 27);
            this.txtMaCT.Name = "txtMaCT";
            this.txtMaCT.ReadOnly = true;
            this.txtMaCT.Size = new System.Drawing.Size(160, 29);
            this.txtMaCT.TabIndex = 1;
            // 
            // lblNgayCT
            // 
            this.lblNgayCT.AutoSize = true;
            this.lblNgayCT.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblNgayCT.Location = new System.Drawing.Point(320, 30);
            this.lblNgayCT.Name = "lblNgayCT";
            this.lblNgayCT.Size = new System.Drawing.Size(115, 21);
            this.lblNgayCT.TabIndex = 2;
            this.lblNgayCT.Text = "Ngày chứng từ:";
            // 
            // dtpNgayCT
            // 
            this.dtpNgayCT.CustomFormat = "dd/MM/yyyy HH:mm";
            this.dtpNgayCT.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.dtpNgayCT.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpNgayCT.Location = new System.Drawing.Point(445, 27);
            this.dtpNgayCT.Name = "dtpNgayCT";
            this.dtpNgayCT.Size = new System.Drawing.Size(180, 29);
            this.dtpNgayCT.TabIndex = 3;
            // 
            // lblLoaiCT
            // 
            this.lblLoaiCT.AutoSize = true;
            this.lblLoaiCT.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblLoaiCT.Location = new System.Drawing.Point(650, 30);
            this.lblLoaiCT.Name = "lblLoaiCT";
            this.lblLoaiCT.Size = new System.Drawing.Size(110, 21);
            this.lblLoaiCT.TabIndex = 4;
            this.lblLoaiCT.Text = "Loại chứng từ:";
            // 
            // cboLoaiCT
            // 
            this.cboLoaiCT.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLoaiCT.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.cboLoaiCT.FormattingEnabled = true;
            this.cboLoaiCT.Items.AddRange(new object[] {
            "Chứng từ bán hàng",
            "Ghi nhận doanh thu",
            "Hạch toán tổng hợp"});
            this.cboLoaiCT.Location = new System.Drawing.Point(770, 27);
            this.cboLoaiCT.Name = "cboLoaiCT";
            this.cboLoaiCT.Size = new System.Drawing.Size(240, 29);
            this.cboLoaiCT.TabIndex = 5;
            // 
            // lblHoaDon
            // 
            this.lblHoaDon.AutoSize = true;
            this.lblHoaDon.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblHoaDon.Location = new System.Drawing.Point(20, 75);
            this.lblHoaDon.Name = "lblHoaDon";
            this.lblHoaDon.Size = new System.Drawing.Size(103, 21);
            this.lblHoaDon.TabIndex = 6;
            this.lblHoaDon.Text = "Hóa đơn bán:";
            // 
            // cboHoaDon
            // 
            this.cboHoaDon.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboHoaDon.DropDownWidth = 700;
            this.cboHoaDon.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.cboHoaDon.FormattingEnabled = true;
            this.cboHoaDon.Location = new System.Drawing.Point(135, 72);
            this.cboHoaDon.Name = "cboHoaDon";
            this.cboHoaDon.Size = new System.Drawing.Size(490, 29);
            this.cboHoaDon.TabIndex = 7;
            this.cboHoaDon.SelectedIndexChanged += new System.EventHandler(this.cboHoaDon_SelectedIndexChanged);
            // 
            // lblThongTinHDB
            // 
            this.lblThongTinHDB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblThongTinHDB.AutoEllipsis = true;
            this.lblThongTinHDB.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Italic);
            this.lblThongTinHDB.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(70)))), ((int)(((byte)(140)))));
            this.lblThongTinHDB.Location = new System.Drawing.Point(640, 72);
            this.lblThongTinHDB.Name = "lblThongTinHDB";
            this.lblThongTinHDB.Size = new System.Drawing.Size(370, 29);
            this.lblThongTinHDB.TabIndex = 8;
            this.lblThongTinHDB.Text = "Chi tiết hóa đơn sẽ hiện ở đây";
            // 
            // lblDienGiai
            // 
            this.lblDienGiai.AutoSize = true;
            this.lblDienGiai.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblDienGiai.Location = new System.Drawing.Point(20, 120);
            this.lblDienGiai.Name = "lblDienGiai";
            this.lblDienGiai.Size = new System.Drawing.Size(74, 21);
            this.lblDienGiai.TabIndex = 9;
            this.lblDienGiai.Text = "Diễn giải:";
            // 
            // txtDienGiai
            // 
            this.txtDienGiai.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDienGiai.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtDienGiai.Location = new System.Drawing.Point(135, 117);
            this.txtDienGiai.Name = "txtDienGiai";
            this.txtDienGiai.Size = new System.Drawing.Size(875, 29);
            this.txtDienGiai.TabIndex = 10;
            // 
            // groupBoxChiTiet
            // 
            this.groupBoxChiTiet.Controls.Add(this.dgvChiTiet);
            this.groupBoxChiTiet.Controls.Add(this.pnlChiTietToolbar);
            this.groupBoxChiTiet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxChiTiet.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.groupBoxChiTiet.Location = new System.Drawing.Point(0, 175);
            this.groupBoxChiTiet.Name = "groupBoxChiTiet";
            this.groupBoxChiTiet.Size = new System.Drawing.Size(1036, 392);
            this.groupBoxChiTiet.TabIndex = 1;
            this.groupBoxChiTiet.TabStop = false;
            this.groupBoxChiTiet.Text = "Chi Tiết Định Khoản Kế Toán";
            // 
            // dgvChiTiet
            // 
            this.dgvChiTiet.AllowUserToAddRows = false;
            this.dgvChiTiet.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvChiTiet.BackgroundColor = System.Drawing.Color.White;
            dgvHeaderStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dgvHeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(240)))), ((int)(((byte)(245)))));
            dgvHeaderStyle.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            dgvHeaderStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.dgvChiTiet.ColumnHeadersDefaultCellStyle = dgvHeaderStyle;
            this.dgvChiTiet.ColumnHeadersHeight = 35;
            this.dgvChiTiet.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colSTT,
            this.colTaiKhoanNo,
            this.colTaiKhoanCo,
            this.colSoTienChiTiet,
            this.colDienGiaiChiTiet});
            this.dgvChiTiet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvChiTiet.EnableHeadersVisualStyles = false;
            this.dgvChiTiet.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.dgvChiTiet.Location = new System.Drawing.Point(3, 70);
            this.dgvChiTiet.Name = "dgvChiTiet";
            this.dgvChiTiet.RowHeadersVisible = false;
            this.dgvChiTiet.RowTemplate.Height = 28;
            this.dgvChiTiet.Size = new System.Drawing.Size(1030, 319);
            this.dgvChiTiet.TabIndex = 1;
            this.dgvChiTiet.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvChiTiet_CellValueChanged);
            this.dgvChiTiet.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.dgvChiTiet_RowsRemoved);
            // 
            // colSTT
            // 
            this.colSTT.FillWeight = 15F;
            this.colSTT.HeaderText = "STT";
            this.colSTT.Name = "colSTT";
            this.colSTT.ReadOnly = true;
            // 
            // colTaiKhoanNo
            // 
            this.colTaiKhoanNo.FillWeight = 25F;
            this.colTaiKhoanNo.HeaderText = "TK Nợ";
            this.colTaiKhoanNo.Name = "colTaiKhoanNo";
            // 
            // colTaiKhoanCo
            // 
            this.colTaiKhoanCo.FillWeight = 25F;
            this.colTaiKhoanCo.HeaderText = "TK Có";
            this.colTaiKhoanCo.Name = "colTaiKhoanCo";
            // 
            // colSoTienChiTiet
            // 
            this.colSoTienChiTiet.FillWeight = 35F;
            this.colSoTienChiTiet.HeaderText = "Số tiền (VNĐ)";
            this.colSoTienChiTiet.Name = "colSoTienChiTiet";
            // 
            // colDienGiaiChiTiet
            // 
            this.colDienGiaiChiTiet.FillWeight = 60F;
            this.colDienGiaiChiTiet.HeaderText = "Diễn giải nghiệp vụ";
            this.colDienGiaiChiTiet.Name = "colDienGiaiChiTiet";
            // 
            // pnlChiTietToolbar
            // 
            this.pnlChiTietToolbar.Controls.Add(this.lblTongNo);
            this.pnlChiTietToolbar.Controls.Add(this.lblTongCo);
            this.pnlChiTietToolbar.Controls.Add(this.lblCanDoi);
            this.pnlChiTietToolbar.Controls.Add(this.btnThemDong);
            this.pnlChiTietToolbar.Controls.Add(this.btnXoaDong);
            this.pnlChiTietToolbar.Controls.Add(this.btnGoiY);
            this.pnlChiTietToolbar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlChiTietToolbar.Location = new System.Drawing.Point(3, 25);
            this.pnlChiTietToolbar.Name = "pnlChiTietToolbar";
            this.pnlChiTietToolbar.Size = new System.Drawing.Size(1030, 45);
            this.pnlChiTietToolbar.TabIndex = 0;
            // 
            // btnThemDong
            // 
            this.btnThemDong.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(139)))), ((int)(((byte)(87)))));
            this.btnThemDong.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThemDong.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnThemDong.ForeColor = System.Drawing.Color.White;
            this.btnThemDong.Location = new System.Drawing.Point(5, 6);
            this.btnThemDong.Name = "btnThemDong";
            this.btnThemDong.Size = new System.Drawing.Size(110, 32);
            this.btnThemDong.TabIndex = 0;
            this.btnThemDong.Text = "+ Thêm dòng";
            this.btnThemDong.UseVisualStyleBackColor = false;
            this.btnThemDong.Click += new System.EventHandler(this.btnThemDong_Click);
            // 
            // btnXoaDong
            // 
            this.btnXoaDong.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(57)))), ((int)(((byte)(43)))));
            this.btnXoaDong.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXoaDong.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnXoaDong.ForeColor = System.Drawing.Color.White;
            this.btnXoaDong.Location = new System.Drawing.Point(125, 6);
            this.btnXoaDong.Name = "btnXoaDong";
            this.btnXoaDong.Size = new System.Drawing.Size(95, 32);
            this.btnXoaDong.TabIndex = 1;
            this.btnXoaDong.Text = "Xóa dòng";
            this.btnXoaDong.UseVisualStyleBackColor = false;
            this.btnXoaDong.Click += new System.EventHandler(this.btnXoaDong_Click);
            // 
            // btnGoiY
            // 
            this.btnGoiY.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(58)))), ((int)(((byte)(237)))));
            this.btnGoiY.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGoiY.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnGoiY.ForeColor = System.Drawing.Color.White;
            this.btnGoiY.Location = new System.Drawing.Point(230, 6);
            this.btnGoiY.Name = "btnGoiY";
            this.btnGoiY.Size = new System.Drawing.Size(170, 32);
            this.btnGoiY.TabIndex = 2;
            this.btnGoiY.Text = "Gợi ý định khoản HĐ";
            this.btnGoiY.UseVisualStyleBackColor = false;
            this.btnGoiY.Click += new System.EventHandler(this.btnGoiY_Click);
            // 
            // lblTongNo
            // 
            this.lblTongNo.AutoSize = true;
            this.lblTongNo.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblTongNo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(100)))), ((int)(((byte)(180)))));
            this.lblTongNo.Location = new System.Drawing.Point(420, 12);
            this.lblTongNo.Name = "lblTongNo";
            this.lblTongNo.Size = new System.Drawing.Size(125, 21);
            this.lblTongNo.TabIndex = 3;
            this.lblTongNo.Text = "Tổng Nợ: 0 VNĐ";
            // 
            // lblTongCo
            // 
            this.lblTongCo.AutoSize = true;
            this.lblTongCo.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblTongCo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(100)))), ((int)(((byte)(180)))));
            this.lblTongCo.Location = new System.Drawing.Point(620, 12);
            this.lblTongCo.Name = "lblTongCo";
            this.lblTongCo.Size = new System.Drawing.Size(123, 21);
            this.lblTongCo.TabIndex = 4;
            this.lblTongCo.Text = "Tổng Có: 0 VNĐ";
            // 
            // lblCanDoi
            // 
            this.lblCanDoi.AutoSize = true;
            this.lblCanDoi.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblCanDoi.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(139)))), ((int)(((byte)(87)))));
            this.lblCanDoi.Location = new System.Drawing.Point(820, 12);
            this.lblCanDoi.Name = "lblCanDoi";
            this.lblCanDoi.Size = new System.Drawing.Size(89, 21);
            this.lblCanDoi.TabIndex = 5;
            this.lblCanDoi.Text = "Cân đối: OK";
            // 
            // pnlActions
            // 
            this.pnlActions.Controls.Add(this.btnLamMoi);
            this.pnlActions.Controls.Add(this.btnLuuChungTu);
            this.pnlActions.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlActions.Location = new System.Drawing.Point(0, 567);
            this.pnlActions.Name = "pnlActions";
            this.pnlActions.Size = new System.Drawing.Size(1036, 60);
            this.pnlActions.TabIndex = 2;
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLamMoi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.btnLamMoi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLamMoi.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnLamMoi.ForeColor = System.Drawing.Color.White;
            this.btnLamMoi.Location = new System.Drawing.Point(710, 10);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(140, 40);
            this.btnLamMoi.TabIndex = 0;
            this.btnLamMoi.Text = "Làm Mới";
            this.btnLamMoi.UseVisualStyleBackColor = false;
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);
            // 
            // btnLuuChungTu
            // 
            this.btnLuuChungTu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLuuChungTu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(139)))), ((int)(((byte)(87)))));
            this.btnLuuChungTu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLuuChungTu.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnLuuChungTu.ForeColor = System.Drawing.Color.White;
            this.btnLuuChungTu.Location = new System.Drawing.Point(865, 10);
            this.btnLuuChungTu.Name = "btnLuuChungTu";
            this.btnLuuChungTu.Size = new System.Drawing.Size(160, 40);
            this.btnLuuChungTu.TabIndex = 1;
            this.btnLuuChungTu.Text = "Lưu Chứng Từ";
            this.btnLuuChungTu.UseVisualStyleBackColor = false;
            this.btnLuuChungTu.Click += new System.EventHandler(this.btnLuuChungTu_Click);
            // 
            // tabDanhSach
            // 
            this.tabDanhSach.Controls.Add(this.splitContainerDS);
            this.tabDanhSach.Controls.Add(this.lblDSStatus);
            this.tabDanhSach.Controls.Add(this.pnlFilter);
            this.tabDanhSach.Location = new System.Drawing.Point(4, 30);
            this.tabDanhSach.Name = "tabDanhSach";
            this.tabDanhSach.Padding = new System.Windows.Forms.Padding(10);
            this.tabDanhSach.Size = new System.Drawing.Size(1056, 647);
            this.tabDanhSach.TabIndex = 1;
            this.tabDanhSach.Text = "Danh Sách Chứng Từ Kế Toán";
            this.tabDanhSach.UseVisualStyleBackColor = true;
            // 
            // splitContainerDS
            // 
            this.splitContainerDS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerDS.Location = new System.Drawing.Point(10, 60);
            this.splitContainerDS.Name = "splitContainerDS";
            this.splitContainerDS.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerDS.Panel1
            // 
            this.splitContainerDS.Panel1.Controls.Add(this.dgvDanhSachChungTu);
            // 
            // splitContainerDS.Panel2
            // 
            this.splitContainerDS.Panel2.Controls.Add(this.groupBoxXemChiTiet);
            this.splitContainerDS.Size = new System.Drawing.Size(1036, 552);
            this.splitContainerDS.SplitterDistance = 320;
            this.splitContainerDS.TabIndex = 1;
            // 
            // pnlFilter
            // 
            this.pnlFilter.Controls.Add(this.lblTimKiem);
            this.pnlFilter.Controls.Add(this.txtTimKiem);
            this.pnlFilter.Controls.Add(this.lblFromDate);
            this.pnlFilter.Controls.Add(this.dtpFromDate);
            this.pnlFilter.Controls.Add(this.lblToDate);
            this.pnlFilter.Controls.Add(this.dtpToDate);
            this.pnlFilter.Controls.Add(this.btnTimKiem);
            this.pnlFilter.Controls.Add(this.btnLamMoiDS);
            this.pnlFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFilter.Location = new System.Drawing.Point(10, 10);
            this.pnlFilter.Name = "pnlFilter";
            this.pnlFilter.Size = new System.Drawing.Size(1036, 50);
            this.pnlFilter.TabIndex = 0;
            // 
            // lblTimKiem
            // 
            this.lblTimKiem.AutoSize = true;
            this.lblTimKiem.Location = new System.Drawing.Point(5, 14);
            this.lblTimKiem.Name = "lblTimKiem";
            this.lblTimKiem.Size = new System.Drawing.Size(71, 21);
            this.lblTimKiem.TabIndex = 0;
            this.lblTimKiem.Text = "Từ khóa:";
            // 
            // txtTimKiem
            // 
            this.txtTimKiem.Location = new System.Drawing.Point(82, 11);
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.Size = new System.Drawing.Size(200, 29);
            this.txtTimKiem.TabIndex = 1;
            // 
            // lblFromDate
            // 
            this.lblFromDate.AutoSize = true;
            this.lblFromDate.Location = new System.Drawing.Point(295, 14);
            this.lblFromDate.Name = "lblFromDate";
            this.lblFromDate.Size = new System.Drawing.Size(68, 21);
            this.lblFromDate.TabIndex = 2;
            this.lblFromDate.Text = "Từ ngày:";
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.CustomFormat = "dd/MM/yyyy";
            this.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFromDate.Location = new System.Drawing.Point(369, 11);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.Size = new System.Drawing.Size(120, 29);
            this.dtpFromDate.TabIndex = 3;
            // 
            // lblToDate
            // 
            this.lblToDate.AutoSize = true;
            this.lblToDate.Location = new System.Drawing.Point(500, 14);
            this.lblToDate.Name = "lblToDate";
            this.lblToDate.Size = new System.Drawing.Size(79, 21);
            this.lblToDate.TabIndex = 4;
            this.lblToDate.Text = "Đến ngày:";
            // 
            // dtpToDate
            // 
            this.dtpToDate.CustomFormat = "dd/MM/yyyy";
            this.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpToDate.Location = new System.Drawing.Point(585, 11);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.Size = new System.Drawing.Size(120, 29);
            this.dtpToDate.TabIndex = 5;
            // 
            // btnTimKiem
            // 
            this.btnTimKiem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(58)))), ((int)(((byte)(237)))));
            this.btnTimKiem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTimKiem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnTimKiem.ForeColor = System.Drawing.Color.White;
            this.btnTimKiem.Location = new System.Drawing.Point(720, 9);
            this.btnTimKiem.Name = "btnTimKiem";
            this.btnTimKiem.Size = new System.Drawing.Size(100, 32);
            this.btnTimKiem.TabIndex = 6;
            this.btnTimKiem.Text = "Tìm kiếm";
            this.btnTimKiem.UseVisualStyleBackColor = false;
            this.btnTimKiem.Click += new System.EventHandler(this.btnTimKiem_Click);
            // 
            // btnLamMoiDS
            // 
            this.btnLamMoiDS.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.btnLamMoiDS.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLamMoiDS.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnLamMoiDS.ForeColor = System.Drawing.Color.White;
            this.btnLamMoiDS.Location = new System.Drawing.Point(830, 9);
            this.btnLamMoiDS.Name = "btnLamMoiDS";
            this.btnLamMoiDS.Size = new System.Drawing.Size(95, 32);
            this.btnLamMoiDS.TabIndex = 7;
            this.btnLamMoiDS.Text = "Làm mới";
            this.btnLamMoiDS.UseVisualStyleBackColor = false;
            this.btnLamMoiDS.Click += new System.EventHandler(this.btnLamMoiDS_Click);
            // 
            // dgvDanhSachChungTu
            // 
            this.dgvDanhSachChungTu.AllowUserToAddRows = false;
            this.dgvDanhSachChungTu.AllowUserToDeleteRows = false;
            this.dgvDanhSachChungTu.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDanhSachChungTu.BackgroundColor = System.Drawing.Color.White;
            dgvHeaderStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dgvHeaderStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(240)))), ((int)(((byte)(245)))));
            dgvHeaderStyle2.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.dgvDanhSachChungTu.ColumnHeadersDefaultCellStyle = dgvHeaderStyle2;
            this.dgvDanhSachChungTu.ColumnHeadersHeight = 35;
            this.dgvDanhSachChungTu.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colMaCT_DS,
            this.colNgayCT_DS,
            this.colLoaiCT_DS,
            this.colMaHDB_DS,
            this.colTenKH_DS,
            this.colTongTien_DS,
            this.colTenNV_DS,
            this.colDienGiai_DS});
            this.dgvDanhSachChungTu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDanhSachChungTu.EnableHeadersVisualStyles = false;
            this.dgvDanhSachChungTu.Location = new System.Drawing.Point(0, 0);
            this.dgvDanhSachChungTu.MultiSelect = false;
            this.dgvDanhSachChungTu.Name = "dgvDanhSachChungTu";
            this.dgvDanhSachChungTu.ReadOnly = true;
            this.dgvDanhSachChungTu.RowHeadersVisible = false;
            this.dgvDanhSachChungTu.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDanhSachChungTu.Size = new System.Drawing.Size(1036, 320);
            this.dgvDanhSachChungTu.TabIndex = 0;
            this.dgvDanhSachChungTu.SelectionChanged += new System.EventHandler(this.dgvDanhSachChungTu_SelectionChanged);
            this.dgvDanhSachChungTu.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvDanhSachChungTu_CellFormatting);
            // 
            // colMaCT_DS
            // 
            this.colMaCT_DS.FillWeight = 25F;
            this.colMaCT_DS.HeaderText = "Mã CT";
            this.colMaCT_DS.Name = "colMaCT_DS";
            this.colMaCT_DS.ReadOnly = true;
            // 
            // colNgayCT_DS
            // 
            this.colNgayCT_DS.FillWeight = 28F;
            this.colNgayCT_DS.HeaderText = "Ngày lập";
            this.colNgayCT_DS.Name = "colNgayCT_DS";
            this.colNgayCT_DS.ReadOnly = true;
            // 
            // colLoaiCT_DS
            // 
            this.colLoaiCT_DS.FillWeight = 30F;
            this.colLoaiCT_DS.HeaderText = "Loại chứng từ";
            this.colLoaiCT_DS.Name = "colLoaiCT_DS";
            this.colLoaiCT_DS.ReadOnly = true;
            // 
            // colMaHDB_DS
            // 
            this.colMaHDB_DS.FillWeight = 25F;
            this.colMaHDB_DS.HeaderText = "Mã HĐB";
            this.colMaHDB_DS.Name = "colMaHDB_DS";
            this.colMaHDB_DS.ReadOnly = true;
            // 
            // colTenKH_DS
            // 
            this.colTenKH_DS.FillWeight = 35F;
            this.colTenKH_DS.HeaderText = "Khách hàng";
            this.colTenKH_DS.Name = "colTenKH_DS";
            this.colTenKH_DS.ReadOnly = true;
            // 
            // colTongTien_DS
            // 
            this.colTongTien_DS.FillWeight = 30F;
            this.colTongTien_DS.HeaderText = "Tổng tiền HĐ";
            this.colTongTien_DS.Name = "colTongTien_DS";
            this.colTongTien_DS.ReadOnly = true;
            // 
            // colTenNV_DS
            // 
            this.colTenNV_DS.FillWeight = 30F;
            this.colTenNV_DS.HeaderText = "Kế toán lập";
            this.colTenNV_DS.Name = "colTenNV_DS";
            this.colTenNV_DS.ReadOnly = true;
            // 
            // colDienGiai_DS
            // 
            this.colDienGiai_DS.FillWeight = 50F;
            this.colDienGiai_DS.HeaderText = "Diễn giải";
            this.colDienGiai_DS.Name = "colDienGiai_DS";
            this.colDienGiai_DS.ReadOnly = true;
            // 
            // groupBoxXemChiTiet
            // 
            this.groupBoxXemChiTiet.Controls.Add(this.dgvXemChiTiet);
            this.groupBoxXemChiTiet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxXemChiTiet.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.groupBoxXemChiTiet.Location = new System.Drawing.Point(0, 0);
            this.groupBoxXemChiTiet.Name = "groupBoxXemChiTiet";
            this.groupBoxXemChiTiet.Size = new System.Drawing.Size(1036, 228);
            this.groupBoxXemChiTiet.TabIndex = 0;
            this.groupBoxXemChiTiet.TabStop = false;
            this.groupBoxXemChiTiet.Text = "Chi Tiết Định Khoản Của Chứng Từ Được Chọn";
            // 
            // dgvXemChiTiet
            // 
            this.dgvXemChiTiet.AllowUserToAddRows = false;
            this.dgvXemChiTiet.AllowUserToDeleteRows = false;
            this.dgvXemChiTiet.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvXemChiTiet.BackgroundColor = System.Drawing.Color.White;
            this.dgvXemChiTiet.ColumnHeadersHeight = 30;
            this.dgvXemChiTiet.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colXemSTT,
            this.colXemNo,
            this.colXemCo,
            this.colXemSoTien,
            this.colXemDienGiai});
            this.dgvXemChiTiet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvXemChiTiet.Location = new System.Drawing.Point(3, 25);
            this.dgvXemChiTiet.Name = "dgvXemChiTiet";
            this.dgvXemChiTiet.ReadOnly = true;
            this.dgvXemChiTiet.RowHeadersVisible = false;
            this.dgvXemChiTiet.Size = new System.Drawing.Size(1030, 200);
            this.dgvXemChiTiet.TabIndex = 0;
            this.dgvXemChiTiet.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvXemChiTiet_CellFormatting);
            // 
            // colXemSTT
            // 
            this.colXemSTT.FillWeight = 15F;
            this.colXemSTT.HeaderText = "STT";
            this.colXemSTT.Name = "colXemSTT";
            this.colXemSTT.ReadOnly = true;
            // 
            // colXemNo
            // 
            this.colXemNo.FillWeight = 25F;
            this.colXemNo.HeaderText = "TK Nợ";
            this.colXemNo.Name = "colXemNo";
            this.colXemNo.ReadOnly = true;
            // 
            // colXemCo
            // 
            this.colXemCo.FillWeight = 25F;
            this.colXemCo.HeaderText = "TK Có";
            this.colXemCo.Name = "colXemCo";
            this.colXemCo.ReadOnly = true;
            // 
            // colXemSoTien
            // 
            this.colXemSoTien.FillWeight = 35F;
            this.colXemSoTien.HeaderText = "Số tiền (VNĐ)";
            this.colXemSoTien.Name = "colXemSoTien";
            this.colXemSoTien.ReadOnly = true;
            // 
            // colXemDienGiai
            // 
            this.colXemDienGiai.FillWeight = 60F;
            this.colXemDienGiai.HeaderText = "Diễn giải";
            this.colXemDienGiai.Name = "colXemDienGiai";
            this.colXemDienGiai.ReadOnly = true;
            // 
            // lblDSStatus
            // 
            this.lblDSStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblDSStatus.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.lblDSStatus.ForeColor = System.Drawing.Color.Gray;
            this.lblDSStatus.Location = new System.Drawing.Point(10, 612);
            this.lblDSStatus.Name = "lblDSStatus";
            this.lblDSStatus.Size = new System.Drawing.Size(1036, 25);
            this.lblDSStatus.TabIndex = 2;
            this.lblDSStatus.Text = "Sẵn sàng.";
            this.lblDSStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // frmChungTu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1064, 681);
            this.Controls.Add(this.tabControlChungTu);
            this.MinimumSize = new System.Drawing.Size(900, 600);
            this.Name = "frmChungTu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Quản Lý Chứng Từ Kế Toán";
            this.Load += new System.EventHandler(this.frmChungTu_Load);
            this.tabControlChungTu.ResumeLayout(false);
            this.tabLapChungTu.ResumeLayout(false);
            this.pnlLapChungTu.ResumeLayout(false);
            this.groupBoxChiTiet.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTiet)).EndInit();
            this.pnlChiTietToolbar.ResumeLayout(false);
            this.pnlChiTietToolbar.PerformLayout();
            this.groupBoxThongTinChungTu.ResumeLayout(false);
            this.groupBoxThongTinChungTu.PerformLayout();
            this.pnlActions.ResumeLayout(false);
            this.tabDanhSach.ResumeLayout(false);
            this.splitContainerDS.Panel1.ResumeLayout(false);
            this.splitContainerDS.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerDS)).EndInit();
            this.splitContainerDS.ResumeLayout(false);
            this.pnlFilter.ResumeLayout(false);
            this.pnlFilter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDanhSachChungTu)).EndInit();
            this.groupBoxXemChiTiet.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvXemChiTiet)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControlChungTu;
        private System.Windows.Forms.TabPage tabLapChungTu;
        private System.Windows.Forms.Panel pnlLapChungTu;
        private System.Windows.Forms.GroupBox groupBoxThongTinChungTu;
        private System.Windows.Forms.Label lblMaCT;
        private System.Windows.Forms.TextBox txtMaCT;
        private System.Windows.Forms.Label lblNgayCT;
        private System.Windows.Forms.DateTimePicker dtpNgayCT;
        private System.Windows.Forms.Label lblLoaiCT;
        private System.Windows.Forms.ComboBox cboLoaiCT;
        private System.Windows.Forms.Label lblHoaDon;
        private System.Windows.Forms.ComboBox cboHoaDon;
        private System.Windows.Forms.Label lblThongTinHDB;
        private System.Windows.Forms.Label lblDienGiai;
        private System.Windows.Forms.TextBox txtDienGiai;
        private System.Windows.Forms.GroupBox groupBoxChiTiet;
        private System.Windows.Forms.Panel pnlChiTietToolbar;
        private System.Windows.Forms.Button btnThemDong;
        private System.Windows.Forms.Button btnXoaDong;
        private System.Windows.Forms.Button btnGoiY;
        private System.Windows.Forms.Label lblTongNo;
        private System.Windows.Forms.Label lblTongCo;
        private System.Windows.Forms.Label lblCanDoi;
        private System.Windows.Forms.DataGridView dgvChiTiet;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSTT;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTaiKhoanNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTaiKhoanCo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSoTienChiTiet;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDienGiaiChiTiet;
        private System.Windows.Forms.Panel pnlActions;
        private System.Windows.Forms.Button btnLamMoi;
        private System.Windows.Forms.Button btnLuuChungTu;
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
        private System.Windows.Forms.SplitContainer splitContainerDS;
        private System.Windows.Forms.DataGridView dgvDanhSachChungTu;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMaCT_DS;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNgayCT_DS;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLoaiCT_DS;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMaHDB_DS;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTenKH_DS;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTongTien_DS;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTenNV_DS;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDienGiai_DS;
        private System.Windows.Forms.GroupBox groupBoxXemChiTiet;
        private System.Windows.Forms.DataGridView dgvXemChiTiet;
        private System.Windows.Forms.DataGridViewTextBoxColumn colXemSTT;
        private System.Windows.Forms.DataGridViewTextBoxColumn colXemNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colXemCo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colXemSoTien;
        private System.Windows.Forms.DataGridViewTextBoxColumn colXemDienGiai;
        private System.Windows.Forms.Label lblDSStatus;
    }
}
