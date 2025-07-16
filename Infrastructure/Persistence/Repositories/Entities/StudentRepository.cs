using Common.Pagination;
using Dapper;
using Domain.Entities;
using Infrastructure.Extensions;
using Infrastructure.Persistence.Repositories.Base;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Persistence.Repositories.Entities
{
    public class StudentRepository : BaseRepository<Student>, IStudentRepository
    {
        private readonly string _connectionString;

        public StudentRepository(AppDbContext context, IConfiguration config) : base(context)
        {
            _connectionString = config.GetConnectionString("DefaultConnection")
                                ?? throw new InvalidOperationException("Строка подключения 'DefaultConnection' не найдена.");
        }

        public async Task<PaginatedList<Student>> GetFilteredStudentsAsync(
           string? nameFilter = null,
           int? minAge = null,
           int? maxAge = null,           
           Guid? departmentId = null,
           bool isActive = true,
           int pageNumber = 1,
           int pageSize = 10)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            var allStudents = await connection.QueryAsync<Student>("SELECT * FROM Students");

            allStudents = allStudents.WhereIf(isActive, s => !s.IsDeleted);

            if (!string.IsNullOrWhiteSpace(nameFilter))
            {
                nameFilter = nameFilter.ToLower();
                var nameParts = nameFilter.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                allStudents = nameParts.Length switch
                {
                    1 => allStudents.Where(s =>
                        s.FirstName.Contains(nameParts[0], StringComparison.OrdinalIgnoreCase) ||
                        s.LastName.Contains(nameParts[0], StringComparison.OrdinalIgnoreCase)),

                    2 => allStudents.Where(s =>
                        (s.FirstName.Contains(nameParts[0], StringComparison.OrdinalIgnoreCase) &&
                         s.LastName.Contains(nameParts[1], StringComparison.OrdinalIgnoreCase)) ||
                        (s.FirstName.Contains(nameParts[1], StringComparison.OrdinalIgnoreCase) &&
                         s.LastName.Contains(nameParts[0], StringComparison.OrdinalIgnoreCase))),

                    _ => allStudents.Where(s =>
                        s.FirstName.Contains(nameFilter, StringComparison.OrdinalIgnoreCase) ||
                        s.LastName.Contains(nameFilter, StringComparison.OrdinalIgnoreCase))
                };
            }

            var query = allStudents.AsQueryable()
                .WhereAgeBetween(minAge, maxAge)
                .WhereIf(departmentId.HasValue, s => s.DepartmentId == departmentId.Value)
                .OrderBy(s => s.LastName)
                .ThenBy(s => s.FirstName);

            // Применяем пагинацию
            return PaginatedList<Student>.Create(query, pageNumber, pageSize);
        }
    }
}
