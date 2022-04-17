using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab7_OS.Exceptions
{
    internal class SyntaxException : Exception
    {
        public SyntaxException(string message):
            base(message)
        { }
    }
}
