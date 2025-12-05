using SerializingShapes.support;
using System.Xml.Serialization;

namespace SerializingShapes
{
    [XmlInclude(typeof(Circle))]
    [XmlInclude(typeof(Rectangle))]
    public class Shape
    {

        string? Colour { get; set; }
    }
}