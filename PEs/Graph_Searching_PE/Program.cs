// Tyler Bye
// 04.18.2025
// Learning Graph Searching Algorithms

namespace Graph_Searching_PE
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // *** MAIN CODE ***
            Graph mansion = new Graph();
            string startingRoom = "hall";

            // prints all rooms in the mansion
            mansion.ListAllVertices();
            Console.WriteLine();

            // performs BFS twice to test the reset
            mansion.BreadthFirst(startingRoom);
            Console.WriteLine();
            mansion.BreadthFirst(startingRoom);

        }
    }
}
