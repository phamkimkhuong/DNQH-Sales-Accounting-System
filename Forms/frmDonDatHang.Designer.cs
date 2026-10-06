namespace DNQH_KeToanBanHang.Forms
{
    partial class frmDonDatHang
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
            this.tcDonHang = new System.Windows.Forms.TabControl();
            this.tpLapDon = new System.Windows.Forms.TabPage();
            this.pnlTongTien = new System.Windows.Forms.Panel();
            this.btnDong = new System.Windows.Forms.Button();
            this.btnLamMoi = new System.Windows.Forms.Button();
            this.btnLuuDon = new System.Windows.Forms.Button();
            this.lblTongTien = new System.Windows.Forms.Label();
            this.dgvChiTiet = new System.Windows.Forms.DataGridView();
            this.colMaSP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTenSP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDonViTinh = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSoLuong = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDonGia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colGiamGia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colThanhTien = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grpSanPham = new System.Windows.Forms.GroupBox();
            this.btnXoaChiTiet = new System.Windows.Forms.Button();
            this.btnThemChiTiet = new System.Windows.Forms.Button();
            this.btnLapHoaDonTuDon = new System.Windows.Forms.Button();
            this.txtThanhTienPreview = new System.Windows.Forms.TextBox();
            this.lblThanhTienPreview = new System.Windows.Forms.Label();
            this.nudGiamGia = new System.Windows.Forms.NumericUpDown();
            this.lblGiamGia = new System.Windows.Forms.Label();
            this.nudSoLuong = new System.Windows.Forms.NumericUpDown();
            this.lblSoLuong = new System.Windows.Forms.Label();
            this.lblTonKhoKhaDung = new System.Windows.Forms.Label();
            this.txtDonGiaBan = new System.Windows.Forms.TextBox();
            this.lblDonGia = new System.Windows.Forms.Label();
            this.txtDonViTinh = new System.Windows.Forms.TextBox();
            this.lblDVT = new System.Windows.Forms.Label();
            this.cboSanPham = new System.Windows.Forms.ComboBox();
            this.lblSanPham = new System.Windows.Forms.Label();
            this.grpThongTinChung = new System.Windows.Forms.GroupBox();
            this.lblNhanVienLap = new System.Windows.Forms.Label();
            this.lblNVTag = new System.Windows.Forms.Label();
            this.txtGhiChu = new System.Windows.Forms.TextBox();
            this.lblGhiChu = new System.Windows.Forms.Label();
            this.cboTrangThai = new System.Windows.Forms.ComboBox();
            this.lblTrangThai = new System.Windows.Forms.Label();
            this.dtpNgayGiao = new System.Windows.Forms.DateTimePicker();
            this.lblNgayGiao = new System.Windows.Forms.Label();
            this.dtpNgayDat = new System.Windows.Forms.DateTimePicker();
            this.lblNgayDat = new System.Windows.Forms.Label();
            this.cboKhachHang = new System.Windows.Forms.ComboBox();
            this.lblKhachHang = new System.Windows.Forms.Label();
            this.txtMaDDH = new System.Windows.Forms.TextBox();
            this.lblMaDDH = new System.Windows.Forms.Label();
            this.tpDanhSach = new System.Windows.Forms.TabPage();
            this.dgvDanhSachDon = new System.Windows.Forms.DataGridView();
            this.colDSMaDDH = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDSNgayDat = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.lblSoDon = new System.Windows.Forms.Label();
            this.pnlHeader.SuspendLayout();
            this.tcDonHang.SuspendLayout();
            this.tpLapDon.SuspendLayout();
            this.pnlTongTien.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTiet)).BeginInit();
            this.grpSanPham.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudGiamGia)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSoLuong)).BeginInit();
            this.grpThongTinChung.SuspendLayout();
            this.tpDanhSach.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDanhSachDon)).BeginInit();
            this.pnlFilterDanhSach.SuspendLayout();
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
            this.pnlHeader.Size = new System.Drawing.Size(1080, 60);
            this.pnlHeader.TabIndex = 0;
            // 
            // lblSubTitle
            // 
            this.lblSubTitle.AutoSize = true;
            this.lblSubTitle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSubTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.lblSubTitle.Location = new System.Drawing.Point(20, 34);
            this.lblSubTitle.Name = "lblSubTitle";
            this.lblSubTitle.Size = new System.Drawing.Size(425, 15);
            this.lblSubTitle.TabIndex = 1;
            this.lblSubTitle.Text = "Tiếp nhận đơn hàng, kiểm tra tồn kho và lưu trữ trong giao dịch atomic an toàn";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(18, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(325, 25);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "QUẢN LÝ ĐƠN ĐẶT HÀNG (SALES ORDER)";
            // 
            // tcDonHang
            // 
            this.tcDonHang.Controls.Add(this.tpLapDon);
            this.tcDonHang.Controls.Add(this.tpDanhSach);
            this.tcDonHang.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcDonHang.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.tcDonHang.Location = new System.Drawing.Point(0, 60);
            this.tcDonHang.Name = "tcDonHang";
            this.tcDonHang.SelectedIndex = 0;
            this.tcDonHang.Size = new System.Drawing.Size(1080, 640);
            this.tcDonHang.TabIndex = 1;
            // 
            // tpLapDon
            // 
            this.tpLapDon.BackColor = System.Drawing.Color.White;
            this.tpLapDon.Controls.Add(this.dgvChiTiet);
            this.tpLapDon.Controls.Add(this.pnlTongTien);
            this.tpLapDon.Controls.Add(this.grpSanPham);
            this.tpLapDon.Controls.Add(this.grpThongTinChung);
            this.tpLapDon.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.tpLapDon.Location = new System.Drawing.Point(4, 26);
            this.tpLapDon.Name = "tpLapDon";
            this.tpLapDon.Padding = new System.Windows.Forms.Padding(10);
            this.tpLapDon.Size = new System.Drawing.Size(1072, 610);
            this.tpLapDon.TabIndex = 0;
            this.tpLapDon.Text = "  Lập Đơn Đặt Hàng Mới  ";
            // 
            // pnlTongTien
            // 
            this.pnlTongTien.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(245)))), ((int)(((byte)(252)))));
            this.pnlTongTien.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlTongTien.Controls.Add(this.btnDong);
            this.pnlTongTien.Controls.Add(this.btnLamMoi);
            this.pnlTongTien.Controls.Add(this.btnLuuDon);
            this.pnlTongTien.Controls.Add(this.lblTongTien);
            this.pnlTongTien.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlTongTien.Location = new System.Drawing.Point(10, 545);
            this.pnlTongTien.Name = "pnlTongTien";
            this.pnlTongTien.Size = new System.Drawing.Size(1052, 55);
            this.pnlTongTien.TabIndex = 3;
            // 
            // btnDong
            // 
            this.btnDong.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDong.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.btnDong.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDong.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDong.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnDong.ForeColor = System.Drawing.Color.White;
            this.btnDong.Location = new System.Drawing.Point(945, 10);
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
            this.btnLamMoi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(162)))), ((int)(((byte)(184)))));
            this.btnLamMoi.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLamMoi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLamMoi.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnLamMoi.ForeColor = System.Drawing.Color.White;
            this.btnLamMoi.Location = new System.Drawing.Point(825, 10);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(110, 34);
            this.btnLamMoi.TabIndex = 2;
            this.btnLamMoi.Text = "Làm Mới";
            this.btnLamMoi.UseVisualStyleBackColor = false;
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);
            // 
            // btnLuuDon
            // 
            this.btnLuuDon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLuuDon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnLuuDon.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLuuDon.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLuuDon.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnLuuDon.ForeColor = System.Drawing.Color.White;
            this.btnLuuDon.Location = new System.Drawing.Point(670, 10);
            this.btnLuuDon.Name = "btnLuuDon";
            this.btnLuuDon.Size = new System.Drawing.Size(145, 34);
            this.btnLuuDon.TabIndex = 1;
            this.btnLuuDon.Text = "LƯU ĐƠN HÀNG";
            this.btnLuuDon.UseVisualStyleBackColor = false;
            this.btnLuuDon.Click += new System.EventHandler(this.btnLuuDon_Click);
            // 
            // lblTongTien
            // 
            this.lblTongTien.AutoSize = true;
            this.lblTongTien.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTongTien.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.lblTongTien.Location = new System.Drawing.Point(15, 16);
            this.lblTongTien.Name = "lblTongTien";
            this.lblTongTien.Size = new System.Drawing.Size(183, 21);
            this.lblTongTien.TabIndex = 0;
            this.lblTongTien.Text = "TỔNG CỘNG: 0 VNĐ";
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
            this.dgvChiTiet.Location = new System.Drawing.Point(10, 235);
            this.dgvChiTiet.MultiSelect = false;
            this.dgvChiTiet.Name = "dgvChiTiet";
            this.dgvChiTiet.ReadOnly = true;
            this.dgvChiTiet.RowHeadersVisible = false;
            this.dgvChiTiet.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvChiTiet.Size = new System.Drawing.Size(1052, 310);
            this.dgvChiTiet.TabIndex = 2;
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
            // grpSanPham
            // 
            this.grpSanPham.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.grpSanPham.Controls.Add(this.btnXoaChiTiet);
            this.grpSanPham.Controls.Add(this.btnThemChiTiet);
            this.grpSanPham.Controls.Add(this.txtThanhTienPreview);
            this.grpSanPham.Controls.Add(this.lblThanhTienPreview);
            this.grpSanPham.Controls.Add(this.nudGiamGia);
            this.grpSanPham.Controls.Add(this.lblGiamGia);
            this.grpSanPham.Controls.Add(this.nudSoLuong);
            this.grpSanPham.Controls.Add(this.lblSoLuong);
            this.grpSanPham.Controls.Add(this.lblTonKhoKhaDung);
            this.grpSanPham.Controls.Add(this.txtDonGiaBan);
            this.grpSanPham.Controls.Add(this.lblDonGia);
            this.grpSanPham.Controls.Add(this.txtDonViTinh);
            this.grpSanPham.Controls.Add(this.lblDVT);
            this.grpSanPham.Controls.Add(this.cboSanPham);
            this.grpSanPham.Controls.Add(this.lblSanPham);
            this.grpSanPham.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpSanPham.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.grpSanPham.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(50)))), ((int)(((byte)(80)))));
            this.grpSanPham.Location = new System.Drawing.Point(10, 130);
            this.grpSanPham.Name = "grpSanPham";
            this.grpSanPham.Size = new System.Drawing.Size(1052, 105);
            this.grpSanPham.TabIndex = 1;
            this.grpSanPham.TabStop = false;
            this.grpSanPham.Text = " CHI TIẾT MẶT HÀNG ĐẶT ";
            // 
            // btnXoaChiTiet
            // 
            this.btnXoaChiTiet.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.btnXoaChiTiet.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnXoaChiTiet.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXoaChiTiet.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnXoaChiTiet.ForeColor = System.Drawing.Color.White;
            this.btnXoaChiTiet.Location = new System.Drawing.Point(920, 58);
            this.btnXoaChiTiet.Name = "btnXoaChiTiet";
            this.btnXoaChiTiet.Size = new System.Drawing.Size(115, 32);
            this.btnXoaChiTiet.TabIndex = 14;
            this.btnXoaChiTiet.Text = "Xóa Dòng";
            this.btnXoaChiTiet.UseVisualStyleBackColor = false;
            this.btnXoaChiTiet.Click += new System.EventHandler(this.btnXoaChiTiet_Click);
            // 
            // btnThemChiTiet
            // 
            this.btnThemChiTiet.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(110)))), ((int)(((byte)(253)))));
            this.btnThemChiTiet.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnThemChiTiet.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThemChiTiet.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnThemChiTiet.ForeColor = System.Drawing.Color.White;
            this.btnThemChiTiet.Location = new System.Drawing.Point(920, 20);
            this.btnThemChiTiet.Name = "btnThemChiTiet";
            this.btnThemChiTiet.Size = new System.Drawing.Size(115, 32);
            this.btnThemChiTiet.TabIndex = 13;
            this.btnThemChiTiet.Text = "Thêm Vào Đơn";
            this.btnThemChiTiet.UseVisualStyleBackColor = false;
            this.btnThemChiTiet.Click += new System.EventHandler(this.btnThemChiTiet_Click);
            // 
            // txtThanhTienPreview
            // 
            this.txtThanhTienPreview.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.txtThanhTienPreview.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.txtThanhTienPreview.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.txtThanhTienPreview.Location = new System.Drawing.Point(745, 63);
            this.txtThanhTienPreview.Name = "txtThanhTienPreview";
            this.txtThanhTienPreview.ReadOnly = true;
            this.txtThanhTienPreview.Size = new System.Drawing.Size(155, 24);
            this.txtThanhTienPreview.TabIndex = 12;
            this.txtThanhTienPreview.Text = "0";
            this.txtThanhTienPreview.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblThanhTienPreview
            // 
            this.lblThanhTienPreview.AutoSize = true;
            this.lblThanhTienPreview.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblThanhTienPreview.Location = new System.Drawing.Point(670, 67);
            this.lblThanhTienPreview.Name = "lblThanhTienPreview";
            this.lblThanhTienPreview.Size = new System.Drawing.Size(66, 15);
            this.lblThanhTienPreview.TabIndex = 11;
            this.lblThanhTienPreview.Text = "Thành tiền:";
            // 
            // nudGiamGia
            // 
            this.nudGiamGia.DecimalPlaces = 1;
            this.nudGiamGia.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.nudGiamGia.Location = new System.Drawing.Point(580, 63);
            this.nudGiamGia.Name = "nudGiamGia";
            this.nudGiamGia.Size = new System.Drawing.Size(75, 24);
            this.nudGiamGia.TabIndex = 10;
            this.nudGiamGia.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudGiamGia.ValueChanged += new System.EventHandler(this.DetailCalculation_Changed);
            // 
            // lblGiamGia
            // 
            this.lblGiamGia.AutoSize = true;
            this.lblGiamGia.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblGiamGia.Location = new System.Drawing.Point(515, 67);
            this.lblGiamGia.Name = "lblGiamGia";
            this.lblGiamGia.Size = new System.Drawing.Size(57, 15);
            this.lblGiamGia.TabIndex = 9;
            this.lblGiamGia.Text = "Giảm (%):";
            // 
            // nudSoLuong
            // 
            this.nudSoLuong.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.nudSoLuong.Location = new System.Drawing.Point(420, 63);
            this.nudSoLuong.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudSoLuong.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudSoLuong.Name = "nudSoLuong";
            this.nudSoLuong.Size = new System.Drawing.Size(80, 24);
            this.nudSoLuong.TabIndex = 8;
            this.nudSoLuong.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudSoLuong.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudSoLuong.ValueChanged += new System.EventHandler(this.DetailCalculation_Changed);
            // 
            // lblSoLuong
            // 
            this.lblSoLuong.AutoSize = true;
            this.lblSoLuong.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSoLuong.Location = new System.Drawing.Point(355, 67);
            this.lblSoLuong.Name = "lblSoLuong";
            this.lblSoLuong.Size = new System.Drawing.Size(57, 15);
            this.lblSoLuong.TabIndex = 7;
            this.lblSoLuong.Text = "Số lượng:";
            // 
            // lblTonKhoKhaDung
            // 
            this.lblTonKhoKhaDung.AutoSize = true;
            this.lblTonKhoKhaDung.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTonKhoKhaDung.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(110)))), ((int)(((byte)(253)))));
            this.lblTonKhoKhaDung.Location = new System.Drawing.Point(75, 67);
            this.lblTonKhoKhaDung.Name = "lblTonKhoKhaDung";
            this.lblTonKhoKhaDung.Size = new System.Drawing.Size(124, 15);
            this.lblTonKhoKhaDung.TabIndex = 6;
            this.lblTonKhoKhaDung.Text = "Tồn kho khả dụng: 0";
            // 
            // txtDonGiaBan
            // 
            this.txtDonGiaBan.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtDonGiaBan.Location = new System.Drawing.Point(630, 25);
            this.txtDonGiaBan.Name = "txtDonGiaBan";
            this.txtDonGiaBan.Size = new System.Drawing.Size(140, 24);
            this.txtDonGiaBan.TabIndex = 5;
            this.txtDonGiaBan.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtDonGiaBan.TextChanged += new System.EventHandler(this.DetailCalculation_Changed);
            // 
            // lblDonGia
            // 
            this.lblDonGia.AutoSize = true;
            this.lblDonGia.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblDonGia.Location = new System.Drawing.Point(570, 29);
            this.lblDonGia.Name = "lblDonGia";
            this.lblDonGia.Size = new System.Drawing.Size(51, 15);
            this.lblDonGia.TabIndex = 4;
            this.lblDonGia.Text = "Đơn giá:";
            // 
            // txtDonViTinh
            // 
            this.txtDonViTinh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.txtDonViTinh.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtDonViTinh.Location = new System.Drawing.Point(475, 25);
            this.txtDonViTinh.Name = "txtDonViTinh";
            this.txtDonViTinh.ReadOnly = true;
            this.txtDonViTinh.Size = new System.Drawing.Size(80, 24);
            this.txtDonViTinh.TabIndex = 3;
            // 
            // lblDVT
            // 
            this.lblDVT.AutoSize = true;
            this.lblDVT.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblDVT.Location = new System.Drawing.Point(435, 29);
            this.lblDVT.Name = "lblDVT";
            this.lblDVT.Size = new System.Drawing.Size(31, 15);
            this.lblDVT.TabIndex = 2;
            this.lblDVT.Text = "ĐVT:";
            // 
            // cboSanPham
            // 
            this.cboSanPham.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSanPham.DropDownWidth = 600;
            this.cboSanPham.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.cboSanPham.FormattingEnabled = true;
            this.cboSanPham.Location = new System.Drawing.Point(75, 25);
            this.cboSanPham.Name = "cboSanPham";
            this.cboSanPham.Size = new System.Drawing.Size(345, 24);
            this.cboSanPham.TabIndex = 1;
            this.cboSanPham.SelectedIndexChanged += new System.EventHandler(this.cboSanPham_SelectedIndexChanged);
            // 
            // lblSanPham
            // 
            this.lblSanPham.AutoSize = true;
            this.lblSanPham.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSanPham.Location = new System.Drawing.Point(10, 29);
            this.lblSanPham.Name = "lblSanPham";
            this.lblSanPham.Size = new System.Drawing.Size(63, 15);
            this.lblSanPham.TabIndex = 0;
            this.lblSanPham.Text = "Sản phẩm:";
            // 
            // grpThongTinChung
            // 
            this.grpThongTinChung.Controls.Add(this.lblNhanVienLap);
            this.grpThongTinChung.Controls.Add(this.lblNVTag);
            this.grpThongTinChung.Controls.Add(this.txtGhiChu);
            this.grpThongTinChung.Controls.Add(this.lblGhiChu);
            this.grpThongTinChung.Controls.Add(this.cboTrangThai);
            this.grpThongTinChung.Controls.Add(this.lblTrangThai);
            this.grpThongTinChung.Controls.Add(this.dtpNgayGiao);
            this.grpThongTinChung.Controls.Add(this.lblNgayGiao);
            this.grpThongTinChung.Controls.Add(this.dtpNgayDat);
            this.grpThongTinChung.Controls.Add(this.lblNgayDat);
            this.grpThongTinChung.Controls.Add(this.cboKhachHang);
            this.grpThongTinChung.Controls.Add(this.lblKhachHang);
            this.grpThongTinChung.Controls.Add(this.txtMaDDH);
            this.grpThongTinChung.Controls.Add(this.lblMaDDH);
            this.grpThongTinChung.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpThongTinChung.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.grpThongTinChung.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(50)))), ((int)(((byte)(80)))));
            this.grpThongTinChung.Location = new System.Drawing.Point(10, 10);
            this.grpThongTinChung.Name = "grpThongTinChung";
            this.grpThongTinChung.Size = new System.Drawing.Size(1052, 120);
            this.grpThongTinChung.TabIndex = 0;
            this.grpThongTinChung.TabStop = false;
            this.grpThongTinChung.Text = " THÔNG TIN ĐƠN ĐẶT HÀNG ";
            // 
            // lblNhanVienLap
            // 
            this.lblNhanVienLap.AutoSize = true;
            this.lblNhanVienLap.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblNhanVienLap.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(100)))), ((int)(((byte)(160)))));
            this.lblNhanVienLap.Location = new System.Drawing.Point(625, 25);
            this.lblNhanVienLap.Name = "lblNhanVienLap";
            this.lblNhanVienLap.Size = new System.Drawing.Size(126, 15);
            this.lblNhanVienLap.TabIndex = 5;
            this.lblNhanVienLap.Text = "Nhân viên đăng nhập";
            // 
            // lblNVTag
            // 
            this.lblNVTag.AutoSize = true;
            this.lblNVTag.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblNVTag.Location = new System.Drawing.Point(535, 25);
            this.lblNVTag.Name = "lblNVTag";
            this.lblNVTag.Size = new System.Drawing.Size(84, 15);
            this.lblNVTag.TabIndex = 4;
            this.lblNVTag.Text = "Nhân viên lập:";
            // 
            // txtGhiChu
            // 
            this.txtGhiChu.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtGhiChu.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtGhiChu.Location = new System.Drawing.Point(625, 83);
            this.txtGhiChu.Name = "txtGhiChu";
            this.txtGhiChu.Size = new System.Drawing.Size(410, 24);
            this.txtGhiChu.TabIndex = 13;
            // 
            // lblGhiChu
            // 
            this.lblGhiChu.AutoSize = true;
            this.lblGhiChu.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblGhiChu.Location = new System.Drawing.Point(565, 87);
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
            "Chờ duyệt",
            "Đã lập hóa đơn",
            "Đã hủy"});
            this.cboTrangThai.Location = new System.Drawing.Point(625, 52);
            this.cboTrangThai.Name = "cboTrangThai";
            this.cboTrangThai.Size = new System.Drawing.Size(160, 24);
            this.cboTrangThai.TabIndex = 9;
            // 
            // lblTrangThai
            // 
            this.lblTrangThai.AutoSize = true;
            this.lblTrangThai.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblTrangThai.Location = new System.Drawing.Point(555, 56);
            this.lblTrangThai.Name = "lblTrangThai";
            this.lblTrangThai.Size = new System.Drawing.Size(64, 15);
            this.lblTrangThai.TabIndex = 8;
            this.lblTrangThai.Text = "Trạng thái:";
            // 
            // dtpNgayGiao
            // 
            this.dtpNgayGiao.CustomFormat = "dd/MM/yyyy";
            this.dtpNgayGiao.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.dtpNgayGiao.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpNgayGiao.Location = new System.Drawing.Point(340, 83);
            this.dtpNgayGiao.Name = "dtpNgayGiao";
            this.dtpNgayGiao.ShowCheckBox = true;
            this.dtpNgayGiao.Size = new System.Drawing.Size(145, 24);
            this.dtpNgayGiao.TabIndex = 11;
            // 
            // lblNgayGiao
            // 
            this.lblNgayGiao.AutoSize = true;
            this.lblNgayGiao.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblNgayGiao.Location = new System.Drawing.Point(245, 87);
            this.lblNgayGiao.Name = "lblNgayGiao";
            this.lblNgayGiao.Size = new System.Drawing.Size(89, 15);
            this.lblNgayGiao.TabIndex = 10;
            this.lblNgayGiao.Text = "Giao dự kiến:";
            // 
            // dtpNgayDat
            // 
            this.dtpNgayDat.CustomFormat = "dd/MM/yyyy HH:mm";
            this.dtpNgayDat.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.dtpNgayDat.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpNgayDat.Location = new System.Drawing.Point(90, 83);
            this.dtpNgayDat.Name = "dtpNgayDat";
            this.dtpNgayDat.Size = new System.Drawing.Size(140, 24);
            this.dtpNgayDat.TabIndex = 7;
            // 
            // lblNgayDat
            // 
            this.lblNgayDat.AutoSize = true;
            this.lblNgayDat.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblNgayDat.Location = new System.Drawing.Point(10, 87);
            this.lblNgayDat.Name = "lblNgayDat";
            this.lblNgayDat.Size = new System.Drawing.Size(58, 15);
            this.lblNgayDat.TabIndex = 6;
            this.lblNgayDat.Text = "Ngày đặt:";
            // 
            // cboKhachHang
            // 
            this.cboKhachHang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboKhachHang.DropDownWidth = 600;
            this.cboKhachHang.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.cboKhachHang.FormattingEnabled = true;
            this.cboKhachHang.Location = new System.Drawing.Point(90, 52);
            this.cboKhachHang.Name = "cboKhachHang";
            this.cboKhachHang.Size = new System.Drawing.Size(395, 24);
            this.cboKhachHang.TabIndex = 3;
            // 
            // lblKhachHang
            // 
            this.lblKhachHang.AutoSize = true;
            this.lblKhachHang.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblKhachHang.Location = new System.Drawing.Point(10, 56);
            this.lblKhachHang.Name = "lblKhachHang";
            this.lblKhachHang.Size = new System.Drawing.Size(73, 15);
            this.lblKhachHang.TabIndex = 2;
            this.lblKhachHang.Text = "Khách hàng:";
            // 
            // txtMaDDH
            // 
            this.txtMaDDH.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(245)))), ((int)(((byte)(255)))));
            this.txtMaDDH.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.txtMaDDH.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(70)))), ((int)(((byte)(150)))));
            this.txtMaDDH.Location = new System.Drawing.Point(90, 21);
            this.txtMaDDH.Name = "txtMaDDH";
            this.txtMaDDH.ReadOnly = true;
            this.txtMaDDH.Size = new System.Drawing.Size(150, 24);
            this.txtMaDDH.TabIndex = 1;
            // 
            // lblMaDDH
            // 
            this.lblMaDDH.AutoSize = true;
            this.lblMaDDH.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblMaDDH.Location = new System.Drawing.Point(10, 25);
            this.lblMaDDH.Name = "lblMaDDH";
            this.lblMaDDH.Size = new System.Drawing.Size(56, 15);
            this.lblMaDDH.TabIndex = 0;
            this.lblMaDDH.Text = "Mã ĐĐH:";
            // 
            // tpDanhSach
            // 
            this.tpDanhSach.BackColor = System.Drawing.Color.White;
            this.tpDanhSach.Controls.Add(this.dgvDanhSachDon);
            this.tpDanhSach.Controls.Add(this.pnlFilterDanhSach);
            this.tpDanhSach.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.tpDanhSach.Location = new System.Drawing.Point(4, 26);
            this.tpDanhSach.Name = "tpDanhSach";
            this.tpDanhSach.Padding = new System.Windows.Forms.Padding(10);
            this.tpDanhSach.Size = new System.Drawing.Size(1072, 610);
            this.tpDanhSach.TabIndex = 1;
            this.tpDanhSach.Text = "  Danh Sách Đơn Đã Lập  ";
            // 
            // dgvDanhSachDon
            // 
            this.dgvDanhSachDon.AllowUserToAddRows = false;
            this.dgvDanhSachDon.AllowUserToDeleteRows = false;
            this.dgvDanhSachDon.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDanhSachDon.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(240)))), ((int)(((byte)(248)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(40)))), ((int)(((byte)(50)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvDanhSachDon.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvDanhSachDon.ColumnHeadersHeight = 32;
            this.dgvDanhSachDon.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colDSMaDDH,
            this.colDSNgayDat,
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
            this.dgvDanhSachDon.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgvDanhSachDon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDanhSachDon.EnableHeadersVisualStyles = false;
            this.dgvDanhSachDon.Location = new System.Drawing.Point(10, 50);
            this.dgvDanhSachDon.MultiSelect = false;
            this.dgvDanhSachDon.Name = "dgvDanhSachDon";
            this.dgvDanhSachDon.ReadOnly = true;
            this.dgvDanhSachDon.RowHeadersVisible = false;
            this.dgvDanhSachDon.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDanhSachDon.Size = new System.Drawing.Size(1052, 550);
            this.dgvDanhSachDon.TabIndex = 1;
            this.dgvDanhSachDon.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDanhSachDon_CellDoubleClick);
            // 
            // colDSMaDDH
            // 
            this.colDSMaDDH.DataPropertyName = "MaDDH";
            this.colDSMaDDH.FillWeight = 65F;
            this.colDSMaDDH.HeaderText = "Mã ĐĐH";
            this.colDSMaDDH.Name = "colDSMaDDH";
            this.colDSMaDDH.ReadOnly = true;
            // 
            // colDSNgayDat
            // 
            this.colDSNgayDat.DataPropertyName = "NgayDat";
            this.colDSNgayDat.FillWeight = 75F;
            this.colDSNgayDat.HeaderText = "Ngày Đặt";
            this.colDSNgayDat.Name = "colDSNgayDat";
            this.colDSNgayDat.ReadOnly = true;
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
            this.pnlFilterDanhSach.Controls.Add(this.btnLapHoaDonTuDon);
            this.pnlFilterDanhSach.Controls.Add(this.lblSoDon);
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
            this.lblTimKiem.TabIndex = 3;
            this.lblTimKiem.Text = "Tìm kiếm:";
            // 
            // txtTimKiem
            // 
            this.txtTimKiem.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtTimKiem.Location = new System.Drawing.Point(0, 0);
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.Size = new System.Drawing.Size(180, 23);
            this.txtTimKiem.TabIndex = 4;
            // 
            // lblFromDate
            // 
            this.lblFromDate.AutoSize = true;
            this.lblFromDate.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblFromDate.Location = new System.Drawing.Point(0, 0);
            this.lblFromDate.Name = "lblFromDate";
            this.lblFromDate.Size = new System.Drawing.Size(52, 15);
            this.lblFromDate.TabIndex = 5;
            this.lblFromDate.Text = "Từ ngày:";
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFromDate.Location = new System.Drawing.Point(0, 0);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.Size = new System.Drawing.Size(115, 23);
            this.dtpFromDate.TabIndex = 6;
            // 
            // lblToDate
            // 
            this.lblToDate.AutoSize = true;
            this.lblToDate.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblToDate.Location = new System.Drawing.Point(0, 0);
            this.lblToDate.Name = "lblToDate";
            this.lblToDate.Size = new System.Drawing.Size(60, 15);
            this.lblToDate.TabIndex = 7;
            this.lblToDate.Text = "Đến ngày:";
            // 
            // dtpToDate
            // 
            this.dtpToDate.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpToDate.Location = new System.Drawing.Point(0, 0);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.Size = new System.Drawing.Size(115, 23);
            this.dtpToDate.TabIndex = 8;
            // 
            // lblTrangThaiLoc
            // 
            this.lblTrangThaiLoc.AutoSize = true;
            this.lblTrangThaiLoc.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblTrangThaiLoc.Location = new System.Drawing.Point(0, 0);
            this.lblTrangThaiLoc.Name = "lblTrangThaiLoc";
            this.lblTrangThaiLoc.Size = new System.Drawing.Size(62, 15);
            this.lblTrangThaiLoc.TabIndex = 9;
            this.lblTrangThaiLoc.Text = "Trạng thái:";
            // 
            // cboTrangThaiLoc
            // 
            this.cboTrangThaiLoc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTrangThaiLoc.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cboTrangThaiLoc.Location = new System.Drawing.Point(0, 0);
            this.cboTrangThaiLoc.Name = "cboTrangThaiLoc";
            this.cboTrangThaiLoc.Size = new System.Drawing.Size(130, 23);
            this.cboTrangThaiLoc.TabIndex = 10;
            // 
            // btnTimKiem
            // 
            this.btnTimKiem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnTimKiem.Location = new System.Drawing.Point(0, 0);
            this.btnTimKiem.Name = "btnTimKiem";
            this.btnTimKiem.Size = new System.Drawing.Size(85, 28);
            this.btnTimKiem.TabIndex = 11;
            this.btnTimKiem.Text = "Tìm Kiếm";
            this.btnTimKiem.UseVisualStyleBackColor = true;
            // 
            // btnLapHoaDonTuDon
            // 
            this.btnLapHoaDonTuDon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLapHoaDonTuDon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnLapHoaDonTuDon.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLapHoaDonTuDon.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLapHoaDonTuDon.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnLapHoaDonTuDon.ForeColor = System.Drawing.Color.White;
            this.btnLapHoaDonTuDon.Location = new System.Drawing.Point(790, 6);
            this.btnLapHoaDonTuDon.Name = "btnLapHoaDonTuDon";
            this.btnLapHoaDonTuDon.Size = new System.Drawing.Size(150, 28);
            this.btnLapHoaDonTuDon.TabIndex = 2;
            this.btnLapHoaDonTuDon.Text = "📄 Lập Hóa Đơn";
            this.btnLapHoaDonTuDon.UseVisualStyleBackColor = false;
            this.btnLapHoaDonTuDon.Click += new System.EventHandler(this.btnLapHoaDonTuDon_Click);
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
            // lblSoDon
            // 
            this.lblSoDon.AutoSize = true;
            this.lblSoDon.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblSoDon.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(60)))), ((int)(((byte)(90)))));
            this.lblSoDon.Location = new System.Drawing.Point(10, 11);
            this.lblSoDon.Name = "lblSoDon";
            this.lblSoDon.Size = new System.Drawing.Size(161, 17);
            this.lblSoDon.TabIndex = 0;
            this.lblSoDon.Text = "Tổng số đơn đặt hàng: 0";
            // 
            // frmDonDatHang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1080, 700);
            this.Controls.Add(this.tcDonHang);
            this.Controls.Add(this.pnlHeader);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "frmDonDatHang";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Quản Lý Đơn Đặt Hàng";
            this.Load += new System.EventHandler(this.frmDonDatHang_Load);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.tcDonHang.ResumeLayout(false);
            this.tpLapDon.ResumeLayout(false);
            this.pnlTongTien.ResumeLayout(false);
            this.pnlTongTien.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTiet)).EndInit();
            this.grpSanPham.ResumeLayout(false);
            this.grpSanPham.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudGiamGia)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSoLuong)).EndInit();
            this.grpThongTinChung.ResumeLayout(false);
            this.grpThongTinChung.PerformLayout();
            this.tpDanhSach.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDanhSachDon)).EndInit();
            this.pnlFilterDanhSach.ResumeLayout(false);
            this.pnlFilterDanhSach.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblSubTitle;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TabControl tcDonHang;
        private System.Windows.Forms.TabPage tpLapDon;
        private System.Windows.Forms.GroupBox grpThongTinChung;
        private System.Windows.Forms.Label lblMaDDH;
        private System.Windows.Forms.TextBox txtMaDDH;
        private System.Windows.Forms.Label lblKhachHang;
        private System.Windows.Forms.ComboBox cboKhachHang;
        private System.Windows.Forms.Label lblNgayDat;
        private System.Windows.Forms.DateTimePicker dtpNgayDat;
        private System.Windows.Forms.Label lblNgayGiao;
        private System.Windows.Forms.DateTimePicker dtpNgayGiao;
        private System.Windows.Forms.Label lblTrangThai;
        private System.Windows.Forms.ComboBox cboTrangThai;
        private System.Windows.Forms.Label lblGhiChu;
        private System.Windows.Forms.TextBox txtGhiChu;
        private System.Windows.Forms.Label lblNVTag;
        private System.Windows.Forms.Label lblNhanVienLap;
        private System.Windows.Forms.GroupBox grpSanPham;
        private System.Windows.Forms.Label lblSanPham;
        private System.Windows.Forms.ComboBox cboSanPham;
        private System.Windows.Forms.Label lblDVT;
        private System.Windows.Forms.TextBox txtDonViTinh;
        private System.Windows.Forms.Label lblDonGia;
        private System.Windows.Forms.TextBox txtDonGiaBan;
        private System.Windows.Forms.Label lblTonKhoKhaDung;
        private System.Windows.Forms.Label lblSoLuong;
        private System.Windows.Forms.NumericUpDown nudSoLuong;
        private System.Windows.Forms.Label lblGiamGia;
        private System.Windows.Forms.NumericUpDown nudGiamGia;
        private System.Windows.Forms.Label lblThanhTienPreview;
        private System.Windows.Forms.TextBox txtThanhTienPreview;
        private System.Windows.Forms.Button btnThemChiTiet;
        private System.Windows.Forms.Button btnXoaChiTiet;
        private System.Windows.Forms.DataGridView dgvChiTiet;
        private System.Windows.Forms.Panel pnlTongTien;
        private System.Windows.Forms.Label lblTongTien;
        private System.Windows.Forms.Button btnLuuDon;
        private System.Windows.Forms.Button btnLamMoi;
        private System.Windows.Forms.Button btnDong;
        private System.Windows.Forms.TabPage tpDanhSach;
        private System.Windows.Forms.DataGridView dgvDanhSachDon;
        private System.Windows.Forms.Panel pnlFilterDanhSach;
        private System.Windows.Forms.Label lblSoDon;
        private System.Windows.Forms.Button btnLamMoiDanhSach;
        private System.Windows.Forms.Button btnLapHoaDonTuDon;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMaSP;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTenSP;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDonViTinh;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSoLuong;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDonGia;
        private System.Windows.Forms.DataGridViewTextBoxColumn colGiamGia;
        private System.Windows.Forms.DataGridViewTextBoxColumn colThanhTien;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDSMaDDH;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDSNgayDat;
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
