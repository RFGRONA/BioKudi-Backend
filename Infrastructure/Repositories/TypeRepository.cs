using Biokudi_Backend.Application.Interfaces;
using Biokudi_Backend.Domain.Entities;
using Biokudi_Backend.Domain.Interfaces;
using Biokudi_Backend.Domain.ValueObject;
using Biokudi_Backend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Biokudi_Backend.Infrastructure.Repositories
{
    public class TypeRepository(ICacheService cacheService, ApplicationDbContext context) : ITypeRepository
    {
        private const string CACHE_KEY = "TypeCache";
        private readonly ICacheService _cacheService = cacheService;
        private readonly ApplicationDbContext _context = context;

        public async Task<Result<CatTypeEntity>> Create(CatTypeEntity entity)
        {
            try
            {
                var existingType = await _context.CatTypes
                    .Where(t => t.NameType == entity.NameType)
                    .FirstOrDefaultAsync();

                if (existingType != null)
                    return Result<CatTypeEntity>.Failure("El tipo ya existe");

                var type = new CatType
                {
                    NameType = entity.NameType,
                    TableRelation = entity.TableRelation
                };

                await _context.CatTypes.AddAsync(type);
                int rowsAffected = await _context.SaveChangesAsync();

                if (rowsAffected == 0)
                    return Result<CatTypeEntity>.Failure("No se pudo crear el tipo");

                entity.IdType = type.IdType;
                _cacheService.Remove(CACHE_KEY);
                return Result<CatTypeEntity>.Success(entity);
            }
            catch (Exception ex)
            {
                return Result<CatTypeEntity>.Failure($"Error al crear el tipo: {ex.Message}");
            }
        }

        public async Task<Result<bool>> Delete(int id)
        {
            try
            {
                var entity = await _context.CatTypes.FindAsync(id);
                if (entity == null)
                    return Result<bool>.Failure("El tipo no fue encontrado.");

                _context.CatTypes.Remove(entity);
                int rowsAffected = await _context.SaveChangesAsync();

                _cacheService.Remove(CACHE_KEY);
                return Result<bool>.Success(rowsAffected > 0);
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure($"Error al eliminar el tipo: {ex.Message}");
            }
        }

        public async Task<Result<IEnumerable<CatTypeEntity>>> GetAll()
        {
            try
            {
                var cachedTypes = _cacheService.GetCollection<CatTypeEntity>(CACHE_KEY);
                if (cachedTypes != null)
                    return Result<IEnumerable<CatTypeEntity>>.Success(cachedTypes);

                var types = await _context.CatTypes
                    .Select(type => new CatTypeEntity
                    {
                        IdType = type.IdType,
                        NameType = type.NameType ?? string.Empty,
                        TableRelation = type.TableRelation ?? string.Empty
                    })
                    .ToListAsync();

                _cacheService.SetCollection(CACHE_KEY, types, TimeSpan.FromHours(1));
                return Result<IEnumerable<CatTypeEntity>>.Success(types);
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<CatTypeEntity>>.Failure($"Error al obtener los tipos: {ex.Message}");
            }
        }

        public async Task<Result<CatTypeEntity>> GetById(int id)
        {
            try
            {
                var cachedTypes = _cacheService.GetCollection<CatTypeEntity>(CACHE_KEY);
                var cachedType = cachedTypes?.FirstOrDefault(t => t.IdType == id);
                if (cachedType != null)
                    return Result<CatTypeEntity>.Success(cachedType);

                var result = await _context.CatTypes.FirstOrDefaultAsync(t => t.IdType == id);
                if (result == null)
                    return Result<CatTypeEntity>.Failure("El tipo no fue encontrado.");

                var type = new CatTypeEntity
                {
                    IdType = result.IdType,
                    NameType = result.NameType ?? string.Empty,
                    TableRelation = result.TableRelation ?? string.Empty
                };

                return Result<CatTypeEntity>.Success(type);
            }
            catch (Exception ex)
            {
                return Result<CatTypeEntity>.Failure($"Error al obtener el tipo con ID {id}: {ex.Message}");
            }
        }

        public async Task<Result<bool>> Update(CatTypeEntity entity)
        {
            try
            {
                var existingEntity = await _context.CatTypes.FindAsync(entity.IdType);
                if (existingEntity == null)
                    return Result<bool>.Failure("El tipo no fue encontrado.");

                existingEntity.NameType = entity.NameType;
                existingEntity.TableRelation = entity.TableRelation;

                _context.CatTypes.Update(existingEntity);
                int rowsAffected = await _context.SaveChangesAsync();

                _cacheService.Remove(CACHE_KEY);
                return Result<bool>.Success(rowsAffected > 0);
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure($"Error al actualizar el tipo: {ex.Message}");
            }
        }

        public Task<IEnumerable<CatTypeEntity>?> GetTypesByTableRelationAsync(string tableRelation)
        {
            throw new NotImplementedException();
        }
    }
}