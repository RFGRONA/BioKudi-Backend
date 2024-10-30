using System;
using System.Collections.Generic;

namespace Biokudi_Backend.Infrastructure;

public partial class CatTag
{
    public int IdTag { get; set; }

    public string NameTag { get; set; } = null!;

    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();
}
