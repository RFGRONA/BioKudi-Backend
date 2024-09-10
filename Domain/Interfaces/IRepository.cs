namespace Biokudi_Backend.Domain.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetById(int id);
        Task<IEnumerable<T>> GetAll();
        Task Create(T entity);
        void Update(T entity);
        void Delete(T entity);
    }

}
