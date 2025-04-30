using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ByeT_HW4
{
    /// <summary>
    /// Custom Linked List data structurue.
    /// </summary>
    /// <typeparam name="T">Data within that index of the list</typeparam>
    public class CustomLinkedList<T>
    {
        // *** FIELDS ***
        private CustomLinkedNode<T> head;
        private CustomLinkedNode<T> tail;
        private int count;

        // *** PROPERTIES ***
        /// <summary>
        /// Read-ONLY Property of count
        /// </summary>
        public int Count
        {
            get
            {
                return count;
            }
        }

        /// <summary>
        /// Gets the element at the specified index.
        /// </summary>
        /// <param name="index">Index of the element to get</param>
        /// <returns>Element at the specified index</returns>
        public T this[int index]
        {
            get
            {
                return GetData(index);
            }
            set
            {
                SetData(index, value);
            }
        }

        // *** CONSTRUCTORS ***
        /// <summary>
        /// Creates a new empty instance of the CustomLinkedList class
        /// </summary>
        public CustomLinkedList()
        {
            head = null!;
            tail = null!;
            count = 0;
        }

        // *** METHODS & HELPERS ***
        /// <summary>
        /// Gets the node at the specified index or null if index is invalid.
        /// </summary>
        /// <param name="index">Index of the element to get</param>
        /// <returns>Node of the index or error</returns>
        private CustomLinkedNode<T> GetNode(int index)
        {
            // checks if the index is within range
            if (index < 0 || index >= count)
            {
                return null!;
            }

            // determines which direction to traverse
            if (index < count / 2)
            {
                // starts at the head node
                CustomLinkedNode<T> current = head;

                // loops through the list to the index's position
                for (int i = 0; i < index; i++)
                {
                    current = current.Next;
                }

                // returns the found node
                return current;
            }
            else
            {
                // starts at the tail node
                CustomLinkedNode<T> current = tail;

                // loops backward through the list to the index's position
                for (int i = count - 1; i > index; i--)
                {
                    current = current.Prev;
                }

                // returns the found node
                return current;
            }
        }

        /// <summary>
        /// Gets the data from the node at the specified index.
        /// </summary>
        /// <param name="index">Index of the element to get</param>
        /// <returns>Data of the index or error</returns>
        /// <exception cref="ArgumentException"></exception>
        private T GetData(int index)
        {
            // checks if the nodes exists
            CustomLinkedNode<T> node = GetNode(index);

            // throws an exception if the node doesn't exist
            if (node == null)
            {
                throw new ArgumentException($"Error: Cannot get data at invalid index {index}.");
            }

            // returns the node's data
            return node.Data;
        }

        /// <summary>
        /// Sets the data at the specified index.
        /// </summary>
        /// <param name="index">Index to set data at</param>
        /// <param name="data">Data to set</param>
        private void SetData(int index, T data)
        {
            // checks if the nodes exists
            CustomLinkedNode<T> node = GetNode(index);

            // throws an exception if the node doesn't exist
            if (node == null)
            {
                throw new ArgumentException($"Error: Cannot set data at invalid index {index}.");
            }

            // sets the new data to the node
            node.Data = data;
        }

        /// <summary>
        /// Adds an item to the end of the list.
        /// </summary>
        /// <param name="data">Data that is being added</param>
        public void Add(T data)
        {
            // creates a new node
            CustomLinkedNode<T> newNode = new CustomLinkedNode<T>(data);

            // checks if the head is empty/null
            if (head == null)
            {
                // creates new node and adds tail
                head = newNode;
                tail = head;
            }
            // adds new node to the end of the list
            else
            {
                // makes new node the last node of the list
                newNode.Prev = tail;
                tail.Next = newNode;
                tail = newNode;
            }

            // increases the count size
            count++;
        }

        /// <summary>
        /// Removes the data from the node at the specified index.
        /// </summary>
        /// <param name="index">Index of the element to get</param>
        /// <returns>Data of the index or error</returns>
        /// <exception cref="ArgumentException"></exception>
        public T RemoveAt(int index)
        {
            // checks if the list is empty
            if (count == 0)
            {
                throw new ArgumentException("Cannot remove data from empty list.");
            }

            // checks if the index is valid
            if (index < 0 || index >= count)
            {
                throw new ArgumentException($"Error: Cannot remove invalid index {index}.");
            }

            // handles the case where the list has only one element
            if (count == 1)
            {
                T data = head.Data;
                head = null!;
                tail = null!;
                count--;
                return data;
            }

            // handles the case where the head is being removed
            if (index == 0)
            {
                T data = head.Data;
                head = head.Next;
                head.Prev = null!;
                count--;
                return data;
            }

            // handles the case where the tail is being removed
            if (index == count - 1)
            {
                T data = tail.Data;
                tail = tail.Prev;
                tail.Next = null!;
                count--;
                return data;
            }

            // handles the case where a middle node is being removed
            CustomLinkedNode<T> currentNode = GetNode(index);
            T removedData = currentNode.Data;

            // updates surrounding nodes' references
            currentNode.Prev.Next = currentNode.Next;
            currentNode.Next.Prev = currentNode.Prev;

            // decreases the count size & returns data
            count--;
            return removedData;
        }

        /// <summary>
        /// Inserts data at the specified index.
        /// </summary>
        /// <param name="data">Data to insert</param>
        /// <param name="index">Index to insert at</param>
        /// <exception cref="ArgumentException">Thrown if index is invalid</exception>
        public void Insert(T data, int index)
        {
            // checks if the index is valid
            if (index < 0 || index > count)
            {
                throw new ArgumentException($"Error: Cannot insert into invalid index {index}.");
            }

            // creates a new node
            CustomLinkedNode<T> newNode = new CustomLinkedNode<T>(data);

            // inserts into empty list
            if (count == 0)
            {
                head = newNode;
                tail = newNode;
            }
            // inserts at head
            else if (index == 0)
            {
                newNode.Next = head;
                head.Prev = newNode;
                head = newNode;
            }
            // inserts at tail
            else if (index == count)
            {
                newNode.Prev = tail;
                tail.Next = newNode;
                tail = newNode;
            }
            // inserts in middle
            else
            {
                CustomLinkedNode<T> current = GetNode(index);
                newNode.Next = current;
                newNode.Prev = current.Prev;
                current.Prev.Next = newNode;
                current.Prev = newNode;
            }

            // increases the count size
            count++;
        }

        /// <summary>
        /// Clears all elements from the list.
        /// </summary>
        public void Clear()
        {
            head = null!;
            tail = null!;
            count = 0;
        }

        /// <summary>
        /// Prints the list in forward order.
        /// </summary>
        public void PrintForward()
        {
            // checks if the list is empty
            if (count == 0)
            {
                Console.WriteLine("There are no items in the list.");
                return;
            }

            // starts at the head and prints through the list
            CustomLinkedNode<T> current = head;
            while (current != null)
            {
                Console.WriteLine(current.Data);
                current = current.Next;
            }
        }

        /// <summary>
        /// Prints the list in backward order.
        /// </summary>
        public void PrintBackward()
        {
            // checks if the list is empty
            if (count == 0)
            {
                Console.WriteLine("There are no items in the list.");
                return;
            }

            // starts at the tail and prints backwards through the list
            CustomLinkedNode<T> current = tail;
            while (current != null)
            {
                Console.WriteLine(current.Data);
                current = current.Prev;
            }
        }
    }
}
