// Jaden Olvera, Final Project

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
            //
            break;
        case '4':   // Attribute List
            Console.Clear();

            break;
        case '5':   // Exit
            Console.Clear();
            Console.WriteLine("Bye!");
            return;
        default:
            break;
    }
}