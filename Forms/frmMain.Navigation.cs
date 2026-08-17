using System;
using System.Drawing;
using System.Windows.Forms;
using DNQH_KeToanBanHang.Helpers;

namespace DNQH_KeToanBanHang.Forms
{
    public partial class frmMain
    {
        #region Role-Based Access Control (RBAC)

        private void ApplyRolePermissions(string vaiTro)
        {
            // 1. Tắt toàn bộ trước tiên (Zero-Trust)
            menuTrangChu.Visible = true;
            menuDanhMuc.Visible = false;
            menuKeToanBanHang.Visible = false;
            menuQuanLyKho.Visible = false;
            menuChungTuTien.Visible = false;
            menuKeToanChiTiet.Visible = false;
            menuKeToanTongHop.Visible = false;

            // Menu con Hệ Thống
            menuQuanLyTaiKhoan.Visible = false;
            menuQuanLyNhanVien.Visible = false;
            menuNhatKyHoatDong.Visible = false;
            menuTraCuuPhimTat.Visible = true;
            toolStripSeparatorTraCuuPhimTat.Visible = false;

            // Menu con Danh Mục
            menuKhachHang.Visible = false;
            menuNhaCungCap.Visible = false;
            menuLoaiSanPham.Visible = false;
            menuSanPham.Visible = false;
            menuKho.Visible = false;

            // Menu con Kế Toán Bán Hàng
            menuDonDatHang.Visible = false;
            menuTraCuuDonDatHang.Visible = false;
            menuHoaDonBan.Visible = false;
            menuTraCuuHoaDon.Visible = false;

            // Menu con Quản Lý Kho
            menuTonKho.Visible = false;
            menuPhieuXuatKho.Visible = false;
            menuTraCuuPhieuXuat.Visible = false;

            // Menu con Chứng Từ Tiền
            menuPhieuThu.Visible = false;
            menuTraCuuPhieuThu.Visible = false;
            menuPhieuChi.Visible = false;
            menuTraCuuPhieuChi.Visible = false;
            menuChungTu.Visible = false;
            menuTraCuuChungTu.Visible = false;

            // Menu con Kế Toán Chi Tiết
            menuSoChiTietKhachHang.Visible = false;
            menuSoChiTietSanPham.Visible = false;
            menuSoChiTietHoaDon.Visible = false;
            menuBaoCaoTuoiNo.Visible = false;

            // Menu con Kế Toán Tổng Hợp
            menuBaoCaoDoanhThu.Visible = false;
            menuBaoCaoThuChi.Visible = false;
            menuBaoCaoTonKho.Visible = false;
            menuBaoCaoBieuDo.Visible = false;

            // Ẩn tất cả nhóm Sidebar
            lblNavGroupSales.Visible = false;
            btnNavDonHang.Visible = false;
            btnNavHoaDon.Visible = false;

            lblNavGroupWarehouse.Visible = false;
            btnNavXuatKho.Visible = false;
            btnNavTonKho.Visible = false;

            lblNavGroupCash.Visible = false;
            btnNavPhieuThu.Visible = false;
            btnNavPhieuChi.Visible = false;
            btnNavChungTu.Visible = false;

            lblNavGroupReport.Visible = false;
            btnNavKeToanChiTiet.Visible = false;
            btnNavBaoCaoTongHop.Visible = false;

            lblNavGroupAdmin.Visible = false;
            btnNavKhachHang.Visible = false;
            btnNavNhaCungCap.Visible = false;
            btnNavSanPham.Visible = false;
            btnNavLoaiSanPham.Visible = false;
            btnNavKho.Visible = false;
            btnNavNhanVien.Visible = false;
            btnNavTaiKhoan.Visible = false;

            // Ẩn các Quick Action buttons
            btnActionDonHang.Visible = false;
            btnActionHoaDon.Visible = false;
            btnActionXuatKho.Visible = false;
            btnActionPhieuThu.Visible = false;
            btnActionBaoCao.Visible = false;
            btnActionTonKho.Visible = false;
            btnActionChungTu.Visible = false;
            btnActionSoQuy.Visible = false;

            if (SessionManager.IsAdmin())
            {
                // Admin: Toàn quyền
                menuQuanLyTaiKhoan.Visible = true;
                menuQuanLyNhanVien.Visible = true;
                menuNhatKyHoatDong.Visible = true;
                toolStripSeparatorTraCuuPhimTat.Visible = true;

                menuDanhMuc.Visible = true;
                menuKhachHang.Visible = true;
                menuNhaCungCap.Visible = true;
                menuLoaiSanPham.Visible = true;
                menuSanPham.Visible = true;
                menuKho.Visible = true;

                menuKeToanBanHang.Visible = true;
                menuDonDatHang.Visible = true;
                menuTraCuuDonDatHang.Visible = true;
                menuHoaDonBan.Visible = true;
                menuTraCuuHoaDon.Visible = true;

                menuQuanLyKho.Visible = true;
                menuTonKho.Visible = true;
                menuPhieuXuatKho.Visible = true;
                menuTraCuuPhieuXuat.Visible = true;

                menuChungTuTien.Visible = true;
                menuPhieuThu.Visible = true;
                menuTraCuuPhieuThu.Visible = true;
                menuPhieuChi.Visible = true;
                menuTraCuuPhieuChi.Visible = true;
                menuChungTu.Visible = true;
                menuTraCuuChungTu.Visible = true;

                menuKeToanChiTiet.Visible = true;
                menuSoChiTietKhachHang.Visible = true;
                menuSoChiTietSanPham.Visible = true;
                menuSoChiTietHoaDon.Visible = true;
                menuBaoCaoTuoiNo.Visible = true;

                menuKeToanTongHop.Visible = true;
                menuBaoCaoDoanhThu.Visible = true;
                menuBaoCaoThuChi.Visible = true;
                menuBaoCaoTonKho.Visible = true;
                menuBaoCaoBieuDo.Visible = true;

                // Sidebar
                lblNavGroupSales.Visible = true;
                btnNavDonHang.Visible = true;
                btnNavHoaDon.Visible = true;

                lblNavGroupWarehouse.Visible = true;
                btnNavXuatKho.Visible = true;
                btnNavTonKho.Visible = true;

                lblNavGroupCash.Visible = true;
                btnNavPhieuThu.Visible = true;
                btnNavPhieuChi.Visible = true;
                btnNavChungTu.Visible = true;

                lblNavGroupReport.Visible = true;
                btnNavKeToanChiTiet.Visible = true;
                btnNavBaoCaoTongHop.Visible = true;

                lblNavGroupAdmin.Visible = true;
                btnNavKhachHang.Visible = true;
                btnNavNhaCungCap.Visible = true;
                btnNavSanPham.Visible = true;
                btnNavLoaiSanPham.Visible = true;
                btnNavKho.Visible = true;
                btnNavNhanVien.Visible = true;
                btnNavTaiKhoan.Visible = true;

                // Quick actions
                btnActionDonHang.Visible = true;
                btnActionHoaDon.Visible = true;
                btnActionXuatKho.Visible = true;
                btnActionPhieuThu.Visible = true;
                btnActionBaoCao.Visible = true;
                btnActionTonKho.Visible = true;
                btnActionChungTu.Visible = true;
                btnActionSoQuy.Visible = true;

                lblWelcomeRoleDesc.Text = "Vai trò: Quản Trị Viên • Bạn có toàn quyền quản trị tài khoản, nhân viên, phân quyền, quản trị bán hàng, kho và xem các báo cáo tổng hợp.";
                lblRoleNoteText.Text = "Bạn có thể quản lý toàn bộ danh mục, nghiệp vụ và báo cáo. Hãy ưu tiên các tác vụ thường dùng ở khu vực Tác vụ nhanh.";
            }
            else if (SessionManager.IsSales())
            {
                // Bán hàng: Danh mục bán hàng, Đơn đặt hàng, Hóa đơn bán, Tra cứu tồn kho
                menuDanhMuc.Visible = true;
                menuKhachHang.Visible = true;
                menuNhaCungCap.Visible = true;
                menuLoaiSanPham.Visible = true;
                menuSanPham.Visible = true;

                menuKeToanBanHang.Visible = true;
                menuDonDatHang.Visible = true;
                menuTraCuuDonDatHang.Visible = true;
                menuHoaDonBan.Visible = true;
                menuTraCuuHoaDon.Visible = true;

                menuQuanLyKho.Visible = true;
                menuTonKho.Visible = true;

                lblNavGroupSales.Visible = true;
                btnNavDonHang.Visible = true;
                btnNavHoaDon.Visible = true;

                lblNavGroupWarehouse.Visible = true;
                btnNavTonKho.Visible = true;

                lblNavGroupAdmin.Visible = true;
                btnNavKhachHang.Visible = true;
                btnNavNhaCungCap.Visible = true;
                btnNavSanPham.Visible = true;
                btnNavLoaiSanPham.Visible = true;

                btnActionDonHang.Visible = true;
                btnActionHoaDon.Visible = true;
                btnActionTonKho.Visible = true;

                lblWelcomeRoleDesc.Text = "Vai trò: Nhân Viên Bán Hàng • Bạn có quyền lập đơn đặt hàng, lập hóa đơn bán, kiểm tra tồn kho và quản lý danh mục khách hàng/sản phẩm.";
                lblRoleNoteText.Text = "Bắt đầu bằng việc kiểm tra tồn kho, chọn khách hàng và lập đơn. Hóa đơn được lập từ đơn hàng hợp lệ.";
            }
            else if (SessionManager.IsWarehouse())
            {
                // Kho: Quản lý kho, Tồn kho, Phiếu xuất kho
                menuDanhMuc.Visible = true;
                menuKho.Visible = true;

                menuQuanLyKho.Visible = true;
                menuTonKho.Visible = true;
                menuPhieuXuatKho.Visible = true;
                menuTraCuuPhieuXuat.Visible = true;

                lblNavGroupWarehouse.Visible = true;
                btnNavXuatKho.Visible = true;
                btnNavTonKho.Visible = true;

                lblNavGroupAdmin.Visible = true;
                btnNavKho.Visible = true;

                btnActionXuatKho.Visible = true;
                btnActionTonKho.Visible = true;

                lblWelcomeRoleDesc.Text = "Vai trò: Nhân Viên Kho • Bạn có quyền quản lý kho, theo dõi số lượng tồn kho, lập và tra cứu phiếu xuất kho cập nhật tồn.";
                lblRoleNoteText.Text = "Kiểm tra tồn kho trước khi xác nhận xuất. Phiếu xuất chỉ được lập cho các mặt hàng còn phải giao trên hóa đơn.";
            }
            else if (SessionManager.IsAccountant())
            {
                // Kế toán: Hóa đơn bán, Phiếu thu/chi, Chứng từ, Sổ chi tiết, Báo cáo tổng hợp, Tra cứu tồn kho
                menuKeToanBanHang.Visible = true;
                menuHoaDonBan.Visible = true;
                menuTraCuuHoaDon.Visible = true;

                menuQuanLyKho.Visible = true;
                menuTonKho.Visible = true;

                menuChungTuTien.Visible = true;
                menuPhieuThu.Visible = true;
                menuTraCuuPhieuThu.Visible = true;
                menuPhieuChi.Visible = true;
                menuTraCuuPhieuChi.Visible = true;
                menuChungTu.Visible = true;
                menuTraCuuChungTu.Visible = true;

                menuKeToanChiTiet.Visible = true;
                menuSoChiTietKhachHang.Visible = true;
                menuSoChiTietSanPham.Visible = true;
                menuSoChiTietHoaDon.Visible = true;
                menuBaoCaoTuoiNo.Visible = true;

                menuKeToanTongHop.Visible = true;
                menuBaoCaoDoanhThu.Visible = true;
                menuBaoCaoThuChi.Visible = true;
                menuBaoCaoTonKho.Visible = true;
                menuBaoCaoBieuDo.Visible = true;

                lblNavGroupSales.Visible = true;
                btnNavHoaDon.Visible = true;

                lblNavGroupWarehouse.Visible = true;
                btnNavTonKho.Visible = true;

                lblNavGroupCash.Visible = true;
                btnNavPhieuThu.Visible = true;
                btnNavPhieuChi.Visible = true;
                btnNavChungTu.Visible = true;

                lblNavGroupReport.Visible = true;
                btnNavKeToanChiTiet.Visible = true;
                btnNavBaoCaoTongHop.Visible = true;

                btnActionPhieuThu.Visible = true;
                btnActionChungTu.Visible = true;
                btnActionBaoCao.Visible = true;
                btnActionTonKho.Visible = true;
                btnActionSoQuy.Visible = true;

                lblWelcomeRoleDesc.Text = "Vai trò: Kế Toán Viên • Bạn có quyền lập phiếu thu/chi, chứng từ hạch toán, hóa đơn bán, xem sổ kế toán chi tiết và báo cáo tổng hợp.";
                lblRoleNoteText.Text = "Theo dõi công nợ trước khi lập phiếu thu và đối chiếu báo cáo sau khi hoàn tất chứng từ trong ngày.";
            }
            else
            {
                lblWelcomeRoleDesc.Text = "Vai trò người dùng thông thường: Chỉ có các quyền cơ bản (đổi mật khẩu, xem thông tin).";
                lblRoleNoteText.Text = "Liên hệ quản trị viên nếu bạn cần bổ sung quyền truy cập chức năng nghiệp vụ.";
            }
        }

