using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StudReg.Context;
using StudReg.Domain;
using StudReg.Repositories.Interfaces;

namespace StudReg.Repositories.Implementaions
{
    public class ProfileRepository : IProfileRepository
    {
        private StudRegContext _context;
        public ProfileRepository(StudRegContext context)
        {
            _context = context;
        }
        public async Task<bool> CheckAsync(Expression<Func<Profile, bool>> exp)
        {
            return await _context.Profiles.AnyAsync(exp);
        }

        public async Task CreateAsync(Profile profile)
        {
            await _context.Profiles.AddAsync(profile);
            _context.SaveChanges();
        }

        public async Task<ICollection<Profile>> GetAllAsync()
        {
            return await _context.Profiles.ToListAsync();
        }

        public async Task<Profile?> GetAsync(Guid id)
        {
            return await _context.Profiles.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Profile?> GetAsync(Expression<Func<Profile, bool>> exp)
        {
            return await _context.Profiles.FirstOrDefaultAsync(exp);
        }

        public void Update(Profile profile)
        {
            _context.Profiles.Update(profile);
        }
    }
}
