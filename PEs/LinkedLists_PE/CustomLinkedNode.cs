using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedLists_PE
{
    public class CustomLinkedNode<T>
    {
        // *** FIELDS ***
        private CustomLinkedNode<T> next;
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

        // *** CONSTRUCTORS ***
        /// <summary>
        /// Creates base data for a new node and references the next node.
        /// </summary>
        /// <param name="data">Data to store in this node</param>
        public CustomLinkedNode(T data)
        {
            Data = data;
            Next = null!;
        }
    }
}
