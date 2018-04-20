using System;

namespace User.Service.CustomExceptions
{
    public class NotValidOrEmptyIdException : Exception
    {
        public NotValidOrEmptyIdException() : base("Id is not valid or empty")
        {

        }

        public NotValidOrEmptyIdException(string message) : base(message)
        {

        }
    }
}
