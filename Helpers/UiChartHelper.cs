using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using DNQH_KeToanBanHang.Models;

namespace DNQH_KeToanBanHang.Helpers
{
    /// <summary>
    /// Tiện ích khởi tạo, cấu hình và tô màu biểu đồ WinForms DataVisualization hiện đại
    /// </summary>
    public static class UiChartHelper
    {
        // Bảng màu hiện đại hài hòa với UiTheme
        public static readonly Color[] Palette = new Color[]
        {
            Color.FromArgb(124, 58, 237),  // Purple / Primary
            Color.FromArgb(14, 165, 233),  // Sky Blue
            Color.FromArgb(16, 185, 129),  // Emerald Green
            Color.FromArgb(245, 158, 11),  // Amber
            Color.FromArgb(244, 63, 94),   // Rose Red
            Color.FromArgb(99, 102, 241),  // Indigo
            Color.FromArgb(20, 184, 166),  // Teal
            Color.FromArgb(234, 88, 12)    // Orange
        };

        public static readonly Color GridLineColor = Color.FromArgb(241, 245, 249);
        public static readonly Color AxisLineColor = Color.FromArgb(203, 213, 225);
        public static readonly Color LabelTextColor = Color.FromArgb(100, 116, 139);

        /// <summary>
        /// Chuẩn hóa giao diện vùng hiển thị biểu đồ theo phong cách phẳng hiện đại
        /// </summary>
        public static void StyleChartArea(ChartArea area, string titleX = null, string titleY = null)
        {
            if (area == null) return;

            area.BackColor = Color.White;
            area.BackSecondaryColor = Color.Transparent;
            area.BorderColor = Color.Transparent;
            area.BorderWidth = 0;
            area.ShadowOffset = 0;

            // Trục X
            area.AxisX.LineColor = AxisLineColor;
            area.AxisX.LineWidth = 1;
            area.AxisX.MajorGrid.LineColor = GridLineColor;
            area.AxisX.MajorGrid.LineDashStyle = ChartDashStyle.Dash;
            area.AxisX.LabelStyle.Font = new Font("Segoe UI", 8.5F, FontStyle.Regular);
            area.AxisX.LabelStyle.ForeColor = LabelTextColor;
            area.AxisX.Interval = 1;
            area.AxisX.IsLabelAutoFit = true;
            if (!string.IsNullOrEmpty(titleX))
            {
                area.AxisX.Title = titleX;
                area.AxisX.TitleFont = new Font("Segoe UI", 9F, FontStyle.Bold);
                area.AxisX.TitleForeColor = LabelTextColor;
            }

            // Trục Y
            area.AxisY.LineColor = AxisLineColor;
            area.AxisY.LineWidth = 1;
            area.AxisY.MajorGrid.LineColor = GridLineColor;
            area.AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dash;
            area.AxisY.LabelStyle.Font = new Font("Segoe UI", 8.5F, FontStyle.Regular);
            area.AxisY.LabelStyle.ForeColor = LabelTextColor;
            area.AxisY.LabelStyle.Format = "#,##0";
            if (!string.IsNullOrEmpty(titleY))
            {
                area.AxisY.Title = titleY;
                area.AxisY.TitleFont = new Font("Segoe UI", 9F, FontStyle.Bold);
                area.AxisY.TitleForeColor = LabelTextColor;
            }
        }

