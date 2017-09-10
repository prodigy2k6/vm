using System;

namespace VendingMachine.Exceptions
{
    public class InvalidInputException : Exception
    {
        public InvalidInputException()
        {}

        public InvalidInputException(string message) : base(message)
        {}

        public InvalidInputException(string message, Exception baseException)
            : base(message, baseException)
        {}
    }
}
