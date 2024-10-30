using Biokudi_Backend.Application.DTOs;
using Biokudi_Backend.Domain.ValueObject;

namespace Biokudi_Backend.Application.Interfaces
{
    public interface IActivityService
    {
        Task<Result<List<ActivityDto>>> GetActivities();
        Task<Result<ActivityDto>> GetActivityById(int id);
        Task<Result<bool>> CreateActivity(ActivityRequestDto activity);
        Task<Result<bool>> UpdateActivity(int id, ActivityRequestDto activity);
        Task<Result<bool>> DeleteActivity(int id);
    }
}
