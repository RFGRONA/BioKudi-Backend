using Biokudi_Backend.Application.DTOs;

namespace Biokudi_Backend.Application.Interfaces
{
    public interface IDepartmentService
    {
        Task<List<DepartmentDto>?> GetDepartments();
        Task<DepartmentDto?> GetDepartmentById(int id);
        Task<bool> CreateDepartment(DepartmentRequestDto department);
        Task<bool> UpdateDepartment(int id, DepartmentRequestDto department);
        Task<bool> DeleteDepartment(int id);
    }
}
