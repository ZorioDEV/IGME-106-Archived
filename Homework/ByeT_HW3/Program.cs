using System.Collections;
using System.Collections.Generic;
using System.Globalization;

// Tyler Bye
// 03/04/25
// Create custom dictionary data structure

namespace ByeT_HW3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // *** OBJECTS & VARIABLES ***
            CustomDictionary<string, string> myDictionary = new CustomDictionary<string, string>(5);

            myDictionary.Add("pizza", "cheese");
            myDictionary.Add("hamburger", "beef");
            myDictionary.Add("taco", "chicken");
            myDictionary["milkshake"] = "cookies and cream";
            myDictionary["stir-fry"] = "vegetables";
            myDictionary["pretzel"] = "salted";

            string userInput = "";
            string userKey = "";
            string userValue = "";

            // *** MAIN CODE ***
            // runs the menu options until the user types "quit"
            while (userInput != "quit")
            {
                // prints the menu options
                Console.WriteLine("\nCustom Dictionary menu:" +
                    "   Count" +
                    "   LoadFactor" +
                    "   Add" +
                    "   Remove" +
                    "   Get" +
                    "   Set" +
                    "   Clear" +
                    "   Quit"
                );
                Console.Write(">> ");

                // stores the user's input and converts it to lowercase
                userInput = Console.ReadLine()!.ToLower();

                // checks which option the user inputed
                switch (userInput)
                {
                    // prints the count of entries in the dictionary to the user
                    case "count":
                        Console.WriteLine($"The dictionary has {myDictionary.Count} entries");
                        break;
                    // prints the load factor value to the user
                    case "loadfactor":
                        Console.WriteLine($"The dictionary has a load factor of {myDictionary.LoadFactor}");
                        break;
                    // adds a new key and value pair to the dictionary
                    case "add":
                        // asks user for a key & stores it
                        Console.Write("Type a key: ");
                        userKey = Console.ReadLine()!.ToLower();

                        // asks user for a value & stores it
                        Console.Write("Type a value: ");
                        userValue = Console.ReadLine()!.ToLower();

                        // tries to add the new key and value pair
                        try
                        {
                            myDictionary.Add(userKey, userValue);
                            Console.WriteLine($"The key '{userKey}' was added");
                        }
                        // catches if the key already exists 
                        catch (ArgumentException error)
                        {
                            Console.WriteLine(error.Message);
                        }
                        break;
                    // adds a new key and value pair to the dictionary using the key name
                    case "remove":
                        // ask the user for a key
                        Console.Write("Type a key: ");
                        userKey = Console.ReadLine()!.ToLower();

                        // tests if the key exsits within the dictionary & removes the key and value pair
                        if (myDictionary.Remove(userKey))
                        {
                            // prints the confirmation that the key has been removes
                            Console.WriteLine($"The key '{userKey}' was removed");
                        }
                        // prints that the key doesn't exists within the dictionary
                        else
                        {
                            Console.WriteLine($"The key '{userKey}' is not in the dictionary");
                        }
                        break;
                    // retrieves the value associated with a key
                    case "get":
                        // ask the user for a key
                        Console.Write("Type a key: ");
                        userKey = Console.ReadLine()!.ToLower();

                        // tests if the key exsits within the dictionary
                        if (myDictionary.ContainsKey(userKey))
                        {
                            // prints the value of the key
                            Console.WriteLine($"The Key's value is '{myDictionary[userKey]}'");
                        }
                        // prints that the key doesn't exists within the dictionary
                        else
                        {
                            Console.WriteLine($"That key does not exist.");
                        }
                        break;
                    // sets a new value for an existing key (or creates a new key and value pair)
                    case "set":
                        // ask the user for a key
                        Console.Write("Type a key: ");
                        userKey = Console.ReadLine()!.ToLower();

                        // ask the user for a value
                        Console.Write("Type a value: ");
                        userValue = Console.ReadLine()!.ToLower();

                        // tests if the key exsits within the dictionary
                        if (myDictionary.ContainsKey(userKey))
                        {
                            // sets the new value for the specific key & prints the confirmation
                            myDictionary[userKey] = userValue;
                            Console.WriteLine($"The value was changed for the key '{userKey}'");
                        }
                        // if the key doesn't exists within the dictionary
                        else
                        {
                            // tries to add the new key and value pair
                            try
                            {
                                myDictionary.Add(userKey, userValue);
                                Console.WriteLine($"The key '{userKey}' was added");
                            }
                            // catches if the key already exists (should be impossible here)
                            catch (ArgumentException error)
                            {
                                Console.WriteLine(error.Message);
                            }
                        }
                        break;
                    // clears the entire dictionary
                    case "clear":
                        myDictionary.Clear();
                        Console.WriteLine("Dictionary was cleared");
                        break;
                    // quits the program and tells the user goodbye
                    case "quit":
                        Console.WriteLine("Goodbye!");
                        break;
                    // prints when user doesn't input a possible option
                    default:
                        Console.WriteLine("Error! Invalid input.");
                        break;
                }
            }
        }
    }
}