        #endregion

        #region Embedded Workspace Controller

        private void CloseCurrentChildForm()
        {
            if (currentChildForm != null)
            {
                Form oldForm = currentChildForm;
                currentChildForm = null; // Gán null trước để chặn hoàn toàn re-entrancy và đệ quy
                oldForm.FormClosed -= ChildForm_FormClosed;
                try
                {
                    oldForm.Close();
                    oldForm.Dispose();
                }
                catch (Exception ex)
                {
                    AppLogger.Warn("CloseCurrentChildForm", "Lỗi khi đóng form con cũ: " + ex.Message);
                }
            }
        }

        private void ChildForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Khi form con tự đóng (do người dùng bấm nút Đóng/Thoát trên form con)
            if (currentChildForm == sender)
            {
                currentChildForm = null;
                ShowDashboard();
            }
        }

        private bool CanAccessForm(Form childForm, out string reason)
        {
            reason = string.Empty;
            if (childForm == null) return false;

            if (SessionManager.IsAdmin())
            {
                return true;
            }

            // Quản trị hệ thống (Tài khoản, Nhân viên, Nhật ký): Chỉ Quản trị viên
            if (childForm is frmTaiKhoan || childForm is frmNhanVien || childForm is frmNhatKyHoatDong)
            {
                reason = "Bạn không có quyền truy cập chức năng này.\nChỉ Quản trị viên mới có quyền quản lý Tài khoản, Nhân viên và Nhật ký hoạt động.";
                return false;
            }

            // Phiếu xuất kho: Chỉ Quản trị viên và Nhân viên kho
            if (childForm is frmPhieuXuatKho)
            {
                if (SessionManager.IsWarehouse()) return true;
                reason = "Bạn không có quyền truy cập vào chức năng Quản lý Phiếu xuất kho.\nChỉ Quản trị viên và Nhân viên kho mới có quyền này.";
                return false;
            }

            // Quản lý kho: Chỉ Quản trị viên và Nhân viên kho
            if (childForm is frmKho)
            {
                if (SessionManager.IsWarehouse()) return true;
                reason = "Bạn không có quyền truy cập vào chức năng Quản lý Kho.\nChỉ Quản trị viên và Nhân viên kho mới có quyền này.";
                return false;
            }

            // Đơn đặt hàng: Chỉ Quản trị viên và Nhân viên bán hàng
            if (childForm is frmDonDatHang)
            {
                if (SessionManager.IsSales()) return true;
                reason = "Bạn không có quyền truy cập vào chức năng Đơn đặt hàng.\nChỉ Quản trị viên và Nhân viên bán hàng mới có quyền này.";
                return false;
            }

            // Hóa đơn bán: Quản trị viên, Nhân viên bán hàng và Kế toán viên
            if (childForm is frmHoaDonBan)
            {
                if (SessionManager.IsSales() || SessionManager.IsAccountant()) return true;
                reason = "Bạn không có quyền truy cập vào chức năng Hóa đơn bán hàng.";
                return false;
            }

            // Thu, Chi, Chứng từ, Sổ chi tiết & Báo cáo tổng hợp: Chỉ Quản trị viên và Kế toán
            if (childForm is frmPhieuThu || childForm is frmPhieuChi || childForm is frmChungTu ||
                childForm is frmKeToanChiTiet || childForm is frmBaoCaoTongHop)
            {
                if (SessionManager.IsAccountant()) return true;
                reason = "Bạn không có quyền truy cập vào chức năng Kế toán và Báo cáo.\nChỉ Quản trị viên và Nhân viên kế toán mới có quyền này.";
                return false;
            }

            // Danh mục bán hàng (Khách hàng, Nhà cung cấp, Loại sản phẩm, Sản phẩm): Quản trị viên và Nhân viên bán hàng
            if (childForm is frmKhachHang || childForm is frmNhaCungCap || childForm is frmSanPham || childForm is frmLoaiSanPham)
            {
                if (SessionManager.IsSales()) return true;
                reason = "Bạn không có quyền truy cập vào chức năng Quản lý danh mục này.\nChỉ Quản trị viên và Nhân viên bán hàng mới có quyền này.";
                return false;
            }

            // Tra cứu tồn kho, Đổi mật khẩu: Tất cả người dùng đã đăng nhập đều có quyền
            if (childForm is frmTonKho || childForm is frmDoiMatKhau)
            {
                return true;
            }

            return true;
        }

