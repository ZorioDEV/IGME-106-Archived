using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Practical_3
{
    /// <summary>
    /// Node/Object within the LinkedStack
    /// </summary>
    /// <typeparam name="T">Generic type of data</typeparam>
    class StackNode<T>
    {
        // *** FIELDS ***
        private T data;
        private StackNode<T> next;

        // *** PROPERTIES ***
        /// <summary>
        /// READ and WRITE Property of node's data.
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
        /// READ and WRITE Property of the next node reference.
        /// </summary>
        public StackNode<T> Next
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

        // *** CONSTRUCTOR ***
        /// <summary>
        /// Main constructor of the node, sets defualt values.
        /// </summary>
        /// <param name="data"></param>
        public StackNode(T data)
        {
            this.data = data;
            this.next = null!;
        }
    }
}
