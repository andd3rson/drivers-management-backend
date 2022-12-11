namespace Drivers_Management.Domain.Utils
{
    public class DomainExceptions : Exception
    {
        public DomainExceptions() { }
        public DomainExceptions(string message)
        : base(message) { }
        public DomainExceptions(string message, Exception exception)
         : base(message, exception) { }

    }
}