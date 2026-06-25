using System;
using System.Drawing;
using System.Windows.Forms;
using DNQH_KeToanBanHang.Helpers;
using DNQH_KeToanBanHang.Services;

namespace DNQH_KeToanBanHang.Forms
{
    public partial class frmDangNhap : Form
    {
        private readonly AuthService authService;
        private readonly ErrorProvider validationErrors;
        private readonly Label lblLoginStatus;

        public frmDangNhap()
        {
            InitializeComponent();
            validationErrors = UiStyler.CreateErrorProvider(this);
            lblLoginStatus = CreateStatusLabel();
            ApplyFoundationDesign();
            authService = new AuthService();
        }

        private Label CreateStatusLabel()
        {
            ClientSize = new Size(460, 388);
            Label label = new Label
            {
                AutoEllipsis = true,
                Location = new Point(36, 326),
                Name = "lblLoginStatus",
                Size = new Size(388, 48),
                TextAlign = ContentAlignment.MiddleLeft
            };
            Controls.Add(label);
            label.BringToFront();
            return label;
        }

        private void ApplyFoundationDesign()
        {
            UiStyler.Apply(this);
            UiStyler.StyleButton(btnDangNhap, UiButtonRole.Primary);
            UiStyler.StyleButton(btnThoat, UiButtonRole.Secondary);
            UiStyler.StyleStatusLabel(lblLoginStatus, UiStatusKind.Neutral, string.Empty);

            UiStyler.SetAccessibleText(txtTenDangNhap, "Tên đăng nhập", "Nhập tên tài khoản được cấp.");
            UiStyler.SetAccessibleText(txtMatKhau, "Mật khẩu", "Nhập mật khẩu của tài khoản.");
            UiStyler.SetAccessibleText(lblLoginStatus, "Trạng thái đăng nhập", "Thông báo kết quả xác thực.");

        }

        private void frmDangNhap_Load(object sender, EventArgs e)
        {
            this.ApplyResponsiveUI();
            this.AcceptButton = btnDangNhap;
            this.CancelButton = btnThoat;

            bool remember = UserPreferenceHelper.GetRememberUsername();
            chkGhiNhoDangNhap.Checked = remember;

            if (remember)
            {
                string savedUser = UserPreferenceHelper.GetSavedUsername();
                txtTenDangNhap.Text = savedUser;
                txtMatKhau.Clear();

                if (!string.IsNullOrEmpty(savedUser))
                {
                    txtMatKhau.Focus();
                }
                else
                {
                    txtTenDangNhap.Focus();
                }
            }
            else
            {
                txtTenDangNhap.Clear();
                txtMatKhau.Clear();
                txtTenDangNhap.Focus();
            }
        }

        private void chkHienMatKhau_CheckedChanged(object sender, EventArgs e)
        {
            txtMatKhau.PasswordChar = chkHienMatKhau.Checked ? '\0' : '•';
        }

        private void txtTenDangNhap_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                txtMatKhau.Focus();
            }
        }

        private void txtMatKhau_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                ThucHienDangNhap();
            }
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            ThucHienDangNhap();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ThucHienDangNhap()
        {
            string username = txtTenDangNhap.Text.Trim();
            string password = txtMatKhau.Text;
            validationErrors.Clear();
            UiStyler.StyleStatusLabel(lblLoginStatus, UiStatusKind.Neutral, string.Empty);

            if (string.IsNullOrEmpty(username))
            {
                UiInteractionHelper.ShowValidationError(validationErrors, txtTenDangNhap, "Vui lòng nhập tên đăng nhập.");
                UiStyler.StyleStatusLabel(lblLoginStatus, UiStatusKind.Warning, "Vui lòng nhập tên đăng nhập để tiếp tục.");
                return;
            }

            if (string.IsNullOrEmpty(password))
            {
                UiInteractionHelper.ShowValidationError(validationErrors, txtMatKhau, "Vui lòng nhập mật khẩu.");
                UiStyler.StyleStatusLabel(lblLoginStatus, UiStatusKind.Warning, "Vui lòng nhập mật khẩu để tiếp tục.");
                return;
            }

            btnDangNhap.Enabled = false;
            btnDangNhap.Text = "ĐANG ĐĂNG NHẬP...";
            Cursor = Cursors.WaitCursor;
            UiStyler.StyleStatusLabel(lblLoginStatus, UiStatusKind.Information, "Đang xác thực tài khoản, vui lòng chờ...");

            try
            {
                LoginResult result = authService.Login(username, password);

                if (!result.Success)
                {
                    UiStyler.StyleStatusLabel(lblLoginStatus, UiStatusKind.Error, result.Message);
                    UiInteractionHelper.ShowValidationError(validationErrors, txtMatKhau, result.Message);
                    return;
                }

                // Lưu tùy chọn ghi nhớ tên đăng nhập an toàn
                UserPreferenceHelper.SaveLoginPreference(chkGhiNhoDangNhap.Checked, username);

                // Đăng nhập thành công -> Ẩn frmDangNhap và mở frmMain
                this.Hide();

                using (frmMain mainForm = new frmMain())
                {
                    mainForm.ShowDialog();
                }

                // Khi frmMain đóng (do đăng xuất hoặc đóng form)
                txtMatKhau.Clear();
                validationErrors.Clear();
                UiStyler.StyleStatusLabel(lblLoginStatus, UiStatusKind.Neutral, "Phiên làm việc đã kết thúc. Vui lòng đăng nhập lại.");
                if (chkGhiNhoDangNhap.Checked && !string.IsNullOrEmpty(txtTenDangNhap.Text))
                {
                    txtMatKhau.Focus();
                }
                else
                {
                    txtTenDangNhap.Clear();
                    txtTenDangNhap.Focus();
                }
                this.Show();
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                string correlationId = Guid.NewGuid().ToString("N");
                AppLogger.Error("LOGIN_DB_ERROR", "Lỗi kết nối cơ sở dữ liệu khi đăng nhập: " + ex.Message, ex, correlationId, username);
                UiStyler.StyleStatusLabel(
                    lblLoginStatus,
                    UiStatusKind.Error,
                    string.Format("Không thể kết nối dữ liệu. Mã tra cứu: {0}", correlationId));
            }
            catch (Exception ex)
            {
                string correlationId = Guid.NewGuid().ToString("N");
                AppLogger.Error("LOGIN_SYSTEM_ERROR", "Lỗi không xác định khi đăng nhập: " + ex.Message, ex, correlationId, username);
                UiStyler.StyleStatusLabel(
                    lblLoginStatus,
                    UiStatusKind.Error,
                    string.Format("Không thể hoàn tất đăng nhập. Mã tra cứu: {0}", correlationId));
            }
            finally
            {
                btnDangNhap.Enabled = true;
                btnDangNhap.Text = "ĐĂNG NHẬP";
                Cursor = Cursors.Default;
            }
        }
    }
}
