using Application.DTOs.Grade;

namespace Application.Interfaces
{
    public interface IGradeService
    {
        Task<GradeDto> GetGradeByIdAsync(Guid id);
        Task<List<GradeDto>> GetAllGradesAsync();

        Task CreateGradeAsync(CreateGradeDto dto);
        Task UpdateGradeAsync(UpdateGradeDto dto);
        Task DeactivateGradeAsync(Guid id);
    }
}
