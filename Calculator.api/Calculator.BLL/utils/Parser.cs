using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Serilog;

namespace Calculator.BLL.utils
{
    public class Parser
    {
        private const string RegularPattern = @"([*+/\-)(])|([0-9.]+|.)";

        public static IEnumerable<Match> ParseExpression(string expression)
        {
            var matches = Regex.Matches(expression, RegularPattern).ToList();
            if (matches.Count <= 0)
            {
                throw new ArgumentException("Syntax Error!");
            }
            return matches.ToList();
        }
    }
}