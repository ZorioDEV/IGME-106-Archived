using System;

namespace CustomList_STARTER
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // ------------------------------
            // ----- Instantiation Test -----
            // ------------------------------

            // Instantiate a custom list of doubles with capacity of 2.
            // A capacity of 2 forces a resize quickly.
            CustomList<double> myList = new CustomList<double>(2);


            // ---------------------------------------------------------
            // ----- GetData/SetData Exceptions Test on Empty List -----
            // ---------------------------------------------------------

            // Test GetData exception is properly working for an empty list.
            Console.WriteLine("--> List is empty. Exceptions should occur here... <--");
            try
            {
                Console.WriteLine(myList[0]);
            }
            catch (Exception error)
            {
                Console.WriteLine("Error occurred: " + error.Message);
            }
            // Test SetData exception is properly working for an empty list.
            try
            {
                myList[0] = 12345.6789;
            }
            catch (Exception error)
            {
                Console.WriteLine("Error occurred: " + error.Message);
            }


            // -----------------------------------
            // ----- Add values to list Test -----
            // ------------------------------------

            // Add data to the list
            myList.Add(2.5);
            myList.Add(4.2);
            myList.Add(592);
            myList.Add(912.68);
            myList.Add(1943.131);


            // ---------------------------
            // ----- GetData Testing -----
            // ---------------------------

            // Print all data in the list by retrieving the value at each index
            Console.WriteLine("\n--> Values in my list BEFORE modification<--");
            for (int i = 0; i < myList.Count; i++)
            {
                Console.WriteLine(myList[i]);
            }


            // ---------------------------
            // ----- SetData Testing -----
            // ---------------------------

            // Test the SetData method on the first and last items in the list
            //   and reprint the list's contents for confirmation.
            myList[0] = 0;
            myList[myList.Count - 1] = myList.Count - 1;

            Console.WriteLine("\n--> Values in my list AFTER modification <--");
            for (int i = 0; i < myList.Count; i++)
            {
                Console.WriteLine(myList[i]);
            }


            // ----------------------------------------------------
            // ----- GetData Exceptions with Data in the List -----
            // ----------------------------------------------------

            // Test that GetData's exceptions are properly working for
            //    a list that contains data. Test -1 and a too-large index.
            Console.WriteLine("\n--> List contains data. " +
                "Exceptions should occur with invalid indices... <--");
            try
            {
                Console.WriteLine(myList[-1]);
            }
            catch(Exception error)
            {
                Console.WriteLine("Error occurred: " + error.Message);
            }

            try
            {
                Console.WriteLine(myList[myList.Count]);
            }
            catch (Exception error)
            {
                Console.WriteLine("Error occurred: " + error.Message);
            }


            // ----------------------------------------------------
            // ----- GetData Exceptions with Data in the List -----
            // ----------------------------------------------------

            // Test that the SetData's exceptions are properly working for
            //    a list that contains data. Test -1 and a too-large index.
            try
            {
                myList[-1] = 12345.6789;
            }
            catch (Exception error)
            {
                Console.WriteLine("Error occurred: " + error.Message);
            }

            try
            {
                myList[myList.Count] = 12345.6789;
            }
            catch (Exception error)
            {
                Console.WriteLine("Error occurred: " + error.Message);
            }


            // **************************************************
            // * Answer the following questions:                *
            // *   Using try/catch blocks allows a programmer   *
            // *   to test a class's exception handling.        *
            // *   However, it's optimal to PREVENT any crashes *
            // *   with conditionals in your code when you can. *
            // * - How could GetData or SetData crashes be      *
            // *   prevented with conditionals?                 *
            // * - Explain or write the code.                   *
            // **************************************************
            // ANSWER(s): 
            //
            //


            // ------------------------------------
            // ----- Count and Capacity Tests -----
            // ------------------------------------

            // Test the count and capacity properties.
            Console.WriteLine("\nCount: " + myList.Count);
            Console.WriteLine("Capacity: " + myList.Capacity);
        }
    }
}