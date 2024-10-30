using Biokudi_Backend.Application.DTOs;
using Biokudi_Backend.Application.Interfaces;
using Biokudi_Backend.Application.Mappings;
using Biokudi_Backend.Domain.Interfaces;
using Biokudi_Backend.Domain.ValueObject;

namespace Biokudi_Backend.Application.Services
{
    public class RoleService(RoleMapping roleMapping, IRoleRepository roleRepository) : IRoleService
    {
        private readonly RoleMapping _roleMapping = roleMapping;
        private readonly IRoleRepository _roleRepository = roleRepository;

        public async Task<Result<bool>> CreateRole(RoleRequestDto role)
        {
            var entity = _roleMapping.RequestToEntity(role);
            var result = await _roleRepository.Create(entity);
            return result.IsSuccess ? Result<bool>.Success(true) : Result<bool>.Failure(result.ErrorMessage);
        }

        public async Task<Result<bool>> DeleteRole(int id)
        {
            var result = await _roleRepository.Delete(id);
            return result.IsSuccess ? Result<bool>.Success(true) : Result<bool>.Failure(result.ErrorMessage);
        }

        public async Task<Result<RoleDto>> GetRoleById(int id)
        {
            var result = await _roleRepository.GetById(id);
            return result.IsSuccess
                ? Result<RoleDto>.Success(_roleMapping.EntityToDto(result.Value))
                : Result<RoleDto>.Failure(result.ErrorMessage);
        }

        public async Task<Result<List<RoleDto>>> GetRole()
        {
            var result = await _roleRepository.GetAll();
            return result.IsSuccess
                ? Result<List<RoleDto>>.Success(result.Value.Select(role => _roleMapping.EntityToDto(role)).ToList())
                : Result<List<RoleDto>>.Failure(result.ErrorMessage);
        }

        public async Task<Result<bool>> UpdateRole(int id, RoleRequestDto role)
        {
            var entity = _roleMapping.RequestToEntity(role);
            entity.IdRole = id;
            var result = await _roleRepository.Update(entity);
            return result.IsSuccess ? Result<bool>.Success(true) : Result<bool>.Failure(result.ErrorMessage);
        }
    }
}