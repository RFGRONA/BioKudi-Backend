using Biokudi_Backend.Application.DTOs;
using Biokudi_Backend.Domain.Entities;

namespace Biokudi_Backend.Application.Mappings
{
    public class CityMapping
    {
        public CatCityEntity DtoToEntity(CityDto city)
        {
            return new CatCityEntity
            {
                IdCity = city.IdCity,
                NameCity = city.NameCity,
                DepartmentId = (int)city.IdDepartment
            };
        }

        public CatCityEntity RequestToEntity(CityRequestDto city)
        {
            return new CatCityEntity
            {
                NameCity = city.NameCity,
                DepartmentId = (int)city.IdDepartment
            };
        }

        public CityDto EntityToDto(CatCityEntity city)
        {
            return new CityDto
            {
                IdCity = city.IdCity,
                NameCity = city.NameCity,
                IdDepartment = city.DepartmentId,
                DepartmentCity = city.Department.NameDepartment
            };
        }
    }
}
