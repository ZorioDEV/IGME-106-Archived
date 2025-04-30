using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Practical_3
{
    class LinkedStack<T>
    {
        // *** FIELDS ***
        private StackNode<T> top;
        private int count;

        // *** PROPERTIES ***
        /// <summary>
        /// READ and WRITE of the reference to the top node.
        /// </summary>
        public StackNode<T> Top
        {
            get
            {
                return top;
            }
            set
            {
                top = value;
            }
        }

        /// <summary>
        /// READ-ONLY Property of the stack's count.
        /// </summary>
        public int Count
        {
            get
            {
                return count;
            }
        }

        // *** CONSTRUCTOR ***
        /// <summary>
        /// Main constructor of the stack, sets defualt values.
        /// </summary>
        public LinkedStack()
        {
            this.count = 0;
            this.top = null!;
        }

        // *** METHODS ***
        /// <summary>
        /// Pushing/Adding a new node onto the stack.
        /// </summary>
        /// <param name="v">Data of the new node</param>
        public void Push(T v)
        {
            StackNode<T> newNode = new StackNode<T>(v);

            // checks if the stack is empty
            if (Count == 0)
            {
                Top = newNode;
            }
            else
            {
                newNode.Next = Top;

                Top = newNode;
            }

            // increases the count by 1
            count++;
        }

        /// <summary>
        /// Pops/Removes the node from the top of the stack.
        /// </summary>
        /// <returns>string of the popped node's data</returns>
        public string Pop()
        {
            StackNode<T> newNode = new StackNode<T>(Top.Data);

            // checks if the stack is empty
            if (Count != 0)
            {
                Top = Top.Next; 
            }
            else
            {
                Top = null!;
            }

            // decreases the count by 1
            count--;

            // returns the popped node's data
            return newNode.Data!.ToString()!;
        }

        /// <summary>
        /// Peeks at the top node in the stack.
        /// </summary>
        /// <returns>string of the peeked node's data</returns>
        public string Peek()
        {
            return Top.Data!.ToString()!;
        }

        /// <summary>
        /// Clears all of the stacks data by reseting to default values.
        /// </summary>
        public void Clear()
        {
            Top = null!;
            count = 0;
        }

        /// <summary>
        /// Prints all the of stack's node's data in order iteratively.
        /// </summary>
        public void PrintDataIterative()
        {
            LinkedStack<T> temp = new LinkedStack<T>();

            // checks if the stack is empty
            if (Top != null)
            {
                // prints the stacks data in order
                for (int i = 0; i < Count; i++)
                {
                    Console.WriteLine($" - {Top.Data!.ToString()!}");

                    temp.Push(Top.Data);

                    Top = Top.Next;
                }

                // pushing data back into correct order
                for (int i = 0; i < Count; i++)
                {
                    //this.Push(temp.Pop());
                }
            }
            else
            {
                Console.WriteLine("No data to print because stack is empty.");
            }
        }

        /// <summary>
        /// Prints all the of stack's node's data in order recursively.
        /// </summary>
        public void PrintDataRecursive()
        {
            // checks if the stack is empty
            if (Top != null)
            {
                StackNode<T> newNode = new StackNode<T>(Top.Data);

                // prints the stacks data in order
                Console.WriteLine($" - {newNode.Data!.ToString()!}");
                newNode.Next = newNode;
                PrintDataRecursive();
            }
            else
            {
                Console.WriteLine("No data to print because stack is empty.");
            }
        }
    }
}
