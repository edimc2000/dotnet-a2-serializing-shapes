namespace SerializingShapes.support;

/// <summary>
/// Represents a rectangle shape.
/// </summary>
public class Rectangle : Shape
{
    /// <summary>Gets or sets the colour of the rectangle.</summary>
    public required string Colour { get; set; }

    /// <summary>Gets or sets the height of the rectangle.</summary>
    public required double Height { get; set; }

    /// <summary>Gets or sets the width of the rectangle.</summary>
    public required double Width { get; set; }

    /// <summary>Gets the area of the rectangle (calculated as height × width).</summary>
    public double Area => Height * Width;
}