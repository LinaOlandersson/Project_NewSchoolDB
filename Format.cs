namespace NewSchoolDB
{
    public class Format
    {
        //Method to only accept valid numeric keypress within specified range.
        public static int Choice(int max)   
        {
            bool valid = false;
            string numberString = "";
            int number = 0;
            ConsoleKeyInfo cki;

            while (!valid)
            {
                do
                {   // Takes input and checks if it is a number
                    cki = Console.ReadKey(true);
                    bool isNumber = int.TryParse(cki.KeyChar.ToString(), out int check);

                    if (cki.Key != ConsoleKey.Backspace)
                    {
                        if (isNumber) //if it is a number, adds to string of numbers 
                        {
                            numberString += cki.KeyChar;
                            Console.Write(cki.KeyChar);
                        }
                    } // Backspace key delete characters from the string
                    else if (cki.Key == ConsoleKey.Backspace && numberString.Length > 0)
                    {
                        numberString = numberString.Substring(0, (numberString.Length - 1));
                        Console.Write("\b \b");
                    }
                    // Exits while loop when user presses enter after entering at least 1 number
                } while (cki.Key != ConsoleKey.Enter || numberString.Length == 0);

                Console.WriteLine();

                // The string is converted to int. If within range the number is returned.
                // If not within range the while loop continues.
                int.TryParse(numberString, out number);

                if (number > max || number == 0)
                {
                    Console.WriteLine("Invalid choice - try again");
                    numberString = "";
                    number = 0;
                }
                else
                {
                    valid = true;
                }
            }
            return number;
        }
        
        // A method to make sure that the navigation back to menu works identically every time.
        public static void ReturnToMenu()
        {
            Console.WriteLine("\n\nPress any key to return to main menu");
            Console.ReadKey();
            Console.Clear();

            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine();
            }

            Console.SetCursorPosition(0, 0);
        }

        
    }
}
