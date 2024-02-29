namespace ShapeAreaCalculator.Contract;

public record ShapesCalculationResponse(bool IsShapeValid, double? Area, string? Comments);