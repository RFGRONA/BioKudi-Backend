using Biokudi_Backend.Application.DTOs.Request;
using Biokudi_Backend.Application.DTOs.Response;
using Biokudi_Backend.Application.Interfaces;
using Biokudi_Backend.Domain.Interfaces;

namespace Biokudi_Backend.Application.Services
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository;
        public PersonService(IPersonRepository _personRepository)
        {
            this._personRepository = _personRepository;
        }
        public Task<LoginResponseDto> LoginPerson(LoginRequestDto loginDto)
        {
            throw new NotImplementedException();
        }

        public Task<RegisterRequestDto> RegisterPerson(RegisterRequestDto registerDto)
        {
            throw new NotImplementedException();
        }
    }
}
