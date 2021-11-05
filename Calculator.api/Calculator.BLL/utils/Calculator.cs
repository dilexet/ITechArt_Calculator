using System;
using System.Collections.Generic;
using System.Globalization;
using Serilog;

namespace Calculator.BLL.utils
{
    public class Calculator
        {
            private readonly IFormatProvider _formatter = new NumberFormatInfo { NumberDecimalSeparator = "." };
    
            public double Calculate(string expression)
            {
                var parseExpression = Parser.ParseExpression(expression);
                List<string> matches = new List<string>();
                foreach (var value in parseExpression)
                {
                    matches.Add(value.ToString());
                }
    
                try
                {
                    List<string> output = GetExpression(matches);
                    double result = Counting(output);
                    return result;
                }
                catch (Exception e)
                {
                    // ReSharper disable once TemplateIsNotCompileTimeConstantProblem
                    Log.Error(e.ToString());
                    throw new Exception("Syntax error!");
                }
            }
    
            private List<string> GetExpression(List<string> expression)
            {
                List<string> output = new List<string>();
                Stack<char> operatorStack = new Stack<char>();
    
                foreach (var t in expression)
                {
                    if (IsDelimiter(t))
                        continue;
    
                    bool isDigit = true;
                    Double digit = 0.0;
                    try
                    {
                        digit = Double.Parse(t, _formatter);
                    }
                    catch (FormatException)
                    {
                        isDigit = false;
                    }
    
                    if (isDigit)
                    {
                        output.Add(digit.ToString(CultureInfo.InvariantCulture));
                    }
    
                    if (IsOperator(t))
                    {
                        if (t == "(")
                            operatorStack.Push(char.Parse(t));
                        else if (t == ")")
                        {
                            char s = operatorStack.Pop();
    
                            while (s != '(')
                            {
                                output.Add(s.ToString());
                                s = operatorStack.Pop();
                            }
                        }
                        else
                        {
                            if (operatorStack.Count > 0)
                            {
                                if (GetPriority(t) <= GetPriority(operatorStack.Peek().ToString()))
                                {
                                    output.Add(operatorStack.Pop().ToString());
                                }
                            }
    
                            operatorStack.Push(
                                char.Parse(t));
                        }
                    }
                }
    
                while (operatorStack.Count > 0)
                    output.Add(operatorStack.Pop().ToString());
    
                return output;
            }
    
            private double Counting(List<string> expression)
            {
                double result = 0;
                Stack<double> temp = new Stack<double>();
    
                foreach (var t in expression)
                {
                    bool isDigit = true;
                    Double digit = 0.0;
                    try
                    {
                        digit = Double.Parse(t, _formatter);
                    }
                    catch (FormatException)
                    {
                        isDigit = false;
                    }
    
                    if (isDigit)
                    {
                        temp.Push(digit);
                    }
    
                    else if (IsOperator(t))
                    {
                        double a = temp.Pop();
                        double b = temp.Pop();
    
                        switch (t)
                        {
                            case "+":
                            {
                                result = b + a;
                                break;
                            }
                            case "-":
                            {
                                result = b - a;
                                break;
                            }
                            case "*":
                            {
                                result = b * a;
                                break;
                            }
                            case "/":
                            {
                                if (a == 0)
                                {
                                    throw new DivideByZeroException("Division by zero!");
                                }
    
                                result = b / a;
                                break;
                            }
                            case "^":
                            {
                                result = double.Parse(Math
                                    .Pow(double.Parse(b.ToString(CultureInfo.InvariantCulture), _formatter),
                                        double.Parse(a.ToString(CultureInfo.InvariantCulture), _formatter))
                                    .ToString(CultureInfo.InvariantCulture), _formatter);
                                break;
                            }
                        }
    
                        temp.Push(result);
                    }
                }
    
                return temp.Peek();
            }
    
            private bool IsDelimiter(string c)
            {
                if ((" =".IndexOf(c, StringComparison.Ordinal) != -1))
                    return true;
                return false;
            }
    
            private bool IsOperator(string с)
            {
                if (("+-/*^()".IndexOf(с, StringComparison.Ordinal) != -1))
                    return true;
                return false;
            }
    
            private byte GetPriority(string s)
            {
                switch (s)
                {
                    case "(": return 0;
                    case ")": return 1;
                    case "+": return 2;
                    case "-": return 3;
                    case "*": return 4;
                    case "/": return 4;
                    case "^": return 5;
                    default: return 6;
                }
            }
        }
}