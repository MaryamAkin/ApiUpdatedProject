using Microsoft.EntityFrameworkCore;
using StudReg.Domain;

namespace StudReg.Context
{
    public class StudRegContext : DbContext
    {
        public StudRegContext(DbContextOptions<StudRegContext> opt) : base(opt)
        {}

        public DbSet<Guardian> Guardians { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
    }
}
