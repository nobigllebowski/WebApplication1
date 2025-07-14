namespace Common.Filters
{
    public abstract class PaginationFilter
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        public virtual bool IsValid()
        {
            return Page >= 1 && PageSize is > 0 and <= 100;
        }
    }
}
