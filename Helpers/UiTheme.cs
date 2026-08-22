using System;
using System.Drawing;
using System.Windows.Forms;

namespace DNQH_KeToanBanHang.Helpers
{
    public enum UiButtonRole
    {
        Primary,
        Secondary,
        Success,
        Danger,
        Information
    }

    public enum UiStatusKind
    {
        Neutral,
        Information,
        Success,
        Warning,
        Error
    }

    public static class UiTheme
    {
        public static readonly Color Primary = Color.FromArgb(124, 58, 237);
        public static readonly Color PrimaryHover = Color.FromArgb(109, 40, 217);
        public static readonly Color Secondary = Color.FromArgb(100, 116, 139);
        public static readonly Color Canvas = Color.FromArgb(248, 250, 252);
        public static readonly Color Surface = Color.White;
        public static readonly Color SurfaceMuted = Color.FromArgb(241, 245, 249);
        public static readonly Color Border = Color.FromArgb(226, 232, 240);
        public static readonly Color TextPrimary = Color.FromArgb(15, 23, 42);
        public static readonly Color TextSecondary = Color.FromArgb(100, 116, 139);
        public static readonly Color Success = Color.FromArgb(4, 120, 87);
        public static readonly Color SuccessHover = Color.FromArgb(6, 95, 70);
        public static readonly Color Warning = Color.FromArgb(180, 83, 9);
        public static readonly Color Danger = Color.FromArgb(190, 18, 60);
        public static readonly Color DangerHover = Color.FromArgb(159, 18, 57);
        public static readonly Color Information = Color.FromArgb(3, 105, 161);
        public static readonly Color InformationHover = Color.FromArgb(7, 89, 133);
        public static readonly Color Selection = Color.FromArgb(237, 233, 254);
        public static readonly Color SelectionText = Color.FromArgb(46, 16, 101);
        public static readonly Color FocusSurface = Color.FromArgb(250, 247, 255);
        public static readonly Color Sidebar = Color.FromArgb(24, 20, 52);
        public static readonly Color SidebarHover = Color.FromArgb(49, 46, 89);
        public static readonly Color SidebarActive = Color.FromArgb(76, 29, 149);

        public const int PagePadding = 20;
        public const int SectionGap = 16;
        public const int ControlHeight = 34;
        public const int ButtonHeight = 38;
        public const int GridRowHeight = 32;
        public const int GridHeaderHeight = 38;
    }

    public static class UiStyler
    {
        private const string GridStatePanelName = "pnlGridState";

        public static void Apply(Form form)
        {
            if (form == null)
            {
                return;
            }

            ApplyAccessibility(form);
            UiInteractionHelper.Apply(form);
            if (SystemInformation.HighContrast)
            {
                return;
            }

            form.BackColor = UiTheme.Canvas;
            form.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            ApplyToContainer(form);
        }

        public static Button CreateButton(string text, UiButtonRole role = UiButtonRole.Secondary)
        {
            Button button = new Button
            {
                Text = text,
                AutoSize = true,
                Height = UiTheme.ButtonHeight
            };
            StyleButton(button, role);
            return button;
        }

        public static Button CreateButton(string text, Color backColor)
        {
            Button button = new Button
            {
                Text = text,
                AutoSize = true,
                Height = UiTheme.ButtonHeight
            };
            StyleButton(button, UiButtonRole.Secondary);
            button.BackColor = backColor;
            button.ForeColor = Color.White;
            button.FlatAppearance.BorderSize = 0;
            return button;
        }

        public static void StyleButton(Button button, UiButtonRole role)
        {
            if (button == null)
            {
                return;
            }

            button.Cursor = Cursors.Hand;
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = role == UiButtonRole.Secondary ? 1 : 0;
            button.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            button.MinimumSize = new Size(88, UiTheme.ButtonHeight);
            button.UseVisualStyleBackColor = false;

            if (SystemInformation.HighContrast)
            {
                return;
            }

            switch (role)
            {
                case UiButtonRole.Primary:
                    SetSolidButton(button, UiTheme.Primary, UiTheme.PrimaryHover);
                    break;
                case UiButtonRole.Success:
                    SetSolidButton(button, UiTheme.Success, UiTheme.SuccessHover);
                    break;
                case UiButtonRole.Danger:
                    SetSolidButton(button, UiTheme.Danger, UiTheme.DangerHover);
                    break;
                case UiButtonRole.Information:
                    SetSolidButton(button, UiTheme.Information, UiTheme.InformationHover);
                    break;
                default:
                    button.BackColor = UiTheme.Surface;
                    button.ForeColor = UiTheme.TextPrimary;
                    button.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225);
                    button.FlatAppearance.MouseOverBackColor = UiTheme.SurfaceMuted;
                    button.FlatAppearance.MouseDownBackColor = UiTheme.Border;
                    break;
            }
        }