        public void ShowEmbeddedForm(Form childForm, string moduleName, string functionName)
        {
            if (childForm == null) return;

            string denyReason;
            if (!CanAccessForm(childForm, out denyReason))
            {
                MessageBox.Show(
                    denyReason,
                    "Từ chối truy cập",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                childForm.Dispose();
                return;
            }

            // Đóng an toàn form con đang hiển thị trước đó, ngắt event để chống lặp đệ quy
            CloseCurrentChildForm();

            currentChildForm = childForm;
            childForm.FormClosed += ChildForm_FormClosed;

            pnlChildContainer.Controls.Clear();

            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            pnlChildContainer.Controls.Add(childForm);

            pnlDashboard.Visible = false;
            pnlChildContainer.Visible = true;
            pnlChildContainer.BringToFront();

            lblBreadcrumb.Text = string.Format("Trang Chủ / {0} / {1}", moduleName, functionName);
            btnBackToDashboard.Visible = true;
            SetActiveNavigation(GetNavigationButton(childForm));

            childForm.Show();
        }

        private Button[] GetNavigationButtons()
        {
            return new[]
            {
                btnNavDashboard,
                btnNavDonHang,
                btnNavHoaDon,
                btnNavXuatKho,
                btnNavTonKho,
                btnNavPhieuThu,
                btnNavPhieuChi,
                btnNavChungTu,
                btnNavKeToanChiTiet,
                btnNavBaoCaoTongHop,
                btnNavKhachHang,
                btnNavNhaCungCap,
                btnNavLoaiSanPham,
                btnNavSanPham,
                btnNavKho,
                btnNavNhanVien,
                btnNavTaiKhoan
            };
        }

        private void SetActiveNavigation(Button activeButton)
        {
            Button[] navigationButtons = GetNavigationButtons();
            for (int i = 0; i < navigationButtons.Length; i++)
            {
                UiStyler.StyleNavigationButton(navigationButtons[i], navigationButtons[i] == activeButton);
            }
        }

        private Button GetNavigationButton(Form childForm)
        {
            if (childForm is frmDonDatHang) return btnNavDonHang;
            if (childForm is frmHoaDonBan) return btnNavHoaDon;
            if (childForm is frmPhieuXuatKho) return btnNavXuatKho;
            if (childForm is frmTonKho) return btnNavTonKho;
            if (childForm is frmPhieuThu) return btnNavPhieuThu;
            if (childForm is frmPhieuChi) return btnNavPhieuChi;
            if (childForm is frmChungTu) return btnNavChungTu;
            if (childForm is frmKeToanChiTiet) return btnNavKeToanChiTiet;
            if (childForm is frmBaoCaoTongHop) return btnNavBaoCaoTongHop;
            if (childForm is frmKhachHang) return btnNavKhachHang;
            if (childForm is frmNhaCungCap) return btnNavNhaCungCap;
            if (childForm is frmLoaiSanPham) return btnNavLoaiSanPham;
            if (childForm is frmSanPham) return btnNavSanPham;
            if (childForm is frmKho) return btnNavKho;
            if (childForm is frmNhanVien) return btnNavNhanVien;
            if (childForm is frmTaiKhoan) return btnNavTaiKhoan;
            return null;
        }

        #endregion

        #region Event Handlers - Navigation & Menus

        private void btnNavDashboard_Click(object sender, EventArgs e)
        {
            ShowDashboard();
        }

        private void btnBackToDashboard_Click(object sender, EventArgs e)
        {
            ShowDashboard();
        }

        private void menuTrangChu_Click(object sender, EventArgs e)
        {
            ShowDashboard();
        }

        // --- BÁN HÀNG ---

        private void menuDonDatHang_Click(object sender, EventArgs e)
        {
            ShowEmbeddedForm(new frmDonDatHang(), "Kế Toán Bán Hàng", "Đơn Đặt Hàng");
        }

        private void menuHoaDonBan_Click(object sender, EventArgs e)
        {
            ShowEmbeddedForm(new frmHoaDonBan(), "Kế Toán Bán Hàng", "Hóa Đơn Bán Hàng");
        }

        // --- QUẢN LÝ KHO ---

        private void menuPhieuXuatKho_Click(object sender, EventArgs e)
        {
            ShowEmbeddedForm(new frmPhieuXuatKho(), "Quản Lý Kho", "Phiếu Xuất Kho");
        }

        private void menuTonKho_Click(object sender, EventArgs e)
        {
            ShowEmbeddedForm(new frmTonKho(), "Quản Lý Kho", "Tra Cứu Tồn Kho");
        }

        // --- CHỨNG TỪ & TIỀN ---

        private void menuPhieuThu_Click(object sender, EventArgs e)
        {
            ShowEmbeddedForm(new frmPhieuThu(), "Chứng Từ & Tiền", "Lập Phiếu Thu");
        }

        private void menuPhieuChi_Click(object sender, EventArgs e)
        {
            ShowEmbeddedForm(new frmPhieuChi(), "Chứng Từ & Tiền", "Lập Phiếu Chi");
        }

        private void menuChungTu_Click(object sender, EventArgs e)
        {
            ShowEmbeddedForm(new frmChungTu(), "Chứng Từ & Tiền", "Chứng Từ Kế Toán");
        }

        // --- BÁO CÁO & SỔ SÁCH ---

        public void OpenKeToanChiTiet(int tabIndex)
        {
            frmKeToanChiTiet frm = new frmKeToanChiTiet();
            string subTitle = "Sổ Chi Tiết";
            if (tabIndex == 0) subTitle = "Sổ Chi Tiết Khách Hàng";
            else if (tabIndex == 1) subTitle = "Sổ Chi Tiết Sản Phẩm";
            else if (tabIndex == 2) subTitle = "Sổ Chi Tiết Hóa Đơn";
            else if (tabIndex == 3) subTitle = "Báo Cáo Tuổi Nợ";
            ShowEmbeddedForm(frm, "Kế Toán Chi Tiết", subTitle);
            frm.SelectTab(tabIndex);
        }

        public void OpenBaoCaoTongHop(int tabIndex)
        {
            frmBaoCaoTongHop frm = new frmBaoCaoTongHop();
            string subTitle = "Báo Cáo Tổng Hợp";
            if (tabIndex == 0) subTitle = "Báo Cáo Doanh Thu";
            else if (tabIndex == 1) subTitle = "Báo Cáo Thu Chi";
            else if (tabIndex == 2) subTitle = "Báo Cáo Tồn Kho";
            else if (tabIndex == 3) subTitle = "Biểu Đồ Phân Tích";
            ShowEmbeddedForm(frm, "Kế Toán Tổng Hợp", subTitle);
            frm.SelectTab(tabIndex);
        }

        private void menuSoChiTietKhachHang_Click(object sender, EventArgs e)
        {
            OpenKeToanChiTiet(0);
        }

        private void menuSoChiTietSanPham_Click(object sender, EventArgs e)
        {
            OpenKeToanChiTiet(1);
        }

        private void menuSoChiTietHoaDon_Click(object sender, EventArgs e)
        {
            OpenKeToanChiTiet(2);
        }

        private void menuBaoCaoTuoiNo_Click(object sender, EventArgs e)
        {
            OpenKeToanChiTiet(3);
        }

        private void menuBaoCaoDoanhThu_Click(object sender, EventArgs e)
        {
            OpenBaoCaoTongHop(0);
        }

        private void menuBaoCaoThuChi_Click(object sender, EventArgs e)
        {
            OpenBaoCaoTongHop(1);
        }

        private void menuBaoCaoTonKho_Click(object sender, EventArgs e)
        {
            OpenBaoCaoTongHop(2);
        }

        private void menuBaoCaoBieuDo_Click(object sender, EventArgs e)
        {
            OpenBaoCaoTongHop(3);
        }

        // --- DANH MỤC & QUẢN TRỊ ---

        private void menuKhachHang_Click(object sender, EventArgs e)
        {
            ShowEmbeddedForm(new frmKhachHang(), "Danh Mục", "Khách Hàng");
        }

        private void menuNhaCungCap_Click(object sender, EventArgs e)
        {
            ShowEmbeddedForm(new frmNhaCungCap(), "Danh Mục", "Nhà Cung Cấp");
        }

        private void menuSanPham_Click(object sender, EventArgs e)
        {
            ShowEmbeddedForm(new frmSanPham(), "Danh Mục", "Sản Phẩm");
        }

        private void menuLoaiSanPham_Click(object sender, EventArgs e)
        {
            ShowEmbeddedForm(new frmLoaiSanPham(), "Danh Mục", "Loại Sản Phẩm");
        }

        private void menuKho_Click(object sender, EventArgs e)
        {
            ShowEmbeddedForm(new frmKho(), "Danh Mục", "Kho Hàng");
        }

        private void menuQuanLyNhanVien_Click(object sender, EventArgs e)
        {
            ShowEmbeddedForm(new frmNhanVien(), "Hệ Thống", "Quản Lý Nhân Viên");
        }

        private void menuQuanLyTaiKhoan_Click(object sender, EventArgs e)
        {
            ShowEmbeddedForm(new frmTaiKhoan(), "Hệ Thống", "Quản Lý Tài Khoản");
        }

        private void menuNhatKyHoatDong_Click(object sender, EventArgs e)
        {
            ShowEmbeddedForm(new frmNhatKyHoatDong(), "Hệ Thống", "Nhật Ký Hoạt Động (Audit Trail)");
        }

        private void menuTraCuuPhimTat_Click(object sender, EventArgs e)
        {
            UiInteractionHelper.ShowHotkeysHelp(this);
        }

        // --- TRA CỨU MAPPING ---

        private void menuTraCuuDonDatHang_Click(object sender, EventArgs e)
        {
            var frm = new frmDonDatHang();
            ShowEmbeddedForm(frm, "Kế Toán Bán Hàng", "Tra Cứu Đơn Đặt Hàng");
            frm.SelectTab(1);
        }

        private void menuTraCuuHoaDon_Click(object sender, EventArgs e)
        {
            var frm = new frmHoaDonBan();
            ShowEmbeddedForm(frm, "Kế Toán Bán Hàng", "Tra Cứu Hóa Đơn Bán");
            frm.SelectTab(1);
        }

        private void menuTraCuuPhieuXuat_Click(object sender, EventArgs e)
        {
            var frm = new frmPhieuXuatKho();
            ShowEmbeddedForm(frm, "Quản Lý Kho", "Tra Cứu Phiếu Xuất Kho");
            frm.SelectTab(1);
        }

        private void menuTraCuuPhieuThu_Click(object sender, EventArgs e)
        {
            var frm = new frmPhieuThu();
            ShowEmbeddedForm(frm, "Chứng Từ & Tiền", "Tra Cứu Phiếu Thu");
            frm.SelectTab(1);
        }

        private void menuTraCuuPhieuChi_Click(object sender, EventArgs e)
        {
            var frm = new frmPhieuChi();
            ShowEmbeddedForm(frm, "Chứng Từ & Tiền", "Tra Cứu Phiếu Chi");
            frm.SelectTab(1);
        }

        private void menuTraCuuChungTu_Click(object sender, EventArgs e)
        {
            var frm = new frmChungTu();
            ShowEmbeddedForm(frm, "Chứng Từ & Tiền", "Tra Cứu Chứng Từ");
            frm.SelectTab(1);
        }

        #endregion

        #region Global Hotkeys Navigation

        public bool HandleGlobalHotkeys(Keys keyData)
        {
            // F1: Mở trợ giúp phím tắt toàn hệ thống
            if (keyData == Keys.F1)
            {
                UiInteractionHelper.ShowHotkeysHelp(this);
                return true;
            }

            // Xử lý các tổ hợp phím điều hướng nhanh Ctrl + ...
            Keys modifiers = keyData & Keys.Modifiers;
            Keys key = keyData & Keys.KeyCode;

            if (modifiers == Keys.Control)
            {
                switch (key)
                {
                    case Keys.D1:
                    case Keys.NumPad1:
                        ShowEmbeddedForm(new frmDonDatHang(), "Kế Toán Bán Hàng", "Đơn Đặt Hàng");
                        return true;

                    case Keys.D2:
                    case Keys.NumPad2:
                        ShowEmbeddedForm(new frmHoaDonBan(), "Kế Toán Bán Hàng", "Hóa Đơn Bán Hàng");
                        return true;

                    case Keys.D3:
                    case Keys.NumPad3:
                        ShowEmbeddedForm(new frmPhieuThu(), "Chứng Từ & Tiền", "Lập Phiếu Thu");
                        return true;

                    case Keys.D4:
                    case Keys.NumPad4:
                        ShowEmbeddedForm(new frmPhieuChi(), "Chứng Từ & Tiền", "Lập Phiếu Chi");
                        return true;

                    case Keys.D5:
                    case Keys.NumPad5:
                        ShowEmbeddedForm(new frmPhieuXuatKho(), "Quản Lý Kho", "Phiếu Xuất Kho");
                        return true;

                    case Keys.K:
                        ShowEmbeddedForm(new frmKhachHang(), "Danh Mục", "Khách Hàng");
                        return true;

                    case Keys.M:
                        ShowEmbeddedForm(new frmSanPham(), "Danh Mục", "Sản Phẩm");
                        return true;

                    case Keys.B:
                        OpenBaoCaoTongHop(0);
                        return true;

                    case Keys.T:
                        OpenKeToanChiTiet(0);
                        return true;

                    case Keys.H:
                        ShowDashboard();
                        return true;
                }
            }

            return false;
        }

        #endregion
    }
}
