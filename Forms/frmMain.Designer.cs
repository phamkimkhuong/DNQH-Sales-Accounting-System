namespace DNQH_KeToanBanHang.Forms
{
    partial class frmMain
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
            this.components = new System.ComponentModel.Container();
            this.menuStripMain = new System.Windows.Forms.MenuStrip();
            this.menuTrangChu = new System.Windows.Forms.ToolStripMenuItem();
            this.menuHeThong = new System.Windows.Forms.ToolStripMenuItem();
            this.menuDoiMatKhau = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuQuanLyTaiKhoan = new System.Windows.Forms.ToolStripMenuItem();
            this.menuQuanLyNhanVien = new System.Windows.Forms.ToolStripMenuItem();
            this.menuNhatKyHoatDong = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparatorTraCuuPhimTat = new System.Windows.Forms.ToolStripSeparator();
            this.menuTraCuuPhimTat = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.menuDangXuat = new System.Windows.Forms.ToolStripMenuItem();
            this.menuThoat = new System.Windows.Forms.ToolStripMenuItem();
            this.menuDanhMuc = new System.Windows.Forms.ToolStripMenuItem();
            this.menuKhachHang = new System.Windows.Forms.ToolStripMenuItem();
            this.menuNhaCungCap = new System.Windows.Forms.ToolStripMenuItem();
            this.menuLoaiSanPham = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSanPham = new System.Windows.Forms.ToolStripMenuItem();
            this.menuKho = new System.Windows.Forms.ToolStripMenuItem();
            this.menuKeToanBanHang = new System.Windows.Forms.ToolStripMenuItem();
            this.menuDonDatHang = new System.Windows.Forms.ToolStripMenuItem();
            this.menuTraCuuDonDatHang = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.menuHoaDonBan = new System.Windows.Forms.ToolStripMenuItem();
            this.menuTraCuuHoaDon = new System.Windows.Forms.ToolStripMenuItem();
            this.menuQuanLyKho = new System.Windows.Forms.ToolStripMenuItem();
            this.menuTonKho = new System.Windows.Forms.ToolStripMenuItem();
            this.menuPhieuXuatKho = new System.Windows.Forms.ToolStripMenuItem();
            this.menuTraCuuPhieuXuat = new System.Windows.Forms.ToolStripMenuItem();
            this.menuChungTuTien = new System.Windows.Forms.ToolStripMenuItem();
            this.menuPhieuThu = new System.Windows.Forms.ToolStripMenuItem();
            this.menuTraCuuPhieuThu = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.menuPhieuChi = new System.Windows.Forms.ToolStripMenuItem();
            this.menuTraCuuPhieuChi = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.menuChungTu = new System.Windows.Forms.ToolStripMenuItem();
            this.menuTraCuuChungTu = new System.Windows.Forms.ToolStripMenuItem();
            this.menuKeToanChiTiet = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSoChiTietKhachHang = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSoChiTietSanPham = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSoChiTietHoaDon = new System.Windows.Forms.ToolStripMenuItem();
            this.menuBaoCaoTuoiNo = new System.Windows.Forms.ToolStripMenuItem();
            this.menuKeToanTongHop = new System.Windows.Forms.ToolStripMenuItem();
            this.menuBaoCaoDoanhThu = new System.Windows.Forms.ToolStripMenuItem();
            this.menuBaoCaoThuChi = new System.Windows.Forms.ToolStripMenuItem();
            this.menuBaoCaoTonKho = new System.Windows.Forms.ToolStripMenuItem();
            this.menuBaoCaoBieuDo = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStripMain = new System.Windows.Forms.StatusStrip();
            this.lblStatusUser = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblStatusRole = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblStatusConnection = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblStatusTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.timerClock = new System.Windows.Forms.Timer(this.components);
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.btnHeaderDangXuat = new System.Windows.Forms.Button();
            this.btnHeaderDoiMatKhau = new System.Windows.Forms.Button();
            this.btnHeaderHotkeys = new System.Windows.Forms.Button();
            this.pnlHeaderRightDivider = new System.Windows.Forms.Panel();
            this.lblUserProfile = new System.Windows.Forms.Label();
            this.btnBackToDashboard = new System.Windows.Forms.Button();
            this.pnlHeaderDivider = new System.Windows.Forms.Panel();
            this.lblBreadcrumb = new System.Windows.Forms.Label();
            this.lblAppSubtitle = new System.Windows.Forms.Label();
            this.lblAppTitle = new System.Windows.Forms.Label();
            this.lblLogoIcon = new System.Windows.Forms.Label();
            this.pnlSidebar = new System.Windows.Forms.Panel();
            this.btnNavTaiKhoan = new System.Windows.Forms.Button();
            this.btnNavNhanVien = new System.Windows.Forms.Button();
            this.btnNavKho = new System.Windows.Forms.Button();
            this.btnNavLoaiSanPham = new System.Windows.Forms.Button();
            this.btnNavSanPham = new System.Windows.Forms.Button();
            this.btnNavNhaCungCap = new System.Windows.Forms.Button();
            this.btnNavKhachHang = new System.Windows.Forms.Button();
            this.lblNavGroupAdmin = new System.Windows.Forms.Label();
            this.btnNavBaoCaoTongHop = new System.Windows.Forms.Button();
            this.btnNavKeToanChiTiet = new System.Windows.Forms.Button();
            this.lblNavGroupReport = new System.Windows.Forms.Label();
            this.btnNavChungTu = new System.Windows.Forms.Button();
            this.btnNavPhieuChi = new System.Windows.Forms.Button();
            this.btnNavPhieuThu = new System.Windows.Forms.Button();
            this.lblNavGroupCash = new System.Windows.Forms.Label();
            this.btnNavTonKho = new System.Windows.Forms.Button();
            this.btnNavXuatKho = new System.Windows.Forms.Button();
            this.lblNavGroupWarehouse = new System.Windows.Forms.Label();
            this.btnNavHoaDon = new System.Windows.Forms.Button();
            this.btnNavDonHang = new System.Windows.Forms.Button();
            this.lblNavGroupSales = new System.Windows.Forms.Label();
            this.btnNavDashboard = new System.Windows.Forms.Button();
            this.lblSidebarHeader = new System.Windows.Forms.Label();
            this.pnlWorkspace = new System.Windows.Forms.Panel();
            this.pnlDashboard = new System.Windows.Forms.Panel();
            this.pnlRoleNote = new System.Windows.Forms.Panel();
            this.lblRoleNoteText = new System.Windows.Forms.Label();
            this.lblRoleNoteTitle = new System.Windows.Forms.Label();
            this.pnlStockAlertContainer = new System.Windows.Forms.Panel();
            this.pnlStockAlertHeader = new System.Windows.Forms.Panel();
            this.lblStockAlertTitle = new System.Windows.Forms.Label();
            this.flpStockAlertActions = new System.Windows.Forms.FlowLayoutPanel();
            this.btnGoToTonKho = new System.Windows.Forms.Button();
            this.btnStockAlertRefresh = new System.Windows.Forms.Button();
            this.cboSafetyThreshold = new System.Windows.Forms.ComboBox();
            this.lblThresholdPrompt = new System.Windows.Forms.Label();
            this.dgvStockAlert = new System.Windows.Forms.DataGridView();
            this.colStockAlertSTT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStockAlertMaSP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStockAlertTenSP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStockAlertLoaiSP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStockAlertDVT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStockAlertTongTon = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStockAlertMucDo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStockAlertChiTietKho = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlStockSafeBanner = new System.Windows.Forms.Panel();
            this.lblStockSafeSub = new System.Windows.Forms.Label();
            this.lblStockSafeMessage = new System.Windows.Forms.Label();
            this.lblStockSafeIcon = new System.Windows.Forms.Label();
            this.pnlQuickActionsContainer = new System.Windows.Forms.Panel();
            this.flpQuickActions = new System.Windows.Forms.FlowLayoutPanel();
            this.btnActionDonHang = new System.Windows.Forms.Button();
            this.btnActionHoaDon = new System.Windows.Forms.Button();
            this.btnActionXuatKho = new System.Windows.Forms.Button();
            this.btnActionPhieuThu = new System.Windows.Forms.Button();
            this.btnActionBaoCao = new System.Windows.Forms.Button();
            this.btnActionTonKho = new System.Windows.Forms.Button();
            this.btnActionChungTu = new System.Windows.Forms.Button();
            this.btnActionSoQuy = new System.Windows.Forms.Button();
            this.lblQuickActionsTitle = new System.Windows.Forms.Label();
            this.tlpKPICards = new System.Windows.Forms.TableLayoutPanel();
            this.pnlCardStock = new System.Windows.Forms.Panel();
            this.pnlCardStockBar = new System.Windows.Forms.Panel();
            this.lblCardStockSub = new System.Windows.Forms.Label();
            this.lblCardStockValue = new System.Windows.Forms.Label();
            this.lblCardStockTitle = new System.Windows.Forms.Label();
            this.pnlCardPayment = new System.Windows.Forms.Panel();
            this.pnlCardPaymentBar = new System.Windows.Forms.Panel();
            this.lblCardPaymentSub = new System.Windows.Forms.Label();
            this.lblCardPaymentValue = new System.Windows.Forms.Label();
            this.lblCardPaymentTitle = new System.Windows.Forms.Label();
            this.pnlCardReceipt = new System.Windows.Forms.Panel();
            this.pnlCardReceiptBar = new System.Windows.Forms.Panel();
            this.lblCardReceiptSub = new System.Windows.Forms.Label();
            this.lblCardReceiptValue = new System.Windows.Forms.Label();
            this.lblCardReceiptTitle = new System.Windows.Forms.Label();
            this.pnlCardRevenue = new System.Windows.Forms.Panel();
            this.pnlCardRevenueBar = new System.Windows.Forms.Panel();
            this.lblCardRevenueSub = new System.Windows.Forms.Label();
            this.lblCardRevenueValue = new System.Windows.Forms.Label();
            this.lblCardRevenueTitle = new System.Windows.Forms.Label();
            this.pnlWelcomeBanner = new System.Windows.Forms.Panel();
            this.btnRefreshKPI = new System.Windows.Forms.Button();
            this.lblWelcomeRoleDesc = new System.Windows.Forms.Label();
            this.lblWelcomeGreeting = new System.Windows.Forms.Label();
            this.pnlChildContainer = new System.Windows.Forms.Panel();
            this.menuStripMain.SuspendLayout();
            this.statusStripMain.SuspendLayout();
            this.pnlHeader.SuspendLayout();
            this.pnlSidebar.SuspendLayout();
            this.pnlWorkspace.SuspendLayout();
            this.pnlDashboard.SuspendLayout();
            this.pnlRoleNote.SuspendLayout();
            this.pnlStockAlertContainer.SuspendLayout();
            this.pnlStockAlertHeader.SuspendLayout();
            this.flpStockAlertActions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStockAlert)).BeginInit();
            this.pnlStockSafeBanner.SuspendLayout();
            this.pnlQuickActionsContainer.SuspendLayout();
            this.flpQuickActions.SuspendLayout();
            this.tlpKPICards.SuspendLayout();
            this.pnlCardStock.SuspendLayout();
            this.pnlCardPayment.SuspendLayout();
            this.pnlCardReceipt.SuspendLayout();
            this.pnlCardRevenue.SuspendLayout();
            this.pnlWelcomeBanner.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStripMain
            // 
            this.menuStripMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(245)))), ((int)(((byte)(255)))));
            this.menuStripMain.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.menuStripMain.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuTrangChu,
            this.menuHeThong,
            this.menuDanhMuc,
            this.menuKeToanBanHang,
            this.menuQuanLyKho,
            this.menuChungTuTien,
            this.menuKeToanChiTiet,
            this.menuKeToanTongHop});
            this.menuStripMain.Location = new System.Drawing.Point(0, 0);
            this.menuStripMain.Name = "menuStripMain";
            this.menuStripMain.Padding = new System.Windows.Forms.Padding(6, 3, 0, 3);
            this.menuStripMain.Size = new System.Drawing.Size(1264, 28);
            this.menuStripMain.TabIndex = 0;
            this.menuStripMain.Text = "menuStrip1";
            // 
            // menuTrangChu
            // 
            this.menuTrangChu.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.menuTrangChu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(40)))), ((int)(((byte)(217)))));
            this.menuTrangChu.Name = "menuTrangChu";
            this.menuTrangChu.Size = new System.Drawing.Size(102, 22);
            this.menuTrangChu.Text = "📊 TRANG CHỦ";
            this.menuTrangChu.Click += new System.EventHandler(this.menuTrangChu_Click);
            // 
            // menuHeThong
            // 
            this.menuHeThong.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuDoiMatKhau,
            this.toolStripSeparator1,
            this.menuQuanLyTaiKhoan,
            this.menuQuanLyNhanVien,
            this.menuNhatKyHoatDong,
            this.toolStripSeparatorTraCuuPhimTat,
            this.menuTraCuuPhimTat,
            this.toolStripSeparator2,
            this.menuDangXuat,
            this.menuThoat});
            this.menuHeThong.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.menuHeThong.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(40)))), ((int)(((byte)(217)))));
            this.menuHeThong.Name = "menuHeThong";
            this.menuHeThong.Size = new System.Drawing.Size(96, 22);
            this.menuHeThong.Text = "HỆ THỐNG";
            // 
            // menuDoiMatKhau
            // 
            this.menuDoiMatKhau.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.menuDoiMatKhau.Name = "menuDoiMatKhau";
            this.menuDoiMatKhau.Size = new System.Drawing.Size(209, 24);
            this.menuDoiMatKhau.Text = "Đổi Mật Khẩu";
            this.menuDoiMatKhau.Click += new System.EventHandler(this.menuDoiMatKhau_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(206, 6);
            // 
            // menuQuanLyTaiKhoan
            // 
            this.menuQuanLyTaiKhoan.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.menuQuanLyTaiKhoan.Name = "menuQuanLyTaiKhoan";
            this.menuQuanLyTaiKhoan.Size = new System.Drawing.Size(209, 24);
            this.menuQuanLyTaiKhoan.Text = "Quản Lý Tài Khoản";
            this.menuQuanLyTaiKhoan.Click += new System.EventHandler(this.menuQuanLyTaiKhoan_Click);
            // 
            // menuQuanLyNhanVien
            // 
            this.menuQuanLyNhanVien.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.menuQuanLyNhanVien.Name = "menuQuanLyNhanVien";
            this.menuQuanLyNhanVien.Size = new System.Drawing.Size(209, 24);
            this.menuQuanLyNhanVien.Text = "Quản Lý Nhân Viên";
            this.menuQuanLyNhanVien.Click += new System.EventHandler(this.menuQuanLyNhanVien_Click);
            // 
            // menuNhatKyHoatDong
            // 
            this.menuNhatKyHoatDong.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.menuNhatKyHoatDong.Name = "menuNhatKyHoatDong";
            this.menuNhatKyHoatDong.Size = new System.Drawing.Size(250, 24);
            this.menuNhatKyHoatDong.Text = "📋 Nhật Ký Hoạt Động (Audit Trail)";
            this.menuNhatKyHoatDong.Click += new System.EventHandler(this.menuNhatKyHoatDong_Click);
            // 
            // toolStripSeparatorTraCuuPhimTat
            // 
            this.toolStripSeparatorTraCuuPhimTat.Name = "toolStripSeparatorTraCuuPhimTat";
            this.toolStripSeparatorTraCuuPhimTat.Size = new System.Drawing.Size(257, 6);
            // 
            // menuTraCuuPhimTat
            // 
            this.menuTraCuuPhimTat.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.menuTraCuuPhimTat.Name = "menuTraCuuPhimTat";
            this.menuTraCuuPhimTat.ShortcutKeyDisplayString = "F1";
            this.menuTraCuuPhimTat.Size = new System.Drawing.Size(260, 24);
            this.menuTraCuuPhimTat.Text = "⌨️ Bảng Tra Cứu Phím Tắt";
            this.menuTraCuuPhimTat.Click += new System.EventHandler(this.menuTraCuuPhimTat_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(257, 6);
            // 
            // menuDangXuat
            // 
            this.menuDangXuat.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.menuDangXuat.ForeColor = System.Drawing.Color.Crimson;
            this.menuDangXuat.Name = "menuDangXuat";
            this.menuDangXuat.Size = new System.Drawing.Size(209, 24);
            this.menuDangXuat.Text = "Đăng Xuất";
            this.menuDangXuat.Click += new System.EventHandler(this.menuDangXuat_Click);
            // 
            // menuThoat
            // 
            this.menuThoat.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.menuThoat.Name = "menuThoat";
            this.menuThoat.Size = new System.Drawing.Size(209, 24);
            this.menuThoat.Text = "Thoát Ứng Dụng";
            this.menuThoat.Click += new System.EventHandler(this.menuThoat_Click);
            // 
            // menuDanhMuc
            // 
            this.menuDanhMuc.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuKhachHang,
            this.menuNhaCungCap,
            this.menuLoaiSanPham,
            this.menuSanPham,
            this.menuKho});
            this.menuDanhMuc.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.menuDanhMuc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(40)))), ((int)(((byte)(217)))));
            this.menuDanhMuc.Name = "menuDanhMuc";
            this.menuDanhMuc.Size = new System.Drawing.Size(166, 22);
            this.menuDanhMuc.Text = "QUẢN LÝ DANH MỤC";
            // 
            // menuKhachHang
            // 
            this.menuKhachHang.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.menuKhachHang.Name = "menuKhachHang";
            this.menuKhachHang.Size = new System.Drawing.Size(189, 24);
            this.menuKhachHang.Text = "Khách Hàng";
            this.menuKhachHang.Click += new System.EventHandler(this.menuKhachHang_Click);
            // 
            // menuNhaCungCap
            // 
            this.menuNhaCungCap.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.menuNhaCungCap.Name = "menuNhaCungCap";
            this.menuNhaCungCap.Size = new System.Drawing.Size(189, 24);
            this.menuNhaCungCap.Text = "Nhà Cung Cấp";
            this.menuNhaCungCap.Click += new System.EventHandler(this.menuNhaCungCap_Click);
            // 
            // menuLoaiSanPham
            // 
            this.menuLoaiSanPham.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.menuLoaiSanPham.Name = "menuLoaiSanPham";
            this.menuLoaiSanPham.Size = new System.Drawing.Size(189, 24);
            this.menuLoaiSanPham.Text = "Loại Sản Phẩm";
            this.menuLoaiSanPham.Click += new System.EventHandler(this.menuLoaiSanPham_Click);
            // 
            // menuSanPham
            // 
            this.menuSanPham.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.menuSanPham.Name = "menuSanPham";
            this.menuSanPham.Size = new System.Drawing.Size(189, 24);
            this.menuSanPham.Text = "Sản Phẩm";
            this.menuSanPham.Click += new System.EventHandler(this.menuSanPham_Click);
            // 
            // menuKho
            // 
            this.menuKho.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.menuKho.Name = "menuKho";
            this.menuKho.Size = new System.Drawing.Size(189, 24);
            this.menuKho.Text = "Kho Hàng";
            this.menuKho.Click += new System.EventHandler(this.menuKho_Click);
            // 
            // menuKeToanBanHang
            // 
            this.menuKeToanBanHang.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuDonDatHang,
            this.menuTraCuuDonDatHang,
            this.toolStripSeparator3,
            this.menuHoaDonBan,
            this.menuTraCuuHoaDon});
            this.menuKeToanBanHang.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.menuKeToanBanHang.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(40)))), ((int)(((byte)(217)))));
            this.menuKeToanBanHang.Name = "menuKeToanBanHang";
            this.menuKeToanBanHang.Size = new System.Drawing.Size(161, 22);
            this.menuKeToanBanHang.Text = "KẾ TOÁN BÁN HÀNG";
            // 
            // menuDonDatHang
            // 
            this.menuDonDatHang.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.menuDonDatHang.Name = "menuDonDatHang";
            this.menuDonDatHang.Size = new System.Drawing.Size(225, 24);
            this.menuDonDatHang.Text = "Lập Đơn Đặt Hàng";
            this.menuDonDatHang.Click += new System.EventHandler(this.menuDonDatHang_Click);
            // 
            // menuTraCuuDonDatHang
            // 
            this.menuTraCuuDonDatHang.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.menuTraCuuDonDatHang.Name = "menuTraCuuDonDatHang";
            this.menuTraCuuDonDatHang.Size = new System.Drawing.Size(225, 24);
            this.menuTraCuuDonDatHang.Text = "Tra Cứu Đơn Đặt Hàng";
            this.menuTraCuuDonDatHang.Click += new System.EventHandler(this.menuTraCuuDonDatHang_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(222, 6);
            // 
            // menuHoaDonBan
            // 
            this.menuHoaDonBan.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.menuHoaDonBan.Name = "menuHoaDonBan";
            this.menuHoaDonBan.Size = new System.Drawing.Size(225, 24);
            this.menuHoaDonBan.Text = "Lập Hóa Đơn Bán";
            this.menuHoaDonBan.Click += new System.EventHandler(this.menuHoaDonBan_Click);
            // 
            // menuTraCuuHoaDon
            // 
            this.menuTraCuuHoaDon.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.menuTraCuuHoaDon.Name = "menuTraCuuHoaDon";
            this.menuTraCuuHoaDon.Size = new System.Drawing.Size(225, 24);
            this.menuTraCuuHoaDon.Text = "Tra Cứu Hóa Đơn Bán";
            this.menuTraCuuHoaDon.Click += new System.EventHandler(this.menuTraCuuHoaDon_Click);
            // 
            // menuQuanLyKho
            // 
            this.menuQuanLyKho.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuTonKho,
            this.menuPhieuXuatKho,
            this.menuTraCuuPhieuXuat});
            this.menuQuanLyKho.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.menuQuanLyKho.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(40)))), ((int)(((byte)(217)))));
            this.menuQuanLyKho.Name = "menuQuanLyKho";
            this.menuQuanLyKho.Size = new System.Drawing.Size(121, 22);
            this.menuQuanLyKho.Text = "QUẢN LÝ KHO";
            // 
            // menuTonKho
            // 
            this.menuTonKho.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.menuTonKho.Name = "menuTonKho";
            this.menuTonKho.Size = new System.Drawing.Size(228, 24);
            this.menuTonKho.Text = "Tra Cứu Tồn Kho";
            this.menuTonKho.Click += new System.EventHandler(this.menuTonKho_Click);
            // 
            // menuPhieuXuatKho
            // 
            this.menuPhieuXuatKho.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.menuPhieuXuatKho.Name = "menuPhieuXuatKho";
            this.menuPhieuXuatKho.Size = new System.Drawing.Size(228, 24);
            this.menuPhieuXuatKho.Text = "Lập Phiếu Xuất Kho";
            this.menuPhieuXuatKho.Click += new System.EventHandler(this.menuPhieuXuatKho_Click);
            // 
            // menuTraCuuPhieuXuat
            // 
            this.menuTraCuuPhieuXuat.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.menuTraCuuPhieuXuat.Name = "menuTraCuuPhieuXuat";
            this.menuTraCuuPhieuXuat.Size = new System.Drawing.Size(228, 24);
            this.menuTraCuuPhieuXuat.Text = "Tra Cứu Phiếu Xuất Kho";
            this.menuTraCuuPhieuXuat.Click += new System.EventHandler(this.menuTraCuuPhieuXuat_Click);
            // 
            // menuChungTuTien
            // 
            this.menuChungTuTien.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuPhieuThu,
            this.menuTraCuuPhieuThu,
            this.toolStripSeparator4,
            this.menuPhieuChi,
            this.menuTraCuuPhieuChi,
            this.toolStripSeparator5,
            this.menuChungTu,
            this.menuTraCuuChungTu});
            this.menuChungTuTien.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.menuChungTuTien.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(40)))), ((int)(((byte)(217)))));
            this.menuChungTuTien.Name = "menuChungTuTien";
            this.menuChungTuTien.Size = new System.Drawing.Size(150, 22);
            this.menuChungTuTien.Text = "CHỨNG TỪ && TIỀN";
            // 
            // menuPhieuThu
            // 
            this.menuPhieuThu.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.menuPhieuThu.Name = "menuPhieuThu";
            this.menuPhieuThu.Size = new System.Drawing.Size(206, 24);
            this.menuPhieuThu.Text = "Lập Phiếu Thu";
            this.menuPhieuThu.Click += new System.EventHandler(this.menuPhieuThu_Click);
            // 
            // menuTraCuuPhieuThu
            // 
            this.menuTraCuuPhieuThu.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.menuTraCuuPhieuThu.Name = "menuTraCuuPhieuThu";
            this.menuTraCuuPhieuThu.Size = new System.Drawing.Size(206, 24);
            this.menuTraCuuPhieuThu.Text = "Tra Cứu Phiếu Thu";
            this.menuTraCuuPhieuThu.Click += new System.EventHandler(this.menuTraCuuPhieuThu_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(203, 6);
            // 
            // menuPhieuChi
            // 
            this.menuPhieuChi.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.menuPhieuChi.Name = "menuPhieuChi";
            this.menuPhieuChi.Size = new System.Drawing.Size(206, 24);
            this.menuPhieuChi.Text = "Lập Phiếu Chi";
            this.menuPhieuChi.Click += new System.EventHandler(this.menuPhieuChi_Click);
            // 
            // menuTraCuuPhieuChi
            // 
            this.menuTraCuuPhieuChi.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.menuTraCuuPhieuChi.Name = "menuTraCuuPhieuChi";
            this.menuTraCuuPhieuChi.Size = new System.Drawing.Size(206, 24);
            this.menuTraCuuPhieuChi.Text = "Tra Cứu Phiếu Chi";
            this.menuTraCuuPhieuChi.Click += new System.EventHandler(this.menuTraCuuPhieuChi_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(203, 6);
            // 
            // menuChungTu
            // 
            this.menuChungTu.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.menuChungTu.Name = "menuChungTu";
            this.menuChungTu.Size = new System.Drawing.Size(206, 24);
            this.menuChungTu.Text = "Lập Chứng Từ";
            this.menuChungTu.Click += new System.EventHandler(this.menuChungTu_Click);
            // 
            // menuTraCuuChungTu
            // 
            this.menuTraCuuChungTu.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.menuTraCuuChungTu.Name = "menuTraCuuChungTu";
            this.menuTraCuuChungTu.Size = new System.Drawing.Size(206, 24);
            this.menuTraCuuChungTu.Text = "Tra Cứu Chứng Từ";
            this.menuTraCuuChungTu.Click += new System.EventHandler(this.menuTraCuuChungTu_Click);
            // 
            // menuKeToanChiTiet
            // 
            this.menuKeToanChiTiet.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuSoChiTietKhachHang,
            this.menuSoChiTietSanPham,
            this.menuSoChiTietHoaDon,
            this.menuBaoCaoTuoiNo});
            this.menuKeToanChiTiet.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.menuKeToanChiTiet.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(40)))), ((int)(((byte)(217)))));
            this.menuKeToanChiTiet.Name = "menuKeToanChiTiet";
            this.menuKeToanChiTiet.Size = new System.Drawing.Size(150, 22);
            this.menuKeToanChiTiet.Text = "KẾ TOÁN CHI TIẾT";
            // 
            // menuSoChiTietKhachHang
            // 
            this.menuSoChiTietKhachHang.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.menuSoChiTietKhachHang.Name = "menuSoChiTietKhachHang";
            this.menuSoChiTietKhachHang.Size = new System.Drawing.Size(236, 24);
            this.menuSoChiTietKhachHang.Text = "Sổ Chi Tiết Khách Hàng";
            this.menuSoChiTietKhachHang.Click += new System.EventHandler(this.menuSoChiTietKhachHang_Click);
            // 
            // menuSoChiTietSanPham
            // 
            this.menuSoChiTietSanPham.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.menuSoChiTietSanPham.Name = "menuSoChiTietSanPham";
            this.menuSoChiTietSanPham.Size = new System.Drawing.Size(236, 24);
            this.menuSoChiTietSanPham.Text = "Sổ Chi Tiết Sản Phẩm";
            this.menuSoChiTietSanPham.Click += new System.EventHandler(this.menuSoChiTietSanPham_Click);
            // 
            // menuSoChiTietHoaDon
            // 
            this.menuSoChiTietHoaDon.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.menuSoChiTietHoaDon.Name = "menuSoChiTietHoaDon";
            this.menuSoChiTietHoaDon.Size = new System.Drawing.Size(236, 24);
            this.menuSoChiTietHoaDon.Text = "Sổ Chi Tiết Hóa Đơn";
            this.menuSoChiTietHoaDon.Click += new System.EventHandler(this.menuSoChiTietHoaDon_Click);
            // 
            // menuBaoCaoTuoiNo
            // 
            this.menuBaoCaoTuoiNo.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.menuBaoCaoTuoiNo.Name = "menuBaoCaoTuoiNo";
            this.menuBaoCaoTuoiNo.Size = new System.Drawing.Size(260, 24);
            this.menuBaoCaoTuoiNo.Text = "Báo Cáo Tuổi Nợ & Quá Hạn";
            this.menuBaoCaoTuoiNo.Click += new System.EventHandler(this.menuBaoCaoTuoiNo_Click);
            // 
            // menuKeToanTongHop
            // 
            this.menuKeToanTongHop.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuBaoCaoDoanhThu,
            this.menuBaoCaoThuChi,
            this.menuBaoCaoTonKho,
            this.menuBaoCaoBieuDo});
            this.menuKeToanTongHop.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.menuKeToanTongHop.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(40)))), ((int)(((byte)(217)))));
            this.menuKeToanTongHop.Name = "menuKeToanTongHop";
            this.menuKeToanTongHop.Size = new System.Drawing.Size(166, 22);
            this.menuKeToanTongHop.Text = "KẾ TOÁN TỔNG HỢP";
            // 
            // menuBaoCaoDoanhThu
            // 
            this.menuBaoCaoDoanhThu.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.menuBaoCaoDoanhThu.Name = "menuBaoCaoDoanhThu";
            this.menuBaoCaoDoanhThu.Size = new System.Drawing.Size(280, 24);
            this.menuBaoCaoDoanhThu.Text = "Báo Cáo Doanh Thu Bán Hàng";
            this.menuBaoCaoDoanhThu.Click += new System.EventHandler(this.menuBaoCaoDoanhThu_Click);
            // 
            // menuBaoCaoThuChi
            // 
            this.menuBaoCaoThuChi.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.menuBaoCaoThuChi.Name = "menuBaoCaoThuChi";
            this.menuBaoCaoThuChi.Size = new System.Drawing.Size(280, 24);
            this.menuBaoCaoThuChi.Text = "Báo Cáo Tổng Hợp Thu - Chi";
            this.menuBaoCaoThuChi.Click += new System.EventHandler(this.menuBaoCaoThuChi_Click);
            // 
            // menuBaoCaoTonKho
            // 
            this.menuBaoCaoTonKho.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.menuBaoCaoTonKho.Name = "menuBaoCaoTonKho";
            this.menuBaoCaoTonKho.Size = new System.Drawing.Size(280, 24);
            this.menuBaoCaoTonKho.Text = "Báo Cáo Tổng Hợp Tồn Kho";
            this.menuBaoCaoTonKho.Click += new System.EventHandler(this.menuBaoCaoTonKho_Click);
            // 
            // menuBaoCaoBieuDo
            // 
            this.menuBaoCaoBieuDo.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.menuBaoCaoBieuDo.Name = "menuBaoCaoBieuDo";
            this.menuBaoCaoBieuDo.Size = new System.Drawing.Size(280, 24);
            this.menuBaoCaoBieuDo.Text = "Biểu Đồ Phân Tích & Trực Quan";
            this.menuBaoCaoBieuDo.Click += new System.EventHandler(this.menuBaoCaoBieuDo_Click);
            // 
            // statusStripMain
            // 
            this.statusStripMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(245)))), ((int)(((byte)(255)))));
            this.statusStripMain.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.statusStripMain.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatusUser,
            this.lblStatusRole,
            this.lblStatusConnection,
            this.lblStatusTime});
            this.statusStripMain.Location = new System.Drawing.Point(0, 723);
            this.statusStripMain.Name = "statusStripMain";
            this.statusStripMain.Size = new System.Drawing.Size(1264, 26);
            this.statusStripMain.TabIndex = 1;
            // 
            // lblStatusUser
            // 
            this.lblStatusUser.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.lblStatusUser.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblStatusUser.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(64)))), ((int)(((byte)(175)))));
            this.lblStatusUser.Name = "lblStatusUser";
            this.lblStatusUser.Size = new System.Drawing.Size(117, 21);
            this.lblStatusUser.Text = "Người dùng: ...";
            // 
            // lblStatusRole
            // 
            this.lblStatusRole.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.lblStatusRole.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblStatusRole.Name = "lblStatusRole";
            this.lblStatusRole.Size = new System.Drawing.Size(76, 21);
            this.lblStatusRole.Text = "Vai trò: ...";
            // 
            // lblStatusConnection
            // 
            this.lblStatusConnection.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.lblStatusConnection.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(185)))), ((int)(((byte)(129)))));
            this.lblStatusConnection.Name = "lblStatusConnection";
            this.lblStatusConnection.Size = new System.Drawing.Size(161, 21);
            this.lblStatusConnection.Text = "● Trạng thái: Sẵn sàng";
            // 
            // lblStatusTime
            // 
            this.lblStatusTime.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblStatusTime.Name = "lblStatusTime";
            this.lblStatusTime.Size = new System.Drawing.Size(895, 21);
            this.lblStatusTime.Spring = true;
            this.lblStatusTime.Text = "07/09/2026 00:00:00";
            this.lblStatusTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // timerClock
            // 
            this.timerClock.Enabled = true;
            this.timerClock.Interval = 1000;
            this.timerClock.Tick += new System.EventHandler(this.timerClock_Tick);
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.pnlHeader.Controls.Add(this.btnHeaderDangXuat);
            this.pnlHeader.Controls.Add(this.btnHeaderDoiMatKhau);
            this.pnlHeader.Controls.Add(this.btnHeaderHotkeys);
            this.pnlHeader.Controls.Add(this.pnlHeaderRightDivider);
            this.pnlHeader.Controls.Add(this.lblUserProfile);
            this.pnlHeader.Controls.Add(this.lblBreadcrumb);
            this.pnlHeader.Controls.Add(this.btnBackToDashboard);
            this.pnlHeader.Controls.Add(this.pnlHeaderDivider);
            this.pnlHeader.Controls.Add(this.lblAppSubtitle);
            this.pnlHeader.Controls.Add(this.lblAppTitle);
            this.pnlHeader.Controls.Add(this.lblLogoIcon);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 28);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1264, 58);
            this.pnlHeader.TabIndex = 2;
            // 
            // btnHeaderDangXuat
            // 
            this.btnHeaderDangXuat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnHeaderDangXuat.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnHeaderDangXuat.FlatAppearance.BorderSize = 1;
            this.btnHeaderDangXuat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHeaderDangXuat.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.btnHeaderDangXuat.Location = new System.Drawing.Point(1152, 14);
            this.btnHeaderDangXuat.Name = "btnHeaderDangXuat";
            this.btnHeaderDangXuat.Size = new System.Drawing.Size(96, 30);
            this.btnHeaderDangXuat.TabIndex = 7;
            this.btnHeaderDangXuat.Text = " Đăng xuất";
            this.btnHeaderDangXuat.UseVisualStyleBackColor = false;
            this.btnHeaderDangXuat.Click += new System.EventHandler(this.menuDangXuat_Click);
            // 
            // btnHeaderDoiMatKhau
            // 
            this.btnHeaderDoiMatKhau.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnHeaderDoiMatKhau.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnHeaderDoiMatKhau.FlatAppearance.BorderSize = 1;
            this.btnHeaderDoiMatKhau.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHeaderDoiMatKhau.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.btnHeaderDoiMatKhau.Location = new System.Drawing.Point(1056, 14);
            this.btnHeaderDoiMatKhau.Name = "btnHeaderDoiMatKhau";
            this.btnHeaderDoiMatKhau.Size = new System.Drawing.Size(88, 30);
            this.btnHeaderDoiMatKhau.TabIndex = 6;
            this.btnHeaderDoiMatKhau.Text = " Đổi MK";
            this.btnHeaderDoiMatKhau.UseVisualStyleBackColor = false;
            this.btnHeaderDoiMatKhau.Click += new System.EventHandler(this.menuDoiMatKhau_Click);
            // 
            // btnHeaderHotkeys
            // 
            this.btnHeaderHotkeys.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnHeaderHotkeys.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnHeaderHotkeys.FlatAppearance.BorderSize = 1;
            this.btnHeaderHotkeys.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHeaderHotkeys.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.btnHeaderHotkeys.Location = new System.Drawing.Point(940, 14);
            this.btnHeaderHotkeys.Name = "btnHeaderHotkeys";
            this.btnHeaderHotkeys.Size = new System.Drawing.Size(108, 30);
            this.btnHeaderHotkeys.TabIndex = 10;
            this.btnHeaderHotkeys.Text = " Phím tắt (F1)";
            this.btnHeaderHotkeys.UseVisualStyleBackColor = false;
            this.btnHeaderHotkeys.Click += new System.EventHandler(this.btnHeaderHotkeys_Click);
            // 
            // pnlHeaderRightDivider
            // 
            this.pnlHeaderRightDivider.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlHeaderRightDivider.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.pnlHeaderRightDivider.Location = new System.Drawing.Point(926, 14);
            this.pnlHeaderRightDivider.Name = "pnlHeaderRightDivider";
            this.pnlHeaderRightDivider.Size = new System.Drawing.Size(1, 30);
            this.pnlHeaderRightDivider.TabIndex = 9;
            // 
            // lblUserProfile
            // 
            this.lblUserProfile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblUserProfile.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblUserProfile.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.lblUserProfile.Location = new System.Drawing.Point(520, 18);
            this.lblUserProfile.Name = "lblUserProfile";
            this.lblUserProfile.Size = new System.Drawing.Size(395, 22);
            this.lblUserProfile.TabIndex = 5;
            this.lblUserProfile.Text = "👤 Nguyễn Văn Quản Trị (Quản trị viên)";
            this.lblUserProfile.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlHeaderDivider
            // 
            this.pnlHeaderDivider.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.pnlHeaderDivider.Location = new System.Drawing.Point(304, 14);
            this.pnlHeaderDivider.Name = "pnlHeaderDivider";
            this.pnlHeaderDivider.Size = new System.Drawing.Size(1, 30);
            this.pnlHeaderDivider.TabIndex = 8;
            // 
            // btnBackToDashboard
            // 
            this.btnBackToDashboard.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBackToDashboard.FlatAppearance.BorderSize = 1;
            this.btnBackToDashboard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBackToDashboard.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.btnBackToDashboard.Location = new System.Drawing.Point(318, 14);
            this.btnBackToDashboard.Name = "btnBackToDashboard";
            this.btnBackToDashboard.Size = new System.Drawing.Size(155, 30);
            this.btnBackToDashboard.TabIndex = 4;
            this.btnBackToDashboard.Text = " Bảng Điều Khiển";
            this.btnBackToDashboard.UseVisualStyleBackColor = false;
            this.btnBackToDashboard.Visible = false;
            this.btnBackToDashboard.Click += new System.EventHandler(this.btnBackToDashboard_Click);
            // 
            // lblBreadcrumb
            // 
            this.lblBreadcrumb.AutoSize = true;
            this.lblBreadcrumb.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblBreadcrumb.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.lblBreadcrumb.Location = new System.Drawing.Point(318, 19);
            this.lblBreadcrumb.Name = "lblBreadcrumb";
            this.lblBreadcrumb.Size = new System.Drawing.Size(277, 20);
            this.lblBreadcrumb.TabIndex = 3;
            this.lblBreadcrumb.Text = "Trang Chủ / Bảng Điều Khiển Tổng Quan";
            this.lblBreadcrumb.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblAppSubtitle
            // 
            this.lblAppSubtitle.AutoSize = true;
            this.lblAppSubtitle.Font = new System.Drawing.Font("Segoe UI", 7.5F);
            this.lblAppSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.lblAppSubtitle.Location = new System.Drawing.Point(46, 33);
            this.lblAppSubtitle.Name = "lblAppSubtitle";
            this.lblAppSubtitle.Size = new System.Drawing.Size(240, 17);
            this.lblAppSubtitle.TabIndex = 2;
            this.lblAppSubtitle.Text = "Hệ thống Kế toán Bán hàng & Quản trị Doanh nghiệp";
            // 
            // lblAppTitle
            // 
            this.lblAppTitle.AutoSize = true;
            this.lblAppTitle.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblAppTitle.ForeColor = System.Drawing.Color.White;
            this.lblAppTitle.Location = new System.Drawing.Point(44, 9);
            this.lblAppTitle.Name = "lblAppTitle";
            this.lblAppTitle.Size = new System.Drawing.Size(245, 25);
            this.lblAppTitle.TabIndex = 1;
            this.lblAppTitle.Text = "DNQH KẾ TOÁN BÁN HÀNG";
            // 
            // lblLogoIcon
            // 
            this.lblLogoIcon.AutoSize = true;
            this.lblLogoIcon.Font = new System.Drawing.Font("Segoe UI", 15F);
            this.lblLogoIcon.ForeColor = System.Drawing.Color.White;
            this.lblLogoIcon.Location = new System.Drawing.Point(10, 11);
            this.lblLogoIcon.Name = "lblLogoIcon";
            this.lblLogoIcon.Size = new System.Drawing.Size(43, 35);
            this.lblLogoIcon.TabIndex = 0;
            this.lblLogoIcon.Text = "💼";
            // 
            // pnlSidebar
            // 
            this.pnlSidebar.AutoScroll = true;
            this.pnlSidebar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.pnlSidebar.Controls.Add(this.btnNavTaiKhoan);
            this.pnlSidebar.Controls.Add(this.btnNavNhanVien);
            this.pnlSidebar.Controls.Add(this.btnNavKho);
            this.pnlSidebar.Controls.Add(this.btnNavLoaiSanPham);
            this.pnlSidebar.Controls.Add(this.btnNavSanPham);
            this.pnlSidebar.Controls.Add(this.btnNavNhaCungCap);
            this.pnlSidebar.Controls.Add(this.btnNavKhachHang);
            this.pnlSidebar.Controls.Add(this.lblNavGroupAdmin);
            this.pnlSidebar.Controls.Add(this.btnNavBaoCaoTongHop);
            this.pnlSidebar.Controls.Add(this.btnNavKeToanChiTiet);
            this.pnlSidebar.Controls.Add(this.lblNavGroupReport);
            this.pnlSidebar.Controls.Add(this.btnNavChungTu);
            this.pnlSidebar.Controls.Add(this.btnNavPhieuChi);
            this.pnlSidebar.Controls.Add(this.btnNavPhieuThu);
            this.pnlSidebar.Controls.Add(this.lblNavGroupCash);
            this.pnlSidebar.Controls.Add(this.btnNavTonKho);
            this.pnlSidebar.Controls.Add(this.btnNavXuatKho);
            this.pnlSidebar.Controls.Add(this.lblNavGroupWarehouse);
            this.pnlSidebar.Controls.Add(this.btnNavHoaDon);
            this.pnlSidebar.Controls.Add(this.btnNavDonHang);
            this.pnlSidebar.Controls.Add(this.lblNavGroupSales);
            this.pnlSidebar.Controls.Add(this.btnNavDashboard);
            this.pnlSidebar.Controls.Add(this.lblSidebarHeader);
            this.pnlSidebar.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlSidebar.Location = new System.Drawing.Point(0, 86);
            this.pnlSidebar.Name = "pnlSidebar";
            this.pnlSidebar.Size = new System.Drawing.Size(230, 637);
            this.pnlSidebar.TabIndex = 3;
            this.pnlSidebar.Visible = false;
            // 
            // btnNavTaiKhoan
            // 
            this.btnNavTaiKhoan.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNavTaiKhoan.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnNavTaiKhoan.FlatAppearance.BorderSize = 0;
            this.btnNavTaiKhoan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNavTaiKhoan.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnNavTaiKhoan.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.btnNavTaiKhoan.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNavTaiKhoan.Location = new System.Drawing.Point(0, 770);
            this.btnNavTaiKhoan.Name = "btnNavTaiKhoan";
            this.btnNavTaiKhoan.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnNavTaiKhoan.Size = new System.Drawing.Size(209, 36);
            this.btnNavTaiKhoan.TabIndex = 22;
            this.btnNavTaiKhoan.Text = "🛡️  Quản Lý Tài Khoản";
            this.btnNavTaiKhoan.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNavTaiKhoan.UseVisualStyleBackColor = true;
            this.btnNavTaiKhoan.Click += new System.EventHandler(this.menuQuanLyTaiKhoan_Click);
            // 
            // btnNavNhanVien
            // 
            this.btnNavNhanVien.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNavNhanVien.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnNavNhanVien.FlatAppearance.BorderSize = 0;
            this.btnNavNhanVien.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNavNhanVien.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnNavNhanVien.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.btnNavNhanVien.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNavNhanVien.Location = new System.Drawing.Point(0, 734);
            this.btnNavNhanVien.Name = "btnNavNhanVien";
            this.btnNavNhanVien.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnNavNhanVien.Size = new System.Drawing.Size(209, 36);
            this.btnNavNhanVien.TabIndex = 21;
            this.btnNavNhanVien.Text = "👔  Quản Lý Nhân Viên";
            this.btnNavNhanVien.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNavNhanVien.UseVisualStyleBackColor = true;
            this.btnNavNhanVien.Click += new System.EventHandler(this.menuQuanLyNhanVien_Click);
            // 
            // btnNavKho
            // 
            this.btnNavKho.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNavKho.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnNavKho.FlatAppearance.BorderSize = 0;
            this.btnNavKho.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNavKho.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnNavKho.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.btnNavKho.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNavKho.Location = new System.Drawing.Point(0, 698);
            this.btnNavKho.Name = "btnNavKho";
            this.btnNavKho.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnNavKho.Size = new System.Drawing.Size(209, 36);
            this.btnNavKho.TabIndex = 20;
            this.btnNavKho.Text = "🏢  Danh Mục Kho";
            this.btnNavKho.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNavKho.UseVisualStyleBackColor = true;
            this.btnNavKho.Click += new System.EventHandler(this.menuKho_Click);
            // 
            // btnNavLoaiSanPham
            // 
            this.btnNavLoaiSanPham.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNavLoaiSanPham.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnNavLoaiSanPham.FlatAppearance.BorderSize = 0;
            this.btnNavLoaiSanPham.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNavLoaiSanPham.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnNavLoaiSanPham.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.btnNavLoaiSanPham.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNavLoaiSanPham.Location = new System.Drawing.Point(0, 662);
            this.btnNavLoaiSanPham.Name = "btnNavLoaiSanPham";
            this.btnNavLoaiSanPham.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnNavLoaiSanPham.Size = new System.Drawing.Size(209, 36);
            this.btnNavLoaiSanPham.TabIndex = 19;
            this.btnNavLoaiSanPham.Text = "🗂️  Loại Sản Phẩm";
            this.btnNavLoaiSanPham.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNavLoaiSanPham.UseVisualStyleBackColor = true;
            this.btnNavLoaiSanPham.Click += new System.EventHandler(this.menuLoaiSanPham_Click);
            // 
            // btnNavSanPham
            // 
            this.btnNavSanPham.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNavSanPham.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnNavSanPham.FlatAppearance.BorderSize = 0;
            this.btnNavSanPham.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNavSanPham.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnNavSanPham.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.btnNavSanPham.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNavSanPham.Location = new System.Drawing.Point(0, 626);
            this.btnNavSanPham.Name = "btnNavSanPham";
            this.btnNavSanPham.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnNavSanPham.Size = new System.Drawing.Size(209, 36);
            this.btnNavSanPham.TabIndex = 18;
            this.btnNavSanPham.Text = "🏷️  Sản Phẩm";
            this.btnNavSanPham.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNavSanPham.UseVisualStyleBackColor = true;
            this.btnNavSanPham.Click += new System.EventHandler(this.menuSanPham_Click);
            // 
            // btnNavNhaCungCap
            // 
            this.btnNavNhaCungCap.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNavNhaCungCap.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnNavNhaCungCap.FlatAppearance.BorderSize = 0;
            this.btnNavNhaCungCap.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNavNhaCungCap.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnNavNhaCungCap.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.btnNavNhaCungCap.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNavNhaCungCap.Location = new System.Drawing.Point(0, 590);
            this.btnNavNhaCungCap.Name = "btnNavNhaCungCap";
            this.btnNavNhaCungCap.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnNavNhaCungCap.Size = new System.Drawing.Size(209, 36);
            this.btnNavNhaCungCap.TabIndex = 17;
            this.btnNavNhaCungCap.Text = "🏭  Nhà Cung Cấp";
            this.btnNavNhaCungCap.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNavNhaCungCap.UseVisualStyleBackColor = true;
            this.btnNavNhaCungCap.Click += new System.EventHandler(this.menuNhaCungCap_Click);
            // 
            // btnNavKhachHang
            // 
            this.btnNavKhachHang.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNavKhachHang.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnNavKhachHang.FlatAppearance.BorderSize = 0;
            this.btnNavKhachHang.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNavKhachHang.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnNavKhachHang.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.btnNavKhachHang.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNavKhachHang.Location = new System.Drawing.Point(0, 554);
            this.btnNavKhachHang.Name = "btnNavKhachHang";
            this.btnNavKhachHang.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnNavKhachHang.Size = new System.Drawing.Size(209, 36);
            this.btnNavKhachHang.TabIndex = 16;
            this.btnNavKhachHang.Text = "👥  Khách Hàng";
            this.btnNavKhachHang.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNavKhachHang.UseVisualStyleBackColor = true;
            this.btnNavKhachHang.Click += new System.EventHandler(this.menuKhachHang_Click);
            // 
            // lblNavGroupAdmin
            // 
            this.lblNavGroupAdmin.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblNavGroupAdmin.Font = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Bold);
            this.lblNavGroupAdmin.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(132)))), ((int)(((byte)(252)))));
            this.lblNavGroupAdmin.Location = new System.Drawing.Point(0, 526);
            this.lblNavGroupAdmin.Name = "lblNavGroupAdmin";
            this.lblNavGroupAdmin.Padding = new System.Windows.Forms.Padding(12, 6, 0, 2);
            this.lblNavGroupAdmin.Size = new System.Drawing.Size(209, 28);
            this.lblNavGroupAdmin.TabIndex = 15;
            this.lblNavGroupAdmin.Text = "DANH MỤC && QUẢN TRỊ";
            // 
            // btnNavBaoCaoTongHop
            // 
            this.btnNavBaoCaoTongHop.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNavBaoCaoTongHop.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnNavBaoCaoTongHop.FlatAppearance.BorderSize = 0;
            this.btnNavBaoCaoTongHop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNavBaoCaoTongHop.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnNavBaoCaoTongHop.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.btnNavBaoCaoTongHop.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNavBaoCaoTongHop.Location = new System.Drawing.Point(0, 490);
            this.btnNavBaoCaoTongHop.Name = "btnNavBaoCaoTongHop";
            this.btnNavBaoCaoTongHop.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnNavBaoCaoTongHop.Size = new System.Drawing.Size(209, 36);
            this.btnNavBaoCaoTongHop.TabIndex = 14;
            this.btnNavBaoCaoTongHop.Text = "📈  Báo Cáo Tổng Hợp";
            this.btnNavBaoCaoTongHop.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNavBaoCaoTongHop.UseVisualStyleBackColor = true;
            this.btnNavBaoCaoTongHop.Click += new System.EventHandler(this.menuBaoCaoDoanhThu_Click);
            // 
            // btnNavKeToanChiTiet
            // 
            this.btnNavKeToanChiTiet.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNavKeToanChiTiet.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnNavKeToanChiTiet.FlatAppearance.BorderSize = 0;
            this.btnNavKeToanChiTiet.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNavKeToanChiTiet.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnNavKeToanChiTiet.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.btnNavKeToanChiTiet.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNavKeToanChiTiet.Location = new System.Drawing.Point(0, 454);
            this.btnNavKeToanChiTiet.Name = "btnNavKeToanChiTiet";
            this.btnNavKeToanChiTiet.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnNavKeToanChiTiet.Size = new System.Drawing.Size(209, 36);
            this.btnNavKeToanChiTiet.TabIndex = 13;
            this.btnNavKeToanChiTiet.Text = "📚  Sổ Chi Tiết";
            this.btnNavKeToanChiTiet.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNavKeToanChiTiet.UseVisualStyleBackColor = true;
            this.btnNavKeToanChiTiet.Click += new System.EventHandler(this.menuSoChiTietKhachHang_Click);
            // 
            // lblNavGroupReport
            // 
            this.lblNavGroupReport.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblNavGroupReport.Font = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Bold);
            this.lblNavGroupReport.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(114)))), ((int)(((byte)(182)))));
            this.lblNavGroupReport.Location = new System.Drawing.Point(0, 426);
            this.lblNavGroupReport.Name = "lblNavGroupReport";
            this.lblNavGroupReport.Padding = new System.Windows.Forms.Padding(12, 6, 0, 2);
            this.lblNavGroupReport.Size = new System.Drawing.Size(209, 28);
            this.lblNavGroupReport.TabIndex = 12;
            this.lblNavGroupReport.Text = "BÁO CÁO && SỔ SÁCH";
            // 
            // btnNavChungTu
            // 
            this.btnNavChungTu.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNavChungTu.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnNavChungTu.FlatAppearance.BorderSize = 0;
            this.btnNavChungTu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNavChungTu.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnNavChungTu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.btnNavChungTu.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNavChungTu.Location = new System.Drawing.Point(0, 390);
            this.btnNavChungTu.Name = "btnNavChungTu";
            this.btnNavChungTu.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnNavChungTu.Size = new System.Drawing.Size(209, 36);
            this.btnNavChungTu.TabIndex = 11;
            this.btnNavChungTu.Text = "📑  Chứng Từ Kế Toán";
            this.btnNavChungTu.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNavChungTu.UseVisualStyleBackColor = true;
            this.btnNavChungTu.Click += new System.EventHandler(this.menuChungTu_Click);
            // 
            // btnNavPhieuChi
            // 
            this.btnNavPhieuChi.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNavPhieuChi.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnNavPhieuChi.FlatAppearance.BorderSize = 0;
            this.btnNavPhieuChi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNavPhieuChi.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnNavPhieuChi.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.btnNavPhieuChi.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNavPhieuChi.Location = new System.Drawing.Point(0, 354);
            this.btnNavPhieuChi.Name = "btnNavPhieuChi";
            this.btnNavPhieuChi.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnNavPhieuChi.Size = new System.Drawing.Size(209, 36);
            this.btnNavPhieuChi.TabIndex = 10;
            this.btnNavPhieuChi.Text = "💸  Lập Phiếu Chi";
            this.btnNavPhieuChi.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNavPhieuChi.UseVisualStyleBackColor = true;
            this.btnNavPhieuChi.Click += new System.EventHandler(this.menuPhieuChi_Click);
            // 
            // btnNavPhieuThu
            // 
            this.btnNavPhieuThu.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNavPhieuThu.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnNavPhieuThu.FlatAppearance.BorderSize = 0;
            this.btnNavPhieuThu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNavPhieuThu.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnNavPhieuThu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.btnNavPhieuThu.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNavPhieuThu.Location = new System.Drawing.Point(0, 318);
            this.btnNavPhieuThu.Name = "btnNavPhieuThu";
            this.btnNavPhieuThu.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnNavPhieuThu.Size = new System.Drawing.Size(209, 36);
            this.btnNavPhieuThu.TabIndex = 9;
            this.btnNavPhieuThu.Text = "💰  Lập Phiếu Thu";
            this.btnNavPhieuThu.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNavPhieuThu.UseVisualStyleBackColor = true;
            this.btnNavPhieuThu.Click += new System.EventHandler(this.menuPhieuThu_Click);
            // 
            // lblNavGroupCash
            // 
            this.lblNavGroupCash.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblNavGroupCash.Font = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Bold);
            this.lblNavGroupCash.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(211)))), ((int)(((byte)(153)))));
            this.lblNavGroupCash.Location = new System.Drawing.Point(0, 290);
            this.lblNavGroupCash.Name = "lblNavGroupCash";
            this.lblNavGroupCash.Padding = new System.Windows.Forms.Padding(12, 6, 0, 2);
            this.lblNavGroupCash.Size = new System.Drawing.Size(209, 28);
            this.lblNavGroupCash.TabIndex = 8;
            this.lblNavGroupCash.Text = "CHỨNG TỪ && TIỀN TỆ";
            // 
            // btnNavTonKho
            // 
            this.btnNavTonKho.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNavTonKho.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnNavTonKho.FlatAppearance.BorderSize = 0;
            this.btnNavTonKho.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNavTonKho.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnNavTonKho.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.btnNavTonKho.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNavTonKho.Location = new System.Drawing.Point(0, 254);
            this.btnNavTonKho.Name = "btnNavTonKho";
            this.btnNavTonKho.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnNavTonKho.Size = new System.Drawing.Size(209, 36);
            this.btnNavTonKho.TabIndex = 7;
            this.btnNavTonKho.Text = "📦  Tra Cứu Tồn Kho";
            this.btnNavTonKho.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNavTonKho.UseVisualStyleBackColor = true;
            this.btnNavTonKho.Click += new System.EventHandler(this.menuTonKho_Click);
            // 
            // btnNavXuatKho
            // 
            this.btnNavXuatKho.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNavXuatKho.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnNavXuatKho.FlatAppearance.BorderSize = 0;
            this.btnNavXuatKho.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNavXuatKho.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnNavXuatKho.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.btnNavXuatKho.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNavXuatKho.Location = new System.Drawing.Point(0, 218);
            this.btnNavXuatKho.Name = "btnNavXuatKho";
            this.btnNavXuatKho.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnNavXuatKho.Size = new System.Drawing.Size(209, 36);
            this.btnNavXuatKho.TabIndex = 6;
            this.btnNavXuatKho.Text = "📤  Phiếu Xuất Kho";
            this.btnNavXuatKho.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNavXuatKho.UseVisualStyleBackColor = true;
            this.btnNavXuatKho.Click += new System.EventHandler(this.menuPhieuXuatKho_Click);
            // 
            // lblNavGroupWarehouse
            // 
            this.lblNavGroupWarehouse.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblNavGroupWarehouse.Font = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Bold);
            this.lblNavGroupWarehouse.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(191)))), ((int)(((byte)(36)))));
            this.lblNavGroupWarehouse.Location = new System.Drawing.Point(0, 190);
            this.lblNavGroupWarehouse.Name = "lblNavGroupWarehouse";
            this.lblNavGroupWarehouse.Padding = new System.Windows.Forms.Padding(12, 6, 0, 2);
            this.lblNavGroupWarehouse.Size = new System.Drawing.Size(209, 28);
            this.lblNavGroupWarehouse.TabIndex = 5;
            this.lblNavGroupWarehouse.Text = "QUẢN LÝ KHO HÀNG";
            // 
            // btnNavHoaDon
            // 
            this.btnNavHoaDon.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNavHoaDon.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnNavHoaDon.FlatAppearance.BorderSize = 0;
            this.btnNavHoaDon.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNavHoaDon.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnNavHoaDon.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.btnNavHoaDon.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNavHoaDon.Location = new System.Drawing.Point(0, 154);
            this.btnNavHoaDon.Name = "btnNavHoaDon";
            this.btnNavHoaDon.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnNavHoaDon.Size = new System.Drawing.Size(209, 36);
            this.btnNavHoaDon.TabIndex = 4;
            this.btnNavHoaDon.Text = "📄  Hóa Đơn Bán Hàng";
            this.btnNavHoaDon.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNavHoaDon.UseVisualStyleBackColor = true;
            this.btnNavHoaDon.Click += new System.EventHandler(this.menuHoaDonBan_Click);
            // 
            // btnNavDonHang
            // 
            this.btnNavDonHang.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNavDonHang.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnNavDonHang.FlatAppearance.BorderSize = 0;
            this.btnNavDonHang.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNavDonHang.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnNavDonHang.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.btnNavDonHang.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNavDonHang.Location = new System.Drawing.Point(0, 118);
            this.btnNavDonHang.Name = "btnNavDonHang";
            this.btnNavDonHang.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnNavDonHang.Size = new System.Drawing.Size(209, 36);
            this.btnNavDonHang.TabIndex = 3;
            this.btnNavDonHang.Text = "📝  Đơn Đặt Hàng";
            this.btnNavDonHang.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNavDonHang.UseVisualStyleBackColor = true;
            this.btnNavDonHang.Click += new System.EventHandler(this.menuDonDatHang_Click);
            // 
            // lblNavGroupSales
            // 
            this.lblNavGroupSales.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblNavGroupSales.Font = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Bold);
            this.lblNavGroupSales.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(189)))), ((int)(((byte)(248)))));
            this.lblNavGroupSales.Location = new System.Drawing.Point(0, 90);
            this.lblNavGroupSales.Name = "lblNavGroupSales";
            this.lblNavGroupSales.Padding = new System.Windows.Forms.Padding(12, 6, 0, 2);
            this.lblNavGroupSales.Size = new System.Drawing.Size(209, 28);
            this.lblNavGroupSales.TabIndex = 2;
            this.lblNavGroupSales.Text = "KẾ TOÁN BÁN HÀNG";
            // 
            // btnNavDashboard
            // 
            this.btnNavDashboard.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(92)))), ((int)(((byte)(246)))));
            this.btnNavDashboard.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNavDashboard.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnNavDashboard.FlatAppearance.BorderSize = 0;
            this.btnNavDashboard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNavDashboard.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnNavDashboard.ForeColor = System.Drawing.Color.White;
            this.btnNavDashboard.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNavDashboard.Location = new System.Drawing.Point(0, 42);
            this.btnNavDashboard.Name = "btnNavDashboard";
            this.btnNavDashboard.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnNavDashboard.Size = new System.Drawing.Size(209, 48);
            this.btnNavDashboard.TabIndex = 1;
            this.btnNavDashboard.Text = "📊  Bảng Điều Khiển";
            this.btnNavDashboard.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNavDashboard.UseVisualStyleBackColor = false;
            this.btnNavDashboard.Click += new System.EventHandler(this.btnNavDashboard_Click);
            // 
            // lblSidebarHeader
            // 
            this.lblSidebarHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblSidebarHeader.Font = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Bold);
            this.lblSidebarHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(132)))), ((int)(((byte)(252)))));
            this.lblSidebarHeader.Location = new System.Drawing.Point(0, 0);
            this.lblSidebarHeader.Name = "lblSidebarHeader";
            this.lblSidebarHeader.Padding = new System.Windows.Forms.Padding(12, 12, 0, 6);
            this.lblSidebarHeader.Size = new System.Drawing.Size(209, 42);
            this.lblSidebarHeader.TabIndex = 0;
            this.lblSidebarHeader.Text = "MENU CHỨC NĂNG";
            // 
            // pnlWorkspace
            // 
            this.pnlWorkspace.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.pnlWorkspace.Controls.Add(this.pnlDashboard);
            this.pnlWorkspace.Controls.Add(this.pnlChildContainer);
            this.pnlWorkspace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlWorkspace.Location = new System.Drawing.Point(230, 86);
            this.pnlWorkspace.Name = "pnlWorkspace";
            this.pnlWorkspace.Size = new System.Drawing.Size(1034, 637);
            this.pnlWorkspace.TabIndex = 4;
            // 
            // pnlDashboard
            // 
            this.pnlDashboard.AutoScroll = true;
            this.pnlDashboard.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.pnlDashboard.Controls.Add(this.pnlRoleNote);
            this.pnlDashboard.Controls.Add(this.pnlQuickActionsContainer);
            this.pnlDashboard.Controls.Add(this.pnlStockAlertContainer);
            this.pnlDashboard.Controls.Add(this.tlpKPICards);
            this.pnlDashboard.Controls.Add(this.pnlWelcomeBanner);
            this.pnlDashboard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDashboard.Location = new System.Drawing.Point(0, 0);
            this.pnlDashboard.Name = "pnlDashboard";
            this.pnlDashboard.Padding = new System.Windows.Forms.Padding(20, 16, 20, 20);
            this.pnlDashboard.Size = new System.Drawing.Size(1034, 637);
            this.pnlDashboard.TabIndex = 0;
            // 
            // pnlRoleNote
            // 
            this.pnlRoleNote.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.pnlRoleNote.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlRoleNote.Controls.Add(this.lblRoleNoteText);
            this.pnlRoleNote.Controls.Add(this.lblRoleNoteTitle);
            this.pnlRoleNote.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlRoleNote.Location = new System.Drawing.Point(20, 422);
            this.pnlRoleNote.Name = "pnlRoleNote";
            this.pnlRoleNote.Padding = new System.Windows.Forms.Padding(16, 12, 16, 12);
            this.pnlRoleNote.Size = new System.Drawing.Size(994, 95);
            this.pnlRoleNote.TabIndex = 3;
            // 
            // lblRoleNoteText
            // 
            this.lblRoleNoteText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblRoleNoteText.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblRoleNoteText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblRoleNoteText.Location = new System.Drawing.Point(16, 36);
            this.lblRoleNoteText.Name = "lblRoleNoteText";
            this.lblRoleNoteText.Size = new System.Drawing.Size(960, 45);
            this.lblRoleNoteText.TabIndex = 1;
            this.lblRoleNoteText.Text = "Hệ thống bảo đảm 100% Parameterized SQL, giao dịch nguyên tử nhiều bảng và tuân t" +
    "hủ nguyên tắc Zero Test Pollution.\r\nMọi hành động nghiệp vụ đều được gắn mã Corr" +
    "elationId và ghi nhật ký hệ thống.";
            // 
            // lblRoleNoteTitle
            // 
            this.lblRoleNoteTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblRoleNoteTitle.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblRoleNoteTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(64)))), ((int)(((byte)(175)))));
            this.lblRoleNoteTitle.Location = new System.Drawing.Point(16, 12);
            this.lblRoleNoteTitle.Name = "lblRoleNoteTitle";
            this.lblRoleNoteTitle.Size = new System.Drawing.Size(960, 24);
            this.lblRoleNoteTitle.TabIndex = 0;
            this.lblRoleNoteTitle.Text = "🛡️ QUY CHUẨN AN TOÀN DỮ LIỆU & BẢO MẬT HỆ THỐNG";
            // 
            // pnlStockAlertContainer
            // 
            this.pnlStockAlertContainer.BackColor = System.Drawing.Color.White;
            this.pnlStockAlertContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlStockAlertContainer.Controls.Add(this.dgvStockAlert);
            this.pnlStockAlertContainer.Controls.Add(this.pnlStockSafeBanner);
            this.pnlStockAlertContainer.Controls.Add(this.pnlStockAlertHeader);
            this.pnlStockAlertContainer.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlStockAlertContainer.Location = new System.Drawing.Point(20, 218);
            this.pnlStockAlertContainer.Margin = new System.Windows.Forms.Padding(0, 16, 0, 16);
            this.pnlStockAlertContainer.Name = "pnlStockAlertContainer";
            this.pnlStockAlertContainer.Padding = new System.Windows.Forms.Padding(16, 12, 16, 12);
            this.pnlStockAlertContainer.Size = new System.Drawing.Size(994, 250);
            this.pnlStockAlertContainer.TabIndex = 2;
            // 
            // pnlStockAlertHeader
            // 
            this.pnlStockAlertHeader.Controls.Add(this.flpStockAlertActions);
            this.pnlStockAlertHeader.Controls.Add(this.lblStockAlertTitle);
            this.pnlStockAlertHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlStockAlertHeader.Location = new System.Drawing.Point(16, 12);
            this.pnlStockAlertHeader.Name = "pnlStockAlertHeader";
            this.pnlStockAlertHeader.Size = new System.Drawing.Size(960, 36);
            this.pnlStockAlertHeader.TabIndex = 0;
            // 
            // lblStockAlertTitle
            // 
            this.lblStockAlertTitle.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblStockAlertTitle.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblStockAlertTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(65)))), ((int)(((byte)(12)))));
            this.lblStockAlertTitle.Location = new System.Drawing.Point(0, 0);
            this.lblStockAlertTitle.Name = "lblStockAlertTitle";
            this.lblStockAlertTitle.Size = new System.Drawing.Size(460, 36);
            this.lblStockAlertTitle.TabIndex = 0;
            this.lblStockAlertTitle.Text = "⚠️ CẢNH BÁO TỒN KHO AN TOÀN (MẶT HÀNG SẮP HẾT)";
            this.lblStockAlertTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // flpStockAlertActions
            // 
            this.flpStockAlertActions.Controls.Add(this.btnGoToTonKho);
            this.flpStockAlertActions.Controls.Add(this.btnStockAlertRefresh);
            this.flpStockAlertActions.Controls.Add(this.cboSafetyThreshold);
            this.flpStockAlertActions.Controls.Add(this.lblThresholdPrompt);
            this.flpStockAlertActions.Dock = System.Windows.Forms.DockStyle.Right;
            this.flpStockAlertActions.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flpStockAlertActions.Location = new System.Drawing.Point(460, 0);
            this.flpStockAlertActions.Name = "flpStockAlertActions";
            this.flpStockAlertActions.Size = new System.Drawing.Size(500, 36);
            this.flpStockAlertActions.TabIndex = 1;
            // 
            // btnGoToTonKho
            // 
            this.btnGoToTonKho.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(242)))), ((int)(((byte)(255)))));
            this.btnGoToTonKho.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGoToTonKho.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(180)))), ((int)(((byte)(252)))));
            this.btnGoToTonKho.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGoToTonKho.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnGoToTonKho.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(56)))), ((int)(((byte)(202)))));
            this.btnGoToTonKho.Location = new System.Drawing.Point(371, 3);
            this.btnGoToTonKho.Margin = new System.Windows.Forms.Padding(4, 3, 0, 3);
            this.btnGoToTonKho.Name = "btnGoToTonKho";
            this.btnGoToTonKho.Size = new System.Drawing.Size(125, 30);
            this.btnGoToTonKho.TabIndex = 3;
            this.btnGoToTonKho.Text = "🔍 Xem Tồn Kho";
            this.btnGoToTonKho.UseVisualStyleBackColor = false;
            this.btnGoToTonKho.Click += new System.EventHandler(this.btnGoToTonKho_Click);
            // 
            // btnStockAlertRefresh
            // 
            this.btnStockAlertRefresh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.btnStockAlertRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnStockAlertRefresh.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.btnStockAlertRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStockAlertRefresh.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnStockAlertRefresh.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.btnStockAlertRefresh.Location = new System.Drawing.Point(272, 3);
            this.btnStockAlertRefresh.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnStockAlertRefresh.Name = "btnStockAlertRefresh";
            this.btnStockAlertRefresh.Size = new System.Drawing.Size(91, 30);
            this.btnStockAlertRefresh.TabIndex = 2;
            this.btnStockAlertRefresh.Text = "🔄 Làm mới";
            this.btnStockAlertRefresh.UseVisualStyleBackColor = false;
            this.btnStockAlertRefresh.Click += new System.EventHandler(this.btnStockAlertRefresh_Click);
            // 
            // cboSafetyThreshold
            // 
            this.cboSafetyThreshold.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSafetyThreshold.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cboSafetyThreshold.FormattingEnabled = true;
            this.cboSafetyThreshold.Location = new System.Drawing.Point(148, 5);
            this.cboSafetyThreshold.Margin = new System.Windows.Forms.Padding(4, 5, 4, 3);
            this.cboSafetyThreshold.Name = "cboSafetyThreshold";
            this.cboSafetyThreshold.Size = new System.Drawing.Size(116, 23);
            this.cboSafetyThreshold.TabIndex = 1;
            this.cboSafetyThreshold.SelectedIndexChanged += new System.EventHandler(this.cboSafetyThreshold_SelectedIndexChanged);
            // 
            // lblThresholdPrompt
            // 
            this.lblThresholdPrompt.AutoSize = true;
            this.lblThresholdPrompt.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblThresholdPrompt.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblThresholdPrompt.Location = new System.Drawing.Point(54, 8);
            this.lblThresholdPrompt.Margin = new System.Windows.Forms.Padding(0, 8, 4, 0);
            this.lblThresholdPrompt.Name = "lblThresholdPrompt";
            this.lblThresholdPrompt.Size = new System.Drawing.Size(90, 15);
            this.lblThresholdPrompt.TabIndex = 0;
            this.lblThresholdPrompt.Text = "Ngưỡng lọc (≤):";
            // 
            // dgvStockAlert
            // 
            this.dgvStockAlert.AllowUserToAddRows = false;
            this.dgvStockAlert.AllowUserToDeleteRows = false;
            this.dgvStockAlert.AllowUserToResizeRows = false;
            this.dgvStockAlert.BackgroundColor = System.Drawing.Color.White;
            this.dgvStockAlert.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvStockAlert.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvStockAlert.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvStockAlert.ColumnHeadersHeight = 32;
            this.dgvStockAlert.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvStockAlert.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colStockAlertSTT,
            this.colStockAlertMaSP,
            this.colStockAlertTenSP,
            this.colStockAlertLoaiSP,
            this.colStockAlertDVT,
            this.colStockAlertTongTon,
            this.colStockAlertMucDo,
            this.colStockAlertChiTietKho});
            this.dgvStockAlert.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvStockAlert.EnableHeadersVisualStyles = false;
            this.dgvStockAlert.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.dgvStockAlert.Location = new System.Drawing.Point(16, 48);
            this.dgvStockAlert.MultiSelect = false;
            this.dgvStockAlert.Name = "dgvStockAlert";
            this.dgvStockAlert.ReadOnly = true;
            this.dgvStockAlert.RowHeadersVisible = false;
            this.dgvStockAlert.RowTemplate.Height = 28;
            this.dgvStockAlert.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvStockAlert.Size = new System.Drawing.Size(960, 188);
            this.dgvStockAlert.TabIndex = 1;
            this.dgvStockAlert.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            // 
            // colStockAlertSTT
            // 
            this.colStockAlertSTT.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colStockAlertSTT.HeaderText = "STT";
            this.colStockAlertSTT.Name = "colStockAlertSTT";
            this.colStockAlertSTT.ReadOnly = true;
            this.colStockAlertSTT.Width = 45;
            // 
            // colStockAlertMaSP
            // 
            this.colStockAlertMaSP.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colStockAlertMaSP.DataPropertyName = "MaSP";
            this.colStockAlertMaSP.HeaderText = "Mã SP";
            this.colStockAlertMaSP.Name = "colStockAlertMaSP";
            this.colStockAlertMaSP.ReadOnly = true;
            this.colStockAlertMaSP.Width = 80;
            // 
            // colStockAlertTenSP
            // 
            this.colStockAlertTenSP.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colStockAlertTenSP.DataPropertyName = "TenSP";
            this.colStockAlertTenSP.FillWeight = 28F;
            this.colStockAlertTenSP.MinimumWidth = 220;
            this.colStockAlertTenSP.HeaderText = "Tên Sản Phẩm";
            this.colStockAlertTenSP.Name = "colStockAlertTenSP";
            this.colStockAlertTenSP.ReadOnly = true;
            // 
            // colStockAlertLoaiSP
            // 
            this.colStockAlertLoaiSP.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colStockAlertLoaiSP.DataPropertyName = "TenLoaiSP";
            this.colStockAlertLoaiSP.FillWeight = 18F;
            this.colStockAlertLoaiSP.MinimumWidth = 140;
            this.colStockAlertLoaiSP.HeaderText = "Loại SP";
            this.colStockAlertLoaiSP.Name = "colStockAlertLoaiSP";
            this.colStockAlertLoaiSP.ReadOnly = true;
            // 
            // colStockAlertDVT
            // 
            this.colStockAlertDVT.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colStockAlertDVT.DataPropertyName = "DonViTinh";
            this.colStockAlertDVT.HeaderText = "ĐVT";
            this.colStockAlertDVT.Name = "colStockAlertDVT";
            this.colStockAlertDVT.ReadOnly = true;
            this.colStockAlertDVT.Width = 55;
            // 
            // colStockAlertTongTon
            // 
            this.colStockAlertTongTon.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colStockAlertTongTon.DataPropertyName = "TongTon";
            this.colStockAlertTongTon.HeaderText = "Tổng Tồn";
            this.colStockAlertTongTon.Name = "colStockAlertTongTon";
            this.colStockAlertTongTon.ReadOnly = true;
            this.colStockAlertTongTon.Width = 80;
            // 
            // colStockAlertMucDo
            // 
            this.colStockAlertMucDo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colStockAlertMucDo.DataPropertyName = "MucDoCanhBao";
            this.colStockAlertMucDo.HeaderText = "Mức Độ";
            this.colStockAlertMucDo.Name = "colStockAlertMucDo";
            this.colStockAlertMucDo.ReadOnly = true;
            this.colStockAlertMucDo.Width = 160;
            // 
            // colStockAlertChiTietKho
            // 
            this.colStockAlertChiTietKho.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colStockAlertChiTietKho.DataPropertyName = "ChiTietKho";
            this.colStockAlertChiTietKho.FillWeight = 54F;
            this.colStockAlertChiTietKho.MinimumWidth = 350;
            this.colStockAlertChiTietKho.HeaderText = "Phân Bổ Tồn Các Kho";
            this.colStockAlertChiTietKho.Name = "colStockAlertChiTietKho";
            this.colStockAlertChiTietKho.ReadOnly = true;
            // 
            // pnlStockSafeBanner
            // 
            this.pnlStockSafeBanner.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(253)))), ((int)(((byte)(244)))));
            this.pnlStockSafeBanner.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlStockSafeBanner.Controls.Add(this.lblStockSafeSub);
            this.pnlStockSafeBanner.Controls.Add(this.lblStockSafeMessage);
            this.pnlStockSafeBanner.Controls.Add(this.lblStockSafeIcon);
            this.pnlStockSafeBanner.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlStockSafeBanner.Location = new System.Drawing.Point(16, 48);
            this.pnlStockSafeBanner.Name = "pnlStockSafeBanner";
            this.pnlStockSafeBanner.Padding = new System.Windows.Forms.Padding(20, 25, 20, 20);
            this.pnlStockSafeBanner.Size = new System.Drawing.Size(960, 188);
            this.pnlStockSafeBanner.TabIndex = 2;
            this.pnlStockSafeBanner.Visible = false;
            // 
            // lblStockSafeSub
            // 
            this.lblStockSafeSub.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblStockSafeSub.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblStockSafeSub.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblStockSafeSub.Location = new System.Drawing.Point(20, 105);
            this.lblStockSafeSub.Name = "lblStockSafeSub";
            this.lblStockSafeSub.Size = new System.Drawing.Size(918, 61);
            this.lblStockSafeSub.TabIndex = 2;
            this.lblStockSafeSub.Text = "Toàn bộ danh mục sản phẩm hiện tại đều đáp ứng tốt nhu cầu bán hàng và vận hành kho.";
            this.lblStockSafeSub.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblStockSafeMessage
            // 
            this.lblStockSafeMessage.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblStockSafeMessage.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblStockSafeMessage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(101)))), ((int)(((byte)(52)))));
            this.lblStockSafeMessage.Location = new System.Drawing.Point(20, 75);
            this.lblStockSafeMessage.Name = "lblStockSafeMessage";
            this.lblStockSafeMessage.Size = new System.Drawing.Size(918, 30);
            this.lblStockSafeMessage.TabIndex = 1;
            this.lblStockSafeMessage.Text = "Tất cả các mặt hàng bảo đảm mức tồn an toàn (> 10 SP)";
            this.lblStockSafeMessage.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblStockSafeIcon
            // 
            this.lblStockSafeIcon.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblStockSafeIcon.Font = new System.Drawing.Font("Segoe UI", 24F);
            this.lblStockSafeIcon.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(197)))), ((int)(((byte)(94)))));
            this.lblStockSafeIcon.Location = new System.Drawing.Point(20, 25);
            this.lblStockSafeIcon.Name = "lblStockSafeIcon";
            this.lblStockSafeIcon.Size = new System.Drawing.Size(918, 50);
            this.lblStockSafeIcon.TabIndex = 0;
            this.lblStockSafeIcon.Text = "✅";
            this.lblStockSafeIcon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlQuickActionsContainer
            // 
            this.pnlQuickActionsContainer.BackColor = System.Drawing.Color.White;
            this.pnlQuickActionsContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlQuickActionsContainer.Controls.Add(this.flpQuickActions);
            this.pnlQuickActionsContainer.Controls.Add(this.lblQuickActionsTitle);
            this.pnlQuickActionsContainer.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlQuickActionsContainer.Location = new System.Drawing.Point(20, 218);
            this.pnlQuickActionsContainer.Margin = new System.Windows.Forms.Padding(0, 16, 0, 16);
            this.pnlQuickActionsContainer.Name = "pnlQuickActionsContainer";
            this.pnlQuickActionsContainer.Padding = new System.Windows.Forms.Padding(16, 12, 16, 12);
            this.pnlQuickActionsContainer.Size = new System.Drawing.Size(994, 204);
            this.pnlQuickActionsContainer.TabIndex = 2;
            // 
            // flpQuickActions
            // 
            this.flpQuickActions.AutoScroll = true;
            this.flpQuickActions.Controls.Add(this.btnActionDonHang);
            this.flpQuickActions.Controls.Add(this.btnActionHoaDon);
            this.flpQuickActions.Controls.Add(this.btnActionXuatKho);
            this.flpQuickActions.Controls.Add(this.btnActionPhieuThu);
            this.flpQuickActions.Controls.Add(this.btnActionBaoCao);
            this.flpQuickActions.Controls.Add(this.btnActionTonKho);
            this.flpQuickActions.Controls.Add(this.btnActionChungTu);
            this.flpQuickActions.Controls.Add(this.btnActionSoQuy);
            this.flpQuickActions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpQuickActions.Location = new System.Drawing.Point(16, 36);
            this.flpQuickActions.Name = "flpQuickActions";
            this.flpQuickActions.Size = new System.Drawing.Size(960, 154);
            this.flpQuickActions.TabIndex = 1;
            // 
            // btnActionDonHang
            // 
            this.btnActionDonHang.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(246)))), ((int)(((byte)(255)))));
            this.btnActionDonHang.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnActionDonHang.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(147)))), ((int)(((byte)(197)))), ((int)(((byte)(253)))));
            this.btnActionDonHang.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnActionDonHang.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnActionDonHang.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(78)))), ((int)(((byte)(216)))));
            this.btnActionDonHang.Location = new System.Drawing.Point(4, 4);
            this.btnActionDonHang.Margin = new System.Windows.Forms.Padding(4);
            this.btnActionDonHang.Name = "btnActionDonHang";
            this.btnActionDonHang.Size = new System.Drawing.Size(220, 68);
            this.btnActionDonHang.TabIndex = 0;
            this.btnActionDonHang.Text = "📝  Lập Đơn Đặt Hàng\r\nTiếp nhận đơn mới";
            this.btnActionDonHang.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnActionDonHang.UseVisualStyleBackColor = false;
            this.btnActionDonHang.Click += new System.EventHandler(this.menuDonDatHang_Click);
            // 
            // btnActionHoaDon
            // 
            this.btnActionHoaDon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(242)))), ((int)(((byte)(255)))));
            this.btnActionHoaDon.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnActionHoaDon.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(180)))), ((int)(((byte)(252)))));
            this.btnActionHoaDon.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnActionHoaDon.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnActionHoaDon.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(56)))), ((int)(((byte)(202)))));
            this.btnActionHoaDon.Location = new System.Drawing.Point(232, 4);
            this.btnActionHoaDon.Margin = new System.Windows.Forms.Padding(4);
            this.btnActionHoaDon.Name = "btnActionHoaDon";
            this.btnActionHoaDon.Size = new System.Drawing.Size(220, 68);
            this.btnActionHoaDon.TabIndex = 1;
            this.btnActionHoaDon.Text = "📄  Lập Hóa Đơn Bán\r\nLập từ đơn hàng";
            this.btnActionHoaDon.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnActionHoaDon.UseVisualStyleBackColor = false;
            this.btnActionHoaDon.Click += new System.EventHandler(this.menuHoaDonBan_Click);
            // 
            // btnActionXuatKho
            // 
            this.btnActionXuatKho.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(254)))), ((int)(((byte)(255)))));
            this.btnActionXuatKho.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnActionXuatKho.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(232)))), ((int)(((byte)(249)))));
            this.btnActionXuatKho.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnActionXuatKho.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnActionXuatKho.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(116)))), ((int)(((byte)(144)))));
            this.btnActionXuatKho.Location = new System.Drawing.Point(460, 4);
            this.btnActionXuatKho.Margin = new System.Windows.Forms.Padding(4);
            this.btnActionXuatKho.Name = "btnActionXuatKho";
            this.btnActionXuatKho.Size = new System.Drawing.Size(220, 68);
            this.btnActionXuatKho.TabIndex = 2;
            this.btnActionXuatKho.Text = "📤  Lập Phiếu Xuất\r\nXuất kho giao hàng";
            this.btnActionXuatKho.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnActionXuatKho.UseVisualStyleBackColor = false;
            this.btnActionXuatKho.Click += new System.EventHandler(this.menuPhieuXuatKho_Click);
            // 
            // btnActionPhieuThu
            // 
            this.btnActionPhieuThu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(253)))), ((int)(((byte)(245)))));
            this.btnActionPhieuThu.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnActionPhieuThu.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(110)))), ((int)(((byte)(231)))), ((int)(((byte)(183)))));
            this.btnActionPhieuThu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnActionPhieuThu.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnActionPhieuThu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(120)))), ((int)(((byte)(87)))));
            this.btnActionPhieuThu.Location = new System.Drawing.Point(688, 4);
            this.btnActionPhieuThu.Margin = new System.Windows.Forms.Padding(4);
            this.btnActionPhieuThu.Name = "btnActionPhieuThu";
            this.btnActionPhieuThu.Size = new System.Drawing.Size(220, 68);
            this.btnActionPhieuThu.TabIndex = 3;
            this.btnActionPhieuThu.Text = "💰  Lập Phiếu Thu\r\nThu tiền theo hóa đơn";
            this.btnActionPhieuThu.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnActionPhieuThu.UseVisualStyleBackColor = false;
            this.btnActionPhieuThu.Click += new System.EventHandler(this.menuPhieuThu_Click);
            // 
            // btnActionBaoCao
            // 
            this.btnActionBaoCao.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.btnActionBaoCao.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnActionBaoCao.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(181)))), ((int)(((byte)(253)))));
            this.btnActionBaoCao.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnActionBaoCao.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnActionBaoCao.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(40)))), ((int)(((byte)(217)))));
            this.btnActionBaoCao.Location = new System.Drawing.Point(4, 80);
            this.btnActionBaoCao.Margin = new System.Windows.Forms.Padding(4);
            this.btnActionBaoCao.Name = "btnActionBaoCao";
            this.btnActionBaoCao.Size = new System.Drawing.Size(220, 68);
            this.btnActionBaoCao.TabIndex = 4;
            this.btnActionBaoCao.Text = "📈  Báo Cáo Doanh Thu\r\nXem doanh số bán";
            this.btnActionBaoCao.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnActionBaoCao.UseVisualStyleBackColor = false;
            this.btnActionBaoCao.Click += new System.EventHandler(this.menuBaoCaoDoanhThu_Click);
            // 
            // btnActionTonKho
            // 
            this.btnActionTonKho.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(243)))), ((int)(((byte)(199)))));
            this.btnActionTonKho.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnActionTonKho.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(211)))), ((int)(((byte)(77)))));
            this.btnActionTonKho.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnActionTonKho.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnActionTonKho.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(83)))), ((int)(((byte)(9)))));
            this.btnActionTonKho.Location = new System.Drawing.Point(232, 80);
            this.btnActionTonKho.Margin = new System.Windows.Forms.Padding(4);
            this.btnActionTonKho.Name = "btnActionTonKho";
            this.btnActionTonKho.Size = new System.Drawing.Size(220, 68);
            this.btnActionTonKho.TabIndex = 5;
            this.btnActionTonKho.Text = "📦  Tra Cứu Tồn Kho\r\nKiểm tra số lượng tồn";
            this.btnActionTonKho.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnActionTonKho.UseVisualStyleBackColor = false;
            this.btnActionTonKho.Click += new System.EventHandler(this.menuTonKho_Click);
            // 
            // btnActionChungTu
            // 
            this.btnActionChungTu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(253)))), ((int)(((byte)(250)))));
            this.btnActionChungTu.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnActionChungTu.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(234)))), ((int)(((byte)(212)))));
            this.btnActionChungTu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnActionChungTu.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnActionChungTu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(118)))), ((int)(((byte)(110)))));
            this.btnActionChungTu.Location = new System.Drawing.Point(460, 80);
            this.btnActionChungTu.Margin = new System.Windows.Forms.Padding(4);
            this.btnActionChungTu.Name = "btnActionChungTu";
            this.btnActionChungTu.Size = new System.Drawing.Size(220, 68);
            this.btnActionChungTu.TabIndex = 6;
            this.btnActionChungTu.Text = "📑  Chứng Từ Kế Toán\r\nHạch toán Nợ/Có";
            this.btnActionChungTu.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnActionChungTu.UseVisualStyleBackColor = false;
            this.btnActionChungTu.Click += new System.EventHandler(this.menuChungTu_Click);
            // 
            // btnActionSoQuy
            // 
            this.btnActionSoQuy.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(241)))), ((int)(((byte)(242)))));
            this.btnActionSoQuy.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnActionSoQuy.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(205)))), ((int)(((byte)(211)))));
            this.btnActionSoQuy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnActionSoQuy.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnActionSoQuy.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(18)))), ((int)(((byte)(60)))));
            this.btnActionSoQuy.Location = new System.Drawing.Point(688, 80);
            this.btnActionSoQuy.Margin = new System.Windows.Forms.Padding(4);
            this.btnActionSoQuy.Name = "btnActionSoQuy";
            this.btnActionSoQuy.Size = new System.Drawing.Size(220, 68);
            this.btnActionSoQuy.TabIndex = 7;
            this.btnActionSoQuy.Text = "💵  Sổ Quỹ Thu - Chi\r\nKiểm soát tiền mặt";
            this.btnActionSoQuy.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnActionSoQuy.UseVisualStyleBackColor = false;
            this.btnActionSoQuy.Click += new System.EventHandler(this.menuBaoCaoThuChi_Click);
            // 
            // lblQuickActionsTitle
            // 
            this.lblQuickActionsTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblQuickActionsTitle.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblQuickActionsTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(40)))), ((int)(((byte)(217)))));
            this.lblQuickActionsTitle.Location = new System.Drawing.Point(16, 12);
            this.lblQuickActionsTitle.Name = "lblQuickActionsTitle";
            this.lblQuickActionsTitle.Size = new System.Drawing.Size(960, 24);
            this.lblQuickActionsTitle.TabIndex = 0;
            this.lblQuickActionsTitle.Text = "⚡ THAO TÁC NHANH (QUICK ACTIONS)";
            // 
            // tlpKPICards
            // 
            this.tlpKPICards.ColumnCount = 4;
            this.tlpKPICards.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpKPICards.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpKPICards.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpKPICards.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpKPICards.Controls.Add(this.pnlCardStock, 3, 0);
            this.tlpKPICards.Controls.Add(this.pnlCardPayment, 2, 0);
            this.tlpKPICards.Controls.Add(this.pnlCardReceipt, 1, 0);
            this.tlpKPICards.Controls.Add(this.pnlCardRevenue, 0, 0);
            this.tlpKPICards.Dock = System.Windows.Forms.DockStyle.Top;
            this.tlpKPICards.Location = new System.Drawing.Point(20, 102);
            this.tlpKPICards.Name = "tlpKPICards";
            this.tlpKPICards.RowCount = 1;
            this.tlpKPICards.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpKPICards.Size = new System.Drawing.Size(994, 116);
            this.tlpKPICards.TabIndex = 1;
            // 
            // pnlCardStock
            // 
            this.pnlCardStock.BackColor = System.Drawing.Color.White;
            this.pnlCardStock.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlCardStock.Controls.Add(this.pnlCardStockBar);
            this.pnlCardStock.Controls.Add(this.lblCardStockSub);
            this.pnlCardStock.Controls.Add(this.lblCardStockValue);
            this.pnlCardStock.Controls.Add(this.lblCardStockTitle);
            this.pnlCardStock.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCardStock.Location = new System.Drawing.Point(748, 3);
            this.pnlCardStock.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.pnlCardStock.Name = "pnlCardStock";
            this.pnlCardStock.Padding = new System.Windows.Forms.Padding(12, 10, 12, 8);
            this.pnlCardStock.Size = new System.Drawing.Size(242, 110);
            this.pnlCardStock.TabIndex = 3;
            // 
            // pnlCardStockBar
            // 
            this.pnlCardStockBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(72)))), ((int)(((byte)(153)))));
            this.pnlCardStockBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCardStockBar.Location = new System.Drawing.Point(12, 10);
            this.pnlCardStockBar.Name = "pnlCardStockBar";
            this.pnlCardStockBar.Size = new System.Drawing.Size(216, 4);
            this.pnlCardStockBar.TabIndex = 3;
            // 
            // lblCardStockSub
            // 
            this.lblCardStockSub.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblCardStockSub.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblCardStockSub.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblCardStockSub.Location = new System.Drawing.Point(12, 82);
            this.lblCardStockSub.Name = "lblCardStockSub";
            this.lblCardStockSub.Size = new System.Drawing.Size(216, 18);
            this.lblCardStockSub.TabIndex = 2;
            this.lblCardStockSub.Text = "Tổng lượng hàng hóa trong kho";
            // 
            // lblCardStockValue
            // 
            this.lblCardStockValue.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblCardStockValue.Font = new System.Drawing.Font("Segoe UI", 13.5F, System.Drawing.FontStyle.Bold);
            this.lblCardStockValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(39)))), ((int)(((byte)(119)))));
            this.lblCardStockValue.Location = new System.Drawing.Point(12, 38);
            this.lblCardStockValue.Name = "lblCardStockValue";
            this.lblCardStockValue.Size = new System.Drawing.Size(216, 38);
            this.lblCardStockValue.TabIndex = 1;
            this.lblCardStockValue.Text = "-- SP";
            this.lblCardStockValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCardStockTitle
            // 
            this.lblCardStockTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblCardStockTitle.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.lblCardStockTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(24)))), ((int)(((byte)(93)))));
            this.lblCardStockTitle.Location = new System.Drawing.Point(12, 10);
            this.lblCardStockTitle.Name = "lblCardStockTitle";
            this.lblCardStockTitle.Padding = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.lblCardStockTitle.Size = new System.Drawing.Size(216, 28);
            this.lblCardStockTitle.TabIndex = 0;
            this.lblCardStockTitle.Text = "📦 TỒN KHO HỆ THỐNG";
            // 
            // pnlCardPayment
            // 
            this.pnlCardPayment.BackColor = System.Drawing.Color.White;
            this.pnlCardPayment.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlCardPayment.Controls.Add(this.pnlCardPaymentBar);
            this.pnlCardPayment.Controls.Add(this.lblCardPaymentSub);
            this.pnlCardPayment.Controls.Add(this.lblCardPaymentValue);
            this.pnlCardPayment.Controls.Add(this.lblCardPaymentTitle);
            this.pnlCardPayment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCardPayment.Location = new System.Drawing.Point(500, 3);
            this.pnlCardPayment.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.pnlCardPayment.Name = "pnlCardPayment";
            this.pnlCardPayment.Padding = new System.Windows.Forms.Padding(12, 10, 12, 8);
            this.pnlCardPayment.Size = new System.Drawing.Size(240, 110);
            this.pnlCardPayment.TabIndex = 2;
            // 
            // pnlCardPaymentBar
            // 
            this.pnlCardPaymentBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(115)))), ((int)(((byte)(22)))));
            this.pnlCardPaymentBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCardPaymentBar.Location = new System.Drawing.Point(12, 10);
            this.pnlCardPaymentBar.Name = "pnlCardPaymentBar";
            this.pnlCardPaymentBar.Size = new System.Drawing.Size(214, 4);
            this.pnlCardPaymentBar.TabIndex = 3;
            // 
            // lblCardPaymentSub
            // 
            this.lblCardPaymentSub.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblCardPaymentSub.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblCardPaymentSub.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblCardPaymentSub.Location = new System.Drawing.Point(12, 82);
            this.lblCardPaymentSub.Name = "lblCardPaymentSub";
            this.lblCardPaymentSub.Size = new System.Drawing.Size(214, 18);
            this.lblCardPaymentSub.TabIndex = 2;
            this.lblCardPaymentSub.Text = "Chi phí hoạt động đã xuất quỹ";
            // 
            // lblCardPaymentValue
            // 
            this.lblCardPaymentValue.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblCardPaymentValue.Font = new System.Drawing.Font("Segoe UI", 13.5F, System.Drawing.FontStyle.Bold);
            this.lblCardPaymentValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(65)))), ((int)(((byte)(12)))));
            this.lblCardPaymentValue.Location = new System.Drawing.Point(12, 38);
            this.lblCardPaymentValue.Name = "lblCardPaymentValue";
            this.lblCardPaymentValue.Size = new System.Drawing.Size(214, 38);
            this.lblCardPaymentValue.TabIndex = 1;
            this.lblCardPaymentValue.Text = "-- VNĐ";
            this.lblCardPaymentValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCardPaymentTitle
            // 
            this.lblCardPaymentTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblCardPaymentTitle.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.lblCardPaymentTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(119)))), ((int)(((byte)(6)))));
            this.lblCardPaymentTitle.Location = new System.Drawing.Point(12, 10);
            this.lblCardPaymentTitle.Name = "lblCardPaymentTitle";
            this.lblCardPaymentTitle.Padding = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.lblCardPaymentTitle.Size = new System.Drawing.Size(214, 28);
            this.lblCardPaymentTitle.TabIndex = 0;
            this.lblCardPaymentTitle.Text = "📤 TỔNG CHI QUỸ TIỀN";
            // 
            // pnlCardReceipt
            // 
            this.pnlCardReceipt.BackColor = System.Drawing.Color.White;
            this.pnlCardReceipt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlCardReceipt.Controls.Add(this.pnlCardReceiptBar);
            this.pnlCardReceipt.Controls.Add(this.lblCardReceiptSub);
            this.pnlCardReceipt.Controls.Add(this.lblCardReceiptValue);
            this.pnlCardReceipt.Controls.Add(this.lblCardReceiptTitle);
            this.pnlCardReceipt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCardReceipt.Location = new System.Drawing.Point(252, 3);
            this.pnlCardReceipt.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.pnlCardReceipt.Name = "pnlCardReceipt";
            this.pnlCardReceipt.Padding = new System.Windows.Forms.Padding(12, 10, 12, 8);
            this.pnlCardReceipt.Size = new System.Drawing.Size(240, 110);
            this.pnlCardReceipt.TabIndex = 1;
            // 
            // pnlCardReceiptBar
            // 
            this.pnlCardReceiptBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(185)))), ((int)(((byte)(129)))));
            this.pnlCardReceiptBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCardReceiptBar.Location = new System.Drawing.Point(12, 10);
            this.pnlCardReceiptBar.Name = "pnlCardReceiptBar";
            this.pnlCardReceiptBar.Size = new System.Drawing.Size(214, 4);
            this.pnlCardReceiptBar.TabIndex = 3;
            // 
            // lblCardReceiptSub
            // 
            this.lblCardReceiptSub.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblCardReceiptSub.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblCardReceiptSub.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblCardReceiptSub.Location = new System.Drawing.Point(12, 82);
            this.lblCardReceiptSub.Name = "lblCardReceiptSub";
            this.lblCardReceiptSub.Size = new System.Drawing.Size(214, 18);
            this.lblCardReceiptSub.TabIndex = 2;
            this.lblCardReceiptSub.Text = "Tổng tiền thực thu theo hóa đơn";
            // 
            // lblCardReceiptValue
            // 
            this.lblCardReceiptValue.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblCardReceiptValue.Font = new System.Drawing.Font("Segoe UI", 13.5F, System.Drawing.FontStyle.Bold);
            this.lblCardReceiptValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(120)))), ((int)(((byte)(87)))));
            this.lblCardReceiptValue.Location = new System.Drawing.Point(12, 38);
            this.lblCardReceiptValue.Name = "lblCardReceiptValue";
            this.lblCardReceiptValue.Size = new System.Drawing.Size(214, 38);
            this.lblCardReceiptValue.TabIndex = 1;
            this.lblCardReceiptValue.Text = "-- VNĐ";
            this.lblCardReceiptValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCardReceiptTitle
            // 
            this.lblCardReceiptTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblCardReceiptTitle.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.lblCardReceiptTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(150)))), ((int)(((byte)(105)))));
            this.lblCardReceiptTitle.Location = new System.Drawing.Point(12, 10);
            this.lblCardReceiptTitle.Name = "lblCardReceiptTitle";
            this.lblCardReceiptTitle.Padding = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.lblCardReceiptTitle.Size = new System.Drawing.Size(214, 28);
            this.lblCardReceiptTitle.TabIndex = 0;
            this.lblCardReceiptTitle.Text = "📥 TỔNG THU QUỸ TIỀN";
            // 
            // pnlCardRevenue
            // 
            this.pnlCardRevenue.BackColor = System.Drawing.Color.White;
            this.pnlCardRevenue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlCardRevenue.Controls.Add(this.pnlCardRevenueBar);
            this.pnlCardRevenue.Controls.Add(this.lblCardRevenueSub);
            this.pnlCardRevenue.Controls.Add(this.lblCardRevenueValue);
            this.pnlCardRevenue.Controls.Add(this.lblCardRevenueTitle);
            this.pnlCardRevenue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCardRevenue.Location = new System.Drawing.Point(4, 3);
            this.pnlCardRevenue.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.pnlCardRevenue.Name = "pnlCardRevenue";
            this.pnlCardRevenue.Padding = new System.Windows.Forms.Padding(12, 10, 12, 8);
            this.pnlCardRevenue.Size = new System.Drawing.Size(240, 110);
            this.pnlCardRevenue.TabIndex = 0;
            // 
            // pnlCardRevenueBar
            // 
            this.pnlCardRevenueBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(102)))), ((int)(((byte)(241)))));
            this.pnlCardRevenueBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCardRevenueBar.Location = new System.Drawing.Point(12, 10);
            this.pnlCardRevenueBar.Name = "pnlCardRevenueBar";
            this.pnlCardRevenueBar.Size = new System.Drawing.Size(214, 4);
            this.pnlCardRevenueBar.TabIndex = 3;
            // 
            // lblCardRevenueSub
            // 
            this.lblCardRevenueSub.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblCardRevenueSub.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblCardRevenueSub.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblCardRevenueSub.Location = new System.Drawing.Point(12, 82);
            this.lblCardRevenueSub.Name = "lblCardRevenueSub";
            this.lblCardRevenueSub.Size = new System.Drawing.Size(214, 18);
            this.lblCardRevenueSub.TabIndex = 2;
            this.lblCardRevenueSub.Text = "Doanh số bán hàng thực tế";
            // 
            // lblCardRevenueValue
            // 
            this.lblCardRevenueValue.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblCardRevenueValue.Font = new System.Drawing.Font("Segoe UI", 13.5F, System.Drawing.FontStyle.Bold);
            this.lblCardRevenueValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(56)))), ((int)(((byte)(202)))));
            this.lblCardRevenueValue.Location = new System.Drawing.Point(12, 38);
            this.lblCardRevenueValue.Name = "lblCardRevenueValue";
            this.lblCardRevenueValue.Size = new System.Drawing.Size(214, 38);
            this.lblCardRevenueValue.TabIndex = 1;
            this.lblCardRevenueValue.Text = "-- VNĐ";
            this.lblCardRevenueValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCardRevenueTitle
            // 
            this.lblCardRevenueTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblCardRevenueTitle.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.lblCardRevenueTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(70)))), ((int)(((byte)(229)))));
            this.lblCardRevenueTitle.Location = new System.Drawing.Point(12, 10);
            this.lblCardRevenueTitle.Name = "lblCardRevenueTitle";
            this.lblCardRevenueTitle.Padding = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.lblCardRevenueTitle.Size = new System.Drawing.Size(214, 28);
            this.lblCardRevenueTitle.TabIndex = 0;
            this.lblCardRevenueTitle.Text = "💰 DOANH THU BÁN HÀNG";
            // 
            // pnlWelcomeBanner
            // 
            this.pnlWelcomeBanner.BackColor = System.Drawing.Color.White;
            this.pnlWelcomeBanner.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlWelcomeBanner.Controls.Add(this.btnRefreshKPI);
            this.pnlWelcomeBanner.Controls.Add(this.lblWelcomeRoleDesc);
            this.pnlWelcomeBanner.Controls.Add(this.lblWelcomeGreeting);
            this.pnlWelcomeBanner.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlWelcomeBanner.Location = new System.Drawing.Point(20, 16);
            this.pnlWelcomeBanner.Margin = new System.Windows.Forms.Padding(0, 0, 0, 16);
            this.pnlWelcomeBanner.Name = "pnlWelcomeBanner";
            this.pnlWelcomeBanner.Padding = new System.Windows.Forms.Padding(16, 12, 16, 12);
            this.pnlWelcomeBanner.Size = new System.Drawing.Size(994, 86);
            this.pnlWelcomeBanner.TabIndex = 0;
            // 
            // btnRefreshKPI
            // 
            this.btnRefreshKPI.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefreshKPI.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.btnRefreshKPI.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefreshKPI.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(181)))), ((int)(((byte)(253)))));
            this.btnRefreshKPI.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefreshKPI.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnRefreshKPI.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(40)))), ((int)(((byte)(217)))));
            this.btnRefreshKPI.Location = new System.Drawing.Point(825, 22);
            this.btnRefreshKPI.Name = "btnRefreshKPI";
            this.btnRefreshKPI.Size = new System.Drawing.Size(150, 38);
            this.btnRefreshKPI.TabIndex = 2;
            this.btnRefreshKPI.Text = "🔄  Làm mới số liệu";
            this.btnRefreshKPI.UseVisualStyleBackColor = false;
            this.btnRefreshKPI.Click += new System.EventHandler(this.btnRefreshKPI_Click);
            // 
            // lblWelcomeRoleDesc
            // 
            this.lblWelcomeRoleDesc.AutoSize = true;
            this.lblWelcomeRoleDesc.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblWelcomeRoleDesc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblWelcomeRoleDesc.Location = new System.Drawing.Point(16, 46);
            this.lblWelcomeRoleDesc.Name = "lblWelcomeRoleDesc";
            this.lblWelcomeRoleDesc.Size = new System.Drawing.Size(438, 20);
            this.lblWelcomeRoleDesc.TabIndex = 1;
            this.lblWelcomeRoleDesc.Text = "Hệ thống đã tự động kích hoạt quyền hạn theo vai trò của bạn.";
            // 
            // lblWelcomeGreeting
            // 
            this.lblWelcomeGreeting.AutoSize = true;
            this.lblWelcomeGreeting.Font = new System.Drawing.Font("Segoe UI", 12.5F, System.Drawing.FontStyle.Bold);
            this.lblWelcomeGreeting.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(40)))), ((int)(((byte)(217)))));
            this.lblWelcomeGreeting.Location = new System.Drawing.Point(15, 14);
            this.lblWelcomeGreeting.Name = "lblWelcomeGreeting";
            this.lblWelcomeGreeting.Size = new System.Drawing.Size(462, 30);
            this.lblWelcomeGreeting.TabIndex = 0;
            this.lblWelcomeGreeting.Text = "Xin chào, Nguyễn Văn Quản Trị (Quản trị viên)";
            // 
            // pnlChildContainer
            // 
            this.pnlChildContainer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.pnlChildContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlChildContainer.Location = new System.Drawing.Point(0, 0);
            this.pnlChildContainer.Name = "pnlChildContainer";
            this.pnlChildContainer.Size = new System.Drawing.Size(1034, 637);
            this.pnlChildContainer.TabIndex = 1;
            this.pnlChildContainer.Visible = false;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.ClientSize = new System.Drawing.Size(1264, 749);
            this.Controls.Add(this.pnlWorkspace);
            this.Controls.Add(this.pnlSidebar);
            this.Controls.Add(this.pnlHeader);
            this.Controls.Add(this.statusStripMain);
            this.Controls.Add(this.menuStripMain);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IsMdiContainer = false;
            this.MainMenuStrip = this.menuStripMain;
            this.MinimumSize = new System.Drawing.Size(1024, 680);
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hệ Thống Thông Tin Kế Toán - Kế Toán Bán Hàng (DNQH)";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.menuStripMain.ResumeLayout(false);
            this.menuStripMain.PerformLayout();
            this.statusStripMain.ResumeLayout(false);
            this.statusStripMain.PerformLayout();
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlSidebar.ResumeLayout(false);
            this.pnlWorkspace.ResumeLayout(false);
            this.pnlDashboard.ResumeLayout(false);
            this.pnlRoleNote.ResumeLayout(false);
            this.pnlStockAlertContainer.ResumeLayout(false);
            this.pnlStockAlertHeader.ResumeLayout(false);
            this.flpStockAlertActions.ResumeLayout(false);
            this.flpStockAlertActions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStockAlert)).EndInit();
            this.pnlStockSafeBanner.ResumeLayout(false);
            this.pnlQuickActionsContainer.ResumeLayout(false);
            this.flpQuickActions.ResumeLayout(false);
            this.tlpKPICards.ResumeLayout(false);
            this.pnlCardStock.ResumeLayout(false);
            this.pnlCardPayment.ResumeLayout(false);
            this.pnlCardReceipt.ResumeLayout(false);
            this.pnlCardRevenue.ResumeLayout(false);
            this.pnlWelcomeBanner.ResumeLayout(false);
            this.pnlWelcomeBanner.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.MenuStrip menuStripMain;
        private System.Windows.Forms.ToolStripMenuItem menuHeThong;
        private System.Windows.Forms.ToolStripMenuItem menuDoiMatKhau;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem menuQuanLyTaiKhoan;
        private System.Windows.Forms.ToolStripMenuItem menuQuanLyNhanVien;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem menuDangXuat;
        private System.Windows.Forms.ToolStripMenuItem menuThoat;
        private System.Windows.Forms.ToolStripMenuItem menuDanhMuc;
        private System.Windows.Forms.ToolStripMenuItem menuKhachHang;
        private System.Windows.Forms.ToolStripMenuItem menuNhaCungCap;
        private System.Windows.Forms.ToolStripMenuItem menuLoaiSanPham;
        private System.Windows.Forms.ToolStripMenuItem menuSanPham;
        private System.Windows.Forms.ToolStripMenuItem menuKho;
        private System.Windows.Forms.ToolStripMenuItem menuKeToanBanHang;
        private System.Windows.Forms.ToolStripMenuItem menuDonDatHang;
        private System.Windows.Forms.ToolStripMenuItem menuTraCuuDonDatHang;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem menuHoaDonBan;
        private System.Windows.Forms.ToolStripMenuItem menuTraCuuHoaDon;
        private System.Windows.Forms.ToolStripMenuItem menuQuanLyKho;
        private System.Windows.Forms.ToolStripMenuItem menuTonKho;
        private System.Windows.Forms.ToolStripMenuItem menuPhieuXuatKho;
        private System.Windows.Forms.ToolStripMenuItem menuTraCuuPhieuXuat;
        private System.Windows.Forms.ToolStripMenuItem menuChungTuTien;
        private System.Windows.Forms.ToolStripMenuItem menuPhieuThu;
        private System.Windows.Forms.ToolStripMenuItem menuTraCuuPhieuThu;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem menuPhieuChi;
        private System.Windows.Forms.ToolStripMenuItem menuTraCuuPhieuChi;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem menuChungTu;
        private System.Windows.Forms.ToolStripMenuItem menuTraCuuChungTu;
        private System.Windows.Forms.ToolStripMenuItem menuKeToanChiTiet;
        private System.Windows.Forms.ToolStripMenuItem menuSoChiTietKhachHang;
        private System.Windows.Forms.ToolStripMenuItem menuSoChiTietSanPham;
        private System.Windows.Forms.ToolStripMenuItem menuSoChiTietHoaDon;
        private System.Windows.Forms.ToolStripMenuItem menuBaoCaoTuoiNo;
        private System.Windows.Forms.ToolStripMenuItem menuKeToanTongHop;
        private System.Windows.Forms.ToolStripMenuItem menuBaoCaoDoanhThu;
        private System.Windows.Forms.ToolStripMenuItem menuBaoCaoThuChi;
        private System.Windows.Forms.ToolStripMenuItem menuBaoCaoTonKho;
        private System.Windows.Forms.ToolStripMenuItem menuBaoCaoBieuDo;
        private System.Windows.Forms.StatusStrip statusStripMain;
        private System.Windows.Forms.ToolStripStatusLabel lblStatusUser;
        private System.Windows.Forms.ToolStripStatusLabel lblStatusRole;
        private System.Windows.Forms.ToolStripStatusLabel lblStatusConnection;
        private System.Windows.Forms.ToolStripStatusLabel lblStatusTime;
        private System.Windows.Forms.Timer timerClock;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblLogoIcon;
        private System.Windows.Forms.Label lblAppSubtitle;
        private System.Windows.Forms.Label lblAppTitle;
        private System.Windows.Forms.Button btnBackToDashboard;
        private System.Windows.Forms.Panel pnlHeaderDivider;
        private System.Windows.Forms.Panel pnlHeaderRightDivider;
        private System.Windows.Forms.Label lblBreadcrumb;
        private System.Windows.Forms.Label lblUserProfile;
        private System.Windows.Forms.Button btnHeaderHotkeys;
        private System.Windows.Forms.Button btnHeaderDangXuat;
        private System.Windows.Forms.Button btnHeaderDoiMatKhau;
        private System.Windows.Forms.Panel pnlSidebar;
        private System.Windows.Forms.Label lblSidebarHeader;
        private System.Windows.Forms.Button btnNavDashboard;
        private System.Windows.Forms.Label lblNavGroupSales;
        private System.Windows.Forms.Button btnNavDonHang;
        private System.Windows.Forms.Button btnNavHoaDon;
        private System.Windows.Forms.Label lblNavGroupWarehouse;
        private System.Windows.Forms.Button btnNavXuatKho;
        private System.Windows.Forms.Button btnNavTonKho;
        private System.Windows.Forms.Label lblNavGroupCash;
        private System.Windows.Forms.Button btnNavPhieuThu;
        private System.Windows.Forms.Button btnNavPhieuChi;
        private System.Windows.Forms.Button btnNavChungTu;
        private System.Windows.Forms.Label lblNavGroupReport;
        private System.Windows.Forms.Button btnNavKeToanChiTiet;
        private System.Windows.Forms.Button btnNavBaoCaoTongHop;
        private System.Windows.Forms.Label lblNavGroupAdmin;
        private System.Windows.Forms.Button btnNavKhachHang;
        private System.Windows.Forms.Button btnNavNhaCungCap;
        private System.Windows.Forms.Button btnNavLoaiSanPham;
        private System.Windows.Forms.Button btnNavSanPham;
        private System.Windows.Forms.Button btnNavKho;
        private System.Windows.Forms.Button btnNavNhanVien;
        private System.Windows.Forms.Button btnNavTaiKhoan;
        private System.Windows.Forms.Panel pnlWorkspace;
        private System.Windows.Forms.Panel pnlDashboard;
        private System.Windows.Forms.Panel pnlStockAlertContainer;
        private System.Windows.Forms.Panel pnlStockAlertHeader;
        private System.Windows.Forms.Label lblStockAlertTitle;
        private System.Windows.Forms.FlowLayoutPanel flpStockAlertActions;
        private System.Windows.Forms.Label lblThresholdPrompt;
        private System.Windows.Forms.ComboBox cboSafetyThreshold;
        private System.Windows.Forms.Button btnStockAlertRefresh;
        private System.Windows.Forms.Button btnGoToTonKho;
        private System.Windows.Forms.DataGridView dgvStockAlert;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStockAlertSTT;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStockAlertMaSP;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStockAlertTenSP;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStockAlertLoaiSP;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStockAlertDVT;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStockAlertTongTon;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStockAlertMucDo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStockAlertChiTietKho;
        private System.Windows.Forms.Panel pnlStockSafeBanner;
        private System.Windows.Forms.Label lblStockSafeIcon;
        private System.Windows.Forms.Label lblStockSafeMessage;
        private System.Windows.Forms.Label lblStockSafeSub;
        private System.Windows.Forms.Panel pnlChildContainer;
        private System.Windows.Forms.Panel pnlWelcomeBanner;
        private System.Windows.Forms.Label lblWelcomeGreeting;
        private System.Windows.Forms.Label lblWelcomeRoleDesc;
        private System.Windows.Forms.Button btnRefreshKPI;
        private System.Windows.Forms.TableLayoutPanel tlpKPICards;
        private System.Windows.Forms.Panel pnlCardRevenue;
        private System.Windows.Forms.Panel pnlCardRevenueBar;
        private System.Windows.Forms.Label lblCardRevenueSub;
        private System.Windows.Forms.Label lblCardRevenueValue;
        private System.Windows.Forms.Label lblCardRevenueTitle;
        private System.Windows.Forms.Panel pnlCardReceipt;
        private System.Windows.Forms.Panel pnlCardReceiptBar;
        private System.Windows.Forms.Label lblCardReceiptSub;
        private System.Windows.Forms.Label lblCardReceiptValue;
        private System.Windows.Forms.Label lblCardReceiptTitle;
        private System.Windows.Forms.Panel pnlCardPayment;
        private System.Windows.Forms.Panel pnlCardPaymentBar;
        private System.Windows.Forms.Label lblCardPaymentSub;
        private System.Windows.Forms.Label lblCardPaymentValue;
        private System.Windows.Forms.Label lblCardPaymentTitle;
        private System.Windows.Forms.Panel pnlCardStock;
        private System.Windows.Forms.Panel pnlCardStockBar;
        private System.Windows.Forms.Label lblCardStockSub;
        private System.Windows.Forms.Label lblCardStockValue;
        private System.Windows.Forms.Label lblCardStockTitle;
        private System.Windows.Forms.Panel pnlQuickActionsContainer;
        private System.Windows.Forms.Label lblQuickActionsTitle;
        private System.Windows.Forms.FlowLayoutPanel flpQuickActions;
        private System.Windows.Forms.Button btnActionDonHang;
        private System.Windows.Forms.Button btnActionHoaDon;
        private System.Windows.Forms.Button btnActionXuatKho;
        private System.Windows.Forms.Button btnActionPhieuThu;
        private System.Windows.Forms.Button btnActionBaoCao;
        private System.Windows.Forms.Button btnActionTonKho;
        private System.Windows.Forms.Button btnActionChungTu;
        private System.Windows.Forms.Button btnActionSoQuy;
        private System.Windows.Forms.Panel pnlRoleNote;
        private System.Windows.Forms.Label lblRoleNoteText;
        private System.Windows.Forms.Label lblRoleNoteTitle;
        private System.Windows.Forms.ToolStripMenuItem menuTrangChu;
        private System.Windows.Forms.ToolStripMenuItem menuNhatKyHoatDong;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparatorTraCuuPhimTat;
        private System.Windows.Forms.ToolStripMenuItem menuTraCuuPhimTat;
    }
}
