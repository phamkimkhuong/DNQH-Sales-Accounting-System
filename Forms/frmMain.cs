using System;
using System.Drawing;
using System.Windows.Forms;
using DNQH_KeToanBanHang.Helpers;
using DNQH_KeToanBanHang.Models;
using DNQH_KeToanBanHang.Services;

namespace DNQH_KeToanBanHang.Forms
{
    public partial class frmMain : Form
    {
        private readonly AuthService authService;
        private readonly ReportingService reportingService;
        private bool isLoggingOut = false;
        private Form currentChildForm = null;

        public frmMain()
        {
            InitializeComponent();
            ApplyFoundationDesign();
            authService = new AuthService();
            reportingService = new ReportingService();
        }

        private void ApplyFoundationDesign()
        {
            UiStyler.Apply(this);
            ApplyHeaderButtonsDesign();
            UiStyler.StyleButton(btnRefreshKPI, UiButtonRole.Secondary);

            if (!SystemInformation.HighContrast)
            {
                pnlHeader.BackColor = UiTheme.Sidebar;
                pnlSidebar.BackColor = UiTheme.Sidebar;
                pnlWorkspace.BackColor = UiTheme.Canvas;
                pnlDashboard.BackColor = UiTheme.Canvas;
                pnlChildContainer.BackColor = UiTheme.Canvas;
                menuStripMain.BackColor = UiTheme.Surface;
                statusStripMain.BackColor = UiTheme.Surface;
            }

            Button[] navigationButtons = GetNavigationButtons();
            for (int i = 0; i < navigationButtons.Length; i++)
            {
                UiStyler.StyleNavigationButton(navigationButtons[i], false);
            }

            UiStyler.SetAccessibleText(
                pnlSidebar,
                "Điều hướng chức năng",
                "Danh sách chức năng được hiển thị theo vai trò đang đăng nhập.");
            UiStyler.SetAccessibleText(
                pnlWorkspace,
                "Khu vực làm việc",
                "Hiển thị bảng điều khiển hoặc màn hình nghiệp vụ đang chọn.");

            pnlSidebar.Visible = false;

            ApplyModernMenuAndIcons();
            ApplyKpiCardBadges();
            ApplyQuickActionIcons();
            ApplyHeaderButtonsDesign();
            UpdateHeaderNavigationLayout();

            cboSafetyThreshold.Items.Clear();
            cboSafetyThreshold.Items.AddRange(new object[] { "≤ 5 SP", "≤ 10 SP (Mặc định)", "≤ 20 SP", "≤ 50 SP" });
            cboSafetyThreshold.SelectedIndex = 1;

            UiStyler.StyleButton(btnGoToTonKho, UiButtonRole.Primary);
            UiStyler.StyleButton(btnStockAlertRefresh, UiButtonRole.Secondary);
            btnStockAlertRefresh.Image = UiIconProvider.GetIcon(UiIconType.Refresh, 12, UiTheme.TextSecondary);
            btnStockAlertRefresh.ImageAlign = ContentAlignment.MiddleLeft;
            btnStockAlertRefresh.TextImageRelation = TextImageRelation.ImageBeforeText;

            btnGoToTonKho.Image = UiIconProvider.GetIcon(UiIconType.Warehouse, 12, Color.White);
            btnGoToTonKho.ImageAlign = ContentAlignment.MiddleLeft;
            btnGoToTonKho.TextImageRelation = TextImageRelation.ImageBeforeText;

            dgvStockAlert.RowPostPaint += dgvStockAlert_RowPostPaint;
            dgvStockAlert.CellFormatting += dgvStockAlert_CellFormatting;
            ConfigureStockAlertGrid();

            lblRoleNoteTitle.Text = "MẸO SỬ DỤNG";
            lblRoleNoteText.Text = "Chọn một tác vụ nhanh trên bảng điều khiển hoặc dùng thanh menu ngang ở trên để bắt đầu công việc.";
            SetActiveNavigation(btnNavDashboard);
        }

