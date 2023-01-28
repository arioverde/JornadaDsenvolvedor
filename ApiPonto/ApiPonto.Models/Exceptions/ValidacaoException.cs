using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiPonto.Models.Exceptions
{ 
    public class ValidadaoException : Exception
    {
        public ValidadaoException() { }

        public ValidadaoException(string message)
            : base(message) { }

        public ValidadaoException(string message, Exception inner)
            : base(message, inner) { }
    }
}
