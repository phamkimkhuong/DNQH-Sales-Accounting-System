using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using DNQH_KeToanBanHang.Forms;

namespace DNQH_KeToanBanHang.Helpers
{
    public static class UiInteractionHelper
    {
        private static readonly string[] SaveActionNames =
        {
            "btnLuu",
            "btnLuuDon",
            "btnLuuPhieu",
            "btnLuuChungTu",
            "btnLapHoaDon",
            "btnXuatKho",
            "btnSua",
            "btnThem"
        };

        private static readonly string[] NewActionNames =
        {
            "btnThem",
            "btnThemChiTiet",
            "btnLamMoi",
            "btnLamMoiDanhSach",
            "btnTaoMoi",
            "btnTaoDon"
        };

        private static readonly string[] RefreshActionNames =
        {
            "btnLamMoiDanhSach",
            "btnLamMoi",
            "btnRefresh",
            "btnTaiLai"
        };

        private static readonly string[] PrintActionNames =
        {
            "btnInDonHangTab1",
            "btnInHoaDonTab1",
            "btnInPhieu",
            "btnInDonHangDS",
            "btnInHoaDonDS",
            "btnIn",
            "btnInHoaDon"
        };

        private static readonly string[] ExportActionNames =
        {
            "btnXuatCsv",
            "btnXuatExcel",
            "btnExport"
        };

        private static readonly string[] ImportActionNames =
        {
            "btnNhapExcel",
            "btnImport"
        };

        private static readonly string[] DeleteActionNames =
        {
            "btnXoaChiTiet",
            "btnXoa"
        };

        private static readonly string[] CloseActionNames =
        {
            "btnDong",
            "btnClose",
            "btnThoat",
            "btnHuy"
        };

        private static readonly ToolTip GlobalToolTip = new ToolTip
        {
            ShowAlways = true,
            InitialDelay = 350,
            ReshowDelay = 150,
            AutoPopDelay = 6000
        };

        public static void Apply(Form form)
        {
            if (form == null)
            {
                return;
            }

            form.KeyPreview = true;
            form.KeyDown -= Form_KeyDown;
            form.KeyDown += Form_KeyDown;
            form.Shown -= Form_Shown;
            form.Shown += Form_Shown;

            ConfigureContainer(form);
            ApplyShortcutDescriptions(form);
            ApplyShortcutToolTips(form);
        }

        public static void ShowHotkeysHelp(Form owner = null)
        {
            try
            {
                using (frmHotkeysHelp helpForm = new frmHotkeysHelp())
                {
                    if (owner != null && owner.Visible && !owner.IsDisposed)
                    {
                        helpForm.ShowDialog(owner);
                    }
                    else
                    {
                        helpForm.ShowDialog();
                    }
                }
            }
            catch (Exception ex)
            {
                AppLogger.Warn("HOTKEYS_HELP_ERROR", "Không thể mở bảng tra cứu phím tắt: " + ex.Message);
            }
        }

        public static void WireValidationAutoClear(ErrorProvider provider, Control container)
        {
            if (provider == null || container == null)
            {
                return;
            }

            foreach (Control control in container.Controls)
            {
                TextBoxBase textBox = control as TextBoxBase;
                if (textBox != null)
                {
                    textBox.TextChanged += delegate { provider.SetError(textBox, string.Empty); };
                }

                ComboBox comboBox = control as ComboBox;
                if (comboBox != null)
                {
                    comboBox.SelectedIndexChanged += delegate { provider.SetError(comboBox, string.Empty); };
                    comboBox.TextChanged += delegate { provider.SetError(comboBox, string.Empty); };
                }

                NumericUpDown numeric = control as NumericUpDown;
                if (numeric != null)
                {
                    numeric.ValueChanged += delegate { provider.SetError(numeric, string.Empty); };
                }

                DateTimePicker datePicker = control as DateTimePicker;
                if (datePicker != null)
                {
                    datePicker.ValueChanged += delegate { provider.SetError(datePicker, string.Empty); };
                }

                DataGridView grid = control as DataGridView;
                if (grid != null)
                {
                    grid.CellValueChanged += delegate { provider.SetError(grid, string.Empty); };
                    grid.RowsAdded += delegate { provider.SetError(grid, string.Empty); };
                }

                if (control.HasChildren)
                {
                    WireValidationAutoClear(provider, control);
                }
            }
        }

