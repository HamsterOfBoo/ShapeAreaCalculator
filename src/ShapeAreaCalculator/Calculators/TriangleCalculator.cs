using ShapeAreaCalculator.Contract;

namespace ShapeAreaCalculator.Calculators;

public class TriangleCalculator : IShapeAreaCalculator
{
    public string ShapeType => "Triangle";

    public ShapesCalculationResponse CalculateArea(ShapesCalculationRequest calculationRequest)
    {
        var edges = calculationRequest.Edges;
        
        // right-angled
        if (IsRightAngledTriangle(edges))
        {
            return new ShapesCalculationResponse(
                IsShapeValid: true,
                Area: CalculateRightAngledTriangle(edges),
                Comments: "This shape is right triangle");
        }

        var distinctAngles = edges.Distinct().ToArray();
        
        // isosceles
        if (distinctAngles.Length == 2)
        {
            return new ShapesCalculationResponse(
                IsShapeValid: true,
                Area: CalculateIsoscelesTriangle(edges),
                Comments: null);
        }
        
        //Equilateral
        if (distinctAngles.Length == 1)
        {
            return new ShapesCalculationResponse(
                IsShapeValid: true,
                Area: CalculateEquilateralTriangle(edges),
                Comments: null);
        }

        // Heron for others
        return new ShapesCalculationResponse(
            IsShapeValid: true,
            Area: CalculateTriangleByHeron(edges),
            Comments: null);
    }
    
    #region CalculationSupports Equilateral
    
    private double CalculateTriangleByHeron(int[] edges)
    {
        var semiPerimeter = CalculateSemiPerimeter(edges);
        return Math.Sqrt(
            semiPerimeter
            * (semiPerimeter - edges[0])
            * (semiPerimeter - edges[1])
            * (semiPerimeter - edges[2]));
    }
    
    
    private double CalculateEquilateralTriangle(int[] edges)
    {
        var semiPerimeter = CalculateSemiPerimeter(edges);
        var bracketValue = semiPerimeter - edges[0];
        return Math.Sqrt(semiPerimeter * bracketValue * bracketValue);
    }
    
    private double CalculateRightAngledTriangle(int[] edges)
    {
        var legs = edges.Where(x => x != edges.Max()).ToArray();
        return 0.5 * legs[0] * legs[1];
    }
    
    private double CalculateIsoscelesTriangle(int[] edges)
    {
        var semiPerimeter = CalculateSemiPerimeter(edges);
        var bracketValue = semiPerimeter - edges[0];
        return Math.Sqrt(semiPerimeter * bracketValue * bracketValue);
    }

    private double CalculateSemiPerimeter(int[] edges)
    {
        return ((long)edges[0] + edges[1] + edges[2]) * 0.5;
    }
    
    private bool IsRightAngledTriangle(int[] edges)
    {
        var hypotenuse = edges.Max();
        var hypotenuseIndex = Array.IndexOf(edges, hypotenuse);
        var legs = GetLegs(edges, hypotenuseIndex).ToArray();

        return hypotenuse * hypotenuse == legs[0] * legs[0] + legs[1] * legs[1];
    }
    
    private IEnumerable<int> GetLegs(int[] edges, int hypotenuseIndex)
    {
        for (int i = 0; i < edges.Length; i++)
        {
            if (i != hypotenuseIndex)
                yield return edges[i];
        }
    }

    private IEnumerable<int> GetNegativeEdgeIndexes(int[] edges)
    {
        for (var i = 0; i < edges.Length; i++)
        {
            if (edges[i] <= 0)
            {
                yield return i;
            }
        }
    }
    
    #endregion

    public ShapesCalculationResponse ValidateShape(int[] edges)
    {
        if (IsTreeEdges(edges.Length))
        {
            return new ShapesCalculationResponse(
                IsShapeValid: false,
                Area: null,
                Comments: "This shape is not Triangle.");
        }

        var negativeEdges = GetNegativeEdgeIndexes(edges).ToArray();
        if (negativeEdges.Length > 0)
        {
            return new ShapesCalculationResponse(
                IsShapeValid: false,
                Area: null,
                Comments: $"The length for edge with indexes {string.Join(", ", negativeEdges)} is zero or negative.");
        }

        if (IsTriangleMalformed(edges))
        {
            return new ShapesCalculationResponse(
                IsShapeValid: false,
                Area: null,
                Comments: "This triangle is malformed.");
        }

        return new ShapesCalculationResponse(
            IsShapeValid: true,
            Area: null,
            Comments: null);
    }

    #region ValidationSupports

    private bool IsTreeEdges(int edgesCount)
    {
        return edgesCount != 3;
    }

    private bool IsTriangleMalformed(int[] edges)
    {
        return !(edges[0] + edges[1] > edges[2] && edges[0] + edges[2] > edges[1] && edges[1] + edges[2] > edges[0]);
    }

    #endregion
}