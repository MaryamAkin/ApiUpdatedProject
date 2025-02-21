using StudReg.Dtos;

namespace StudReg.Services.Interfaces
{
    public interface IUserService
    {
        Task<BaseResponse<UserDto>> CreateAsync(CreateUserRequestModel model);
        Task<BaseResponse<UserDto>> GetAsync(Guid id);
        Task<BaseResponse<UserDto>> LoginAsync(LoginRequestModel model);
        Task<BaseResponse<ICollection<UserDto>>> GetAllAsync();

    }
}
