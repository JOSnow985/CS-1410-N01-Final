// Jaden Olvera, Final Project, RPG Character Manager

// Simple loop for the user to interact with the menus
// all functionality is implemented in the classes
while (true)
{
    Console.Clear();
    Menu.ScreenHeader(Menu.MainMenuHeader);
    switch (Console.ReadKey(true).KeyChar)
    {
        case '1':   // Create a Character
            CharGen.CreateNewCharacter();
            Menu.CharacterList();
            break;
        case '2':   // Character List
            Menu.CharacterList();
            break;
        case '3':   // Class List
            Menu.ClassList();
            break;
        case '4':   // Attribute List
            Menu.AttributeList();
            break;
        case '5':   // Exit
            Console.Clear();
            Console.WriteLine("End of Line.");
            return;
        default:
            break;
    }
}