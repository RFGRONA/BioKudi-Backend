using Biokudi_Backend.Application.DTOs.Request;
using Biokudi_Backend.Application.DTOs.Response;

namespace Biokudi_Backend.Application.Interfaces
{
    public interface IPersonService
    {
        Task<LoginResponseDto>? LoginPerson(LoginRequestDto loginDto);
        Task<RegisterRequestDto>? RegisterPerson(RegisterRequestDto registerDto);
        Task<LoginResponseDto>? GetPersonById(int id);
    }
}
