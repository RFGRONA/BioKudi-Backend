using Biokudi_Backend.Domain.Entities;
using Biokudi_Backend.Domain.Interfaces;
using Biokudi_Backend.Domain.ValueObject;
using Biokudi_Backend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Biokudi_Backend.Infrastructure.Repositories
{
    public class PictureRepository(ApplicationDbContext context) : IPictureRepository
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<Result<IEnumerable<PictureEntity>>> GetAll()
        {
            try
            {
                var pictures = await _context.Pictures
                    .AsNoTracking()
                    .Select(p => new PictureEntity
                    {
                        IdPicture = p.IdPicture,
                        Name = p.Name,
                        Link = p.Link,
                        DateCreated = p.DateCreated,
                        TypeId = p.TypeId,
                        PlaceId = p.PlaceId,
                        PersonId = p.PersonId,
                        TicketId = p.TicketId,
                        ReviewId = p.ReviewId,
                    })
                    .ToListAsync();

                return Result<IEnumerable<PictureEntity>>.Success(pictures);
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<PictureEntity>>.Failure($"Error al obtener las imágenes: {ex.Message}");
            }
        }

        public async Task<Result<bool>> Delete(int id)
        {
            try
            {
                var picture = await _context.Pictures.FindAsync(id);
                if (picture == null)
                    return Result<bool>.Failure("La imagen no fue encontrada.");

                _context.Pictures.Remove(picture);
                int rowsAffected = await _context.SaveChangesAsync();

                return rowsAffected > 0
                    ? Result<bool>.Success(true)
                    : Result<bool>.Failure("Error al eliminar la imagen.");
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure($"Error al eliminar la imagen {id}: {ex.Message}");
            }
        }

        public Task<Result<PictureEntity>> Create(PictureEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<Result<PictureEntity>> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Result<bool>> Update(PictureEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
