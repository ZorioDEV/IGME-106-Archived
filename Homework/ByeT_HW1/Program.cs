// Tyler Bye
// 2/2/2025
// Game of Life
namespace ByeT_HW1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // *** VARIABLES ***
            Game myGameOfLife = new Game();
            bool isRunning = true;
            string filename;

            // *** MAIN CODE ***
            // checks if user wants to keep playing
            while (isRunning)
            {
                // main menu options
                Console.WriteLine("Welcome to the Game of Life!" +
                "\n1 - Generate a random board" +
                "\n2 - Display board" +
                "\n3 - Load initial board from file" +
                "\n4 - Quit");
                Console.Write("\nYour choice: ");

                string choice = Console.ReadLine()!;

                // tests which main menu options the users choose
                switch (choice)
                {
                    // generates random board
                    case "1":
                        myGameOfLife.GenerateBoard();
                        break;
                    // tests if board exists and prints board
                    case "2":
                        if (myGameOfLife.HasBoard == true)
                        {
                            myGameOfLife.DisplayBoard();
                            Console.WriteLine();
                        }
                        else
                        {
                            Console.WriteLine("Generate or load a board first.\n");
                        }
                        break;
                    // loads board from file
                    case "3":
                        Console.Write("Filename? ");
                        filename = "../../../" + Console.ReadLine()!;
                        bool subIsRunning = true;

                        // loads and prints board
                        Console.WriteLine();
                        myGameOfLife.LoadBoard(filename);

                        // tests if user wants to continue with loaded file
                        while (subIsRunning)
                        {
                            // sub menu options
                            Console.Write("\n1-Advance, 2-save current board, or 3-main menu? ");
                            string subChoice = Console.ReadLine()!;

                            // tests which sub menu options the users choose
                            switch (subChoice)
                            {
                                // advances board life by 1
                                case "1":
                                    myGameOfLife.AdvanceBoard();
                                    myGameOfLife.DisplayBoard();
                                    break;
                                // saves state of board to new file
                                case "2":
                                    Console.Write("Filename? ");
                                    string saveFilename = "../../../" + Console.ReadLine()!;
                                    myGameOfLife.SaveBoard(saveFilename);
                                    break;
                                // ends sub menu options
                                case "3":
                                    subIsRunning = false;
                                    Console.WriteLine();
                                    break;
                                // tells user they inputed an invlaid choice
                                default:
                                    Console.WriteLine("Invalid choice. Try again.");
                                    break;
                            }
                        }
                        break;
                    // ends main menu options and program
                    case "4":
                        isRunning = false;
                        Console.WriteLine("Goodbye!");
                        break;
                    // tells user they inputed an invlaid choice
                    default:
                        Console.WriteLine("Invalid choice. Try again.");
                        break;
                }
            }
        }
    }
}
