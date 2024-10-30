using Biokudi_Backend.Application.DTOs;
using Biokudi_Backend.Application.Interfaces;

namespace Biokudi_Backend.Application.Services
{
    public class TableRelationService : ITableRelationService
    {
        public Task<List<TableRelationDto>?> GetRelations()
        {
            return Task.FromResult<List<TableRelationDto>?>(new List<TableRelationDto>
                {
                    new() { IdTableRelation = "PLACE", TableRelation = "PLACE" },
                    new() { IdTableRelation = "PERSON", TableRelation = "PERSON" },
                    new() { IdTableRelation = "PICTURE", TableRelation = "PICTURE" },
                    new() { IdTableRelation = "TICKET", TableRelation = "TICKET" }
                });
        }
    }
}
