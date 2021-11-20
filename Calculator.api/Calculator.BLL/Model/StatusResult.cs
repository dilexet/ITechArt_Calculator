using Calculator.BLL.Enums;

namespace Calculator.BLL.Model
{
    public class StatusResult
    {
        public StatusType StatusType { get; set; }
        public string Message { get; set; }
        public OperationResult OperationResult { get; set; }
    }
}