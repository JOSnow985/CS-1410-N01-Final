namespace PolybiusTwo;

public static class CharGen
{
    private static string CurrentStep = "";

    // --- Methods ---
    // Essentially a publicly accessible wrapper for the other methods 
    // that make up the character generation process
    public static Character CreateNewCharacter()
    {
        string name = CollectName();
        string description = CollectDescription();
        CharClass charClass = ChooseClass();
        List<(Attribute, int)> charAttributes = SetAttributes();
        Character charGenned = new(name, description, charClass, charAttributes);

        // Make a save file for the newly generated character before returning it
        SaveCharacter(charGenned);
        return charGenned;
    }

    private static void SaveCharacter(Character character)
    {
        List<string> lines = [];
        lines.Add(string.Join(',', ["Name", character.Name]));
        lines.Add(string.Join(',', ["Description", character.Description]));
        lines.Add(string.Join(',', ["CharClass", character.CharClass.Name]));
        foreach((Attribute a, int v) in character.Attributes)
        {
            lines.Add(string.Join(',', [a.Name, v]));
        }
        // Creates a filepath by combining current directory and a "characterfiles" directory
        string savepath = Path.Combine(Directory.GetCurrentDirectory(),"CharacterFiles");
        // Must create this directory if it doesn't exist
        Directory.CreateDirectory(savepath);
        File.WriteAllLines(Path.Combine(savepath,$"{character.Name}.csv"), lines.ToArray());
    }

    // Simple method to update the CharGen step and collect the character name
    private static string CollectName()
    {
        CurrentStep = "Character Name";
        return HandleCharGenInput("What should your character's name be?");
    }

    // Asks the user if they want to enter a description or not. Collects a description to return or returns a default one.
    private static string CollectDescription()
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
    private static CharClass ChooseClass()
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
            int selectedIndex = HandleCharGenIndexChoice(GameCore.CoreClasses.Count);
            CharClass selectedClass = GameCore.CoreClasses[selectedIndex];

            // Now that we have a class to inspect, show the player and ask if they want to lock it in or go back
            CharGenHeader();
            Console.WriteLine($"Character Class: {selectedClass.Name}\nDescription: {selectedClass.Description}\nHit Die: {selectedClass.HealthDie}");
            Console.WriteLine($"\nDo you want to be a {GameCore.CoreClasses[selectedIndex].Name}? Y / N");
            
            // Only lock their selection in if they press Y here. 
            ConsoleKey keyInput = ConsoleKey.None;
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

    // Method that accepts a delegate and returns a list of numbers for our attributes
    private static List<int> RollAttributes(Func<List<int>> roller) => roller();

    // Roller delegate, Roll attributes using 4d6 drop lowest, I like this way...
    private static List<int> FourDropLow()
    {
        Random rng = new();
        List<int> results = [];
        for (int i = 0; i <= 6; i++)
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
    private static List<int> ThreeInOrder()
    {
        Random rng = new();
        List<int> results = [];
        for (int i = 0; i < 6; i++)
        {
            // Roll 3d6
            List<int> rolls = [rng.Next(1,7), rng.Next(1,7), rng.Next(1,7)];

            // Save the sum of the dice to the results
            results.Add(rolls.Sum());
        }
        return results;
    }
    private static List<(Attribute, int)> SetAttributes()
    {
        CurrentStep = "Attributes";
        List<(Attribute, int)>? rolls = null;

        while (rolls is null)
        {
            CharGenHeader();
            Console.WriteLine("Which style of attribute rolling do you want to use?\n(They're all \"in order\", I'm sorry)\n");

            // Print list of roller delegates
            Console.WriteLine("1 - Roll 4d6, Drop Low\n2 - Roll 3d6");
            List<Func<List<int>>> rollers = [FourDropLow, ThreeInOrder];

            // Allow the user to select from the roller delegates we have, then use that delegate to roll the attributes
            int selectedIndex = HandleCharGenIndexChoice(rollers.Count);
            // Loop here to allow rerolling fast
            while (selectedIndex != -1 && rolls is null)
            {
                List<int> results = RollAttributes(rollers[selectedIndex]);

                // Combine the results with our attribute list
                List<(Attribute, int)> pairs = [];
                for (int i = 0; i < GameCore.CoreAttributes.Count; i++)
                {
                    pairs.Add((GameCore.CoreAttributes[i], results[i]));
                }

                // We've now rolled a list of pairs, ask if they want to lock it in or go back
                CharGenHeader();
                foreach((Attribute a, int v) pair in pairs)
                {
                    Console.WriteLine($"{pair.a.Name} - {pair.v}");
                }

                // Only lock their selection in if they press Y here, N will return to selecting a roller, R will use the selected roller again
                Console.WriteLine($"\nAccept these numbers?");
                Console.WriteLine($"Y - Yes    N - Back to Rollers    R - Reroll");
                ConsoleKey keyInput = ConsoleKey.None;
                while (keyInput != ConsoleKey.Y && keyInput != ConsoleKey.N && keyInput != ConsoleKey.R)
                {
                    keyInput = Console.ReadKey(true).Key;
                }
                if (keyInput == ConsoleKey.Y)
                    rolls = pairs;
                else if (keyInput == ConsoleKey.N)
                    selectedIndex = -1;     // Set index to -1 to signal that we need to select a roller again
            }
        }
        return rolls;
    }

    // Input collector that lets the user select from a list of options, only returns when we have a valid choice
    private static int HandleCharGenIndexChoice(int listCount)
    {
        // Allow the player to select an option from a 1 indexed list
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
        } while (selectedOption is null || selectedOption < 1 || selectedOption > listCount);

        // Correct the index so it's 0 indexed again and not an int? either
        int selectedIndex = selectedOption.Value - 1;

        return selectedIndex;
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

    // Handler for collecting strings from the user, makes sure we don't get one that's Null or Empty.
    private static string HandleCharGenInput(string prompt)
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
    private static void CharGenHeader()
    {
        Console.Clear();
        Console.WriteLine($"--- Character Creation ---\nCurrently Choosing: {CurrentStep}\n");
    }
}
