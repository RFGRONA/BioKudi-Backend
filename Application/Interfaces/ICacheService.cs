namespace Biokudi_Backend.Application.Interfaces
{
    public interface ICacheService
    {
        T? Get<T>(string key);
        IEnumerable<T>? GetCollection<T>(string key);
        void Set<T>(string key, T value, TimeSpan expirationTime);
        void SetCollection<T>(string key, IEnumerable<T> value, TimeSpan expirationTime);
        void Remove(string key);
    }
}
