using StudReg.Domain;
using StudReg.Dtos;
using StudReg.Repositories.Interfaces;
using StudReg.Services.Interfaces;

namespace StudReg.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<BaseResponse<UserDto>> CreateAsync(CreateUserRequestModel model)
        {
            var exists = await _userRepository.CheckAsync(a => a.Email == model.Email);
            if(exists)
            {
                return new BaseResponse<UserDto>
                {
                    Message = $"{model.Email} already exist",
                    Status = false
                };
            }
            var user = new User
            {
                Email = model.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(model.Password)
            };

            await _userRepository.CreateAsync(user);
            return new BaseResponse<UserDto>
            {
                Message = "created successfully",
                Status = true,
                Data = new UserDto
                {
                    Id = user.Id,
                    Email = user.Email,
                    DateCreated = user.DateCreated,

                }
            };
        }
        public async Task<BaseResponse<ICollection<UserDto>>> GetAllAsync()
        {
            var users = await _userRepository.GetAllAsync();
            var listOfUser = users.Select(a => new UserDto
            {
                Id = a.Id,
                Email = a.Email,
                DateCreated = a.DateCreated,
            });
            return new BaseResponse<ICollection<UserDto>>
            {
                Message = "users retrieved",
                Status = true,
                Data = listOfUser.ToList()
            };
        }

        public async Task<BaseResponse<UserDto>> GetAsync(Guid id)
        {
            var user = await _userRepository.GetAsync(a => a.Id == id);
            if(user is null)
            {
                return new BaseResponse<UserDto>
                {
                    Message = $"user with {id} not found",
                    Status = false,
                    Data = null
                };
            }
            return new BaseResponse<UserDto>
            {
                Message = "user found",
                Status = true,
                Data = new UserDto
                {
                    Id = user.Id,
                    Email = user.Email,
                    DateCreated = user.DateCreated
                }
            };
        }

        public BaseResponse<UserDto> LoginAsync(LoginRequestModel model)
        {
            return new BaseResponse<UserDto>
            {
                Data = new UserDto{
                    Email = "test-user@email.com",
                    Id = Guid.NewGuid(),
                    Roles = [new RoleDto{Name = "Admin"}, new RoleDto{Name = "Manager"}]
                },
                Status = true
            };
        }
    }
}
