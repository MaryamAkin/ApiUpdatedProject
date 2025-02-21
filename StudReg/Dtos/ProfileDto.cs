using StudReg.Domain;
using StudReg.Enums;

namespace StudReg.Dtos
{
    public class ProfileDto
    {
        public Guid Id { get; set; } 
        public DateTime DateCreated { get; set; }
        public bool IsDeleted { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public string? Image { get; set; }
        public Gender Gender { get; set; }
        public Student? Student { get; set; }
    }
}
