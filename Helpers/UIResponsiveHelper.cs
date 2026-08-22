using System;
using System.Drawing;
using System.Windows.Forms;

namespace DNQH_KeToanBanHang.Helpers
{
    /// <summary>
    /// Tiện ích hỗ trợ giao diện thông minh & co giãn phản hồi (Responsive UI):
    /// - Tự động tính toán DropDownWidth cho ComboBox để không bao giờ bị cắt ngắn (...)
    /// - Tự động bật AutoEllipsis + ToolTip cho Label thông tin dài (tên khách hàng, mã hóa đơn,...)
    /// - Tự động gắn ToolTip và co giãn ngang (Anchor Top-Left-Right) cho các trường ghi chú, diễn giải, lý do
    /// </summary>
    public static class UIResponsiveHelper
    {
        private static readonly ToolTip _sharedToolTip = new ToolTip
        {
            InitialDelay = 300,
            ReshowDelay = 150,
            AutoPopDelay = 12000,
            ShowAlways = true
        };

        private static Icon _cachedAppIcon = null;
        private static bool _iconLoadAttempted = false;

        /// <summary>
        /// Gán biểu tượng ứng dụng DNQH (App Icon) cho Form từ assets/DNQH_App.ico hoặc trích xuất từ file thực thi.
        /// </summary>
        public static void SetAppIcon(this Form form)
        {
            if (form == null) return;
            try
            {
                if (!_iconLoadAttempted)
                {
                    _iconLoadAttempted = true;
                    string icoPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "assets", "DNQH_App.ico");
                    if (System.IO.File.Exists(icoPath))
                    {
                        _cachedAppIcon = new Icon(icoPath);
                    }
                    else
                    {
                        string exePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
                        if (System.IO.File.Exists(exePath))
                        {
                            _cachedAppIcon = Icon.ExtractAssociatedIcon(exePath);
                        }
                    }
                }

                if (_cachedAppIcon != null)
                {
                    form.Icon = _cachedAppIcon;
                }
            }
            catch
            {
                // Bỏ qua nếu có sự cố, giữ icon mặc định an toàn
            }
        }

        /// <summary>
        /// Áp dụng toàn bộ kiến trúc Responsive UI cho Form:
        /// - Tự động thiết lập biểu tượng App Icon đồng bộ cho Form.
        /// - Tự động mở rộng DropDownWidth cho toàn bộ ComboBox.
        /// - Tự động thêm ToolTip cho Label và ComboBox.
        /// - Co giãn các trường ghi chú/diễn giải khi phóng to cửa sổ.
        /// </summary>
        public static void ApplyResponsiveUI(this Form form)
        {
            if (form == null) return;
            form.SetAppIcon();
            ApplyToContainer(form);
        }

        private static void ApplyToContainer(Control container)
        {
            foreach (Control ctrl in container.Controls)
            {
                ComboBox cbo = ctrl as ComboBox;
                if (cbo != null)
                {
                    cbo.EnableAutoDropDownWidth();
                }
                else
                {
                    TextBox txt = ctrl as TextBox;
                    if (txt != null)
                    {
                        string name = (txt.Name ?? string.Empty).ToLowerInvariant();
                        if (name.Contains("diengiai") || name.Contains("ghichu") || name.Contains("lydo") || txt.Multiline)
                        {
                            txt.EnableResponsiveInput();
                        }
                    }
                    else
                    {
                        Label lbl = ctrl as Label;
                        if (lbl != null)
                        {
                            string name = (lbl.Name ?? string.Empty).ToLowerInvariant();
                            if (name.Contains("info") || name.Contains("thongtin") || name.Contains("khachhang") || name.Contains("conlai") || name.Contains("tongtien"))
                            {
                                lbl.EnableSmartLabel();
                            }
                        }
                    }
                }

                if (ctrl.HasChildren)
                {
                    ApplyToContainer(ctrl);
                }
            }
        }

        /// <summary>
        /// Tự động mở rộng DropDownWidth của ComboBox dựa trên nội dung dài nhất của các item.
        /// Khi menu thả xuống mở ra, toàn bộ chuỗi văn bản (Mã - Tên - Số tiền) được hiển thị đầy đủ, không bị cắt xén.
        /// </summary>
        public static void EnableAutoDropDownWidth(this ComboBox cbo, int minExtra = 40)
        {
            if (cbo == null) return;

            // Xử lý khi menu thả xuống mở ra
            cbo.DropDown -= Cbo_DropDown;
            cbo.DropDown += Cbo_DropDown;

            // Gán ToolTip hiển thị text đầy đủ khi chọn hoặc đổi item
            cbo.SelectedIndexChanged -= Cbo_SelectedIndexChanged;
            cbo.SelectedIndexChanged += Cbo_SelectedIndexChanged;

            // Đo ngay nếu đã có sẵn items
            if (cbo.Items.Count > 0)
            {
                AdjustDropDownWidth(cbo, minExtra);
            }
        }

