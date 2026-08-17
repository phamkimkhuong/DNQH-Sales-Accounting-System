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
    public partial class frmBaoCaoTongHop : Form
    {
        private readonly ReportingService _reportingService;
        private readonly KhoDAL _khoDAL;
        private Label _doanhThuStatus;
        private Label _thuChiStatus;
        private Label _tonKhoStatus;
        private List<BaoCaoDoanhThuDTO> _currentDoanhThuList;
        private List<BaoCaoThuChiDTO> _currentThuChiList;
        private List<BaoCaoTonKhoDTO> _currentTonKhoList;

        public frmBaoCaoTongHop()
        {
            InitializeComponent();
            BuildResponsiveLayout();
            ApplyFoundationDesign();
            _reportingService = new ReportingService();
            _khoDAL = new KhoDAL();
        }

        private async void frmBaoCaoTongHop_Load(object sender, EventArgs e)
        {
            this.ApplyResponsiveUI();

            if (!SessionManager.IsAdmin() && !SessionManager.IsAccountant())
            {
                MessageBox.Show("Bạn không có quyền truy cập chức năng Báo Cáo & Thống Kê Tổng Hợp!", "Cảnh báo phân quyền", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
                return;
            }

            InitDatePickers();
            InitComboBoxKho();
            InitGridColumns();
            InitializeChartComponents();

            // Hook chuyển tab để tự động nạp Biểu đồ khi chọn Tab 4
            tabControlMain.SelectedIndexChanged += async (s, ev) =>
            {
                if (tabControlMain.SelectedIndex == 3)
                {
                    await LoadChartsAsync(btnXemBieuDo);
                }
            };

            // Tải dữ liệu mặc định
            await RefreshKpiBannerAsync(dtpTuNgayDT.Value, dtpDenNgayDT.Value);
            await LoadDoanhThuAsync(null);
            await LoadThuChiAsync(null);
            await LoadTonKhoAsync(null);

            if (tabControlMain.SelectedIndex == 3)
            {
                await LoadChartsAsync(btnXemBieuDo);
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

            dtpTuNgayDT.Value = startOfMonth;
            dtpDenNgayDT.Value = now;

            dtpTuNgayTC.Value = startOfMonth;
            dtpDenNgayTC.Value = now;
        }

        private void InitComboBoxKho()
        {
            try
            {
                DataTable dtKho = _khoDAL.GetAll();
                DataRow emptyRow = dtKho.NewRow();
                emptyRow["MaKho"] = "";
                emptyRow["TenKho"] = "--- Tất cả kho hàng ---";
                dtKho.Rows.InsertAt(emptyRow, 0);

                cboKho.DisplayMember = "TenKho";
                cboKho.ValueMember = "MaKho";
                cboKho.DataSource = dtKho;
            }
            catch (Exception ex)
            {
                string correlationId = Guid.NewGuid().ToString("N");
                AppLogger.Error("LOAD_WAREHOUSE_LOOKUP", "Không thể tải danh sách kho cho báo cáo.", ex,
                    correlationId, SessionManager.CurrentUser != null ? SessionManager.CurrentUser.MaNV : "System");
                MessageBox.Show(
                    string.Format("Không thể tải danh sách kho. Mã tra cứu: {0}", correlationId),
                    "Lỗi dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                UiStyler.StyleStatusLabel(_tonKhoStatus, UiStatusKind.Error, "Không thể tải danh sách kho. Mã tra cứu: " + correlationId);
            }
        }

        private async Task RefreshKpiBannerAsync(DateTime fromDate, DateTime toDate)
        {
            string err = string.Empty;
            TongHopKpiDTO kpi = null;
            await UiFeedbackHelper.RunBusyAsync(
                this,
                null,
                null,
                delegate
                {
                    kpi = _reportingService.GetTongHopKpi(fromDate, toDate, out err);
                    return true;
                });
            if (IsDisposed)
            {
                return;
            }

            if (string.IsNullOrEmpty(err) && kpi != null)
            {
                lblKpi1Value.Text = string.Format("{0:N0} VNĐ", kpi.TongDoanhThu);
                lblKpi2Value.Text = string.Format("{0:N0} VNĐ", kpi.TongThu);
                lblKpi3Value.Text = string.Format("{0:N0} VNĐ", kpi.TongChi);
                lblKpi4Value.Text = string.Format("{0:N0} VNĐ", kpi.ChenhLechThuChi);
                lblKpi5Value.Text = string.Format("{0:N0} VNĐ", kpi.TongGiaTriTonKho);
            }
        }

        #region Tab 1: Doanh Thu

        private async void btnXemDoanhThu_Click(object sender, EventArgs e)
        {
            if (await LoadDoanhThuAsync(btnXemDoanhThu))
            {
                await RefreshKpiBannerAsync(dtpTuNgayDT.Value, dtpDenNgayDT.Value);
            }
        }

        private void btnXuatCsvDoanhThu_Click(object sender, EventArgs e)
        {
            CsvExporter.ExportDataGridViewToExcel(dgvDoanhThu, "BaoCaoDoanhThu", "BÁO CÁO DOANH THU BÁN HÀNG");
        }

        private async void btnInDoanhThu_Click(object sender, EventArgs e)
        {
            if (_currentDoanhThuList == null)
            {
                if (!await LoadDoanhThuAsync(btnInDoanhThu))
                    return;
            }
            decimal tongDoanhThu = 0;
            if (_currentDoanhThuList != null)
            {
                foreach (var item in _currentDoanhThuList)
                    tongDoanhThu += item.TongTien;
            }
            string html = ReportPrintHelper.GenerateBaoCaoDoanhThuHtml(
                dtpTuNgayDT.Value, dtpDenNgayDT.Value, _currentDoanhThuList, tongDoanhThu, _currentDoanhThuList != null ? _currentDoanhThuList.Count : 0);
            frmInChungTu.ShowVoucher(this, "Báo Cáo Doanh Thu Bán Hàng", html,
                string.Format("BaoCaoDoanhThu_{0:yyyyMMdd}_{1:yyyyMMdd}", dtpTuNgayDT.Value, dtpDenNgayDT.Value));
        }

        private async Task<bool> LoadDoanhThuAsync(Button actionButton)
        {
            if (!ValidateDateRange(dtpTuNgayDT, dtpDenNgayDT, _doanhThuStatus))
                return false;

            DateTime fromDate = dtpTuNgayDT.Value;
            DateTime toDate = dtpDenNgayDT.Value;
            string err = string.Empty;
            List<BaoCaoDoanhThuDTO> list = null;
            UiStyler.SetGridLoading(dgvDoanhThu, "Đang tải báo cáo doanh thu...");
            UiStyler.StyleStatusLabel(_doanhThuStatus, UiStatusKind.Information, "Đang tải dữ liệu doanh thu...");
            await UiFeedbackHelper.RunBusyAsync(
                this,
                actionButton,
                "ĐANG TẢI...",
                delegate
                {
                    list = _reportingService.GetBaoCaoDoanhThu(fromDate, toDate, out err);
                    return true;
                });
            if (IsDisposed)
            {
                return false;
            }

            if (!string.IsNullOrEmpty(err))
            {
                UiStyler.StyleStatusLabel(_doanhThuStatus, UiStatusKind.Error, err);
                UiStyler.SetGridError(dgvDoanhThu, err);
                return false;
            }

            list = list ?? new List<BaoCaoDoanhThuDTO>();
            _currentDoanhThuList = list;

            dgvDoanhThu.Rows.Clear();
            decimal tongDoanhThu = 0;

            foreach (var item in list)
            {
                tongDoanhThu += item.TongTien;

                dgvDoanhThu.Rows.Add(
                    item.MaHDB,
                    item.NgayLap,
                    item.TenKH,
                    item.TenNV,
                    item.TongTien,
                    item.TrangThai,
                    item.SoMatHang,
                    item.GhiChu
                );
            }

            lblDTTongDoanhThu.Text = string.Format("Tổng Doanh Thu: {0:N0} VNĐ", tongDoanhThu);
            lblDTSoHoaDon.Text = string.Format("Số lượng hóa đơn: {0:N0} HĐ", list.Count);
            UiStyler.StyleStatusLabel(
                _doanhThuStatus,
                list.Count == 0 ? UiStatusKind.Warning : UiStatusKind.Neutral,
                list.Count == 0 ? "Không có doanh thu trong khoảng thời gian đã chọn." : string.Format("Đang hiển thị {0:N0} hóa đơn.", list.Count));
            UiStyler.ClearGridState(dgvDoanhThu);
            UiStyler.UpdateGridEmptyState(dgvDoanhThu, "Không có doanh thu trong khoảng thời gian đã chọn.");
            return true;
        }

        #endregion

        #region Tab 2: Thu Chi

        private async void btnXemThuChi_Click(object sender, EventArgs e)
        {
            if (await LoadThuChiAsync(btnXemThuChi))
            {
                await RefreshKpiBannerAsync(dtpTuNgayTC.Value, dtpDenNgayTC.Value);
            }
        }

        private void btnXuatCsvThuChi_Click(object sender, EventArgs e)
        {
            CsvExporter.ExportDataGridViewToExcel(dgvThuChi, "BaoCaoThuChi", "BÁO CÁO TỔNG HỢP THU - CHI (SỔ QUỸ)");
        }

        private async void btnInThuChi_Click(object sender, EventArgs e)
        {
            if (_currentThuChiList == null)
            {
                if (!await LoadThuChiAsync(btnInThuChi))
                    return;
            }
            decimal tongThu = 0;
            decimal tongChi = 0;
            if (_currentThuChiList != null)
            {
                foreach (var item in _currentThuChiList)
                {
                    tongThu += item.SoTienThu;
                    tongChi += item.SoTienChi;
                }
            }
            decimal chenhLech = tongThu - tongChi;
            string html = ReportPrintHelper.GenerateBaoCaoThuChiHtml(
                dtpTuNgayTC.Value, dtpDenNgayTC.Value, _currentThuChiList, tongThu, tongChi, chenhLech);
            frmInChungTu.ShowVoucher(this, "Báo Cáo Tổng Hợp Thu - Chi", html,
                string.Format("BaoCaoThuChi_{0:yyyyMMdd}_{1:yyyyMMdd}", dtpTuNgayTC.Value, dtpDenNgayTC.Value));
        }

        private async Task<bool> LoadThuChiAsync(Button actionButton)
        {
            if (!ValidateDateRange(dtpTuNgayTC, dtpDenNgayTC, _thuChiStatus))
                return false;

            DateTime fromDate = dtpTuNgayTC.Value;
            DateTime toDate = dtpDenNgayTC.Value;
            string err = string.Empty;
            List<BaoCaoThuChiDTO> list = null;
            UiStyler.SetGridLoading(dgvThuChi, "Đang tải báo cáo thu chi...");
            UiStyler.StyleStatusLabel(_thuChiStatus, UiStatusKind.Information, "Đang tải dữ liệu thu chi...");
            await UiFeedbackHelper.RunBusyAsync(
                this,
                actionButton,
                "ĐANG TẢI...",
                delegate
                {
                    list = _reportingService.GetBaoCaoThuChi(fromDate, toDate, out err);
                    return true;
                });
            if (IsDisposed)
            {
                return false;
            }

            if (!string.IsNullOrEmpty(err))
            {
                UiStyler.StyleStatusLabel(_thuChiStatus, UiStatusKind.Error, err);
                UiStyler.SetGridError(dgvThuChi, err);
                return false;
            }

            list = list ?? new List<BaoCaoThuChiDTO>();
            _currentThuChiList = list;

            dgvThuChi.Rows.Clear();
            decimal tongThu = 0;
            decimal tongChi = 0;

            foreach (var item in list)
            {
                tongThu += item.SoTienThu;
                tongChi += item.SoTienChi;

                dgvThuChi.Rows.Add(
                    item.NgayGiaoDich,
                    item.LoaiGiaoDich,
                    item.MaChungTu,
                    item.NguoiGiaoDich,
                    item.SoTienThu,
                    item.SoTienChi,
                    item.HinhThuc,
                    item.LyDo,
                    item.TenNV,
                    item.MaHDBLienKet
                );
            }

            lblTCTongThu.Text = string.Format("Tổng Thu Kỳ: {0:N0} VNĐ", tongThu);
            lblTCTongChi.Text = string.Format("Tổng Chi Kỳ: {0:N0} VNĐ", tongChi);
            lblTCChenhLech.Text = string.Format("Chênh Lệch Thu - Chi: {0:N0} VNĐ", tongThu - tongChi);
            UiStyler.StyleStatusLabel(
                _thuChiStatus,
                list.Count == 0 ? UiStatusKind.Warning : UiStatusKind.Neutral,
                list.Count == 0 ? "Không có giao dịch thu chi trong khoảng thời gian đã chọn." : string.Format("Đang hiển thị {0:N0} giao dịch thu chi.", list.Count));
            UiStyler.ClearGridState(dgvThuChi);
            UiStyler.UpdateGridEmptyState(dgvThuChi, "Không có giao dịch thu chi trong khoảng thời gian đã chọn.");
            return true;
        }

        #endregion

        #region Tab 3: Tồn Kho

        private async void btnXemTonKho_Click(object sender, EventArgs e)
        {
            await LoadTonKhoAsync(btnXemTonKho);
        }

        private void btnXuatCsvTonKho_Click(object sender, EventArgs e)
        {
            CsvExporter.ExportDataGridViewToExcel(dgvTonKho, "BaoCaoTonKho", "BÁO CÁO TỔNG HỢP TỒN KHO HIỆN TẠI");
        }

        private void btnInTonKho_Click(object sender, EventArgs e)
        {
            if (_currentTonKhoList == null || _currentTonKhoList.Count == 0)
            {
                UiFeedbackHelper.ShowToast(this, "Không có dữ liệu tồn kho để in. Vui lòng nhấn 'Lọc Dữ Liệu' trước.", UiStatusKind.Warning, 3000);
                return;
            }

            string tenKho = cboKho.Text;
            if (string.IsNullOrWhiteSpace(tenKho) || cboKho.SelectedIndex <= 0)
            {
                tenKho = "Tất cả các kho";
            }

            int tongSoLuong = 0;
            decimal tongGiaTri = 0;
            foreach (var item in _currentTonKhoList)
            {
                tongSoLuong += item.SoLuongTon;
                tongGiaTri += item.GiaTriTon;
            }

            string html = ReportPrintHelper.GenerateBaoCaoTonKhoHtml(tenKho, _currentTonKhoList, tongSoLuong, tongGiaTri);
            frmInChungTu.ShowVoucher(this, "Báo Cáo Tồn Kho Hiện Tại", html, "BaoCaoTonKho_" + DateTime.Now.ToString("yyyyMMdd"));
        }

        private async Task<bool> LoadTonKhoAsync(Button actionButton)
        {
            string maKho = cboKho.SelectedValue != null ? cboKho.SelectedValue.ToString() : null;

            string err = string.Empty;
            List<BaoCaoTonKhoDTO> list = null;
            UiStyler.SetGridLoading(dgvTonKho, "Đang tải báo cáo tồn kho...");
            UiStyler.StyleStatusLabel(_tonKhoStatus, UiStatusKind.Information, "Đang tải dữ liệu tồn kho...");
            await UiFeedbackHelper.RunBusyAsync(
                this,
                actionButton,
                "ĐANG TẢI...",
                delegate
                {
                    list = _reportingService.GetBaoCaoTonKho(out err, maKho);
                    return true;
                });
            if (IsDisposed)
            {
                return false;
            }

            if (!string.IsNullOrEmpty(err))
            {
                UiStyler.StyleStatusLabel(_tonKhoStatus, UiStatusKind.Error, err);
                UiStyler.SetGridError(dgvTonKho, err);
                return false;
            }

            list = list ?? new List<BaoCaoTonKhoDTO>();
            _currentTonKhoList = list;

            dgvTonKho.Rows.Clear();
            int tongTon = 0;
            decimal tongGiaTri = 0;

            foreach (var item in list)
            {
                tongTon += item.SoLuongTon;
                tongGiaTri += item.GiaTriTon;

                dgvTonKho.Rows.Add(
                    item.TenKho,
                    item.MaSP,
                    item.TenSP,
                    item.TenLoaiSP,
                    item.DonViTinh,
                    item.SoLuongTon,
                    item.DonGia,
                    item.GiaTriTon,
                    item.NgayCapNhat
                );
            }

            lblTKTongTon.Text = string.Format("Tổng số lượng tồn: {0:N0} sản phẩm", tongTon);
            lblTKTongGiaTri.Text = string.Format("Tổng giá trị tồn kho: {0:N0} VNĐ", tongGiaTri);
            UiStyler.StyleStatusLabel(
                _tonKhoStatus,
                list.Count == 0 ? UiStatusKind.Warning : UiStatusKind.Neutral,
                list.Count == 0 ? "Không có dữ liệu tồn kho phù hợp." : string.Format("Đang hiển thị {0:N0} dòng tồn kho.", list.Count));
            UiStyler.ClearGridState(dgvTonKho);
            UiStyler.UpdateGridEmptyState(dgvTonKho, "Không có dữ liệu tồn kho phù hợp.");
            return true;
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
