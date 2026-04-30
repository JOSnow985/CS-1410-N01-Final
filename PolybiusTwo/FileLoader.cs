namespace PolybiusTwo;

public static class FileLoader
{
    // Methods to load files
    // Loads Core Attribute list from file and returns it
    public static List<Attribute> LoadCoreAttributes()
    {
        List<Attribute> list = [];

        // Attributes are read from core_attributes.csv
        string[] lines = File.ReadAllLines("core_attributes.csv");

        // Split each line by ',', add the two parts to the list as a new Attribute object
        foreach(string line in lines)
        {
            string[] strings = line.Split(',');
            list.Add(new(strings[0], strings[1]));
        }

        return list;
    }
}
