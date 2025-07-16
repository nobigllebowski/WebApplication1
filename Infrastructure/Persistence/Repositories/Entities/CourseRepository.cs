using Common.Pagination;
using Domain.Entities;
using Infrastructure.Extensions;
using Infrastructure.Persistence.Repositories.Base;

namespace Infrastructure.Persistence.Repositories.Entities
{
    public class CourseRepository(AppDbContext context) : BaseRepository<Course>(context), ICourseRepository
    {
        public PaginatedList<Course> GetFilteredCourses(
           string? nameFilter = null,
           bool isActive = true,
           int pageNumber = 1,
           int pageSize = 10)
        {
            var query = _context.Courses.AsQueryable();

            query = query
                .WhereIf(isActive, s => !s.IsDeleted)
                .WhereIf(!string.IsNullOrWhiteSpace(nameFilter), _ => _.Name.Contains(nameFilter))
                .OrderBy(s => s.Name);

            // Применяем пагинацию
            return PaginatedList<Course>.Create(query, pageNumber, pageSize);
        }
    }
}
