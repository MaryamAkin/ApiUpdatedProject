using StudReg.Domain;

namespace StudReg.Dtos
{
    public class RoleDto
    {
        public Guid Id { get; set; } 
        public DateTime DateCreated { get; set; } 
        public bool IsDeleted { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public ICollection<UserDto> Users { get; set; } = new HashSet<UserDto>();
    }
}
