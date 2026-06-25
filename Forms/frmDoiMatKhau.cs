using System;
using System.Drawing;
using System.Windows.Forms;
using DNQH_KeToanBanHang.Helpers;
using DNQH_KeToanBanHang.Services;

namespace DNQH_KeToanBanHang.Forms
{
    public partial class frmDoiMatKhau : Form
    {
        private readonly AuthService _authService;
        private readonly ErrorProvider _validationErrors;
        private Label _passwordHint;
        private Label _matchHint;
        private Label _statusLabel;

        public frmDoiMatKhau()
        {
            InitializeComponent();
            _validationErrors = UiStyler.CreateErrorProvider(this);
            BuildResponsiveLayout();
            ApplyFoundationDesign();
            _authService = new AuthService();
        }

        private void BuildResponsiveLayout()
        {
            SuspendLayout();
            ClientSize = new Size(460, 545);
            MinimumSize = new Size(440, 540);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            StartPosition = FormStartPosition.CenterParent;
            MaximizeBox = false;
            MinimizeBox = false;
            btnHuy.DialogResult = DialogResult.Cancel;
            txtTenDangNhap.TabStop = false;
            txtMatKhauCu.TabIndex = 0;
            txtMatKhauMoi.TabIndex = 1;
            txtXacNhanMatKhau.TabIndex = 2;
            chkHienMatKhau.TabIndex = 3;
            btnLuu.TabIndex = 4;
            btnHuy.TabIndex = 5;

            _passwordHint = CreateFeedbackLabel("Mật khẩu mới cần ít nhất 4 ký tự.");
            _passwordHint.Name = "lblPasswordHint";
            _matchHint = CreateFeedbackLabel("Nhập lại mật khẩu mới để xác nhận.");
            _matchHint.Name = "lblMatchHint";
            _statusLabel = new Label
            {
                Name = "lblChangePasswordStatus",
                Dock = DockStyle.Fill,
                AutoEllipsis = true,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(8, 0, 0, 0),
                Margin = new Padding(0)
            };

            TableLayoutPanel fields = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 11,
                Padding = new Padding(2, 0, 2, 0)
            };
            fields.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            AddStackedField(fields, lblTenDangNhap, txtTenDangNhap, 0);
            AddStackedField(fields, lblMatKhauCu, txtMatKhauCu, 2);
            AddStackedField(fields, lblMatKhauMoi, txtMatKhauMoi, 4);
            fields.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
            fields.Controls.Add(_passwordHint, 0, 6);
            AddStackedField(fields, lblXacNhanMatKhau, txtXacNhanMatKhau, 7);
            fields.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
            fields.Controls.Add(_matchHint, 0, 9);
            fields.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            chkHienMatKhau.Dock = DockStyle.Fill;
            chkHienMatKhau.Margin = new Padding(0, 2, 0, 0);
            fields.Controls.Add(chkHienMatKhau, 0, 10);

            FlowLayoutPanel actions = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.RightToLeft,
                WrapContents = false,
                Padding = new Padding(0, 7, 0, 0),
                Margin = new Padding(0)
            };
            btnLuu.Margin = new Padding(8, 0, 0, 0);
            btnHuy.Margin = new Padding(0);
            actions.Controls.Add(btnLuu);
            actions.Controls.Add(btnHuy);

            Panel heading = new Panel { Dock = DockStyle.Fill };
            lblTitle.AutoSize = true;
            lblTitle.Location = new Point(0, 0);
            Label subtitle = new Label
            {
                AutoSize = true,
                Location = new Point(2, 35),
                Text = "Cập nhật mật khẩu cho tài khoản đang đăng nhập.",
                ForeColor = UiTheme.TextSecondary
            };
            heading.Controls.Add(lblTitle);
            heading.Controls.Add(subtitle);

