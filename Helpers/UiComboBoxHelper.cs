using System;
using System.Windows.Forms;

namespace DNQH_KeToanBanHang.Helpers
{
    public static class UiComboBoxHelper
    {
        /// <summary>
        /// Nạp danh sách các mục trạng thái an toàn vào ComboBox
        /// </summary>
        public static void PopulateItems(ComboBox cbo, string[] items, string defaultSelection = null, bool includeAllOption = false, string allOptionText = "-- Tất cả --")
        {
            if (cbo == null || items == null) return;

            cbo.BeginUpdate();
            try
            {
                cbo.Items.Clear();

                if (includeAllOption)
                {
                    cbo.Items.Add(allOptionText);
                }

                for (int i = 0; i < items.Length; i++)
                {
                    if (items[i] != null)
                    {
                        cbo.Items.Add(items[i]);
                    }
                }

                if (!string.IsNullOrEmpty(defaultSelection) && cbo.Items.Contains(defaultSelection))
                {
                    cbo.SelectedItem = defaultSelection;
                }
                else if (cbo.Items.Count > 0)
                {
                    cbo.SelectedIndex = 0;
                }
            }
            finally
            {
                cbo.EndUpdate();
            }
        }

        /// <summary>
        /// Chọn giá trị an toàn trong ComboBox theo chuỗi (không phân biệt hoa thường và hỗ trợ fallback)
        /// </summary>
        public static bool SafeSelect(ComboBox cbo, string targetValue, int fallbackIndex = 0)
        {
            if (cbo == null || cbo.Items.Count == 0) return false;

            if (!string.IsNullOrWhiteSpace(targetValue))
            {
                string trimmed = targetValue.Trim();
                for (int i = 0; i < cbo.Items.Count; i++)
                {
                    object item = cbo.Items[i];
                    if (item != null && string.Equals(item.ToString().Trim(), trimmed, StringComparison.OrdinalIgnoreCase))
                    {
                        cbo.SelectedIndex = i;
                        return true;
                    }
                }
            }

            if (fallbackIndex >= 0 && fallbackIndex < cbo.Items.Count)
            {
                cbo.SelectedIndex = fallbackIndex;
            }
            else
            {
                cbo.SelectedIndex = -1;
            }
            return false;
        }
    }
}
