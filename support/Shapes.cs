using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace SerializingShapes.support
{
    [XmlInclude(typeof(Circle))]
    [XmlInclude(typeof(Rectangle))]


    [JsonDerivedType(typeof(Circle), typeDiscriminator: "circle")]
    [JsonDerivedType(typeof(Rectangle), typeDiscriminator: "rectangle")]

    public class Shape
    {

        string? Colour { get; set; }
    }
}