using Common.Pagination;
using Domain.Entities;
using Infrastructure.Persistence.Repositories.Base;

namespace Infrastructure.Persistence.Repositories.Entities
{
    public class CourseRepository(AppDbContext context) : BaseRepository<Course>(context), ICourseRepository
    {
        public PaginatedList<Course> GetFilteredCourses(
           string? nameFilter = null,
           int pageNumber = 1,
           int pageSize = 10)
        {
            var query = _context.Courses.AsQueryable();

            if (!string.IsNullOrWhiteSpace(nameFilter))
            {
                query.Where(_ => _.Name.Contains(nameFilter));
            }

            // Добавляем сортировку (по умолчанию - по имени)
            query = query.OrderBy(s => s.Name);

            // Применяем пагинацию
            return PaginatedList<Course>.Create(query, pageNumber, pageSize);
        }
    }
}
