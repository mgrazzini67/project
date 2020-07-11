using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Parsers.Exceptions 
{
    class UnknownFileTypeException : ApplicationException
    {
        public UnknownFileTypeException(string message) : base(message)
        {
        }
    }
}
