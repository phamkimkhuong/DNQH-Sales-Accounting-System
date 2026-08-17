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
    public partial class frmMain
    {
        private Panel pnlDashboardCharts;
        private TableLayoutPanel tlpDashboardCharts;
        private Chart chartDashRevenue;
        private Chart chartDashCategory;
        private Button btnDashChart7D;
        private Button btnDashChart14D;
        private Button btnDashChart30D;
        private Button btnDashChartThisMonth;
        private Button btnDashChartDetail;
        private int _currentDashDays = 7;
        private bool _isDashThisMonth = false;
        private bool _dashChartsInitialized = false;

        public void ShowDashboard()
        {
            CloseCurrentChildForm();

            pnlChildContainer.Controls.Clear();
            pnlChildContainer.Visible = false;
            pnlDashboard.Visible = true;
            pnlDashboard.BringToFront();

            lblBreadcrumb.Text = "Trang Chủ / Bảng Điều Khiển Tổng Quan";
            btnBackToDashboard.Visible = false;
            SetActiveNavigation(btnNavDashboard);

            Task fireAndForget = LoadDashboardKPIAsync();
        }

        private async Task LoadDashboardKPIAsync()
        {
            try
            {
                if (SessionManager.IsAdmin() || SessionManager.IsAccountant())
                {
                    tlpKPICards.Visible = true;
                    btnRefreshKPI.Visible = true;

                    InitializeDashboardCharts();
                    await LoadDashboardChartsAsync(_currentDashDays, _isDashThisMonth);

                    // Lấy toàn bộ chỉ số từ đầu năm đến nay
                    DateTime fromDate = new DateTime(DateTime.Today.Year, 1, 1);
                    DateTime toDate = DateTime.Today.AddDays(1);

                    TongHopKpiDTO kpi = await Task.Run(() => reportingService.GetTongHopKpi(fromDate, toDate));
                    if (this.IsDisposed) return;

                    if (kpi != null)
                    {
                        lblCardRevenueValue.Text = string.Format("{0:N0} VNĐ", kpi.TongDoanhThu);
                        lblCardReceiptValue.Text = string.Format("{0:N0} VNĐ", kpi.TongThu);
                        lblCardPaymentValue.Text = string.Format("{0:N0} VNĐ", kpi.TongChi);
                        lblCardStockValue.Text = string.Format("{0:N0} SP", kpi.TongSoLuongTon);
                    }
                }
                else
                {
                    tlpKPICards.Visible = false;
                    btnRefreshKPI.Visible = false;
                    if (pnlDashboardCharts != null) pnlDashboardCharts.Visible = false;
                }
            }
            catch (Exception ex)
            {
                AppLogger.Warn("LoadDashboardKPI", "Không thể cập nhật chỉ số KPI: " + ex.Message);
                if (!this.IsDisposed)
                {
                    lblCardRevenueValue.Text = "-- VNĐ";
                    lblCardReceiptValue.Text = "-- VNĐ";
                    lblCardPaymentValue.Text = "-- VNĐ";
                    lblCardStockValue.Text = "-- SP";
                }
            }

            await LoadStockAlertsAsync();
        }

        private async Task LoadStockAlertsAsync()
        {
            bool canViewStock = SessionManager.IsAdmin() || SessionManager.IsAccountant() || SessionManager.IsWarehouse() || SessionManager.IsSales();
            if (!canViewStock)
            {
                pnlStockAlertContainer.Visible = false;
                return;
            }

            pnlStockAlertContainer.Visible = true;
            int threshold = GetSelectedThreshold();

            try
            {
                List<CanhBaoTonKhoDTO> alerts = await Task.Run(() => reportingService.GetDanhSachCanhBaoTonKho(threshold));
                if (this.IsDisposed) return;

                if (alerts != null && alerts.Count > 0)
                {
                    dgvStockAlert.AutoGenerateColumns = false;
                    dgvStockAlert.DataSource = alerts;
                    dgvStockAlert.Visible = true;
                    pnlStockSafeBanner.Visible = false;

                    int countCanNhap = 0;
                    int countDieuChuyen = 0;
                    foreach (var a in alerts)
                    {
                        if (a.TongTon <= threshold) countCanNhap++;
                        else countDieuChuyen++;
                    }

                    string statusSummary;
                    if (countCanNhap > 0 && countDieuChuyen > 0)
                    {
                        statusSummary = string.Format("{0} cần nhập mới, {1} cần điều chuyển kho", countCanNhap, countDieuChuyen);
                    }
                    else if (countCanNhap > 0)
                    {
                        statusSummary = string.Format("{0} cần nhập mới", countCanNhap);
                    }
                    else
                    {
                        statusSummary = string.Format("{0} cần điều chuyển kho", countDieuChuyen);
                    }

                    lblStockAlertTitle.Text = string.Format("⚠️ CẢNH BÁO TỒN KHO AN TOÀN ({0} MẶT HÀNG: {1})", alerts.Count, statusSummary.ToUpper());
                    lblStockAlertTitle.ForeColor = countCanNhap > 0 ? Color.FromArgb(194, 65, 12) : Color.FromArgb(3, 105, 161);

                    pnlCardStockBar.BackColor = countCanNhap > 0 ? Color.FromArgb(239, 68, 68) : Color.FromArgb(14, 165, 233);
                    lblCardStockSub.Text = string.Format("⚠️ {0} mặt hàng cần nhập/chuyển kho", alerts.Count);
                    lblCardStockSub.ForeColor = countCanNhap > 0 ? Color.FromArgb(220, 38, 38) : Color.FromArgb(3, 105, 161);
                }
                else
                {
                    dgvStockAlert.DataSource = null;
                    dgvStockAlert.Visible = false;
                    pnlStockSafeBanner.Visible = true;
                    lblStockSafeMessage.Text = string.Format("Tất cả các mặt hàng bảo đảm mức tồn an toàn (> {0} SP trên từng kho)", threshold);
                    lblStockAlertTitle.Text = "✅ TÌNH TRẠNG TỒN KHO AN TOÀN ĐẠT CHUẨN";
                    lblStockAlertTitle.ForeColor = Color.FromArgb(22, 101, 52);

                    pnlCardStockBar.BackColor = Color.FromArgb(236, 72, 153);
                    lblCardStockSub.Text = "Tổng lượng hàng hóa trong kho";
                    lblCardStockSub.ForeColor = Color.FromArgb(100, 116, 139);
                }
            }
            catch (Exception ex)
            {
                AppLogger.Warn("LoadStockAlerts", "Không thể tải danh sách cảnh báo tồn kho: " + ex.Message);
            }
        }

        private int GetSelectedThreshold()
        {
            if (cboSafetyThreshold != null && cboSafetyThreshold.SelectedItem != null)
            {
                string text = cboSafetyThreshold.SelectedItem.ToString();
                if (text.Contains("5 SP")) return 5;
                if (text.Contains("10 SP")) return 10;
                if (text.Contains("20 SP")) return 20;
                if (text.Contains("50 SP")) return 50;
            }
            return 10;
        }

        private async void cboSafetyThreshold_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!this.IsDisposed)
            {
                await LoadStockAlertsAsync();
            }
        }

        private async void btnStockAlertRefresh_Click(object sender, EventArgs e)
        {
            btnStockAlertRefresh.Enabled = false;
            try
            {
                await LoadStockAlertsAsync();
            }
            finally
            {
                btnStockAlertRefresh.Enabled = true;
            }
        }

        private void btnGoToTonKho_Click(object sender, EventArgs e)
        {
            menuTonKho_Click(sender, e);
        }

        private void dgvStockAlert_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvStockAlert.Rows.Count > e.RowIndex)
            {
                dgvStockAlert.Rows[e.RowIndex].Cells["colStockAlertSTT"].Value = (e.RowIndex + 1).ToString();
            }
        }

        private void dgvStockAlert_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (SystemInformation.HighContrast || e.RowIndex < 0) return;

            if (e.ColumnIndex == colStockAlertMucDo.Index && e.Value != null)
            {
                string val = e.Value.ToString();
                if (val.Contains("Hết hàng"))
                {
                    e.CellStyle.ForeColor = Color.FromArgb(220, 38, 38);
                    e.CellStyle.Font = new Font(dgvStockAlert.Font, FontStyle.Bold);
                }
                else if (val.Contains("nhập hàng") || val.Contains("Sắp hết"))
                {
                    e.CellStyle.ForeColor = Color.FromArgb(217, 119, 6);
                    e.CellStyle.Font = new Font(dgvStockAlert.Font, FontStyle.Bold);
                }
                else if (val.Contains("điều chuyển"))
                {
                    e.CellStyle.ForeColor = Color.FromArgb(2, 132, 199);
                    e.CellStyle.Font = new Font(dgvStockAlert.Font, FontStyle.Bold);
                }
            }
            else if (e.ColumnIndex == colStockAlertTongTon.Index && e.Value != null)
            {
                int qty;
                if (int.TryParse(e.Value.ToString(), out qty))
                {
                    if (qty <= 0)
                    {
                        e.CellStyle.ForeColor = Color.FromArgb(220, 38, 38);
                        e.CellStyle.Font = new Font(dgvStockAlert.Font, FontStyle.Bold);
                    }
                    else if (qty <= 10)
                    {
                        e.CellStyle.ForeColor = Color.FromArgb(217, 119, 6);
                        e.CellStyle.Font = new Font(dgvStockAlert.Font, FontStyle.Bold);
                    }
                    else
                    {
                        e.CellStyle.ForeColor = Color.FromArgb(30, 41, 59);
                    }
                }
            }
        }

        private void ConfigureStockAlertGrid()
        {
            dgvStockAlert.AutoGenerateColumns = false;
            dgvStockAlert.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            colStockAlertSTT.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colStockAlertSTT.Width = 45;
            colStockAlertSTT.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            colStockAlertMaSP.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colStockAlertMaSP.Width = 80;
            colStockAlertMaSP.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            colStockAlertTenSP.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colStockAlertTenSP.FillWeight = 28F;
            colStockAlertTenSP.MinimumWidth = 220;
            colStockAlertTenSP.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            colStockAlertLoaiSP.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colStockAlertLoaiSP.FillWeight = 18F;
            colStockAlertLoaiSP.MinimumWidth = 140;
            colStockAlertLoaiSP.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            colStockAlertDVT.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colStockAlertDVT.Width = 55;
            colStockAlertDVT.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            colStockAlertTongTon.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colStockAlertTongTon.Width = 80;
            colStockAlertTongTon.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colStockAlertTongTon.DefaultCellStyle.Format = "N0";

            colStockAlertMucDo.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colStockAlertMucDo.Width = 160;
            colStockAlertMucDo.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            colStockAlertChiTietKho.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colStockAlertChiTietKho.FillWeight = 54F;
            colStockAlertChiTietKho.MinimumWidth = 350;
            colStockAlertChiTietKho.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
        }

        private async void btnRefreshKPI_Click(object sender, EventArgs e)
        {
            btnRefreshKPI.Enabled = false;
            lblStatusConnection.Text = "● Đang cập nhật số liệu...";
            if (!SystemInformation.HighContrast)
            {
                lblStatusConnection.ForeColor = UiTheme.Information;
            }

            try
            {
                await LoadDashboardKPIAsync();
                lblStatusConnection.Text = "● Số liệu đã được cập nhật";
                if (!SystemInformation.HighContrast)
                {
                    lblStatusConnection.ForeColor = UiTheme.Success;
                }
            }
            finally
            {
                btnRefreshKPI.Enabled = true;
            }
        }

        private void InitializeDashboardCharts()
        {
            if (_dashChartsInitialized) return;

            pnlDashboardCharts = new Panel
            {
                Dock = DockStyle.Top,
                Height = 340,
                BackColor = Color.White,
                Margin = new Padding(0, 0, 0, 16),
                Padding = new Padding(16, 10, 16, 12)
            };

            Panel pnlHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 38,
                BackColor = Color.White
            };

            Label lblHeaderTitle = new Label
            {
                Text = "📈 XU HƯỚNG KINH DOANH & CƠ CẤU DOANH SỐ",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.FromArgb(30, 41, 59),
                AutoSize = true,
                Location = new Point(0, 8)
            };

            FlowLayoutPanel flpActions = new FlowLayoutPanel
            {
                Dock = DockStyle.Right,
                AutoSize = true,
                WrapContents = false
            };

            btnDashChart7D = CreateDashboardFilterButton("7 Ngày", 65);
            btnDashChart14D = CreateDashboardFilterButton("14 Ngày", 70);
            btnDashChart30D = CreateDashboardFilterButton("30 Ngày", 70);
            btnDashChartThisMonth = CreateDashboardFilterButton("Tháng Này", 82);

            btnDashChartDetail = new Button
            {
                Text = "Báo Cáo Chi Tiết ➔",
                AutoSize = true,
                Height = 28,
                Margin = new Padding(8, 4, 0, 4),
                Cursor = Cursors.Hand
            };
            UiStyler.StyleButton(btnDashChartDetail, UiButtonRole.Secondary);
            btnDashChartDetail.Click += (s, e) =>
            {
                OpenBaoCaoTongHop(3);
            };

            HighlightDashboardFilterButton(btnDashChart7D);

            btnDashChart7D.Click += async (s, e) =>
            {
                HighlightDashboardFilterButton(btnDashChart7D);
                await LoadDashboardChartsAsync(7, false);
            };
            btnDashChart14D.Click += async (s, e) =>
            {
                HighlightDashboardFilterButton(btnDashChart14D);
                await LoadDashboardChartsAsync(14, false);
            };
            btnDashChart30D.Click += async (s, e) =>
            {
                HighlightDashboardFilterButton(btnDashChart30D);
                await LoadDashboardChartsAsync(30, false);
            };
            btnDashChartThisMonth.Click += async (s, e) =>
            {
                HighlightDashboardFilterButton(btnDashChartThisMonth);
                await LoadDashboardChartsAsync(0, true);
            };

            flpActions.Controls.AddRange(new Control[] {
                btnDashChart7D, btnDashChart14D, btnDashChart30D, btnDashChartThisMonth, btnDashChartDetail
            });

            pnlHeader.Controls.Add(lblHeaderTitle);
            pnlHeader.Controls.Add(flpActions);

            tlpDashboardCharts = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1,
                Margin = new Padding(0)
            };
            tlpDashboardCharts.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 58F));
            tlpDashboardCharts.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 42F));
            tlpDashboardCharts.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            chartDashRevenue = new Chart { Dock = DockStyle.Fill, Margin = new Padding(0, 0, 8, 0) };
            chartDashCategory = new Chart { Dock = DockStyle.Fill, Margin = new Padding(8, 0, 0, 0) };

            tlpDashboardCharts.Controls.Add(chartDashRevenue, 0, 0);
            tlpDashboardCharts.Controls.Add(chartDashCategory, 1, 0);

            pnlDashboardCharts.Controls.Add(tlpDashboardCharts);
            pnlDashboardCharts.Controls.Add(pnlHeader);

            pnlDashboard.Controls.Add(pnlDashboardCharts);
            pnlDashboard.Controls.SetChildIndex(pnlWelcomeBanner, 0);
            pnlDashboard.Controls.SetChildIndex(tlpKPICards, 1);
            pnlDashboard.Controls.SetChildIndex(pnlDashboardCharts, 2);
            pnlDashboard.Controls.SetChildIndex(pnlStockAlertContainer, 3);
            pnlDashboard.Controls.SetChildIndex(pnlQuickActionsContainer, 4);
            pnlDashboard.Controls.SetChildIndex(pnlRoleNote, 5);

            _dashChartsInitialized = true;
        }

        private async Task LoadDashboardChartsAsync(int days, bool isThisMonth = false)
        {
            if (!SessionManager.IsAdmin() && !SessionManager.IsAccountant())
            {
                if (pnlDashboardCharts != null) pnlDashboardCharts.Visible = false;
                return;
            }

            if (pnlDashboardCharts != null) pnlDashboardCharts.Visible = true;
            _currentDashDays = days;
            _isDashThisMonth = isThisMonth;

            DateTime tuNgay;
            DateTime denNgay = DateTime.Today;

            if (isThisMonth)
            {
                tuNgay = new DateTime(denNgay.Year, denNgay.Month, 1);
            }
            else
            {
                tuNgay = denNgay.AddDays(-days + 1);
            }

            try
            {
                List<BaoCaoDoanhThuDTO> listDT = null;
                List<DoanhThuTheoLoaiSPDTO> listCoCau = null;

                await Task.Run(() =>
                {
                    string errDT, errCC;
                    listDT = reportingService.GetBaoCaoDoanhThu(tuNgay, denNgay, out errDT);
                    listCoCau = reportingService.GetCoCauDoanhThuTheoLoaiSP(tuNgay, denNgay, out errCC);
                });

                if (this.IsDisposed || chartDashRevenue == null || chartDashCategory == null) return;

                List<DoanhThuTheoNgayDTO> dataDoanhThu = new List<DoanhThuTheoNgayDTO>();
                if (listDT != null && listDT.Count > 0)
                {
                    dataDoanhThu = listDT
                        .GroupBy(x => x.NgayLap.Date)
                        .OrderBy(g => g.Key)
                        .Select(g => new DoanhThuTheoNgayDTO
                        {
                            Ngay = g.Key,
                            NhanNgay = g.Key.ToString("dd/MM"),
                            DoanhThu = g.Sum(x => x.TongTien),
                            SoHoaDon = g.Count()
                        }).ToList();
                }

                string chartTitleDT = isThisMonth
                    ? string.Format("DOANH THU THÁNG {0:MM/yyyy}", denNgay)
                    : string.Format("DOANH THU {0} NGÀY GẦN NHẤT ({1:dd/MM} - {2:dd/MM})", days, tuNgay, denNgay);

                UiChartHelper.SetupDashboardRevenueChart(chartDashRevenue, chartTitleDT, dataDoanhThu);

                string chartTitleCC = isThisMonth
                    ? string.Format("CƠ CẤU LOẠI HÀNG THÁNG {0:MM/yyyy}", denNgay)
                    : string.Format("CƠ CẤU LOẠI HÀNG ({0:dd/MM} - {1:dd/MM})", tuNgay, denNgay);

                UiChartHelper.SetupDashboardCategoryChart(chartDashCategory, chartTitleCC, listCoCau);
            }
            catch (Exception ex)
            {
                AppLogger.Warn("LoadDashboardCharts", "Không thể cập nhật biểu đồ Dashboard: " + ex.Message);
            }
        }

        private Button CreateDashboardFilterButton(string text, int width)
        {
            Button btn = new Button
            {
                Text = text,
                Width = width,
                Height = 28,
                Margin = new Padding(2, 4, 2, 4),
                Cursor = Cursors.Hand,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 8.5F, FontStyle.Regular),
                BackColor = Color.FromArgb(241, 245, 249),
                ForeColor = Color.FromArgb(51, 65, 85)
            };
            btn.FlatAppearance.BorderSize = 0;
            return btn;
        }

        private void HighlightDashboardFilterButton(Button activeBtn)
        {
            Button[] btns = new Button[] { btnDashChart7D, btnDashChart14D, btnDashChart30D, btnDashChartThisMonth };
            foreach (var b in btns)
            {
                if (b == null) continue;
                if (b == activeBtn)
                {
                    b.BackColor = Color.FromArgb(99, 102, 241);
                    b.ForeColor = Color.White;
                    b.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
                }
                else
                {
                    b.BackColor = Color.FromArgb(241, 245, 249);
                    b.ForeColor = Color.FromArgb(51, 65, 85);
                    b.Font = new Font("Segoe UI", 8.5F, FontStyle.Regular);
                }
            }
        }
    }
}
