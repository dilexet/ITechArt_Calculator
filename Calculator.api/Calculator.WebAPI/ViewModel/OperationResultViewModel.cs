using System;

namespace Calculator.WebAPI.ViewModel
{
    public class OperationResultViewModel
    {
        public Double OperatorA { get; set; }
        public Double OperatorB { get; set; }
        public char OperationType { get; set; }
        public Double Result { get; set; }
    }
}