        /// <summary>
        /// Tạo và cấu hình Biểu đồ Cột (Column Chart) doanh thu theo thời gian
        /// </summary>
        public static void SetupColumnChart(Chart chart, string chartTitle, List<DoanhThuTheoNgayDTO> data)
        {
            if (chart == null) return;

            chart.Series.Clear();
            chart.ChartAreas.Clear();
            chart.Titles.Clear();
            chart.Legends.Clear();

            chart.BackColor = Color.White;

            // Title
            Title title = new Title(chartTitle, Docking.Top, new Font("Segoe UI", 11F, FontStyle.Bold), Color.FromArgb(30, 41, 59));
            title.Alignment = ContentAlignment.MiddleLeft;
            chart.Titles.Add(title);

            ChartArea area = new ChartArea("AreaDoanhThu");
            StyleChartArea(area, "Thời gian", "Doanh thu (VNĐ)");
            chart.ChartAreas.Add(area);

            Series series = new Series("DoanhThu")
            {
                ChartType = SeriesChartType.Column,
                Color = Color.FromArgb(124, 58, 237),
                BorderWidth = 0,
                IsValueShownAsLabel = false,
                XValueType = ChartValueType.String,
                YValueType = ChartValueType.Double
            };
            series["PointWidth"] = "0.6";

            if (data != null && data.Count > 0)
            {
                foreach (var item in data)
                {
                    int ptIdx = series.Points.AddXY(item.NhanNgay, (double)item.DoanhThu);
                    DataPoint pt = series.Points[ptIdx];
                    pt.ToolTip = string.Format("Ngày: {0}\nDoanh thu: {1:N0} VNĐ\nSố hóa đơn: {2}", item.NhanNgay, item.DoanhThu, item.SoHoaDon);

                    if (item.DoanhThu > 50000000)
                    {
                        pt.Color = Color.FromArgb(109, 40, 217);
                    }
                }
            }
            else
            {
                int ptIdx = series.Points.AddXY("Chưa có phát sinh", 0);
                series.Points[ptIdx].Color = Color.FromArgb(226, 232, 240);
                series.Points[ptIdx].ToolTip = "Không có giao dịch trong kỳ";
            }

            chart.Series.Add(series);
        }

        /// <summary>
        /// Cấu hình biểu đồ cột Doanh thu tối ưu cho Trang chủ Dashboard (gọn gàng, mượt mà)
        /// </summary>
        public static void SetupDashboardRevenueChart(Chart chart, string chartTitle, List<DoanhThuTheoNgayDTO> data)
        {
            if (chart == null) return;

            chart.Series.Clear();
            chart.ChartAreas.Clear();
            chart.Titles.Clear();
            chart.Legends.Clear();

            chart.BackColor = Color.White;

            Title title = new Title(chartTitle, Docking.Top, new Font("Segoe UI", 10.5F, FontStyle.Bold), Color.FromArgb(30, 41, 59));
            title.Alignment = ContentAlignment.MiddleLeft;
            chart.Titles.Add(title);

            ChartArea area = new ChartArea("AreaDashRevenue");
            StyleChartArea(area, "Ngày", "Doanh thu (VNĐ)");
            area.Position.Auto = true;
            chart.ChartAreas.Add(area);

            Series series = new Series("RevenueSeries")
            {
                ChartType = SeriesChartType.Column,
                Color = Color.FromArgb(99, 102, 241), // Indigo
                BorderWidth = 0,
                IsValueShownAsLabel = false,
                XValueType = ChartValueType.String,
                YValueType = ChartValueType.Double
            };
            series["PointWidth"] = "0.55";

            if (data != null && data.Count > 0)
            {
                foreach (var item in data)
                {
                    int ptIdx = series.Points.AddXY(item.NhanNgay, (double)item.DoanhThu);
                    DataPoint pt = series.Points[ptIdx];
                    pt.ToolTip = string.Format("Ngày: {0}\nDoanh thu: {1:N0} VNĐ\nSố hóa đơn: {2}", item.NhanNgay, item.DoanhThu, item.SoHoaDon);
                    if (item.DoanhThu > 50000000)
                    {
                        pt.Color = Color.FromArgb(124, 58, 237); // Deep Purple
                    }
                }
            }
            else
            {
                int ptIdx = series.Points.AddXY("Chưa có phát sinh", 0);
                series.Points[ptIdx].Color = Color.FromArgb(226, 232, 240);
                series.Points[ptIdx].ToolTip = "Không có giao dịch trong kỳ";
            }

            chart.Series.Add(series);
        }

