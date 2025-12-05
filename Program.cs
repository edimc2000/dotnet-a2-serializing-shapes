using static  SerializingShapes.Utility;  
using System.Xml.Serialization;

namespace SerializingShapes;

internal class Program
{
    private static void Main(string[] args)
    {
        // create a list of Shapes to serialize
        List<Shape> listOfShapes = new()
        {
            new Circle { Colour = "Red", Radius = 2.5 },
            new Rectangle { Colour = "Blue", Height = 20.0, Width = 10.0 },
            new Circle { Colour = "Green", Radius = 8 },
            new Circle { Colour = "Purple", Radius = 12.44 },
            new Rectangle { Colour = "Blue", Height = 45.0, Width = 18.0 }
        };

        string dir = Combine(CurrentDirectory, "OutputFilesForShapesSerialization"); //folder
        CreateDirectory(dir);
        
        SectionTitle("Serializing as XML");
        XmlSerializer serializeShapes = new(listOfShapes.GetType());
        string xmlFileSerial = Combine(dir, "shapesSerial.xml"); //file
        using (FileStream stream = File.Create(xmlFileSerial))
        {
            serializeShapes.Serialize(stream, listOfShapes);
        }

        SectionTitle("Loading shapes from XML:");
        using (FileStream xmlLoad = File.Open(xmlFileSerial, FileMode.Open))
        {
            List<Shape>? loadedShapesXml = serializeShapes.Deserialize(xmlLoad) as List<Shape>;


            foreach (Shape item in loadedShapesXml)
            {
                Type? type = item.GetType();
                string? name = type.Name;
                string? colour = (string)type.GetProperty("Colour").GetValue(item);
                double? area = (double)type.GetProperty("Area").GetValue(item);

                WriteLine($"{name} is {colour} and has an area of {area:F4}");
            }
        }
    }
}