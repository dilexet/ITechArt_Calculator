using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Calculator.BLL.Abstract
{
    public interface IParser
    {
        IEnumerable<Match> ParseExpression(string expression);
    }
}