using System;
using System.Collections.Generic;

namespace Biokudi_Backend.Infrastructure;

public partial class CatState
{
    public int IdState { get; set; }

    public string NameState { get; set; } = null!;

    public string? TableRelation { get; set; }

    public virtual ICollection<Person> People { get; set; } = new List<Person>();

    public virtual ICollection<Place> Places { get; set; } = new List<Place>();

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
