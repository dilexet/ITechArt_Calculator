using System;

namespace Calculator.DAL.Entity
{
    public class Expression
    {
        public Guid Id { get; set; }
        public string MathExpression { get; set; }
        public Double Result { get; set; }
    }
}