        public static void ShowValidationError(ErrorProvider provider, Control control, string message)
        {
            if (provider == null || control == null)
            {
                return;
            }

            provider.Clear();
            provider.SetIconAlignment(control, ErrorIconAlignment.MiddleRight);
            provider.SetIconPadding(control, 4);
            provider.SetError(control, message ?? string.Empty);
            control.Focus();

            TextBoxBase textBox = control as TextBoxBase;
            if (textBox != null)
            {
                textBox.SelectAll();
            }
        }

        private static void Form_Shown(object sender, EventArgs e)
        {
            Form form = sender as Form;
            if (form == null)
            {
                return;
            }

            ConfigureContainer(form);
            Control focused = FindFocusedControl(form);
            if (IsEditableInput(focused))
            {
                return;
            }

            Control firstInput = FindFirstEditableInput(form);
            if (firstInput != null)
            {
                firstInput.Focus();
            }
        }

        private static void Form_KeyDown(object sender, KeyEventArgs e)
        {
            Form form = sender as Form;
            if (form == null)
            {
                return;
            }

            // F1: Mở bảng tra cứu phím tắt toàn hệ thống
            if (e.KeyCode == Keys.F1 && e.Modifiers == Keys.None)
            {
                ShowHotkeysHelp(form);
                e.Handled = true;
                e.SuppressKeyPress = true;
                return;
            }

            // F2 hoặc Ctrl + N: Thêm mới / Làm mới form
            if ((e.KeyCode == Keys.F2 && e.Modifiers == Keys.None) ||
                (e.Control && e.KeyCode == Keys.N && !e.Shift && !e.Alt))
            {
                Button newBtn = FindNewAction(form);
                if (newBtn != null)
                {
                    newBtn.PerformClick();
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                    return;
                }
            }

            // F3 hoặc Ctrl + S: Lưu / Ghi sổ chứng từ / Cập nhật
            if ((e.KeyCode == Keys.F3 && e.Modifiers == Keys.None) ||
                (e.Control && e.KeyCode == Keys.S && !e.Shift && !e.Alt))
            {
                Button saveButton = FindSaveAction(form);
                if (saveButton != null)
                {
                    saveButton.PerformClick();
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                    return;
                }
            }

            // F5: Nạp lại danh sách (Refresh)
            if (e.KeyCode == Keys.F5 && e.Modifiers == Keys.None)
            {
                Button refreshBtn = FindRefreshAction(form);
                if (refreshBtn != null)
                {
                    refreshBtn.PerformClick();
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                    return;
                }
            }

            // Ctrl + F: Tìm kiếm nhanh
            if (e.Control && e.KeyCode == Keys.F && !e.Shift && !e.Alt)
            {
                Control searchControl = FindVisibleControl(form, IsSearchInput);
                if (searchControl != null)
                {
                    FocusAndSelect(searchControl);
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                    return;
                }
            }

            // Ctrl + P: In ấn chứng từ / hóa đơn
            if (e.Control && e.KeyCode == Keys.P && !e.Shift && !e.Alt)
            {
                Button printBtn = FindPrintAction(form);
                if (printBtn != null)
                {
                    printBtn.PerformClick();
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                    return;
                }
            }

            // Ctrl + E: Xuất Excel
            if (e.Control && e.KeyCode == Keys.E && !e.Shift && !e.Alt)
            {
                Button exportBtn = FindExportAction(form);
                if (exportBtn != null)
                {
                    exportBtn.PerformClick();
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                    return;
                }
            }

            // Ctrl + I: Nhập Excel
            if (e.Control && e.KeyCode == Keys.I && !e.Shift && !e.Alt)
            {
                Button importBtn = FindImportAction(form);
                if (importBtn != null)
                {
                    importBtn.PerformClick();
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                    return;
                }
            }

            // Delete: Xóa dòng chi tiết nếu đang ở DataGridView
            if (e.KeyCode == Keys.Delete && e.Modifiers == Keys.None)
            {
                Control focused = FindFocusedControl(form);
                if (focused is DataGridView)
                {
                    Button deleteBtn = FindDeleteAction(form);
                    if (deleteBtn != null)
                    {
                        deleteBtn.PerformClick();
                        e.Handled = true;
                        e.SuppressKeyPress = true;
                        return;
                    }
                }
            }

            // Esc: Đóng form hoặc hủy bỏ chế độ sửa
            if (e.KeyCode == Keys.Escape && e.Modifiers == Keys.None)
            {
                Control focused = FindFocusedControl(form);
                ComboBox cbo = focused as ComboBox;
                if (cbo != null && cbo.DroppedDown)
                {
                    return; // Để ComboBox tự đóng danh sách
                }

                Button closeBtn = FindCloseAction(form);
                if (closeBtn != null)
                {
                    closeBtn.PerformClick();
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                    return;
                }

                if (form.Modal)
                {
                    form.Close();
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                    return;
                }
            }

            // Shift + Enter: Lùi về trường nhập liệu trước đó
            if (e.KeyCode == Keys.Enter && e.Shift && !e.Control && !e.Alt)
            {
                Control active = FindFocusedControl(form);
                if (active != null && CanAdvanceWithEnter(active))
                {
                    if (form.SelectNextControl(active, false, true, true, true))
                    {
                        e.Handled = true;
                        e.SuppressKeyPress = true;
                        return;
                    }
                }
            }

            // Enter: Chuyển trường nhập liệu kế tiếp (Next Control on Enter)
            if (e.KeyCode == Keys.Enter && e.Modifiers == Keys.None)
            {
                Control activeControl = FindFocusedControl(form);
                if (activeControl == null)
                {
                    return;
                }

                if (IsSearchInput(activeControl))
                {
                    Button searchButton = FindVisibleButton(form, "btnTimKiem");
                    if (searchButton != null)
                    {
                        searchButton.PerformClick();
                        e.Handled = true;
                        e.SuppressKeyPress = true;
                        return;
                    }
                }

                if (form.AcceptButton != null || !CanAdvanceWithEnter(activeControl))
                {
                    return;
                }

                if (form.SelectNextControl(activeControl, true, true, true, true))
                {
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                }
            }
        }

