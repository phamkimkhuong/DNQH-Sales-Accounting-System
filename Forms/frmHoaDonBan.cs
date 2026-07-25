using System;
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
    public partial class frmHoaDonBan : Form
    {
        private readonly InvoiceService _invoiceService;
        private readonly HoaDonBanDAL _hoaDonBanDAL;
        private readonly DonDatHangDAL _donDatHangDAL;
        private readonly ErrorProvider _validationErrors;
        private readonly UiDebouncer _searchDebouncer;
        private readonly UiPaginationControl _pagerHoaDon;

        private DataTable _dtChiTiet;
        private bool _isBinding = false;
        private readonly string _initialMaDDH;

        public frmHoaDonBan() : this(null)
        {
        }

        public frmHoaDonBan(string initialMaDDH)
        {
            _initialMaDDH = initialMaDDH;
            InitializeComponent();
            _pagerHoaDon = new UiPaginationControl();
            _pagerHoaDon.PageChanged += async delegate(object sender, PageChangedEventArgs e)
            {
                await _searchDebouncer.RunNowAsync(
                    delegate(int version) { return LoadDanhSachHoaDonAsync(version, null, true); });
            };
            _validationErrors = UiStyler.CreateErrorProvider(this);
            BuildResponsiveLayout();
            ApplyFoundationDesign();
            _invoiceService = new InvoiceService();
            _hoaDonBanDAL = new HoaDonBanDAL();
            _donDatHangDAL = new DonDatHangDAL();
            _searchDebouncer = new UiDebouncer(350);
            Disposed += delegate { _searchDebouncer.Dispose(); };

            InitChiTietTable();

            if (!string.IsNullOrEmpty(_initialMaDDH))
            {
                this.StartPosition = FormStartPosition.CenterParent;
                this.Text = string.Format("Lập Hóa Đơn Bán Hàng - Đơn hàng [{0}]", _initialMaDDH);
                this.MaximizeBox = false;
                this.MinimizeBox = false;
                this.ShowInTaskbar = false;
                this.Size = new Size(1000, 680);
                lblTitle.Text = "LẬP HÓA ĐƠN BÁN HÀNG";
                lblSubTitle.Text = string.Format("Xác nhận thông tin và phát hành hóa đơn cho đơn đặt hàng [{0}]", _initialMaDDH);

                if (tcHoaDon.TabPages.Contains(tpDanhSach))
                {
                    tcHoaDon.TabPages.Remove(tpDanhSach);
                }
                tcHoaDon.Appearance = TabAppearance.FlatButtons;
                tcHoaDon.ItemSize = new Size(0, 1);
                tcHoaDon.SizeMode = TabSizeMode.Fixed;

                btnLamMoi.Visible = false;
            }
        }

        private void InitChiTietTable()
        {
            _dtChiTiet = new DataTable();
            _dtChiTiet.Columns.Add("MaSP", typeof(string));
            _dtChiTiet.Columns.Add("TenSP", typeof(string));
            _dtChiTiet.Columns.Add("DonViTinh", typeof(string));
            _dtChiTiet.Columns.Add("SoLuong", typeof(int));
            _dtChiTiet.Columns.Add("DonGia", typeof(decimal));
            _dtChiTiet.Columns.Add("GiamGia", typeof(decimal));
            _dtChiTiet.Columns.Add("ThanhTien", typeof(decimal));
        }

        private void frmHoaDonBan_Load(object sender, EventArgs e)
        {
            this.ApplyResponsiveUI();

            // Kiểm tra phân quyền truy cập form (Admin, Sales, Accountant)
            if (!SessionManager.IsAdmin() && !SessionManager.IsSales() && !SessionManager.IsAccountant())
            {
                MessageBox.Show("Bạn không có quyền truy cập vào chức năng Quản lý Hóa đơn bán hàng.", "Từ chối truy cập", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
                return;
            }

            if (SessionManager.CurrentUser != null)
            {
                lblNhanVienLap.Text = string.Format("{0} ({1})", SessionManager.CurrentUser.HoTen, SessionManager.CurrentUser.MaNV);
            }

            // Nếu là Kế toán: chỉ có quyền tra cứu/kiểm tra hóa đơn, không được lập mới
            if (SessionManager.IsAccountant())
            {
                btnLapHoaDon.Enabled = false;
                btnLapHoaDon.Text = "Lập Hóa Đơn (Chỉ xem)";
                cboDonDatHang.Enabled = false;
                txtGhiChu.ReadOnly = true;
                cboTrangThai.Enabled = false;
            }

            ResetForm();
            LoadDonDatHangChuaLapHoaDon();

            dtpFromDate.Value = DateTime.Today.AddDays(-30);
            dtpToDate.Value = DateTime.Today;
            UiComboBoxHelper.PopulateItems(cboTrangThaiLoc, InvoiceStatusConstants.FilterList, "-- Tất cả --");

            txtTimKiem.TextChanged += txtTimKiem_TextChanged;
            btnTimKiem.Click += btnTimKiem_Click;
            cboTrangThaiLoc.SelectedIndexChanged += cboTrangThaiLoc_SelectedIndexChanged;

            if (!string.IsNullOrEmpty(_initialMaDDH))
            {
                cboDonDatHang.Enabled = false;
            }
            else
            {
                LoadDanhSachHoaDon();

                if (SessionManager.IsAccountant())
                {
                    tcHoaDon.SelectedIndex = 1; // Mặc định mở Tab Danh sách hóa đơn
                }
                else
                {
                    tcHoaDon.SelectedIndex = 0; // Tab Lập hóa đơn
                }
            }
        }

        public void SelectTab(int index)
        {
            if (index >= 0 && index < tcHoaDon.TabPages.Count)
            {
                tcHoaDon.SelectedIndex = index;
                if (index == 1 && txtTimKiem != null && txtTimKiem.CanFocus)
                {
                    txtTimKiem.Focus();
                }
            }
        }

        private void LoadDonDatHangChuaLapHoaDon()
        {
            try
            {
                _isBinding = true;
                DataTable dtOrders = _hoaDonBanDAL.GetDonDatHangChuaLapHoaDon();

                DataTable dtCombo = dtOrders.Clone();
                dtCombo.Columns.Add("DisplayText", typeof(string));

                foreach (DataRow r in dtOrders.Rows)
                {
                    DataRow newR = dtCombo.NewRow();
                    newR.ItemArray = r.ItemArray;
                    string code = r["MaDDH"] != null ? r["MaDDH"].ToString().Trim() : "";
                    newR["MaDDH"] = code;
                    newR["DisplayText"] = string.Format("{0} - {1} ({2:N0} VNĐ)", code, r["TenKH"], r["TongTien"]);
                    dtCombo.Rows.Add(newR);
                }

                cboDonDatHang.DisplayMember = "DisplayText";
                cboDonDatHang.ValueMember = "MaDDH";
                cboDonDatHang.DataSource = dtCombo;
                UiComboBoxHelper.PopulateItems(cboTrangThai, InvoiceStatusConstants.All, InvoiceStatusConstants.ChuaThanhToan);

                if (!string.IsNullOrEmpty(_initialMaDDH))
                {
                    string target = _initialMaDDH.Trim();
                    int foundIndex = -1;
                    for (int i = 0; i < dtCombo.Rows.Count; i++)
                    {
                        if (string.Equals(dtCombo.Rows[i]["MaDDH"].ToString().Trim(), target, StringComparison.OrdinalIgnoreCase))
                        {
                            foundIndex = i;
                            break;
                        }
                    }

                    if (foundIndex >= 0)
                    {
                        cboDonDatHang.SelectedIndex = foundIndex;
                    }
                    else if (cboDonDatHang.Items.Count > 0)
                    {
                        cboDonDatHang.SelectedIndex = 0;
                    }
                }
                else if (cboDonDatHang.Items.Count > 0)
                {
                    cboDonDatHang.SelectedIndex = 0;
                }
                else
                {
                    lblKhachHangInfo.Text = "(Không có đơn đặt hàng nào đang chờ lập hóa đơn)";
                    lblKhachHangInfo.ForeColor = UiTheme.TextSecondary;
                    _dtChiTiet.Clear();
                    RecalculateGrandTotal();
                }
            }
            catch (Exception ex)
            {
                UiErrorHandler.Show(this, "FRMHOADONBAN_UI_ERROR", "Lỗi khi tải danh sách đơn đặt hàng.", ex);
            }
            finally
            {
                _isBinding = false;
                UpdateOrderSelection();
            }
        }

        private void ResetForm()
        {
            txtMaHDB.Text = _invoiceService.GenerateNewInvoiceId();
            dtpNgayLap.Value = DateTime.Now;
            cboTrangThai.SelectedIndex = 0; // "Chưa thanh toán"
            txtGhiChu.Clear();

            _dtChiTiet.Clear();
            dgvChiTiet.AutoGenerateColumns = false;
            dgvChiTiet.DataSource = _dtChiTiet;

            RecalculateGrandTotal();
        }

        private void UpdateOrderSelection()
        {
            if (_isBinding)
            {
                return;
            }

            string maDDH = null;
            DataRowView drv = cboDonDatHang.SelectedItem as DataRowView;
            if (drv != null && drv.Row.Table.Columns.Contains("MaDDH"))
            {
                maDDH = drv["MaDDH"].ToString().Trim();
            }
            else if (cboDonDatHang.SelectedValue != null && !(cboDonDatHang.SelectedValue is DataRowView))
            {
                maDDH = cboDonDatHang.SelectedValue.ToString().Trim();
            }

            if (string.IsNullOrEmpty(maDDH))
            {
                return;
            }

            try
            {
                DonDatHang order = _donDatHangDAL.GetById(maDDH);
                if (order != null)
                {
                    lblKhachHangInfo.Text = string.Format("{0} (Mã KH: {1})", order.TenKH, order.MaKH);
                    lblKhachHangInfo.ForeColor = UiTheme.Information;

                    // Nạp toàn bộ danh sách sản phẩm từ đơn đặt hàng sang bảng chi tiết hóa đơn
                    _dtChiTiet.Clear();
                    DataTable dtCT = _donDatHangDAL.GetChiTietDataTable(maDDH);
                    foreach (DataRow r in dtCT.Rows)
                    {
                        _dtChiTiet.ImportRow(r);
                    }

                    txtGhiChu.Text = string.Format("Hóa đơn bán lập cho đơn đặt hàng {0}", order.MaDDH);
                    RecalculateGrandTotal();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi cập nhật chọn đơn: " + ex.Message);
            }
        }

        private void RecalculateGrandTotal()
        {
            decimal tongTien = 0;
            foreach (DataRow row in _dtChiTiet.Rows)
            {
                if (row["ThanhTien"] != DBNull.Value)
                {
                    tongTien += Convert.ToDecimal(row["ThanhTien"]);
                }
            }

            lblTongTien.Text = string.Format("TỔNG HÓA ĐƠN: {0:N0} VNĐ", tongTien);
        }

        private void cboDonDatHang_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateOrderSelection();
        }

        private async void btnLapHoaDon_Click(object sender, EventArgs e)
        {
            if (!SessionManager.IsAdmin() && !SessionManager.IsSales())
            {
                MessageBox.Show("Bạn không có quyền lập hóa đơn bán (Kế toán chỉ có quyền tra cứu/kiểm tra hóa đơn).", "Từ chối quyền", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cboDonDatHang.SelectedValue == null || string.IsNullOrWhiteSpace(cboDonDatHang.SelectedValue.ToString()))
            {
                ShowValidation(cboDonDatHang, "Vui lòng chọn đơn đặt hàng để lập hóa đơn.");
                return;
            }

            if (_dtChiTiet.Rows.Count == 0)
            {
                ShowValidation(dgvChiTiet, "Đơn hàng được chọn không có chi tiết sản phẩm nào để lập hóa đơn.");
                return;
            }

            string maDDH = cboDonDatHang.SelectedValue.ToString().Trim();
            string maHDB = txtMaHDB.Text.Trim();
            string ghiChu = txtGhiChu.Text.Trim();
            string trangThai = cboTrangThai.SelectedItem != null ? cboTrangThai.SelectedItem.ToString() : InvoiceStatusConstants.ChuaThanhToan;

            string error = string.Empty;
            string createdMaHDB = string.Empty;
            bool success = await UiFeedbackHelper.RunBusyAsync(
                this,
                btnLapHoaDon,
                "ĐANG LẬP HÓA ĐƠN...",
                delegate { return _invoiceService.CreateInvoiceFromOrder(maDDH, maHDB, ghiChu, trangThai, out error, out createdMaHDB); });
            if (IsDisposed)
            {
                return;
            }

            if (success)
            {
                UiFeedbackHelper.ShowSuccess(
                    this,
                    string.Format("Lập Hóa đơn bán [{0}] thành công cho đơn đặt hàng [{1}]!\nTổng giá trị: {2}",
                        createdMaHDB, maDDH, lblTongTien.Text));

                if (this.Modal)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                    return;
                }

                ResetForm();
                LoadDonDatHangChuaLapHoaDon(); // Đơn vừa lập biến mất khỏi danh sách chờ
                LoadDanhSachHoaDon();
            }
            else
            {
                MessageBox.Show(error, "Lỗi lập hóa đơn bán", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void LoadDanhSachHoaDon()
        {
            await _searchDebouncer.RunNowAsync(
                delegate(int version) { return LoadDanhSachHoaDonAsync(version, null, false); });
        }

        private async Task LoadDanhSachHoaDonAsync(int requestVersion, Button actionButton, bool useFilters)
        {
            string keyword = txtTimKiem != null ? txtTimKiem.Text.Trim() : "";
            DateTime? from = (useFilters && dtpFromDate != null) ? dtpFromDate.Value.Date : (DateTime?)null;
            DateTime? to = (useFilters && dtpToDate != null) ? dtpToDate.Value.Date : (DateTime?)null;
            string trangThai = (useFilters && cboTrangThaiLoc != null && cboTrangThaiLoc.SelectedItem != null)
                ? cboTrangThaiLoc.SelectedItem.ToString()
                : null;

            UiStyler.SetGridLoading(dgvDanhSachHoaDon, "Đang tải danh sách hóa đơn...");
            try
            {
                PagedDataTable paged = null;
                await UiFeedbackHelper.RunBusyAsync(
                    this,
                    actionButton,
                    "ĐANG TẢI...",
                    delegate
                    {
                        paged = _hoaDonBanDAL.SearchPhanTrang(
                            keyword, from, to, trangThai,
                            _pagerHoaDon.CurrentPage, _pagerHoaDon.PageSize);
                        return true;
                    });

                if (IsDisposed || !_searchDebouncer.IsCurrent(requestVersion))
                {
                    return;
                }

                DataTable dt = paged != null ? paged.Table : null;
                dgvDanhSachHoaDon.AutoGenerateColumns = false;
                dgvDanhSachHoaDon.DataSource = dt;

                int total = paged != null ? paged.TotalRecords : 0;
                _pagerHoaDon.UpdateState(_pagerHoaDon.CurrentPage, _pagerHoaDon.PageSize, total);

                UiStyler.ClearGridState(dgvDanhSachHoaDon);
                UiStyler.UpdateGridEmptyState(dgvDanhSachHoaDon, "Không có hóa đơn nào phù hợp với bộ lọc.");
            }
            catch (Exception ex)
            {
                if (!_searchDebouncer.IsCurrent(requestVersion))
                {
                    return;
                }
                UiStyler.SetGridError(dgvDanhSachHoaDon, "Không thể tải danh sách hóa đơn.");
                UiErrorHandler.Show(this, "FRMHOADONBAN_UI_ERROR", "Lỗi nạp danh sách hóa đơn.", ex);
            }
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            _searchDebouncer.Restart(
                delegate(int version)
                {
                    _pagerHoaDon.Reset(_pagerHoaDon.PageSize);
                    return LoadDanhSachHoaDonAsync(version, null, true);
                });
        }

        private async void btnTimKiem_Click(object sender, EventArgs e)
        {
            _pagerHoaDon.Reset(_pagerHoaDon.PageSize);
            await _searchDebouncer.RunNowAsync(
                delegate(int version) { return LoadDanhSachHoaDonAsync(version, btnTimKiem, true); });
        }

        private void cboTrangThaiLoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            _searchDebouncer.Restart(
                delegate(int version)
                {
                    _pagerHoaDon.Reset(_pagerHoaDon.PageSize);
                    return LoadDanhSachHoaDonAsync(version, null, true);
                });
        }

        private async void btnLamMoiDanhSach_Click(object sender, EventArgs e)
        {
            if (txtTimKiem != null) txtTimKiem.Clear();
            if (cboTrangThaiLoc != null && cboTrangThaiLoc.Items.Count > 0) cboTrangThaiLoc.SelectedIndex = 0;
            if (dtpFromDate != null) dtpFromDate.Value = DateTime.Today.AddDays(-30);
            if (dtpToDate != null) dtpToDate.Value = DateTime.Today;
            _pagerHoaDon.Reset(_pagerHoaDon.PageSize);
            await _searchDebouncer.RunNowAsync(
                delegate(int version) { return LoadDanhSachHoaDonAsync(version, btnLamMoiDanhSach, false); });
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            ResetForm();
            LoadDonDatHangChuaLapHoaDon();
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            if (this.Modal)
            {
                this.DialogResult = DialogResult.Cancel;
            }
            this.Close();
        }

        private void dgvDanhSachHoaDon_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            string maHDB = dgvDanhSachHoaDon.Rows[e.RowIndex].Cells["colDSMaHDB"].Value.ToString().Trim();
            HoaDonBan hdb = _hoaDonBanDAL.GetById(maHDB);
            if (hdb != null)
            {
                txtMaHDB.Text = hdb.MaHDB;
                dtpNgayLap.Value = hdb.NgayLap;
                UiComboBoxHelper.SafeSelect(cboTrangThai, hdb.TrangThai);
                txtGhiChu.Text = hdb.GhiChu;
                lblKhachHangInfo.Text = string.Format("{0} (Mã KH: {1})", hdb.TenKH, hdb.MaKH);

                // Nạp chi tiết hóa đơn
                _dtChiTiet.Clear();
                DataTable dtCT = _hoaDonBanDAL.GetChiTietDataTable(maHDB);
                foreach (DataRow r in dtCT.Rows)
                {
                    _dtChiTiet.ImportRow(r);
                }

                RecalculateGrandTotal();
                tcHoaDon.SelectedIndex = 0; // Chuyển về Tab Lập hóa đơn để xem chi tiết
            }
        }

        private void ShowValidation(Control control, string message)
        {
            UiInteractionHelper.ShowValidationError(_validationErrors, control, message);
        }

        private void btnLapPhieuThuTuHDB_Click(object sender, EventArgs e)
        {
            if (dgvDanhSachHoaDon.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn một hóa đơn từ danh sách để lập phiếu thu.", "Chưa chọn hóa đơn", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!SessionManager.IsAdmin() && !SessionManager.IsAccountant())
            {
                MessageBox.Show("Chức năng Lập phiếu thu chỉ dành cho Quản trị viên và Kế toán.", "Từ chối quyền", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string maHDB = dgvDanhSachHoaDon.CurrentRow.Cells["colDSMaHDB"].Value.ToString().Trim();
            using (var frm = new frmPhieuThu(maHDB))
            {
                if (frm.ShowDialog(this) == DialogResult.OK)
                {
                    LoadDanhSachHoaDon();
                }
            }
        }

        private void btnLapPhieuXuatTuHDB_Click(object sender, EventArgs e)
        {
            if (dgvDanhSachHoaDon.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn một hóa đơn từ danh sách để lập phiếu xuất kho.", "Chưa chọn hóa đơn", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!SessionManager.IsAdmin() && !SessionManager.IsWarehouse())
            {
                MessageBox.Show("Chức năng Lập phiếu xuất kho chỉ dành cho Quản trị viên và Nhân viên kho.", "Từ chối quyền", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string maHDB = dgvDanhSachHoaDon.CurrentRow.Cells["colDSMaHDB"].Value.ToString().Trim();
            using (var frm = new frmPhieuXuatKho(maHDB))
            {
                if (frm.ShowDialog(this) == DialogResult.OK)
                {
                    LoadDanhSachHoaDon();
                }
            }
        }

        private void InHoaDonDangChon()
        {
            if (dgvDanhSachHoaDon.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn một hóa đơn cần in!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string maHDB = dgvDanhSachHoaDon.CurrentRow.Cells["colDSMaHDB"].Value != null
                ? dgvDanhSachHoaDon.CurrentRow.Cells["colDSMaHDB"].Value.ToString().Trim()
                : string.Empty;

            InHoaDon(maHDB);
        }

        private void InHoaDon(string maHDB)
        {
            if (string.IsNullOrEmpty(maHDB))
            {
                MessageBox.Show("Vui lòng chỉ định mã hóa đơn cần in!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                HoaDonBan hdb = _hoaDonBanDAL.GetById(maHDB);
                if (hdb == null)
                {
                    MessageBox.Show("Không tìm thấy dữ liệu hóa đơn " + maHDB, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DataTable dtChiTiet = _hoaDonBanDAL.GetChiTietDataTable(maHDB);
                string html = VoucherPrintHelper.GenerateHoaDonBanHtml(hdb, dtChiTiet);
                frmInChungTu.ShowVoucher(this, "Hóa Đơn Bán Hàng - " + maHDB, html, "HoaDonBan_" + maHDB);
            }
            catch (Exception ex)
            {
                AppLogger.Error("PRINT_HDB_ERROR", "Lỗi khi xuất mẫu in hóa đơn: " + ex.Message, ex);
                MessageBox.Show("Lỗi khi mở mẫu in hóa đơn: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
