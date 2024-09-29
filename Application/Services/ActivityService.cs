using Biokudi_Backend.Application.DTOs;
using Biokudi_Backend.Application.Interfaces;
using Biokudi_Backend.Application.Mappings;
using Biokudi_Backend.Domain.Interfaces;

namespace Biokudi_Backend.Application.Services
{
    public class ActivityService(ActivityMapping activityMapping, IActivityRepository activityRepository) : IActivityService
    {
        private readonly ActivityMapping _activityMapping = activityMapping;
        private readonly IActivityRepository _activityRepository = activityRepository;

        public async Task<bool> CreateActivity(ActivityRequestDto activity)
        {
            try
            {
                var result = await _activityRepository.Create(_activityMapping.RequestToEntity(activity));
                return result != null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteActivity(int id)
        {
            try
            {
                var result = await _activityRepository.Delete(id);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<ActivityDto>?> GetActivities()
        {
            try
            {
                var result = await _activityRepository.GetAll();
                return result?.Select(a => _activityMapping.EntityToDto(a)).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ActivityDto?> GetActivityById(int id)
        {
            try
            {
                var result = await _activityRepository.GetById(id);
                return _activityMapping.EntityToDto(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> UpdateActivity(int id, ActivityRequestDto activity)
        {
            try
            {
                var entity = _activityMapping.RequestToEntity(activity);
                entity.IdActivity = id;
                var result = await _activityRepository.Update(entity);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
