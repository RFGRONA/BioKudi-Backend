using Biokudi_Backend.Application.Interfaces;
using Biokudi_Backend.Domain.Entities;
using Biokudi_Backend.Domain.Interfaces;
using Biokudi_Backend.Domain.ValueObject;
using Biokudi_Backend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Biokudi_Backend.Infrastructure.Repositories
{
    public class ActivityRepository(ICacheService cacheService, ApplicationDbContext context) : IActivityRepository
    {
        private const string CACHE_KEY = "ActivityCache";
        private readonly ICacheService _cacheService = cacheService;
        private readonly ApplicationDbContext _context = context;

        public async Task<Result<CatActivityEntity>> Create(CatActivityEntity entity)
        {
            try
            {
                var existingActivity = await _context.CatActivities
                    .Where(a => a.NameActivity == entity.NameActivity)
                    .FirstOrDefaultAsync();

                if (existingActivity != null)
                    return Result<CatActivityEntity>.Failure("La actividad ya existe.");

                var activity = new CatActivity
                {
                    NameActivity = entity.NameActivity,
                    UrlIcon = entity.UrlIcon
                };

                await _context.CatActivities.AddAsync(activity);
                int rowsAffected = await _context.SaveChangesAsync();

                if (rowsAffected == 0)
                    return Result<CatActivityEntity>.Failure("No se pudo crear la actividad.");

                entity.IdActivity = activity.IdActivity;
                _cacheService.Remove(CACHE_KEY);

                return Result<CatActivityEntity>.Success(entity);
            }
            catch (Exception ex)
            {
                return Result<CatActivityEntity>.Failure($"Error al crear la actividad: {ex.Message}");
            }
        }

        public async Task<Result<bool>> Delete(int id)
        {
            try
            {
                var activity = await _context.CatActivities.FindAsync(id);
                if (activity == null)
                    return Result<bool>.Failure("La actividad no fue encontrada.");

                _context.CatActivities.Remove(activity);
                int rowsAffected = await _context.SaveChangesAsync();
                _cacheService.Remove(CACHE_KEY);

                return rowsAffected > 0
                    ? Result<bool>.Success(true)
                    : Result<bool>.Failure("Error al eliminar la actividad.");
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure($"Error al eliminar la actividad: {ex.Message}");
            }
        }

        public async Task<Result<IEnumerable<CatActivityEntity>>> GetAll()
        {
            try
            {
                var cachedPlaces = _cacheService.GetCollection<CatActivityEntity>(CACHE_KEY);
                if (cachedPlaces != null)
                    return Result<IEnumerable<CatActivityEntity>>.Success(cachedPlaces);

                var activities = await _context.CatActivities
                    .Select(a => new CatActivityEntity
                    {
                        IdActivity = a.IdActivity,
                        NameActivity = a.NameActivity ?? string.Empty,
                        UrlIcon = a.UrlIcon ?? string.Empty
                    })
                    .ToListAsync();

                _cacheService.SetCollection(CACHE_KEY, activities, TimeSpan.FromHours(1));
                return Result<IEnumerable<CatActivityEntity>>.Success(activities);
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<CatActivityEntity>>.Failure($"Error al obtener las actividades: {ex.Message}");
            }
        }

        public async Task<Result<CatActivityEntity>> GetById(int id)
        {
            try
            {
                var cachedPlaces = _cacheService.GetCollection<CatActivityEntity>(CACHE_KEY);
                var cachedPlace = cachedPlaces?.FirstOrDefault(p => p.IdActivity == id);
                if (cachedPlace != null)
                    return Result<CatActivityEntity>.Success(cachedPlace);

                var activity = await _context.CatActivities.FirstOrDefaultAsync(a => a.IdActivity == id);

                if (activity == null)
                    return Result<CatActivityEntity>.Failure("La actividad no fue encontrada.");

                var activityEntity = new CatActivityEntity
                {
                    IdActivity = activity.IdActivity,
                    NameActivity = activity.NameActivity ?? string.Empty,
                    UrlIcon = activity.UrlIcon ?? string.Empty
                };

                return Result<CatActivityEntity>.Success(activityEntity);
            }
            catch (Exception ex)
            {
                return Result<CatActivityEntity>.Failure($"Error al obtener la actividad con ID {id}: {ex.Message}");
            }
        }

        public async Task<Result<bool>> Update(CatActivityEntity entity)
        {
            try
            {
                var existingActivity = await _context.CatActivities.FindAsync(entity.IdActivity);

                if (existingActivity == null)
                    return Result<bool>.Failure("La actividad no fue encontrada.");

                existingActivity.NameActivity = entity.NameActivity;
                existingActivity.UrlIcon = entity.UrlIcon;

                _context.CatActivities.Update(existingActivity);
                int rowsAffected = await _context.SaveChangesAsync();
                _cacheService.Remove(CACHE_KEY);

                return rowsAffected > 0
                    ? Result<bool>.Success(true)
                    : Result<bool>.Failure("Error al actualizar la actividad.");
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure($"Error al actualizar la actividad: {ex.Message}");
            }
        }
    }
}
