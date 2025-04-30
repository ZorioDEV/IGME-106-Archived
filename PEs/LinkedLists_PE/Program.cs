namespace LinkedLists_PE
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // *** VARIABLES ***
            CustomLinkedList<string> list = new CustomLinkedList<string>();

            // *** MAIN CODE ***
            // tells user to add 5 new items to their inventory
            Console.WriteLine("Linked list is created. Please add 5 inventory items to the list.");

            // asks the user 5 times & adds their input into the list
            for (int i = 0; i < 5; i++)
            {
                Console.Write($"Enter item #{i + 1}: ");
                string item = Console.ReadLine()!;
                list.Add(item);
            }

            // prints the count and items
            PrintListContents(list);

            // tests the invalid indices
            Console.WriteLine();
            TestIndex(list, -1);
            TestIndex(list, list.Count);

            // tests situational removals
            TestRemoveAt(list, 8);

            TestRemoveAt(list, 4);
            TestRemoveAt(list, 0);
            TestRemoveAt(list, 1);

            // prints the count and remaining items
            PrintListContents(list);

            // tests the only node removal case
            TestRemoveAt(list, 0);
            TestRemoveAt(list, 0);

            // prints the count and remaining items
            PrintListContents(list);

            // tests empty list removal
            TestRemoveAt(list, 0);
        }

        // *** HELPERS ***
        /// <summary>
        /// Tests the indexer of the CustomLinkedList.
        /// </summary>
        /// <param name="list">Linked list to test</param>
        /// <param name="index">Index to attempt to access</param>
        static void TestIndex(CustomLinkedList<string> list, int index)
        {
            // tries to access the list at given index
            try
            {
                string item = list[index];
            }
            // catches and displays any argument exceptions thrown by invalid indices
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        /// <summary>
        /// Tests the RemoveAt method of the CustomLinkedList.
        /// </summary>
        /// <param name="list">Linked list to test</param>
        /// <param name="index">Index of the node to remove</param>
        static void TestRemoveAt(CustomLinkedList<string> list, int index)
        {
            // tries to remove at given index
            try
            {
                Console.WriteLine($"\nRemoving item at index {index}...");
                string removedItem = list.RemoveAt(index);
                Console.WriteLine($"Removed '{removedItem}'");
            }
            // catches and displays any argument exceptions thrown by invalid indices
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        /// <summary>
        /// Prints the current count and contents of the list.
        /// </summary>
        /// <param name="list">Linked list to print</param>
        static void PrintListContents(CustomLinkedList<string> list)
        {
            // displays list count to user & prints header for items list
            Console.Write($"\nThe list has {list.Count} items. ");

            // tests if the list is empty
            if (list.Count == 0)
            {
                Console.WriteLine("\nList is empty: nothing to print.");
            }
            else
            {
                // loops through all the items in the list
                Console.WriteLine("These items are:");
                for (int i = 0; i < list.Count; i++)
                {
                    Console.WriteLine($"- {list[i]}");
                }
            }
        }
    }
}
