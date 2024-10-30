namespace Biokudi_Backend.Infrastructure;

public partial class Ticket
{
    public int IdTicket { get; set; }

    public string Affair { get; set; } = null!;

    public DateTime DateCreated { get; set; }

    public DateTime? DateAnswered { get; set; }

    public string? AnsweredBy { get; set; }

    public bool? ScalpAdmin { get; set; }

    public int PersonId { get; set; }

    public int? StateId { get; set; }

    public int? TypeId { get; set; }

    public virtual Person Person { get; set; } = null!;

    public virtual ICollection<Picture> Pictures { get; set; } = new List<Picture>();

    public virtual CatState? State { get; set; }

    public virtual CatType? Type { get; set; }
}