using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows.Forms;
using DNQH_KeToanBanHang.DataAccess;
using DNQH_KeToanBanHang.Helpers;
using DNQH_KeToanBanHang.Models;
using DNQH_KeToanBanHang.Services;

namespace DNQH_KeToanBanHang.Forms
{
    public partial class frmKeToanChiTiet : Form
    {
        private readonly ReportingService _reportingService;
        private readonly KhachHangDAL _khachHangDAL;
        private readonly SanPhamDAL _sanPhamDAL;
        private readonly HoaDonBanDAL _hoaDonBanDAL;
        private Label _khachHangStatus;
        private Label _sanPhamStatus;
        private Label _hoaDonStatus;
        private List<SoChiTietKhachHangDTO> _currentKHList;
        private List<SoChiTietSanPhamDTO> _currentSPList;
        private SoChiTietHoaDonDTO _currentHDBDetail;

        public frmKeToanChiTiet()
        {
            InitializeComponent();
            BuildResponsiveLayout();
            ApplyFoundationDesign();
            _reportingService = new ReportingService();
            _khachHangDAL = new KhachHangDAL();
            _sanPhamDAL = new SanPhamDAL();
            _hoaDonBanDAL = new HoaDonBanDAL();
        }

        private async void frmKeToanChiTiet_Load(object sender, EventArgs e)
        {
            this.ApplyResponsiveUI();

            if (!SessionManager.IsAdmin() && !SessionManager.IsAccountant())
            {
                MessageBox.Show("Bạn không có quyền truy cập chức năng Kế Toán Chi Tiết!", "Cảnh báo phân quyền", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
                return;
            }

            InitDatePickers();
            InitComboBoxes();
            InitGridColumns();

            // Hook sự kiện chuyển tab để tự động nạp dữ liệu Tuổi nợ nếu chưa có
            tabControlMain.SelectedIndexChanged += async (s, ev) =>
            {
                if (tabControlMain.SelectedIndex == 3 && _currentTuoiNoTongHop == null)
                {
                    await LoadBaoCaoTuoiNoAsync(btnXemTuoiNo);
                }
            };

            // Tự động tải dữ liệu nếu có
            if (tabControlMain.SelectedIndex == 3)
            {
                await LoadBaoCaoTuoiNoAsync(btnXemTuoiNo);
            }
            else if (cboKhachHang.Items.Count > 0)
            {
                cboKhachHang.SelectedIndex = 0;
                await LoadSoKhachHangAsync(null);
            }
        }

        public void SelectTab(int index)
        {
            if (index >= 0 && index < tabControlMain.TabCount)
            {
                tabControlMain.SelectedIndex = index;
            }
        }

        private void InitDatePickers()
        {
            DateTime now = DateTime.Now;
            DateTime startOfMonth = new DateTime(now.Year, now.Month, 1);

            dtpTuNgayKH.Value = startOfMonth;
            dtpDenNgayKH.Value = now;

            dtpTuNgaySP.Value = startOfMonth;
            dtpDenNgaySP.Value = now;

            if (dtpNgayChot != null)
            {
                dtpNgayChot.Value = now;
            }
        }

        private void InitComboBoxes()
        {
            // 1. Khách hàng cho Tab 1
            try
            {
                DataTable dtKH = _khachHangDAL.GetAll();
                cboKhachHang.DataSource = dtKH;
                cboKhachHang.DisplayMember = "TenKH";
                cboKhachHang.ValueMember = "MaKH";
            }
            catch (Exception ex)
            {
                LogLookupError("LOAD_CUSTOMER_LOOKUP", "khách hàng", ex);
            }

            // 1.1 Khách hàng cho Tab 4 (Tuổi nợ)
            try
            {
                DataTable dtKHTuoiNo = _khachHangDAL.GetAll();
                DataRow emptyRow = dtKHTuoiNo.NewRow();
                emptyRow["MaKH"] = "";
                emptyRow["TenKH"] = "--- Tất cả khách hàng ---";
                dtKHTuoiNo.Rows.InsertAt(emptyRow, 0);

                if (cboKhachHangTuoiNo != null)
                {
                    cboKhachHangTuoiNo.DataSource = dtKHTuoiNo;
                    cboKhachHangTuoiNo.DisplayMember = "TenKH";
                    cboKhachHangTuoiNo.ValueMember = "MaKH";
                    cboKhachHangTuoiNo.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                LogLookupError("LOAD_CUSTOMER_AGING_LOOKUP", "khách hàng (tuổi nợ)", ex);
            }

            // 2. Sản phẩm cho Tab 2
            try
            {
                DataTable dtSP = _sanPhamDAL.GetAll();
                DataRow emptyRow = dtSP.NewRow();
                emptyRow["MaSP"] = "";
                emptyRow["TenSP"] = "--- Tất cả sản phẩm ---";
                dtSP.Rows.InsertAt(emptyRow, 0);

                cboSanPham.DataSource = dtSP;
                cboSanPham.DisplayMember = "TenSP";
                cboSanPham.ValueMember = "MaSP";
            }
            catch (Exception ex)
            {
                LogLookupError("LOAD_PRODUCT_LOOKUP", "sản phẩm", ex);
            }

            // 3. Hóa đơn bán cho Tab 3
            try
            {
                DataTable dtHDB = _hoaDonBanDAL.GetAll();
                cboHoaDon.Items.Clear();
                foreach (DataRow r in dtHDB.Rows)
                {
                    string maHDB = r["MaHDB"].ToString().Trim();
                    string tenKH = r["TenKH"] != DBNull.Value ? r["TenKH"].ToString().Trim() : "";
                    decimal tongTien = Convert.ToDecimal(r["TongTien"]);
                    cboHoaDon.Items.Add(string.Format("{0} - {1} ({2:N0} VNĐ)", maHDB, tenKH, tongTien));
                }
                if (cboHoaDon.Items.Count > 0)
                {
                    cboHoaDon.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                LogLookupError("LOAD_INVOICE_LOOKUP", "hóa đơn", ex);
            }
        }

        private void LogLookupError(string operation, string lookupName, Exception ex)
        {
            string correlationId = Guid.NewGuid().ToString("N");
            AppLogger.Error(operation, "Không thể tải danh mục " + lookupName + " cho sổ chi tiết.", ex,
                correlationId, SessionManager.CurrentUser != null ? SessionManager.CurrentUser.MaNV : "System");
            MessageBox.Show(
                string.Format("Không thể tải danh mục {0}. Mã tra cứu: {1}", lookupName, correlationId),
                "Lỗi dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        #region Tab 1: Sổ Khách Hàng

        private async void btnXemKH_Click(object sender, EventArgs e)
        {
            await LoadSoKhachHangAsync(btnXemKH);
        }

        private void btnXuatCsvKH_Click(object sender, EventArgs e)
        {
            string tenKH = cboKhachHang.Text;
            CsvExporter.ExportDataGridViewToExcel(dgvKhachHang, "SoChiTietKhachHang", "SỔ CHI TIẾT CÔNG NỢ KHÁCH HÀNG: " + tenKH);
        }

        private void btnInKH_Click(object sender, EventArgs e)
        {
            if (_currentKHList == null || _currentKHList.Count == 0)
            {
                UiFeedbackHelper.ShowToast(this, "Không có dữ liệu sổ chi tiết khách hàng để in. Vui lòng nhấn 'Tra Cứu' trước.", UiStatusKind.Warning, 3000);
                return;
            }

            string maKH = cboKhachHang.SelectedValue != null ? cboKhachHang.SelectedValue.ToString() : "";
            string tenKH = cboKhachHang.Text;
            DateTime fromDate = dtpTuNgayKH.Value;
            DateTime toDate = dtpDenNgayKH.Value;

            decimal tongNo = 0;
            decimal tongCo = 0;
            decimal duCuoiKy = 0;
            foreach (var item in _currentKHList)
            {
                tongNo += item.PhatSinhNo;
                tongCo += item.PhatSinhCo;
                duCuoiKy = item.SoDuCuoiKy;
            }

            string html = ReportPrintHelper.GenerateSoChiTietKhachHangHtml(maKH, tenKH, fromDate, toDate, _currentKHList, tongNo, tongCo, duCuoiKy);
            frmInChungTu.ShowVoucher(this, "Sổ Chi Tiết Công Nợ Khách Hàng", html,
                string.Format("SoChiTietKH_{0}_{1:yyyyMMdd}", maKH, DateTime.Now));
        }

        private async Task<bool> LoadSoKhachHangAsync(Button actionButton)
        {
            if (!ValidateDateRange(dtpTuNgayKH, dtpDenNgayKH, _khachHangStatus))
                return false;
            if (cboKhachHang.SelectedValue == null)
            {
                UiStyler.StyleStatusLabel(_khachHangStatus, UiStatusKind.Warning, "Vui lòng chọn khách hàng cần xem.");
                cboKhachHang.Focus();
                return false;
            }
            string maKH = cboKhachHang.SelectedValue.ToString();
            DateTime fromDate = dtpTuNgayKH.Value;
            DateTime toDate = dtpDenNgayKH.Value;

            string err = string.Empty;
            List<SoChiTietKhachHangDTO> list = null;
            UiStyler.SetGridLoading(dgvKhachHang, "Đang tải sổ chi tiết khách hàng...");
            UiStyler.StyleStatusLabel(_khachHangStatus, UiStatusKind.Information, "Đang tải dữ liệu công nợ khách hàng...");
            await UiFeedbackHelper.RunBusyAsync(
                this,
                actionButton,
                "ĐANG TẢI...",
                delegate
                {
                    list = _reportingService.GetSoChiTietKhachHang(maKH, fromDate, toDate, out err);
                    return true;
                });
            if (IsDisposed)
            {
                return false;
            }

            if (!string.IsNullOrEmpty(err))
            {
                UiStyler.StyleStatusLabel(_khachHangStatus, UiStatusKind.Error, err);
                UiStyler.SetGridError(dgvKhachHang, err);
                return false;
            }

            list = list ?? new List<SoChiTietKhachHangDTO>();
            _currentKHList = list;

            dgvKhachHang.Rows.Clear();
            decimal tongNo = 0;
            decimal tongCo = 0;
            decimal soDuCuoi = 0;

            foreach (var item in list)
            {
                tongNo += item.PhatSinhNo;
                tongCo += item.PhatSinhCo;
                soDuCuoi = item.SoDuCuoiKy;

                dgvKhachHang.Rows.Add(
                    item.NgayGiaoDich,
                    item.LoaiNghiepVu,
                    item.SoChungTu,
                    item.DienGiai,
                    item.PhatSinhNo,
                    item.PhatSinhCo,
                    item.SoDuCuoiKy
                );
            }

            lblKHTongPhatSinhNo.Text = string.Format("Tổng phát sinh nợ: {0:N0} VNĐ", tongNo);
            lblKHTongPhatSinhCo.Text = string.Format("Tổng đã thanh toán: {0:N0} VNĐ", tongCo);
            lblKHSoDuCuoiKy.Text = string.Format("Công nợ còn lại: {0:N0} VNĐ", soDuCuoi);
            UiStyler.StyleStatusLabel(
                _khachHangStatus,
                list.Count == 0 ? UiStatusKind.Warning : UiStatusKind.Neutral,
                list.Count == 0 ? "Không có phát sinh công nợ trong khoảng thời gian đã chọn." : string.Format("Đang hiển thị {0:N0} giao dịch công nợ.", list.Count));
            UiStyler.ClearGridState(dgvKhachHang);
            UiStyler.UpdateGridEmptyState(dgvKhachHang, "Không có phát sinh công nợ trong khoảng thời gian đã chọn.");
            return true;
        }

        #endregion

        #region Tab 2: Sổ Sản Phẩm

        private async void btnXemSP_Click(object sender, EventArgs e)
        {
            await LoadSoSanPhamAsync(btnXemSP);
        }

        private void btnXuatCsvSP_Click(object sender, EventArgs e)
        {
            CsvExporter.ExportDataGridViewToExcel(dgvSanPham, "SoChiTietSanPham", "SỔ BÁN HÀNG THEO SẢN PHẨM");
        }

        private void btnInSP_Click(object sender, EventArgs e)
        {
            if (_currentSPList == null || _currentSPList.Count == 0)
            {
                UiFeedbackHelper.ShowToast(this, "Không có dữ liệu sổ bán hàng sản phẩm để in. Vui lòng nhấn 'Tra Cứu' trước.", UiStatusKind.Warning, 3000);
                return;
            }

            string maSP = cboSanPham.SelectedValue != null ? cboSanPham.SelectedValue.ToString() : "";
            string tenSP = cboSanPham.Text;
            if (string.IsNullOrWhiteSpace(tenSP) || cboSanPham.SelectedIndex <= 0)
            {
                tenSP = "Tất cả các sản phẩm";
            }
            DateTime fromDate = dtpTuNgaySP.Value;
            DateTime toDate = dtpDenNgaySP.Value;

            int tongSoLuong = 0;
            decimal tongDoanhThu = 0;
            foreach (var item in _currentSPList)
            {
                tongSoLuong += item.TongSoLuongBan;
                tongDoanhThu += item.TongDoanhThu;
            }

            string html = ReportPrintHelper.GenerateSoChiTietSanPhamHtml(maSP, tenSP, fromDate, toDate, _currentSPList, tongSoLuong, tongDoanhThu);
            frmInChungTu.ShowVoucher(this, "Sổ Chi Tiết Bán Hàng Sản Phẩm", html,
                string.Format("SoBanHangSP_{0:yyyyMMdd}", DateTime.Now));
        }

        private async Task<bool> LoadSoSanPhamAsync(Button actionButton)
        {
            if (!ValidateDateRange(dtpTuNgaySP, dtpDenNgaySP, _sanPhamStatus))
                return false;
            string maSP = cboSanPham.SelectedValue != null ? cboSanPham.SelectedValue.ToString() : null;
            DateTime fromDate = dtpTuNgaySP.Value;
            DateTime toDate = dtpDenNgaySP.Value;

            string err = string.Empty;
            List<SoChiTietSanPhamDTO> list = null;
            UiStyler.SetGridLoading(dgvSanPham, "Đang tải sổ chi tiết sản phẩm...");
            UiStyler.StyleStatusLabel(_sanPhamStatus, UiStatusKind.Information, "Đang tải dữ liệu bán hàng theo sản phẩm...");
            await UiFeedbackHelper.RunBusyAsync(
                this,
                actionButton,
                "ĐANG TẢI...",
                delegate
                {
                    list = _reportingService.GetSoChiTietSanPham(maSP, fromDate, toDate, out err);
                    return true;
                });
            if (IsDisposed)
            {
                return false;
            }

            if (!string.IsNullOrEmpty(err))
            {
                UiStyler.StyleStatusLabel(_sanPhamStatus, UiStatusKind.Error, err);
                UiStyler.SetGridError(dgvSanPham, err);
                return false;
            }

            list = list ?? new List<SoChiTietSanPhamDTO>();
            _currentSPList = list;

            dgvSanPham.Rows.Clear();
            int tongSL = 0;
            decimal tongDT = 0;

            foreach (var item in list)
            {
                tongSL += item.TongSoLuongBan;
                tongDT += item.TongDoanhThu;

                dgvSanPham.Rows.Add(
                    item.MaSP,
                    item.TenSP,
                    item.TenLoaiSP,
                    item.DonViTinh,
                    item.TongSoLuongBan,
                    item.DonGiaTrungBinh,
                    item.TongDoanhThu,
                    item.SoHoaDonPhatSinh
                );
            }

            lblSPTongSoLuong.Text = string.Format("Tổng số lượng bán: {0:N0} SP", tongSL);
            lblSPTongDoanhThu.Text = string.Format("Tổng doanh số bán: {0:N0} VNĐ", tongDT);
            UiStyler.StyleStatusLabel(
                _sanPhamStatus,
                list.Count == 0 ? UiStatusKind.Warning : UiStatusKind.Neutral,
                list.Count == 0 ? "Không có doanh số sản phẩm trong khoảng thời gian đã chọn." : string.Format("Đang hiển thị {0:N0} sản phẩm.", list.Count));
            UiStyler.ClearGridState(dgvSanPham);
            UiStyler.UpdateGridEmptyState(dgvSanPham, "Không có doanh số sản phẩm trong khoảng thời gian đã chọn.");
            return true;
        }

        #endregion

        #region Tab 3: Sổ Hóa Đơn & Chứng Từ

        private async void btnXemHDB_Click(object sender, EventArgs e)
        {
            await LoadSoHoaDonAsync(btnXemHDB);
        }

        private void btnXuatCsvHDB_Click(object sender, EventArgs e)
        {
            CsvExporter.ExportDataGridViewToExcel(dgvMatHang, "ChiTietHoaDon", "CHI TIẾT MẶT HÀNG HÓA ĐƠN BÁN " + lblHDBMa.Text);
        }

        private void btnInHDB_Click(object sender, EventArgs e)
        {
            if (_currentHDBDetail == null)
            {
                UiFeedbackHelper.ShowToast(this, "Không có thông tin hóa đơn để in. Vui lòng nhấn 'Xem Chi Tiết' trước.", UiStatusKind.Warning, 3000);
                return;
            }

            string html = ReportPrintHelper.GenerateSoChiTietHoaDonHtml(_currentHDBDetail);
            frmInChungTu.ShowVoucher(this, "Hồ Sơ Luân Chuyển Hóa Đơn & Chứng Từ", html,
                string.Format("HoSoHoaDon_{0}_{1:yyyyMMdd}", _currentHDBDetail.MaHDB, DateTime.Now));
        }

        private async Task<bool> LoadSoHoaDonAsync(Button actionButton)
        {
            if (cboHoaDon.SelectedItem == null)
            {
                UiStyler.StyleStatusLabel(_hoaDonStatus, UiStatusKind.Warning, "Vui lòng chọn hóa đơn cần xem chi tiết.");
                cboHoaDon.Focus();
                return false;
            }
            string selectedText = cboHoaDon.SelectedItem.ToString();
            string maHDB = selectedText.Split('-')[0].Trim();

            string err = string.Empty;
            SoChiTietHoaDonDTO hdb = null;
            SetInvoiceGridsLoading("Đang tải chi tiết hóa đơn...");
            UiStyler.StyleStatusLabel(_hoaDonStatus, UiStatusKind.Information, "Đang tải mặt hàng, phiếu thu và định khoản...");
            await UiFeedbackHelper.RunBusyAsync(
                this,
                actionButton,
                "ĐANG TẢI...",
                delegate
                {
                    hdb = _reportingService.GetSoChiTietHoaDon(maHDB, out err);
                    return true;
                });
            if (IsDisposed)
            {
                return false;
            }

            if (!string.IsNullOrEmpty(err))
            {
                UiStyler.StyleStatusLabel(_hoaDonStatus, UiStatusKind.Error, err);
                SetInvoiceGridsError(err);
                return false;
            }

            if (hdb == null)
            {
                UiStyler.StyleStatusLabel(_hoaDonStatus, UiStatusKind.Warning, "Không tìm thấy thông tin chi tiết của hóa đơn đã chọn.");
                ClearInvoiceGridStates();
                UpdateInvoiceGridEmptyStates();
                return false;
            }

            _currentHDBDetail = hdb;

            // Cập nhật Header Panel
            lblHDBMa.Text = string.Format("Mã HĐ: {0}", hdb.MaHDB);
            lblHDBNgay.Text = string.Format("Ngày lập: {0:dd/MM/yyyy}", hdb.NgayLap);
            lblHDBKhachHang.Text = string.Format("Khách hàng: {0} ({1})", hdb.TenKH, hdb.DienThoai);
            lblHDBTongTien.Text = string.Format("Tổng tiền HĐ: {0:N0} VNĐ", hdb.TongTien);
            lblHDBDaThu.Text = string.Format("Đã thu: {0:N0} VNĐ", hdb.DaThu);
            lblHDBConLai.Text = string.Format("Còn nợ: {0:N0} VNĐ", hdb.ConLai);
            lblHDBTrangThai.Text = string.Format("Trạng thái: {0}", hdb.TrangThai);

            // Nạp chi tiết mặt hàng
            dgvMatHang.Rows.Clear();
            foreach (var item in hdb.DanhSachMatHang)
            {
                dgvMatHang.Rows.Add(
                    item.MaSP,
                    item.TenSP,
                    item.DonViTinh,
                    item.SoLuong,
                    item.DonGia,
                    item.GiamGia,
                    item.ThanhTien
                );
            }

            // Nạp danh sách phiếu thu
            dgvPhieuThu.Rows.Clear();
            foreach (var pt in hdb.DanhSachPhieuThu)
            {
                dgvPhieuThu.Rows.Add(
                    pt.MaPT,
                    pt.NgayThu,
                    pt.NguoiNop,
                    pt.SoTien,
                    pt.HinhThuc,
                    pt.LyDoThu
                );
            }

            // Nạp danh sách định khoản chứng từ
            dgvDinhKhoan.Rows.Clear();
            foreach (var dk in hdb.DanhSachDinhKhoan)
            {
                dgvDinhKhoan.Rows.Add(
                    dk.MaCT,
                    dk.NgayLap,
                    dk.STT,
                    dk.TaiKhoanNo,
                    dk.TaiKhoanCo,
                    dk.SoTien,
                    dk.DienGiai
                );
            }
            UiStyler.StyleStatusLabel(
                _hoaDonStatus,
                UiStatusKind.Neutral,
                string.Format("Đã nạp {0:N0} mặt hàng, {1:N0} phiếu thu và {2:N0} bút toán.",
                    hdb.DanhSachMatHang.Count, hdb.DanhSachPhieuThu.Count, hdb.DanhSachDinhKhoan.Count));
            ClearInvoiceGridStates();
            UpdateInvoiceGridEmptyStates();
            return true;
        }

        private void SetInvoiceGridsLoading(string message)
        {
            UiStyler.SetGridLoading(dgvMatHang, message);
            UiStyler.SetGridLoading(dgvPhieuThu, message);
            UiStyler.SetGridLoading(dgvDinhKhoan, message);
        }

        private void SetInvoiceGridsError(string message)
        {
            UiStyler.SetGridError(dgvMatHang, message);
            UiStyler.SetGridError(dgvPhieuThu, message);
            UiStyler.SetGridError(dgvDinhKhoan, message);
        }

        private void ClearInvoiceGridStates()
        {
            UiStyler.ClearGridState(dgvMatHang);
            UiStyler.ClearGridState(dgvPhieuThu);
            UiStyler.ClearGridState(dgvDinhKhoan);
        }

        private void UpdateInvoiceGridEmptyStates()
        {
            UiStyler.UpdateGridEmptyState(dgvMatHang, "Hóa đơn không có mặt hàng.");
            UiStyler.UpdateGridEmptyState(dgvPhieuThu, "Hóa đơn chưa có phiếu thu.");
            UiStyler.UpdateGridEmptyState(dgvDinhKhoan, "Hóa đơn chưa có định khoản.");
        }

        private bool ValidateDateRange(DateTimePicker fromPicker, DateTimePicker toPicker, Label status)
        {
            if (fromPicker.Value.Date <= toPicker.Value.Date)
                return true;

            UiStyler.StyleStatusLabel(status, UiStatusKind.Warning, "Từ ngày không được lớn hơn đến ngày.");
            fromPicker.Focus();
            return false;
        }

        #endregion
    }
}
