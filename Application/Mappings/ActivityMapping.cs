using Biokudi_Backend.Application.DTOs;
using Biokudi_Backend.Domain.Entities;

namespace Biokudi_Backend.Application.Mappings
{
    public class ActivityMapping
    {
        public CatActivityEntity DtoToEntity(ActivityDto activity)
        {
            return new CatActivityEntity
            {
                IdActivity = activity.IdActivity,
                NameActivity = activity.NameActivity,
                UrlIcon = activity.UrlIcon 
            };
        }

        public CatActivityEntity RequestToEntity(ActivityRequestDto activity)
        {
            return new CatActivityEntity
            {
                NameActivity = activity.NameActivity,
                UrlIcon = activity.UrlIcon
            };
        }

        public ActivityDto EntityToDto(CatActivityEntity activity)
        {
            return new ActivityDto
            {
                IdActivity = activity.IdActivity,
                NameActivity = activity.NameActivity,
                UrlIcon = activity.UrlIcon 
            };
        }
    }
}
