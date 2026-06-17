using System;
using System.Collections.Generic;
using System.Data;

namespace DNQH_KeToanBanHang.Models
{
    /// <summary>
    /// Model chứa kết quả truy vấn phân trang dùng chung cho các danh sách kiểu đối tượng (DTO / Entity).
    /// </summary>
    public class PagedResult<T>
    {
        public List<T> Items { get; set; }
        public int TotalRecords { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }

        public PagedResult()
        {
            Items = new List<T>();
            PageIndex = 1;
            PageSize = 25;
            TotalRecords = 0;
        }

        public PagedResult(List<T> items, int totalRecords, int pageIndex, int pageSize)
        {
            Items = items ?? new List<T>();
            TotalRecords = Math.Max(0, totalRecords);
            PageIndex = Math.Max(1, pageIndex);
            PageSize = pageSize > 0 ? pageSize : 25;
        }

        public int TotalPages
        {
            get
            {
                if (PageSize <= 0 || TotalRecords <= 0)
                {
                    return 1;
                }
                return (int)Math.Ceiling((double)TotalRecords / PageSize);
            }
        }

        public bool HasPreviousPage
        {
            get { return PageIndex > 1; }
        }

        public bool HasNextPage
        {
            get { return PageIndex < TotalPages; }
        }

        public int FromRecord
        {
            get
            {
                if (TotalRecords == 0) return 0;
                return (PageIndex - 1) * PageSize + 1;
            }
        }

        public int ToRecord
        {
            get
            {
                if (TotalRecords == 0) return 0;
                return Math.Min(PageIndex * PageSize, TotalRecords);
            }
        }
    }

    /// <summary>
    /// Model chứa kết quả truy vấn phân trang trả về DataTable.
    /// </summary>
    public class PagedDataTable
    {
        public DataTable Table { get; set; }
        public int TotalRecords { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }

        public PagedDataTable()
        {
            Table = new DataTable();
            PageIndex = 1;
            PageSize = 25;
            TotalRecords = 0;
        }

        public PagedDataTable(DataTable table, int totalRecords, int pageIndex, int pageSize)
        {
            Table = table ?? new DataTable();
            TotalRecords = Math.Max(0, totalRecords);
            PageIndex = Math.Max(1, pageIndex);
            PageSize = pageSize > 0 ? pageSize : 25;
        }

        public int TotalPages
        {
            get
            {
                if (PageSize <= 0 || TotalRecords <= 0)
                {
                    return 1;
                }
                return (int)Math.Ceiling((double)TotalRecords / PageSize);
            }
        }

        public bool HasPreviousPage
        {
            get { return PageIndex > 1; }
        }

        public bool HasNextPage
        {
            get { return PageIndex < TotalPages; }
        }

        public int FromRecord
        {
            get
            {
                if (TotalRecords == 0) return 0;
                return (PageIndex - 1) * PageSize + 1;
            }
        }

        public int ToRecord
        {
            get
            {
                if (TotalRecords == 0) return 0;
                return Math.Min(PageIndex * PageSize, TotalRecords);
            }
        }
    }
}
