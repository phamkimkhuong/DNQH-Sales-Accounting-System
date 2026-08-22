using System;
using System.Drawing;
using System.Windows.Forms;
using DNQH_KeToanBanHang.Helpers;

namespace DNQH_KeToanBanHang.Forms
{
    public partial class frmHotkeysHelp : Form
    {
        public frmHotkeysHelp()
        {
            InitializeComponent();
            ApplyStyling();
        }

        private void ApplyStyling()
        {
            pnlHeader.BackColor = Color.FromArgb(30, 58, 138);
            pnlFooter.BackColor = UiTheme.SurfaceMuted;
            btnDong.BackColor = Color.FromArgb(30, 58, 138);

            picHeaderIcon.Image = UiIconProvider.GetIcon(UiIconType.Keyboard, 24, Color.White);
            picTipIcon.Image = UiIconProvider.GetIcon(UiIconType.Help, 18, Color.FromArgb(217, 119, 6));

            ImageList tabImageList = new ImageList { ImageSize = new Size(16, 16), ColorDepth = ColorDepth.Depth32Bit };
            tabImageList.Images.Add("voucher", UiIconProvider.GetIcon(UiIconType.Voucher, 16, UiTheme.Primary));
            tabImageList.Images.Add("home", UiIconProvider.GetIcon(UiIconType.Home, 16, UiTheme.Primary));
            tabImageList.Images.Add("keyboard", UiIconProvider.GetIcon(UiIconType.Keyboard, 16, UiTheme.Primary));
            tcCategories.ImageList = tabImageList;

            tpChung.ImageKey = "voucher";
            tpDieuHuong.ImageKey = "home";
            tpBanPhim.ImageKey = "keyboard";

            ConfigureGrid(dgvChung);
            ConfigureGrid(dgvDieuHuong);
            ConfigureGrid(dgvBanPhim);
        }

        private void ConfigureGrid(DataGridView dgv)
        {
            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(30, 58, 138);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            dgv.ColumnHeadersHeight = 36;
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular);
            dgv.DefaultCellStyle.ForeColor = UiTheme.TextPrimary;
            dgv.DefaultCellStyle.SelectionBackColor = UiTheme.Selection;
            dgv.DefaultCellStyle.SelectionForeColor = UiTheme.SelectionText;
            dgv.RowTemplate.Height = 32;
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.GridColor = UiTheme.Border;
        }

        private void frmHotkeysHelp_Load(object sender, EventArgs e)
        {
            LoadDataChung();
            LoadDataDieuHuong();
            LoadDataBanPhim();
        }

        private void LoadDataChung()
        {
            dgvChung.Rows.Clear();
            AddRowChung("F1", "Bật bảng tra cứu phím tắt toàn hệ thống", "Toàn bộ các màn hình");
            AddRowChung("F2 hoặc Ctrl + N", "Thêm mới bản ghi / Xóa trắng ô nhập liệu", "Danh mục (Khách hàng, SP, NCC...), Lập đơn hàng, Hóa đơn bán");
            AddRowChung("F3 hoặc Ctrl + S", "Lưu dữ liệu / Ghi sổ chứng từ", "Tất cả các màn hình nhập liệu chứng từ và cập nhật danh mục");
            AddRowChung("F5", "Nạp lại danh sách dữ liệu (Refresh)", "Mọi bảng danh sách dữ liệu (DataGridView)");
            AddRowChung("Ctrl + F", "Nhảy nhanh con trỏ vào ô tìm kiếm", "Mọi màn hình có thanh công cụ tìm kiếm");
            AddRowChung("Ctrl + P", "In hóa đơn / đơn hàng / phiếu thu / phiếu chi", "Đơn đặt hàng, Hóa đơn bán, Phiếu thu, Phiếu chi, Báo cáo");
            AddRowChung("Ctrl + E", "Xuất dữ liệu ra tệp Excel (.xls / SpreadsheetML)", "Mọi màn hình bảng dữ liệu có hỗ trợ xuất Excel");
            AddRowChung("Ctrl + I", "Nhập dữ liệu hàng loạt từ Excel / CSV", "Danh mục Khách hàng, Sản phẩm, Nhà cung cấp");
            AddRowChung("Delete", "Xóa dòng chi tiết đang chọn trong bảng", "Bảng giỏ hàng đơn đặt hàng, chi tiết hóa đơn bán");
            AddRowChung("Esc", "Đóng cửa sổ hiện tại / Hủy bỏ chế độ sửa", "Mọi cửa sổ form dialog, popup");
        }

