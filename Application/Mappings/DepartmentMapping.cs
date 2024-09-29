using Biokudi_Backend.Application.DTOs;
using Biokudi_Backend.Domain.Entities;

namespace Biokudi_Backend.Application.Mappings
{
    public class DepartmentMapping
    {
        public CatDepartmentEntity DtoToEntity(DepartmentDto department)
        {
            return new CatDepartmentEntity
            {
                IdDepartment = department.IdDepartment,
                NameDepartment = department.NameDepartment
            };
        }

        public CatDepartmentEntity RequestToEntity(DepartmentRequestDto department)
        {
            return new CatDepartmentEntity
            {
                NameDepartment = department.NameDepartment
            };
        }

        public DepartmentDto EntityToDto(CatDepartmentEntity department)
        {
            return new DepartmentDto
            {
                IdDepartment = department.IdDepartment,
                NameDepartment = department.NameDepartment
            };
        }

    }
}
