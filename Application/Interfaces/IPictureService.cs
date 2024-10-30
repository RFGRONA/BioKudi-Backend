using Biokudi_Backend.Application.DTOs.Response;
using Biokudi_Backend.Domain.ValueObject;

namespace Biokudi_Backend.Application.Interfaces
{
    public interface IPictureService
    {
        Task<Result<List<PictureResponseDto>>> GetPictures();
        Task<Result<bool>> DeletePicture(int id);
    }
}
