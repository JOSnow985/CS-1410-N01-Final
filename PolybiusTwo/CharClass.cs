namespace PolybiusTwo;

public class CharClass  // May be a bad idea to name this "Class"
{
    // --- Fields ---
    private string _name;
    private string _description;
    // private List<Skill> _classSkills;
    // private List<Ability> _classAbilities;

    // -- Properties --
    public string Name { get => _name; set => _name = value; }
    public string Description { get => _description; set => _description = value; }
    public string HealthDie { get; set; }

    // --- Constructor ---
    public CharClass(string name, string description, string healthDie)
    {
        _name = name;
        Description = description;
        HealthDie = healthDie;
    }

    // --- Methods ---
}
