using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Calculator.BLL.Abstract;
using Calculator.BLL.Enums;
using Calculator.WebAPI.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

// ReSharper disable TemplateIsNotCompileTimeConstantProblem

namespace Calculator.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CalculatorController : ControllerBase
    {
        private readonly ICalculatorService _calculatorService;
        private readonly IMapper _mapper;
        private readonly ILogger<CalculatorController> _log;

        public CalculatorController(ICalculatorService calculatorService, IMapper mapper,
            ILogger<CalculatorController> log)
        {
            _calculatorService = calculatorService;
            _mapper = mapper;
            _log = log;
        }

        [HttpPost("calculate")]
        public async Task<IActionResult> Calculator(string expression)
        {
            if (string.IsNullOrEmpty(expression) || expression.Contains(','))
            {
                return BadRequest(new Response()
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = "error",
                    Message = "Syntax error!"
                });
            }

            OperationResultViewModel operationResultViewModel;
            try
            {
                var result = await _calculatorService.Calculate(expression);
                if (result.StatusType == StatusType.Error)
                {
                    return BadRequest(new Response()
                    {
                        Code = StatusCodes.Status400BadRequest,
                        Status = "error",
                        Message = result.Message
                    });
                }

                operationResultViewModel = _mapper.Map<OperationResultViewModel>(result.OperationResult);
            }
            catch (Exception e)
            {
                _log.LogError(e.ToString());
                return BadRequest(new Response()
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = "error",
                    Message = "Syntax error!"
                });
            }

            return Ok(new Response()
            {
                Code = StatusCodes.Status200OK,
                Status = "success",
                Message = "The operation was completed correctly",
                Data = operationResultViewModel
            });
        }

        [HttpGet("get_expressions")]
        public async Task<IActionResult> GetExpressions()
        {
            IEnumerable<OperationResultViewModel> operations;

            try
            {
                var result = await _calculatorService.GetAllExpression();
                operations = _mapper.Map<IEnumerable<OperationResultViewModel>>(result);
            }
            catch (SqlException e)
            {
                _log.LogError(e.ToString());
                return BadRequest(new Response()
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = "error",
                    Message = "Server error"
                });
            }

            return Ok(new Response()
            {
                Code = StatusCodes.Status200OK,
                Status = "success",
                Message = "The operation was completed correctly",
                Data = operations
            });
        }
    }
}