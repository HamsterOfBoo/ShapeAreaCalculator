using ShapeAreaCalculator;
using ShapeAreaCalculator.Contract;

namespace ShapeSquareCalculator.IntegrationTests;

public class AreaCalculatorIntegrationTests
{
    [Fact]
    public void TriangleCalculator_Success()
    {
        //arrange
        var areaCalculator = new AreaCalculator(null);
        var request = new ShapesCalculationRequest("Triangle", new[] { 2, 2, 2 });
        
        //act
        var response =  areaCalculator.CalculateShapeArea(request);
        
        //assert
        Assert.NotNull(response);
        Assert.NotNull(response.Area);
        Assert.Null(response.Comments);
        Assert.True(response.IsShapeValid);
    }
    
    [Fact]
    public void CircleCalculator_Success()
    {
        //arrange
        var areaCalculator = new AreaCalculator(null);
        var request = new ShapesCalculationRequest("Circle", new[] { 2 });
        
        //act
        var response =  areaCalculator.CalculateShapeArea(request);
        
        //assert
        Assert.NotNull(response);
        Assert.NotNull(response.Area);
        Assert.Null(response.Comments);
        Assert.True(response.IsShapeValid);
    }
}