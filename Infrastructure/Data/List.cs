using System;
using System.Collections.Generic;

namespace Biokudi_Backend.Infrastructure.Data;

public partial class List
{
    public int IdList { get; set; }

    public int? PersonId { get; set; }

    public string? NameList { get; set; }

    public virtual Person? Person { get; set; }

    public virtual ICollection<Place> Places { get; set; } = new List<Place>();
}
