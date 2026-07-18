using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using DNQH_KeToanBanHang.Constants;
using DNQH_KeToanBanHang.DataAccess;
using DNQH_KeToanBanHang.Helpers;
using DNQH_KeToanBanHang.Models;
using DNQH_KeToanBanHang.Services;

namespace DNQH_KeToanBanHang.Forms
{
    public partial class frmDonDatHang : Form
    {
        private readonly OrderService _orderService;
        private readonly DonDatHangDAL _donDatHangDAL;
        private readonly KhachHangDAL _khachHangDAL;
        private readonly SanPhamDAL _sanPhamDAL;
        private readonly TonKhoDAL _tonKhoDAL;
        private readonly BaoCaoDAL _baoCaoDAL;
        private readonly ErrorProvider _validationErrors;
        private readonly UiDebouncer _searchDebouncer;
        private readonly UiPaginationControl _pagerDonDatHang;

        private DataTable _dtChiTiet;
        private bool _isBinding = false;
        private int _currentOrderVersion = 1;
        private bool? _isWideOrderLayout;
        private int? _productLayoutMode;
        private Button btnInDonHangTab1;
        private Button btnInDonHangDS;
        private ContextMenuStrip cmsDonHang;
        private string _lastCreatedMaDDH = null;
        private Label lblCanhBaoCongNo;

        private const int WideLayoutBreakpoint = 1280;

        public frmDonDatHang()
        {
            InitializeComponent();
            _pagerDonDatHang = new UiPaginationControl();
            _pagerDonDatHang.PageChanged += async delegate(object sender, PageChangedEventArgs e)
            {
                await LoadDanhSachDonAsync(++_currentOrderVersion, null, true);
            };
            _validationErrors = UiStyler.CreateErrorProvider(this);
            BuildResponsiveLayout();
            ApplyFoundationDesign();
            _orderService = new OrderService();
            _donDatHangDAL = new DonDatHangDAL();
            _khachHangDAL = new KhachHangDAL();
            _sanPhamDAL = new SanPhamDAL();
            _tonKhoDAL = new TonKhoDAL();
            _baoCaoDAL = new BaoCaoDAL();
            _searchDebouncer = new UiDebouncer(350);
            Disposed += delegate { _searchDebouncer.Dispose(); };

            InitChiTietTable();
            dgvDanhSachDon.SelectionChanged += dgvDanhSachDon_SelectionChanged;
            cboKhachHang.SelectedIndexChanged += async delegate { await CapNhatCanhBaoCongNoAsync(); };
        }

