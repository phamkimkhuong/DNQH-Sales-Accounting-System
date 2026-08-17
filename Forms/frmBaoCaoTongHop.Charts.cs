using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using DNQH_KeToanBanHang.Helpers;
using DNQH_KeToanBanHang.Models;

namespace DNQH_KeToanBanHang.Forms
{
    public partial class frmBaoCaoTongHop
    {
        private TabPage tabBieuDo;
        private Panel pnlFilterBD;
        private Label lblTuNgayBD;
        private DateTimePicker dtpTuNgayBD;
        private Label lblDenNgayBD;
        private DateTimePicker dtpDenNgayBD;
        private Button btnXemBieuDo;
        private Button btnXuatAnhBieuDo;
        private Label _bieuDoStatus;

        private Chart chartDoanhThu;
        private Chart chartCoCau;
        private Chart chartThuChi;

        private bool _isLoadingCharts;
        private bool _chartsInitialized;

        private void InitializeChartComponents()
        {
            if (_chartsInitialized) return;

            tabBieuDo = new TabPage
            {
                Text = "4. Biểu Đồ & Trực Quan",
                Padding = new Padding(4),
                BackColor = UiTheme.Canvas
            };

            // Thanh lọc thời gian
            pnlFilterBD = new Panel
            {
                Dock = DockStyle.Top,
                Height = 48,
                Padding = new Padding(8, 6, 8, 6),
                BackColor = Color.White
            };

            lblTuNgayBD = new Label { Text = "Từ ngày:", AutoSize = true, Margin = new Padding(0, 7, 4, 0) };
            dtpTuNgayBD = new DateTimePicker { Format = DateTimePickerFormat.Short, Width = 110 };

            lblDenNgayBD = new Label { Text = "Đến ngày:", AutoSize = true, Margin = new Padding(8, 7, 4, 0) };
            dtpDenNgayBD = new DateTimePicker { Format = DateTimePickerFormat.Short, Width = 110 };

            DateTime now = DateTime.Now;
            dtpTuNgayBD.Value = new DateTime(now.Year, now.Month, 1);
            dtpDenNgayBD.Value = now;

            btnXemBieuDo = new Button { Text = "Tra Cứu", Width = 90 };
            btnXemBieuDo.Click += async delegate { await LoadChartsAsync(btnXemBieuDo); };

            Button btnQuick7D = CreateQuickFilterButton("7 Ngày", 65);
            btnQuick7D.Click += async delegate
            {
                DateTime today = DateTime.Today;
                dtpDenNgayBD.Value = today;
                dtpTuNgayBD.Value = today.AddDays(-6);
                await LoadChartsAsync(btnXemBieuDo);
            };

            Button btnQuickThisMonth = CreateQuickFilterButton("Tháng Này", 78);
            btnQuickThisMonth.Click += async delegate
            {
                DateTime today = DateTime.Today;
                dtpTuNgayBD.Value = new DateTime(today.Year, today.Month, 1);
                dtpDenNgayBD.Value = today;
                await LoadChartsAsync(btnXemBieuDo);
            };

            Button btnQuickThisQuarter = CreateQuickFilterButton("Quý Này", 72);
            btnQuickThisQuarter.Click += async delegate
            {
                DateTime today = DateTime.Today;
                int quarter = (today.Month - 1) / 3 + 1;
                dtpTuNgayBD.Value = new DateTime(today.Year, (quarter - 1) * 3 + 1, 1);
                dtpDenNgayBD.Value = today;
                await LoadChartsAsync(btnXemBieuDo);
            };

            Button btnQuickThisYear = CreateQuickFilterButton("Năm Nay", 72);
            btnQuickThisYear.Click += async delegate
            {
                DateTime today = DateTime.Today;
                dtpTuNgayBD.Value = new DateTime(today.Year, 1, 1);
                dtpDenNgayBD.Value = today;
                await LoadChartsAsync(btnXemBieuDo);
            };

            btnXuatAnhBieuDo = new Button { Text = "🖼️ Xuất Biểu Đồ", Width = 120 };
            btnXuatAnhBieuDo.Click += btnXuatAnhBieuDo_Click;

            UiStyler.StyleButton(btnXemBieuDo, UiButtonRole.Primary);
            UiStyler.StyleButton(btnXuatAnhBieuDo, UiButtonRole.Secondary);

            FlowLayoutPanel flpFilter = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoSize = true,
                WrapContents = false
            };
            flpFilter.Controls.AddRange(new Control[] {
                lblTuNgayBD, dtpTuNgayBD,
                lblDenNgayBD, dtpDenNgayBD,
                btnXemBieuDo,
                btnQuick7D, btnQuickThisMonth, btnQuickThisQuarter, btnQuickThisYear,
                btnXuatAnhBieuDo
            });
            pnlFilterBD.Controls.Add(flpFilter);

