using Biokudi_Backend.Application.DTOs;
using Biokudi_Backend.Application.Interfaces;
using Biokudi_Backend.Application.Mappings;
using Biokudi_Backend.Domain.Interfaces;
using Biokudi_Backend.Infrastructure.Repositories;

namespace Biokudi_Backend.Application.Services
{
    public class DepartmentService(DepartmentMapping _departmentMapping, IDepartmentRepository _departmentRepository) : IDepartmentService
    {
        private readonly DepartmentMapping _departmentMapping = _departmentMapping;
        private readonly IDepartmentRepository _departmentRepository = _departmentRepository;

        public async Task<bool> CreateDepartment(DepartmentRequestDto department)
        {
            try
            {
                var result = await _departmentRepository.Create(_departmentMapping.RequestToEntity(department));
                return result != null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteDepartment(int id)
        {
            try
            {
                var result = await _departmentRepository.Delete(id);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<DepartmentDto?> GetDepartmentById(int id)
        {
            try
            {
                var result = await _departmentRepository.GetById(id);
                return _departmentMapping.EntityToDto(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<DepartmentDto>> GetDepartments()
        {
            try
            {
                var result = await _departmentRepository.GetAll();
                return result.Select(department => _departmentMapping.EntityToDto(department)).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<bool> UpdateDepartment(int id, DepartmentRequestDto department)
        {
            try
            {
                var entity = _departmentMapping.RequestToEntity(department);
                entity.IdDepartment = id;
                var result = await _departmentRepository.Update(entity);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }

}