        private static void ConfigureContainer(Control container)
        {
            List<Control> controls = new List<Control>();
            foreach (Control control in container.Controls)
            {
                controls.Add(control);
            }

            if (!(container is TabControl))
            {
                controls.Sort(CompareByPosition);
            }
            for (int i = 0; i < controls.Count; i++)
            {
                Control control = controls[i];
                control.TabIndex = i;
                control.TabStop = ShouldUseTabStop(control);
                ConfigureFocusCue(control);

                if (IsLayoutContainer(control))
                {
                    ConfigureContainer(control);
                }
            }
        }

        private static int CompareByPosition(Control left, Control right)
        {
            int rowComparison = left.Top.CompareTo(right.Top);
            if (rowComparison != 0)
            {
                return rowComparison;
            }

            int columnComparison = left.Left.CompareTo(right.Left);
            return columnComparison != 0
                ? columnComparison
                : string.Compare(left.Name, right.Name, StringComparison.OrdinalIgnoreCase);
        }

        private static bool IsLayoutContainer(Control control)
        {
            return control is Panel || control is GroupBox ||
                   control is TabControl || control is SplitContainer ||
                   control is UserControl;
        }

        private static bool ShouldUseTabStop(Control control)
        {
            TextBoxBase textBox = control as TextBoxBase;
            if (textBox != null)
            {
                return !textBox.ReadOnly && textBox.Enabled;
            }

            if (control is Label || control is Panel || control is GroupBox ||
                control is TableLayoutPanel || control is FlowLayoutPanel ||
                control is PictureBox || control is ProgressBar)
            {
                return false;
            }

            if (control is ComboBox || control is DateTimePicker ||
                control is NumericUpDown || control is CheckBox ||
                control is RadioButton || control is Button ||
                control is DataGridView || control is TabControl ||
                control is ListControl)
            {
                return control.Enabled;
            }

            return control.TabStop;
        }

