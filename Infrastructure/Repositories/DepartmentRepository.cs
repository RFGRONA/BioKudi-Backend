using Biokudi_Backend.Application.Interfaces;
using Biokudi_Backend.Domain.Entities;
using Biokudi_Backend.Domain.Interfaces;
using Biokudi_Backend.Domain.ValueObject;
using Biokudi_Backend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Biokudi_Backend.Infrastructure.Repositories
{
    public class DepartmentRepository(ICacheService cacheService, ApplicationDbContext context) : IDepartmentRepository
    {
        private const string CACHE_KEY = "DepartmentCache";
        private readonly ICacheService _cacheService = cacheService;
        private readonly ApplicationDbContext _context = context;

        public async Task<Result<CatDepartmentEntity>> Create(CatDepartmentEntity entity)
        {
            try
            {
                var existingDepartment = await _context.CatDepartments
                    .Where(d => d.NameDepartment == entity.NameDepartment)
                    .FirstOrDefaultAsync();

                if (existingDepartment != null)
                    return Result<CatDepartmentEntity>.Failure("El departamento ya existe.");

                var department = new CatDepartment
                {
                    NameDepartment = entity.NameDepartment
                };

                await _context.CatDepartments.AddAsync(department);
                int rowsAffected = await _context.SaveChangesAsync();

                if (rowsAffected == 0)
                    return Result<CatDepartmentEntity>.Failure("No se pudo crear el departamento.");

                entity.IdDepartment = department.IdDepartment;
                _cacheService.Remove(CACHE_KEY);
                return Result<CatDepartmentEntity>.Success(entity);
            }
            catch (Exception ex)
            {
                return Result<CatDepartmentEntity>.Failure($"Error al crear el departamento: {ex.Message}");
            }
        }

        public async Task<Result<bool>> Delete(int id)
        {
            try
            {
                var entity = await _context.CatDepartments.FindAsync(id);
                if (entity == null)
                    return Result<bool>.Failure("El departamento no fue encontrado.");

                _context.CatDepartments.Remove(entity);
                int rowsAffected = await _context.SaveChangesAsync();
                _cacheService.Remove(CACHE_KEY);

                return rowsAffected > 0
                    ? Result<bool>.Success(true)
                    : Result<bool>.Failure("Error al eliminar el departamento.");
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure($"Error al eliminar el departamento: {ex.Message}");
            }
        }

        public async Task<Result<IEnumerable<CatDepartmentEntity>>> GetAll()
        {
            try
            {
                var cachedDepartments = _cacheService.GetCollection<CatDepartmentEntity>(CACHE_KEY);
                if (cachedDepartments != null)
                    return Result<IEnumerable<CatDepartmentEntity>>.Success(cachedDepartments);

                var departments = await _context.CatDepartments
                    .AsNoTracking()
                    .Select(department => new CatDepartmentEntity
                    {
                        IdDepartment = department.IdDepartment,
                        NameDepartment = department.NameDepartment ?? string.Empty
                    })
                    .ToListAsync();

                _cacheService.SetCollection(CACHE_KEY, departments, TimeSpan.FromHours(1));
                return Result<IEnumerable<CatDepartmentEntity>>.Success(departments);
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<CatDepartmentEntity>>.Failure($"Error al obtener los departamentos: {ex.Message}");
            }
        }

        public async Task<Result<CatDepartmentEntity>> GetById(int id)
        {
            try
            {
                var cachedDepartments = _cacheService.GetCollection<CatDepartmentEntity>(CACHE_KEY);
                var cachedDepartment = cachedDepartments?.FirstOrDefault(p => p.IdDepartment == id);
                if (cachedDepartment != null)
                    return Result<CatDepartmentEntity>.Success(cachedDepartment);

                var result = await _context.CatDepartments
                    .AsNoTracking()
                    .FirstOrDefaultAsync(d => d.IdDepartment == id);

                if (result == null)
                    return Result<CatDepartmentEntity>.Failure("El departamento no fue encontrado.");

                var department = new CatDepartmentEntity
                {
                    IdDepartment = result.IdDepartment,
                    NameDepartment = result.NameDepartment ?? string.Empty
                };

                return Result<CatDepartmentEntity>.Success(department);
            }
            catch (Exception ex)
            {
                return Result<CatDepartmentEntity>.Failure($"Error al obtener el departamento con ID {id}: {ex.Message}");
            }
        }

        public async Task<Result<bool>> Update(CatDepartmentEntity entity)
        {
            try
            {
                var existingEntity = await _context.CatDepartments.FindAsync(entity.IdDepartment);

                if (existingEntity == null)
                    return Result<bool>.Failure("El departamento no fue encontrado.");

                existingEntity.NameDepartment = entity.NameDepartment;

                _context.CatDepartments.Update(existingEntity);
                int rowsAffected = await _context.SaveChangesAsync();
                _cacheService.Remove(CACHE_KEY);

                return rowsAffected > 0
                    ? Result<bool>.Success(true)
                    : Result<bool>.Failure("Error al actualizar el departamento.");
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure($"Error al actualizar el departamento: {ex.Message}");
            }
        }

        public Task<IEnumerable<CatDepartmentEntity>?> GetDepartmentsWithCities()
        {
            throw new NotImplementedException();
        }
    }
}
