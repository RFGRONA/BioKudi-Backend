namespace Biokudi_Backend.Domain.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<T>? GetById(int id);
        Task<IEnumerable<T>?> GetAll();
        Task<T>? Create(T entity);
        Task<bool> Update(T entity);
        Task<bool> Delete(int id);
    }

}
