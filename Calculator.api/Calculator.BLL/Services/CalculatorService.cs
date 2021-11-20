using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Calculator.BLL.Abstract;
using Calculator.BLL.Enums;
using Calculator.BLL.Model;
using Calculator.DAL.Abstract;
using Calculator.DAL.Entity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Calculator.BLL.Services
{
    public class CalculatorService : ICalculatorService
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;
        private readonly ICalculator _calculator;
        private readonly ILogger<CalculatorService> _log;

        public CalculatorService(IRepository repository, IMapper mapper, ICalculator calculator,
            ILogger<CalculatorService> log)
        {
            _repository = repository;
            _mapper = mapper;
            _calculator = calculator;
            _log = log;
        }

        public async Task<StatusResult> Calculate(string expression)
        {
            Double result = _calculator.Calculate(expression);

            OperationResult operationResult = new OperationResult { MathExpression = expression, Result = result };
            operationResult.Result = result;
            try
            {
                var createResult = await _repository.CreateAsync(_mapper.Map<Expression>(operationResult));
                if (createResult)
                {
                    await _repository.SaveAsync();
                }
            }
            catch (SqlException e)
            {
                // ReSharper disable once TemplateIsNotCompileTimeConstantProblem
                _log.LogError(e.ToString());
                return new StatusResult() { StatusType = StatusType.Error, Message = "Error adding to database" };
            }

            return new StatusResult() { StatusType = StatusType.Success, OperationResult = operationResult };
        }

        public async Task<IEnumerable<OperationResult>> GetAllExpression()
        {
            var result = await _repository.GetAsync<Expression>().ToListAsync();
            IEnumerable<OperationResult> operationResult = _mapper.Map<IEnumerable<OperationResult>>(result);
            return operationResult;
        }
    }
}