        private static void ConfigureFocusCue(Control control)
        {
            if (!(control is TextBoxBase) && !(control is ComboBox) && !(control is NumericUpDown))
            {
                return;
            }

            control.Enter -= Input_Enter;
            control.Enter += Input_Enter;
            control.Leave -= Input_Leave;
            control.Leave += Input_Leave;
        }

        private static void Input_Enter(object sender, EventArgs e)
        {
            Control control = sender as Control;
            if (control == null || SystemInformation.HighContrast || !IsEditableInput(control))
            {
                return;
            }

            control.BackColor = UiTheme.FocusSurface;
        }

        private static void Input_Leave(object sender, EventArgs e)
        {
            Control control = sender as Control;
            if (control == null || SystemInformation.HighContrast)
            {
                return;
            }

            TextBoxBase textBox = control as TextBoxBase;
            control.BackColor = textBox != null && textBox.ReadOnly
                ? UiTheme.SurfaceMuted
                : UiTheme.Surface;
        }

        private static bool CanAdvanceWithEnter(Control control)
        {
            TextBox standardTextBox = control as TextBox;
            if (standardTextBox != null)
            {
                return !standardTextBox.Multiline || !standardTextBox.AcceptsReturn;
            }

            TextBoxBase textBox = control as TextBoxBase;
            if (textBox != null)
            {
                return !textBox.Multiline;
            }

            ComboBox comboBox = control as ComboBox;
            if (comboBox != null)
            {
                return !comboBox.DroppedDown;
            }

            NumericUpDown numeric = control as NumericUpDown;
            if (numeric != null)
            {
                return true;
            }

            return control is DateTimePicker;
        }

        private static bool IsEditableInput(Control control)
        {
            if (control == null || !control.Visible || !control.Enabled)
            {
                return false;
            }

            TextBoxBase textBox = control as TextBoxBase;
            if (textBox != null)
            {
                return !textBox.ReadOnly;
            }

            return control is ComboBox || control is DateTimePicker || control is NumericUpDown;
        }

        private static Control FindFirstEditableInput(Control container)
        {
            List<Control> controls = GetOrderedChildren(container);
            for (int i = 0; i < controls.Count; i++)
            {
                Control control = controls[i];
                if (!control.Visible || !control.Enabled)
                {
                    continue;
                }

                if (IsEditableInput(control))
                {
                    return control;
                }

                if (control.HasChildren)
                {
                    Control nested = FindFirstEditableInput(control);
                    if (nested != null)
                    {
                        return nested;
                    }
                }
            }

            return null;
        }

        private static List<Control> GetOrderedChildren(Control container)
        {
            List<Control> controls = new List<Control>();
            foreach (Control control in container.Controls)
            {
                controls.Add(control);
            }
            controls.Sort(delegate(Control left, Control right)
            {
                return left.TabIndex.CompareTo(right.TabIndex);
            });
            return controls;
        }

        private static Control FindFocusedControl(Control container)
        {
            foreach (Control control in container.Controls)
            {
                if (control.Focused)
                {
                    return control;
                }

                if (control.ContainsFocus && control.HasChildren)
                {
                    Control nested = FindFocusedControl(control);
                    if (nested != null)
                    {
                        return nested;
                    }
                }
            }

            return null;
        }