        private void frmDonDatHang_Load(object sender, EventArgs e)
        {
            this.ApplyResponsiveUI();

            // Kiểm tra phân quyền truy cập form
            if (!SessionManager.IsAdmin() && !SessionManager.IsSales())
            {
                MessageBox.Show("Bạn không có quyền truy cập vào chức năng Quản lý Đơn đặt hàng.", "Từ chối truy cập", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
                return;
            }

            // Hiển thị thông tin nhân viên đăng nhập
            if (SessionManager.CurrentUser != null)
            {
                lblNhanVienLap.Text = string.Format("{0} ({1})", SessionManager.CurrentUser.HoTen, SessionManager.CurrentUser.MaNV);
            }

            LoadCombos();
            ResetForm();

            dtpFromDate.Value = DateTime.Today.AddDays(-30);
            dtpToDate.Value = DateTime.Today;
            UiComboBoxHelper.PopulateItems(cboTrangThaiLoc, OrderStatusConstants.FilterList, "-- Tất cả --");

            txtTimKiem.TextChanged += txtTimKiem_TextChanged;
            btnTimKiem.Click += btnTimKiem_Click;
            cboTrangThaiLoc.SelectedIndexChanged += cboTrangThaiLoc_SelectedIndexChanged;

            LoadDanhSachDon();
        }

        public void SelectTab(int index)
        {
            if (index >= 0 && index < tcDonHang.TabPages.Count)
            {
                tcDonHang.SelectedIndex = index;
                if (index == 1 && txtTimKiem != null && txtTimKiem.CanFocus)
                {
                    txtTimKiem.Focus();
                }
            }
        }

        private void LoadCombos()
        {
            try
            {
                _isBinding = true;

                // Load Khách Hàng
                DataTable dtKH = _khachHangDAL.GetAll();
                cboKhachHang.DisplayMember = "TenKH";
                cboKhachHang.ValueMember = "MaKH";
                cboKhachHang.DataSource = dtKH;
                if (cboKhachHang.Items.Count > 0)
                {
                    cboKhachHang.SelectedIndex = 0;
                }

                // Load Sản Phẩm (chỉ lấy các sản phẩm đang kinh doanh / hoạt động)
                DataTable dtSP = _sanPhamDAL.GetActiveProducts();
                if (dtSP == null || dtSP.Rows.Count == 0)
                {
                    dtSP = _sanPhamDAL.GetAll();
                }
                cboSanPham.DisplayMember = "TenSP";
                cboSanPham.ValueMember = "MaSP";
                cboSanPham.DataSource = dtSP;
                if (cboSanPham.Items.Count > 0)
                {
                    cboSanPham.SelectedIndex = 0;
                }

                UiComboBoxHelper.PopulateItems(cboTrangThai, OrderStatusConstants.All, OrderStatusConstants.ChoXuLy);
            }
            catch (Exception ex)
            {
                UiErrorHandler.Show(this, "FRMDONDATHANG_UI_ERROR", "Lỗi khi tải danh mục hỗ trợ.", ex);
            }
            finally
            {
                _isBinding = false;
                UpdateSanPhamInfo();
            }
        }

        private void ResetForm()
        {
            _currentOrderVersion = 1;
            txtMaDDH.Text = _orderService.GenerateNewOrderId();
            dtpNgayDat.Value = DateTime.Now;
            dtpNgayGiao.Checked = false;
            cboTrangThai.SelectedIndex = 0;
            txtGhiChu.Clear();

            _dtChiTiet.Clear();
            dgvChiTiet.AutoGenerateColumns = false;
            dgvChiTiet.DataSource = _dtChiTiet;

            nudSoLuong.Value = 1;
            nudGiamGia.Value = 0;
            UpdateSanPhamInfo();
            RecalculateGrandTotal();
        }

        private async Task CapNhatCanhBaoCongNoAsync()
        {
            if (cboKhachHang.SelectedValue == null || lblCanhBaoCongNo == null)
            {
                if (lblCanhBaoCongNo != null) lblCanhBaoCongNo.Visible = false;
                return;
            }

            string maKH = cboKhachHang.SelectedValue.ToString().Trim();
            if (string.IsNullOrEmpty(maKH))
            {
                lblCanhBaoCongNo.Visible = false;
                return;
            }

            try
            {
                ThongTinCongNoKhachHangDTO debt = await Task.Run(() => _baoCaoDAL.GetThongTinCongNoKhachHang(maKH));
                if (debt == null)
                {
                    lblCanhBaoCongNo.Visible = false;
                    return;
                }

                lblCanhBaoCongNo.Visible = true;
                if (debt.HasBadDebt)
                {
                    lblCanhBaoCongNo.Text = string.Format("⛔ NỢ KHÓ ĐÒI: {0:N0} đ (>90 ngày, tổng nợ: {1:N0} đ) - TẠM KHÓA ĐẶT HÀNG!", debt.KhoDoi_Tren90, debt.TongNo);
                    lblCanhBaoCongNo.BackColor = Color.FromArgb(254, 226, 226);
                    lblCanhBaoCongNo.ForeColor = Color.FromArgb(185, 28, 28);
                }
                else if (debt.QuaHanTB_61_90 > 0)
                {
                    lblCanhBaoCongNo.Text = string.Format("⚠️ CẢNH BÁO CAO: Nợ quá hạn 61-90 ngày: {0:N0} đ (Tổng nợ: {1:N0} đ)", debt.QuaHanTB_61_90, debt.TongNo);
                    lblCanhBaoCongNo.BackColor = Color.FromArgb(255, 237, 213);
                    lblCanhBaoCongNo.ForeColor = Color.FromArgb(194, 65, 12);
                }
                else if (debt.QuaHanNhe_31_60 > 0)
                {
                    lblCanhBaoCongNo.Text = string.Format("⚠️ LƯU Ý: Nợ quá hạn 31-60 ngày: {0:N0} đ (Tổng nợ: {1:N0} đ)", debt.QuaHanNhe_31_60, debt.TongNo);
                    lblCanhBaoCongNo.BackColor = Color.FromArgb(254, 249, 195);
                    lblCanhBaoCongNo.ForeColor = Color.FromArgb(133, 77, 14);
                }
                else if (debt.TongNo > 0)
                {
                    lblCanhBaoCongNo.Text = string.Format("ℹ️ Công nợ trong hạn: {0:N0} đ (≤30 ngày) - Lịch sử tốt", debt.TongNo);
                    lblCanhBaoCongNo.BackColor = Color.FromArgb(240, 253, 244);
                    lblCanhBaoCongNo.ForeColor = Color.FromArgb(21, 128, 61);
                }
                else
                {
                    lblCanhBaoCongNo.Text = "✅ Khách hàng không có công nợ - Tín dụng chuẩn";
                    lblCanhBaoCongNo.BackColor = Color.FromArgb(240, 253, 244);
                    lblCanhBaoCongNo.ForeColor = Color.FromArgb(21, 128, 61);
                }
            }
            catch
            {
                lblCanhBaoCongNo.Visible = false;
            }
        }

        private async void btnLuuDon_Click(object sender, EventArgs e)
        {
            if (cboKhachHang.SelectedValue == null)
            {
                ShowValidation(cboKhachHang, "Vui lòng chọn khách hàng đặt hàng.");
                return;
            }

            string maKH = cboKhachHang.SelectedValue.ToString().Trim();

            // 1. Kiểm tra chính sách tuổi nợ khách hàng và cảnh báo / chặn lập đơn
            ThongTinCongNoKhachHangDTO debt = _baoCaoDAL.GetThongTinCongNoKhachHang(maKH);
            if (debt != null && debt.HasBadDebt)
            {
                MessageBox.Show(
                    string.Format("Khách hàng đang có khoản nợ khó đòi quá hạn trên 90 ngày ({0:N0} VNĐ, tổng nợ: {1:N0} VNĐ).\n\nTheo quy chế tín dụng doanh nghiệp, hệ thống tự động khóa tính năng lập đơn đặt hàng mới đối với khách hàng này. Vui lòng hoàn tất thu hồi nợ trước khi tiếp tục!",
                        debt.KhoDoi_Tren90, debt.TongNo),
                    "Chặn đặt hàng - Nợ khó đòi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Stop);
                return;
            }

            if (debt != null && (debt.QuaHanTB_61_90 > 0 || debt.QuaHanNhe_31_60 > 0))
            {
                decimal tongQuaHan = debt.QuaHanTB_61_90 + debt.QuaHanNhe_31_60;
                DialogResult confirmDebt = MessageBox.Show(
                    string.Format("CẢNH BÁO NỢ QUÁ HẠN:\nKhách hàng hiện có khoản nợ quá hạn {0:N0} VNĐ (Tổng nợ: {1:N0} VNĐ).\n\nBạn có chắc chắn đã được phê duyệt hạn mức để tiếp tục lập đơn hàng mới này không?",
                        tongQuaHan, debt.TongNo),
                    "Cảnh báo rủi ro tín dụng",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (confirmDebt != DialogResult.Yes)
                {
                    return;
                }
            }

            if (_dtChiTiet.Rows.Count == 0)
            {
                ShowValidation(dgvChiTiet, "Đơn đặt hàng phải có ít nhất một dòng sản phẩm.");
                return;
            }

            DonDatHang order = new DonDatHang
            {
                MaDDH = txtMaDDH.Text.Trim(),
                MaKH = cboKhachHang.SelectedValue.ToString().Trim(),
                MaNV = SessionManager.CurrentUser != null ? SessionManager.CurrentUser.MaNV : "NV001",
                NgayDat = dtpNgayDat.Value,
                NgayGiaoDuKien = dtpNgayGiao.Checked ? (DateTime?)dtpNgayGiao.Value.Date : null,
                TrangThai = cboTrangThai.SelectedItem != null ? cboTrangThai.SelectedItem.ToString() : OrderStatusConstants.ChoXuLy,
                GhiChu = txtGhiChu.Text.Trim()
            };

            List<ChiTietDonDatHang> details = new List<ChiTietDonDatHang>();
            foreach (DataRow row in _dtChiTiet.Rows)
            {
                details.Add(new ChiTietDonDatHang
                {
                    MaDDH = order.MaDDH,
                    MaSP = row["MaSP"].ToString().Trim(),
                    TenSP = row["TenSP"].ToString().Trim(),
                    DonViTinh = row["DonViTinh"].ToString().Trim(),
                    SoLuong = Convert.ToInt32(row["SoLuong"]),
                    DonGia = Convert.ToDecimal(row["DonGia"]),
                    GiamGia = Convert.ToDecimal(row["GiamGia"]),
                    ThanhTien = Convert.ToDecimal(row["ThanhTien"])
                });
            }

            string error = string.Empty;
            bool success = await UiFeedbackHelper.RunBusyAsync(
                this,
                btnLuuDon,
                "ĐANG LƯU...",
                delegate { return _orderService.CreateOrder(order, details, out error); });
            if (IsDisposed)
            {
                return;
            }

            if (success)
            {
                _lastCreatedMaDDH = order.MaDDH;
                DialogResult printConfirm = MessageBox.Show(
                    string.Format("Lập đơn đặt hàng [{0}] thành công!\n\nBạn có muốn in phiếu đơn đặt hàng ngay bây giờ không?", order.MaDDH),
                    "Thành công",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (printConfirm == DialogResult.Yes)
                {
                    InDonHang(order.MaDDH);
                }

                ResetForm();
                LoadDanhSachDon();
            }
            else
            {
                MessageBox.Show(error, "Lỗi lập đơn đặt hàng", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void LoadDanhSachDon()
        {
            await LoadDanhSachDonAsync(++_currentOrderVersion, null, false);
        }

        private async Task LoadDanhSachDonAsync(int requestVersion, Button actionButton, bool useFilters)
        {
            string keyword = txtTimKiem.Text.Trim();
            DateTime? from = useFilters ? (DateTime?)dtpFromDate.Value.Date : null;
            DateTime? to = useFilters ? (DateTime?)dtpToDate.Value.Date.AddDays(1).AddTicks(-1) : null;
            string trangThai = null;

            if (useFilters && cboTrangThaiLoc.SelectedIndex > 0)
            {
                trangThai = cboTrangThaiLoc.SelectedItem.ToString().Trim();
            }

            try
            {
                PagedDataTable paged = await UiFeedbackHelper.RunBusyAsync(
                    this,
                    actionButton,
                    "ĐANG TẢI...",
                    delegate { return _donDatHangDAL.SearchPhanTrang(keyword, from, to, trangThai, _pagerDonDatHang.CurrentPage, _pagerDonDatHang.PageSize); });
                if (IsDisposed || requestVersion != _currentOrderVersion)
                {
                    return;
                }

                DataTable dt = paged != null ? paged.Table : null;
                dgvDanhSachDon.AutoGenerateColumns = false;
                dgvDanhSachDon.DataSource = dt;

                int total = paged != null ? paged.TotalRecords : 0;
                _pagerDonDatHang.UpdateState(_pagerDonDatHang.CurrentPage, _pagerDonDatHang.PageSize, total);
            }
            catch (Exception ex)
            {
                if (IsDisposed || requestVersion != _currentOrderVersion)
                {
                    return;
                }
                UiErrorHandler.Show(this, "FRMDONDATHANG_LOAD_ERROR", "Lỗi khi tải danh sách đơn đặt hàng.", ex);
            }
            finally
            {
                UpdateLapHoaDonButtonState();
            }
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            _searchDebouncer.Restart(delegate(int version)
            {
                _pagerDonDatHang.Reset(_pagerDonDatHang.PageSize);
                return LoadDanhSachDonAsync(version, null, true);
            });
        }

        private async void btnTimKiem_Click(object sender, EventArgs e)
        {
            _pagerDonDatHang.Reset(_pagerDonDatHang.PageSize);
            await _searchDebouncer.RunNowAsync(delegate(int version) { return LoadDanhSachDonAsync(version, btnTimKiem, true); });
        }

        private void cboTrangThaiLoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            _searchDebouncer.Restart(delegate(int version)
            {
                _pagerDonDatHang.Reset(_pagerDonDatHang.PageSize);
                return LoadDanhSachDonAsync(version, null, true);
            });
        }

        private async void btnLamMoiDanhSach_Click(object sender, EventArgs e)
        {
            txtTimKiem.Clear();
            cboTrangThaiLoc.SelectedIndex = 0;
            dtpFromDate.Value = DateTime.Today.AddDays(-30);
            dtpToDate.Value = DateTime.Today;
            _pagerDonDatHang.Reset(_pagerDonDatHang.PageSize);
            await _searchDebouncer.RunNowAsync(delegate(int version) { return LoadDanhSachDonAsync(version, btnLamMoiDanhSach, false); });
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            ResetForm();
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvDanhSachDon_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            string maDDH = dgvDanhSachDon.Rows[e.RowIndex].Cells["colDSMaDDH"].Value != null
                ? dgvDanhSachDon.Rows[e.RowIndex].Cells["colDSMaDDH"].Value.ToString().Trim()
                : "";

            if (!string.IsNullOrEmpty(maDDH))
            {
                try
                {
                    DonDatHang order = _donDatHangDAL.GetById(maDDH);
                    if (order != null)
                    {
                        txtMaDDH.Text = order.MaDDH;
                        cboKhachHang.SelectedValue = order.MaKH;
                        dtpNgayDat.Value = order.NgayDat;
                        if (order.NgayGiaoDuKien.HasValue)
                        {
                            dtpNgayGiao.Checked = true;
                            dtpNgayGiao.Value = order.NgayGiaoDuKien.Value;
                        }
                        else
                        {
                            dtpNgayGiao.Checked = false;
                        }
                        UiComboBoxHelper.SafeSelect(cboTrangThai, order.TrangThai);
                        txtGhiChu.Text = order.GhiChu;

                        // Nạp chi tiết
                        DataTable dtDetails = _donDatHangDAL.GetChiTietDataTable(maDDH);
                        _dtChiTiet.Clear();
                        foreach (DataRow r in dtDetails.Rows)
                        {
                            DataRow newRow = _dtChiTiet.NewRow();
                            newRow["MaSP"] = r["MaSP"];
                            newRow["TenSP"] = r["TenSP"];
                            newRow["DonViTinh"] = r["DonViTinh"];
                            newRow["SoLuong"] = r["SoLuong"];
                            newRow["DonGia"] = r["DonGia"];
                            newRow["GiamGia"] = r["GiamGia"];
                            newRow["ThanhTien"] = r["ThanhTien"];
                            _dtChiTiet.Rows.Add(newRow);
                        }

                        RecalculateGrandTotal();
                        tcDonHang.SelectedIndex = 0; // Chuyển về tab Lập đơn
                    }
                }
                catch (Exception ex)
                {
                    UiErrorHandler.Show(this, "FRMDONDATHANG_VIEW_ERROR", "Lỗi khi xem chi tiết đơn đặt hàng.", ex);
                }
            }
        }

        private void ShowValidation(Control control, string message)
        {
            _validationErrors.SetError(control, message);
            UiFeedbackHelper.ShowToast(this, message, UiStatusKind.Warning, 4000);
            if (control != null && control.CanFocus)
            {
                control.Focus();
            }
        }

        private void dgvDanhSachDon_SelectionChanged(object sender, EventArgs e)
        {
            UpdateLapHoaDonButtonState();
        }

        private void UpdateLapHoaDonButtonState()
        {
            if (dgvDanhSachDon.CurrentRow == null || dgvDanhSachDon.CurrentRow.Index < 0)
            {
                btnLapHoaDonTuDon.Enabled = false;
                btnLapHoaDonTuDon.Text = "📄 Lập hóa đơn từ đơn";
                return;
            }

            string trangThai = dgvDanhSachDon.CurrentRow.Cells["colDSTrangThai"].Value != null
                ? dgvDanhSachDon.CurrentRow.Cells["colDSTrangThai"].Value.ToString().Trim()
                : "";

            bool daLapHD = string.Equals(trangThai, "Đã lập hóa đơn", StringComparison.OrdinalIgnoreCase) ||
                           string.Equals(trangThai, "Đã xuất hóa đơn", StringComparison.OrdinalIgnoreCase);
            bool daHuy = string.Equals(trangThai, "Đã hủy", StringComparison.OrdinalIgnoreCase);

            if (daLapHD)
            {
                btnLapHoaDonTuDon.Enabled = false;
                btnLapHoaDonTuDon.Text = "✓ Đã có hóa đơn";
            }
            else if (daHuy)
            {
                btnLapHoaDonTuDon.Enabled = false;
                btnLapHoaDonTuDon.Text = "⛔ Đơn đã hủy";
            }
            else
            {
                btnLapHoaDonTuDon.Enabled = true;
                btnLapHoaDonTuDon.Text = "📄 Lập hóa đơn từ đơn";
            }
        }

        private void btnLapHoaDonTuDon_Click(object sender, EventArgs e)
        {
            if (dgvDanhSachDon.CurrentRow == null || dgvDanhSachDon.CurrentRow.Index < 0)
            {
                MessageBox.Show("Vui lòng chọn một đơn đặt hàng trên danh sách để lập hóa đơn.", "Chưa chọn đơn hàng", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string maDDH = dgvDanhSachDon.CurrentRow.Cells["colDSMaDDH"].Value != null
                ? dgvDanhSachDon.CurrentRow.Cells["colDSMaDDH"].Value.ToString().Trim()
                : "";

            if (string.IsNullOrEmpty(maDDH))
            {
                MessageBox.Show("Không xác định được mã đơn đặt hàng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string trangThai = dgvDanhSachDon.CurrentRow.Cells["colDSTrangThai"].Value != null
                ? dgvDanhSachDon.CurrentRow.Cells["colDSTrangThai"].Value.ToString().Trim()
                : "";

            if (string.Equals(trangThai, "Đã hủy", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show(string.Format("Đơn đặt hàng [{0}] đã bị hủy, không thể lập hóa đơn.", maDDH), "Đơn hàng đã hủy", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.Equals(trangThai, "Đã lập hóa đơn", StringComparison.OrdinalIgnoreCase) ||
                string.Equals(trangThai, "Đã xuất hóa đơn", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show(string.Format("Đơn đặt hàng [{0}] đã được lập hóa đơn bán trước đó. Mỗi đơn đặt hàng chỉ được lập tối đa 1 hóa đơn bán.", maDDH), "Đã lập hóa đơn", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (var frm = new frmHoaDonBan(maDDH))
            {
                if (frm.ShowDialog(this) == DialogResult.OK)
                {
                    LoadDanhSachDon();
                }
            }
        }

        private void InDonHangDangChon()
        {
            if (dgvDanhSachDon.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn một đơn đặt hàng cần in!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string maDDH = dgvDanhSachDon.CurrentRow.Cells["colDSMaDDH"].Value != null
                ? dgvDanhSachDon.CurrentRow.Cells["colDSMaDDH"].Value.ToString().Trim()
                : string.Empty;

            InDonHang(maDDH);
        }

        private void InDonHang(string maDDH)
        {
            if (string.IsNullOrEmpty(maDDH))
            {
                MessageBox.Show("Vui lòng chỉ định mã đơn đặt hàng cần in!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                DonDatHang order = _donDatHangDAL.GetById(maDDH);
                if (order == null)
                {
                    MessageBox.Show("Không tìm thấy dữ liệu đơn đặt hàng " + maDDH, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DataTable dtChiTiet = _donDatHangDAL.GetChiTietDataTable(maDDH);
                string html = VoucherPrintHelper.GenerateDonDatHangHtml(order, dtChiTiet);
                frmInChungTu.ShowVoucher(this, "Đơn Đặt Hàng - " + maDDH, html, "DonDatHang_" + maDDH);
            }
            catch (Exception ex)
            {
                AppLogger.Error("PRINT_DDH_ERROR", "Lỗi khi xuất mẫu in đơn đặt hàng: " + ex.Message, ex);
                MessageBox.Show("Lỗi khi mở mẫu in đơn đặt hàng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