            // Khởi tạo các Chart
            chartDoanhThu = new Chart { Dock = DockStyle.Fill, Margin = new Padding(0, 0, 4, 0) };
            chartCoCau = new Chart { Dock = DockStyle.Fill, Margin = new Padding(4, 0, 0, 0) };
            chartThuChi = new Chart { Dock = DockStyle.Fill, Margin = new Padding(0) };

            // Panel hàng trên (Split 60% Doanh thu - 40% Cơ cấu)
            TableLayoutPanel tlpTopCharts = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1,
                Margin = new Padding(0, 0, 0, 8)
            };
            tlpTopCharts.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 58F));
            tlpTopCharts.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 42F));
            tlpTopCharts.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpTopCharts.Controls.Add(chartDoanhThu, 0, 0);
            tlpTopCharts.Controls.Add(chartCoCau, 1, 0);

            _bieuDoStatus = new Label
            {
                Dock = DockStyle.Fill,
                Height = 24,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(8, 0, 0, 0),
                Text = "Sẵn sàng phân tích biểu đồ trực quan."
            };
            UiStyler.StyleStatusLabel(_bieuDoStatus, UiStatusKind.Neutral, "Sẵn sàng phân tích biểu đồ trực quan.");

            // Bố cục tổng thể của Tab Biểu Đồ
            TableLayoutPanel tlpRoot = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 4,
                BackColor = UiTheme.Canvas,
                Padding = new Padding(0)
            };
            tlpRoot.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpRoot.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F)); // Filter bar
            tlpRoot.RowStyles.Add(new RowStyle(SizeType.Percent, 52F));   // Top charts (Column + Doughnut)
            tlpRoot.RowStyles.Add(new RowStyle(SizeType.Percent, 48F));   // Bottom chart (Spline)
            tlpRoot.RowStyles.Add(new RowStyle(SizeType.Absolute, 28F)); // Status bar

            tlpRoot.Controls.Add(pnlFilterBD, 0, 0);
            tlpRoot.Controls.Add(tlpTopCharts, 0, 1);
            tlpRoot.Controls.Add(chartThuChi, 0, 2);
            tlpRoot.Controls.Add(_bieuDoStatus, 0, 3);

            tabBieuDo.Controls.Add(tlpRoot);
            tabControlMain.TabPages.Add(tabBieuDo);
            _chartsInitialized = true;
        }

        public async Task<bool> LoadChartsAsync(Button actionButton)
        {
            if (_isLoadingCharts) return false;
            _isLoadingCharts = true;

            try
            {
                DateTime tuNgay = dtpTuNgayBD.Value.Date;
                DateTime denNgay = dtpDenNgayBD.Value.Date;

                if (tuNgay > denNgay)
                {
                    UiStyler.StyleStatusLabel(_bieuDoStatus, UiStatusKind.Warning, "Từ ngày không được lớn hơn Đến ngày.");
                    return false;
                }

                UiStyler.StyleStatusLabel(_bieuDoStatus, UiStatusKind.Information, "Đang truy vấn số liệu và vẽ biểu đồ phân tích...");

                List<BaoCaoDoanhThuDTO> listDT = null;
                List<BaoCaoThuChiDTO> listTC = null;
                List<DoanhThuTheoLoaiSPDTO> listCoCau = null;
                string errDT = string.Empty;
                string errTC = string.Empty;
                string errCC = string.Empty;

                await UiFeedbackHelper.RunBusyAsync(
                    this,
                    actionButton,
                    "ĐANG TẢI...",
                    delegate
                    {
                        listDT = _reportingService.GetBaoCaoDoanhThu(tuNgay, denNgay, out errDT);
                        listTC = _reportingService.GetBaoCaoThuChi(tuNgay, denNgay, out errTC);
                        listCoCau = _reportingService.GetCoCauDoanhThuTheoLoaiSP(tuNgay, denNgay, out errCC);
                        return true;
                    });

                if (IsDisposed) return false;

                if (!string.IsNullOrEmpty(errDT) || !string.IsNullOrEmpty(errTC) || !string.IsNullOrEmpty(errCC))
                {
                    string errCombined = errDT + " " + errTC + " " + errCC;
                    UiStyler.StyleStatusLabel(_bieuDoStatus, UiStatusKind.Error, "Lỗi khi nạp dữ liệu biểu đồ: " + errCombined.Trim());
                    return false;
                }

                // 1. Tổng hợp doanh thu theo ngày cho Biểu đồ Cột
                List<DoanhThuTheoNgayDTO> dataDoanhThuNgay = new List<DoanhThuTheoNgayDTO>();
                if (listDT != null && listDT.Count > 0)
                {
                    var groupedDT = listDT
                        .GroupBy(x => x.NgayLap.Date)
                        .OrderBy(g => g.Key)
                        .Select(g => new DoanhThuTheoNgayDTO
                        {
                            Ngay = g.Key,
                            NhanNgay = g.Key.ToString("dd/MM"),
                            DoanhThu = g.Sum(x => x.TongTien),
                            SoHoaDon = g.Count()
                        }).ToList();
                    dataDoanhThuNgay = groupedDT;
                }

                // 2. Tổng hợp dòng tiền Thu - Chi theo ngày cho Biểu đồ Đường kép
                List<ThuChiTheoNgayDTO> dataThuChiNgay = new List<ThuChiTheoNgayDTO>();
                if (listTC != null && listTC.Count > 0)
                {
                    var groupedTC = listTC
                        .GroupBy(x => x.NgayGiaoDich.Date)
                        .OrderBy(g => g.Key)
                        .Select(g => new ThuChiTheoNgayDTO
                        {
                            Ngay = g.Key,
                            NhanNgay = g.Key.ToString("dd/MM"),
                            TongThu = g.Sum(x => x.SoTienThu),
                            TongChi = g.Sum(x => x.SoTienChi),
                            ChenhLech = g.Sum(x => x.SoTienThu) - g.Sum(x => x.SoTienChi)
                        }).ToList();
                    dataThuChiNgay = groupedTC;
                }

                // Vẽ 3 biểu đồ
                string titleDT = string.Format("BIẾN ĐỘNG DOANH THU BÁN HÀNG ({0:dd/MM/yyyy} - {1:dd/MM/yyyy})", tuNgay, denNgay);
                UiChartHelper.SetupColumnChart(chartDoanhThu, titleDT, dataDoanhThuNgay);

                string titleCC = "CƠ CẤU DOANH SỐ THEO LOẠI SẢN PHẨM";
                UiChartHelper.SetupDoughnutChart(chartCoCau, titleCC, listCoCau);

                string titleTC = string.Format("CÂN ĐỐI DÒNG TIỀN VÀO (THU) - RA (CHI) THEO NGÀY ({0:dd/MM/yyyy} - {1:dd/MM/yyyy})", tuNgay, denNgay);
                UiChartHelper.SetupSplineCashFlowChart(chartThuChi, titleTC, dataThuChiNgay);

                decimal tongDT = listDT != null ? listDT.Sum(x => x.TongTien) : 0;
                decimal tongThu = listTC != null ? listTC.Sum(x => x.SoTienThu) : 0;
                decimal tongChi = listTC != null ? listTC.Sum(x => x.SoTienChi) : 0;

                UiStyler.StyleStatusLabel(
                    _bieuDoStatus,
                    UiStatusKind.Success,
                    string.Format("Đã cập nhật biểu đồ: Doanh thu: {0:N0} VNĐ | Tiền thu: {1:N0} VNĐ | Tiền chi: {2:N0} VNĐ",
                        tongDT, tongThu, tongChi));

                return true;
            }
            finally
            {
                _isLoadingCharts = false;
                if (btnXemBieuDo != null && !btnXemBieuDo.IsDisposed)
                {
                    btnXemBieuDo.Text = "Tra Cứu";
                    btnXemBieuDo.Enabled = true;
                }
            }
        }

        private void btnXuatAnhBieuDo_Click(object sender, EventArgs e)
        {
            ContextMenuStrip menu = new ContextMenuStrip();
            menu.Items.Add("📊 Xuất ảnh Biểu đồ Doanh thu (Column Chart)", null, (s, ev) =>
            {
                UiChartHelper.ExportChartImage(chartDoanhThu, "BieuDoDoanhThu");
            });
            menu.Items.Add("🍩 Xuất ảnh Biểu đồ Cơ cấu loại SP (Doughnut Chart)", null, (s, ev) =>
            {
                UiChartHelper.ExportChartImage(chartCoCau, "BieuDoCoCauSanPham");
            });
            menu.Items.Add("📈 Xuất ảnh Biểu đồ Dòng tiền Thu - Chi (Spline Chart)", null, (s, ev) =>
            {
                UiChartHelper.ExportChartImage(chartThuChi, "BieuDoDongTienThuChi");
            });

            if (btnXuatAnhBieuDo != null)
            {
                menu.Show(btnXuatAnhBieuDo, new Point(0, btnXuatAnhBieuDo.Height));
            }
        }

        private Button CreateQuickFilterButton(string text, int width)
        {
            Button btn = new Button
            {
                Text = text,
                Width = width,
                Height = 28,
                Margin = new Padding(3, 4, 3, 4),
                Cursor = Cursors.Hand,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 8.5F, FontStyle.Regular),
                BackColor = Color.FromArgb(241, 245, 249),
                ForeColor = Color.FromArgb(51, 65, 85)
            };
            btn.FlatAppearance.BorderSize = 0;
            return btn;
        }
    }
}
