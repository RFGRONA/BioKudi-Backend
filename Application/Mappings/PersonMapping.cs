using Biokudi_Backend.Application.DTOs.Request;
using Biokudi_Backend.Application.DTOs.Response;
using Biokudi_Backend.Domain.Entities;

namespace Biokudi_Backend.Application.Mappings
{
    public class PersonMapping
    {
        private static readonly Random Random = new Random();
        private static readonly string[] DefaultImages = new[]
        {
            "https://i.postimg.cc/qvP3LVqg/429c70dd-fd68-4fa2-86ec-b841eadd013e.jpg",
            "https://i.postimg.cc/q72CDb6w/6f7a34f8-0f6d-46b1-aaa1-834343d344a6.jpg",
            "https://i.postimg.cc/8kZfYrc2/a3c3ce00-a292-4102-a95c-4c468b7dafca.jpg",
            "https://i.postimg.cc/nzZ9y4vJ/bb709903-7e35-4ac1-baf7-3fd75f9c457a.jpg",
            "https://i.postimg.cc/4x7cVNJr/bb759a8d-1ed9-45d5-8d52-acd2a5bef8ec.jpg",
            "https://i.postimg.cc/0QfKPzfJ/f7f1149a-5b7d-4940-807b-482990e764d3.jpg",
            "https://i.postimg.cc/nrSs9NNF/f9c40f50-2a10-4071-a5ad-dc320246f3ed.jpg",
            "https://i.postimg.cc/c1FhsWZq/Profile-PNG-Photo.png"
        };

        public PersonEntity LoginToPersonEntity(LoginRequestDto loginDto)
        {
            return new PersonEntity
            {
                Email = loginDto.Email,
                Password = loginDto.Password
            };
        }

        public LoginResponseDto PersonEntityToLoginDto(PersonEntity personEntity)
        {
            return new LoginResponseDto
            {
                Email = personEntity.Email,
                NameUser = personEntity.NameUser,
                UserId = personEntity.IdUser,
                Role = personEntity.Role.NameRole,
                ProfilePicture = personEntity.Pictures.FirstOrDefault()?.Link
                      ?? DefaultImages[Random.Next(DefaultImages.Length)]
            };
        }

        public PersonEntity RegisterToPersonEntity(RegisterRequestDto registerDto)
        {
            return new PersonEntity
            {
                Email = registerDto.Email,
                Password = registerDto.Password,
                NameUser = registerDto.NameUser,
            };
        }

        public ProfileResponseDto PersonToProfile(PersonEntity person)
        {
            return new ProfileResponseDto
            {
                UserName = person.NameUser,
                Email = person.Email,
                PhoneNumber = person.Telephone ?? "300 000 0000",
                ProfilePicture = person.Pictures.FirstOrDefault()?.Link
                      ?? DefaultImages[Random.Next(DefaultImages.Length)],
                EmailNotification = person.EmailNotification ?? true,
                EmailPost = person.EmailPost ?? true
            };
        }

        public List<PersonListCrudDto> MapToPersonList(IEnumerable<PersonEntity> persons)
        {
            return persons.Select(person => new PersonListCrudDto
            {
                IdUser = person.IdUser,
                UserName = person.NameUser,
                Email = person.Email,
                RoleName = person.Role.NameRole,
                StateName = person.State.NameState
            }).ToList();
        }

        public PersonEntity PersonCrudRequestToEntity(PersonCrudRequestDto person)
        {
            return new PersonEntity
            {
                RoleId = person.RoleId
            };
        }

        public PersonEntity PersonRequestToEntity(PersonRequestDto person)
        {
            return new PersonEntity
            {
                NameUser = person.NameUser,
                Email = person.Email,
                Telephone = person.Telephone,
                EmailNotification = person.EmailNotification,
                EmailPost = person.EmailPost
            };
        }

        public PersonCrudResponseDto EntityToPersonCrudResponse(PersonEntity person)
        {
            return new PersonCrudResponseDto
            {
                NameUser = person.NameUser,
                Email = person.Email,
                RoleId = person.RoleId
            };
        }
    }
}
