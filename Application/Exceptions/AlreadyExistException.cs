using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Exceptions
{
   public class AlreadyExistException : Exception
    {
        public AlreadyExistException() : base()
        {

        }

        public AlreadyExistException(string message): base(message)
        {

        }
    }
}
