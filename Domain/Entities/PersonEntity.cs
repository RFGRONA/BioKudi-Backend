using Biokudi_Backend.Infrastructure;

namespace Biokudi_Backend.Domain.Entities
{
    public class PersonEntity
    {
        public int IdUser { get; set; }
        public string NameUser { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public DateTime DateCreated { get; set; } 
        public DateTime? DateModified { get; set; } 
        public string Password { get; set; }
        public string Hash { get; set; } = "0";
        public int? StateId { get; set; } = 1;
        public int RoleId { get; set; } = 1;
        public bool? AccountDeleted { get; set; } = false;
        public bool? EmailNotification { get; set; } = true;
        public bool? EmailPost { get; set; } = true;
        public bool? EmailList { get; set; } = true;
        public ICollection<List> Lists { get; set; } = [];
        public ICollection<Picture> Pictures { get; set; } = [];
        public ICollection<Review> Reviews { get; set; } = [];
        public CatRole Role { get; set; } = null!;
        public CatState? State { get; set; }
        public ICollection<Ticket> Tickets { get; set; } = [];

        public static DateTime DateNowColombia()
        {
            TimeZoneInfo timeZoneColombia = TimeZoneInfo.FindSystemTimeZoneById("SA Pacific Standard Time");
            return TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneColombia);
        }
    }
}
