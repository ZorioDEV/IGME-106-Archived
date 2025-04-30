using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedLists_PE
{
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

        // *** HELPERS ***
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
                throw new ArgumentException($"Invalid index of {index}. Index must be between 0 and {count - 1}.");
            }

            // returns the node's data
            return node.Data;
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
                // starts at the head node
                CustomLinkedNode<T> current = head;

                // loops through the list to reach the end
                while (current.Next != null)
                {
                    current = current.Next;
                }

                // makes new node the last node of the list
                current.Next = newNode;

                // assigns tail to last node
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
                throw new ArgumentException($"Invalid index of {index}. Index must be between 0 and {count - 1}.");
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
                count--;
                return data;
            }

            // handles the case where the tail is being removed
            if (index == count - 1)
            {
                T data = tail.Data;
                CustomLinkedNode<T> previous = GetNode(index - 1);
                previous.Next = null!;
                tail = previous;
                count--;
                return data;
            }

            // handles the case where a middle node is being removed
            CustomLinkedNode<T> previousNode = GetNode(index - 1);
            CustomLinkedNode<T> currentNode = previousNode.Next;
            T removedData = currentNode.Data;
            previousNode.Next = currentNode.Next;
            count--;
            return removedData;
        }
    }
}
