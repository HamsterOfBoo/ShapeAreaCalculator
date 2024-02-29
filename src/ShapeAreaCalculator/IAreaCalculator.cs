using System.Runtime.CompilerServices;
using ShapeAreaCalculator.Contract;
[assembly: InternalsVisibleTo("ShapeSquareCalculator.UnitTests")]

namespace ShapeAreaCalculator;

public interface IAreaCalculator
{
    ShapesCalculationResponse CalculateShapeArea(ShapesCalculationRequest request);
}