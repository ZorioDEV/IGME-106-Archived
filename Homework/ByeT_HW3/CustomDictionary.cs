using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ByeT_HW3
{
    /// <summary>
    /// Custom dictionary class using generic values for key and value types.
    /// </summary>
    /// <typeparam name="T">Generic Key Type</typeparam>
    /// <typeparam name="U">Generic Value Type</typeparam>
    internal class CustomDictionary<T,U>
    {
        // *** FIELDS ***
        private List<CustomPair<T, U>>[] data;
        private int count;

        // *** PROPERTIES ***
        /// <summary>
        /// Read-ONLY Property for the count.
        /// </summary>
        public int Count
        {
            get
            {
                return count;
            }
        }

        /// <summary>
        /// Read-ONLY Property for the load factor calculation.
        /// </summary>
        public double LoadFactor
        {
            get
            {
                return (double)count / data.Length;
            }
        }

        /// <summary>
        /// Indexer to get or set a value based on a given key.
        /// </summary>
        /// <param name="key">Specific Key Value</param>
        /// <returns>The value associated with the key.</returns>
        /// <exception cref="KeyNotFoundException">Thrown when the key does not exist.</exception>
        public U this[T key]
        {
            get 
            {
                // gets the hash index of the key
                int index = GetIndex(key);

                // test if the index of the key contains a value
                if (data[index] != null)
                {
                    // loops through all the pairs at that index
                    foreach (CustomPair<T,U> customPair in data[index])
                    {
                        // tests if the pair key equals the key given
                        if (customPair.Key!.Equals(key))
                        {
                            // returns the value of that key
                            return customPair.Value;
                        }
                    }
                }

                // thrown if the key doesn't exist
                throw new KeyNotFoundException("That key does not exist.");
            }
            set
            {
                // gets the hash index of the key
                int index = GetIndex(key);

                // test if the index of the key is empty
                if (data[index] == null)
                {
                    // if nothing is there, it creates a new list of pairs at that index
                    data[index] = new List<CustomPair<T, U>>();
                }

                // loops through all the pairs at that index
                foreach (CustomPair<T, U> customPair in data[index])
                {
                    // tests if the pair key equals the key given
                    if (customPair.Key!.Equals(key))
                    {
                        // updates the key's value the new value
                        customPair.Value = value;
                        return;
                    }
                }

                // if the key doesn't exist, it adds a new key and value pair
                data[index].Add(new CustomPair<T, U>(key, value));
                // increases the size & count of the dictionary
                count++;
            }
        }

        // *** CONSTRUCTORS ***
        /// <summary>
        /// Default constructor of the dictionary with a default size of 100.
        /// </summary>
        public CustomDictionary()
        {
            data = new List<CustomPair<T, U>>[100];
            count = 0;
        }

        /// <summary>
        /// Paramaterized constructor of the dictionary with a specified array size.
        /// </summary>
        /// <param name="arraySize">specific array size</param>
        public CustomDictionary(int arraySize)
        {
            data = new List<CustomPair<T, U>>[arraySize];
            count = 0;
        }

        // *** HASH FUNCTION ***
        /// <summary>
        /// Custom index assignment function for a given key using its hash code.
        /// </summary>
        /// <param name="key">Specific Key value</param>
        /// <returns>Calculated index for the key within the array</returns>
        public int GetIndex(T key)
        {
            // gets the hash code of the key
            int hash = key!.GetHashCode();

            // calculates the index so that its positive and within the bound of the array
            return Math.Abs(hash % data.Length);
        }

        // *** METHODS ***
        /// <summary>
        /// Searched the dictonary and tells if it contains a specific key.
        /// </summary>
        /// <param name="key">Specific Key value</param>
        /// <returns>True or False</returns>
        public bool ContainsKey(T key)
        {
            // gets the hash code of the key
            int index = GetIndex(key);

            // test if the index of the key contains a value
            if (data[index] != null)
            {
                // loops through all the pairs at that index
                foreach (CustomPair<T,U> customPair in data[index])
                {
                    // tests if the pair key equals the key given
                    if (customPair.Key!.Equals(key))
                    {
                        // returns true, telling the user it exists witin the dictionary
                        return true;
                    }
                }
            }

            // returns false, telling the user it doesn't exist witin the dictionary
            return false;
        }

        /// <summary>
        /// Adds a new key and value pair to the dictionary.
        /// </summary>
        /// <param name="key">New Key value</param>
        /// <param name="value">New Value value</param>
        /// <exception cref="ArgumentException">Thrown when the key already exists.</exception>
        public void Add(T key, U value)
        {
            // test if the key already exists within the dictonary
            if (ContainsKey(key))
            {
                throw new ArgumentException("Error! That key already exists.");
            }

            // // gets the hash code of the key
            int index = GetIndex(key);

            // test if the index of the key is empty
            if (data[index] == null)
            {
                // if nothing is there, it creates a new list of pairs at that index
                data[index] = new List<CustomPair<T, U>>();
            }

            // if the key doesn't exist, it adds a new key and value pair
            data[index].Add(new CustomPair<T, U>(key, value));
            // increases the size & count of the dictionary
            count++;
        }

        /// <summary>
        /// Removes a key and value pair to the dictionary.
        /// </summary>
        /// <param name="key">Specific Key value</param>
        /// <returns>True or False</returns>
        public bool Remove(T key)
        {
            // gets the hash code of the key
            int index = GetIndex(key);

            // test if the index of the key contains a value
            if (data[index] != null)
            {
                // loops through all the indexes of the dictionary
                for (int i = 0; i < data[index].Count; i++)
                {
                    // tests if the pair key equals the key given
                    if (data[index][i].Key!.Equals(key))
                    {
                        // removes the index of the key
                        data[index].RemoveAt(i);

                        // decreases the size & count of the dictionary
                        count--;

                        // returns true, telling the user it exsits and was removed
                        return true;
                    }
                }
            }

            // returns false, telling the user it doesn't exist witin the dictionary
            return false;
        }

        /// <summary>
        /// Clears all data within the dictionary.
        /// </summary>
        public void Clear()
        {
            // loops through all the indexes of the dictionary
            for (int i = 0; i < data.Length; i++)
            {
                // updates all the indexes to be null and empty
                data[i] = null!;
            }

            // resets the count to zero
            count = 0;
        }
    }
}
