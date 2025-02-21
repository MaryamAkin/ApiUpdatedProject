using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using StudReg.Context;
using StudReg.Domain;
using StudReg.Repositories.Interfaces;

namespace StudReg.Repositories.Implementaions
{
    public class RoleRepository : IRoleRepository
    {
        private readonly StudRegContext _context;
        public RoleRepository(StudRegContext context)
        {
            _context = context;
        }
        public async Task<bool> CheckAsync(Expression<Func<Role, bool>> exp)
        {
            return await _context.Roles.AnyAsync(exp);
        }

        public async Task CreateAsync(Role role)
        {
            await _context.Roles.AddAsync(role);
        }

        public async Task<ICollection<Role>> GetAllAsync()
        {
            return await _context.Roles.ToListAsync();
        }

        public async Task<Role> GetAsync(Guid id)
        {
            return await _context.Roles.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Role?> GetAsync(Expression<Func<Role, bool>> exp)
        {
            return await _context.Roles.FirstOrDefaultAsync(exp);
        }

        public void Update(Role role)
        {
            _context.Roles.Update(role);
        }
    }
}
