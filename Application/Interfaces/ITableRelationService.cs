using Biokudi_Backend.Application.DTOs;

namespace Biokudi_Backend.Application.Interfaces
{
    public interface ITableRelationService
    {
        Task<List<TableRelationDto>?> GetRelations();
    }
}
