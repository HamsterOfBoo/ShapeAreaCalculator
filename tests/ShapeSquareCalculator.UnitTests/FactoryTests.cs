using ShapeAreaCalculator.Calculators;
using ShapeAreaCalculator.Contract;
using ShapeAreaCalculator.Factories;

namespace ShapeSquareCalculator.UnitTests;

public class FactoryTests
{
    [Fact]
    public void CreateCalculator_Circle_CorrectImplementation()
    {
        //arrange
        var factory = new ShapeCalculatorFactory(null);
        var request = new ShapesCalculationRequest("Circle", new[] { 1 });
        
        //act
        var calculator = factory.CreateShapeCalculator(request);

        //assert
        Assert.NotNull(calculator);
        Assert.Equal(typeof(CircleCalculator), calculator.GetType());
    }
    
    [Fact]
    public void CreateCalculator_Triangle_CorrectImplementation()
    {
        //arrange
        var factory = new ShapeCalculatorFactory(null);
        var request = new ShapesCalculationRequest("Triangle", new[] { 1, 2, 3 });
        
        //act
        var calculator = factory.CreateShapeCalculator(request);

        //assert
        Assert.NotNull(calculator);
        Assert.Equal(typeof(TriangleCalculator), calculator.GetType());
    }
}