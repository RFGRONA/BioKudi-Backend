﻿using Biokudi_Backend.Domain.Entities;
using Biokudi_Backend.Domain.Interfaces;

namespace Biokudi_Backend.Infrastructure.Repositories
{
    public class CityRepository : ICityRepository
    {
        public Task Create(CatCityEntity entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(CatCityEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CatCityEntity>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<CatCityEntity> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CatCityEntity>> GetCitiesByDepartmentIdAsync(int departmentId)
        {
            throw new NotImplementedException();
        }

        public void Update(CatCityEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
