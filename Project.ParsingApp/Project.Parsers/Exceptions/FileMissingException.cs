using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Parsers.Exceptions
{
    class FileMissingException : ApplicationException
    {
        public FileMissingException(string message) : base(message)
        {
        }
    }
}
