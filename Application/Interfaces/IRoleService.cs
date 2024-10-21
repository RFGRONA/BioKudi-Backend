using Biokudi_Backend.Application.DTOs;
using Biokudi_Backend.Domain.ValueObject;

namespace Biokudi_Backend.Application.Interfaces
{
    public interface IRoleService
    {
        Task<Result<List<RoleDto>>> GetRole();
        Task<Result<RoleDto>> GetRoleById(int id);
        Task<Result<bool>> CreateRole(RoleRequestDto role);
        Task<Result<bool>> UpdateRole(int id, RoleRequestDto role);
        Task<Result<bool>> DeleteRole(int id);
    }
}