        /// <summary>
        /// Tạo và cấu hình Biểu đồ Tròn/Bánh (Doughnut Chart) cơ cấu loại sản phẩm
        /// </summary>
        public static void SetupDoughnutChart(Chart chart, string chartTitle, List<DoanhThuTheoLoaiSPDTO> data)
        {
            if (chart == null) return;

            chart.Series.Clear();
            chart.ChartAreas.Clear();
            chart.Titles.Clear();
            chart.Legends.Clear();

            chart.BackColor = Color.White;

            Title title = new Title(chartTitle, Docking.Top, new Font("Segoe UI", 11F, FontStyle.Bold), Color.FromArgb(30, 41, 59));
            title.Alignment = ContentAlignment.MiddleLeft;
            chart.Titles.Add(title);

            Legend legend = new Legend("LegendCoCau")
            {
                Docking = Docking.Bottom,
                Alignment = StringAlignment.Center,
                Font = new Font("Segoe UI", 8.5F, FontStyle.Regular),
                ForeColor = LabelTextColor,
                BackColor = Color.Transparent
            };
            chart.Legends.Add(legend);

            ChartArea area = new ChartArea("AreaCoCau");
            area.BackColor = Color.White;
            chart.ChartAreas.Add(area);

            Series series = new Series("CoCauLoaiSP")
            {
                ChartType = SeriesChartType.Doughnut,
                Legend = "LegendCoCau",
                IsValueShownAsLabel = true
            };
            series["DoughnutRadius"] = "55";
            series["PieLabelStyle"] = "Inside";
            series.Font = new Font("Segoe UI", 8F, FontStyle.Bold);

            if (data != null && data.Count > 0)
            {
                int colorIdx = 0;
                foreach (var item in data)
                {
                    int ptIdx = series.Points.AddXY(item.TenLoaiSP, (double)item.TongDoanhThu);
                    DataPoint pt = series.Points[ptIdx];
                    pt.Color = Palette[colorIdx % Palette.Length];
                    pt.LegendText = string.Format("{0} ({1:F1}%)", item.TenLoaiSP, item.TyLePhanTram);
                    pt.Label = item.TyLePhanTram >= 5 ? string.Format("{0:F1}%", item.TyLePhanTram) : "";
                    pt.LabelForeColor = Color.White;
                    pt.ToolTip = string.Format("Nhóm hàng: {0}\nDoanh thu: {1:N0} VNĐ ({2:F1}%)\nSố lượng bán: {3:N0} SP",
                        item.TenLoaiSP, item.TongDoanhThu, item.TyLePhanTram, item.TongSoLuongBan);
                    colorIdx++;
                }
            }
            else
            {
                int ptIdx = series.Points.AddXY("Chưa có số liệu", 1);
                DataPoint pt = series.Points[ptIdx];
                pt.Color = Color.FromArgb(226, 232, 240);
                pt.Label = "Không có số liệu";
                pt.LegendText = "Không có số liệu";
            }

            chart.Series.Add(series);
        }

        /// <summary>
        /// Cấu hình biểu đồ tròn/bánh (Doughnut) cơ cấu loại SP tối ưu cho Trang chủ Dashboard
        /// </summary>
        public static void SetupDashboardCategoryChart(Chart chart, string chartTitle, List<DoanhThuTheoLoaiSPDTO> data)
        {
            if (chart == null) return;

            chart.Series.Clear();
            chart.ChartAreas.Clear();
            chart.Titles.Clear();
            chart.Legends.Clear();

            chart.BackColor = Color.White;

            Title title = new Title(chartTitle, Docking.Top, new Font("Segoe UI", 10.5F, FontStyle.Bold), Color.FromArgb(30, 41, 59));
            title.Alignment = ContentAlignment.MiddleLeft;
            chart.Titles.Add(title);

            Legend legend = new Legend("LegendDashCategory")
            {
                Docking = Docking.Right,
                Alignment = StringAlignment.Center,
                Font = new Font("Segoe UI", 8F, FontStyle.Regular),
                ForeColor = LabelTextColor,
                BackColor = Color.Transparent
            };
            chart.Legends.Add(legend);

            ChartArea area = new ChartArea("AreaDashCategory");
            area.BackColor = Color.White;
            area.Position.Auto = true;
            chart.ChartAreas.Add(area);

            Series series = new Series("DashCategorySeries")
            {
                ChartType = SeriesChartType.Doughnut,
                Legend = "LegendDashCategory",
                IsValueShownAsLabel = true
            };
            series["DoughnutRadius"] = "52";
            series["PieLabelStyle"] = "Inside";
            series.Font = new Font("Segoe UI", 7.5F, FontStyle.Bold);

            if (data != null && data.Count > 0)
            {
                int colorIdx = 0;
                foreach (var item in data)
                {
                    int ptIdx = series.Points.AddXY(item.TenLoaiSP, (double)item.TongDoanhThu);
                    DataPoint pt = series.Points[ptIdx];
                    pt.Color = Palette[colorIdx % Palette.Length];
                    pt.LegendText = string.Format("{0} ({1:F1}%)", item.TenLoaiSP, item.TyLePhanTram);
                    pt.Label = item.TyLePhanTram >= 7 ? string.Format("{0:F0}%", item.TyLePhanTram) : "";
                    pt.LabelForeColor = Color.White;
                    pt.ToolTip = string.Format("Nhóm: {0}\nDoanh số: {1:N0} VNĐ ({2:F1}%)\nSL bán: {3:N0} SP",
                        item.TenLoaiSP, item.TongDoanhThu, item.TyLePhanTram, item.TongSoLuongBan);
                    colorIdx++;
                }
            }
            else
            {
                int ptIdx = series.Points.AddXY("Chưa có số liệu", 1);
                DataPoint pt = series.Points[ptIdx];
                pt.Color = Color.FromArgb(226, 232, 240);
                pt.Label = "Không có số liệu";
                pt.LegendText = "Không có số liệu";
            }

            chart.Series.Add(series);
        }

