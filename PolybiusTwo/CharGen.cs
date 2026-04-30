namespace PolybiusTwo;

public static class CharGen
{
    public static string CurrentStep = "";
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
        CurrentStep = "Character Name";
        // ask user for character name
        // verify name isn't null
        // return name
        return null;
    }
    public static string CollectDescription()
    {
        CurrentStep = "Character Description";
        // ask user if they want to enter a description
        // if yes, collect description
        // verify collected description
        // if no, use base description
        // return description
        return null;
    }
    public static CharClass ChooseClass()
    {
        CurrentStep = "Character Class";
        // present user a list of class options to choose from
        // allow selecting class to view description
        // ask to confirm
        // allow going back to class list screen
        // return class
        return null;
    }
    public static List<(Attribute, int)> RollAttributes()
    {
        CurrentStep = "Attributes";
        // present user a list of attribute roll styles to choose from
        // roll attributes
        // show user the generated numbers and let them reroll
        // return list of attributes
        return [];
    }
    // Handler for collecting strings from the user, makes sure we don't get one that's Null or Empty.
    public static string HandleCharGenInput(string prompt)
    {
        bool isSecondTry = false;

        // Loop only ends when we have a valid string to return
        while (true)
        {
            Console.Clear();
            CharGenHeader();
            Console.WriteLine(prompt);
            // Only print the complaint if it's been set
            if (isSecondTry)
                Console.WriteLine("Enter something that isn't Null or Empty!");

            // Collect user input and check for Null and Empty
            string? userInput = Console.ReadLine();
            if (string.IsNullOrEmpty(userInput))
                isSecondTry = true;
            else
                return userInput;
        }
    }
    public static void CharGenHeader() => Console.WriteLine($"Creating a new character, currently choosing: {CurrentStep}\n");
}
