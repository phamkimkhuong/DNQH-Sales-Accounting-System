using System;
using System.Drawing;
using System.Windows.Forms;

namespace DNQH_KeToanBanHang.Helpers
{
    public class PageChangedEventArgs : EventArgs
    {
        public int PageIndex { get; private set; }
        public int PageSize { get; private set; }

        public PageChangedEventArgs(int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
        }
    }

    /// <summary>
    /// Thanh điều khiển phân trang (Pagination Control) hiện đại dùng chung cho các màn hình danh sách lớn.
    /// </summary>
    public class UiPaginationControl : Panel
    {
        private readonly Label _lblRecordInfo;
        private readonly Label _lblPageInfo;
        private readonly Button _btnFirst;
        private readonly Button _btnPrev;
        private readonly Button _btnNext;
        private readonly Button _btnLast;
        private readonly ComboBox _cboPageSize;
        private readonly Label _lblPageSizePrompt;

        private int _currentPage = 1;
        private int _pageSize = 25;
        private int _totalRecords = 0;
        private bool _isUpdatingState = false;

        public event EventHandler<PageChangedEventArgs> PageChanged;

        public int CurrentPage
        {
            get { return _currentPage; }
        }

        public int PageSize
        {
            get { return _pageSize; }
        }

        public int TotalRecords
        {
            get { return _totalRecords; }
        }

        public int TotalPages
        {
            get
            {
                if (_pageSize <= 0 || _totalRecords <= 0) return 1;
                return (int)Math.Ceiling((double)_totalRecords / _pageSize);
            }
        }

        public UiPaginationControl()
        {
            Height = 38;
            Dock = DockStyle.Bottom;
            BackColor = UiTheme.Surface;
            Padding = new Padding(10, 4, 10, 4);

            _lblRecordInfo = new Label
            {
                AutoSize = true,
                Text = "Đang tải dữ liệu...",
                Font = new Font("Segoe UI", 8.75F, FontStyle.Regular),
                ForeColor = UiTheme.TextSecondary,
                TextAlign = ContentAlignment.MiddleLeft,
                Dock = DockStyle.Left,
                Margin = new Padding(0)
            };

            FlowLayoutPanel rightControls = new FlowLayoutPanel
            {
                Dock = DockStyle.Right,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                Margin = new Padding(0)
            };

            _lblPageSizePrompt = new Label
            {
                AutoSize = true,
                Text = "Số dòng/trang:",
                Font = new Font("Segoe UI", 8.75F, FontStyle.Regular),
                ForeColor = UiTheme.TextSecondary,
                TextAlign = ContentAlignment.MiddleRight,
                Margin = new Padding(0, 5, 4, 0)
            };

            _cboPageSize = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 8.5F, FontStyle.Regular),
                Width = 65,
                Height = 24,
                Margin = new Padding(0, 2, 12, 0)
            };
            _cboPageSize.Items.AddRange(new object[] { "25", "50", "100", "200" });
            _cboPageSize.SelectedIndex = 0; // Mặc định 25
            _cboPageSize.SelectedIndexChanged += CboPageSize_SelectedIndexChanged;

            _btnFirst = CreateNavButton("⏮", "Trang đầu");
            _btnPrev = CreateNavButton("◀", "Trang trước");

