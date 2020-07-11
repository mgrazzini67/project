using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Parsers.ParserUtils
{
    public interface IParser<T>
    {
        IList<T> Parse();
    }
}
