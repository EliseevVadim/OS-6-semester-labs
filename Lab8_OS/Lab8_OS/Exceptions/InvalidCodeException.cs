using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab8_OS.Exceptions
{
    internal class InvalidCodeException : Exception
    {
        public InvalidCodeException(string message) : base(message)
        {

        }
    }
}
