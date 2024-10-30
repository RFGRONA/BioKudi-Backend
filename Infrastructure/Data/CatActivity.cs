using System;
using System.Collections.Generic;

namespace Biokudi_Backend.Infrastructure;

public partial class CatActivity
{
    public int IdActivity { get; set; }

    public string NameActivity { get; set; } = null!;

    public string? UrlIcon { get; set; }

    public virtual ICollection<Place> Places { get; set; } = new List<Place>();
}
