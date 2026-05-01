using System.Runtime.InteropServices;

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

    // Loads all Character Files found and reconstructs characters into a list
    public static List<Character> LoadCharacterFiles()
    {
        // Build the path to the class folder
        string characterFolder = Path.Combine(Directory.GetCurrentDirectory(), "CharacterFiles");

        // If the save directory exists, iterate over it and load every character file we find
        if (Directory.Exists(characterFolder))
        {
            // Get an array of the files in the folder
            string[] files = Directory.GetFiles(characterFolder);

            List<Character> characterList = [];

            // Iterate over every retrieved file to construct a list of character objects
            foreach (string file in files)
            {
                // Load file and divide each comma-separated line into strings
                string[] lines = LoadFile(file);
                List<string[]> lineList = DivideLines(lines);

                // Construct a list of identifiers we'll need to look for
                List<string> playerIdentifiers = ["Name", "Description", "CharClass", "Level"];
                playerIdentifiers.AddRange(GameCore.AttributeIdentifiers);

                // Use query expressions to match the identifiers we need to reconstruct the character we're loading
                Dictionary<string, string> playerValues = ProcessLines(lineList, playerIdentifiers);

                // Parse Ability Scores from dictionary
                List<(Attribute, int)> loadedAbilityScores = LoadAbilityScores(playerValues);

                // Parse CharClass from dictionary or use a blank entry to indicate we couldn't find it
                CharClass loadedClass = LoadCharClass(playerValues["CharClass"]) ?? new("CLASS NOT FOUND", "", "");

                // Parse Level from string to int, will be 0 if the parse fails
                Int32.TryParse(playerValues["Level"], out int loadedLevel);

                characterList.Add(new(playerValues["Name"], playerValues["Description"], loadedClass, loadedAbilityScores, loadedLevel));
            }
            return characterList;
        }
        // If the directory isn't found, just return an empty list
        else
            return [];
    }

    // Loads all core classes found and constructs a CharClass list
    public static List<CharClass> LoadCoreClasses()
    {
        // Build the path to the class folder
        string coreClassFolder = Path.Combine(Directory.GetCurrentDirectory(), "GameCore", "CoreClasses");

        if (Directory.Exists(coreClassFolder))
        {
            // Get an array of the files in the class folder
            string[] files = Directory.GetFiles(coreClassFolder);

            // Create a class from every class file in the directory
            List<CharClass> classList = [];
            foreach (string file in files)
            {
                string[] lines = LoadFile(file);

                List<string[]> lineList = [];

                foreach (string line in lines)
                    lineList.Add(line.Split(','));

                Dictionary<string, string> classValues = ProcessLines(lineList, ["Name", "Description", "HitDie"]);

                classList.Add(new(classValues["Name"], classValues["Description"], classValues["HitDie"]));
            }

            return classList;
        }
        // If the directory isn't found, just return an empty list
        else
            return [];
    }

    // Uses a dictionary and the CoreAttributes list to reconstruct a player's (ability, score) list
    private static List<(Attribute, int)> LoadAbilityScores(Dictionary<string, string> playerValues)
    {
        // Reconstruct ability list
        List<(Attribute, int)> loadedAbilityScores = [];

        foreach (Attribute attr in GameCore.CoreAttributes)
        {
            // Use the key from the dictionary or a zero, a zero ability score would catch my attention
            string stringScore = playerValues.ContainsKey(attr.Name) ? playerValues[attr.Name] : stringScore = "0";

            // Parse stringScore into an int or a 0 if it went wrong
            Int32.TryParse(stringScore, out int score);
            loadedAbilityScores.Add((attr, score));
        }

        return loadedAbilityScores;
    }

    // Finds the matching CharClass with a provided string for Name
    private static CharClass? LoadCharClass(string className)
    {
        CharClass? loadedClass = null;

        foreach (CharClass cc in GameCore.CoreClasses)
            if (className == cc.Name)
                loadedClass = cc;

        return loadedClass;
    }

    // Divides each comma-separated string in passed array into strings
    private static List<string[]> DivideLines(string[] lines)
    {
        List<string[]> lineList = [];

        foreach (string line in lines)
            lineList.Add(line.Split(','));

        return lineList;
    }

    // Repeatedly queries a list of string arrays based on a passed list of identifiers to build a dictionary
    public static Dictionary<string,string> ProcessLines(List<string[]> lines, List<string> identifiers)
    {
        Dictionary<string, string> extracted = [];
        foreach (string identifier in identifiers)
        {
            (string key, string val) = QueryListForString(lines, identifier);
            extracted.Add(key, val);
        }
        return extracted;
    }

    // Queries a passed list of string arrays to return a tuple, the ID and the Value
    public static (string key, string val) QueryListForString(List<string[]> list, string target)
    {
        // Only use the first entry found, files should only have one entry to find
        return (from line in list
                where line[0] == target && line.Length > 1
                select (line[0], line[1])).FirstOrDefault((target, "KEY NOT FOUND IN FILE"));
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
