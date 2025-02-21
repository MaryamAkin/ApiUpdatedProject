using StudReg.Domain;

namespace StudReg.Dtos
{
    public class GuardianDto
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string PhoneNo { get; set; }
        public ICollection<StudentDto> Students { get; set; } = new HashSet<StudentDto>();
    }

    public class CreateGuardianRequestModel
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string Name { get; set; }
        public required string PhoneNo { get; set; }
    }
}
