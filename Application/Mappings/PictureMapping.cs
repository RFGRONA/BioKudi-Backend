using Biokudi_Backend.Application.DTOs.Response;
using Biokudi_Backend.Domain.Entities;

namespace Biokudi_Backend.Application.Mappings
{
    public class PictureMapping
    {
        public PictureResponseDto EntitytoResponse(PictureEntity picture)
        {
            return new PictureResponseDto
            {
                IdPicture = picture.IdPicture,
                Name = picture.Name,
                Url = picture.Link,
                TypeName = picture.Type?.NameType 
            };
        }
    }
}
