using ShapeAreaCalculator.Calculators;
using ShapeAreaCalculator.Contract;

namespace ShapeAreaCalculator.Factories;

internal class ShapeCalculatorFactory
{
    private readonly Dictionary<string, IShapeAreaCalculator> _calculators;

    public ShapeCalculatorFactory(IShapeAreaCalculator[]? externalCalculators)
    {
        _calculators = MapInnerCalculators();
        AddAdditionalCalculatorsToMap(externalCalculators);
    }

    internal IShapeAreaCalculator CreateShapeCalculator(ShapesCalculationRequest request)
    {
        var shapeType = request.ShapeType;
        
        if (!_calculators.TryGetValue(shapeType, out var concreteCalculator))
        {
            throw new NotImplementedException($"A calculator for \"{shapeType}\" is not implemented");
        }

        return concreteCalculator;
    }

    private Dictionary<string, IShapeAreaCalculator> MapInnerCalculators()
    {
        var circleCalculator = new CircleCalculator();
        var triangleCalculator = new TriangleCalculator();

        var map = new Dictionary<string, IShapeAreaCalculator>
        {
            { circleCalculator.ShapeType, circleCalculator },
            { triangleCalculator.ShapeType, triangleCalculator }
        };

        return map;
    }

    private void AddAdditionalCalculatorsToMap(IShapeAreaCalculator[]? externalCalculators)
    {
        if (externalCalculators is { Length: > 0 })
        {
            foreach (var calculator in externalCalculators)
            {
                if (!_calculators.TryAdd(calculator.ShapeType, calculator))
                {
                    //use external calculator if the same is existing 
                    _calculators.Remove(calculator.ShapeType);
                    _calculators.Add(calculator.ShapeType, calculator);
                }
            }
        }
    }
}