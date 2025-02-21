using StudReg.Domain;
using System.Linq.Expressions;

namespace StudReg.Repositories.Interfaces
{
    public interface IProfileRepository :IBaseRepository<Profile>
    {
        Task<Profile?> GetAsync(Guid id);
        Task<Profile?> GetAsync(Expression<Func<Profile, bool>> exp);
        Task<ICollection<Profile>> GetAllAsync();
    }
}
