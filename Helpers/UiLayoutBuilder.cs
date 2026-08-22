using System.Drawing;
using System.Windows.Forms;

namespace DNQH_KeToanBanHang.Helpers
{
    public enum UiFieldSize
    {
        Code,
        Date,
        DateTime,
        Number,
        Money,
        ShortText,
        Selection,
        Medium,
        Wide
    }

    public sealed class UiFieldSpec
    {
        internal UiFieldSpec(Label label, Control field, UiFieldSize size, bool fullWidth, int columnSpan = 1)
        {
            Label = label;
            Field = field;
            Size = size;
            FullWidth = fullWidth;
            ColumnSpan = columnSpan;
        }

        internal Label Label { get; private set; }
        internal Control Field { get; private set; }
        internal UiFieldSize Size { get; private set; }
        internal bool FullWidth { get; private set; }
        internal int ColumnSpan { get; private set; }
    }

    public static class UiLayoutBuilder
    {
        public const int DefaultLabelWidth = 95;

        public static int GetFieldWidth(UiFieldSize size)
        {
            switch (size)
            {
                case UiFieldSize.Code:
                    return 160;
                case UiFieldSize.Date:
                    return 170;
                case UiFieldSize.DateTime:
                    return 185;
                case UiFieldSize.Number:
                    return 110;
                case UiFieldSize.Money:
                    return 190;
                case UiFieldSize.ShortText:
                    return 140;
                case UiFieldSize.Selection:
                    return 190;
                case UiFieldSize.Medium:
                    return 260;
                case UiFieldSize.Wide:
                    return 420;
                default:
                    return 190;
            }
        }

        public static UiFieldSpec Field(Label label, Control field, UiFieldSize size = UiFieldSize.Medium)
        {
            return new UiFieldSpec(label, field, size, false, 1);
        }

        public static UiFieldSpec FullWidthField(Label label, Control field)
        {
            return new UiFieldSpec(label, field, UiFieldSize.Wide, true, -1);
        }

        public static UiFieldSpec SpannedField(Label label, Control field, int columnSpan)
        {
            return new UiFieldSpec(label, field, UiFieldSize.Wide, false, columnSpan);
        }

        public static TableLayoutPanel CreateStructuredGridLayout(int columns, params UiFieldSpec[] fields)
        {
            return CreateStructuredGridLayout(columns, DefaultLabelWidth, fields);
        }

