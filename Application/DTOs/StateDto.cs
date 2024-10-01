namespace Biokudi_Backend.Application.DTOs
{
    public class StateDto
    {
        public int IdState { get; set; }

        public string NameState { get; set; } = null!;

        public string? TableRelation { get; set; }
    }
    public class StateRequestDto
    {
        public string NameState { get; set; } = null!;
        public string? TableRelation { get; set; }
    }
}
