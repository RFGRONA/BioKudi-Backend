using Biokudi_Backend.Application.DTOs;
using Biokudi_Backend.Application.Interfaces;
using Biokudi_Backend.Domain.Entities;
using Biokudi_Backend.Domain.Interfaces;
using Biokudi_Backend.Domain.ValueObject;
using Biokudi_Backend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Biokudi_Backend.Infrastructure.Repositories
{
    public class CityRepository(ICacheService cacheService, ApplicationDbContext context) : ICityRepository
    {
        private const string CACHE_KEY = "CityCache";
        private readonly ICacheService _cacheService = cacheService;
        private readonly ApplicationDbContext _context = context;

        public async Task<Result<CatCityEntity>> Create(CatCityEntity entity)
        {
            try
            {
                var existingCity = await _context.CatCities
                    .Where(c => c.NameCity == entity.NameCity)
                    .FirstOrDefaultAsync();

                if (existingCity != null)
                    return Result<CatCityEntity>.Failure("La ciudad ya existe.");

                var city = new CatCity
                {
                    NameCity = entity.NameCity,
                    DepartmentId = entity.DepartmentId
                };

                await _context.CatCities.AddAsync(city);
                int rowsAffected = await _context.SaveChangesAsync();

                if (rowsAffected == 0)
                    return Result<CatCityEntity>.Failure("No se pudo crear la ciudad.");

                _cacheService.Remove(CACHE_KEY);
                return Result<CatCityEntity>.Success(entity);
            }
            catch (Exception ex)
            {
                return Result<CatCityEntity>.Failure($"Error al crear la ciudad: {ex.Message}");
            }
        }

        public async Task<Result<bool>> Delete(int id)
        {
            try
            {
                var entity = await _context.CatCities.FindAsync(id);
                if (entity == null)
                    return Result<bool>.Failure("La ciudad no fue encontrada.");

                _context.CatCities.Remove(entity);
                int rowsAffected = await _context.SaveChangesAsync();
                _cacheService.Remove(CACHE_KEY);

                return rowsAffected > 0
                    ? Result<bool>.Success(true)
                    : Result<bool>.Failure("Error al eliminar la ciudad.");
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure($"Error al eliminar la ciudad: {ex.Message}");
            }
        }

        public async Task<Result<IEnumerable<CatCityEntity>>> GetAll()
        {
            try
            {
                var cachedCities = _cacheService.GetCollection<CatCityEntity>(CACHE_KEY);
                if (cachedCities != null)
                    return Result<IEnumerable<CatCityEntity>>.Success(cachedCities);

                var cities = await _context.CatCities
                    .Include(c => c.Department)
                    .Select(city => new CatCityEntity
                    {
                        IdCity = city.IdCity,
                        NameCity = city.NameCity,
                        DepartmentId = (int)city.DepartmentId,
                        Department = new CatDepartmentEntity
                        {
                            IdDepartment = city.Department.IdDepartment,
                            NameDepartment = city.Department.NameDepartment
                        }
                    })
                    .ToListAsync();

                _cacheService.SetCollection(CACHE_KEY, cities, TimeSpan.FromHours(1));
                return Result<IEnumerable<CatCityEntity>>.Success(cities);
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<CatCityEntity>>.Failure($"Error al obtener las ciudades: {ex.Message}");
            }
        }

        public async Task<Result<CatCityEntity>> GetById(int id)
        {
            try
            {
                var cachedCities = _cacheService.GetCollection<CatCityEntity>(CACHE_KEY);
                var cachedCity = cachedCities?.FirstOrDefault(p => p.IdCity == id);
                if (cachedCity != null)
                    return Result<CatCityEntity>.Success(cachedCity);

                var result = await _context.CatCities
                    .Include(c => c.Department)
                    .FirstOrDefaultAsync(c => c.IdCity == id);

                if (result == null)
                    return Result<CatCityEntity>.Failure("La ciudad no fue encontrada.");

                var city = new CatCityEntity
                {
                    IdCity = result.IdCity,
                    NameCity = result.NameCity,
                    DepartmentId = (int)result.DepartmentId,
                    Department = new CatDepartmentEntity
                    {
                        IdDepartment = result.Department.IdDepartment,
                        NameDepartment = result.Department.NameDepartment
                    }
                };

                return Result<CatCityEntity>.Success(city);
            }
            catch (Exception ex)
            {
                return Result<CatCityEntity>.Failure($"Error al obtener la ciudad con ID {id}: {ex.Message}");
            }
        }

        public async Task<Result<bool>> Update(CatCityEntity entity)
        {
            try
            {
                var existingEntity = await _context.CatCities.FindAsync(entity.IdCity);
                if (existingEntity == null)
                    return Result<bool>.Failure("La ciudad no fue encontrada.");

                existingEntity.NameCity = entity.NameCity;
                existingEntity.DepartmentId = entity.DepartmentId;

                _context.CatCities.Update(existingEntity);
                int rowsAffected = await _context.SaveChangesAsync();
                _cacheService.Remove(CACHE_KEY);

                return rowsAffected > 0
                    ? Result<bool>.Success(true)
                    : Result<bool>.Failure("Error al actualizar la ciudad.");
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure($"Error al actualizar la ciudad: {ex.Message}");
            }
        }

        public Task<IEnumerable<CatCityEntity>?> GetCitiesByDepartmentIdAsync(int departmentId)
        {
            throw new NotImplementedException();
        }
    }
}