        public static TableLayoutPanel CreateStructuredGridLayout(int columns, int labelWidth, params UiFieldSpec[] fields)
        {
            if (columns <= 0) columns = 1;
            if (labelWidth <= 0) labelWidth = DefaultLabelWidth;

            int tableColumns = columns * 2;
            int currentCol = 0;
            int currentRow = 0;

            var placements = new System.Collections.Generic.List<GridPlacement>();
            var multilineRows = new System.Collections.Generic.HashSet<int>();

            for (int i = 0; i < fields.Length; i++)
            {
                UiFieldSpec spec = fields[i];
                if (spec == null) continue;

                bool isMulti = spec.FullWidth || (spec.Field is TextBox && ((TextBox)spec.Field).Multiline);

                if (spec.FullWidth)
                {
                    if (currentCol > 0)
                    {
                        currentRow++;
                        currentCol = 0;
                    }
                    placements.Add(new GridPlacement(spec, currentRow, 0, tableColumns - 1));
                    if (isMulti) multilineRows.Add(currentRow);
                    currentRow++;
                    currentCol = 0;
                }
                else if (spec.ColumnSpan > 1)
                {
                    int span = System.Math.Min(spec.ColumnSpan, columns);
                    if (currentCol + span > columns)
                    {
                        currentRow++;
                        currentCol = 0;
                    }
                    placements.Add(new GridPlacement(spec, currentRow, currentCol * 2, span * 2 - 1));
                    if (isMulti) multilineRows.Add(currentRow);
                    currentCol += span;
                    if (currentCol >= columns)
                    {
                        currentCol = 0;
                        currentRow++;
                    }
                }
                else
                {
                    placements.Add(new GridPlacement(spec, currentRow, currentCol * 2, 1));
                    if (isMulti) multilineRows.Add(currentRow);
                    currentCol++;
                    if (currentCol >= columns)
                    {
                        currentCol = 0;
                        currentRow++;
                    }
                }
            }

            int totalRows = currentCol > 0 ? currentRow + 1 : currentRow;
            if (totalRows == 0) totalRows = 1;

            TableLayoutPanel grid = new TableLayoutPanel
            {
                Name = "tlpStructuredFields",
                Dock = DockStyle.Top,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                ColumnCount = tableColumns,
                RowCount = totalRows,
                Margin = new Padding(0),
                Padding = new Padding(0, 4, 0, 4)
            };

            float fieldPercent = 100F / columns;
            for (int c = 0; c < columns; c++)
            {
                grid.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, labelWidth));
                grid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, fieldPercent));
            }

            for (int r = 0; r < totalRows; r++)
            {
                if (multilineRows.Contains(r))
                {
                    grid.RowStyles.Add(new RowStyle(SizeType.Absolute, 52F));
                }
                else
                {
                    grid.RowStyles.Add(new RowStyle(SizeType.Absolute, 34F));
                }
            }

            for (int i = 0; i < placements.Count; i++)
            {
                GridPlacement p = placements[i];
                UiFieldSpec spec = p.Spec;

                spec.Label.AutoSize = false;
                spec.Label.Dock = DockStyle.Fill;
                spec.Label.TextAlign = ContentAlignment.MiddleRight;
                spec.Label.Margin = new Padding(0, 0, 6, 0);

                spec.Field.Dock = DockStyle.Fill;
                spec.Field.Margin = new Padding(0, 3, 10, 3);
                if (spec.Field is Label)
                {
                    Label valueLabel = (Label)spec.Field;
                    valueLabel.AutoSize = false;
                    valueLabel.AutoEllipsis = true;
                    valueLabel.TextAlign = ContentAlignment.MiddleLeft;
                }

                grid.Controls.Add(spec.Label, p.Col, p.Row);
                grid.Controls.Add(spec.Field, p.Col + 1, p.Row);
                if (p.Span > 1)
                {
                    grid.SetColumnSpan(spec.Field, p.Span);
                }
            }

            return grid;
        }

        private sealed class GridPlacement
        {
            public GridPlacement(UiFieldSpec spec, int row, int col, int span)
            {
                Spec = spec;
                Row = row;
                Col = col;
                Span = span;
            }
            public UiFieldSpec Spec { get; private set; }
            public int Row { get; private set; }
            public int Col { get; private set; }
            public int Span { get; private set; }
        }

        public static FlowLayoutPanel CreateSemanticFieldLayout(params UiFieldSpec[] fields)
        {
            FlowLayoutPanel layout = new FlowLayoutPanel
            {
                Name = "flpSemanticFields",
                Dock = DockStyle.Top,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = true,
                Margin = new Padding(0),
                Padding = new Padding(0, 4, 0, 0)
            };

            Control previousHost = null;
            for (int i = 0; i < fields.Length; i++)
            {
                UiFieldSpec spec = fields[i];
                if (spec.FullWidth && previousHost != null)
                {
                    layout.SetFlowBreak(previousHost, true);
                }

                Control host = CreateSemanticFieldHost(spec);
                layout.Controls.Add(host);
                if (spec.FullWidth)
                {
                    host.Tag = "FullWidthField";
                    layout.SetFlowBreak(host, true);
                }
                previousHost = host;
            }

            layout.SizeChanged += delegate { UpdateSemanticLayoutBounds(layout); };
            layout.Layout += delegate { UpdateSemanticLayoutBounds(layout); };
            layout.ParentChanged += delegate { UpdateSemanticLayoutBounds(layout); };
            return layout;
        }

        public static void AddFullWidthNote(Control layout, Label note)
        {
            if (layout is TableLayoutPanel)
            {
                TableLayoutPanel grid = (TableLayoutPanel)layout;
                int row = grid.RowCount;
                grid.RowCount = row + 1;
                grid.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                note.AutoSize = true;
                int labelWidth = grid.ColumnStyles.Count > 0 ? (int)grid.ColumnStyles[0].Width : DefaultLabelWidth;
                note.Margin = new Padding(labelWidth + 6, 2, 0, 4);
                grid.Controls.Add(note, 0, row);
                grid.SetColumnSpan(note, grid.ColumnCount);
                return;
            }

            if (layout is FlowLayoutPanel)
            {
                FlowLayoutPanel flow = (FlowLayoutPanel)layout;
                if (flow.Controls.Count > 0)
                {
                    flow.SetFlowBreak(flow.Controls[flow.Controls.Count - 1], true);
                }
                note.AutoSize = true;
                note.Margin = new Padding(0, 2, 0, 4);
                flow.Controls.Add(note);
                flow.SetFlowBreak(note, true);
            }
        }

        private static Control CreateSemanticFieldHost(UiFieldSpec spec)
        {
            int labelWidth = GetSemanticLabelWidth(spec.Label);
            int fieldWidth = GetFieldWidth(spec.Size);
            int hostHeight = spec.FullWidth ? 58 : 38;

            TableLayoutPanel host = new TableLayoutPanel
            {
                Name = "tlpSemantic_" + spec.Field.Name,
                ColumnCount = 2,
                RowCount = 1,
                Width = labelWidth + fieldWidth,
                Height = hostHeight,
                MinimumSize = new Size(labelWidth + (spec.FullWidth ? 240 : fieldWidth), hostHeight),
                Margin = new Padding(0, 0, 14, 4),
                Padding = new Padding(0)
            };
            host.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, labelWidth));
            host.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            host.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            spec.Label.AutoSize = false;
            spec.Label.Dock = DockStyle.Fill;
            spec.Label.TextAlign = ContentAlignment.MiddleRight;
            spec.Label.Margin = new Padding(0, 0, 6, 0);
            spec.Field.Dock = DockStyle.Fill;
            spec.Field.Margin = new Padding(0, 4, 0, 4);
            if (spec.Field is Label)
            {
                Label valueLabel = (Label)spec.Field;
                valueLabel.AutoSize = false;
                valueLabel.AutoEllipsis = true;
                valueLabel.TextAlign = ContentAlignment.MiddleLeft;
            }

            host.Controls.Add(spec.Label, 0, 0);
            host.Controls.Add(spec.Field, 1, 0);
            return host;
        }

        private static int GetSemanticLabelWidth(Label label)
        {
            int preferred = TextRenderer.MeasureText(label.Text ?? string.Empty, label.Font).Width + 14;
            return System.Math.Max(72, System.Math.Min(132, preferred));
        }

        private static void ResizeFullWidthFields(FlowLayoutPanel layout)
        {
            int availableWidth = layout.ClientSize.Width - layout.Padding.Horizontal;
            if (availableWidth <= 0)
            {
                return;
            }

            foreach (Control host in layout.Controls)
            {
                if (!object.Equals(host.Tag, "FullWidthField"))
                {
                    continue;
                }

                int targetWidth = System.Math.Max(host.MinimumSize.Width, availableWidth - host.Margin.Horizontal);
                if (host.Width != targetWidth)
                {
                    host.Width = targetWidth;
                }
            }
        }

        private static void UpdateSemanticLayoutBounds(FlowLayoutPanel layout)
        {
            ResizeFullWidthFields(layout);

            Control parent = layout.Parent;
            if (parent == null || !parent.AutoSize)
            {
                return;
            }

            int requiredHeight = layout.Top + layout.Height + parent.Padding.Bottom + 4;
            if (parent.MinimumSize.Height != requiredHeight)
            {
                parent.MinimumSize = new Size(parent.MinimumSize.Width, requiredHeight);
            }
        }

        public static Label BuildCrudPage(
            Form form,
            Label title,
            string subtitleText,
            GroupBox informationGroup,
            Control informationLayout,
            Control[] toolbarControls,
            DataGridView grid)
        {
            form.SuspendLayout();
            form.Controls.Clear();
            form.MinimumSize = new Size(720, 520);
            form.FormBorderStyle = FormBorderStyle.Sizable;
            form.MaximizeBox = true;
            form.Padding = new Padding(UiTheme.PagePadding);

            TableLayoutPanel root = new TableLayoutPanel
            {
                Name = "tlpCrudRoot",
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 5,
                BackColor = UiTheme.Canvas
            };
            root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            root.RowStyles.Add(new RowStyle(SizeType.Absolute, 58F));
            root.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            root.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            root.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            root.RowStyles.Add(new RowStyle(SizeType.Absolute, 28F));

            Panel heading = new Panel { Dock = DockStyle.Fill };
            title.Location = new Point(0, 0);
            title.AutoSize = true;
            Label subtitle = new Label
            {
                AutoSize = true,
                Location = new Point(2, 34),
                Text = subtitleText,
                ForeColor = UiTheme.TextSecondary
            };
            heading.Controls.Add(title);
            heading.Controls.Add(subtitle);

            informationGroup.Controls.Clear();
            informationGroup.Controls.Add(informationLayout);
            informationGroup.Dock = DockStyle.Fill;
            informationGroup.AutoSize = true;
            informationGroup.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            informationGroup.Padding = new Padding(12, 10, 12, 12);
            informationGroup.Margin = new Padding(0, 0, 0, UiTheme.SectionGap);

            FlowLayoutPanel toolbar = new FlowLayoutPanel
            {
                Name = "flpCrudActions",
                Dock = DockStyle.Fill,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                WrapContents = true,
                Padding = new Padding(0),
                Margin = new Padding(0)
            };
            foreach (Control control in toolbarControls)
            {
                control.Anchor = AnchorStyles.None;
                control.Margin = control is Label
                    ? new Padding(12, 10, 6, 8)
                    : new Padding(0, 0, 10, 8);
                toolbar.Controls.Add(control);
            }

            grid.Dock = DockStyle.Fill;
            grid.Margin = new Padding(0, UiTheme.SectionGap, 0, 0);

            Label status = new Label
            {
                Name = "lblPageStatus",
                AutoEllipsis = true,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                Margin = new Padding(0, 4, 0, 0)
            };

            root.Controls.Add(heading, 0, 0);
            root.Controls.Add(informationGroup, 0, 1);
            root.Controls.Add(toolbar, 0, 2);
            root.Controls.Add(grid, 0, 3);
            root.Controls.Add(status, 0, 4);
            form.Controls.Add(root);
            form.ResumeLayout(true);
            return status;
        }

        public static void ApplyCrudStyle(
            Form form,
            Button addButton,
            Button editButton,
            Button deleteButton,
            Button refreshButton,
            Button searchButton,
            DataGridView grid,
            Label status)
        {
            if (grid != null)
            {
                grid.AutoGenerateColumns = false;
                UiStyler.HideInternalColumns(grid);
            }
            UiStyler.Apply(form);
            UiStyler.StyleButton(addButton, UiButtonRole.Primary);
            UiStyler.StyleButton(editButton, UiButtonRole.Information);
            UiStyler.StyleButton(deleteButton, UiButtonRole.Danger);
            UiStyler.StyleButton(refreshButton, UiButtonRole.Secondary);
            UiStyler.StyleButton(searchButton, UiButtonRole.Secondary);
            UiStyler.StyleStatusLabel(status, UiStatusKind.Neutral, "Sẵn sàng.");
        }

        public static Label BuildCommandBar(Panel host, Button[] buttons, string initialStatus)
        {
            host.Controls.Clear();
            host.Dock = DockStyle.Fill;
            host.Padding = new Padding(12, 6, 10, 6);

            TableLayoutPanel layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1
            };
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));

            Label status = new Label
            {
                Name = "lblEntryStatus",
                Dock = DockStyle.Fill,
                AutoEllipsis = true,
                Text = initialStatus,
                TextAlign = ContentAlignment.MiddleLeft,
                Margin = new Padding(0)
            };

            FlowLayoutPanel actions = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoSize = true,
                WrapContents = false,
                Padding = new Padding(8, 0, 0, 0)
            };
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].Margin = new Padding(0, 0, i == buttons.Length - 1 ? 0 : 8, 0);
                actions.Controls.Add(buttons[i]);
            }

            layout.Controls.Add(status, 0, 0);
            layout.Controls.Add(actions, 1, 0);
            host.Controls.Add(layout);
            return status;
        }

        public static void BuildHistoryPage(
            TabPage page,
            Panel filterHost,
            Control[] filterControls,
            Control content,
            Label status)
        {
            BuildHistoryPage(page, filterHost, filterControls, content, (Control)status, 30);
        }

        public static void BuildHistoryPage(
            TabPage page,
            Panel filterHost,
            Control[] filterControls,
            Control content,
            Control footerControl,
            int footerHeight = 38)
        {
            filterHost.Controls.Clear();
            filterHost.Dock = DockStyle.Fill;
            filterHost.AutoSize = true;
            filterHost.AutoSizeMode = AutoSizeMode.GrowAndShrink;

            FlowLayoutPanel filters = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                WrapContents = true,
                Padding = new Padding(12, 7, 8, 3)
            };
            foreach (Control control in filterControls)
            {
                control.Anchor = AnchorStyles.None;
                control.Margin = control is Label
                    ? new Padding(0, 9, 6, 6)
                    : new Padding(0, 0, 10, 6);
                filters.Controls.Add(control);
            }
            filterHost.Controls.Add(filters);

            page.Controls.Clear();
            TableLayoutPanel root = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 3,
                BackColor = UiTheme.Canvas
            };
            root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            root.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            root.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            root.RowStyles.Add(new RowStyle(SizeType.Absolute, footerHeight > 0 ? (float)footerHeight : 38F));

            content.Dock = DockStyle.Fill;
            content.Margin = new Padding(0, 10, 0, 0);
            if (footerControl != null)
            {
                footerControl.Dock = DockStyle.Fill;
                footerControl.Margin = new Padding(0);
                Label lbl = footerControl as Label;
                if (lbl != null)
                {
                    lbl.Padding = new Padding(8, 0, 0, 0);
                    lbl.TextAlign = ContentAlignment.MiddleLeft;
                }
            }

            root.Controls.Add(filterHost, 0, 0);
            root.Controls.Add(content, 0, 1);
            if (footerControl != null)
            {
                root.Controls.Add(footerControl, 0, 2);
            }
            page.Controls.Add(root);
        }

        public static FlowLayoutPanel BuildFilterBar(Panel host, Control[] controls)
        {
            host.Controls.Clear();
            host.Dock = DockStyle.Fill;
            host.AutoSize = true;
            host.AutoSizeMode = AutoSizeMode.GrowAndShrink;

            FlowLayoutPanel filters = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                WrapContents = true,
                Padding = new Padding(12, 7, 8, 3),
                Margin = new Padding(0)
            };
            foreach (Control control in controls)
            {
                control.Anchor = AnchorStyles.None;
                control.Margin = control is Label
                    ? new Padding(0, 9, 6, 6)
                    : new Padding(0, 0, 10, 6);
                filters.Controls.Add(control);
            }
            host.Controls.Add(filters);
            return filters;
        }

        public static Label BuildReportPage(
            TabPage page,
            Panel filterHost,
            Control[] filterControls,
            DataGridView grid,
            Panel summaryHost,
            Control[] summaryControls,
            string initialStatus)
        {
            BuildFilterBar(filterHost, filterControls);

            summaryHost.Controls.Clear();
            summaryHost.Dock = DockStyle.Fill;
            summaryHost.AutoSize = true;
            summaryHost.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            summaryHost.Padding = new Padding(12, 7, 8, 3);
            FlowLayoutPanel summaries = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                WrapContents = true,
                Margin = new Padding(0)
            };
            for (int i = 0; i < summaryControls.Length; i++)
            {
                summaryControls[i].AutoSize = true;
                summaryControls[i].Margin = new Padding(0, 0, i == summaryControls.Length - 1 ? 0 : 28, 4);
                summaries.Controls.Add(summaryControls[i]);
            }
            summaryHost.Controls.Add(summaries);

            Label status = new Label
            {
                Name = "lblReportStatus",
                Dock = DockStyle.Fill,
                AutoEllipsis = true,
                Text = initialStatus,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(8, 0, 0, 0),
                Margin = new Padding(0)
            };

            page.Controls.Clear();
            TableLayoutPanel root = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 4,
                BackColor = UiTheme.Canvas
            };
            root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            root.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            root.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            root.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            root.RowStyles.Add(new RowStyle(SizeType.Absolute, 28F));
            grid.Dock = DockStyle.Fill;
            grid.Margin = new Padding(0, 10, 0, 10);
            summaryHost.Margin = new Padding(0);

            root.Controls.Add(filterHost, 0, 0);
            root.Controls.Add(grid, 0, 1);
            root.Controls.Add(summaryHost, 0, 2);
            root.Controls.Add(status, 0, 3);
            page.Controls.Add(root);
            return status;
        }
    }
}
