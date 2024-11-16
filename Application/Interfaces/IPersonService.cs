using Biokudi_Backend.Application.DTOs.Request;
using Biokudi_Backend.Application.DTOs.Response;
using Biokudi_Backend.Domain.ValueObject;

namespace Biokudi_Backend.Application.Interfaces
{
    public interface IPersonService
    {
        Task<Result<LoginResponseDto>> LoginPerson(LoginRequestDto loginDto);
        Task<Result<RegisterRequestDto>> RegisterPerson(RegisterRequestDto registerDto);
        Task<Result<LoginResponseDto>> GetPersonById(int id);
        Task<Result<PersonCrudResponseDto>> GetCrudPersonById(int id);
        Task<Result<ProfileResponseDto>> GetUserProfile(int id);
        Task<Result<List<PersonListCrudDto>>> GetUsers();
        Task<Result<bool>> UpdateCrudUser(int id, PersonCrudRequestDto person);
        Task<Result<bool>> UpdateUserProfile(int id, PersonRequestDto person);
        Task<Result<bool>> DeleteUser(int id);
        Task<Result<bool>> GeneratePasswordResetToken(string email);
        Task<Result<bool>> VerifyAndResetPassword(ResetPasswordRequestDto request);
        Task<Result<bool>> UpdatePassword(int userId, UpdatePasswordRequestDto request);
    }
}
