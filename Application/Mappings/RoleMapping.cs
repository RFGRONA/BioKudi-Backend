using Biokudi_Backend.Application.DTOs;
using Biokudi_Backend.Domain.Entities;

namespace Biokudi_Backend.Application.Mappings
{
    public class RoleMapping
    {
        public CatRoleEntity DtoToEntity(RoleDto role)
        {
            return new CatRoleEntity
            {
                IdRole = role.IdRole,
                NameRole = role.NameRole
            };
        }

        public CatRoleEntity RequestToEntity(RoleRequestDto role)
        {
            return new CatRoleEntity
            {
                NameRole = role.NameRole
            };
        }

        public RoleDto EntityToDto(CatRoleEntity role)
        {
            return new RoleDto
            {
                IdRole = role.IdRole,
                NameRole = role.NameRole
            };
        }
    }
}
