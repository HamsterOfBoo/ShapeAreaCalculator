using ShapeAreaCalculator.Calculators;
using ShapeAreaCalculator.Contract;

namespace ShapeSquareCalculator.UnitTests;

public class CircleTest
{
    [Fact]
    public void CalculateCircleArea__Success()
    {
        //arrange
        var calculator = new CircleCalculator();
        var radius = 25;
        var request = new ShapesCalculationRequest("Circle", new[] { 25 });
        var actual = Math.PI * radius * radius;

        //act
        var response = calculator.CalculateArea(request);

        //assert
        Assert.True(response.IsShapeValid);
        Assert.Null(response.Comments);
        Assert.Equal(actual, response.Area);
    }
    
    
    [Fact]
    public void ValidateCircle_NegativeRadius_NotValid()
    {
        //arrange
        var calculator = new CircleCalculator();
        var edges = new[] { -25 };
        var actualMessage = "This shape is not Circle or their radius is less than zero.";

        //act
        var response = calculator.ValidateShape(edges);

        //assert
        Assert.False(response.IsShapeValid);
        Assert.Null(response.Area);
        Assert.Equal(actualMessage, response.Comments);
    }
    
    [Fact]
    public void ValidateCircle__Valid()
    {
        //arrange
        var calculator = new CircleCalculator();
        var edges = new[] { 25 };

        //act
        var response = calculator.ValidateShape(edges);

        //assert
        Assert.True(response.IsShapeValid);
        Assert.Null(response.Area);
        Assert.Null(response.Comments);
    }
}