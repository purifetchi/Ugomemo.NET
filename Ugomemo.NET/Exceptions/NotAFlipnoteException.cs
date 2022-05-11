using System;

namespace Ugomemo.NET.Exceptions
{
    public class NotAFlipnoteException : Exception
    {
        public NotAFlipnoteException(string message) : base(message)
        {
            
        }
    }
}
