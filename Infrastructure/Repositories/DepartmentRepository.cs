using Biokudi_Backend.Application.Interfaces;
using Biokudi_Backend.Domain.Entities;
using Biokudi_Backend.Domain.Interfaces;
using Biokudi_Backend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Biokudi_Backend.Infrastructure.Repositories
{
    public class DepartmentRepository(ICacheService cacheService, ApplicationDbContext context) : IDepartmentRepository
    {
        private const string CACHE_KEY = "DepartmentCache";
        private readonly ICacheService _cacheService = cacheService;
        private readonly ApplicationDbContext _context = context;
        public async Task<CatDepartmentEntity>? Create(CatDepartmentEntity entity)
        {
            try
            {
                var result = await _context.CatDepartments
                    .Where(d => d.NameDepartment == entity.NameDepartment)
                    .FirstOrDefaultAsync();

                if (result != null)
                    throw new InvalidOperationException("El departamento ya existe");

                var department = new CatDepartment
                {
                    NameDepartment = entity.NameDepartment
                };

                await _context.CatDepartments.AddAsync(department);
                int rowsAffected = await _context.SaveChangesAsync();
                if (rowsAffected == 0)
                    throw new InvalidOperationException("No se pudo crear el departamento");
                entity.IdDepartment = department.IdDepartment;
                _cacheService.Remove(CACHE_KEY);
                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al crear el departamento: {ex.Message}");
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var entity = await _context.CatDepartments.FindAsync(id);
                if (entity == null)
                    throw new Exception("El departamento no fue encontrado.");

                _context.CatDepartments.Remove(entity);
                int rowsAffected = await _context.SaveChangesAsync();
                _cacheService.Remove(CACHE_KEY);
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al eliminar el departamento");
            }
        }

        public async Task<IEnumerable<CatDepartmentEntity>?> GetAll()
        {
            try
            {
                var cachedPlaces = _cacheService.GetCollection<CatDepartmentEntity>(CACHE_KEY);
                if (cachedPlaces != null)
                    return cachedPlaces;

                var departments = await _context.CatDepartments
                    .Select(department => new CatDepartmentEntity
                    {
                        IdDepartment = department.IdDepartment,
                        NameDepartment = department.NameDepartment ?? string.Empty
                    })
                    .ToListAsync();

                return departments;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener los departamentos");
            }
        }

        public async Task<CatDepartmentEntity>? GetById(int id)
        {
            try
            {
                var cachedPlaces = _cacheService.GetCollection<CatDepartmentEntity>(CACHE_KEY);
                var cachedPlace = cachedPlaces?.FirstOrDefault(p => p.IdDepartment == id);
                if (cachedPlace != null)
                    return cachedPlace;

                var result = await _context.CatDepartments.FirstOrDefaultAsync(d => d.IdDepartment == id);

                if (result == null)
                    throw new Exception("El departamento no fue encontrado.");

                var department = new CatDepartmentEntity
                {
                    IdDepartment = result.IdDepartment,
                    NameDepartment = result.NameDepartment ?? string.Empty
                };

                return department;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener el departamento con ID {id}");
            }
        }

        public async Task<bool> Update(CatDepartmentEntity entity)
        {
            try
            {
                var existingEntity = await _context.CatDepartments.FindAsync(entity.IdDepartment);

                if (existingEntity == null)
                    throw new Exception("El departamento no fue encontrado.");

                existingEntity.NameDepartment = entity.NameDepartment;

                _context.CatDepartments.Update(existingEntity);
                int rowsAffected = await _context.SaveChangesAsync();
                _cacheService.Remove(CACHE_KEY);
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al actualizar el departamento: {ex.Message}");
            }
        }

        public Task<IEnumerable<CatDepartmentEntity>?> GetDepartmentsWithCities()
        {
            throw new NotImplementedException();
        }
    }
}
