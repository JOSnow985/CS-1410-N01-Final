namespace PolybiusTwo;

public static class CharGen
{
    private static string CurrentStep = "";
    // Methods
    public static Character CreateNewCharacter()
    {
        string name = CollectName();
        string description = CollectDescription();
        CharClass charClass = ChooseClass();
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
        CharClass? selection = null;
        while (selection is null)
        {
            CharGenHeader();
            Console.WriteLine("Press a number to see more info on a class, and choose!\n");

            // Print list of class options to choose from, start index at 1 for convenience
            for(int i = 0; i < GameCore.CoreClasses.Count; i++)
            {
                Console.WriteLine($"{i + 1} - {GameCore.CoreClasses[i].Name}");
            }

            // Allow the player to select a class from the class list to inspect
            ConsoleKey keyInput;
            int? selectedOption = null;
            do
            {
                keyInput = Console.ReadKey(true).Key;

                if (IsNumericKey(keyInput))
                {
                    selectedOption = ConvertKeyToNumber(keyInput);
                }
            // We continue the loop while any of these conditions is satisfied.
            } while (selectedOption is null || selectedOption < 1 || selectedOption > GameCore.CoreClasses.Count);

            // Correct the index so it's 0 indexed again and not an int? either
            int selectedIndex = selectedOption.Value - 1;
            CharClass selectedClass = GameCore.CoreClasses[selectedIndex];

            // Now that we have a class to inspect, show the player and ask if they want to lock it in or go back
            CharGenHeader();
            Console.WriteLine($"Character Class: {selectedClass.Name}\nDescription: {selectedClass.Description}\nHit Die: {selectedClass.HealthDie}");
            Console.WriteLine($"\nDo you want to be a {GameCore.CoreClasses[selectedIndex].Name}? Y / N");
            
            // Only lock their selection in if they press Y here. 
            keyInput = ConsoleKey.None;
            while (keyInput != ConsoleKey.Y && keyInput != ConsoleKey.N)
            {
                keyInput = Console.ReadKey(true).Key;
            }
            if (keyInput == ConsoleKey.Y)
                selection = GameCore.CoreClasses[selectedIndex];
        }

        // If we exit the above loop, we've either broken something or we have our class pick, return!
        return selection;
    }

    // Checks the passed ConsoleKey to see if it's a number key we can convert to an int
    private static bool IsNumericKey(ConsoleKey key)
    {
        return  key >= ConsoleKey.D0 && key <= ConsoleKey.D9 ||
                key >= ConsoleKey.NumPad0 && key <= ConsoleKey.NumPad9;
    }

    // Takes a ConsoleKey and returns an int from it, this can be a switch expression but it's honestly easier to understand this way
    private static int? ConvertKeyToNumber(ConsoleKey key)
    {
        if (key >= ConsoleKey.D0 && key <= ConsoleKey.D9)
            return key - ConsoleKey.D0;

        // Converts number pad too! (even though I don't have one)
        if (key >= ConsoleKey.NumPad0 && key <= ConsoleKey.NumPad9)
            return key - ConsoleKey.NumPad0;

        return null;
    }

    // Method that accepts a delegate and returns a list of numbers for our attributes
    public static List<int> RollAttributes(Func<List<int>> roller)
    {
        return roller();
    }

    // Roller delegate, Roll attributes using 4d6 drop lowest, I like this way...
    public static List<int> AttRoller_FourDropLow()
    {
        Random rng = new();
        List<int> results = [0, 0, 0, 0, 0, 0];
        for (int i = 0; i < results.Count; i++)
        {
            // Roll 4d6
            List<int> rolls = [rng.Next(1,7), rng.Next(1,7), rng.Next(1,7), rng.Next(1,7)];

            // Sort the list and remove the lowest roll
            rolls.Sort();
            rolls.RemoveAt(0);

            // Save the sum of the remaining three dice to the results
            results.Add(rolls.Sum());
        }
        return results;
    }

    // Roller delegate, Roll attributes using 3d6
    public static List<int> ThreeInOrder()
    {
        Random rng = new();
        List<int> results = [0, 0, 0, 0, 0, 0];
        for (int i = 0; i < results.Count; i++)
        {
            // Roll 3d6
            List<int> rolls = [rng.Next(1,7), rng.Next(1,7), rng.Next(1,7)];

            // Save the sum of the dice to the results
            results.Add(rolls.Sum());
        }
        return results;
    }
    public static List<(Attribute, int)> SetAttributes()
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
        Console.WriteLine($"--- Character Creation ---\nCurrently Choosing: {CurrentStep}\n");
    }
}
