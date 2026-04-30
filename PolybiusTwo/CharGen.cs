namespace PolybiusTwo;

public static class CharGen
{
    // Methods
    public static Character CreateNewCharacter()
    {
        // collect name
        // collect description
        // collect class choice
        // collect attribute roll style
        // roll attributes
        // return newly created character
        return null;
    }
    public static string CollectName()
    {
        // ask user for character name
        // verify name isn't null
        // return name
        return null;
    }
    public static string CollectDescription()
    {
        // ask user if they want to enter a description
        // if yes, collect description
        // verify collected description
        // if no, use base description
        // return description
        return null;
    }
    public static CharClass ChooseClass()
    {
        // present user a list of class options to choose from
        // allow selecting class to view description
        // ask to confirm
        // allow going back to class list screen
        // return class
        return null;
    }
    public static List<(Attribute, int)> RollAttributes()
    {
        // present user a list of attribute roll styles to choose from
        // roll attributes
        // show user the generated numbers and let them reroll
        // return list of attributes
        return [];
    }
}
