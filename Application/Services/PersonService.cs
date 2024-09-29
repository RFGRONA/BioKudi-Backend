using Biokudi_Backend.Application.DTOs.Request;
using Biokudi_Backend.Application.DTOs.Response;
using Biokudi_Backend.Application.Interfaces;
using Biokudi_Backend.Application.Mappings;
using Biokudi_Backend.Application.Utilities;
using Biokudi_Backend.Domain.Interfaces;

namespace Biokudi_Backend.Application.Services
{
    public class PersonService(IPersonRepository personRepository, EmailUtility emailUtility, RSAUtility _rsaUtility) : IPersonService
    {
        private readonly IPersonRepository _personRepository = personRepository;
        private readonly EmailUtility _emailUtility = emailUtility;
        private readonly RSAUtility _rsaUtility = _rsaUtility;

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
                if (!EmailValidatorUtility.ValidateEmailAsync(loginDto.Email).Result)
                    throw new KeyNotFoundException("Correo invalido");
                var personEntity = PersonMapping.LoginToPersonEntity(loginDto);
                var result = await _personRepository.GetAccountByEmail(personEntity.Email);
                loginDto.Password = _rsaUtility.DecryptWithPrivateKey(loginDto.Password);
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
                if (!EmailValidatorUtility.ValidateEmailAsync(registerDto.Email).Result)
                    throw new KeyNotFoundException("Correo invalido");
                var personEntity = PersonMapping.RegisterToPersonEntity(registerDto);
                personEntity.Password = _rsaUtility.DecryptWithPrivateKey(personEntity.Password);
                personEntity.Password = PasswordUtility.HashPassword(personEntity.Password);
                var result = await _personRepository.Create(personEntity);
                _emailUtility.SendEmail(result.Email, "Registro exitoso", _emailUtility.CreateAccountAlert(result.NameUser));
                return registerDto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
