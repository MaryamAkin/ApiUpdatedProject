using System.Linq.Expressions;
using StudReg.Domain;
namespace StudReg.Repositories.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User?> GetAsync(Guid id);
        Task<User?> GetAsync(Expression<Func<User, bool>> exp);
        Task<ICollection<User>> GetSelectedAsync(List<Guid> ids);
        Task<ICollection<User>> GetSelectedAsync(Expression<Func<User, bool>> exp);
        Task<ICollection<User>> GetAllAsync();
    }
}
