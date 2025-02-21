using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using StudReg.Context;
using StudReg.Domain;
using StudReg.Repositories.Interfaces;

namespace StudReg.Repositories.Implementaions
{
    public class StudentRepository : BaseRepository<Student>, IStudentRepository
    {
        public StudentRepository(StudRegContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Student>> GetAllAsync()
        {
            return await _context.Set<Student>()
                .Include(a => a.Guardian)
                .Include(a => a.Profile)
                .ToListAsync();
        }

        public async Task<Student?> GetAsync(Guid id)
        {
            return await _context.Set<Student>()
                .Include(a => a.Guardian)
                .Include(a => a.Profile)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Student?> GetAsync(Expression<Func<Student, bool>> exp)
        {
            return await _context.Set<Student>()
                .Include(a => a.Guardian)
                .Include(a => a.Profile)
                .FirstOrDefaultAsync(exp);
        }

        public Task<ICollection<Student>> GetSelectedAsync(List<Guid> ids)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<Student>> GetSelectedAsync(Expression<Func<Student, bool>> exp)
        {
            throw new NotImplementedException();
        }
    }
}
