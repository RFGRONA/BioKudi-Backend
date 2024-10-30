using Biokudi_Backend.Application.DTOs;
using Biokudi_Backend.Domain.ValueObject;

namespace Biokudi_Backend.Application.Interfaces
{
    public interface ITypeService
    {
        Task<Result<List<TypeDto>>> GetTypes();
        Task<Result<TypeDto>> GetTypeById(int id);
        Task<Result<bool>> CreateType(TypeRequestDto department);
        Task<Result<bool>> UpdateType(int id, TypeRequestDto department);
        Task<Result<bool>> DeleteType(int id);
    }
}
