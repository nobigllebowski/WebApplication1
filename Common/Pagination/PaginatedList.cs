﻿namespace Common.Pagination
{
    public class PaginatedList<T>
    {
        public int PageIndex { get; }
        public int PageSize { get; }
        public int TotalCount { get; }
        public int TotalPages { get; }
        public List<T> Items { get; } = new();

        public bool HasPreviousPage => PageIndex > 1;
        public bool HasNextPage => PageIndex < TotalPages;

        public PaginatedList(List<T> items, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalCount = count;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            Items.AddRange(items);
        }

        public static PaginatedList<T> Create(
            IQueryable<T> source,
            int pageIndex,
            int pageSize)
        {
            var count = source.Count();
            var items = source
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return new PaginatedList<T>(items, count, pageIndex, pageSize);
        }
    }
}
