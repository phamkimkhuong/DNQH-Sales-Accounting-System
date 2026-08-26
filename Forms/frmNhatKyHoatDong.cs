using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using DNQH_KeToanBanHang.DataAccess;
using DNQH_KeToanBanHang.Helpers;
using DNQH_KeToanBanHang.Models;

namespace DNQH_KeToanBanHang.Forms
{
    public partial class frmNhatKyHoatDong : Form
    {
        private List<NhatKyHoatDong> currentLogs = new List<NhatKyHoatDong>();
        private readonly UiPaginationControl _pager;

        public frmNhatKyHoatDong()
        {
            InitializeComponent();
            dgvNhatKy.AutoGenerateColumns = false;

            _pager = new UiPaginationControl();
            _pager.PageChanged += async delegate(object sender, PageChangedEventArgs e)
            {
                await TaiDuLieuNhatKyAsync(e.PageIndex, e.PageSize);
            };
            pnlBottomStatus.Controls.Clear();
            pnlBottomStatus.Height = 38;
            pnlBottomStatus.Controls.Add(_pager);
        }

        private async void frmNhatKyHoatDong_Load(object sender, EventArgs e)
        {
            // Kiểm tra phân quyền: Chỉ Quản trị viên mới được phép truy cập
            if (!SessionManager.IsAdmin())
            {
                MessageBox.Show("Chức năng tra cứu Nhật ký hoạt động chỉ dành riêng cho Quản trị viên hệ thống.",
                                "Từ chối truy cập", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
                return;
            }

            // Nạp bộ lọc nhân viên
            NapDanhSachNhanVien();

            // Mặc định chọn 7 ngày gần nhất
            cboKhoangThoiGian.SelectedIndex = 2; // 7 ngày gần nhất
            cboCapDo.SelectedIndex = 0; // Tất cả

            await TaiDuLieuNhatKyAsync(1);
        }

        private void NapDanhSachNhanVien()
        {
            try
            {
                DataTable dtNV = NhatKyHoatDongDal.LayDanhSachNhanVienCoHoatDong();
                DataTable dtSource = new DataTable();
                dtSource.Columns.Add("MaNV", typeof(string));
                dtSource.Columns.Add("HienThi", typeof(string));

                dtSource.Rows.Add("ALL", "-- Tất cả nhân viên --");
                foreach (DataRow row in dtNV.Rows)
                {
                    string ma = row["MaNV"].ToString();
                    string ten = row["TenNV"] != DBNull.Value ? row["TenNV"].ToString() : ma;
                    dtSource.Rows.Add(ma, string.Format("{0} - {1}", ma, ten));
                }

                cboNhanVien.DataSource = dtSource;
                cboNhanVien.DisplayMember = "HienThi";
                cboNhanVien.ValueMember = "MaNV";
                cboNhanVien.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi nạp danh sách nhân viên: " + ex.Message);
            }
        }

        private void cboKhoangThoiGian_SelectedIndexChanged(object sender, EventArgs e)
        {
            DateTime today = DateTime.Today;
            switch (cboKhoangThoiGian.SelectedIndex)
            {
                case 0: // Hôm nay
                    dtpTuNgay.Value = today;
                    dtpDenNgay.Value = today;
                    SetCustomDateEnabled(false);
                    break;
                case 1: // Hôm qua
                    dtpTuNgay.Value = today.AddDays(-1);
                    dtpDenNgay.Value = today.AddDays(-1);
                    SetCustomDateEnabled(false);
                    break;
                case 2: // 7 ngày gần nhất
                    dtpTuNgay.Value = today.AddDays(-6);
                    dtpDenNgay.Value = today;
                    SetCustomDateEnabled(false);
                    break;
                case 3: // 30 ngày gần nhất
                    dtpTuNgay.Value = today.AddDays(-29);
                    dtpDenNgay.Value = today;
                    SetCustomDateEnabled(false);
                    break;
                case 4: // Tháng này
                    dtpTuNgay.Value = new DateTime(today.Year, today.Month, 1);
                    dtpDenNgay.Value = today;
                    SetCustomDateEnabled(false);
                    break;
                case 5: // Tùy chọn...
                    SetCustomDateEnabled(true);
                    break;
            }
        }

        private void SetCustomDateEnabled(bool enabled)
        {
            dtpTuNgay.Enabled = enabled;
            dtpDenNgay.Enabled = enabled;
        }

        private async Task TaiDuLieuNhatKyAsync(int pageIndex = 1, int? pageSize = null)
        {
            btnTraCuu.Enabled = false;
            btnLamMoi.Enabled = false;
            Cursor = Cursors.WaitCursor;

            int size = pageSize.HasValue ? pageSize.Value : _pager.PageSize;
            DateTime tuNgay = dtpTuNgay.Value.Date;
            DateTime denNgay = dtpDenNgay.Value.Date.AddDays(1).AddTicks(-1);

            string maNV = cboNhanVien.SelectedValue != null ? cboNhanVien.SelectedValue.ToString() : null;
            string capDo = null;
            if (cboCapDo.SelectedIndex == 1) capDo = "INFO";
            else if (cboCapDo.SelectedIndex == 2) capDo = "WARN";
            else if (cboCapDo.SelectedIndex == 3) capDo = "ERROR";

            string tuKhoa = txtTuKhoa.Text.Trim();

            try
            {
                int countInfo = 0;
                int countWarn = 0;
                int countError = 0;
                PagedResult<NhatKyHoatDong> paged = null;

                await Task.Run(delegate
                {
                    paged = NhatKyHoatDongDal.LayDanhSachNhatKyPhanTrang(
                        tuNgay, denNgay, maNV, null, capDo, tuKhoa, pageIndex, size,
                        out countInfo, out countWarn, out countError);
                });

                currentLogs = paged != null ? paged.Items : new List<NhatKyHoatDong>();
                dgvNhatKy.AutoGenerateColumns = false;
                dgvNhatKy.DataSource = new System.ComponentModel.BindingList<NhatKyHoatDong>(currentLogs);

                int total = paged != null ? paged.TotalRecords : 0;
                _pager.UpdateState(pageIndex, size, total);

                if (total == 0)
                {
                    txtChiTietNoiDung.Text = "Không có nhật ký nào phù hợp với điều kiện tìm kiếm.";
                    lblDetailContext.Text = "Không có dữ liệu.";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể tải nhật ký hoạt động: " + ex.Message,
                                "Lỗi truy vấn", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnTraCuu.Enabled = true;
                btnLamMoi.Enabled = true;
                Cursor = Cursors.Default;
            }
        }

        private async void btnTraCuu_Click(object sender, EventArgs e)
        {
            await TaiDuLieuNhatKyAsync(1);
        }

        private async void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtTuKhoa.Clear();
            cboCapDo.SelectedIndex = 0;
            cboNhanVien.SelectedIndex = 0;
            await TaiDuLieuNhatKyAsync(1);
        }

        private void txtTuKhoa_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnTraCuu.PerformClick();
            }
        }

