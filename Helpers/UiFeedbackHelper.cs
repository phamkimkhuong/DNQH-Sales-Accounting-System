using System;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DNQH_KeToanBanHang.Helpers
{
    public static class UiFeedbackHelper
    {
        private const string ToastPanelName = "pnlAppToast";
        private static readonly ConditionalWeakTable<Form, BusyOwnerState> BusyOwners =
            new ConditionalWeakTable<Form, BusyOwnerState>();

        public static void ShowSuccess(Form owner, string message)
        {
            ShowToast(owner, message, UiStatusKind.Success, 4000);
        }

        public static void ShowToast(Form owner, string message, UiStatusKind kind, int durationMilliseconds)
        {
            if (owner == null || owner.IsDisposed || string.IsNullOrWhiteSpace(message))
            {
                return;
            }

            if (owner.InvokeRequired)
            {
                owner.BeginInvoke(new Action<Form, string, UiStatusKind, int>(ShowToast),
                    owner, message, kind, durationMilliseconds);
                return;
            }

            RemoveExistingToast(owner);

            int toastWidth = Math.Max(300, Math.Min(460, owner.ClientSize.Width - 40));
            Font messageFont = new Font("Segoe UI", 9.5F, FontStyle.Regular, GraphicsUnit.Point);
            Size measured = TextRenderer.MeasureText(
                message,
                messageFont,
                new Size(toastWidth - 58, 120),
                TextFormatFlags.WordBreak | TextFormatFlags.TextBoxControl);
            int toastHeight = Math.Max(54, Math.Min(140, measured.Height + 28));

            Panel toast = new Panel
            {
                Name = ToastPanelName,
                Size = new Size(toastWidth, toastHeight),
                Location = new Point(Math.Max(20, owner.ClientSize.Width - toastWidth - 20), 20),
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                BackColor = GetToastColor(kind),
                Padding = new Padding(16, 10, 14, 10),
                AccessibleRole = AccessibleRole.Alert,
                AccessibleName = "Thông báo",
                AccessibleDescription = message,
                TabStop = false
            };

            Label icon = new Label
            {
                AutoSize = false,
                Dock = DockStyle.Left,
                Width = 30,
                Text = GetToastIcon(kind),
                Font = new Font("Segoe UI Symbol", 13F, FontStyle.Bold, GraphicsUnit.Point),
                ForeColor = Color.White,
                TextAlign = ContentAlignment.MiddleLeft,
                TabStop = false
            };

            Label content = new Label
            {
                AutoSize = false,
                Dock = DockStyle.Fill,
                Text = message,
                Font = messageFont,
                ForeColor = Color.White,
                TextAlign = ContentAlignment.MiddleLeft,
                UseMnemonic = false,
                TabStop = false
            };

            toast.Controls.Add(content);
            toast.Controls.Add(icon);
            owner.Controls.Add(toast);
            toast.BringToFront();

            Timer timer = new Timer { Interval = Math.Max(1500, durationMilliseconds) };
            toast.Tag = timer;
            EventHandler dismiss = null;
            dismiss = delegate
            {
                timer.Stop();
                timer.Tick -= dismiss;
                if (!toast.IsDisposed)
                {
                    toast.Parent = null;
                    toast.Dispose();
                }
                timer.Dispose();
            };
            timer.Tick += dismiss;
            toast.Click += dismiss;
            icon.Click += dismiss;
            content.Click += dismiss;
            timer.Start();
        }

        public static IDisposable BeginBusy(Form owner, Button actionButton, string busyText)
        {
            return new BusyScope(owner, actionButton, busyText);
        }

        public static async Task<T> RunBusyAsync<T>(
            Form owner,
            Button actionButton,
            string busyText,
            Func<T> operation)
        {
            if (operation == null)
            {
                throw new ArgumentNullException("operation");
            }

            using (BeginBusy(owner, actionButton, busyText))
            {
                return await Task.Run(operation);
            }
        }

        private static void RemoveExistingToast(Form owner)
        {
            Control[] existing = owner.Controls.Find(ToastPanelName, false);
            for (int i = 0; i < existing.Length; i++)
            {
                Timer timer = existing[i].Tag as Timer;
                if (timer != null)
                {
                    timer.Stop();
                    timer.Dispose();
                }
                existing[i].Parent = null;
                existing[i].Dispose();
            }
        }

        private static Color GetToastColor(UiStatusKind kind)
        {
            if (SystemInformation.HighContrast)
            {
                return SystemColors.Highlight;
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

        private static string GetToastIcon(UiStatusKind kind)
        {
            switch (kind)
            {
                case UiStatusKind.Error:
                    return "×";
                case UiStatusKind.Warning:
                    return "!";
                case UiStatusKind.Information:
                    return "i";
                case UiStatusKind.Success:
                    return "✓";
                default:
                    return "•";
            }
        }

        private sealed class BusyScope : IDisposable
        {
            private readonly Form _owner;
            private readonly Button _button;
            private readonly BusyOwnerState _ownerState;
            private readonly bool _buttonWasEnabled;
            private readonly string _buttonText;
            private bool _disposed;

            public BusyScope(Form owner, Button button, string busyText)
            {
                _owner = owner;
                _button = button;

                if (_owner != null)
                {
                    _ownerState = BusyOwners.GetOrCreateValue(_owner);
                    if (_ownerState.ActiveCount == 0)
                    {
                        _ownerState.Cursor = _owner.Cursor;
                        _ownerState.UseWaitCursor = _owner.UseWaitCursor;
                        _owner.UseWaitCursor = true;
                        _owner.Cursor = Cursors.WaitCursor;
                    }
                    _ownerState.ActiveCount++;
                }

                if (_button != null)
                {
                    _buttonWasEnabled = _button.Enabled;
                    _buttonText = _button.Text;
                    _button.Enabled = false;
                    if (!string.IsNullOrWhiteSpace(busyText))
                    {
                        _button.Text = busyText;
                    }
                    _button.Refresh();
                }
            }

            public void Dispose()
            {
                if (_disposed)
                {
                    return;
                }
                _disposed = true;

                if (_button != null && !_button.IsDisposed)
                {
                    _button.Text = _buttonText;
                    _button.Enabled = _buttonWasEnabled;
                }

                if (_owner != null && !_owner.IsDisposed)
                {
                    _ownerState.ActiveCount = Math.Max(0, _ownerState.ActiveCount - 1);
                    if (_ownerState.ActiveCount == 0)
                    {
                        _owner.UseWaitCursor = _ownerState.UseWaitCursor;
                        _owner.Cursor = _ownerState.Cursor;
                        BusyOwners.Remove(_owner);
                    }
                }
            }
        }

        private sealed class BusyOwnerState
        {
            public int ActiveCount { get; set; }
            public Cursor Cursor { get; set; }
            public bool UseWaitCursor { get; set; }
        }
    }
}
