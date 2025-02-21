using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using StudReg.Context;
using StudReg.Domain;
using StudReg.Repositories.Interfaces;

namespace StudReg.Repositories.Implementaions
{
    public class GuardianRepository : BaseRepository<Guardian>, IGuardianRepository
    {
        public GuardianRepository(StudRegContext context)
        {
            _context = context;
        }
        public Task<ICollection<Guardian>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Guardian?> GetAsync(Guid id)
        {
            return await _context.Set<Guardian>()
                .Include(a => a.Students)
                .ThenInclude(a => a.Profile)
                .FirstOrDefaultAsync(a => a.Id == id && !a.IsDeleted);
        }

        public Task<Guardian?> GetAsync(Expression<Func<Guardian, bool>> exp)
        {
            throw new NotImplementedException();
        }
    }
}
