// Tyler Bye
// 02/14/25
// Learning about Dictionaries
using System.Reflection.Metadata;
using System.Diagnostics;

namespace Dictionaries_PE
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // *** VARIABLES ***
            Dictionary<string, Player> playersDictionary = new Dictionary<string, Player>();

            Player dwight = new Player("Dwight", 105);
            Player pam = new Player("Pam", 105);

            playersDictionary["Dwight"] = dwight;
            playersDictionary["Pam"] = pam;

            string userName = "";

            // *** MAIN CODE ***
            // --- PART ONE ---
            Console.WriteLine("--- Part 1: Using a dictionary ---");

            // test if the user want to continue playing
            while (userName.ToLower() != "quit")
            {
                // ask user for a name
                Console.Write("Enter a name: ");
                userName = Console.ReadLine()!;

                // end the loop if player enters QUIT
                if (userName.ToLower() == "quit")
                {
                    Console.WriteLine("Ok! All done.\n");
                    break;
                }
                
                // searches dictionary for given name and prints if it contains the name
                if (playersDictionary.ContainsKey(userName))
                {
                    Console.WriteLine($"Player {userName} found. {playersDictionary[userName]}\n");
                }
                else
                {
                    Console.WriteLine($"Player {userName} not found.\n");
                }
            }

            // --- PART TWO ---
            Console.WriteLine("--- Part 2: Performance testing ---");

            // clears dictionary and creates a new list
            playersDictionary.Clear();
            List<Player> playersList = new List<Player>();

            // creates 100,000 players and adds them to both the dictionary and the list
            for (int i = 0; i < 100000; i++)
            {
                string playerName = "p" + i;
                Player newPlayer = new Player(playerName, i * 10); // Assign score as 10 * index
                playersDictionary[playerName] = newPlayer;
                playersList.Add(newPlayer);
            }

            // calculates the last player's name
            string lastPlayerName = "p" + (playersList.Count - 1);

            // measures the dictionary's search time
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            if (playersDictionary.ContainsKey(lastPlayerName))
            {
                Player lastPlayer = playersDictionary[lastPlayerName];
            }

            stopwatch.Stop();

            // prints the total search time of the dictionary
            Console.WriteLine($"Searching dictionary: {stopwatch.Elapsed.TotalMilliseconds} ms");

            // measures the list's search time
            stopwatch.Restart();

            foreach (Player p in playersList)
            {
                if (p.Name == lastPlayerName)
                {
                    Console.WriteLine($"Player {p.Name} found in list.");
                    break;
                }
            }

            stopwatch.Stop();

            // prints the total search time of the list
            Console.WriteLine($"Searching list: {stopwatch.Elapsed.TotalMilliseconds} ms");
        }
    }
}
