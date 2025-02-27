using StudReg.Domain;
using System.Linq.Expressions;

namespace StudReg.Repositories.Interfaces
{
    public interface IStudentRepository : IBaseRepository<Student>
    {
        Task<Student?> GetAsync(Guid id);
        Task<Student?> GetAsync(Expression<Func<Student, bool>> exp);
        Task<ICollection<Student>> GetSelectedAsync(List<Guid> ids);
        Task<ICollection<Student>> GetSelectedAsync(Expression<Func<Student, bool>> exp);
        Task<PaginationResult<Student>> GetAllAsync(PageRequest pageRequest);
    }
}
