using Biokudi_Backend.Application.DTOs.Request;
using Biokudi_Backend.Application.DTOs.Response;
using Biokudi_Backend.Application.Interfaces;
using Biokudi_Backend.Application.Mappings;
using Biokudi_Backend.Application.Utilities;
using Biokudi_Backend.Domain.Interfaces;
using Biokudi_Backend.Domain.ValueObject;
using Biokudi_Backend.Infrastructure.Repositories;

namespace Biokudi_Backend.Application.Services
{
    public class PersonService(IPersonRepository personRepository, EmailUtility emailUtility, RSAUtility rsaUtility, PersonMapping personMapping) : IPersonService
    {
        private readonly IPersonRepository _personRepository = personRepository;
        private readonly EmailUtility _emailUtility = emailUtility;
        private readonly RSAUtility _rsaUtility = rsaUtility;
        private readonly PersonMapping _personMapping = personMapping;

        public async Task<Result<LoginResponseDto>> GetPersonById(int id)
        {
            var result = await _personRepository.GetById(id);
            if (result.IsSuccess)
                return Result<LoginResponseDto>.Success(_personMapping.PersonEntityToLoginDto(result.Value));
            return Result<LoginResponseDto>.Failure(result.ErrorMessage);
        }

        public async Task<Result<ProfileResponseDto>> GetUserProfile(int id)
        {
            var result = await _personRepository.GetById(id);
            return result.IsSuccess
                ? Result<ProfileResponseDto>.Success(_personMapping.PersonToProfile(result.Value))
                : Result<ProfileResponseDto>.Failure(result.ErrorMessage);
        }

        public async Task<Result<List<PersonListCrudDto>>> GetUsers()
        {
            var result = await _personRepository.GetAll();
            return result.IsSuccess
                ? Result<List<PersonListCrudDto>>.Success(_personMapping.MapToPersonList(result.Value).OrderBy(p => p.IdUser).ToList())
                : Result<List<PersonListCrudDto>>.Failure(result.ErrorMessage);
        }

        public async Task<Result<LoginResponseDto>> LoginPerson(LoginRequestDto loginDto)
        {
            if (!EmailValidatorUtility.ValidateEmailAsync(loginDto.Email).Result)
                return Result<LoginResponseDto>.Failure("Correo inválido");

            var personEntity = _personMapping.LoginToPersonEntity(loginDto);
            var result = await _personRepository.GetAccountByEmail(personEntity.Email);

            if (!result.IsSuccess)
                return Result<LoginResponseDto>.Failure(result.ErrorMessage);

            loginDto.Password = _rsaUtility.DecryptWithPrivateKey(loginDto.Password);
            if (!PasswordUtility.VerifyPassword(loginDto.Password, result.Value.Password))
                return Result<LoginResponseDto>.Failure("Contraseña incorrecta.");

            return Result<LoginResponseDto>.Success(_personMapping.PersonEntityToLoginDto(result.Value));
        }

        public async Task<Result<RegisterRequestDto>> RegisterPerson(RegisterRequestDto registerDto)
        {
            if (!EmailValidatorUtility.ValidateEmailAsync(registerDto.Email).Result)
                return Result<RegisterRequestDto>.Failure("Correo inválido");

            var personEntity = _personMapping.RegisterToPersonEntity(registerDto);
            personEntity.Password = _rsaUtility.DecryptWithPrivateKey(personEntity.Password);
            personEntity.Password = PasswordUtility.HashPassword(personEntity.Password);

            var result = await _personRepository.Create(personEntity);
            if (!result.IsSuccess)
                return Result<RegisterRequestDto>.Failure(result.ErrorMessage);

            _emailUtility.SendEmail(result.Value.Email, "Registro exitoso", _emailUtility.CreateAccountAlert(result.Value.NameUser));
            return Result<RegisterRequestDto>.Success(registerDto);
        }

        public async Task<Result<bool>> DeleteUser(int id)
        {
            var result = await _personRepository.Delete(id);
            return result.IsSuccess ? Result<bool>.Success(true) : Result<bool>.Failure(result.ErrorMessage);
        }

        public async Task<Result<bool>> UpdateCrudUser(int id, PersonCrudRequestDto person)
        {
            var personEntity = _personMapping.PersonCrudRequestToEntity(person);
            personEntity.IdUser = id;
            var result = await _personRepository.Update(personEntity);
            return result.IsSuccess ? Result<bool>.Success(true) : Result<bool>.Failure(result.ErrorMessage);
        }

        public async Task<Result<bool>> UpdateUserProfile(int id, PersonRequestDto person)
        {
            var personEntity = _personMapping.PersonRequestToEntity(person);
            personEntity.IdUser = id;
            var result = await _personRepository.Update(personEntity);
            return result.IsSuccess ? Result<bool>.Success(true) : Result<bool>.Failure(result.ErrorMessage);
        }

        public async Task<Result<PersonCrudResponseDto>> GetCrudPersonById(int id)
        {
            var result = await _personRepository.GetById(id);
            return result.IsSuccess
                ? Result<PersonCrudResponseDto>.Success(_personMapping.EntityToPersonCrudResponse(result.Value))
                : Result<PersonCrudResponseDto>.Failure(result.ErrorMessage);
        }
    }
}
