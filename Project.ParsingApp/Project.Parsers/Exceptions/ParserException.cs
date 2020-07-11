namespace Project.Parsers.Exceptions
{
    using System;

    public class ParserException : ApplicationException
    {
        public ParserException(string message) : base(message)
        {
        }
    }
}