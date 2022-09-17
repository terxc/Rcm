using System;

namespace Rcm.Shared.Exceptions;
public class CustomException : Exception
{
    public CustomException(string message) : base(message)
    {
    }
}
