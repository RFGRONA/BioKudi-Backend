using System;
using System.Collections.Generic;

namespace Biokudi_Backend.Infrastructure.Data;

public partial class Picture
{
    public int IdPicture { get; set; }

    public string Name { get; set; } = null!;

    public string Link { get; set; } = null!;

    public DateTime? DateCreated { get; set; }

    public int? TypeId { get; set; }

    public int? PlaceId { get; set; }

    public int? PersonId { get; set; }

    public int? TicketId { get; set; }

    public int? ReviewId { get; set; }

    public virtual Person? Person { get; set; }

    public virtual Place? Place { get; set; }

    public virtual Review? Review { get; set; }

    public virtual Ticket? Ticket { get; set; }

    public virtual CatType? Type { get; set; }
}
