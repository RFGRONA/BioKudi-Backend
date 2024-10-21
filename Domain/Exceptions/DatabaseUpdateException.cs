namespace Biokudi_Backend.Domain.Exceptions
{
    public class DatabaseUpdateException : Exception
    {
        public DatabaseUpdateException(string message) : base(message) { }
    }
}
