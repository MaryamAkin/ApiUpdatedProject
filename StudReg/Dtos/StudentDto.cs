using Microsoft.AspNetCore.Mvc;
using StudReg.Domain;
using StudReg.Enums;

namespace StudReg.Dtos
{
    public class StudentDto
    {
        public Guid Id { get; set; }
        public DateTime DateCreated { get; set; }
        public bool IsDeleted { get; set; }
        public required string Email { get; set; }
        public required string AdmissionNumber { get; set; }
        public required string Class { get; set; }
        public Guid GuardianId { get; set; }
        public required string GuardianPhoneNumber { get; set; }
        public Guid ProfileId { get; set; }
        public required string FullName { get; set; }
        public Gender Gender { get; set; }
    }
    public class CreateStudentRequestModel
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string Class { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public Gender Gender { get; set; }
    }
}
