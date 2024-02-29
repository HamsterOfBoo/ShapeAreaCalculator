using ShapeAreaCalculator.Contract;

namespace ShapeAreaCalculator.Calculators;

public interface IShapeAreaCalculator
{
    string ShapeType { get; }
    
    ShapesCalculationResponse CalculateArea(ShapesCalculationRequest calculationRequest);

    ShapesCalculationResponse ValidateShape(int[] edges);
}