        private void ApplyModernMenuAndIcons()
        {
            if (!SystemInformation.HighContrast)
            {
                menuStripMain.Renderer = new ModernMenuRenderer();
            }

            // Menu chính
            menuTrangChu.Image = UiIconProvider.GetIcon(UiIconType.Dashboard, 16, UiTheme.Primary);
            menuHeThong.Image = UiIconProvider.GetIcon(UiIconType.System, 16, UiTheme.Primary);
            menuDanhMuc.Image = UiIconProvider.GetIcon(UiIconType.Category, 16, UiTheme.Primary);
            menuKeToanBanHang.Image = UiIconProvider.GetIcon(UiIconType.Order, 16, UiTheme.Primary);
            menuQuanLyKho.Image = UiIconProvider.GetIcon(UiIconType.Warehouse, 16, UiTheme.Primary);
            menuChungTuTien.Image = UiIconProvider.GetIcon(UiIconType.MoneyIn, 16, UiTheme.Primary);
            menuKeToanChiTiet.Image = UiIconProvider.GetIcon(UiIconType.DetailLedger, 16, UiTheme.Primary);
            menuKeToanTongHop.Image = UiIconProvider.GetIcon(UiIconType.GeneralReport, 16, UiTheme.Primary);

            // Menu con Hệ Thống
            menuDoiMatKhau.Image = UiIconProvider.GetIcon(UiIconType.Key, 16, Color.FromArgb(14, 165, 233));
            menuQuanLyTaiKhoan.Image = UiIconProvider.GetIcon(UiIconType.Shield, 16, UiTheme.Primary);
            menuQuanLyNhanVien.Image = UiIconProvider.GetIcon(UiIconType.User, 16, Color.FromArgb(99, 102, 241));
            menuTraCuuPhimTat.Image = UiIconProvider.GetIcon(UiIconType.Keyboard, 16, Color.FromArgb(30, 58, 138));
            menuDangXuat.Image = UiIconProvider.GetIcon(UiIconType.Logout, 16, UiTheme.Danger);
            menuThoat.Image = UiIconProvider.GetIcon(UiIconType.Exit, 16, Color.FromArgb(100, 116, 139));

            // Menu con Danh Mục
            menuKhachHang.Image = UiIconProvider.GetIcon(UiIconType.Users, 16, Color.FromArgb(14, 165, 233));
            menuNhaCungCap.Image = UiIconProvider.GetIcon(UiIconType.Supplier, 16, Color.FromArgb(245, 158, 11));
            menuLoaiSanPham.Image = UiIconProvider.GetIcon(UiIconType.Category, 16, Color.FromArgb(14, 165, 233));
            menuSanPham.Image = UiIconProvider.GetIcon(UiIconType.Product, 16, Color.FromArgb(59, 130, 246));
            menuKho.Image = UiIconProvider.GetIcon(UiIconType.Warehouse, 16, Color.FromArgb(234, 88, 12));

            // Menu con Kế Toán Bán Hàng
            menuDonDatHang.Image = UiIconProvider.GetIcon(UiIconType.Order, 16, Color.FromArgb(37, 99, 235));
            menuTraCuuDonDatHang.Image = UiIconProvider.GetIcon(UiIconType.Search, 16, Color.FromArgb(100, 116, 139));
            menuHoaDonBan.Image = UiIconProvider.GetIcon(UiIconType.Invoice, 16, Color.FromArgb(30, 64, 175));
            menuTraCuuHoaDon.Image = UiIconProvider.GetIcon(UiIconType.Search, 16, Color.FromArgb(100, 116, 139));

            // Menu con Quản Lý Kho
            menuTonKho.Image = UiIconProvider.GetIcon(UiIconType.Stock, 16, Color.FromArgb(2, 132, 199));
            menuPhieuXuatKho.Image = UiIconProvider.GetIcon(UiIconType.Delivery, 16, Color.FromArgb(249, 115, 22));
            menuTraCuuPhieuXuat.Image = UiIconProvider.GetIcon(UiIconType.Search, 16, Color.FromArgb(100, 116, 139));

            // Menu con Chứng Từ Tiền
            menuPhieuThu.Image = UiIconProvider.GetIcon(UiIconType.MoneyIn, 16, Color.FromArgb(16, 185, 129));
            menuTraCuuPhieuThu.Image = UiIconProvider.GetIcon(UiIconType.Search, 16, Color.FromArgb(100, 116, 139));
            menuPhieuChi.Image = UiIconProvider.GetIcon(UiIconType.MoneyOut, 16, Color.FromArgb(239, 68, 68));
            menuTraCuuPhieuChi.Image = UiIconProvider.GetIcon(UiIconType.Search, 16, Color.FromArgb(100, 116, 139));
            menuChungTu.Image = UiIconProvider.GetIcon(UiIconType.Voucher, 16, Color.FromArgb(13, 148, 136));
            menuTraCuuChungTu.Image = UiIconProvider.GetIcon(UiIconType.Search, 16, Color.FromArgb(100, 116, 139));

            // Menu con Kế Toán Chi Tiết
            menuSoChiTietKhachHang.Image = UiIconProvider.GetIcon(UiIconType.Users, 16, Color.FromArgb(14, 165, 233));
            menuSoChiTietSanPham.Image = UiIconProvider.GetIcon(UiIconType.Product, 16, Color.FromArgb(59, 130, 246));
            menuSoChiTietHoaDon.Image = UiIconProvider.GetIcon(UiIconType.Invoice, 16, Color.FromArgb(30, 64, 175));
            menuBaoCaoTuoiNo.Image = UiIconProvider.GetIcon(UiIconType.DetailLedger, 16, Color.FromArgb(220, 38, 38));

            // Menu con Kế Toán Tổng Hợp
            menuBaoCaoDoanhThu.Image = UiIconProvider.GetIcon(UiIconType.Revenue, 16, Color.FromArgb(37, 99, 235));
            menuBaoCaoThuChi.Image = UiIconProvider.GetIcon(UiIconType.MoneyIn, 16, Color.FromArgb(16, 185, 129));
            menuBaoCaoTonKho.Image = UiIconProvider.GetIcon(UiIconType.Stock, 16, Color.FromArgb(234, 88, 12));
            menuBaoCaoBieuDo.Image = UiIconProvider.GetIcon(UiIconType.GeneralReport, 16, Color.FromArgb(30, 64, 175));
        }