        public static void StyleNavigationButton(Button button, bool active)
        {
            if (button == null)
            {
                return;
            }

            button.Cursor = Cursors.Hand;
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.TextAlign = ContentAlignment.MiddleLeft;
            button.Padding = new Padding(active ? 16 : 12, 0, 8, 0);

            if (SystemInformation.HighContrast)
            {
                return;
            }

            button.ForeColor = Color.White;
            button.BackColor = active ? UiTheme.SidebarActive : UiTheme.Sidebar;
            button.FlatAppearance.MouseOverBackColor = active ? UiTheme.SidebarActive : UiTheme.SidebarHover;
            button.FlatAppearance.MouseDownBackColor = UiTheme.PrimaryHover;
        }

        public static void StyleDataGridView(DataGridView grid)
        {
            if (grid == null)
            {
                return;
            }

            grid.AllowUserToAddRows = false;
            grid.AllowUserToDeleteRows = false;
            grid.MultiSelect = false;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.RowHeadersVisible = false;
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            grid.AllowUserToResizeRows = false;
            grid.RowTemplate.Height = UiTheme.GridRowHeight;
            grid.ColumnHeadersHeight = UiTheme.GridHeaderHeight;
            grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            grid.ColumnAdded -= Grid_ColumnAdded;
            grid.ColumnAdded += Grid_ColumnAdded;
            grid.DataBindingComplete -= Grid_DataBindingComplete;
            grid.DataBindingComplete += Grid_DataBindingComplete;
            grid.RowsAdded -= Grid_RowsChanged;
            grid.RowsAdded += Grid_RowsChanged;
            grid.RowsRemoved -= Grid_RowsChanged;
            grid.RowsRemoved += Grid_RowsChanged;
            grid.Resize -= Grid_Resize;
            grid.Resize += Grid_Resize;
            ApplySemanticGridColumns(grid);
            HideInternalColumns(grid);
            UpdateGridEmptyState(grid, "Không có dữ liệu để hiển thị.");

            if (SystemInformation.HighContrast)
            {
                return;
            }

            grid.BackgroundColor = UiTheme.Surface;
            grid.BorderStyle = BorderStyle.FixedSingle;
            grid.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            grid.GridColor = UiTheme.Border;
            grid.EnableHeadersVisualStyles = false;
            grid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            grid.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = UiTheme.SurfaceMuted,
                ForeColor = Color.FromArgb(51, 65, 85),
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Alignment = DataGridViewContentAlignment.MiddleLeft,
                SelectionBackColor = UiTheme.SurfaceMuted,
                SelectionForeColor = Color.FromArgb(51, 65, 85),
                WrapMode = DataGridViewTriState.False
            };
            grid.DefaultCellStyle.BackColor = UiTheme.Surface;
            grid.DefaultCellStyle.ForeColor = UiTheme.TextPrimary;
            grid.DefaultCellStyle.Font = new Font("Segoe UI", 9F);
            grid.DefaultCellStyle.SelectionBackColor = UiTheme.Selection;
            grid.DefaultCellStyle.SelectionForeColor = UiTheme.SelectionText;
            grid.DefaultCellStyle.Padding = new Padding(4, 0, 4, 0);
            grid.AlternatingRowsDefaultCellStyle.BackColor = UiTheme.Canvas;
        }

        public static void ApplySemanticGridColumns(DataGridView grid)
        {
            if (grid == null)
            {
                return;
            }

            foreach (DataGridViewColumn column in grid.Columns)
            {
                ApplySemanticGridColumn(column);
                column.HeaderCell.Style.WrapMode = DataGridViewTriState.False;
            }
        }

        private static void Grid_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            ApplySemanticGridColumn(e.Column);
        }

        private static void Grid_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            DataGridView grid = sender as DataGridView;
            if (grid == null)
            {
                return;
            }

            HideInternalColumns(grid);
            UpdateGridEmptyState(grid, "Không có dữ liệu phù hợp.");
        }

        private static void Grid_RowsChanged(object sender, EventArgs e)
        {
            UpdateGridEmptyState(sender as DataGridView, "Không có dữ liệu phù hợp.");
        }

        private static void Grid_Resize(object sender, EventArgs e)
        {
            PositionGridStatePanel(sender as DataGridView);
        }

        public static void SetGridLoading(DataGridView grid, string message)
        {
            ShowGridState(grid, "loading", "…", message, UiStatusKind.Information);
        }

        public static void SetGridError(DataGridView grid, string message)
        {
            ShowGridState(grid, "error", "!", message, UiStatusKind.Error);
        }

        public static void ClearGridState(DataGridView grid)
        {
            Panel panel = FindGridStatePanel(grid);
            if (panel != null)
            {
                panel.Parent = null;
                panel.Dispose();
            }
        }

        public static void UpdateGridEmptyState(DataGridView grid, string message)
        {
            if (grid == null || grid.IsDisposed || !grid.ReadOnly)
            {
                return;
            }

            Panel panel = FindGridStatePanel(grid);
            string state = panel == null ? null : panel.Tag as string;
            if (string.Equals(state, "loading", StringComparison.Ordinal) ||
                string.Equals(state, "error", StringComparison.Ordinal))
            {
                return;
            }

            if (grid.Rows.Count == 0)
            {
                ShowGridState(grid, "empty", "○", message, UiStatusKind.Neutral);
            }
            else
            {
                ClearGridState(grid);
            }
        }

        private static void ShowGridState(
            DataGridView grid,
            string state,
            string iconText,
            string message,
            UiStatusKind kind)
        {
            if (grid == null || grid.IsDisposed)
            {
                return;
            }

            Panel panel = FindGridStatePanel(grid);
            if (panel == null)
            {
                panel = new Panel
                {
                    Name = GridStatePanelName,
                    BackColor = SystemInformation.HighContrast ? SystemColors.Window : UiTheme.Surface,
                    TabStop = false
                };

                Label content = new Label
                {
                    Name = "lblGridState",
                    Dock = DockStyle.Fill,
                    TextAlign = ContentAlignment.MiddleCenter,
                    AutoEllipsis = true,
                    Padding = new Padding(20),
                    Font = new Font("Segoe UI", 9.5F, FontStyle.Regular, GraphicsUnit.Point)
                };
                panel.Controls.Add(content);
                grid.Controls.Add(panel);
            }

            panel.Tag = state;
            Label label = panel.Controls["lblGridState"] as Label;
            if (label != null)
            {
                label.Text = string.Format("{0}  {1}", iconText, string.IsNullOrWhiteSpace(message) ? "Không có dữ liệu để hiển thị." : message);
                label.ForeColor = GetStatusColor(kind);
                label.AccessibleName = label.Text;
            }

            PositionGridStatePanel(grid);
            panel.BringToFront();
            panel.Visible = true;
        }

        private static Panel FindGridStatePanel(DataGridView grid)
        {
            if (grid == null)
            {
                return null;
            }

            return grid.Controls[GridStatePanelName] as Panel;
        }

        private static void PositionGridStatePanel(DataGridView grid)
        {
            Panel panel = FindGridStatePanel(grid);
            if (panel == null)
            {
                return;
            }

            int headerHeight = grid.ColumnHeadersVisible ? grid.ColumnHeadersHeight : 0;
            panel.Bounds = new Rectangle(
                1,
                headerHeight + 1,
                Math.Max(0, grid.ClientSize.Width - 2),
                Math.Max(0, grid.ClientSize.Height - headerHeight - 2));
            panel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        }

        private static Color GetStatusColor(UiStatusKind kind)
        {
            if (SystemInformation.HighContrast)
            {
                return SystemColors.WindowText;
            }

            switch (kind)
            {
                case UiStatusKind.Error:
                    return UiTheme.Danger;
                case UiStatusKind.Warning:
                    return UiTheme.Warning;
                case UiStatusKind.Information:
                    return UiTheme.Information;
                case UiStatusKind.Success:
                    return UiTheme.Success;
                default:
                    return UiTheme.TextSecondary;
            }
        }

        public static void HideInternalColumns(DataGridView grid)
        {
            if (grid == null)
            {
                return;
            }

            foreach (DataGridViewColumn column in grid.Columns)
            {
                if (IsInternalColumn(column))
                {
                    column.Visible = false;
                }
            }
        }

        public static bool IsInternalColumn(DataGridViewColumn column)
        {
            if (column == null)
            {
                return false;
            }

            string name = column.Name ?? string.Empty;
            string prop = column.DataPropertyName ?? string.Empty;
            string header = column.HeaderText ?? string.Empty;

            if (string.Equals(name, "Version", StringComparison.OrdinalIgnoreCase) ||
                string.Equals(prop, "Version", StringComparison.OrdinalIgnoreCase) ||
                string.Equals(header, "Version", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            if (name.IndexOf("version", StringComparison.OrdinalIgnoreCase) >= 0 ||
                prop.IndexOf("version", StringComparison.OrdinalIgnoreCase) >= 0 ||
                header.IndexOf("version", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                return true;
            }

            return false;
        }

        private static void ApplySemanticGridColumn(DataGridViewColumn column)
        {
            if (column == null)
            {
                return;
            }

            if (IsInternalColumn(column))
            {
                column.Visible = false;
                return;
            }

            string key = string.Concat(column.Name, " ", column.DataPropertyName).ToLowerInvariant();
            column.Resizable = DataGridViewTriState.True;

            if (column is DataGridViewCheckBoxColumn)
            {
                SetGridColumnLayout(column, 8F, 72, DataGridViewContentAlignment.MiddleCenter, null);
                return;
            }

            if (ContainsAny(key, "tongtien", "thanhtien", "dongia", "sotien", "doanhthu", "phatsinhno", "phatsinhco", "sodu", "giatriton", "dathu", "conlai", "tongthu", "tongchi", "chenhlech"))
            {
                SetGridColumnLayout(column, 18F, 135, DataGridViewContentAlignment.MiddleRight, "N0");
                return;
            }

            if (ContainsAny(key, "giamgia", "phantram", "tile"))
            {
                SetGridColumnLayout(column, 10F, 90, DataGridViewContentAlignment.MiddleRight, "N1");
                return;
            }

            if (ContainsAny(key, "soluong", "somathang", "soluot"))
            {
                SetGridColumnLayout(column, 11F, 95, DataGridViewContentAlignment.MiddleRight, "N0");
                return;
            }

            if (ContainsAny(key, "ngay", "date"))
            {
                SetGridColumnLayout(column, 15F, 135, DataGridViewContentAlignment.MiddleCenter, null);
                return;
            }

            if (ContainsAny(key, "trangthai", "isactive"))
            {
                SetGridColumnLayout(column, 14F, 125, DataGridViewContentAlignment.MiddleCenter, null);
                return;
            }

            if (ContainsAny(key, "donvitinh", "dvt", "gioitinh", "hinhthuc", "loaigiaodich", "loainghiepvu", "taikhoanno", "taikhoanco", "stt"))
            {
                SetGridColumnLayout(column, 10F, 82, DataGridViewContentAlignment.MiddleCenter, null);
                return;
            }

            if (ContainsAny(key, "maddh", "mahdb", "masp", "makh", "manv", "matk", "mapxk", "mapt", "mapc", "mact", "makho", "mancc", "maloai", "machungtu", "sochungtu"))
            {
                SetGridColumnLayout(column, 12F, 105, DataGridViewContentAlignment.MiddleCenter, null);
                return;
            }

            if (ContainsAny(key, "ghichu", "diengiai", "lydo", "diachi", "mota", "noidung"))
            {
                SetGridColumnLayout(column, 30F, 210, DataGridViewContentAlignment.MiddleLeft, null);
                return;
            }

            if (ContainsAny(key, "tenkh", "tennv", "tensp", "tenncc", "khachhang", "nhanvien", "nguoinop", "nguoinhan", "nguoigiaodich"))
            {
                SetGridColumnLayout(column, 26F, 180, DataGridViewContentAlignment.MiddleLeft, null);
                return;
            }

            if (key.Contains("ten"))
            {
                SetGridColumnLayout(column, 21F, 150, DataGridViewContentAlignment.MiddleLeft, null);
                return;
            }

            SetGridColumnLayout(column, 16F, 110, DataGridViewContentAlignment.MiddleLeft, null);
        }

        private static void SetGridColumnLayout(
            DataGridViewColumn column,
            float fillWeight,
            int minimumWidth,
            DataGridViewContentAlignment alignment,
            string defaultFormat)
        {
            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            column.FillWeight = fillWeight;
            column.MinimumWidth = minimumWidth;
            column.DefaultCellStyle.Alignment = alignment;
            column.HeaderCell.Style.Alignment = alignment;
            column.HeaderCell.Style.WrapMode = DataGridViewTriState.False;
            if (!string.IsNullOrEmpty(defaultFormat) && string.IsNullOrEmpty(column.DefaultCellStyle.Format))
            {
                column.DefaultCellStyle.Format = defaultFormat;
            }
        }

        private static bool ContainsAny(string value, params string[] candidates)
        {
            for (int i = 0; i < candidates.Length; i++)
            {
                if (value.Contains(candidates[i]))
                {
                    return true;
                }
            }
            return false;
        }

        public static void StyleStatusLabel(Label label, UiStatusKind kind, string message)
        {
            if (label == null)
            {
                return;
            }

            label.Text = message ?? string.Empty;
            label.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            label.AutoEllipsis = true;

            if (SystemInformation.HighContrast)
            {
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
                case UiStatusKind.Error:
                    label.ForeColor = UiTheme.Danger;
                    break;
                case UiStatusKind.Information:
                    label.ForeColor = UiTheme.Information;
                    break;
                default:
                    label.ForeColor = UiTheme.TextSecondary;
                    break;
            }
        }

        public static ErrorProvider CreateErrorProvider(Form owner)
        {
            ErrorProvider provider = new ErrorProvider();
            provider.BlinkStyle = ErrorBlinkStyle.NeverBlink;
            provider.ContainerControl = owner;
            UiInteractionHelper.WireValidationAutoClear(provider, owner);
            owner.Disposed += delegate { provider.Dispose(); };
            return provider;
        }

        public static void SetAccessibleText(Control control, string accessibleName, string description)
        {
            if (control == null)
            {
                return;
            }

            control.AccessibleName = accessibleName;
            control.AccessibleDescription = description;
        }

        private static void ApplyToContainer(Control container)
        {
            foreach (Control control in container.Controls)
            {
                TextBox textBox = control as TextBox;
                if (textBox != null)
                {
                    textBox.BorderStyle = BorderStyle.FixedSingle;
                    if (!SystemInformation.HighContrast)
                    {
                        textBox.BackColor = textBox.ReadOnly ? UiTheme.SurfaceMuted : UiTheme.Surface;
                        textBox.ForeColor = UiTheme.TextPrimary;
                    }
                }

                DataGridView grid = control as DataGridView;
                if (grid != null)
                {
                    StyleDataGridView(grid);
                }

                GroupBox group = control as GroupBox;
                if (group != null && !SystemInformation.HighContrast)
                {
                    group.BackColor = UiTheme.Surface;
                    group.ForeColor = UiTheme.TextPrimary;
                }

                if (control.HasChildren)
                {
                    ApplyToContainer(control);
                }
            }
        }

        private static void ApplyAccessibility(Control container)
        {
            foreach (Control control in container.Controls)
            {
                if (string.IsNullOrWhiteSpace(control.AccessibleName))
                {
                    string caption = GetAccessibleCaption(control);
                    if (!string.IsNullOrWhiteSpace(caption))
                    {
                        control.AccessibleName = caption;
                    }
                }

                if (control.HasChildren)
                {
                    ApplyAccessibility(control);
                }
            }
        }

        private static string GetAccessibleCaption(Control control)
        {
            Button button = control as Button;
            if (button != null)
            {
                return CleanCaption(button.Text);
            }

            CheckBox checkBox = control as CheckBox;
            if (checkBox != null)
            {
                return CleanCaption(checkBox.Text);
            }

            Label label = control as Label;
            if (label != null)
            {
                return CleanCaption(label.Text);
            }

            return string.IsNullOrWhiteSpace(control.Name) ? null : control.Name;
        }

        private static string CleanCaption(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return null;
            }

            string normalized = value.Replace("\r", " ").Replace("\n", " - ").Trim();
            int firstTextIndex = 0;
            while (firstTextIndex < normalized.Length &&
                   !char.IsLetterOrDigit(normalized[firstTextIndex]))
            {
                firstTextIndex++;
            }

            return firstTextIndex < normalized.Length
                ? normalized.Substring(firstTextIndex).Trim()
                : normalized;
        }

        private static void SetSolidButton(Button button, Color color, Color hoverColor)
        {
            button.BackColor = color;
            button.ForeColor = Color.White;
            button.FlatAppearance.BorderColor = color;
            button.FlatAppearance.MouseOverBackColor = hoverColor;
            button.FlatAppearance.MouseDownBackColor = hoverColor;
        }
    }
}
