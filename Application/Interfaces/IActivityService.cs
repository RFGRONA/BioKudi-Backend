using Biokudi_Backend.Application.DTOs;

namespace Biokudi_Backend.Application.Interfaces
{
    public interface IActivityService
    {
        Task<List<ActivityDto>?> GetActivities();
        Task<ActivityDto?> GetActivityById(int id);
        Task<bool> CreateActivity(ActivityRequestDto department);
        Task<bool> UpdateActivity(int id, ActivityRequestDto department);
        Task<bool> DeleteActivity(int id);
    }
}
