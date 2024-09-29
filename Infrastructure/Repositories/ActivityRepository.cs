using Biokudi_Backend.Domain.Entities;
using Biokudi_Backend.Domain.Interfaces;
using Biokudi_Backend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Biokudi_Backend.Infrastructure.Repositories
{
    public class ActivityRepository(ApplicationDbContext context) : IActivityRepository
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<CatActivityEntity>? Create(CatActivityEntity entity)
        {
            try
            {
                var existingActivity = await _context.CatActivities
                    .Where(a => a.NameActivity == entity.NameActivity)
                    .FirstOrDefaultAsync();

                if (existingActivity != null)
                    throw new InvalidOperationException("La actividad ya existe");

                var activity = new CatActivity
                {
                    NameActivity = entity.NameActivity,
                    UrlIcon = entity.UrlIcon
                };

                await _context.CatActivities.AddAsync(activity);
                int rowsAffected = await _context.SaveChangesAsync();

                if (rowsAffected == 0)
                    throw new InvalidOperationException("No se pudo crear la actividad");

                entity.IdActivity = activity.IdActivity;
                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al crear la actividad");
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var activity = await _context.CatActivities.FindAsync(id);
                if (activity == null)
                    throw new Exception("La actividad no fue encontrada.");

                _context.CatActivities.Remove(activity);
                int rowsAffected = await _context.SaveChangesAsync();

                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al eliminar la actividad");
            }
        }

        public async Task<IEnumerable<CatActivityEntity>?> GetAll()
        {
            try
            {
                var activities = await _context.CatActivities
                    .Select(a => new CatActivityEntity
                    {
                        IdActivity = a.IdActivity,
                        NameActivity = a.NameActivity ?? string.Empty,
                        UrlIcon = a.UrlIcon ?? string.Empty
                    })
                    .ToListAsync();

                return activities;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener las actividades");
            }
        }

        public async Task<CatActivityEntity>? GetById(int id)
        {
            try
            {
                var activity = await _context.CatActivities.FirstOrDefaultAsync(a => a.IdActivity == id);

                if (activity == null)
                    throw new Exception("La actividad no fue encontrada.");

                var activityEntity = new CatActivityEntity
                {
                    IdActivity = activity.IdActivity,
                    NameActivity = activity.NameActivity ?? string.Empty,
                    UrlIcon = activity.UrlIcon ?? string.Empty
                };

                return activityEntity;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener la actividad con ID {id}");
            }
        }

        public async Task<bool> Update(CatActivityEntity entity)
        {
            try
            {
                var existingActivity = await _context.CatActivities.FindAsync(entity.IdActivity);

                if (existingActivity == null)
                    throw new Exception("La actividad no fue encontrada.");

                existingActivity.NameActivity = entity.NameActivity;
                existingActivity.UrlIcon = entity.UrlIcon;

                _context.CatActivities.Update(existingActivity);
                int rowsAffected = await _context.SaveChangesAsync();

                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al actualizar la actividad");
            }
        }
    }
}
 