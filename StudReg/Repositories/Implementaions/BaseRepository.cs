using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using StudReg.Context;
using StudReg.Domain;
using StudReg.Repositories.Interfaces;

namespace StudReg.Repositories.Implementaions
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected StudRegContext _context;

        public async Task<bool> CheckAsync(Expression<Func<T, bool>> exp)
        {
            return await _context.Set<T>().AnyAsync(exp);
        }

        public async Task CreateAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }
    }
}
