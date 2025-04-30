using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomList_STARTER
{
    /// <summary>
    /// A list of doubles with custom written methods.
    /// </summary>
    internal class CustomList<T>
    {
        // --------------------------------------------------------------------
        // Fields of the class
        // --------------------------------------------------------------------

        private T[] data;      // Underlying array that holds all list data
        private int count;          // Size of the list

        // **************************************************
        // * Answer the following question:                 *
        // * - Why DON'T we need an extra field to hold     *
        // *   the list's capacity?                         *
        // **************************************************
        // ANSWER(s): 
        // The list's capacity can be obtained through data.Length, making another
        // variable not needed becuase there is already a path to it.


        // --------------------------------------------------------------------
        // Properties of the class
        // --------------------------------------------------------------------

        /// <summary>
        /// Returns the current amount of data in the list.
        /// </summary>
        public int Count
        {
            get { return count; }
        }

        // **************************************************
        // * Answer the following questions:                *
        // * - Why is the Count property get-only?          *
        // * - Why not include a set?                       *
        // **************************************************
        // ANSWER(s): 
        // We dont want to be able to alter the count and mess up the overall count 
        // from the outside classes. Including a set would allow for outside changes.


        /// <summary>
        /// Returns the overall number of elements the internal data structure
        /// can hold before a resize operation.
        /// </summary>
        public int Capacity
        {
            get { return data.Length; }
        }


        /*
        // NOOOOO! Don't do this! Included to show what NOT to do.
        public double[] Data
        {
            get { return data; }
            set { data = value; }
        }
        */
        // **************************************************
        // * Answer the following question:                 *
        // * - Why DON'T we want a property that gets or    *
        // *   sets the data array?                         *
        // **************************************************
        // ANSWER(s): 
        // Same as the count, we don't want outsider code affecting the data
        // array and messing with the functionality of the array's purpose.


        // --------------------------------------------------------------------
        // Indexer Property
        // --------------------------------------------------------------------

        /// <summary>
        /// Read & Write Indexer Properity of array.
        /// </summary>
        /// <param name="index">Index of the array</param>
        /// <returns>Gets or sets data value at index</returns>
        /// <exception cref="InvalidOperationException">Array is empty</exception>
        /// <exception cref="IndexOutOfRangeException">Index is out of array range</exception>
        public T this[int index]
        {
            get
            {
                // test if the array is empty
                if (count == 0)
                    throw new InvalidOperationException("No data to retrieve, list is empty.");

                // test if the index is within the valid range
                if (index < 0 || index >= count)
                    throw new IndexOutOfRangeException($"Index {index} is out of range. Index must be between 0 and {count - 1}.");

                // sends back generic value of index
                return data[index];
            }
            set
            {
                // test if the array is empty
                if (count == 0)
                    throw new InvalidOperationException("No data to change, list is empty.");

                // test if the index is within the valid range
                if (index < 0 || index >= count)
                    throw new IndexOutOfRangeException($"Index {index} is out of range. Index must be between 0 and {count - 1}.");

                // sets new value to specified index in array
                data[index] = value;
            }
        }


        // --------------------------------------------------------------------
        // Class Constructors
        // --------------------------------------------------------------------

        /// <summary>
        /// Instantiates a new list of doubles with a starting size of 4.
        /// </summary>
        public CustomList()
        {
            data = new T[4];
        }


        /// <summary>
        /// Instantiates a new list of doubles with a specified starting size.
        /// </summary>
        /// <param name="listSize">Initial size of the list.</param>
        public CustomList(int listSize)
        {
            data = new T[listSize];
        }


        // --------------------------------------------------------------------
        // Methods
        // --------------------------------------------------------------------

        /// <summary>
        /// Add new data to the list.
        /// </summary>
        /// <param name="item">Item to add to the next available spot.</param>
        public void Add(T item)
        {
            // test if the current number of elements reaches the array's maximum
            if (count == data.Length - 1)
            {
                // increases the size of the array to make more space
                IncreaseSizeAndCopyData();
            }

            // adds new item at next available position in the array
            data[count] = item;
            // increases count amount to reflect new size of the list
            count++;
        }


        // **************************************************
        // * Answer the following questions:                *
        // * - What is the purpose of a private method?     *
        // * - Why is the IncreaseSizeAndCopyData method    *
        // *   private?                                     *
        // **************************************************
        // ANSWER(s): 
        // Certain methods should not be excessable from outside classes and
        // this one adds to the array which should not be changed from outside code.

        /// <summary>
        /// Doubles the size of the data array.
        /// </summary>
        private void IncreaseSizeAndCopyData()
        {
            // tests if the resize is needed
            if (count != data.Length - 1)
            {
                // does nothing because array doesnt need an increase
                return;
            }


            // creates an arrays with twice the size of the previous array
            T[] largerCopy = new T[data.Length * 2];

            // loops through the old array and copies it into the new array
            for (int i = 0; i < data.Length; i++)
            {
                largerCopy[i] = data[i];
            }

            // replaces old array with new array
            data = largerCopy;
        }


        // **************************************************
        // * Answer the following questions:                *
        // * - Where is GetData's thrown exception being    *
        // *   caught?                                      *
        // * - Why is the thrown exception NOT caught       *
        // *   directly in the GetData method?              *
        // **************************************************
        // ANSWER(s): 
        // GetData's thrown exceptions are caught in the Program.cs so that the
        // function can be preformed properly and errors are handled appropriately.

        ///// <summary>
        ///// Retrieve data at an index.
        ///// </summary>
        ///// <param name="index">Integer index between 0 and the list's count.</param>
        ///// <returns>Data at a specified index.</returns>
        ///// <exception cref="Exception">Thrown exception when the index is out of range.</exception>
        //public T GetData(int index)
        //{
        //    // test if the index is within the valid range
        //    if (index >= 0 && index < count)
        //    {
        //        // returns back the value at the index
        //        return data[index];
        //    }

        //    // if the list is empty or the data cannot be recieve since its
        //    // out of range, it will throw the exception message there was an
        //    // error that occurred
        //    string exceptionMessage;
        //    if (count == 0)
        //    {
        //        exceptionMessage = "No data to retrieve, list is empty.";
        //    }
        //    else 
        //    {
        //        exceptionMessage = String.Format(
        //            "Index {0} is out of range. Index must be between 0 and {1}",
        //            index,
        //            count - 1);
        //    }
        //    throw new IndexOutOfRangeException(exceptionMessage);
        //}


        ///// <summary>
        ///// Updates an element in the list at the specified index.
        ///// </summary>
        ///// <param name="index">Index of the array</param>
        ///// <param name="newValue">New generic value</param>
        ///// <exception cref="IndexOutOfRangeException">Out of Range error</exception>
        //public void SetData(int index, T newValue)
        //{
        //    // test if the index is within the valid range
        //    if (index >= 0 && index < count)
        //    {
        //        // sets newValue to specified index in array
        //        data[index] = newValue;
        //    }
        //    else
        //    {
        //        // if the list is empty or the data cannot be recieve since its
        //        // out of range, it will throw the exception message there was an
        //        // error that occurred
        //        string exceptionMessage;
        //        if (count == 0)
        //        {
        //            exceptionMessage = "No data to changed, list is empty.";
        //        }
        //        else
        //        {
        //            exceptionMessage = $"Index {index} is out of range. Index must be between 0 and {count - 1}";
        //        }
        //        throw new IndexOutOfRangeException(exceptionMessage);
        //    }
        //}
    }
}
