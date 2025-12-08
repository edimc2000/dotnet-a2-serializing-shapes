using static SerializingShapes.Formatting;
using static SerializingShapes.Utility;
using SerializingShapes.support;

namespace SerializingShapes;

/// <summary>
/// Main program class for serializing shapes.
/// </summary>
/// <remarks>
/// <para>Author: Eddie C.</para>
/// <para>Version: 2.0</para>
/// <para>Since: 2025-12-07</para>
/// </remarks>
internal class Program
{
    /// <summary>Directory name for output files.</summary>
    private const string OutputDirectory = "Output";

    /// <summary>Filename for XML output.</summary>
    private const string XmlFileName = "shapes.xml";
    
    /// <summary>Filename for JSON output.</summary>
    private const string JsonFileName = "shapes.json";
    
    
    /// <summary>
    /// Main entry point for Shape Serialization.
    /// Usage: SerializingShapes [json|xml]
    /// Default: XML serialization
    /// </summary>

    private static void Main(string[] args)
    {
        Clear();

        // setting up the output directory
        string dir = Combine(CurrentDirectory, OutputDirectory);
        CreateDirectory(dir);

        // Create full file paths
        string fullPathXmlFile = Combine(dir, XmlFileName); //file
        string fullPathJsonFile = Combine(dir, JsonFileName); //file

        // Create sample shapes
        List<Shape> listOfShapes = CreateListOfShapes();

        // Display title
        DisplayTitle("Serialization and Deserialization", "all", 80);
        
        // Check for JSON argument
        // Any argument that's not "json" defaults to XML
        if (args.Length > 0 && args[0].ToLower().Equals("json"))
        {
            // JSON - not part of the requirements 
            SectionTitle($"Serializing as JSON... and saving the file \"Output\\{JsonFileName}\"");
            SerializeAsJson(listOfShapes, fullPathJsonFile);

            SectionTitle("Loading shapes from a JSON file");
            DeserializeJson(fullPathJsonFile);
        }
        else 
        {
            // XML serialization
            // Default to XML (includes explicit "xml" or any other argument)
            SectionTitle($"Serializing as XML... and saving the a file \"Output\\{XmlFileName}\"");
            SerializeAsXml(listOfShapes, fullPathXmlFile);

            SectionTitle("Loading shapes from XML");
            DeserializeXml(fullPathXmlFile);
        }

        // Teardown/cleanup 
        SectionTitle("Cleaning up files and folders ...");
        TeardownOnExit(dir);
    }
}