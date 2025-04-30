// Tyler Bye
// 04.17.2025
// Learning Graph Data Structure

namespace Graphs_PE
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // *** MAIN CODE ***
            Graph mansion = new Graph();
            string currentRoom = "hall";

            // prints all rooms in the mansion
            mansion.ListAllVertices();
            Console.WriteLine();

            // main game loop
            while (currentRoom != "exit")
            {
                // gets adjacent rooms & checks if null
                List<Vertex> adjacentRooms = mansion.GetAdjacentList(currentRoom);
                if (adjacentRooms == null)
                {
                    Console.WriteLine($"Error: No adjacency data found for {currentRoom}.");
                    break;
                }

                // displays current room & adjacent rooms
                Console.WriteLine($"You are currently in the {currentRoom}.");
                Console.Write("Nearby are:");
                foreach (Vertex room in adjacentRooms)
                {
                    Console.Write($" -{room.Name}");
                }
                Console.WriteLine();

                // asks user for next room
                Console.Write("Where would you like to go? ");
                string userInput = Console.ReadLine()!.Trim().ToLower();

                // checks if valid input
                if (!mansion.MapContainsRoom(userInput))
                {
                    Console.WriteLine($"\nSorry, '{userInput}' is not a room in the map.");
                    continue;
                }

                if (!mansion.AreAdjacent(currentRoom, userInput))
                {
                    Console.WriteLine($"\nSorry, {userInput} is not adjacent to the {currentRoom}.");
                    continue;
                }

                // moves user to the new room
                currentRoom = userInput;
                Console.WriteLine();
            }

            // tells user they have exited the mansion & ends program
            Console.WriteLine("You have successfully left the mansion.");
        }
    }
}
