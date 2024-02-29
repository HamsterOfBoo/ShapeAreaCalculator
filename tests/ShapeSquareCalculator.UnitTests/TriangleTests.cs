using ShapeAreaCalculator.Calculators;
using ShapeAreaCalculator.Contract;

namespace ShapeSquareCalculator.UnitTests;

public class TriangleTests
{
    [Theory]
    [InlineData(new[] { 3, 4, 5 }, "This shape is right triangle", 6)]
    [InlineData(new[] { 2, 2, 4 }, null, 4)]
    [InlineData(new[] { 3, 3, 3 }, null, 3)]
    [InlineData(new[] { 4, 5, 6 }, null, 10)]
    public void CalculateTriangleArea__Success(int[] edges, string? expectedMessage, double result)
    {
        //arrange
        var calculator = new TriangleCalculator();
        var request = new ShapesCalculationRequest("Triangle", edges);

        //act
        var response = calculator.CalculateArea(request);

        //assert
        Assert.True(response.IsShapeValid);
        Assert.Equal(result, Math.Round(response.Area!.Value, 0));
        Assert.Equal(expectedMessage, response.Comments);
    }
    
    [Fact]
    public void ValidateTriangle__Valid()
    {
        //arrange
        var calculator = new TriangleCalculator();
        var edges = new[] { 6, 8, 10 };

        //act
        var response = calculator.ValidateShape(edges);

        //assert
        Assert.True(response.IsShapeValid);
        Assert.Null(response.Area);
        Assert.Null(response.Comments);
    }
    
    [Fact]
    public void ValidateTriangle_NotTriangle_NotValid()
    {
        //arrange
        var calculator = new TriangleCalculator();
        var edges = new[] { -25 };
        var expectedMessage = "This shape is not Triangle.";

        //act
        var response = calculator.ValidateShape(edges);

        //assert
        Assert.False(response.IsShapeValid);
        Assert.Null(response.Area);
        Assert.Equal(expectedMessage, response.Comments);
    }
    
    [Theory]
    [InlineData(new[] { 25, 14, -1 }, "The length for edge with indexes 2 is zero or negative.")]
    [InlineData(new[] { 25, -14, -1 }, "The length for edge with indexes 1, 2 is zero or negative.")]
    [InlineData(new[] { 0, -14, -1 }, "The length for edge with indexes 0, 1, 2 is zero or negative.")]
    public void ValidateTriangle_NegativeEdges_NotValid(int[] edges, string expectedMessage)
    {
        //arrange
        var calculator = new TriangleCalculator();

        //act
        var response = calculator.ValidateShape(edges);

        //assert
        Assert.False(response.IsShapeValid);
        Assert.Null(response.Area);
        Assert.Equal(expectedMessage, response.Comments);
    }
    
    [Fact]
    public void ValidateTriangle_MalformedTangle_NotValid()
    {
        //arrange
        var calculator = new TriangleCalculator();
        var edges = new[] { 25, 40, 1 };
        var actualMessage = "This triangle is malformed.";

        //act
        var response = calculator.ValidateShape(edges);

        //assert
        Assert.False(response.IsShapeValid);
        Assert.Null(response.Area);
        Assert.Equal(actualMessage, response.Comments);
    }
}