using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using DNQH_KeToanBanHang.Helpers;

namespace DNQH_KeToanBanHang.Forms
{
    public partial class frmInChungTu : Form
    {
        private string _htmlContent = string.Empty;
        private string _defaultFileName = "ChungTu";

        public frmInChungTu()
        {
            InitializeComponent();
            ApplyFoundationDesign();
        }

        private void ApplyFoundationDesign()
        {
            UiStyler.Apply(this);
            UiStyler.StyleButton(btnIn, UiButtonRole.Primary);
            UiStyler.StyleButton(btnXemTruoc, UiButtonRole.Information);
            UiStyler.StyleButton(btnLuuFile, UiButtonRole.Secondary);
            UiStyler.StyleButton(btnDong, UiButtonRole.Secondary);

            flpActions.SendToBack();
            lblDocTitle.BringToFront();

            if (!SystemInformation.HighContrast)
            {
                pnlToolbar.BackColor = UiTheme.Surface;
                lblDocTitle.ForeColor = UiTheme.TextPrimary;
            }
        }

        /// <summary>
        /// Nạp nội dung HTML cần in và hiển thị lên WebBrowser.
        /// </summary>
        public void LoadHtmlContent(string title, string htmlContent, string defaultFileName = "ChungTu")
        {
            this.Text = title;
            lblDocTitle.Text = title;
            _htmlContent = htmlContent ?? string.Empty;
            _defaultFileName = defaultFileName ?? "ChungTu";

            webBrowser.DocumentText = _htmlContent;
        }

        /// <summary>
        /// Hiển thị hộp thoại xem trước và in chứng từ dạng modal.
        /// </summary>
        public static void ShowVoucher(Form owner, string title, string htmlContent, string defaultFileName = "ChungTu")
        {
            if (string.IsNullOrEmpty(htmlContent))
            {
                MessageBox.Show("Không có dữ liệu mẫu in để hiển thị.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (frmInChungTu form = new frmInChungTu())
            {
                if (owner != null && owner.Icon != null)
                {
                    form.Icon = owner.Icon;
                }
                form.LoadHtmlContent(title, htmlContent, defaultFileName);
                form.ShowDialog(owner);
            }
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            try
            {
                webBrowser.ShowPrintDialog();
            }
            catch (Exception ex)
            {
                AppLogger.Error("PRINT_VOUCHER", "Lỗi khi gọi hộp thoại in: " + ex.Message, ex);
                MessageBox.Show("Không thể mở hộp thoại in. Vui lòng kiểm tra lại cấu hình máy in của bạn.",
                    "Lỗi in ấn", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXemTruoc_Click(object sender, EventArgs e)
        {
            try
            {
                webBrowser.ShowPrintPreviewDialog();
            }
            catch (Exception ex)
            {
                AppLogger.Error("PRINT_PREVIEW", "Lỗi khi mở xem trước bản in: " + ex.Message, ex);
                MessageBox.Show("Không thể hiển thị xem trước trang in: " + ex.Message,
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnLuuFile_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "Tệp HTML (*.html)|*.html|Tất cả tệp (*.*)|*.*";
                sfd.FileName = string.Format("{0}_{1:yyyyMMdd_HHmmss}.html", _defaultFileName, DateTime.Now);
                sfd.Title = "Chọn vị trí lưu tệp mẫu in";

                if (sfd.ShowDialog(this) == DialogResult.OK)
                {
                    try
                    {
                        File.WriteAllText(sfd.FileName, _htmlContent, new UTF8Encoding(true));
                        MessageBox.Show(string.Format("Đã xuất mẫu in thành công ra tệp:\n{0}\n\nBạn có thể mở tệp này bằng Microsoft Edge hoặc Google Chrome và nhấn Ctrl+P để 'Lưu dưới dạng PDF' (Save as PDF).", sfd.FileName),
                            "Xuất tệp thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        AppLogger.Error("SAVE_HTML_VOUCHER", "Lỗi khi lưu tệp HTML mẫu in: " + ex.Message, ex);
                        MessageBox.Show("Không thể lưu tệp: " + ex.Message, "Lỗi lưu tệp", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
