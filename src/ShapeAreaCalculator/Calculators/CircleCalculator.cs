using ShapeAreaCalculator.Contract;

namespace ShapeAreaCalculator.Calculators;

internal class CircleCalculator : IShapeAreaCalculator
{
    public string ShapeType => "Circle";
    public ShapesCalculationResponse CalculateArea(ShapesCalculationRequest calculationRequest)
    {
        var radius = calculationRequest.Edges[0];
        var area = Math.PI * radius * radius;
        
        return new ShapesCalculationResponse(
            IsShapeValid: true,
            Area: area,
            Comments: null);
    }
    
    public ShapesCalculationResponse ValidateShape(int[] edges)
    {
        if (edges.Length > 1 || edges[0] < 0)
        {
            return new ShapesCalculationResponse(
                IsShapeValid: false,
                Area: null,
                Comments: "This shape is not Circle or their radius is less than zero.");
        }

        return new ShapesCalculationResponse(
            IsShapeValid: true,
            Area: null,
            Comments: null);
    }
}