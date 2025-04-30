using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stacks_Queues_PE
{
    /// <summary>
    /// Interface that sets rules for GameQueue
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal interface IQueue<T>
    {
        /// <summary>
        /// Read-ONLY property of the queue count.
        /// </summary>
        public int Count { get; }

        /// <summary>
        /// Read-ONLY property which returns if the queue is empty.
        /// </summary>
        public bool IsEmpty { get; }

        /// <summary>
        /// Returns the item at the front of the queue.
        /// </summary>
        /// <returns>Item at the front of the queue</returns>
        T Peek();

        /// <summary>
        /// Adds an item to the end of the queue.
        /// </summary>
        /// <param name="item">Item that is being added</param>
        public void Enqueue(T item);

        /// <summary>
        /// Removes and returns the item at the front of the queue.
        /// </summary>
        /// <returns>Item at the front of the queue that is being removed</returns>
        T Dequeue();
    }
}
