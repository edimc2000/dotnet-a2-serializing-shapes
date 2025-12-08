using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace SerializingShapes.support;

// AI was used to solve the serialization bug where the XML and JSON files are
// created but there are no contents
[XmlInclude(typeof(Circle))]
[XmlInclude(typeof(Rectangle))]

[JsonDerivedType(typeof(Circle), "circle")]
[JsonDerivedType(typeof(Rectangle), "rectangle")]

/// <summary>
/// Abstract base class for all shapes.
/// Contains common properties and polymorphic serialization support.
/// </summary>
public class Shape
{
    /// <summary>Gets or sets the colour of the shape.</summary>
    private string? Colour { get; set; }
}