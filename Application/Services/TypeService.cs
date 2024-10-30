using Biokudi_Backend.Application.DTOs;
using Biokudi_Backend.Application.Interfaces;
using Biokudi_Backend.Application.Mappings;
using Biokudi_Backend.Domain.Interfaces;
using Biokudi_Backend.Domain.ValueObject;

namespace Biokudi_Backend.Application.Services
{
    public class TypeService(TypeMapping typeMapping, ITypeRepository typeRepository) : ITypeService
    {
        private readonly TypeMapping _typeMapping = typeMapping;
        private readonly ITypeRepository _typeRepository = typeRepository;

        public async Task<Result<bool>> CreateType(TypeRequestDto typeRequest)
        {
            var entity = _typeMapping.RequestToEntity(typeRequest);
            var result = await _typeRepository.Create(entity);
            return result.IsSuccess ? Result<bool>.Success(true) : Result<bool>.Failure(result.ErrorMessage);
        }

        public async Task<Result<bool>> DeleteType(int id)
        {
            var result = await _typeRepository.Delete(id);
            return result.IsSuccess ? Result<bool>.Success(true) : Result<bool>.Failure(result.ErrorMessage);
        }

        public async Task<Result<TypeDto>> GetTypeById(int id)
        {
            var result = await _typeRepository.GetById(id);
            return result.IsSuccess
                ? Result<TypeDto>.Success(_typeMapping.EntityToDto(result.Value))
                : Result<TypeDto>.Failure(result.ErrorMessage);
        }

        public async Task<Result<List<TypeDto>>> GetTypes()
        {
            var result = await _typeRepository.GetAll();
            return result.IsSuccess
                ? Result<List<TypeDto>>.Success(result.Value.Select(type => _typeMapping.EntityToDto(type)).ToList())
                : Result<List<TypeDto>>.Failure(result.ErrorMessage);
        }

        public async Task<Result<bool>> UpdateType(int id, TypeRequestDto typeRequest)
        {
            var entity = _typeMapping.RequestToEntity(typeRequest);
            entity.IdType = id;
            var result = await _typeRepository.Update(entity);
            return result.IsSuccess ? Result<bool>.Success(true) : Result<bool>.Failure(result.ErrorMessage);
        }
    }
}