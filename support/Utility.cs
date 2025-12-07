using SerializingShapes.support;
using System.Text.Json;
using System.Xml.Serialization;
using JsonShortcut = System.Text.Json.JsonSerializer;

namespace SerializingShapes;

internal class Utility
{
    internal static List<Shape> CreateListOfShapes()
    {
        // create a list of Shapes for serialization
        return new List<Shape>
        {
            new Circle { Colour = "Red", Radius = 2.5 },
            new Rectangle { Colour = "Blue", Height = 20.0, Width = 10.0 },
            new Circle { Colour = "Green", Radius = 8 },
            new Circle { Colour = "Purple", Radius = 12.44 },
            new Rectangle { Colour = "Blue", Height = 45.0, Width = 18.0 }
        };
    }

    internal static void SerializeAsXml(List<Shape> listOfShapes, string path)
    {
        XmlSerializer serializeShapes = new(listOfShapes.GetType());
        using FileStream stream = File.Create(path);
        serializeShapes.Serialize(stream, listOfShapes);
    }

    internal static void DeserializeXml(string path)
    {
        XmlSerializer serializeShapes = new(typeof(List<Shape>));
        using FileStream xmlLoad = File.Open(path, FileMode.Open);

        List<Shape>? loadedShapesXml = serializeShapes.Deserialize(xmlLoad) as List<Shape>;
        DisplayRequiredInfo(loadedShapesXml!);
    }

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        IncludeFields = true,
        PropertyNameCaseInsensitive = false,
        WriteIndented = true,
        IgnoreReadOnlyProperties = true
    };

    internal static void SerializeAsJson(List<Shape> listOfShapes, string path)
    {
        using Stream fileStream = File.Create(path);
        JsonShortcut.Serialize(fileStream, listOfShapes, JsonOptions);
    }


    internal static void DeserializeJson(string path)
    {
        string jsonString = File.ReadAllText(path);
        List<Shape>? loadedShapesJson = JsonShortcut.Deserialize<List<Shape>>(jsonString);
        DisplayRequiredInfo(loadedShapesJson!);
    }


    internal static void DisplayRequiredInfo(List<Shape> list)
    {
        foreach (Shape item in list!)
        {
            Type type = item.GetType();

            // Handle different shape types with pattern matching
            switch (item)
            {
                case Circle circle:
                    WriteLine($"   {type.Name} is {circle.Colour} and has an area of {circle.Area:F4}");
                    break;
                case Rectangle rectangle:
                    WriteLine($"   {type.Name} is {rectangle.Colour} and has an area of {rectangle.Area:F4}");
                    break;
                default:
                    WriteLine($"   Unknown shape type: {item.GetType().Name}");
                    break;
            }

            // Below are from the documentation that failed extensibility and maintainability 
            //string colour = (string)type.GetProperty("Colour").GetValue(item);
            //double area = (double)type.GetProperty("Area").GetValue(item);
            //WriteLine($"   {type.Name} is {colour} and has an area of {area:F4}");
        }
    }

    internal static void TeardownOnExit(string directory)
    {
        try
        {
            if (Directory.Exists(directory))
            {
                // Optionally ask for confirmation
                Write("   Clean up temporary files? (Y for Yes / Any key for No) : ");
                if (ReadKey().Key == ConsoleKey.Y)
                {
                    Delete(directory, true);
                    WriteLine($"\n   Cleaned up: {GetFileName(directory)}");
                }
            }
        }
        catch (Exception ex)
        {
            WriteLine($"\n   Warning: Could not clean up: {GetFileName(directory)} folder\n" +
                      $"   {ex.Message}");
        }

        WriteLine("\n\n");
    }
}