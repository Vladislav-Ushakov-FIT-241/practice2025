using Xunit;
using task14;

public class DefiniteIntegralTests
{
    [Fact]
    public void Solve_LinFunction_SymmetricInterval()
    {
        var X = (double x) => x;
        Assert.Equal(0, DefiniteIntegral.Solve(-1, 1, X, 1e-4, 2), 1e-4);
    }

    [Fact]
    public void Solve_SinFunction_SymmetricInterval()
    {
        var SIN = (double x) => Math.Sin(x);
        Assert.Equal(0, DefiniteIntegral.Solve(-1, 1, SIN, 1e-5, 8), 1e-4);
    }

    [Fact]
    public void Solve_LinFunction_PositiveInterval()
    {
        var X = (double x) => x;
        Assert.Equal(12.5, DefiniteIntegral.Solve(0, 5, X, 1e-6, 8), 1e-5); //исправил 10 на 12.5, тк x^2/2 от 0 до 5 = (5^2)/2 - (0^2)/2 = 25/2 - 0 = 12.5
    }

    [Fact]
    public void Solve_ConstFunction()
    {
        var CONSTANT_FIVE = (double x) => 5.0;
        Assert.Equal(50.0, DefiniteIntegral.Solve(0, 10, CONSTANT_FIVE, 1e-3, 4), 1e-3);
    }

    [Fact]
    public void Solve_QuadrFunction()
    {
        var X_SQUARED = (double x) => x * x;
        Assert.Equal(9.0, DefiniteIntegral.Solve(0, 3, X_SQUARED, 1e-4, 6), 1e-3);
    }
}
