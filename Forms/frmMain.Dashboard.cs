using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DNQH_KeToanBanHang.Helpers;
using DNQH_KeToanBanHang.Models;

namespace DNQH_KeToanBanHang.Forms
{
    public partial class frmMain
    {
        public void ShowDashboard()
        {
            CloseCurrentChildForm();

            pnlChildContainer.Controls.Clear();
            pnlChildContainer.Visible = false;
            pnlDashboard.Visible = true;
            pnlDashboard.BringToFront();

            lblBreadcrumb.Text = "Trang Chủ / Bảng Điều Khiển Tổng Quan";
            btnBackToDashboard.Visible = false;
            UpdateHeaderNavigationLayout();
            SetActiveNavigation(btnNavDashboard);

            SetupDashboardLayoutOrder();

            Task fireAndForget = LoadDashboardKPIAsync();
        }

        private async Task LoadDashboardKPIAsync()
        {
            if (reportingService == null) return;
            try
            {
                if (SessionManager.IsAdmin() || SessionManager.IsAccountant())
                {
                    tlpKPICards.Visible = true;
                    btnRefreshKPI.Visible = true;

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
            if (!canViewStock || reportingService == null)
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
            if (!this.IsDisposed && reportingService != null)
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

        private void SetupDashboardLayoutOrder()
        {
            if (pnlDashboard != null)
            {
                pnlDashboard.Controls.SetChildIndex(pnlWelcomeBanner, 0);
                pnlDashboard.Controls.SetChildIndex(tlpKPICards, 1);
                pnlDashboard.Controls.SetChildIndex(pnlStockAlertContainer, 2);
                pnlDashboard.Controls.SetChildIndex(pnlQuickActionsContainer, 3);
                pnlDashboard.Controls.SetChildIndex(pnlRoleNote, 4);
            }
        }
    }
}
