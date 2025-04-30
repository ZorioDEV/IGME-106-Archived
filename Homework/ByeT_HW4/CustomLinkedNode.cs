using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ByeT_HW4
{
    /// <summary>
    /// Custom node within the Linked List that holds data and points to the next & previous node.
    /// </summary>
    /// <typeparam name="T">Data the node is holding</typeparam>
    public class CustomLinkedNode<T>
    {
        // *** FIELDS ***
        private CustomLinkedNode<T> next;
        private CustomLinkedNode<T> prev;
        private T data;

        // *** PROPERTIES ***
        /// <summary>
        /// Read and write Property of data
        /// </summary>
        public T Data
        {
            get
            {
                return data;
            }
            set
            {
                data = value;
            }
        }

        /// <summary>
        /// Read and write Property of next
        /// </summary>
        public CustomLinkedNode<T> Next
        {
            get
            {
                return next;
            }
            set
            {
                next = value;
            }
        }

        /// <summary>
        /// Read and write Property of prev
        /// </summary>
        public CustomLinkedNode<T> Prev
        {
            get
            {
                return prev;
            }
            set
            {
                prev = value;
            }
        }

        // *** CONSTRUCTORS ***
        /// <summary>
        /// Creates base data for a new node and references the next node.
        /// </summary>
        /// <param name="data">Data to store in this node</param>
        public CustomLinkedNode(T data)
        {
            Data = data;
            Next = null!;
            Prev = null!;
        }
    }
}
