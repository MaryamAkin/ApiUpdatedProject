using StudReg.Dtos;

namespace StudReg.Services.Interfaces
{
    public interface IGuardianService
    {
        Task<BaseResponse<Guid>> RegisterGuardian(CreateGuardianRequestModel model);
        Task<BaseResponse<GuardianDto>> GetGuardian(Guid id);
        Task<BaseResponse<ICollection<GuardianDto>>> GetAll();
    }
}
