namespace PolybiusTwo;

public static class GameCore
{
    // --- Fields ---
    // These are lists of core objects we'll reference from other places, loaded from files
    private static readonly List<Attribute> coreAttributes = FileLoader.LoadCoreAttributes();
    private static readonly List<CharClass> coreClasses = FileLoader.LoadCoreClasses();

    // --- Properties ---
    public static List<Attribute> CoreAttributes => coreAttributes;

    public static List<CharClass> CoreClasses => coreClasses;
    // --- Constructor ---
    // --- Methods ---
}
