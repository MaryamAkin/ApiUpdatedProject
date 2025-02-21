using StudReg.Domain;

namespace StudReg.Dtos
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public DateTime DateCreated { get; set; } 
        public bool IsDeleted { get; set; }
        public required string Email { get; set; }
        public ICollection<RoleDto> Roles { get; set; } = new HashSet<RoleDto>();
    }
    public class CreateUserRequestModel
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
    public class LoginRequestModel
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}
