namespace Stacks_Queues_PE
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // *** VARIABLES ***
            GameStack<string> spellStack = new GameStack<string>();
            string[] spells = { "Shock", "Fork", "Counterspell", "Force of Will" };

            GameQueue<string> playerQueue = new GameQueue<string>();
            string[] players = { "GandalfThePurple", "SporkNinja", "TacticalTurtle", "LaggyMcLagz" };

            // *** MAIN CODE ***
            // --- PART ONE ---
            // welcomes user to the stack section
            Console.WriteLine(
                "---------------------------------------------------------" +
                "\nTESTING THE GAME STACK" +
                "\n---------------------------------------------------------"
            );

            // pushes all the spells in the array onto the stack
            Console.WriteLine("The following spells are being put on the stack:");
            foreach (string spell in spells)
            {
                spellStack.Push(spell);
                Console.WriteLine($"- {spellStack.Peek()}");
            }

            // prints the count of spells in the stack
            Console.WriteLine($"\nThere are {spellStack.Count} spells in the stack.");

            // pops out all the spells one-by-one, making them print in reverse order
            Console.WriteLine("\nSpells resolving in reverse order:");
            while (!spellStack.IsEmpty)
            {
                Console.WriteLine($"- {spellStack.Pop()}");
            }

            // prints the count of spells in the stack
            Console.WriteLine($"\nThere are {spellStack.Count} spells in the stack.\n");

            // tries to pop an empty stack
            try
            {
                spellStack.Pop();
            }
            // error message if the stack is empty
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Error occurred in Main: {ex.Message}");
            }

            // tries to peek an empty stack
            try
            {
                spellStack.Peek();
            }
            // error message if the stack is empty
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Error occurred in Main: {ex.Message}\n");
            }

            // --- PART TWO ---
            // welcomes user to the queue section
            Console.WriteLine(
                "---------------------------------------------------------" +
                "\nTESTING THE GAME QUEUE" +
                "\n---------------------------------------------------------"
            );

            // enqueues all the players in the array onto the queue
            Console.WriteLine("The following players are joining the queue:");
            foreach (string player in players)
            {
                playerQueue.Enqueue(player);
                Console.WriteLine($"- {player}");
            }

            // prints the count of players in the queue
            Console.WriteLine($"\nThere are {playerQueue.Count} players in the queue.\n");

            // dequeues each player one-by-one and prints out the queue line wait
            while (!playerQueue.IsEmpty)
            {
                string player = playerQueue.Dequeue();
                Console.WriteLine($"\"{player}\" has joined the server: \t{playerQueue.Count} player(s) left in queue");
            }
            Console.WriteLine();

            // tries to dequeue an empty queue
            try
            {
                playerQueue.Dequeue();
            }
            // error message if the queue is empty
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Error occurred in Main: {ex.Message}");
            }

            // tries to peek an empty queue
            try
            {
                playerQueue.Peek();
            }
            // error message if the queue is empty
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Error occurred in Main: {ex.Message}");
            }
        }
    }
}
