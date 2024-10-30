using Biokudi_Backend.Application.DTOs;
using Biokudi_Backend.Application.Interfaces;
using Biokudi_Backend.Application.Mappings;
using Biokudi_Backend.Domain.Interfaces;
using Biokudi_Backend.Domain.ValueObject;

namespace Biokudi_Backend.Application.Services
{
    public class ActivityService(ActivityMapping activityMapping, IActivityRepository activityRepository) : IActivityService
    {
        private readonly ActivityMapping _activityMapping = activityMapping;
        private readonly IActivityRepository _activityRepository = activityRepository;

        public async Task<Result<bool>> CreateActivity(ActivityRequestDto activity)
        {
            var result = await _activityRepository.Create(_activityMapping.RequestToEntity(activity));
            return result.IsSuccess
                ? Result<bool>.Success(true)
                : Result<bool>.Failure(result.ErrorMessage);
        }

        public async Task<Result<bool>> DeleteActivity(int id)
        {
            var result = await _activityRepository.Delete(id);
            return result;
        }

        public async Task<Result<List<ActivityDto>>> GetActivities()
        {
            var result = await _activityRepository.GetAll();
            return result.IsSuccess
                ? Result<List<ActivityDto>>.Success(result.Value.Select(a => _activityMapping.EntityToDto(a)).ToList())
                : Result<List<ActivityDto>>.Failure(result.ErrorMessage);
        }

        public async Task<Result<ActivityDto>> GetActivityById(int id)
        {
            var result = await _activityRepository.GetById(id);
            return result.IsSuccess
                ? Result<ActivityDto>.Success(_activityMapping.EntityToDto(result.Value))
                : Result<ActivityDto>.Failure(result.ErrorMessage);
        }

        public async Task<Result<bool>> UpdateActivity(int id, ActivityRequestDto activity)
        {
            var entity = _activityMapping.RequestToEntity(activity);
            entity.IdActivity = id;
            var result = await _activityRepository.Update(entity);
            return result;
        }
    }
}
