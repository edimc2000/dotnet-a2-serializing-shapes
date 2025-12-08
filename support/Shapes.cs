using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace SerializingShapes.support;

/// <summary>
/// Abstract base class for all shapes.
/// Contains common properties and polymorphic serialization support.
/// </summary>
[XmlInclude(typeof(Circle))]
[XmlInclude(typeof(Rectangle))]
[JsonDerivedType(typeof(Circle), "circle")]
[JsonDerivedType(typeof(Rectangle), "rectangle")]
public class Shape
{
    /// <summary>Gets or sets the colour of the shape.</summary>
    private string? Colour { get; set; }
}