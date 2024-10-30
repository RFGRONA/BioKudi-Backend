using Biokudi_Backend.Application.DTOs;
using Biokudi_Backend.Domain.ValueObject;

namespace Biokudi_Backend.Application.Interfaces
{
    public interface IStateService
    {
        Task<Result<List<StateDto>>> GetStates();
        Task<Result<StateDto>> GetStateById(int id);
        Task<Result<bool>> CreateState(StateRequestDto state);
        Task<Result<bool>> UpdateState(int id, StateRequestDto state);
        Task<Result<bool>> DeleteState(int id);
    }
}
