using Biokudi_Backend.Infrastructure;

namespace Biokudi_Backend.Domain.Entities
{
    public class PersonEntity
    {
        public int IdUser { get; set; }
        public string NameUser { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        public DateTime? DateModified { get; set; } = DateTime.UtcNow;
        public string Password { get; set; }
        public string Hash { get; set; }
        public int StateId { get; set; }
        public CatState State { get; set; }
        public int RoleId { get; set; }
        public CatRole Role { get; set; }
        public bool AccountDeleted { get; set; } = false;
        public bool EmailNotification { get; set; } = true;
        public bool EmailPost { get; set; } = true;
        public bool EmailList { get; set; } = true;
    }
}
