namespace PolybiusTwo;

public class CharClass  // May be a bad idea to name this "Class"
{
    // --- Fields ---
    private string _name;
    private string _description;
    // private List<Skill> _classSkills;
    // private List<Ability> _classAbilities;

    // -- Properties --
    public string ClassName { get => _name; set => _name = value; }

    // --- Constructor ---
    public CharClass(string name, string description)
    {
        _name = name;
        _description = description;
    }

    // --- Methods ---
}
