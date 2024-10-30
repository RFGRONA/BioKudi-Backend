using Biokudi_Backend.Application.DTOs;
using Biokudi_Backend.Application.Interfaces;
using Biokudi_Backend.Application.Mappings;
using Biokudi_Backend.Domain.Interfaces;
using Biokudi_Backend.Domain.ValueObject;

namespace Biokudi_Backend.Application.Services
{
    public class StateService(StateMapping stateMapping, IStateRepository stateRepository) : IStateService
    {
        private readonly StateMapping _stateMapping = stateMapping;
        private readonly IStateRepository _stateRepository = stateRepository;

        public async Task<Result<bool>> CreateState(StateRequestDto state)
        {
            var entity = _stateMapping.RequestToEntity(state);
            var result = await _stateRepository.Create(entity);
            return result.IsSuccess ? Result<bool>.Success(true) : Result<bool>.Failure(result.ErrorMessage);
        }

        public async Task<Result<bool>> DeleteState(int id)
        {
            var result = await _stateRepository.Delete(id);
            return result.IsSuccess ? Result<bool>.Success(true) : Result<bool>.Failure(result.ErrorMessage);
        }

        public async Task<Result<StateDto>> GetStateById(int id)
        {
            var result = await _stateRepository.GetById(id);
            return result.IsSuccess
                ? Result<StateDto>.Success(_stateMapping.EntityToDto(result.Value))
                : Result<StateDto>.Failure(result.ErrorMessage);
        }

        public async Task<Result<List<StateDto>>> GetStates()
        {
            var result = await _stateRepository.GetAll();
            return result.IsSuccess
                ? Result<List<StateDto>>.Success(result.Value.Select(state => _stateMapping.EntityToDto(state)).ToList())
                : Result<List<StateDto>>.Failure(result.ErrorMessage);
        }

        public async Task<Result<bool>> UpdateState(int id, StateRequestDto state)
        {
            var entity = _stateMapping.RequestToEntity(state);
            entity.IdState = id;
            var result = await _stateRepository.Update(entity);
            return result.IsSuccess ? Result<bool>.Success(true) : Result<bool>.Failure(result.ErrorMessage);
        }
    }
}