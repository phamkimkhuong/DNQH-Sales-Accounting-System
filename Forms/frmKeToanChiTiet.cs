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
        private Label _khachHangStatus;
        private Label _sanPhamStatus;
        private List<SoChiTietKhachHangDTO> _currentKHList;
        private List<SoChiTietSanPhamDTO> _currentSPList;

        public frmKeToanChiTiet()
        {
            InitializeComponent();
            BuildResponsiveLayout();
            ApplyFoundationDesign();
            _reportingService = new ReportingService();
            _khachHangDAL = new KhachHangDAL();
            _sanPhamDAL = new SanPhamDAL();
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

            if (cboKhachHang.Items.Count > 0)
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

        private bool ValidateDateRange(DateTimePicker fromPicker, DateTimePicker toPicker, Label status)
        {
            if (fromPicker.Value.Date <= toPicker.Value.Date)
                return true;

            UiStyler.StyleStatusLabel(status, UiStatusKind.Warning, "Từ ngày không được lớn hơn đến ngày.");
            fromPicker.Focus();
            return false;
        }
    }
}
