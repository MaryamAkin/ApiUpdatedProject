using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using StudReg.Domain;

namespace StudReg.Repositories.Interfaces
{
    public interface IRoleRepository
    {
        Task<bool> CheckAsync(Expression<Func<Role, bool>> exp);
        Task CreateAsync(Role role);
        void Update(Role role);
        Task<Role> GetAsync(Guid id);
        Task<Role?> GetAsync(Expression<Func<Role, bool>> exp);
        Task<ICollection<Role>> GetAllAsync();
    }
}