        private void ApplyKpiCardBadges()
        {
            AttachCardBadge(pnlCardRevenue, UiIconType.Revenue, Color.FromArgb(239, 246, 255), Color.FromArgb(30, 64, 175));
            AttachCardBadge(pnlCardReceipt, UiIconType.MoneyIn, Color.FromArgb(236, 253, 245), Color.FromArgb(5, 150, 105));
            AttachCardBadge(pnlCardPayment, UiIconType.MoneyOut, Color.FromArgb(254, 242, 242), Color.FromArgb(225, 29, 72));
            AttachCardBadge(pnlCardStock, UiIconType.Stock, Color.FromArgb(254, 243, 199), Color.FromArgb(217, 119, 6));
        }

        private void AttachCardBadge(Panel cardPanel, UiIconType iconType, Color bgColor, Color iconColor)
        {
            PictureBox pic = new PictureBox();
            pic.Size = new Size(38, 38);
            pic.Location = new Point(cardPanel.Width - 52, 24);
            pic.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            pic.SizeMode = PictureBoxSizeMode.CenterImage;
            pic.Image = UiIconProvider.GetIconBadge(iconType, 36, bgColor, iconColor);
            pic.BackColor = Color.Transparent;
            cardPanel.Controls.Add(pic);
            pic.BringToFront();
        }

        private void ApplyQuickActionIcons()
        {
            SetupQuickActionButton(btnActionDonHang, UiIconType.Order, Color.FromArgb(37, 99, 235), "Lập Đơn Đặt Hàng\r\nTiếp nhận đơn mới");
            SetupQuickActionButton(btnActionHoaDon, UiIconType.Invoice, Color.FromArgb(30, 64, 175), "Lập Hóa Đơn Bán\r\nLập từ đơn hàng");
            SetupQuickActionButton(btnActionXuatKho, UiIconType.Delivery, Color.FromArgb(194, 65, 12), "Lập Phiếu Xuất\r\nXuất kho giao hàng");
            SetupQuickActionButton(btnActionPhieuThu, UiIconType.MoneyIn, Color.FromArgb(4, 120, 87), "Lập Phiếu Thu\r\nThu tiền theo hóa đơn");
            SetupQuickActionButton(btnActionBaoCao, UiIconType.GeneralReport, Color.FromArgb(2, 132, 199), "Báo Cáo Doanh Thu\r\nXem doanh số bán");
            SetupQuickActionButton(btnActionTonKho, UiIconType.Stock, Color.FromArgb(180, 83, 9), "Tra Cứu Tồn Kho\r\nKiểm tra số lượng tồn");
            SetupQuickActionButton(btnActionChungTu, UiIconType.Voucher, Color.FromArgb(15, 118, 110), "Chứng Từ Kế Toán\r\nHạch toán Nợ/Có");
            SetupQuickActionButton(btnActionSoQuy, UiIconType.Revenue, Color.FromArgb(190, 18, 60), "Sổ Quỹ Thu - Chi\r\nKiểm soát tiền mặt");
        }

        private void SetupQuickActionButton(Button btn, UiIconType iconType, Color iconColor, string cleanText)
        {
            btn.Image = UiIconProvider.GetIcon(iconType, 22, iconColor);
            btn.ImageAlign = ContentAlignment.MiddleLeft;
            btn.TextImageRelation = TextImageRelation.ImageBeforeText;
            btn.TextAlign = ContentAlignment.MiddleLeft;
            btn.Padding = new Padding(10, 0, 6, 0);
            btn.Text = "  " + cleanText;
        }

