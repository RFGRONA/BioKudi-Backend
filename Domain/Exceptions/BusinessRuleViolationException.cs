namespace Biokudi_Backend.Domain.Exceptions
{
    public class BusinessRuleViolationException : Exception
    {
        public BusinessRuleViolationException(string message) : base(message) { }
    }
}
