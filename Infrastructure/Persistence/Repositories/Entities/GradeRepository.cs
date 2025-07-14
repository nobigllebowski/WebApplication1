using Domain.Entities;
using Infrastructure.Persistence.Repositories.Base;

namespace Infrastructure.Persistence.Repositories.Entities
{
    public class GradeRepository(AppDbContext context) : BaseRepository<Grade>(context), IGradeRepository
    {
    }
}
