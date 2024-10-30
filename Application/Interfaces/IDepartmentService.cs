using Biokudi_Backend.Application.DTOs;
using Biokudi_Backend.Domain.ValueObject;

namespace Biokudi_Backend.Application.Interfaces
{
    public interface IDepartmentService
    {
        Task<Result<List<DepartmentDto>>> GetDepartments();
        Task<Result<DepartmentDto>> GetDepartmentById(int id);
        Task<Result<bool>> CreateDepartment(DepartmentRequestDto department);
        Task<Result<bool>> UpdateDepartment(int id, DepartmentRequestDto department);
        Task<Result<bool>> DeleteDepartment(int id);
    }
}
