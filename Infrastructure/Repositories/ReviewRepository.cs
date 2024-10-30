using Biokudi_Backend.Application.Utilities;
using Biokudi_Backend.Domain.Entities;
using Biokudi_Backend.Domain.Interfaces;
using Biokudi_Backend.Domain.ValueObject;
using Biokudi_Backend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Biokudi_Backend.Infrastructure.Repositories
{
    public class ReviewRepository(ApplicationDbContext context) : IReviewRepository
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<Result<ReviewEntity>> Create(ReviewEntity entity)
        {
            try
            {
                var review = new Review
                {
                    Rate = entity.Rate,
                    Comment = entity.Comment,
                    DateCreated = DateUtility.DateNowColombia(),
                    DateModified = DateUtility.DateNowColombia(),
                    PersonId = entity.PersonId,
                    PlaceId = entity.PlaceId
                };

                await _context.Reviews.AddAsync(review);
                int rowsAffected = await _context.SaveChangesAsync();

                if (rowsAffected == 0)
                    return Result<ReviewEntity>.Failure("No se pudo crear la reseña");

                var createdReview = await _context.Reviews
                    .Include(r => r.Person)
                    .Include(r => r.Place)
                    .FirstOrDefaultAsync(r => r.IdReview == review.IdReview);

                if (createdReview == null)
                    return Result<ReviewEntity>.Failure("Error al obtener la reseña creada.");

                entity.IdReview = createdReview.IdReview;
                entity.Person = createdReview.Person;
                entity.Place = createdReview.Place;

                return Result<ReviewEntity>.Success(entity);
            }
            catch (Exception ex)
            {
                return Result<ReviewEntity>.Failure($"Error al crear la reseña: {ex.Message}");
            }
        }

        public async Task<Result<bool>> Delete(int id)
        {
            try
            {
                var entity = await _context.Reviews.FindAsync(id);
                if (entity == null)
                    return Result<bool>.Failure("La reseña no fue encontrada.");

                _context.Reviews.Remove(entity);
                int rowsAffected = await _context.SaveChangesAsync();

                return Result<bool>.Success(rowsAffected > 0);
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure($"Error al eliminar la reseña: {ex.Message}");
            }
        }

        public async Task<Result<IEnumerable<ReviewEntity>>> GetAll()
        {
            try
            {
                var reviews = await _context.Reviews
                    .AsNoTracking()
                    .Select(review => new ReviewEntity
                    {
                        IdReview = review.IdReview,
                        Rate = review.Rate,
                        Comment = review.Comment ?? string.Empty,
                        DateCreated = review.DateCreated ?? DateUtility.DateNowColombia(),
                        DateModified = review.DateModified,
                        PersonId = review.PersonId,
                        PlaceId = review.PlaceId,
                        Person = review.Person,
                        Place = review.Place
                    })
                    .ToListAsync();

                return Result<IEnumerable<ReviewEntity>>.Success(reviews);
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<ReviewEntity>>.Failure($"Error al obtener las reseñas: {ex.Message}");
            }
        }

        public async Task<Result<ReviewEntity>> GetById(int id)
        {
            try
            {
                var result = await _context.Reviews
                    .AsNoTracking()
                    .Where(r => r.IdReview == id)
                    .Select(review => new ReviewEntity
                    {
                        IdReview = review.IdReview,
                        Rate = review.Rate,
                        Comment = review.Comment ?? string.Empty,
                        DateCreated = review.DateCreated ?? DateUtility.DateNowColombia(),
                        DateModified = review.DateModified,
                        PersonId = review.PersonId,
                        PlaceId = review.PlaceId,
                        Person = review.Person,
                        Place = review.Place
                    })
                    .FirstOrDefaultAsync();

                if (result == null)
                    return Result<ReviewEntity>.Failure("La reseña no fue encontrada.");

                return Result<ReviewEntity>.Success(result);
            }
            catch (Exception ex)
            {
                return Result<ReviewEntity>.Failure($"Error al obtener la reseña con ID {id}: {ex.Message}");
            }
        }

        public async Task<Result<IEnumerable<ReviewEntity>>> GetReviewsByPlaceId(int placeId)
        {
            try
            {
                var reviews = await _context.Reviews
                    .AsNoTracking()
                    .Where(r => r.PlaceId == placeId)
                    .Select(review => new ReviewEntity
                    {
                        IdReview = review.IdReview,
                        Rate = review.Rate,
                        Comment = review.Comment ?? string.Empty,
                        DateCreated = review.DateCreated ?? DateUtility.DateNowColombia(),
                        DateModified = review.DateModified,
                        PersonId = review.PersonId,
                        PlaceId = review.PlaceId,
                        Person = review.Person,
                        Place = review.Place
                    })
                    .ToListAsync();

                return Result<IEnumerable<ReviewEntity>>.Success(reviews);
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<ReviewEntity>>.Failure($"Error al obtener las reseñas del lugar con ID {placeId}: {ex.Message}");
            }
        }

        public async Task<Result<bool>> Update(ReviewEntity entity)
        {
            try
            {
                var existingEntity = await _context.Reviews.FindAsync(entity.IdReview);
                if (existingEntity == null)
                    return Result<bool>.Failure("La reseña no fue encontrada.");

                existingEntity.Rate = entity.Rate;
                existingEntity.Comment = entity.Comment;
                existingEntity.DateModified = DateUtility.DateNowColombia();

                _context.Reviews.Update(existingEntity);
                int rowsAffected = await _context.SaveChangesAsync();

                return Result<bool>.Success(rowsAffected > 0);
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure($"Error al actualizar la reseña: {ex.Message}");
            }
        }
    }
}