            _lblPageInfo = new Label
            {
                AutoSize = true,
                Text = "Trang 1 / 1",
                Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold),
                ForeColor = UiTheme.TextPrimary,
                TextAlign = ContentAlignment.MiddleCenter,
                Margin = new Padding(6, 5, 6, 0)
            };

            _btnNext = CreateNavButton("▶", "Trang sau");
            _btnLast = CreateNavButton("⏭", "Trang cuối");

            _btnFirst.Click += (s, e) => NavigateTo(1);
            _btnPrev.Click += (s, e) => NavigateTo(_currentPage - 1);
            _btnNext.Click += (s, e) => NavigateTo(_currentPage + 1);
            _btnLast.Click += (s, e) => NavigateTo(TotalPages);

            rightControls.Controls.Add(_lblPageSizePrompt);
            rightControls.Controls.Add(_cboPageSize);
            rightControls.Controls.Add(_btnFirst);
            rightControls.Controls.Add(_btnPrev);
            rightControls.Controls.Add(_lblPageInfo);
            rightControls.Controls.Add(_btnNext);
            rightControls.Controls.Add(_btnLast);

            Controls.Add(_lblRecordInfo);
            Controls.Add(rightControls);

            UpdateButtons();
        }

        private Button CreateNavButton(string text, string tooltip)
        {
            Button btn = new Button
            {
                Text = text,
                Width = 32,
                Height = 26,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI Symbol", 8.5F, FontStyle.Regular),
                Margin = new Padding(2, 1, 2, 0),
                Cursor = Cursors.Hand,
                TabStop = false
            };
            btn.FlatAppearance.BorderSize = 1;
            btn.FlatAppearance.BorderColor = UiTheme.Border;
            btn.BackColor = UiTheme.Surface;
            btn.ForeColor = UiTheme.TextPrimary;

            ToolTip tt = new ToolTip();
            tt.SetToolTip(btn, tooltip);

            return btn;
        }

        public void UpdateState(int currentPage, int pageSize, int totalRecords)
        {
            _isUpdatingState = true;
            try
            {
                _currentPage = Math.Max(1, currentPage);
                _pageSize = pageSize > 0 ? pageSize : 25;
                _totalRecords = Math.Max(0, totalRecords);

                // Đồng bộ combobox page size nếu khác
                string psStr = _pageSize.ToString();
                for (int i = 0; i < _cboPageSize.Items.Count; i++)
                {
                    if (_cboPageSize.Items[i].ToString() == psStr)
                    {
                        if (_cboPageSize.SelectedIndex != i)
                        {
                            _cboPageSize.SelectedIndex = i;
                        }
                        break;
                    }
                }

                int totalPages = TotalPages;
                if (_currentPage > totalPages && totalPages > 0)
                {
                    _currentPage = totalPages;
                }

                if (_totalRecords == 0)
                {
                    _lblRecordInfo.Text = "Không có bản ghi nào.";
                    _lblPageInfo.Text = "Trang 0 / 0";
                }
                else
                {
                    int from = (_currentPage - 1) * _pageSize + 1;
                    int to = Math.Min(_currentPage * _pageSize, _totalRecords);
                    _lblRecordInfo.Text = string.Format("Hiển thị {0:N0} - {1:N0} trong tổng số {2:N0} bản ghi", from, to, _totalRecords);
                    _lblPageInfo.Text = string.Format("Trang {0:N0} / {1:N0}", _currentPage, totalPages);
                }

                UpdateButtons();
            }
            finally
            {
                _isUpdatingState = false;
            }
        }

        public void Reset(int pageSize = 25)
        {
            _currentPage = 1;
            _pageSize = pageSize;
            UpdateState(1, pageSize, 0);
        }

        private void UpdateButtons()
        {
            int totalPages = TotalPages;
            bool canGoBack = _currentPage > 1 && _totalRecords > 0;
            bool canGoForward = _currentPage < totalPages && _totalRecords > 0;

            _btnFirst.Enabled = canGoBack;
            _btnPrev.Enabled = canGoBack;
            _btnNext.Enabled = canGoForward;
            _btnLast.Enabled = canGoForward;

            _btnFirst.BackColor = canGoBack ? UiTheme.Surface : UiTheme.SurfaceMuted;
            _btnPrev.BackColor = canGoBack ? UiTheme.Surface : UiTheme.SurfaceMuted;
            _btnNext.BackColor = canGoForward ? UiTheme.Surface : UiTheme.SurfaceMuted;
            _btnLast.BackColor = canGoForward ? UiTheme.Surface : UiTheme.SurfaceMuted;
        }

        private void NavigateTo(int page)
        {
            if (page < 1) page = 1;
            int totalPages = TotalPages;
            if (page > totalPages) page = totalPages;

            if (page != _currentPage)
            {
                _currentPage = page;
                OnPageChanged();
            }
        }

        private void CboPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_isUpdatingState) return;

            int newSize;
            if (int.TryParse(_cboPageSize.SelectedItem.ToString(), out newSize) && newSize != _pageSize)
            {
                _pageSize = newSize;
                _currentPage = 1; // Reset về trang 1 khi đổi page size
                OnPageChanged();
            }
        }

        protected virtual void OnPageChanged()
        {
            EventHandler<PageChangedEventArgs> handler = PageChanged;
            if (handler != null)
            {
                handler(this, new PageChangedEventArgs(_currentPage, _pageSize));
            }
        }
    }
}
