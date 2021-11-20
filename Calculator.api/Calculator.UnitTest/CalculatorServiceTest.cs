using System;
using System.Threading.Tasks;
using AutoMapper;
using Calculator.BLL.Abstract;
using Calculator.BLL.Model;
using Calculator.BLL.Services;
using Calculator.BLL.utils;
using Calculator.DAL.Abstract;
using Calculator.DAL.Entity;
using Calculator.DAL.Factory;
using Calculator.DAL.Repository;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using NUnit.Framework;

namespace Calculator.UnitTest
{
    public class CalculatorServiceTest
    {
        private ICalculatorService _calculatorService;

        [SetUp]
        public void Setup()
        {
            string ConnectionString =
                "Server=(LocalDb)\\MSSQLLocalDB;Database=calculator_db;Trusted_Connection=True;MultipleActiveResultSets=true";
            IContextFactory contextFactory = new AppDbContextFactory();
            ILogger<GenericRepository> loggerRepo = new NullLogger<GenericRepository>();
            IRepository repository = new GenericRepository(ConnectionString, contextFactory, loggerRepo);

            IParser parser = new Parser();
            ILogger<BLL.utils.Calculator> logger = new NullLogger<BLL.utils.Calculator>();
            ICalculator calculator = new BLL.utils.Calculator(parser, logger);

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<OperationResult, OperationResult>();
                cfg.CreateMap<OperationResult, Expression>();
                cfg.CreateMap<Expression, OperationResult>();
            });
            IMapper mapper = new Mapper(config);
            ILogger<CalculatorService> loggerService = new NullLogger<CalculatorService>();
            _calculatorService = new CalculatorService(repository, mapper, calculator, loggerService);
        }

        [Test]
        public async Task Test1()
        {
            // arrange
            string expression = "1.1*1.1";
            Double result = 1.1 * 1.1;

            // act
            StatusResult calcResult = await _calculatorService.Calculate(expression);

            // assert
            Assert.AreEqual(result, calcResult.OperationResult.Result);
        }

        [Test]
        public async Task Test2()
        {
            // arrange
            string expression = "2.341+6.235";
            Double result = 2.341 + 6.235;

            // act
            StatusResult calcResult = await _calculatorService.Calculate(expression);

            // assert
            Assert.AreEqual(result, calcResult.OperationResult.Result);
        }

        [Test]
        public async Task Test3()
        {
            // arrange
            string expression = "(2.341+6.235^3.76)*(2.56-1.73)/5.54";
            Double result = (2.341 + Math.Pow(6.235, 3.76)) * (2.56 - 1.73) / 5.54;

            // act
            StatusResult calcResult = await _calculatorService.Calculate(expression);

            // assert
            Assert.AreEqual(result, calcResult.OperationResult.Result);
        }
    }
}