        private void dgvNhatKy_SelectionChanged(object sender, EventArgs e)
        {
            NhatKyHoatDong log = dgvNhatKy.CurrentRow != null ? dgvNhatKy.CurrentRow.DataBoundItem as NhatKyHoatDong : null;
            if (log != null)
            {
                lblDetailContext.Text = string.Format(
                    "Thời gian: {0:yyyy-MM-dd HH:mm:ss} | Nhân viên: {1} ({2}) | Máy trạm: {3} | Thao tác: {4} | Thời gian: {5}ms",
                    log.ThoiGian, log.TenNV ?? log.MaNV, log.VaiTro, log.TenMay, log.HanhDong, log.ThoiGianXuLyMs);

                txtChiTietNoiDung.Text = log.NoiDung;
                btnSaoChepCorrelationId.Enabled = !string.IsNullOrEmpty(log.CorrelationId);
            }
            else
            {
                lblDetailContext.Text = "Chưa chọn bản ghi nào.";
                txtChiTietNoiDung.Clear();
                btnSaoChepCorrelationId.Enabled = false;
            }
        }

        private void dgvNhatKy_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvNhatKy.Rows.Count) return;

            var row = dgvNhatKy.Rows[e.RowIndex];
            NhatKyHoatDong item = row.DataBoundItem as NhatKyHoatDong;
            if (item != null)
            {
                // Format cột thời gian
                if (dgvNhatKy.Columns[e.ColumnIndex].Name == "colThoiGian" && e.Value != null && e.Value is DateTime)
                {
                    DateTime dt = (DateTime)e.Value;
                    e.Value = dt.ToString("yyyy-MM-dd HH:mm:ss");
                    e.FormattingApplied = true;
                }

                // Highlight theo cấp độ
                if (item.CapDo == "ERROR")
                {
                    row.DefaultCellStyle.BackColor = Color.FromArgb(254, 242, 242); // Hồng nhạt
                    row.DefaultCellStyle.ForeColor = Color.FromArgb(153, 27, 27);    // Đỏ sẫm
                    row.DefaultCellStyle.SelectionBackColor = Color.FromArgb(252, 165, 165);
                    row.DefaultCellStyle.SelectionForeColor = Color.FromArgb(127, 29, 29);
                }
                else if (item.CapDo == "WARN")
                {
                    row.DefaultCellStyle.BackColor = Color.FromArgb(254, 249, 195); // Vàng nhạt
                    row.DefaultCellStyle.ForeColor = Color.FromArgb(133, 77, 14);    // Nâu cam
                    row.DefaultCellStyle.SelectionBackColor = Color.FromArgb(253, 224, 71);
                    row.DefaultCellStyle.SelectionForeColor = Color.FromArgb(113, 63, 18);
                }

                // Highlight kết quả thất bại
                if (dgvNhatKy.Columns[e.ColumnIndex].Name == "colKetQua" && item.KetQua != null && item.KetQua.Contains("Thất bại"))
                {
                    e.CellStyle.ForeColor = Color.FromArgb(220, 38, 38);
                    e.CellStyle.Font = new Font(dgvNhatKy.Font, FontStyle.Bold);
                }
            }
        }

        private void btnSaoChepCorrelationId_Click(object sender, EventArgs e)
        {
            NhatKyHoatDong log = dgvNhatKy.CurrentRow != null ? dgvNhatKy.CurrentRow.DataBoundItem as NhatKyHoatDong : null;
            if (log != null)
            {
                if (!string.IsNullOrEmpty(log.CorrelationId))
                {
                    try
                    {
                        Clipboard.SetText(log.CorrelationId);
                        btnSaoChepCorrelationId.Text = "✓ Đã sao chép!";
                        Timer t = new Timer { Interval = 1500 };
                        t.Tick += (s, ev) =>
                        {
                            btnSaoChepCorrelationId.Text = "📋 Sao chép mã truy vết";
                            t.Stop();
                            t.Dispose();
                        };
                        t.Start();
                    }
                    catch
                    {
                        MessageBox.Show("Mã truy vết: " + log.CorrelationId, "Mã CorrelationId", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private async void btnDonDepLog_Click(object sender, EventArgs e)
        {
            DialogResult confirm = MessageBox.Show(
                "Bạn có chắc chắn muốn dọn dẹp các bản ghi nhật ký hoạt động cũ hơn 30 ngày trên cơ sở dữ liệu máy chủ?\n\nThao tác này giúp tối ưu hóa dung lượng lưu trữ và tăng tốc độ truy vấn.",
                "Xác nhận dọn dẹp nhật ký hệ thống",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return;

            btnDonDepLog.Enabled = false;
            Cursor = Cursors.WaitCursor;
            try
            {
                int deletedDb = await Task.Run(() => NhatKyHoatDongDal.DonDepNhatKy(30));
                int deletedFile = await Task.Run(() => AppLogger.CleanOldLogs(30));

                MessageBox.Show(
                    string.Format("Đã hoàn tất dọn dẹp nhật ký hệ thống:\n- Cơ sở dữ liệu tập trung: Đã xóa {0:N0} bản ghi quá hạn 30 ngày.\n- Tệp lưu trữ cục bộ: Đã xóa {1:N0} tệp log cũ.", deletedDb, deletedFile),
                    "Dọn dẹp thành công",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                await TaiDuLieuNhatKyAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra khi dọn dẹp nhật ký: " + ex.Message,
                                "Lỗi dọn dẹp", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnDonDepLog.Enabled = true;
                Cursor = Cursors.Default;
            }
        }
    }
}
