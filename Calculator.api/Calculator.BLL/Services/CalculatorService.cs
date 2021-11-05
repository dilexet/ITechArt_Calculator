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
using Serilog;

namespace Calculator.BLL.Services
{
    public class CalculatorService : ICalculatorService
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;


        public CalculatorService(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<StatusResult> Calculate(string expression)
        {
            utils.Calculator calculator = new utils.Calculator();

            Double result = calculator.Calculate(expression);

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
                Log.Error(e.ToString());
                return new StatusResult() { StatusType = StatusType.Error, Result = "Error adding to database" };
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