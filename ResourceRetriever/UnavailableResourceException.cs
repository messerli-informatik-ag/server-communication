using System;

namespace ResourceRetriever
{
    public class UnavailableResourceException : Exception
    {
        public UnavailableResourceException()
        {
        }

        public UnavailableResourceException(string message)
            : base(message)
        {
        }

        public UnavailableResourceException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}