using ShapeAreaCalculator.Calculators;
using ShapeAreaCalculator.Contract;
using ShapeAreaCalculator.Factories;

namespace ShapeAreaCalculator;

public class AreaCalculator(IShapeAreaCalculator[]? externalCalculators) : IAreaCalculator
{
    private readonly ShapeCalculatorFactory _factory = new(externalCalculators);
    
    public ShapesCalculationResponse CalculateShapeArea(ShapesCalculationRequest request)
    {
        var concreteCalculator = _factory.CreateShapeCalculator(request);

        var validationResult = concreteCalculator.ValidateShape(request.Edges);

        if (!validationResult.IsShapeValid)
        {
            return validationResult;
        }
        
        return concreteCalculator.CalculateArea(request);
    }
}