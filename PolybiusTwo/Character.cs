namespace PolybiusTwo;


public class Character(string characterName, string description, CharClass charClass, List<(Attribute attribute, int value)> attributes)
{
    // --- Fields ---
    private string _name = characterName;
    private string _description = description;
    private CharClass _charClass = charClass;
    private int _level = 1;
    private List<(Attribute attribute, int value)> _attributes = attributes;
    // private int _maxHealth => _attributes.Constitution;
    private int _maxStamina;

    // --- Properties ---
    public string Name { get => _name; private set => _name = value; }
    public string Description { get => _description; set => _description = value; }
    public CharClass CharClass { get => _charClass; set => _charClass = value; }
    public int Level { get => _level; set => _level = value; }
    public List<(Attribute attribute, int value)> Attributes { get => _attributes; set => _attributes = value; }
    // public int MaxHealth { get => _maxHealth; set => _maxHealth = value; }
    public int MaxStamina { get => _maxStamina; set => _maxStamina = value; }

    // --- Methods ---
    // Save the current character state to a csv file
    public void SaveToFile()
    {
        // Just grabbing strings, putting them with their identifiers
        List<string> lines = [];
        lines.Add(string.Join(',', ["Name", Name]));
        lines.Add(string.Join(',', ["Description", Description]));
        lines.Add(string.Join(',', ["CharClass", CharClass.Name]));
        lines.Add(string.Join(',', ["Level", Level]));
        foreach((Attribute attr, int v) in Attributes)
        {
            lines.Add(string.Join(',', [attr.Name, v]));
        }

        // Creates a filepath by combining current directory and a "characterfiles" directory
        string savepath = Path.Combine(Directory.GetCurrentDirectory(),"CharacterFiles");

        // Must create this directory if it doesn't exist
        Directory.CreateDirectory(savepath);
        File.WriteAllLines(Path.Combine(savepath,$"{Name}.csv"), lines.ToArray());
    }
}
