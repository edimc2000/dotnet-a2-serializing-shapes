using static SerializingShapes.AnsiColorCodes;
using SerializingShapes.support;
using System.Text.Json;
using JsonShortcut = System.Text.Json.JsonSerializer;
using System.Xml.Serialization;

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

    internal static void DeserializeXml(List<Shape> listOfShapes, string path)
    {
        XmlSerializer serializeShapes = new(listOfShapes.GetType());
        using FileStream xmlLoad = File.Open(path, FileMode.Open);

        List<Shape>? loadedShapesXml = serializeShapes.Deserialize(xmlLoad) as List<Shape>;
        DisplayRequiredInfo(loadedShapesXml!);
    }

    internal static void SerializeAsJson(List<Shape> listOfShapes, string path)
    {
        JsonSerializerOptions options = new()
        {
            IncludeFields = true, // Includes all fields.
            PropertyNameCaseInsensitive = false,
            WriteIndented = true,
            //PropertyNamingPolicy = JsonNamingPolicy.PascalCase,
            IgnoreReadOnlyProperties = true
        };

        using Stream fileStream = File.Create(path);
        JsonShortcut.Serialize(fileStream, listOfShapes, options);
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
            string name = type.Name;
            string colour = (string)type.GetProperty("Colour").GetValue(item);
            double area = (double)type.GetProperty("Area").GetValue(item);
            WriteLine($"   {name} is {colour} and has an area of {area:F4}");
        }
    }

    internal static void TeardownOnExit(string directory)
    {
        try
        {
            if (Directory.Exists(directory))
            {
                // Optionally ask for confirmation
                Write("   Clean up temporary files? (Y/N): ");
                if (Console.ReadKey().Key == ConsoleKey.Y)
                {
                    Directory.Delete(directory, recursive: true);
                    WriteLine($"\n   Cleaned up: {Path.GetFileName(directory)}");
                }
            }
        }
        catch (Exception ex)
        {
            WriteLine($"\n   Warning: Could not clean up: {Path.GetFileName(directory)} folder\n" +
                      $"   {ex.Message}");
        }
        WriteLine("\n\n");
    }
}