namespace StudReg.Domain
{
    public class User : BaseEntity
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
        public ICollection<UserRole> UserRoles { get; set; } = new HashSet<UserRole>();
    }
}
