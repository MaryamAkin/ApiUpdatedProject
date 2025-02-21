using StudReg.Dtos;
using StudReg.Domain;
using StudReg.Repositories.Interfaces;
using StudReg.Services.Interfaces;
using StudReg.Repositories.Implementaions;
using Mapster;
using StudReg.Enums;

namespace StudReg.Services.Implementations
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IUserRepository _userRepository;
        private readonly IProfileRepository _profileRepository;
        private readonly IGuardianRepository _guardianRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUser _currentUser;
        public StudentService(IStudentRepository studentRepository, IUserRepository userRepository, IProfileRepository profileRepository, IUnitOfWork unitOfWork, IGuardianRepository guardianRepository, ICurrentUser currentUser)
        {
            _studentRepository = studentRepository;
            _userRepository = userRepository;
            _profileRepository = profileRepository;
            _unitOfWork = unitOfWork;
            _guardianRepository = guardianRepository;
            _currentUser = currentUser;
        }
        public async Task<BaseResponse<StudentDto>> CreateAsync(CreateStudentRequestModel model)
        {
            var exist = await _studentRepository.CheckAsync(a => a.Email == model.Email);
            if(exist)
            {
                return new BaseResponse<StudentDto>
                {
                    Message = $"{model.Email} already exist",
                    Status = false,
                    Data = null
                };
            }
            var user = new User
            {
                Email = model.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(model.Password)
            };
            
            var profile = new Profile
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Gender = model.Gender,
                
            };
            var currentUser = _currentUser.GetCurrentUser();
            var guardian = await _guardianRepository.GetAsync(a => a.Email == currentUser);
            if(guardian is null)
            {
                throw new Exception();
            }
            var student = new Student
            {
                Email = model.Email,
                Class = model.Class,
                AdmissionNumber = $"MA{new Random().Next(1000, 1500)}",
                ProfileId = profile.Id,
                Profile = profile,
                GuardianId = guardian.Id,
                Guardian = guardian

            };
            await _profileRepository.CreateAsync(profile);
            await _studentRepository.CreateAsync(student);
            await _userRepository.CreateAsync(user);
            await _unitOfWork.SaveChangesAsync();
            return new BaseResponse<StudentDto>
            {
                Message = "Student created successfully",
                Status = true,
                //Data = student.Adapt<StudentDto>()
                Data = new StudentDto
                {
                    Email = student.Email,
                    AdmissionNumber = student.AdmissionNumber,
                    Class = student.Class,
                    DateCreated = student.DateCreated,
                    FullName = student.Profile.FirstName + " " + student.Profile.LastName,
                    GuardianPhoneNumber = student.Guardian.PhoneNo,
                    Id = student.Id,
                    Gender = student.Profile.Gender,
                    GuardianId = student.GuardianId,
                    ProfileId = student.ProfileId,
                    
                }
            };
        }

        public async Task<BaseResponse<ICollection<StudentDto>>> GetAllAsync()
        {
            var students = await _studentRepository.GetAllAsync();
            //var listOfStudent = students.Select(a => new StudentDto
            //{
            //    Id = a.Id,
            //    Email = a.Email,
            //    AdmissionNumber =a.AdmissionNumber,
            //    Class = a.Class,
            //    DateCreated = a.DateCreated,
            //    FullName = "",
            //    GuardianPhoneNumber = "",
            //});
            var listOfStudent = students.Select(a => a.Adapt<StudentDto>());
            return new BaseResponse<ICollection<StudentDto>>
            {
                Message = "students retrieved",
                Status = true,
                Data = listOfStudent.ToList()
            };
        }

        public async Task<BaseResponse<StudentDto>> GetAsync(Guid id)
        {
            var student = await _studentRepository.GetAsync(a => a.Id == id);
            if (student is null)
            {
                return new BaseResponse<StudentDto>
                {
                    Message = $"student with {id} not found",
                    Status = false,
                    Data = null
                };
            }
            return new BaseResponse<StudentDto>
            {
                Message = "student found",
                Status = true,
                Data = new StudentDto
                {
                    Id = student.Id,
                    Email = student.Email,
                    AdmissionNumber = student.AdmissionNumber,
                    Class = student.Class,
                    DateCreated = student.DateCreated,
                    FullName = $"{student.Profile.FirstName} {student.Profile.LastName}",
                    GuardianPhoneNumber = student.Guardian.PhoneNo,
                    Gender = student.Profile.Gender,
                     GuardianId = student.GuardianId,
                     ProfileId = student.ProfileId
                }
            };
        }
    }
}
