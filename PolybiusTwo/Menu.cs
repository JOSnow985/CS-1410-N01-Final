namespace PolybiusTwo;

public static class Menu
{
    public static void CharacterList()
    {
        
    }
    public static void ClassList()
    {
        Console.Clear();
        Console.WriteLine("Class List\n");
        foreach(CharClass c in GameCore.CoreClasses)
        {
            Console.WriteLine($"{c.Name}- {c.HealthDie} - {c.Description}");
        }
        Console.WriteLine("Press any key to return to the main menu.");
        Console.ReadKey(true);
    }
        public static void AttributeList()
    {
        Console.Clear();
        Console.WriteLine("Attribute List\n");
        foreach(Attribute attr in GameCore.CoreAttributes)
        {
            Console.WriteLine($"{attr.Name} - {attr.Description}");
        }
        Console.WriteLine("Press any key to return to the main menu.");
        Console.ReadKey(true);
    }

    // Input collector that lets the user select from a list of options, only returns when we have a valid choice
    public static int HandleIndexChoice(int listCount)
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
    public static string HandleStringInput(string prompt, Action printHeader)
    {
        bool isSecondTry = false;

        // Loop only ends when we have a valid string to return
        while (true)
        {
            ScreenHeader(printHeader);
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
    public static void ScreenHeader(Action printHeader)
    {
        Console.Clear();
        printHeader();
    }
    public static void MainMenuHeader()
    {
        Console.WriteLine("Welcome to Jaden's RPG Character Sheet Manager!\n");
        Console.WriteLine("[1] Create a Character\n[2] Character List\n[3] Class List\n[4] Attribute List\n[5] Exit");
    }
}