namespace SerializingShapes.support;

/// <summary>
/// Represents a circle shape.
/// </summary>
public class Circle : Shape
{
    /// <summary>Gets or sets the colour of the circle.</summary>
    public required string Colour { get; set; }
    
    /// <summary>Gets or sets the radius of the circle.</summary>
    public required double Radius { get; set; }
    
    /// <summary>Gets the area of the circle (calculated as π × radius²).</summary>
    public double Area => Math.Pow(Radius, 2) * Math.PI;
}