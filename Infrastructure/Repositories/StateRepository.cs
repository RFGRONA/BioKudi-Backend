using Biokudi_Backend.Application.Interfaces;
using Biokudi_Backend.Domain.Entities;
using Biokudi_Backend.Domain.Interfaces;
using Biokudi_Backend.Domain.ValueObject;
using Biokudi_Backend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Biokudi_Backend.Infrastructure.Repositories
{
    public class StateRepository(ICacheService cacheService, ApplicationDbContext context) : IStateRepository
    {
        private const string CACHE_KEY = "StateCache";
        private readonly ICacheService _cacheService = cacheService;
        private readonly ApplicationDbContext _context = context;

        public async Task<Result<CatStateEntity>> Create(CatStateEntity entity)
        {
            try
            {
                var existingState = await _context.CatStates
                    .Where(s => s.NameState == entity.NameState)
                    .FirstOrDefaultAsync();

                if (existingState != null)
                    return Result<CatStateEntity>.Failure("El estado ya existe");

                var state = new CatState
                {
                    NameState = entity.NameState,
                    TableRelation = entity.TableRelation
                };

                await _context.CatStates.AddAsync(state);
                int rowsAffected = await _context.SaveChangesAsync();
                if (rowsAffected == 0)
                    return Result<CatStateEntity>.Failure("No se pudo crear el estado");

                entity.IdState = state.IdState;
                _cacheService.Remove(CACHE_KEY);
                return Result<CatStateEntity>.Success(entity);
            }
            catch (Exception ex)
            {
                return Result<CatStateEntity>.Failure($"Error al crear el estado: {ex.Message}");
            }
        }

        public async Task<Result<bool>> Delete(int id)
        {
            try
            {
                var entity = await _context.CatStates.FindAsync(id);
                if (entity == null)
                    return Result<bool>.Failure("El estado no fue encontrado.");

                _context.CatStates.Remove(entity);
                int rowsAffected = await _context.SaveChangesAsync();
                _cacheService.Remove(CACHE_KEY);
                return Result<bool>.Success(rowsAffected > 0);
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure($"Error al eliminar el estado: {ex.Message}");
            }
        }

        public async Task<Result<IEnumerable<CatStateEntity>>> GetAll()
        {
            try
            {
                var cachedStates = _cacheService.GetCollection<CatStateEntity>(CACHE_KEY);
                if (cachedStates != null)
                    return Result<IEnumerable<CatStateEntity>>.Success(cachedStates);

                var states = await _context.CatStates
                    .AsNoTracking()
                    .Select(state => new CatStateEntity
                    {
                        IdState = state.IdState,
                        NameState = state.NameState ?? string.Empty,
                        TableRelation = state.TableRelation ?? string.Empty
                    })
                    .ToListAsync();
                _cacheService.SetCollection(CACHE_KEY, states, TimeSpan.FromHours(1));
                return Result<IEnumerable<CatStateEntity>>.Success(states);
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<CatStateEntity>>.Failure($"Error al obtener los estados: {ex.Message}");
            }
        }

        public async Task<Result<CatStateEntity>> GetById(int id)
        {
            try
            {
                var cachedStates = _cacheService.GetCollection<CatStateEntity>(CACHE_KEY);
                var cachedState = cachedStates?.FirstOrDefault(p => p.IdState == id);
                if (cachedState != null)
                    return Result<CatStateEntity>.Success(cachedState);

                var result = await _context.CatStates
                    .AsNoTracking()
                    .FirstOrDefaultAsync(s => s.IdState == id);

                if (result == null)
                    return Result<CatStateEntity>.Failure("El estado no fue encontrado.");

                var state = new CatStateEntity
                {
                    IdState = result.IdState,
                    NameState = result.NameState ?? string.Empty,
                    TableRelation = result.TableRelation ?? string.Empty
                };

                return Result<CatStateEntity>.Success(state);
            }
            catch (Exception ex)
            {
                return Result<CatStateEntity>.Failure($"Error al obtener el estado con ID {id}: {ex.Message}");
            }
        }

        public async Task<Result<bool>> Update(CatStateEntity entity)
        {
            try
            {
                var existingEntity = await _context.CatStates.FindAsync(entity.IdState);
                if (existingEntity == null)
                    return Result<bool>.Failure("El estado no fue encontrado.");

                existingEntity.NameState = entity.NameState;
                existingEntity.TableRelation = entity.TableRelation;

                _context.CatStates.Update(existingEntity);
                int rowsAffected = await _context.SaveChangesAsync();
                _cacheService.Remove(CACHE_KEY);
                return Result<bool>.Success(rowsAffected > 0);
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure($"Error al actualizar el estado: {ex.Message}");
            }
        }

        public Task<IEnumerable<CatStateEntity>?> GetStatesByTableRelation(string tableRelation)
        {
            throw new NotImplementedException();
        }
    }
}