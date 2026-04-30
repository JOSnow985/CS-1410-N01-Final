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

    // Loads all the core class files present in the core class folder
    public static List<CharClass> LoadCoreClasses()
    {
        List<CharClass> classList = [];

        // Build the path to the class folder
        string currentDir = Directory.GetCurrentDirectory();
        string coreClassFolder = "GameCore/CoreClasses";

        // Get an array of the files in the class folder
        string[] files = Directory.GetFiles(Path.Combine(currentDir, coreClassFolder));

        // Create a class from every class file in the directory
        foreach(string file in files)
        {
            string[] lines = LoadFile(file);

            List<string[]> lineList = [];

            foreach(string line in lines)
                lineList.Add(line.Split(','));
            
            // Using query expressions to parse the lines from the file based on the first string, the identifier
            var name =  from x in lineList
                        where x[0] == "Name"        && x.Length > 1     // Make sure we're getting lines that aren't just an identifier
                        select x;
            var desc =  from x in lineList
                        where x[0] == "Description" && x.Length > 1
                        select x;
            var hitD =  from x in lineList
                        where x[0] == "HitDie"      && x.Length > 1
                        select x;
            
            // Grab the first entry from the enumerables and use the second string in the array, the value
            classList.Add(new(name.First()[1], desc.First()[1], hitD.First()[1]));
        }

        return classList;
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
