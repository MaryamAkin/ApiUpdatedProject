namespace StudReg.Domain
{
    public class Student : BaseEntity
    {
        public required string AdmissionNumber { get; set; } 
        public required string Class { get; set; }
        public required string Email { get; set; }
        public Guid GuardianId { get; set; }
        public Guardian? Guardian { get; set; }
        public Guid ProfileId { get; set; }
        public Profile? Profile { get; set; }
    }
}
