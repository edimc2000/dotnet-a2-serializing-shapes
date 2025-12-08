using SerializingShapes.support;
using System.Text.Json;
using System.Xml.Serialization;
using JsonShortcut = System.Text.Json.JsonSerializer;

namespace SerializingShapes;

/// <summary>
/// Utility class for shape serialization and helper methods.
/// </summary
/// <remarks>
/// <para>Author: Eddie C.</para>
/// <para>Version: 2.0</para>
/// <para>Since: 2025-12-07</para>
/// </remarks>
internal class Utility
{
    /// <summary>Creates a sample list of shapes for serialization.</summary>
    /// <returns>A list containing Circle and Rectangle objects.</returns>
    internal static List<Shape> CreateListOfShapes()
    {
        return new List<Shape>
        {
            new Circle { Colour = "Red", Radius = 2.5 },
            new Rectangle { Colour = "Blue", Height = 20.0, Width = 10.0 },
            new Circle { Colour = "Green", Radius = 8 },
            new Circle { Colour = "Purple", Radius = 12.44 },
            new Rectangle { Colour = "Blue", Height = 45.0, Width = 18.0 }
        };
    }

    /// <summary>Serializes a list of shapes to XML format. </summary>
    /// <param name="listOfShapes">The list of shapes to serialize.</param>
    /// <param name="path">The file path to save the XML to.</param>
    internal static void SerializeAsXml(List<Shape> listOfShapes, string path)
    {
        XmlSerializer serializeShapes = new(listOfShapes.GetType());
        using FileStream stream = File.Create(path);
        serializeShapes.Serialize(stream, listOfShapes);
    }

    /// <summary>Deserializes shapes from an XML file. </summary>
    /// <param name="path">The file path to load XML from.</param>
    internal static void DeserializeXml(string path)
    {
        XmlSerializer serializeShapes = new(typeof(List<Shape>));
        using FileStream xmlLoad = File.Open(path, FileMode.Open);

        List<Shape>? loadedShapesXml = serializeShapes.Deserialize(xmlLoad) as List<Shape>;
        DisplayRequiredInfo(loadedShapesXml!);
    }

    /// <summary>JSON serializer configuration options.</summary>
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        IncludeFields = true,
        PropertyNameCaseInsensitive = false,
        WriteIndented = true,
        IgnoreReadOnlyProperties = true
    };

    /// <summary>Serializes a list of shapes to JSON format.</summary>
    /// <param name="listOfShapes">The list of shapes to serialize.</param>
    /// <param name="path">The file path to save the JSON to.</param>
    internal static void SerializeAsJson(List<Shape> listOfShapes, string path)
    {
        using Stream fileStream = File.Create(path);
        JsonShortcut.Serialize(fileStream, listOfShapes, JsonOptions);
    }

    /// <summary>Deserializes shapes from a JSON file.</summary>
    /// <param name="path">The file path to load JSON from.</param>
    internal static void DeserializeJson(string path)
    {
        string jsonString = File.ReadAllText(path);
        List<Shape>? loadedShapesJson = JsonShortcut.Deserialize<List<Shape>>(jsonString);
        DisplayRequiredInfo(loadedShapesJson!);
    }

    /// <summary>Displays information about each shape in the list.</summary>
    /// <param name="list">The list of shapes to display.</param>
    internal static void DisplayRequiredInfo(List<Shape> list)
    {
        foreach (Shape item in list!)
        {
            Type type = item.GetType();

            // Handle different shape types with pattern matching
            // this part was improved to make it more maintainable and readable compared to th code
            // from the exercise
            switch (item)
            {
                case Circle circle:
                    WriteLine(
                        $"   {type.Name} is {circle.Colour} and has an area of {circle.Area:F4}");
                    break;
                case Rectangle rectangle:
                    WriteLine(
                        $"   {type.Name} is {rectangle.Colour} and has an area of " +
                        $"{rectangle.Area:F4}");
                    break;
                default:
                    WriteLine($"   Unknown shape type: {item.GetType().Name}");
                    break;
            }
        }
    }

    /// <summary>Cleans up temporary files and directories.</summary>
    /// <param name="directory">The directory to clean up.</param>
    internal static void TeardownOnExit(string directory)
    {
        try
        {
            if (Directory.Exists(directory))
            {
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