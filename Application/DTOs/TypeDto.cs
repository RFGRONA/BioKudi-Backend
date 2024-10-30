namespace Biokudi_Backend.Application.DTOs
{
    public class TypeDto
    {
        public int IdType { get; set; }

        public string NameType { get; set; } = null!;

        public string? TableRelation { get; set; }
    }

    public class TypeRequestDto
    {
        public string NameType { get; set; } = null!;
        public string? TableRelation { get; set; }
    }

    public class IdTypeDto
    {
        public int IdType { get; set; }
    }
}