        private static Control FindVisibleControl(Control container, Predicate<Control> predicate)
        {
            foreach (Control control in container.Controls)
            {
                if (control.Visible && control.Enabled && predicate(control))
                {
                    return control;
                }

                if (control.HasChildren)
                {
                    Control nested = FindVisibleControl(control, predicate);
                    if (nested != null)
                    {
                        return nested;
                    }
                }
            }

            return null;
        }

        private static bool IsSearchInput(Control control)
        {
            return control is TextBoxBase &&
                   (string.Equals(control.Name, "txtTimKiem", StringComparison.OrdinalIgnoreCase) ||
                    control.Name.IndexOf("TimKiem", StringComparison.OrdinalIgnoreCase) >= 0);
        }

        private static Button FindSaveAction(Form form)
        {
            for (int i = 0; i < SaveActionNames.Length; i++)
            {
                Button button = FindVisibleButton(form, SaveActionNames[i]);
                if (button != null)
                {
                    return button;
                }
            }

            return null;
        }

        private static Button FindVisibleButton(Control container, string name)
        {
            Control match = FindVisibleControl(container, delegate(Control control)
            {
                return control is Button && string.Equals(control.Name, name, StringComparison.OrdinalIgnoreCase);
            });
            return match as Button;
        }

        private static void FocusAndSelect(Control control)
        {
            control.Focus();
            TextBoxBase textBox = control as TextBoxBase;
            if (textBox != null)
            {
                textBox.SelectAll();
            }
        }

        private static void ApplyShortcutDescriptions(Control container)
        {
            foreach (Control control in container.Controls)
            {
                if (IsSearchInput(control))
                {
                    control.AccessibleDescription = AppendHint(control.AccessibleDescription, "Nhấn Ctrl+F để chuyển nhanh đến ô tìm kiếm.");
                }

                Button button = control as Button;
                if (button != null && IsSaveActionName(button.Name))
                {
                    button.AccessibleDescription = AppendHint(button.AccessibleDescription, "Nhấn Ctrl+S để thực hiện thao tác lưu.");
                }

                if (control.HasChildren)
                {
                    ApplyShortcutDescriptions(control);
                }
            }
        }

