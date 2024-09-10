using System;
using System.Collections.Generic;

namespace Biokudi_Backend.Infrastructure;

public partial class CatRole
{
    public int IdRole { get; set; }

    public string NameRole { get; set; } = null!;

    public virtual ICollection<Person> People { get; set; } = new List<Person>();
}
