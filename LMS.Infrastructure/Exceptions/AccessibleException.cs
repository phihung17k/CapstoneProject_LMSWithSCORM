using System;

namespace LMS.Infrastructure.Exceptions
{
    public class AccessibleException : Exception
    {
        public AccessibleException(string message) : base(message)
        {
        }
    }
}
