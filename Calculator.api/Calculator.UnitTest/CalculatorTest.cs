using System;
using Calculator.BLL.Abstract;
using Calculator.BLL.utils;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using NUnit.Framework;

namespace Calculator.UnitTest
{
    public class CalculatorTest
    {
        private ICalculator _calculator;

        [SetUp]
        public void Setup()
        {
            IParser parser = new Parser();
            ILogger<BLL.utils.Calculator> logger = new NullLogger<BLL.utils.Calculator>();
            _calculator = new BLL.utils.Calculator(parser, logger);
        }

        [Test]
        public void Test1()
        {
            // arrange
            string expression = "1.1*1.1";
            Double result = 1.1 * 1.1;

            // act
            Double calcResult = _calculator.Calculate(expression);

            // assert
            Assert.AreEqual(result, calcResult);
        }

        [Test]
        public void Test2()
        {
            // arrange
            string expression = "2.341+6.235";
            Double result = 2.341 + 6.235;

            // act
            Double calcResult = _calculator.Calculate(expression);

            // assert
            Assert.AreEqual(result, calcResult);
        }

        [Test]
        public void Test3()
        {
            // arrange
            string expression = "(2.341+6.235^3.76)*(2.56-1.73)/5.54";
            Double result = (2.341 + Math.Pow(6.235, 3.76)) * (2.56 - 1.73) / 5.54;

            // act
            Double calcResult = _calculator.Calculate(expression);

            // assert
            Assert.AreEqual(result, calcResult);
        }
    }
}