        private void StyleHeaderButton(Button btn, Color iconColor, Color hoverColor, string text, UiIconType iconType)
        {
            if (btn == null) return;
            btn.Cursor = Cursors.Hand;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 1;
            btn.FlatAppearance.BorderColor = Color.FromArgb(71, 85, 105);
            btn.BackColor = Color.FromArgb(30, 41, 59);
            btn.ForeColor = Color.FromArgb(241, 245, 249);
            btn.FlatAppearance.MouseOverBackColor = hoverColor;
            btn.FlatAppearance.MouseDownBackColor = Color.FromArgb(15, 23, 42);
            btn.Font = new Font("Segoe UI", 8.5F, FontStyle.Regular);
            btn.Image = UiIconProvider.GetIcon(iconType, 13, iconColor);
            btn.ImageAlign = ContentAlignment.MiddleLeft;
            btn.TextImageRelation = TextImageRelation.ImageBeforeText;
            btn.Padding = new Padding(6, 0, 6, 0);
            btn.Text = " " + text;
        }

        private void ApplyHeaderButtonsDesign()
        {
            // Nút Bảng Điều Khiển (chỉ hiện khi đang xem form con)
            StyleHeaderButton(
                btnBackToDashboard,
                Color.FromArgb(147, 197, 253),
                Color.FromArgb(51, 65, 85),
                "Bảng Điều Khiển",
                UiIconType.Home);

            // Nút Phím tắt (F1)
            StyleHeaderButton(
                btnHeaderHotkeys,
                Color.FromArgb(147, 197, 253),
                Color.FromArgb(51, 65, 85),
                "Phím tắt (F1)",
                UiIconType.Keyboard);

            // Nút Đổi MK
            StyleHeaderButton(
                btnHeaderDoiMatKhau,
                Color.FromArgb(250, 204, 21),
                Color.FromArgb(51, 65, 85),
                "Đổi MK",
                UiIconType.Key);

            // Nút Đăng xuất
            StyleHeaderButton(
                btnHeaderDangXuat,
                Color.FromArgb(244, 63, 94),
                Color.FromArgb(190, 18, 60),
                "Đăng xuất",
                UiIconType.Logout);

            btnRefreshKPI.Image = UiIconProvider.GetIcon(UiIconType.Refresh, 14, UiTheme.Primary);
            btnRefreshKPI.ImageAlign = ContentAlignment.MiddleLeft;
            btnRefreshKPI.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnRefreshKPI.Text = " Làm mới số liệu";
        }

        public void UpdateHeaderNavigationLayout()
        {
            if (btnBackToDashboard != null && lblBreadcrumb != null)
            {
                if (btnBackToDashboard.Visible)
                {
                    lblBreadcrumb.Location = new Point(btnBackToDashboard.Right + 12, 19);
                }
                else
                {
                    lblBreadcrumb.Location = new Point(318, 19);
                }
            }
        }

        private void btnHeaderHotkeys_Click(object sender, EventArgs e)
        {
            UiInteractionHelper.ShowHotkeysHelp(this);
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            this.ApplyResponsiveUI();
            if (!SessionManager.IsLoggedIn || SessionManager.CurrentUser == null)
            {
                MessageBox.Show("Phiên làm việc không tồn tại. Vui lòng đăng nhập.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
                return;
            }

            UserSession user = SessionManager.CurrentUser;

            // Hiển thị thông tin người dùng lên Header & StatusStrip
            lblUserProfile.Text = string.Format("👤 {0} ({1})", user.HoTen, user.VaiTro);
            lblStatusUser.Text = string.Format("Người dùng: {0} ({1})", user.HoTen, user.TenDangNhap);
            lblStatusRole.Text = string.Format("Vai trò: {0}", user.VaiTro);
            lblStatusTime.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

            lblWelcomeGreeting.Text = string.Format("Xin chào, {0}!", user.HoTen);

            // Phân quyền theo vai trò cho cả MenuStrip và Sidebar
            ApplyRolePermissions(user.VaiTro);

            // Nạp dữ liệu KPI và khởi tạo Dashboard
            ShowDashboard();
        }

        private void timerClock_Tick(object sender, EventArgs e)
        {
            lblStatusTime.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        }

        private void menuDoiMatKhau_Click(object sender, EventArgs e)
        {
            using (var frm = new frmDoiMatKhau())
            {
                frm.ShowDialog(this);
            }
        }

        private void menuDangXuat_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show(
                "Bạn có chắc chắn muốn đăng xuất khỏi hệ thống không?",
                "Xác nhận đăng xuất",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (dialogResult == DialogResult.Yes)
            {
                isLoggingOut = true;
                authService.Logout();
                this.Close();
            }
        }

        private void menuThoat_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show(
                "Bạn có chắc chắn muốn thoát ứng dụng không?",
                "Xác nhận thoát",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (dialogResult == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            CloseCurrentChildForm();
            if (!isLoggingOut && e.CloseReason == CloseReason.UserClosing)
            {
                authService.Logout();
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (HandleGlobalHotkeys(keyData))
            {
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
