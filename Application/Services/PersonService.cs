using Biokudi_Backend.Application.DTOs.Request;
using Biokudi_Backend.Application.DTOs.Response;
using Biokudi_Backend.Application.Interfaces;
using Biokudi_Backend.Application.Mappings;
using Biokudi_Backend.Application.Utilities;
using Biokudi_Backend.Domain.Interfaces;

namespace Biokudi_Backend.Application.Services
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository;

        public PersonService(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public async Task<LoginResponseDto>? GetPersonById(int id)
        {
            try
            {
                var result = await _personRepository.GetById(id);
                return PersonMapping.PersonEntityToLoginDto(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<LoginResponseDto?> LoginPerson(LoginRequestDto loginDto)
        {
            try
            {
                var personEntity = PersonMapping.LoginToPersonEntity(loginDto);
                var result = await _personRepository.GetAccountByEmail(personEntity.Email);
                if (!PasswordUtility.VerifyPassword(loginDto.Password, result.Password))
                    throw new KeyNotFoundException($"Contraseña incorrecta");
                return PersonMapping.PersonEntityToLoginDto(result);
            } 
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<RegisterRequestDto?> RegisterPerson(RegisterRequestDto registerDto)
        {
            try
            {
                var personEntity = PersonMapping.RegisterToPersonEntity(registerDto);
                personEntity.Password = PasswordUtility.HashPassword(registerDto.Password);
                var result = await _personRepository.Create(personEntity);
                return registerDto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
