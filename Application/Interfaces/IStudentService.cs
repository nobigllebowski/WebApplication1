using Application.DTOs.Student;
using Common.Pagination;

namespace Application.Interfaces
{
    public interface IStudentService
    {
        Task<StudentDto> GetStudentByIdAsync(Guid id);
        Task<PaginatedList<StudentDto>> GetStudentsAsync(StudentFilterDto filter);
        Task<List<StudentDto>> GetAllStudentsAsync();
        Task<List<StudentDto>> GetAllActiveStudentsAsync();

        Task CreateStudentAsync(CreateStudentDto dto);
        Task UpdateStudentAsync(UpdateStudentDto dto);
        Task DeactivateStudentAsync(Guid id);
    }
}
