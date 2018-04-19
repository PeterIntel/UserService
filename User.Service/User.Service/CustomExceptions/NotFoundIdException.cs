using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace User.Service.CustomExceptions
{
    public class NotFoundIdException : Exception
    {
        public NotFoundIdException() : base("Record with such Id was not found")
        {

        }

        public NotFoundIdException(string message): base(message)
        {

        }
    }
}
