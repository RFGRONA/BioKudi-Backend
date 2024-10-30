using Biokudi_Backend.Application.DTOs;
using Biokudi_Backend.Application.Interfaces;
using Biokudi_Backend.Application.Mappings;
using Biokudi_Backend.Domain.Interfaces;
using Biokudi_Backend.Domain.ValueObject;
using Biokudi_Backend.Infrastructure.Repositories;

namespace Biokudi_Backend.Application.Services
{
    public class DepartmentService(DepartmentMapping _departmentMapping, IDepartmentRepository _departmentRepository) : IDepartmentService
    {
        private readonly DepartmentMapping _departmentMapping = _departmentMapping;
        private readonly IDepartmentRepository _departmentRepository = _departmentRepository;

        public async Task<Result<bool>> CreateDepartment(DepartmentRequestDto department)
        {
            var result = await _departmentRepository.Create(_departmentMapping.RequestToEntity(department));
            return result.IsSuccess
                ? Result<bool>.Success(true)
                : Result<bool>.Failure(result.ErrorMessage);
        }

        public async Task<Result<bool>> DeleteDepartment(int id)
        {
            return await _departmentRepository.Delete(id);
        }

        public async Task<Result<DepartmentDto>> GetDepartmentById(int id)
        {
            var result = await _departmentRepository.GetById(id);
            return result.IsSuccess
                ? Result<DepartmentDto>.Success(_departmentMapping.EntityToDto(result.Value))
                : Result<DepartmentDto>.Failure(result.ErrorMessage);
        }

        public async Task<Result<List<DepartmentDto>>> GetDepartments()
        {
            var result = await _departmentRepository.GetAll();
            return result.IsSuccess
                ? Result<List<DepartmentDto>>.Success(result.Value.Select(department => _departmentMapping.EntityToDto(department)).ToList())
                : Result<List<DepartmentDto>>.Failure(result.ErrorMessage);
        }

        public async Task<Result<bool>> UpdateDepartment(int id, DepartmentRequestDto department)
        {
            var entity = _departmentMapping.RequestToEntity(department);
            entity.IdDepartment = id;
            return await _departmentRepository.Update(entity);
        }
    }
}
