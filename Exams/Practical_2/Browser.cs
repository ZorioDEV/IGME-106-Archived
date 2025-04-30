using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practical_2
{
    /// <summary>
    /// Browser's search history data structure.
    /// </summary>
    class Browser
    {
        // *** FIELDS ***
        Stack<string> backwards = new();
        Stack<string> forwards = new();

        // *** CONSTRUCTOR ***
        /// <summary>
        /// Default constructor for the browser. (NOT NEEDED)
        /// </summary>
        public Browser()
        {
            
        }

        // *** METHODS ***
        /// <summary>
        /// Closes & removes all browser data.
        /// </summary>
        internal void CloseBrowser()
        {
            // pops out & goes through all of backwards data
            while (backwards.Count != 0)
            {
                backwards.Pop();
            }

            // pops out & goes through all of forwards data
            while (forwards.Count != 0)
            {
                forwards.Pop();
            }

            Console.WriteLine("Broswer closed.");
        }

        /// <summary>
        /// Moves the webpage backwards to previous page.
        /// </summary>
        internal void MoveBackward()
        {
            // checks if the page is the last in the stack
            if (backwards.Count > 1)
            {
                // moves to the forwards data
                forwards.Push(backwards.Pop());

                this.PrintCurrentPage();
                this.PrintHistory();
            }
            else
            {
                Console.WriteLine("Cannot go back.");
            }
        }

        /// <summary>
        /// Moves the webpage forwards to previous page.
        /// </summary>
        internal void MoveForward()
        {
            // checks if the forwards stack is empty
            if (forwards.Count != 0)
            {
                // moves to the forwards data
                backwards.Push(forwards.Pop());

                this.PrintCurrentPage();
                this.PrintHistory();
            }
            else
            {
                Console.WriteLine("Cannot go forward.");
            }
        }

        /// <summary>
        /// Prints the current page the user is on.
        /// </summary>
        internal void PrintCurrentPage()
        {
            Console.WriteLine("-----Current Page-----");

            // checks if backwards stack is empty
            if (backwards.Count != 0)
            {
                Console.WriteLine(backwards.Peek());
            }
            else
            {
                Console.WriteLine("<none>");
            }
        }

        /// <summary>
        /// Prints out all backwards and forwards data.
        /// </summary>
        internal void PrintHistory()
        {
            Console.WriteLine("-----Backwards stack-----");

            // checks if backwards stack is empty
            if (backwards.Count != 0)
            {
                Stack<string> temp = new();

                // goes through all of backwards data
                while (backwards.Count != 0)
                {
                    Console.WriteLine(backwards.Peek());

                    // temporarily pops out backwards to a different stack
                    temp.Push(backwards.Pop());
                }

                // places the data back into the backwards stack in order
                while (temp.Count != 0)
                {
                    backwards.Push(temp.Pop());
                }
            }
            else
            {
                Console.WriteLine("<empty>");
            }

            Console.WriteLine("-----Forwards stack-----");

            // checks if forwards stack is empty
            if (forwards.Count != 0)
            {
                Stack<string> temp = new();

                // goes through all of backwards data
                while (forwards.Count != 0)
                {
                    Console.WriteLine(forwards.Peek());

                    // temporarily pops out forwards to a different stack
                    temp.Push(forwards.Pop());
                }

                // places the data back into the forwards stack in order
                while (temp.Count != 0)
                {
                    forwards.Push(temp.Pop());
                }
            }
            else
            {
                Console.WriteLine("<empty>");
            }
        }

        /// <summary>
        /// Visits a certain page & makes it the current webpage.
        /// </summary>
        /// <param name="page">Uesr's specified webpage</param>
        internal void VisitPage(string page)
        {
            // places the page on top of the backwards stack
            backwards.Push(page);

            // pops out & goes through all of forwards data
            while (forwards.Count != 0)
            {
                forwards.Pop();
            }

            this.PrintCurrentPage();
            this.PrintHistory();
        }
    }
}
