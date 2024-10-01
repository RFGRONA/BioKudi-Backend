using Biokudi_Backend.Application.DTOs;
using Biokudi_Backend.Domain.Entities;

namespace Biokudi_Backend.Application.Mappings
{
    public class StateMapping
    {
        public CatStateEntity DtoToEntity(StateDto state)
        {
            return new CatStateEntity
            {
                IdState = state.IdState,
                NameState = state.NameState,
                TableRelation = state.TableRelation
            };
        }

        public CatStateEntity RequestToEntity(StateRequestDto state)
        {
            return new CatStateEntity
            {
                NameState = state.NameState,
                TableRelation = state.TableRelation
            };
        }

        public StateDto EntityToDto(CatStateEntity state)
        {
            return new StateDto
            {
                IdState = state.IdState,
                NameState = state.NameState,
                TableRelation = state.TableRelation
            };
        }
    }
}