        private static void Cbo_DropDown(object sender, EventArgs e)
        {
            ComboBox cbo = sender as ComboBox;
            if (cbo != null)
            {
                AdjustDropDownWidth(cbo, 40);
            }
        }

        private static void Cbo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cbo = sender as ComboBox;
            if (cbo != null)
            {
                if (cbo.SelectedItem != null)
                {
                    string text = cbo.GetItemText(cbo.SelectedItem);
                    _sharedToolTip.SetToolTip(cbo, text);
                }
            }
        }

        /// <summary>
        /// Đo đạc văn bản và điều chỉnh DropDownWidth của ComboBox
        /// </summary>
        public static void AdjustDropDownWidth(ComboBox cbo, int minExtra = 40)
        {
            if (cbo == null || cbo.Items.Count == 0) return;

            try
            {
                int maxWidth = cbo.Width;
                int scrollBarWidth = (cbo.Items.Count > cbo.MaxDropDownItems) ? SystemInformation.VerticalScrollBarWidth : 0;

                using (Graphics g = cbo.CreateGraphics())
                {
                    foreach (object item in cbo.Items)
                    {
                        string text = cbo.GetItemText(item);
                        if (!string.IsNullOrEmpty(text))
                        {
                            Size size = TextRenderer.MeasureText(g, text, cbo.Font);
                            if (size.Width > maxWidth)
                            {
                                maxWidth = size.Width;
                            }
                        }
                    }
                }

                // Đảm bảo chiều rộng tối thiểu 450px cho các combobox chứa dữ liệu kép
                int targetWidth = maxWidth + scrollBarWidth + minExtra;
                if (targetWidth < 450 && cbo.Items.Count > 1)
                {
                    targetWidth = Math.Max(cbo.Width, 450);
                }

                Rectangle workingArea = Screen.FromControl(cbo).WorkingArea;
                int maximumWidth = Math.Max(cbo.Width, workingArea.Width - 48);
                cbo.DropDownWidth = Math.Min(targetWidth, maximumWidth);
            }
            catch
            {
                Rectangle workingArea = Screen.FromControl(cbo).WorkingArea;
                cbo.DropDownWidth = Math.Min(
                    Math.Max(cbo.Width, 450),
                    Math.Max(cbo.Width, workingArea.Width - 48));
            }
        }

        /// <summary>
        /// Bật tính năng AutoEllipsis cho Label kèm theo ToolTip hiển thị toàn bộ nội dung khi rê chuột.
        /// </summary>
        public static void EnableSmartLabel(this Label lbl)
        {
            if (lbl == null) return;
            lbl.AutoEllipsis = true;

            lbl.TextChanged -= Lbl_TextChanged;
            lbl.TextChanged += Lbl_TextChanged;

            if (!string.IsNullOrEmpty(lbl.Text))
            {
                _sharedToolTip.SetToolTip(lbl, lbl.Text);
            }
        }

        private static void Lbl_TextChanged(object sender, EventArgs e)
        {
            Label lbl = sender as Label;
            if (lbl != null)
            {
                _sharedToolTip.SetToolTip(lbl, lbl.Text);
            }
        }

        /// <summary>
        /// Cấu hình TextBox tự động co giãn theo chiều ngang (Anchor: Top | Left | Right)
        /// và gắn ToolTip hiển thị nội dung đầy đủ
        /// </summary>
        public static void EnableResponsiveInput(this TextBox txt)
        {
            if (txt == null) return;
            if (txt.Dock == DockStyle.None)
            {
                txt.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            }

            txt.TextChanged -= Txt_TextChanged;
            txt.TextChanged += Txt_TextChanged;

            if (!string.IsNullOrEmpty(txt.Text) && txt.Text.Length > 25)
            {
                _sharedToolTip.SetToolTip(txt, txt.Text);
            }
        }

        private static void Txt_TextChanged(object sender, EventArgs e)
        {
            TextBox txt = sender as TextBox;
            if (txt != null)
            {
                if (!string.IsNullOrEmpty(txt.Text) && txt.Text.Length > 25)
                {
                    _sharedToolTip.SetToolTip(txt, txt.Text);
                }
                else
                {
                    _sharedToolTip.SetToolTip(txt, null);
                }
            }
        }

        /// <summary>
        /// Gán ToolTip cho bất kỳ control nào
        /// </summary>
        public static void SetToolTip(Control ctrl, string caption)
        {
            if (ctrl == null) return;
            _sharedToolTip.SetToolTip(ctrl, caption);
        }
    }
}
