namespace PolybiusTwo;

public static class GameCore
{
    // --- Fields ---
    // These are lists of core objects we'll reference from other places, loaded from files
    private static readonly List<Attribute> coreAttributes = FileLoader.LoadCoreAttributes();
    private static readonly List<CharClass> coreClasses = FileLoader.LoadCoreClasses();
    private static List<Character> loadedCharacters = FileLoader.LoadCharacterFiles(); 

    // --- Properties ---
    public static List<Attribute> CoreAttributes => coreAttributes;
    public static List<string> AttributeIdentifiers
    {
        get
        {
            List<string> attributeIdentifiers = [];
            foreach (Attribute attr in CoreAttributes)
                attributeIdentifiers.Add(attr.Name);
            return attributeIdentifiers;
        }
    }
    public static List<CharClass> CoreClasses => coreClasses;
    public static List<Character> LoadedCharacters => loadedCharacters;
    public static void RefreshCharacterFiles() => loadedCharacters = FileLoader.LoadCharacterFiles();
}
