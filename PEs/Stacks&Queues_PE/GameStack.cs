using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stacks_Queues_PE
{
    /// <summary>
    /// Custom stack data structure
    /// </summary>
    /// <typeparam name="T">Generic variable</typeparam>
    internal class GameStack<T> : IStack<T>
    {
        private List<T> items = new List<T>();

        /// <summary>
        /// Read-ONLY property of the queue count.
        /// </summary>
        public int Count
        {
            get { return items.Count; }
        }

        /// <summary>
        /// Read-ONLY property which returns if the queue is empty.
        /// </summary>
        public bool IsEmpty
        {
            get { return items.Count == 0; }
        }

        /// <summary>
        /// Returns the item at the top of the stack.
        /// </summary>
        /// <returns>Item at the top of the stack</returns>
        /// <exception cref="InvalidOperationException">Thrown when attempting to peek at an empty stack</exception>
        public T Peek()
        {
            // tests if the stack is empty before peeking
            if (IsEmpty)
            {
                throw new InvalidOperationException("Cannot peek at an empty stack.");
            }

            return items[items.Count - 1];
        }

        /// <summary>
        /// Places the item onto the top of the stack.
        /// </summary>
        /// <param name="item">Item that is being added</param>
        public void Push(T item)
        {
            items.Add(item);
        }

        /// <summary>
        /// Removes and returns the item at the top of the stack.
        /// </summary>
        /// <returns>Item at the top of the stack that is being removed</returns>
        /// <exception cref="InvalidOperationException">Thrown when attempting to pop from an empty stack.</exception>
        public T Pop()
        {
            // tests if the stack is empty before popping
            if (IsEmpty)
            {
                throw new InvalidOperationException("Cannot pop from an empty stack.");
            }

            // gets the last item and removes it from the list
            T item = items[items.Count - 1];
            items.RemoveAt(items.Count - 1);

            return item;
        }
    }
}
