using Biokudi_Backend.Domain.ValueObject;

namespace Biokudi_Backend.Domain.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<Result<T>> GetById(int id);                  
        Task<Result<IEnumerable<T>>> GetAll();            
        Task<Result<T>> Create(T entity);                 
        Task<Result<bool>> Update(T entity);              
        Task<Result<bool>> Delete(int id);
    }

}
