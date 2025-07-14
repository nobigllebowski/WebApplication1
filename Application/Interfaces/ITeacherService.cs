using Application.DTOs.Teacher;
using Common.Pagination;

namespace Application.Interfaces
{
    public interface ITeacherService
    {
        Task<TeacherDto> GetTeacherByIdAsync(Guid id);
        Task<PaginatedList<TeacherDto>> GetTeachersAsync(TeacherFilterDto filter);
        Task<List<TeacherDto>> GetAllTeachersAsync();
        Task<List<TeacherDto>> GetAllActiveTeachersAsync();

        Task CreateTeacherAsync(CreateTeacherDto dto);
        Task UpdateTeacherAsync(UpdateTeacherDto dto);
        Task DeactivateTeacherAsync(Guid id);
    }
}