        private static bool IsSaveActionName(string name)
        {
            for (int i = 0; i < SaveActionNames.Length; i++)
            {
                if (string.Equals(name, SaveActionNames[i], StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }

        private static Button FindNewAction(Form form)
        {
            for (int i = 0; i < NewActionNames.Length; i++)
            {
                Button button = FindVisibleButton(form, NewActionNames[i]);
                if (button != null)
                {
                    return button;
                }
            }
            return null;
        }

        private static Button FindRefreshAction(Form form)
        {
            for (int i = 0; i < RefreshActionNames.Length; i++)
            {
                Button button = FindVisibleButton(form, RefreshActionNames[i]);
                if (button != null)
                {
                    return button;
                }
            }
            return null;
        }

        private static Button FindPrintAction(Form form)
        {
            for (int i = 0; i < PrintActionNames.Length; i++)
            {
                Button button = FindVisibleButton(form, PrintActionNames[i]);
                if (button != null)
                {
                    return button;
                }
            }
            return null;
        }

        private static Button FindExportAction(Form form)
        {
            for (int i = 0; i < ExportActionNames.Length; i++)
            {
                Button button = FindVisibleButton(form, ExportActionNames[i]);
                if (button != null)
                {
                    return button;
                }
            }
            return null;
        }

        private static Button FindImportAction(Form form)
        {
            for (int i = 0; i < ImportActionNames.Length; i++)
            {
                Button button = FindVisibleButton(form, ImportActionNames[i]);
                if (button != null)
                {
                    return button;
                }
            }
            return null;
        }

        private static Button FindDeleteAction(Form form)
        {
            for (int i = 0; i < DeleteActionNames.Length; i++)
            {
                Button button = FindVisibleButton(form, DeleteActionNames[i]);
                if (button != null)
                {
                    return button;
                }
            }
            return null;
        }

        private static Button FindCloseAction(Form form)
        {
            for (int i = 0; i < CloseActionNames.Length; i++)
            {
                Button button = FindVisibleButton(form, CloseActionNames[i]);
                if (button != null)
                {
                    return button;
                }
            }
            return null;
        }

        public static void ApplyShortcutToolTips(Control container)
        {
            if (container == null) return;

            foreach (Control control in container.Controls)
            {
                Button btn = control as Button;
                if (btn != null)
                {
                    string hint = GetButtonShortcutHint(btn);
                    if (!string.IsNullOrEmpty(hint))
                    {
                        GlobalToolTip.SetToolTip(btn, hint);
                    }
                }

                if (control.HasChildren)
                {
                    ApplyShortcutToolTips(control);
                }
            }
        }

        private static string GetButtonShortcutHint(Button btn)
        {
            string name = btn.Name ?? string.Empty;
            string text = btn.Text ?? string.Empty;

            if (IsSaveActionName(name) || text.IndexOf("Lưu", StringComparison.OrdinalIgnoreCase) >= 0 || text.IndexOf("Ghi sổ", StringComparison.OrdinalIgnoreCase) >= 0)
                return string.Format("{0} [F3 hoặc Ctrl+S]", text);

            if (IsActionName(name, NewActionNames) || text.IndexOf("Thêm", StringComparison.OrdinalIgnoreCase) >= 0 || text.IndexOf("Tạo mới", StringComparison.OrdinalIgnoreCase) >= 0)
                return string.Format("{0} [F2 hoặc Ctrl+N]", text);

            if (IsActionName(name, PrintActionNames) || text.IndexOf("In ", StringComparison.OrdinalIgnoreCase) >= 0)
                return string.Format("{0} [Ctrl+P]", text);

            if (IsActionName(name, ExportActionNames) || text.IndexOf("Xuất", StringComparison.OrdinalIgnoreCase) >= 0)
                return string.Format("{0} [Ctrl+E]", text);

            if (IsActionName(name, ImportActionNames) || text.IndexOf("Nhập", StringComparison.OrdinalIgnoreCase) >= 0)
                return string.Format("{0} [Ctrl+I]", text);

            if (IsActionName(name, RefreshActionNames) || text.IndexOf("Làm mới", StringComparison.OrdinalIgnoreCase) >= 0 || text.IndexOf("Nạp lại", StringComparison.OrdinalIgnoreCase) >= 0)
                return string.Format("{0} [F5]", text);

            if (string.Equals(name, "btnTimKiem", StringComparison.OrdinalIgnoreCase) || text.IndexOf("Tìm", StringComparison.OrdinalIgnoreCase) >= 0)
                return string.Format("{0} [Enter hoặc Ctrl+F]", text);

            if (IsActionName(name, CloseActionNames) || text.IndexOf("Đóng", StringComparison.OrdinalIgnoreCase) >= 0 || text.IndexOf("Thoát", StringComparison.OrdinalIgnoreCase) >= 0)
                return string.Format("{0} [Esc]", text);

            return null;
        }

        private static bool IsActionName(string name, string[] candidateNames)
        {
            if (string.IsNullOrEmpty(name) || candidateNames == null) return false;
            for (int i = 0; i < candidateNames.Length; i++)
            {
                if (string.Equals(name, candidateNames[i], StringComparison.OrdinalIgnoreCase))
                    return true;
            }
            return false;
        }

        private static string AppendHint(string current, string hint)
        {
            if (string.IsNullOrWhiteSpace(current))
            {
                return hint;
            }

            return current.IndexOf(hint, StringComparison.Ordinal) >= 0
                ? current
                : current.Trim() + " " + hint;
        }
    }
}
