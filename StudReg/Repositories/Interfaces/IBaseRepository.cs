using StudReg.Domain;
using System.Linq.Expressions;

namespace StudReg.Repositories.Interfaces
{
    public interface IBaseRepository<T>
    {
        Task CreateAsync(T entity);
        void Update(T entity);
        Task<bool> CheckAsync(Expression<Func<T, bool>> exp);
    }
}
