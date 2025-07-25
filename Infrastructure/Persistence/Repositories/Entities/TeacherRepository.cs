﻿using Common.Pagination;
using Dapper;
using Domain.Entities;
using Infrastructure.Extensions;
using Infrastructure.Persistence.Repositories.Base;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Persistence.Repositories.Entities
{
    public class TeacherRepository : BaseRepository<Teacher>, ITeacherRepository
    {
        private readonly string _connectionString;

        public TeacherRepository(AppDbContext context, IConfiguration config) : base(context)
        {
            _connectionString = config.GetConnectionString("DefaultConnection")
                                ?? throw new InvalidOperationException("Строка подключения 'DefaultConnection' не найдена.");
        }

        public async Task<PaginatedList<Teacher>> GetFilteredTeachersAsync(
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

            var allTeachers = await connection.QueryAsync<Teacher>("SELECT * FROM Teachers");

            if (isActive)
            {
                allTeachers = allTeachers.Where(s => !s.IsDeleted);
            }

            if (!string.IsNullOrWhiteSpace(nameFilter))
            {
                nameFilter = nameFilter.ToLower();
                var nameParts = nameFilter.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                allTeachers = nameParts.Length switch
                {
                    1 => allTeachers.Where(s =>
                        s.FirstName.Contains(nameParts[0], StringComparison.OrdinalIgnoreCase) ||
                        s.LastName.Contains(nameParts[0], StringComparison.OrdinalIgnoreCase)),

                    2 => allTeachers.Where(s =>
                        (s.FirstName.Contains(nameParts[0], StringComparison.OrdinalIgnoreCase) &&
                         s.LastName.Contains(nameParts[1], StringComparison.OrdinalIgnoreCase)) ||
                        (s.FirstName.Contains(nameParts[1], StringComparison.OrdinalIgnoreCase) &&
                         s.LastName.Contains(nameParts[0], StringComparison.OrdinalIgnoreCase))),

                    _ => allTeachers.Where(s =>
                        s.FirstName.Contains(nameFilter, StringComparison.OrdinalIgnoreCase) ||
                        s.LastName.Contains(nameFilter, StringComparison.OrdinalIgnoreCase))
                };
            }

            var query = allTeachers.AsQueryable();

            query = query.WhereAgeBetween(minAge, maxAge);

            if (departmentId.HasValue)
            {
                query = query.Where(s => s.DepartmentId == departmentId.Value);
            }

            query = query.OrderBy(s => s.LastName).ThenBy(s => s.FirstName);

            // Применяем пагинацию
            return PaginatedList<Teacher>.Create(query, pageNumber, pageSize);
        }
    }
}
