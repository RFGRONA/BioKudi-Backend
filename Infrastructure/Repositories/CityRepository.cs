using Biokudi_Backend.Application.DTOs;
using Biokudi_Backend.Application.Interfaces;
using Biokudi_Backend.Domain.Entities;
using Biokudi_Backend.Domain.Interfaces;
using Biokudi_Backend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Biokudi_Backend.Infrastructure.Repositories
{
    public class CityRepository(ICacheService cacheService, ApplicationDbContext context) : ICityRepository
    {
        private const string CACHE_KEY = "CityCache";
        private readonly ICacheService _cacheService = cacheService;
        private readonly ApplicationDbContext _context = context;
        public async Task<CatCityEntity>? Create(CatCityEntity entity)
        {
            try
            {
                var result = await _context.CatCities.Where(c => c.NameCity == entity.NameCity).FirstOrDefaultAsync();
                if (result != null)
                    throw new InvalidOperationException("La ciudad ya existe");
                var city = new CatCity
                {
                    NameCity = entity.NameCity,
                    DepartmentId = entity.DepartmentId
                };
                await _context.CatCities.AddAsync(city);
                int rowsAffected = await _context.SaveChangesAsync();
                if (rowsAffected == 0)
                    throw new InvalidOperationException("No se pudo crear la ciudad");
                _cacheService.Remove(CACHE_KEY);
                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al crear la ciudad");
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var entity = await _context.CatCities.FindAsync(id);
                if (entity == null)
                    throw new Exception("La ciudad no fue encontrada.");
                _context.CatCities.Remove(entity);
                int rowsAffected = await _context.SaveChangesAsync();
                _cacheService.Remove(CACHE_KEY);
                return rowsAffected > 0; 
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al eliminar la ciudad");
            }
        }

        public async Task<IEnumerable<CatCityEntity>?> GetAll()
        {
            try
            {
                var cachedPlaces = _cacheService.GetCollection<CatCityEntity>(CACHE_KEY);
                if (cachedPlaces != null)
                    return cachedPlaces;

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
                return cities;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener las ciudades");
            }
        }

        public async Task<CatCityEntity>? GetById(int id)
        {
            try
            {
                var cachedPlaces = _cacheService.GetCollection<CatCityEntity>(CACHE_KEY);
                var cachedPlace = cachedPlaces?.FirstOrDefault(p => p.IdCity == id);
                if (cachedPlace != null)
                    return cachedPlace;

                var result = await _context.CatCities
                    .Include(c => c.Department) 
                    .FirstOrDefaultAsync(c => c.IdCity == id);
                if (result == null)
                    throw new Exception("La ciudad no fue encontrada.");
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
                return city;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener la ciudad con ID {id}");
            }
        }

        public async Task<bool> Update(CatCityEntity entity)
        {
            try
            {
                var existingEntity = await _context.CatCities.FindAsync(entity.IdCity);
                if (existingEntity == null)
                    throw new Exception("La ciudad no fue encontrada.");
                existingEntity.NameCity = entity.NameCity; 
                existingEntity.DepartmentId = entity.DepartmentId;
                _context.CatCities.Update(existingEntity);
                int rowsAffected = await _context.SaveChangesAsync();
                _cacheService.Remove(CACHE_KEY);
                return rowsAffected > 0; 
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al actualizar la ciudad");
            }
        }

        public Task<IEnumerable<CatCityEntity>?> GetCitiesByDepartmentIdAsync(int departmentId)
        {
            throw new NotImplementedException();
        }
    }
}
