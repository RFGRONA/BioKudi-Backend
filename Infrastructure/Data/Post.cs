using System;
using System.Collections.Generic;

namespace Biokudi_Backend.Infrastructure.Data;

public partial class Post
{
    public int IdPost { get; set; }

    public string Title { get; set; } = null!;

    public string CreatedBy { get; set; } = null!;

    public DateTime DateCreated { get; set; }

    public string Text { get; set; } = null!;

    public int? PlaceId { get; set; }

    public virtual Place? Place { get; set; }

    public virtual ICollection<CatTag> Tags { get; set; } = new List<CatTag>();
}
