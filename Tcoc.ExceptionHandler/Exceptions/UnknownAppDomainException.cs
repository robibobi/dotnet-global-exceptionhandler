using System;

namespace Tcoc.ExceptionHandling.Exceptions
{
    public class UnknownAppDomainException : Exception
    {
        public UnknownAppDomainException(string msg) : base(msg)
        {
        }
    }
}
