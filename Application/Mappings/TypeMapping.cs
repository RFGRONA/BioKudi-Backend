using Biokudi_Backend.Application.DTOs;
using Biokudi_Backend.Domain.Entities;

namespace Biokudi_Backend.Application.Mappings
{
    public class TypeMapping
    {
        public CatTypeEntity DtoToEntity(TypeDto type)
        {
            return new CatTypeEntity
            {
                IdType = type.IdType,
                NameType = type.NameType,
                TableRelation = type.TableRelation
            };
        }

        public CatTypeEntity RequestToEntity(TypeRequestDto state)
        {
            return new CatTypeEntity
            {
                NameType = state.NameType,
                TableRelation = state.TableRelation
            };
        }

        public TypeDto EntityToDto(CatTypeEntity state)
        {
            return new TypeDto
            {
                IdType = state.IdType,
                NameType = state.NameType,
                TableRelation = state.TableRelation
            };
        }
    }
}
