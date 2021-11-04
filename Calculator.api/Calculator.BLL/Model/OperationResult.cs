using System;

namespace Calculator.BLL.Model
{
    public class OperationResult
    {
        public Double OperatorA { get; set; }
        public Double OperatorB { get; set; }
        public char OperationType { get; set; }
        public Double Result { get; set; }
    }
}