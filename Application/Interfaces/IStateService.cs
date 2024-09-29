using Biokudi_Backend.Application.DTOs;

namespace Biokudi_Backend.Application.Interfaces
{
    public interface IStateService
    {
        Task<List<StateDto>?> GetStates();
        Task<StateDto?> GetStateById(int id);
        Task<bool> CreateState(StateRequestDto department);
        Task<bool> UpdateState(int id, StateRequestDto department);
        Task<bool> DeleteState(int id);
    }
}