        private void AddRowChung(string key, string action, string scope)
        {
            int idx = dgvChung.Rows.Add(key, action, scope);
            dgvChung.Rows[idx].Cells[0].Style.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            dgvChung.Rows[idx].Cells[0].Style.ForeColor = Color.FromArgb(30, 58, 138);
        }

        private void LoadDataDieuHuong()
        {
            dgvDieuHuong.Rows.Clear();
            AddRowDH("Ctrl + 1", "Đơn Đặt Hàng Bán", "Quản lý đơn hàng, tạo đơn mới, in đơn đặt hàng");
            AddRowDH("Ctrl + 2", "Hóa Đơn Bán Hàng", "Lập hóa đơn từ đơn hàng, in hóa đơn, theo dõi trạng thái");
            AddRowDH("Ctrl + 3", "Phiếu Thu Tiền", "Lập phiếu thu tiền bán hàng hoặc thu nợ khách hàng");
            AddRowDH("Ctrl + 4", "Phiếu Chi Tiền", "Lập phiếu chi tiền hoạt động doanh nghiệp");
            AddRowDH("Ctrl + 5", "Phiếu Xuất Kho", "Lập phiếu xuất kho giao hàng cho khách");
            AddRowDH("Ctrl + K", "Danh Mục Khách Hàng", "Hồ sơ khách hàng, công nợ, xuất/nhập Excel");
            AddRowDH("Ctrl + M", "Danh Mục Sản Phẩm", "Hàng hóa, bảng giá, đơn vị tính, xuất/nhập Excel");
            AddRowDH("Ctrl + B", "Báo Cáo Tổng Hợp", "Báo cáo doanh thu, trực quan hóa biểu đồ động");
            AddRowDH("Ctrl + T", "Kế Toán Chi Tiết", "Sổ quỹ tiền mặt, sổ chi tiết bán hàng, phân tích tuổi nợ");
        }

        private void AddRowDH(string key, string action, string scope)
        {
            int idx = dgvDieuHuong.Rows.Add(key, action, scope);
            dgvDieuHuong.Rows[idx].Cells[0].Style.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            dgvDieuHuong.Rows[idx].Cells[0].Style.ForeColor = Color.FromArgb(30, 58, 138);
        }

        private void LoadDataBanPhim()
        {
            dgvBanPhim.Rows.Clear();
            AddRowBP("Enter", "Chuyển sang ô nhập tiếp theo (Next Control)", "Tự động đưa con trỏ sang ô nhập liệu liền kề (bỏ qua nút bấm & ô văn bản nhiều dòng). Giúp kế toán nhập nhanh bằng bàn phím số Numpad.");
            AddRowBP("Shift + Enter", "Lùi về ô nhập trước đó", "Đưa con trỏ quay lại ô nhập liệu liền trước.");
            AddRowBP("Tab", "Nhảy ô tiếp theo", "Di chuyển focus tuần tự theo chuẩn giao diện Windows.");
            AddRowBP("Shift + Tab", "Lùi ô trước đó", "Di chuyển focus ngược chiều.");
            AddRowBP("Mũi tên ↑ / ↓", "Duyệt dòng trong bảng hoặc ComboBox", "Di chuyển chọn nhanh dòng trong bảng dữ liệu hoặc tùy chọn hộp danh sách.");
            AddRowBP("Phím cách (Space)", "Đánh dấu / Bỏ dấu CheckBox", "Bật tắt hộp kiểm (ví dụ: 'Chỉ nhập các dòng hợp lệ').");
            AddRowBP("F4 hoặc Alt + ↓", "Mở rộng danh sách ComboBox", "Mở nhanh danh sách lựa chọn của hộp chọn đang focus.");
        }

        private void AddRowBP(string key, string action, string desc)
        {
            int idx = dgvBanPhim.Rows.Add(key, action, desc);
            dgvBanPhim.Rows[idx].Cells[0].Style.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            dgvBanPhim.Rows[idx].Cells[0].Style.ForeColor = Color.FromArgb(30, 58, 138);
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmHotkeysHelp_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape || e.KeyCode == Keys.Enter)
            {
                this.Close();
                e.Handled = true;
            }
        }
    }
}
