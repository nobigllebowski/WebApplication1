using Application.DTOs.Department;
using Common.Pagination;

namespace Application.Interfaces
{
    public interface IDepartmentService
    {
        Task<DepartmentDto> GetDepartmentByIdAsync(Guid id);
        PaginatedList<DepartmentDto> GetDepartments(DepartmentFilterDto filter);
        Task<List<DepartmentDto>> GetAllDepartmentsAsync();
        Task<List<DepartmentDto>> GetAllActiveDepartmentsAsync();

        Task CreateDepartmentAsync(CreateDepartmentDto dto);
        Task UpdateDepartmentAsync(UpdateDepartmentDto dto);
        Task DeactivateDepartmentAsync(Guid id);
    }
}
