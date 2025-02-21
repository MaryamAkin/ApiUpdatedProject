using StudReg.Dtos;

namespace StudReg.Services.Interfaces
{
    public interface IStudentService
    {
        Task<BaseResponse<StudentDto>> CreateAsync(CreateStudentRequestModel model);
        Task<BaseResponse<StudentDto>> GetAsync(Guid id);
        Task<BaseResponse<ICollection<StudentDto>>> GetAllAsync();
    }
}
