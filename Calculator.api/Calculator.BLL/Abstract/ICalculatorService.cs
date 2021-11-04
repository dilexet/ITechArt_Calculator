using System.Collections.Generic;
using System.Threading.Tasks;
using Calculator.BLL.Model;

namespace Calculator.BLL.Abstract
{
    public interface ICalculatorService
    {
        Task<StatusResult> Calculate(Operation operation);
        Task<IEnumerable<OperationResult>> GetAllExpression();
    }
}