using Biokudi_Backend.Application.DTOs;
using Biokudi_Backend.Application.Interfaces;
using Biokudi_Backend.Application.Mappings;
using Biokudi_Backend.Domain.Interfaces;

namespace Biokudi_Backend.Application.Services
{
    public class StateService(StateMapping stateMapping, IStateRepository stateRepository) : IStateService
    {
        private readonly StateMapping _stateMapping = stateMapping;
        private readonly IStateRepository _stateRepository = stateRepository;
        public async Task<bool> CreateState(StateRequestDto state)
        {
            try
            {
                var result = await _stateRepository.Create(_stateMapping.RequestToEntity(state));
                return result != null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteState(int id)
        {
            try
            {
                var result = await _stateRepository.Delete(id);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<StateDto?> GetStateById(int id)
        {
            try
            {
                var result = await _stateRepository.GetById(id);
                return _stateMapping.EntityToDto(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<StateDto>?> GetStates()
        {
            try
            {
                var result = await _stateRepository.GetAll();
                return result?.Select(state => _stateMapping.EntityToDto(state)).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> UpdateState(int id, StateRequestDto state)
        {
            try
            {
                var entity = _stateMapping.RequestToEntity(state);
                entity.IdState = id;
                var result = await _stateRepository.Update(entity);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
