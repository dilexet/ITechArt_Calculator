using System;

namespace Calculator.DAL.Entity
{
    public class Expression
    {
        public Guid Id { get; set; }
        public Double OperatorA { get; set; }
        public Double OperatorB { get; set; }
        public char OperationType { get; set; }
        public Double Result { get; set; }
    }
}