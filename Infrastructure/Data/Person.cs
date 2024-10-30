using System;
using System.Collections.Generic;

namespace Biokudi_Backend.Infrastructure;

public partial class Person
{
    public int IdUser { get; set; }

    public string NameUser { get; set; } = null!;

    public string? Telephone { get; set; }

    public string Email { get; set; } = null!;

    public DateTime DateCreated { get; set; }

    public DateTime? DateModified { get; set; }

    public string Password { get; set; } = null!;

    public string Hash { get; set; } = null!;

    public int? StateId { get; set; }

    public int RoleId { get; set; }

    public bool? AccountDeleted { get; set; }

    public bool? EmailNotification { get; set; }

    public bool? EmailPost { get; set; }

    public bool? EmailList { get; set; }

    public virtual ICollection<List> Lists { get; set; } = new List<List>();

    public virtual ICollection<Picture> Pictures { get; set; } = new List<Picture>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    public virtual CatRole Role { get; set; } = null!;

    public virtual CatState? State { get; set; }

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
