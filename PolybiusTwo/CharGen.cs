namespace PolybiusTwo;

public static class CharGen
{
    public static string CurrentStep = "";
    // Methods
    public static Character CreateNewCharacter()
    {
        string name = CollectName();
        string description = CollectDescription();
        // collect class choice
        // collect attribute roll style
        // roll attributes
        // return newly created character
        return null;
    }

    // Simple method to update the CharGen step and collect the character name
    public static string CollectName()
    {
        CurrentStep = "Character Name";
        return HandleCharGenInput("What should your character's name be?");
    }

    // Asks the user if they want to enter a description or not. Collects a description to return or returns a default one.
    public static string CollectDescription()
    {
        CurrentStep = "Character Description";
        CharGenHeader();

        // Prompt user if they want to enter a description, loop until they press Y or N
        Console.WriteLine("Do you want to enter a description for your character or use a default one?\nY - Yes\nN - No");
        ConsoleKey keyInput = ConsoleKey.None;
        while (keyInput != ConsoleKey.Y && keyInput != ConsoleKey.N)
        {
            keyInput = Console.ReadKey(true).Key;
        }

        // After they've pressed Y or N, we either collect a description or use the default
        if (keyInput == ConsoleKey.Y)
            return HandleCharGenInput("Please enter your character's description!");
        else
            return "Yet another hero in the fight against communism.";
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
    public static void CharGenHeader()
    {
        Console.Clear();
        Console.WriteLine($"Creating a new character, currently choosing: {CurrentStep}\n");
    }
}
