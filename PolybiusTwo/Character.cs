namespace PolybiusTwo;

public class Character
{
    // --- Fields ---
    private string _name;
    private string _description;
    // private int _maxHealth => _attributes.Constitution;
    private int _maxStamina;
    private int _level;
    private List<(Attribute attribute, int value)> _attributes;

    // --- Properties ---
    public string Name { get => _name; private set => _name = value; }
    public string Description { get => _description; set => _description = value; }
    public List<(Attribute attribute, int value)> Attributes { get => _attributes; set => _attributes = value; }
    // public int MaxHealth { get => _maxHealth; set => _maxHealth = value; }
    public int MaxStamina { get => _maxStamina; set => _maxStamina = value; }
    public int Level { get => _level; set => _level = value; }


    // character class
    // skill list
    // ability list

    // --- Constructor ---
    public Character(string characterName)
    {
        _name = characterName;
        // select class
        // roll attributes
        // use class and attributes to set up basic stats
        // 
    }
}
