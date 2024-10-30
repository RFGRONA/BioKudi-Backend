using Biokudi_Backend.Application.DTOs.Response;
using Biokudi_Backend.Application.Interfaces;
using Biokudi_Backend.Application.Mappings;
using Biokudi_Backend.Domain.Interfaces;
using Biokudi_Backend.Domain.ValueObject;

namespace Biokudi_Backend.Application.Services
{
    public class PictureService(PictureMapping pictureMapping, IPictureRepository pictureRepository) : IPictureService
    {
        private readonly PictureMapping _pictureMapping = pictureMapping;
        private readonly IPictureRepository _pictureRepository = pictureRepository;

        public async Task<Result<List<PictureResponseDto>>> GetPictures()
        {
            var result = await _pictureRepository.GetAll();
            if (result.IsFailure)
                return Result<List<PictureResponseDto>>.Failure(result.ErrorMessage);

            var pictures = result.Value?.Select(picture => _pictureMapping.EntitytoResponse(picture)).ToList();
            return Result<List<PictureResponseDto>>.Success(pictures);
        }

        public async Task<Result<bool>> DeletePicture(int id)
        {
            var result = await _pictureRepository.Delete(id);
            return result.IsSuccess
                ? Result<bool>.Success(true)
                : Result<bool>.Failure(result.ErrorMessage);
        }
    }
}
