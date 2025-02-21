using StudReg.Domain;
using StudReg.Dtos;
using StudReg.Repositories.Interfaces;
using StudReg.Services.Interfaces;

namespace StudReg.Services.Implementations
{
    public class GuardianService : IGuardianService
    {
        private readonly IUserRepository _userRepository;
        private readonly IGuardianRepository _guardianRepository;
        private readonly IUnitOfWork _unitOfWork;

        public GuardianService(IUserRepository userRepository, IGuardianRepository guardianRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _guardianRepository = guardianRepository;
            _unitOfWork = unitOfWork;
        }

        public Task<BaseResponse<ICollection<GuardianDto>>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<BaseResponse<GuardianDto>> GetGuardian(Guid id)
        {
            var guardian = await _guardianRepository.GetAsync(id);
            if(guardian is null)
            {

            }
            return new BaseResponse<GuardianDto>
            {
                Message = "retrieved",
                Status = true,
                Data = new GuardianDto
                {
                    Id = guardian.Id,
                    Email = guardian.Email,
                    Name = guardian.Name,
                    PhoneNo = guardian.PhoneNo,
                    Students = guardian.Students.Select(student => new StudentDto
                    {
                        Id = student.Id,
                        FullName = student.Profile.FirstName + " " + student.Profile.LastName,
                        AdmissionNumber = student.AdmissionNumber,
                        Class = student.Class,
                        Email = student.Email,
                        GuardianPhoneNumber = guardian.PhoneNo,

                    }).ToList()
                }
            };
        }

        public async Task<BaseResponse<Guid>> RegisterGuardian(CreateGuardianRequestModel model)
        {
            var exists = await _userRepository.CheckAsync(a => a.Email == model.Email);
            if(exists)
            {
                return new BaseResponse<Guid>
                {
                    Status = false,
                    Message = "already exist",
                };
            }

            var user = new User
            {
                Email = model.Email,
                Password = model.Password
            };
            await _userRepository.CreateAsync(user);

            var guardian = new Guardian
            {
                Email = model.Email,
                Name = model.Name,
                PhoneNo = model.PhoneNo,

            };

            await _guardianRepository.CreateAsync(guardian);
            await _unitOfWork.SaveChangesAsync();

            return new BaseResponse<Guid>
            {
                Message = "created successfully",
                Status = true,
                Data = guardian.Id
            };

        }
    }
}
