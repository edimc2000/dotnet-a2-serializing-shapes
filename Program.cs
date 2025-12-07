using static SerializingShapes.Formatting;
using static SerializingShapes.Utility;
using SerializingShapes.support;

namespace SerializingShapes;

internal class Program
{
    private const string OutputDirectory = "Output";
    private const string XmlFileName = "shapes.xml";
    private const string JsonFileName = "shapes.json";
    
    
    /// <summary>
    /// Main entry point for Shape Serialization.
    /// Usage: SerializingShapes [json|xml]
    /// Default: XML serialization
    /// </summary>
    private static void Main(string[] args)
    {
        Clear();

        // setting up the output directory and files
        string dir = Combine(CurrentDirectory, OutputDirectory);
        CreateDirectory(dir);
        
        string fullPathXmlFile = Combine(dir, XmlFileName); //file
        string fullPathJsonFile = Combine(dir, JsonFileName); //file

        // create a list of Shapes to serialize
        List<Shape> listOfShapes = CreateListOfShapes();


        DisplayTitle("Serialization and Deserialization", "all", 80);
        
        // Any argument that's not "json" defaults to XML
        if (args.Length > 0 && args[0].ToLower().Equals("json"))
        {
            // JSON - not part of the requirements 
            SectionTitle($"Serializing as JSON... and saving the file \"Output\\{JsonFileName}\"");
            SerializeAsJson(listOfShapes, fullPathJsonFile);

            SectionTitle("Loading shapes from a JSON file");
            DeserializeJson(fullPathJsonFile);
        }
        else // Default to XML (includes explicit "xml" or any other argument)
        {
            SectionTitle($"Serializing as XML... and saving the a file \"Output\\{XmlFileName}\"");
            SerializeAsXml(listOfShapes, fullPathXmlFile);

            SectionTitle("Loading shapes from XML");
            DeserializeXml(fullPathXmlFile);
        }

        SectionTitle("Cleaning up files and folders ...");
        TeardownOnExit(dir);
    }
}