using Domain.Entities.Base;

namespace Infrastructure.Extensions
{
    public static class QueryExtensions
    {
        public static IQueryable<TPerson> WhereAgeBetween<TPerson>(
            this IQueryable<TPerson> query,
            int? minAge,
            int? maxAge) where TPerson : Person
        {
            var today = DateTime.Today;

            if (minAge.HasValue)
            {
                var minDate = today.AddYears(-minAge.Value);
                query = query.Where(s => s.DateOfBirth <= minDate);
            }

            if (maxAge.HasValue)
            {
                var maxDate = today.AddYears(-maxAge.Value - 1).AddDays(1);
                query = query.Where(s => s.DateOfBirth >= maxDate);
            }

            return query;
        }
    }
}
