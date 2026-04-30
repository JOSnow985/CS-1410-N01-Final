namespace PolybiusTwo;

public static class FileLoader
{
    // Methods to load files
    // Loads core_attributes.csv and parses it into a list of Attribute objects to return
    public static List<Attribute> LoadCoreAttributes()
    {
        List<Attribute> list = [];
        string[] lines = LoadFile("GameCore/core_attributes.csv");

        // Split each line by ',', add the two parts to the list as a new Attribute object
        foreach(string line in lines)
        {
            string[] strings = line.Split(',');
            list.Add(new(strings[0], strings[1]));
        }

        return list;
    }

    // Loads the indicated file, returns an empty array if the file's missing
    public static string[] LoadFile(string filepath)
    {
        // Basic catch for a missing file
        string[] lines;
        try
        {
            // Attributes are read from core_attributes.csv
            lines = File.ReadAllLines(filepath);
        }
        catch (FileNotFoundException)
        {
            lines = [];
        }

        return lines;
    }
}
