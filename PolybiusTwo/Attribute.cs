namespace PolybiusTwo;

public record Attribute
{
    // -- Properties --
    public string Name { get; set; }
    public string Description { get; set; }

    // --- Constructor ---
    public Attribute(string name, string description)
    {
        Name = name;
        Description = description;
    }
}
