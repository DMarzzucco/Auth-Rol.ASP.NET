using System.Net;

namespace User.Utils.Exceptions
{

    /// <summary>
    /// 409
    /// </summary>
    public class ConflictExceptions : Exception
    {
        public ConflictExceptions(string message) : base(message) { }
    }

    /// <summary>
    /// 400
    /// </summary>
    public class BadRequestException : Exception
    {
        public BadRequestException(string message) : base(message)
        {
        }
    }

    /// <summary>
    /// 404
    /// </summary>
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message)
        {
        }
    }
}
