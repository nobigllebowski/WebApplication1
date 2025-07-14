using Common.Pagination;
using Domain.Entities;
using Infrastructure.Persistence.Repositories.Base;

namespace Infrastructure.Persistence.Repositories.Entities
{
    public class DepartmentRepository(AppDbContext context) : BaseRepository<Department>(context), IDepartmentRepository
    {
        public PaginatedList<Department> GetFilteredDepartments(
            string? nameFilter = null,
            bool isActive = true,
            int pageNumber = 1,
            int pageSize = 10)
        {
            var query = _context.Departments.AsQueryable();

            if (isActive)
            {
                query = query.Where(s => !s.IsDeleted);
            }

            if (!string.IsNullOrWhiteSpace(nameFilter))
            {
                query.Where(_ => _.Name.Contains(nameFilter));
            }

            // Добавляем сортировку (по умолчанию - по имени)
            query = query.OrderBy(s => s.Name);

            // Применяем пагинацию
            return PaginatedList<Department>.Create(query, pageNumber, pageSize);
        }
    }
}
