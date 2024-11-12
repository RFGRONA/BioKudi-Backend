using System;
using System.Collections.Generic;

namespace Biokudi_Backend.Infrastructure.Data;

public partial class CatType
{
    public int IdType { get; set; }

    public string? NameType { get; set; }

    public string? TableRelation { get; set; }

    public virtual ICollection<Picture> Pictures { get; set; } = new List<Picture>();

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
