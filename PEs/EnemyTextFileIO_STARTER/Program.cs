namespace EnemyTextFileIO_STARTER
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // ----------------------------------------------------------------
            // Variable setup block
            // ----------------------------------------------------------------

            // Menu system and input:
            string userChoice = "";
            int numberEnemies = 0;

            // Local data storage:
            List<string> nameList = new List<string>();
            List<int> damageList = new List<int>();

            // File reading/writing:
            string filename = "../../../enemies.txt";

            // ----------------------------------------------------------------
            // Looping Menu
            // ----------------------------------------------------------------
            Console.WriteLine("Welcome to the enemy name program!");

            while (userChoice != "QUIT")
            {
                // Provide menu options to user. Retrieve their chosen option.
                userChoice = DisplayMenu();
                Console.WriteLine();

                // Run game
                if (userChoice == "ENTER")
                {
                    // Get user's chosen amount of enemies
                    numberEnemies = GetEnemyQuantity();

                    // Prompt for enemy attributes.
                    PromptForEnemyData(numberEnemies, nameList, damageList);
                }
                else if (userChoice == "SAVE")
                {
                    // Does any enemy data exist to write to the file?
                    // When there is no data, don't write to the file and let user know.
                    if (nameList.Count == 0)
                    {
                        Console.WriteLine("There is no data to save.");
                    }
                    // There IS data to save to the file!
                    else
                    {
                        // Provide confirmation for user that data is being saved to the file.
                        Console.WriteLine("Saving enemy names to the file.");

                        StreamWriter writer = null;

                        // try-block for writer
                        try
                        {
                            writer = new StreamWriter(filename);

                            // adds name & damage amount for each character
                            for (int i = 0; i < nameList.Count; i++)
                            {
                                writer.WriteLine($"{nameList[i]},{damageList[i]}");
                            }
                        }
                        // runs if there is an error
                        catch (Exception error)
                        {
                            Console.WriteLine("Error occurred reading the file: " + error.Message);
                        }
                        // Close the stream, which closes the file
                        finally
                        {
                            if (writer != null)
                            {
                                writer.Close();
                            }
                        }
                    }
                }
                else if (userChoice == "LOAD")
                {
                    nameList.Clear();
                    damageList.Clear();

                    StreamReader reader = null;

                    // try-block for reader
                    try
                    {
                        reader = new StreamReader(filename);
                        string line = "";

                        // splits & adds parts of character data into respected lists
                        while ((line = reader.ReadLine()!) != null)
                        {
                            string[] parts = line.Split(',');
                            nameList.Add(parts[0]);
                            damageList.Add(int.Parse(parts[1]));
                        }
                    }
                    // runs if there is an error
                    catch (Exception error)
                    {
                        Console.WriteLine("Error occurred reading the file: " + error.Message);
                    }
                    // Close the stream, which closes the file
                    finally
                    {
                        if (reader != null)
                        {
                            reader.Close();
                        }
                    }

                    // tells user if there is data that can be loaded
                    if (nameList.Count == 0)
                    {
                        Console.WriteLine("The file does not contain any data to load.");
                    }
                    else
                    {
                        Console.WriteLine("Loading data from file.");
                        // loops & prints through existing data
                        PrintEnemyData(nameList, damageList);
                    }
                }
                else if (userChoice == "PRINT")
                {
                    PrintEnemyData(nameList, damageList);
                }
                else if (userChoice == "QUIT")
                {
                    Console.WriteLine("Goodbye!");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("I do not recognize that response. Please try again.");
                    Console.ForegroundColor = ConsoleColor.White;
                }

                // Blank line for the next "round" of the program.
                Console.WriteLine("-----------------------------");
            }
        }

        #region Helper Methods already written for you!

        /// <summary>
        /// Displays the menu choices to a user.
        /// </summary>
        /// <returns>User's chosen option from one of four options.</returns>
        public static string DisplayMenu()
        {
            // Provide menu options
            Console.WriteLine("Choose one of the following options:");
            Console.WriteLine(" - 'ENTER' data");
            Console.WriteLine(" - 'SAVE' data");
            Console.WriteLine(" - 'LOAD' data");
            Console.WriteLine(" - 'PRINT' data");
            Console.WriteLine(" - 'QUIT' the program");
            Console.Write("Your choice >> ");

            // Retrieve user's choice in blue, then reset to white
            Console.ForegroundColor = ConsoleColor.Blue;
            string userChoice = Console.ReadLine().Trim().ToUpper();
            Console.ForegroundColor = ConsoleColor.White;

            return userChoice;
        }


        /// <summary>
        /// Prompts user for number of enemies with which to enter data for.
        /// Validates between 1 and 5 enemies.
        /// </summary>
        /// <returns>Number of enemies the user will enter valid data for.</returns>
        public static int GetEnemyQuantity()
        {
            // Prompt user
            Console.Write("How many enemies are being added? (1 - 5): ");
            string userChoice = Console.ReadLine();
            int numberEnemies = -1;

            // Did they enter an invalid amount? Force re-entry until valid.
            while (!int.TryParse(userChoice, out numberEnemies) ||
                  numberEnemies < 1 ||
                  numberEnemies > 5)
            {
                // Print error message in red, then return to white.
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("Invalid amount. Enter 1 - 5. ");
                Console.ForegroundColor = ConsoleColor.White;
                userChoice = Console.ReadLine();
            }

            return numberEnemies;
        }


        /// <summary>
        /// Gets all enemy data from a user. 
        /// Enters data into the appropriate name and damage lists.
        /// </summary>
        /// <param name="amount">Number of enemies to enter data for.</param>
        /// <param name="names">List of enemy names</param>
        /// <param name="damages">List of enemy damages</param>
        public static void PromptForEnemyData(int amount, List<string> names, List<int> damages)
        {
            // Loop for as many enemies as user indicates...
            for (int i = 0; i < amount; i++)
            {
                // Get name and damage information
                Console.Write($"   Enter enemy {i + 1} name: ");
                string name = Console.ReadLine().Trim();

                Console.Write($"   Enter enemy {i + 1} damage: ");
                string damage = Console.ReadLine().Trim();
                int parsedDamage = -1;

                // If damage is an appropriate value, enter data in lists.
                if (int.TryParse(damage, out parsedDamage))
                {
                    names.Add(name);
                    damages.Add(parsedDamage);

                    // Green confirmation message that data was added to lists.
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"   {name} has been added to the system.");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                // Invalid damage? Do not enter the name or damage into lists. Force re-entry.
                else
                {
                    // Red error message printed to user.
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"   Invalid damage range. Could not add {name}. Try again.");
                    Console.ForegroundColor = ConsoleColor.White;

                    // Reduce iterations to account for invalid data.
                    i--;
                }
            }
        }


        /// <summary>
        /// Prints all locally-stored enemy data from the name and damage lists.
        /// </summary>
        /// <param name="names">Reference to the list of names</param>
        /// <param name="damages">Reference to the list of damages</param>
        public static void PrintEnemyData(List<string> names, List<int> damages)
        {
            // No data found!
            if (names.Count == 0)
            {
                Console.WriteLine("No enemy data found.");
            }
            else
            {
                // Print all enemies in easy-to-read format.
                Console.WriteLine("Here are all enemies in this program:");

                for (int i = 0; i < names.Count; i++)
                {
                    Console.WriteLine($"  Name: {names[i]}. Damage: {damages[i]}");
                }
            }
        }

        #endregion
    }
}
