using Biokudi_Backend.Application.DTOs.Request;
using Biokudi_Backend.Application.DTOs.Response;
using Biokudi_Backend.Domain.Entities;

namespace Biokudi_Backend.Application.Mappings
{
    public static class PersonMapping
    {
        public static PersonEntity LoginToPersonEntity(LoginRequestDto loginDto)
        {
            return new PersonEntity
            {
                Email = loginDto.Email,
                Password = loginDto.Password
            };
        }

        public static LoginResponseDto PersonEntityToLoginDto(PersonEntity personEntity)
        {
            return new LoginResponseDto
            {
                Email = personEntity.Email,
                NameUser = personEntity.NameUser
            };
        }

        public static PersonEntity RegisterToPersonEntity(RegisterRequestDto registerDto)
        {
            return new PersonEntity
            {
                Email = registerDto.Email,
                Password = registerDto.Password,
                NameUser = registerDto.NameUser,
            };
        }

    }
            
}
