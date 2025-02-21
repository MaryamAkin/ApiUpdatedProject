using StudReg.Domain;
using System.Linq.Expressions;

namespace StudReg.Repositories.Interfaces
{
    public interface IGuardianRepository : IBaseRepository<Guardian>
    {
        
        Task<Guardian?> GetAsync(Guid id);
        Task<Guardian?> GetAsync(Expression<Func<Guardian, bool>> exp);
        Task<ICollection<Guardian>> GetAllAsync();
    }
}
