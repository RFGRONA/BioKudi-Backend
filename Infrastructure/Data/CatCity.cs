using System;
using System.Collections.Generic;

namespace Biokudi_Backend.Infrastructure;

public partial class CatCity
{
    public int IdCity { get; set; }

    public string? NameCity { get; set; }

    public int? DepartmentId { get; set; }

    public virtual CatDepartment? Department { get; set; }

    public virtual ICollection<Place> Places { get; set; } = new List<Place>();
}
