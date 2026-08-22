using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DNQH_KeToanBanHang.Helpers
{
    public sealed class UiDebouncer : IDisposable
    {
        private readonly Timer _timer;
        private Func<int, Task> _pendingAction;
        private int _version;
        private bool _disposed;

        public UiDebouncer(int delayMilliseconds)
        {
            _timer = new Timer
            {
                Interval = Math.Max(100, delayMilliseconds)
            };
            _timer.Tick += Timer_Tick;
        }

        public int CurrentVersion
        {
            get { return _version; }
        }

        public bool IsCurrent(int version)
        {
            return !_disposed && version == _version;
        }

        public void Restart(Func<int, Task> action)
        {
            if (_disposed || action == null)
            {
                return;
            }

            _version++;
            _pendingAction = action;
            _timer.Stop();
            _timer.Start();
        }

        public async Task RunNowAsync(Func<int, Task> action)
        {
            if (_disposed || action == null)
            {
                return;
            }

            _timer.Stop();
            _pendingAction = null;
            int version = ++_version;
            await action(version);
        }

        public void Cancel()
        {
            if (_disposed)
            {
                return;
            }

            _timer.Stop();
            _pendingAction = null;
            _version++;
        }

        private async void Timer_Tick(object sender, EventArgs e)
        {
            _timer.Stop();
            Func<int, Task> action = _pendingAction;
            _pendingAction = null;
            int version = _version;

            if (action != null && !_disposed)
            {
                await action(version);
            }
        }

        public void Dispose()
        {
            if (_disposed)
            {
                return;
            }

            _disposed = true;
            _version++;
            _pendingAction = null;
            _timer.Stop();
            _timer.Tick -= Timer_Tick;
            _timer.Dispose();
        }
    }
}
