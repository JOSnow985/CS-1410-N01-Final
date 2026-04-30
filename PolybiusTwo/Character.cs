namespace PolybiusTwo;


public class Character(string characterName, string description, CharClass charClass, List<(Attribute attribute, int value)> attributes)
{
    // --- Fields ---
    private string _name = characterName;
    private string _description = description;
    private CharClass _charClass = charClass;
    private List<(Attribute attribute, int value)> _attributes = attributes;
    // private int _maxHealth => _attributes.Constitution;
    private int _maxStamina;
    private int _level;

    // --- Properties ---
    public string Name { get => _name; private set => _name = value; }
    public string Description { get => _description; set => _description = value; }
    public CharClass CharClass { get => _charClass; set => _charClass = value; }
    public List<(Attribute attribute, int value)> Attributes { get => _attributes; set => _attributes = value; }
    // public int MaxHealth { get => _maxHealth; set => _maxHealth = value; }
    public int MaxStamina { get => _maxStamina; set => _maxStamina = value; }
    public int Level { get => _level; set => _level = value; }
}
