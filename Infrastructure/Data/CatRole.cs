using System;
using System.Collections.Generic;

namespace Biokudi_Backend.Infrastructure.Data;

public partial class CatRole
{
    public int IdRole { get; set; }

    public string NameRole { get; set; } = null!;

    public virtual ICollection<Person> People { get; set; } = new List<Person>();
}
