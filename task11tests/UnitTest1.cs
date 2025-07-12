using Xunit;
using task11;

namespace task11tests
{
    public class CalculatorTests
    {
        private readonly dynamic _calculator;

        public CalculatorTests()
        {
            _calculator = CalculatorGenerator.CreateCalculator();
        }

        [Fact]
        public void Add_ReturnsCorrectAnswer()
            => Assert.Equal(10, _calculator.Add(7, 3));

        [Fact]
        public void Minus_ReturnsCorrectAnswer()
            => Assert.Equal(7, _calculator.Minus(10, 3));

        [Fact]
        public void Mul_ReturnsCorrectAnswer()
            => Assert.Equal(21, _calculator.Mul(7, 3));

        [Fact]
        public void Div_ReturnsCorrectAnswer()
            => Assert.Equal(7, _calculator.Div(21, 3));
    }
}
