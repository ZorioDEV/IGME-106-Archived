// Tyler Bye 
// 04.21.2025
// Learning Dijkstra's Algorithm

namespace Dijkstra_Algo_PE
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // *** VARIABLES ***
            Graph mansion = new Graph();
            string startingRoom1 = "hall";
            string startingRoom2 = "conservatory";

            // search 1
            Console.WriteLine("------------------------------------------------------");
            Console.WriteLine($"Search 1: Starting in the {startingRoom1}.");
            mansion.ShortestPath(startingRoom1);

            string destination;
            do
            {
                Console.Write("Which room are you trying to get to? > ");
                destination = Console.ReadLine()!.ToLower();
                Console.WriteLine();

                if (!mansion.MapContainsRoom(destination))
                {
                    Console.WriteLine("Sorry, that is not a room.");
                }
            } while (!mansion.MapContainsRoom(destination));

            Vertex destVertex = mansion.FindVertex(destination);
            mansion.PrintShortestPath(destVertex);
            Console.WriteLine();

            // search 2
            Console.WriteLine("------------------------------------------------------");
            Console.WriteLine($"Search 2: Starting in the {startingRoom2}.");
            mansion.ShortestPath(startingRoom2);

            do
            {
                Console.Write("Which room are you trying to get to? > ");
                destination = Console.ReadLine()!.ToLower();
                Console.WriteLine();

                if (!mansion.MapContainsRoom(destination))
                {
                    Console.WriteLine("Sorry, that is not a room.");
                }
            } while (!mansion.MapContainsRoom(destination));

            destVertex = mansion.FindVertex(destination);
            mansion.PrintShortestPath(destVertex);
        }
    }
}