            Controls.Clear();
            TableLayoutPanel root = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 4,
                BackColor = UiTheme.Canvas,
                Padding = new Padding(24, 18, 24, 16)
            };
            root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            root.RowStyles.Add(new RowStyle(SizeType.Absolute, 66F));
            root.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            root.RowStyles.Add(new RowStyle(SizeType.Absolute, 58F));
            root.RowStyles.Add(new RowStyle(SizeType.Absolute, 28F));
            root.Controls.Add(heading, 0, 0);
            root.Controls.Add(fields, 0, 1);
            root.Controls.Add(actions, 0, 2);
            root.Controls.Add(_statusLabel, 0, 3);
            Controls.Add(root);
            AcceptButton = btnLuu;
            CancelButton = btnHuy;

            txtMatKhauMoi.TextChanged += PasswordFields_TextChanged;
            txtXacNhanMatKhau.TextChanged += PasswordFields_TextChanged;
            ResumeLayout(true);
        }

        private Label CreateFeedbackLabel(string text)
        {
            return new Label
            {
                Dock = DockStyle.Fill,
                AutoEllipsis = true,
                Text = text,
                TextAlign = ContentAlignment.MiddleLeft,
                ForeColor = UiTheme.TextSecondary,
                Margin = new Padding(0)
            };
        }

        private void AddStackedField(TableLayoutPanel layout, Label label, Control field, int labelRow)
        {
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 38F));
            label.AutoSize = true;
            label.Anchor = AnchorStyles.Left;
            label.Margin = new Padding(0, 0, 0, 2);
            field.Dock = DockStyle.Fill;
            field.Margin = new Padding(0, 2, 12, 5);
            layout.Controls.Add(label, 0, labelRow);
            layout.Controls.Add(field, 0, labelRow + 1);
        }

        private void ApplyFoundationDesign()
        {
            UiStyler.Apply(this);
            UiStyler.StyleButton(btnLuu, UiButtonRole.Primary);
            UiStyler.StyleButton(btnHuy, UiButtonRole.Secondary);
            UiStyler.StyleStatusLabel(_statusLabel, UiStatusKind.Neutral, "Nhập mật khẩu hiện tại và mật khẩu mới.");
            UiStyler.SetAccessibleText(txtTenDangNhap, "Tên đăng nhập", "Tài khoản đang được đổi mật khẩu.");
            UiStyler.SetAccessibleText(txtMatKhauCu, "Mật khẩu hiện tại", "Nhập mật khẩu đang sử dụng.");
            UiStyler.SetAccessibleText(txtMatKhauMoi, "Mật khẩu mới", "Mật khẩu mới cần có ít nhất 4 ký tự.");
            UiStyler.SetAccessibleText(txtXacNhanMatKhau, "Xác nhận mật khẩu", "Nhập lại chính xác mật khẩu mới.");
            UiStyler.SetAccessibleText(chkHienMatKhau, "Hiện mật khẩu", "Bật hoặc tắt hiển thị nội dung các trường mật khẩu.");
        }

        private void frmDoiMatKhau_Load(object sender, EventArgs e)
        {
            if (SessionManager.CurrentUser != null)
            {
                txtTenDangNhap.Text = SessionManager.CurrentUser.TenDangNhap;
            }
            else
            {
                this.Close();
            }
        }

        private void chkHienMatKhau_CheckedChanged(object sender, EventArgs e)
        {
            char pChar = chkHienMatKhau.Checked ? '\0' : '•';
            txtMatKhauCu.PasswordChar = pChar;
            txtMatKhauMoi.PasswordChar = pChar;
            txtXacNhanMatKhau.PasswordChar = pChar;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string oldPass = txtMatKhauCu.Text;
            string newPass = txtMatKhauMoi.Text;
            string confirmPass = txtXacNhanMatKhau.Text;

            if (string.IsNullOrWhiteSpace(oldPass))
            {
                ShowValidation(txtMatKhauCu, "Vui lòng nhập mật khẩu hiện tại.");
                return;
            }

            if (string.IsNullOrWhiteSpace(newPass))
            {
                ShowValidation(txtMatKhauMoi, "Vui lòng nhập mật khẩu mới.");
                return;
            }

            if (newPass.Length < 4)
            {
                ShowValidation(txtMatKhauMoi, "Mật khẩu mới phải có ít nhất 4 ký tự.");
                return;
            }

            if (newPass != confirmPass)
            {
                ShowValidation(txtXacNhanMatKhau, "Xác nhận mật khẩu mới không trùng khớp.");
                txtXacNhanMatKhau.SelectAll();
                return;
            }

            try
            {
                _validationErrors.Clear();
                btnLuu.Enabled = false;
                UseWaitCursor = true;
                UiStyler.StyleStatusLabel(_statusLabel, UiStatusKind.Information, "Đang cập nhật mật khẩu...");
                string message;
                bool success = _authService.DoiMatKhau(SessionManager.CurrentUser.MaTK, oldPass, newPass, out message);

                if (success)
                {
                    MessageBox.Show(message, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    UiInteractionHelper.ShowValidationError(_validationErrors, txtMatKhauCu, message);
                    UiStyler.StyleStatusLabel(_statusLabel, UiStatusKind.Error, message);
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                UiErrorHandler.Show(this, "CHANGE_PASSWORD_DB_ERROR",
                    "Không thể kết nối đến cơ sở dữ liệu để cập nhật mật khẩu.", ex);
            }
            catch (Exception ex)
            {
                UiErrorHandler.Show(this, "CHANGE_PASSWORD_ERROR",
                    "Không thể hoàn tất thao tác đổi mật khẩu.", ex);
            }
            finally
            {
                btnLuu.Enabled = true;
                UseWaitCursor = false;
            }
        }

        private void PasswordFields_TextChanged(object sender, EventArgs e)
        {
            string newPassword = txtMatKhauMoi.Text;
            string confirmation = txtXacNhanMatKhau.Text;

            if (newPassword.Length == 0)
            {
                SetFeedback(_passwordHint, "Mật khẩu mới cần ít nhất 4 ký tự.", UiStatusKind.Neutral);
            }
            else if (newPassword.Length < 4)
            {
                SetFeedback(_passwordHint, string.Format("Cần thêm {0} ký tự.", 4 - newPassword.Length), UiStatusKind.Warning);
            }
            else
            {
                SetFeedback(_passwordHint, "Đã đạt yêu cầu độ dài tối thiểu.", UiStatusKind.Success);
            }

            if (confirmation.Length == 0)
            {
                SetFeedback(_matchHint, "Nhập lại mật khẩu mới để xác nhận.", UiStatusKind.Neutral);
            }
            else if (newPassword == confirmation)
            {
                SetFeedback(_matchHint, "Mật khẩu xác nhận đã khớp.", UiStatusKind.Success);
                _validationErrors.SetError(txtXacNhanMatKhau, string.Empty);
            }
            else
            {
                SetFeedback(_matchHint, "Mật khẩu xác nhận chưa khớp.", UiStatusKind.Warning);
            }
        }

        private void SetFeedback(Label label, string text, UiStatusKind kind)
        {
            label.Text = text;
            if (SystemInformation.HighContrast)
            {
                label.ForeColor = SystemColors.ControlText;
                return;
            }

            switch (kind)
            {
                case UiStatusKind.Success:
                    label.ForeColor = UiTheme.Success;
                    break;
                case UiStatusKind.Warning:
                    label.ForeColor = UiTheme.Warning;
                    break;
                default:
                    label.ForeColor = UiTheme.TextSecondary;
                    break;
            }
        }

        private void ShowValidation(Control control, string message)
        {
            UiInteractionHelper.ShowValidationError(_validationErrors, control, message);
            UiStyler.StyleStatusLabel(_statusLabel, UiStatusKind.Warning, message);
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
