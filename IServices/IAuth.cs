using WebApiClass.DTO;
using static WebApiClass.DTO.Responses;

namespace WebApiClass.IServices
{
    public interface IAuth
    {
        Task<GeneralResponse> CreateUser(RegisterDTO registerDTO);
        Task<LogInResponse> LogInUser(LogInDTO logInDTO);
    }
}
