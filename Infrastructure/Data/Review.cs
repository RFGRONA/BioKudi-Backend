using System;
using System.Collections.Generic;

namespace Biokudi_Backend.Infrastructure.Data;

public partial class Review
{
    public int IdReview { get; set; }

    public decimal Rate { get; set; }

    public string? Comment { get; set; }

    public DateTime? DateCreated { get; set; }

    public DateTime? DateModified { get; set; }

    public int PersonId { get; set; }

    public int PlaceId { get; set; }

    public virtual Person Person { get; set; } = null!;

    public virtual ICollection<Picture> Pictures { get; set; } = new List<Picture>();

    public virtual Place Place { get; set; } = null!;
}
