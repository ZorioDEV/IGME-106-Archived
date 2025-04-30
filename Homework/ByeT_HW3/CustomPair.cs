using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ByeT_HW3
{
    /// <summary>
    /// A generic class that holds the key and value of the index in the dictionary.
    /// </summary>
    /// <typeparam name="T">Generic Key Type</typeparam>
    /// <typeparam name="U">Generic Value Type</typeparam>
    internal class CustomPair<T,U>
    {
        // *** FEILDS ***
        private T key;
        private U value;

        // *** PROPERTIES ***
        /// <summary>
        /// Read and Write Property for the Key value
        /// </summary>
        public T Key {
            get
            {
                return key;
            }
            set
            {
                key = value;
            }
        }

        /// <summary>
        /// Read and Write Property for the Value value
        /// </summary>
        public U Value
        {
            get
            {
                return value;
            }
            set
            {
                this.value = value;
            }
        }

        // *** CONTRUCTOR ***
        /// <summary>
        /// Main constructor for the index and initaializes the custom pair's key and value.
        /// </summary>
        /// <param name="key">Key value of the pair</param>
        /// <param name="value">Value value of the pair</param>
        public CustomPair(T key, U value)
        {
            this.key = key;
            this.value = value;
        }
    }
}
