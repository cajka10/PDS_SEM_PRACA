using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy_IS.Model
{
    class NoSuchUserException : Exception
    {
        public NoSuchUserException()
        {
        }

        public NoSuchUserException(string message)
            : base(message)
        {
        }

        public NoSuchUserException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
