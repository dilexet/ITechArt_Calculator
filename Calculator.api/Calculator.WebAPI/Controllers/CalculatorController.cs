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
using Serilog;

// ReSharper disable TemplateIsNotCompileTimeConstantProblem

namespace Calculator.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CalculatorController : ControllerBase
    {
        private readonly ICalculatorService _calculatorService;
        private readonly IMapper _mapper;


        public CalculatorController(ICalculatorService calculatorService, IMapper mapper)
        {
            _calculatorService = calculatorService;
            _mapper = mapper;
        }

        [HttpPost("calculate")]
        public async Task<IActionResult> Calculator(string expression)
        {
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
                        Message = result.Result
                    });
                }

                operationResultViewModel = _mapper.Map<OperationResultViewModel>(result.OperationResult);
            }
            catch (Exception e)
            {
                Log.Error(e.ToString());
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
                Log.Error(e.ToString());
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