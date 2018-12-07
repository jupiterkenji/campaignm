using System;

namespace campaignmonitor
{
    public class InvalidTriangleException : Exception
    {
        public InvalidTriangleException()
        {
        }

        public InvalidTriangleException(string message)
            : base(message)
        {
        }

        public InvalidTriangleException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}