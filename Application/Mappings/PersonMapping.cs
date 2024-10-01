using Biokudi_Backend.Application.DTOs.Request;
using Biokudi_Backend.Application.DTOs.Response;
using Biokudi_Backend.Domain.Entities;
using Biokudi_Backend.Infrastructure;

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
                NameUser = personEntity.NameUser,
                UserId = personEntity.IdUser,
                Role = personEntity.Role.NameRole,
                ProfilePicture = personEntity.Pictures.FirstOrDefault()?.Link
                      ?? "https://i.postimg.cc/c1FhsWZq/Profile-PNG-Photo.png"
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

        public static ProfileResponseDto PersonToProfile(PersonEntity person)
        {
            return new ProfileResponseDto
            {
                UserName = person.NameUser,
                Email = person.Email,
                PhoneNumber = person.Telephone ?? "300 000 0000",
                ProfilePicture = person.Pictures.FirstOrDefault()?.Link
                      ?? "https://i.postimg.cc/c1FhsWZq/Profile-PNG-Photo.png",
                EmailNotification = person.EmailNotification ?? true,
                EmailPost = person.EmailPost ?? true
            };
        }
    }
}
