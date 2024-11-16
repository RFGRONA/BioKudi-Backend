using System;
using System.Collections.Generic;

namespace Biokudi_Backend.Infrastructure.Data;

public partial class Place
{
    public int IdPlace { get; set; }

    public string NamePlace { get; set; } = null!;

    public decimal Latitude { get; set; }

    public decimal Longitude { get; set; }

    public string Address { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Link { get; set; } = null!;

    public DateTime DateCreated { get; set; }

    public DateTime? DateModified { get; set; }

    public int? CityId { get; set; }

    public int? StateId { get; set; }

    public virtual CatCity? City { get; set; }

    public virtual ICollection<Picture> Pictures { get; set; } = new List<Picture>();

    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    public virtual CatState? State { get; set; }

    public virtual ICollection<CatActivity> Activities { get; set; } = new List<CatActivity>();

    public virtual ICollection<List> Lists { get; set; } = new List<List>();
}
