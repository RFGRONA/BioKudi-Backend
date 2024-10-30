using Biokudi_Backend.Application.Interfaces;
using Biokudi_Backend.Domain.Entities;
using Biokudi_Backend.Domain.Interfaces;
using Biokudi_Backend.Domain.ValueObject;
using Biokudi_Backend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Biokudi_Backend.Infrastructure.Repositories
{
    public class RoleRepository(ICacheService cacheService, ApplicationDbContext context) : IRoleRepository
    {
        private const string CACHE_KEY = "RoleCache";
        private readonly ICacheService _cacheService = cacheService;
        private readonly ApplicationDbContext _context = context;

        public async Task<Result<CatRoleEntity>> Create(CatRoleEntity entity)
        {
            try
            {
                var existingRole = await _context.CatRoles
                    .Where(r => r.NameRole == entity.NameRole)
                    .FirstOrDefaultAsync();

                if (existingRole != null)
                    return Result<CatRoleEntity>.Failure("El rol ya existe");

                var role = new CatRole
                {
                    NameRole = entity.NameRole
                };

                await _context.CatRoles.AddAsync(role);
                int rowsAffected = await _context.SaveChangesAsync();

                if (rowsAffected == 0)
                    return Result<CatRoleEntity>.Failure("No se pudo crear el rol");

                entity.IdRole = role.IdRole;
                _cacheService.Remove(CACHE_KEY);

                return Result<CatRoleEntity>.Success(entity);
            }
            catch (Exception ex)
            {
                return Result<CatRoleEntity>.Failure($"Error al crear el rol: {ex.Message}");
            }
        }

        public async Task<Result<bool>> Delete(int id)
        {
            try
            {
                var entity = await _context.CatRoles.FindAsync(id);
                if (entity == null)
                    return Result<bool>.Failure("El rol no fue encontrado.");

                _context.CatRoles.Remove(entity);
                int rowsAffected = await _context.SaveChangesAsync();
                _cacheService.Remove(CACHE_KEY);

                return Result<bool>.Success(rowsAffected > 0);
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure($"Error al eliminar el rol: {ex.Message}");
            }
        }

        public async Task<Result<IEnumerable<CatRoleEntity>>> GetAll()
        {
            try
            {
                var cachedRoles = _cacheService.GetCollection<CatRoleEntity>(CACHE_KEY);
                if (cachedRoles != null)
                    return Result<IEnumerable<CatRoleEntity>>.Success(cachedRoles);

                var roles = await _context.CatRoles
                    .AsNoTracking()
                    .Select(role => new CatRoleEntity
                    {
                        IdRole = role.IdRole,
                        NameRole = role.NameRole
                    })
                    .ToListAsync();

                _cacheService.SetCollection(CACHE_KEY, roles, TimeSpan.FromHours(1));
                return Result<IEnumerable<CatRoleEntity>>.Success(roles);
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<CatRoleEntity>>.Failure($"Error al obtener los roles: {ex.Message}");
            }
        }

        public async Task<Result<CatRoleEntity>> GetById(int id)
        {
            try
            {
                var cachedRoles = _cacheService.GetCollection<CatRoleEntity>(CACHE_KEY);
                var cachedRole = cachedRoles?.FirstOrDefault(r => r.IdRole == id);
                if (cachedRole != null)
                    return Result<CatRoleEntity>.Success(cachedRole);

                var result = await _context.CatRoles
                    .AsNoTracking()
                    .FirstOrDefaultAsync(r => r.IdRole == id);

                if (result == null)
                    return Result<CatRoleEntity>.Failure("El rol no fue encontrado.");

                var role = new CatRoleEntity
                {
                    IdRole = result.IdRole,
                    NameRole = result.NameRole
                };

                return Result<CatRoleEntity>.Success(role);
            }
            catch (Exception ex)
            {
                return Result<CatRoleEntity>.Failure($"Error al obtener el rol con ID {id}: {ex.Message}");
            }
        }

        public async Task<Result<bool>> Update(CatRoleEntity entity)
        {
            try
            {
                var existingEntity = await _context.CatRoles.FindAsync(entity.IdRole);
                if (existingEntity == null)
                    return Result<bool>.Failure("El rol no fue encontrado.");

                existingEntity.NameRole = entity.NameRole;

                _context.CatRoles.Update(existingEntity);
                int rowsAffected = await _context.SaveChangesAsync();
                _cacheService.Remove(CACHE_KEY);

                return Result<bool>.Success(rowsAffected > 0);
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure($"Error al actualizar el rol: {ex.Message}");
            }
        }
    }
}