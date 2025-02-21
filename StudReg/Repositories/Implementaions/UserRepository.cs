using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using StudReg.Context;
using StudReg.Domain;
using StudReg.Repositories.Interfaces;

namespace StudReg.Repositories.Implementaions
{
    public class UserRepository :  BaseRepository<User>, IUserRepository
    {
        public UserRepository(StudRegContext context)
        {
            _context = context;
        }

        public async Task<ICollection<User>> GetAllAsync()
        {
            return await _context.Set<User>()
                .Include(a => a.UserRoles)
                .ThenInclude(a => a.Role)
                .ToListAsync();
        }

        public async Task<User?> GetAsync(Guid id)
        {
            return await _context.Set<User>()
                .Include(a => a.UserRoles)
                .ThenInclude(a => a.Role)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<User?> GetAsync(Expression<Func<User, bool>> exp)
        {
            return await _context.Set<User>()
                .Include(a => a.UserRoles)
                .ThenInclude(a => a.Role)
                .FirstOrDefaultAsync(exp);
        }

        public async Task<ICollection<User>> GetSelectedAsync(List<Guid> ids)
        {
            return await _context.Set<User>()
                .Include(a => a.UserRoles)
                .ThenInclude(a => a.Role)
                .Where(a => ids.Contains(a.Id))
                .ToListAsync();
        }

        public async Task<ICollection<User>> GetSelectedAsync(Expression<Func<User, bool>> exp)
        {
            return await _context.Set<User>()
                .Include(a => a.UserRoles)
                .ThenInclude(a => a.Role)
                .Where(exp)
                .ToListAsync();
        }
    }
}
