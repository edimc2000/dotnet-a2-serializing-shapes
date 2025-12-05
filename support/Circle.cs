namespace SerializingShapes.support;

public class Circle : Shape
{
    public required string Colour { get; set; }

    public required double Radius { get; set; }

    public double Area => Math.Pow(Radius, 2) * Math.PI;
}