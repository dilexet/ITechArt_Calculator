using System;

namespace Calculator.WebAPI.ViewModel
{
    public class OperationViewModel
    {
        public Double OperatorA { get; set; }
        public Double OperatorB { get; set; }
        public char OperationType { get; set; }
    }
}