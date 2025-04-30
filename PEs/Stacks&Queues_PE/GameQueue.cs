using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stacks_Queues_PE
{
    /// <summary>
    /// Custom queue data structure
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class GameQueue<T> : IQueue<T>
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
        /// Returns the item at the front of the queue.
        /// </summary>
        /// <returns>Item at the front of the queue</returns>
        /// <exception cref="InvalidOperationException">Thrown when attempting to peek at an empty stack</exception>
        public T Peek()
        {
            // tests if the queue is empty before peeking
            if (IsEmpty)
            {
                throw new InvalidOperationException("Cannot peek at an empty queue.");
            }
                
            return items[0];
        }

        /// <summary>
        /// Adds an item to the end of the queue.
        /// </summary>
        /// <param name="item">Item that is being added</param>
        public void Enqueue(T item)
        {
            items.Add(item);
        }

        /// <summary>
        /// Removes and returns the item at the front of the queue.
        /// </summary>
        /// <returns>Item at the front of the queue that is being removed</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public T Dequeue()
        {
            // tests if the queue is empty before dequeuing
            if (IsEmpty)
            {
                throw new InvalidOperationException("Cannot dequeue from an empty queue.");
            }

            // gets the last item and removes it from the list
            T item = items[0];
            items.RemoveAt(0);

            return item;
        }
    }
}
