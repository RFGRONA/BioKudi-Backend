using Biokudi_Backend.Domain.Entities;
using Biokudi_Backend.Domain.Interfaces;

namespace Biokudi_Backend.Infrastructure.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        public Task Create(ReviewEntity entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(ReviewEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ReviewEntity>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<ReviewEntity> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ReviewEntity>> GetReviewsByPlaceIdAsync(int placeId)
        {
            throw new NotImplementedException();
        }

        public void Update(ReviewEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
