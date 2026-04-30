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
        charGenned.SaveToFile();
        return charGenned;
    }

    // Simple method to update the CharGen step and collect the character name
    private static string CollectName()
    {
        CurrentStep = "Character Name";
        return Menu.HandleStringInput("What should your character's name be?", CharGenHeader);
    }

    // Asks the user if they want to enter a description or not. Collects a description to return or returns a default one.
    private static string CollectDescription()
    {
        CurrentStep = "Character Description";
        Menu.ScreenHeader(CharGenHeader);

        // Prompt user if they want to enter a description, loop until they press Y or N
        Console.WriteLine("Do you want to enter a description for your character or use a default one?\nY - Yes\nN - No");
        ConsoleKey keyInput = ConsoleKey.None;
        while (keyInput != ConsoleKey.Y && keyInput != ConsoleKey.N)
        {
            keyInput = Console.ReadKey(true).Key;
        }

        // After they've pressed Y or N, we either collect a description or use the default
        if (keyInput == ConsoleKey.Y)
            return Menu.HandleStringInput("Please enter your character's description!", CharGenHeader);
        else
            return "Yet another hero in the fight against communism.";
    }
    private static CharClass ChooseClass()
    {
        CurrentStep = "Character Class";
        CharClass? selection = null;
        while (selection is null)
        {
            Menu.ScreenHeader(CharGenHeader);
            Console.WriteLine("Press a number to see more info on a class, and choose!\n");

            // Print list of class options to choose from, start index at 1 for convenience
            for(int i = 0; i < GameCore.CoreClasses.Count; i++)
            {
                Console.WriteLine($"{i + 1} - {GameCore.CoreClasses[i].Name}");
            }

            // Allow the player to select a class from the class list to inspect
            int selectedIndex = Menu.HandleIndexChoice(GameCore.CoreClasses.Count);
            CharClass selectedClass = GameCore.CoreClasses[selectedIndex];

            // Now that we have a class to inspect, show the player and ask if they want to lock it in or go back
            Menu.ScreenHeader(CharGenHeader);
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
            Menu.ScreenHeader(CharGenHeader);
            Console.WriteLine("Which style of attribute rolling do you want to use?\n(They're all \"in order\", I'm sorry)\n");

            // Print list of roller delegates
            Console.WriteLine("1 - Roll 4d6, Drop Low\n2 - Roll 3d6");
            List<Func<List<int>>> rollers = [FourDropLow, ThreeInOrder];

            // Allow the user to select from the roller delegates we have, then use that delegate to roll the attributes
            int selectedIndex = Menu.HandleIndexChoice(rollers.Count);
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
                Menu.ScreenHeader(CharGenHeader);
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
    // Delegate to pass to Menu.ScreenHeader(Action a)
    public static void CharGenHeader()
    {
        Console.WriteLine($"--- Character Creation ---\nCurrently Choosing: {CurrentStep}\n");
    }
}
