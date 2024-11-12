using System;
using System.Collections.Generic;

namespace Biokudi_Backend.Infrastructure.Data;

public partial class CatDepartment
{
    public int IdDepartment { get; set; }

    public string? NameDepartment { get; set; }

    public virtual ICollection<CatCity> CatCities { get; set; } = new List<CatCity>();
}
