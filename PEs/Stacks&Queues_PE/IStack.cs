using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stacks_Queues_PE
{
    /// <summary>
    /// Interface that sets rules for GameStack
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal interface IStack<T>
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
        /// Returns the item at the top of the stack.
        /// </summary>
        /// <returns>Item at the top of the stack</returns>
        T Peek();

        /// <summary>
        /// Places the item onto the top of the stack.
        /// </summary>
        /// <param name="item">Item that is being added</param>
        public void Push(T item);

        /// <summary>
        /// Removes and returns the item at the top of the stack.
        /// </summary>
        /// <returns>Item at the top of the stack that is being removed</returns>
        T Pop();
    }
}