        /// <summary>
        /// Tạo và cấu hình Biểu đồ Đường mềm kép (Spline Chart) Thu - Chi
        /// </summary>
        public static void SetupSplineCashFlowChart(Chart chart, string chartTitle, List<ThuChiTheoNgayDTO> data)
        {
            if (chart == null) return;

            chart.Series.Clear();
            chart.ChartAreas.Clear();
            chart.Titles.Clear();
            chart.Legends.Clear();

            chart.BackColor = Color.White;

            Title title = new Title(chartTitle, Docking.Top, new Font("Segoe UI", 11F, FontStyle.Bold), Color.FromArgb(30, 41, 59));
            title.Alignment = ContentAlignment.MiddleLeft;
            chart.Titles.Add(title);

            Legend legend = new Legend("LegendThuChi")
            {
                Docking = Docking.Top,
                Alignment = StringAlignment.Far,
                Font = new Font("Segoe UI", 8.5F, FontStyle.Bold),
                ForeColor = LabelTextColor,
                BackColor = Color.Transparent
            };
            chart.Legends.Add(legend);

            ChartArea area = new ChartArea("AreaThuChi");
            StyleChartArea(area, "Thời gian", "Số tiền (VNĐ)");
            chart.ChartAreas.Add(area);

            // Series Thu
            Series seriesThu = new Series("Tiền Thu (Vào)")
            {
                ChartType = SeriesChartType.Spline,
                Color = Color.FromArgb(16, 185, 129),
                BorderWidth = 3,
                MarkerStyle = MarkerStyle.Circle,
                MarkerSize = 7,
                MarkerColor = Color.FromArgb(5, 150, 105),
                Legend = "LegendThuChi"
            };

            // Series Chi
            Series seriesChi = new Series("Tiền Chi (Ra)")
            {
                ChartType = SeriesChartType.Spline,
                Color = Color.FromArgb(239, 68, 68),
                BorderWidth = 3,
                MarkerStyle = MarkerStyle.Diamond,
                MarkerSize = 7,
                MarkerColor = Color.FromArgb(220, 38, 38),
                Legend = "LegendThuChi"
            };

            if (data != null)
            {
                foreach (var item in data)
                {
                    int ptThu = seriesThu.Points.AddXY(item.NhanNgay, (double)item.TongThu);
                    seriesThu.Points[ptThu].ToolTip = string.Format("Ngày: {0}\nTiền thu: {1:N0} VNĐ", item.NhanNgay, item.TongThu);

                    int ptChi = seriesChi.Points.AddXY(item.NhanNgay, (double)item.TongChi);
                    seriesChi.Points[ptChi].ToolTip = string.Format("Ngày: {0}\nTiền chi: {1:N0} VNĐ\nChênh lệch: {2:N0} VNĐ", item.NhanNgay, item.TongChi, item.ChenhLech);
                }
            }

            chart.Series.Add(seriesThu);
            chart.Series.Add(seriesChi);
        }

        /// <summary>
        /// Tiện ích lưu ảnh biểu đồ thành tệp PNG
        /// </summary>
        public static bool ExportChartImage(Chart chart, string defaultName)
        {
            if (chart == null) return false;

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "Ảnh PNG (*.png)|*.png|Ảnh JPEG (*.jpg)|*.jpg";
                sfd.FileName = defaultName + "_" + DateTime.Now.ToString("yyyyMMdd_HHmm");
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    ChartImageFormat format = sfd.FileName.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase)
                        ? ChartImageFormat.Jpeg : ChartImageFormat.Png;
                    chart.SaveImage(sfd.FileName, format);
                    MessageBox.Show("Đã lưu biểu đồ thành công tại:\n" + sfd.FileName, "Xuất biểu đồ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return true;
                }
            }
            return false;
        }
    }
}
