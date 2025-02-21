using StudReg.Enums;

namespace StudReg.Domain
{
    public class Profile : BaseEntity
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public string? Image { get; set; }
        public Gender Gender { get; set; }
        public Student? Student { get; set; }
    }
}
