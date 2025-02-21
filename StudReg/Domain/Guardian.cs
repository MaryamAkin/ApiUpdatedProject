namespace StudReg.Domain
{
    public class Guardian : BaseEntity
    {
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string PhoneNo { get; set; }
        public ICollection<Student> Students { get; set; } = new HashSet<Student>();

    }
}
