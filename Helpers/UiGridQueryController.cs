using System;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DNQH_KeToanBanHang.Helpers
{
    public sealed class UiGridQueryController : IDisposable
    {
        private readonly Form _owner;
        private readonly TextBox _searchBox;
        private readonly DataGridView _grid;
        private readonly Label _statusLabel;
        private readonly Func<string, Func<DataTable>> _queryFactory;
        private readonly Func<int, string> _successMessageFactory;
        private readonly Action<Exception> _errorHandler;
        private readonly string _loadingMessage;
        private readonly string _emptyMessage;
        private readonly UiDebouncer _debouncer;
        private bool _disposed;

        public UiGridQueryController(
            Form owner,
            TextBox searchBox,
            DataGridView grid,
            Label statusLabel,
            Func<string, Func<DataTable>> queryFactory,
            Func<int, string> successMessageFactory,
            string loadingMessage,
            string emptyMessage,
            Action<Exception> errorHandler)
        {
            if (owner == null) throw new ArgumentNullException("owner");
            if (searchBox == null) throw new ArgumentNullException("searchBox");
            if (grid == null) throw new ArgumentNullException("grid");
            if (queryFactory == null) throw new ArgumentNullException("queryFactory");

            _owner = owner;
            _searchBox = searchBox;
            _grid = grid;
            _statusLabel = statusLabel;
            _queryFactory = queryFactory;
            _successMessageFactory = successMessageFactory;
            _loadingMessage = loadingMessage;
            _emptyMessage = emptyMessage;
            _errorHandler = errorHandler;
            _debouncer = new UiDebouncer(350);

            _searchBox.TextChanged += SearchBox_TextChanged;
            _owner.Disposed += Owner_Disposed;
        }

        public Task RefreshAsync(Button actionButton)
        {
            return _debouncer.RunNowAsync(
                delegate(int version) { return LoadAsync(version, actionButton); });
        }

        private void SearchBox_TextChanged(object sender, EventArgs e)
        {
            _debouncer.Restart(
                delegate(int version) { return LoadAsync(version, null); });
        }

        private async Task LoadAsync(int requestVersion, Button actionButton)
        {
            if (_disposed || _owner.IsDisposed)
            {
                return;
            }

            string keyword = _searchBox.Text.Trim();
            Func<DataTable> query = _queryFactory(keyword);
            if (query == null)
            {
                return;
            }

            UiStyler.SetGridLoading(_grid, _loadingMessage);
            UiStyler.StyleStatusLabel(_statusLabel, UiStatusKind.Information, _loadingMessage);

            try
            {
                DataTable result = await UiFeedbackHelper.RunBusyAsync(
                    _owner,
                    actionButton,
                    "ĐANG TẢI...",
                    query);

                if (_disposed || _owner.IsDisposed || !_debouncer.IsCurrent(requestVersion))
                {
                    return;
                }

                result = result ?? new DataTable();
                _grid.AutoGenerateColumns = false;
                _grid.DataSource = result;
                UiStyler.HideInternalColumns(_grid);

                int count = result.Rows.Count;
                UiStyler.StyleStatusLabel(
                    _statusLabel,
                    count == 0 ? UiStatusKind.Warning : UiStatusKind.Neutral,
                    count == 0
                        ? _emptyMessage
                        : (_successMessageFactory == null ? string.Format("Đang hiển thị {0:N0} dòng dữ liệu.", count) : _successMessageFactory(count)));
                UiStyler.ClearGridState(_grid);
                UiStyler.UpdateGridEmptyState(_grid, _emptyMessage);
            }
            catch (Exception ex)
            {
                if (_disposed || !_debouncer.IsCurrent(requestVersion))
                {
                    return;
                }

                UiStyler.SetGridError(_grid, "Không thể tải dữ liệu. Vui lòng thử lại.");
                UiStyler.StyleStatusLabel(_statusLabel, UiStatusKind.Error, "Không thể tải dữ liệu. Vui lòng thử lại.");
                if (_errorHandler != null)
                {
                    _errorHandler(ex);
                }
            }
        }

        private void Owner_Disposed(object sender, EventArgs e)
        {
            Dispose();
        }

        public void Dispose()
        {
            if (_disposed)
            {
                return;
            }

            _disposed = true;
            _searchBox.TextChanged -= SearchBox_TextChanged;
            _owner.Disposed -= Owner_Disposed;
            _debouncer.Dispose();
        }
    }
}
