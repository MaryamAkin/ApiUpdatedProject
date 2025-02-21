using StudReg.Context;
using StudReg.Repositories.Interfaces;

namespace StudReg.Repositories.Implementaions
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StudRegContext _context;

        public UnitOfWork(StudRegContext context)
        {
            _context = context;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
