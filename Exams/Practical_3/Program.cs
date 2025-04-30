#region Practical Introduction
// -----------------------------------------------------------------
// ** TYLER BYE **
// Spring 2025 - Practical Exam 3
// April 30, 2025
// -----------------------------------------------------------------
//
// Students have the class period to complete this practical. 
// All myCourses dropbox submissions of GitHub releases must be
// before the end of the class period.
//
// Read the practical instructions carefully before starting!!!!!!!
//   _____                       _     _                  _      _ 
//  / ____|                     | |   | |                | |    | |
// | |  __    ___     ___     __| |   | |  _   _    ___  | | __ | |
// | | |_ |  / _ \   / _ \   / _` |   | | | | | |  / __| | |/ / | |
// | |__| | | (_) | | (_) | | (_| |   | | | |_| | | (__  |   <  |_|
//  \_____|  \___/   \___/   \__,_|   |_|  \__,_|  \___| |_|\_\ (_)
//
//  (ASCII art from textkool.com/en)
//
// p.s. You can minimize this region text.
//      Look for the tiny down arrow by the line numbers.
// -----------------------------------------------------------------
#endregion

namespace Practical_3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // ----------------------------------------------------------------
            // ---                     String stack                         ---
            // ----------------------------------------------------------------
            #region String Stack STARTER
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            Console.WriteLine("~~ Creating a string stack...              ~~");
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");

            // CREATE THE LINKED STACK OBJECT
            Console.WriteLine("\nInitializing the LinkedStack...");
            LinkedStack<string> stringStack = new LinkedStack<string>();

            // PUSH INITIAL DATA. INSPECT STACK TOP AND COUNT.
            Console.WriteLine("\nPUSHING strings 'Ant Man', 'Black Widow', 'Captain Marvel' " +
                "and 'Daredevil'...");
            stringStack.Push("Ant Man");
            stringStack.Push("Black Widow");
            stringStack.Push("Captain Marvel");
            stringStack.Push("Daredevil");
            Console.WriteLine($"Top of the stack: '{stringStack.Peek()}'");
            Console.WriteLine($"Stack count: {stringStack.Count}");

            // POP 2 DATA. INSPECT STACK TOP AND COUNT.
            Console.WriteLine("\nPOPPING twice...");
            Console.WriteLine($"Popped '{stringStack.Pop()}'");
            Console.WriteLine($"Popped '{stringStack.Pop()}'");
            Console.WriteLine($"Top of the stack after popping twice: '{stringStack.Peek()}'");
            Console.WriteLine($"Stack count after popping twice: {stringStack.Count}");

            // CLEAR STACK. INSPECT STACK COUNT.
            Console.WriteLine("\nCLEARING the stack...");
            stringStack.Clear();
            Console.WriteLine($"Stack count after clearing: {stringStack.Count}");

            // PEEK AT EMPTY STACK TO TEST EXCEPTION.
            Console.WriteLine("\nPEEKING at the empty stack...");

            if (stringStack.Count > 0)
            {
                stringStack.Peek();
            }
            else
            {
                Console.WriteLine("ERROR OCCURRED: Stack empty.");
            }


            // POP FROM EMPTY STACK TO TEST EXCEPTION.
            Console.WriteLine("\nPOPPING from the empty string stack...");

            if (stringStack.Count > 0)
            {
                stringStack.Pop();
            }
            else
            {
                Console.WriteLine("ERROR OCCURRED: Stack empty.");
            }


            // ADD MORE DATA. INSPECT STACK TOP AND COUNT.
            Console.WriteLine("\nPUSHING the strings 'Echo', 'Falcon', and 'Gamora'...");
            stringStack.Push("Echo");
            stringStack.Push("Falcon");
            stringStack.Push("Gamora");
            Console.WriteLine($"Top of the stack: '{stringStack.Peek()}'");
            Console.WriteLine($"Stack count: {stringStack.Count}");

            // PRINT ALL STACK DATA.
            Console.WriteLine("\nPRINTING stack data iteratively...");
            //stringStack.PrintDataIterative();

            Console.WriteLine("\nPRINTING stack data recursively...");
            //stringStack.PrintDataRecursive();

            // POP ALL STACK DATA IN A LOOP. 
            Console.WriteLine("\nPOPPING all stack data...");

            for (int i = 0; i < stringStack.Count - 1; i++)
            {
                Console.WriteLine($"Popped {stringStack.Pop()}");
            }


            // INSPECT COUNT.
            Console.WriteLine($"Final stack count: {stringStack.Count}");

            // PRINT ALL STACK DATA AGAIN.
            Console.WriteLine("\nPRINTING stack data iteratively...");
            stringStack.PrintDataIterative();

            Console.WriteLine("\nPRINTING stack data recursively...");
            stringStack.PrintDataRecursive();

            #endregion
        }
    }
}

// ----------------------------------------------------------------
// ---                     Code Analysis                        ---
// ----------------------------------------------------------------

#region Code analysis

// 1. Using a Stack built on an array or List, where Push() is a call to Add()...
//    a) What is the Big O of the Push method?
//          O(1)
//    b) Explain why.
//          It is only doing one operation to one item.
//
//    c) What is the Big O of the Pop method?
//          O(1)
//    d) Explain why.
//          It is only doing one operation to one item.
//
// 2. Using this LinkedStack class..
//    a) What is the Big O of the Push method?
//          O(1)
//    b) Why?
//          It is only doing one operation to one node.
//
//    c) What is the Big O of the Pop method?
//          O(1)
//    d) Explain why.
//          It is only doing one operation to one node.
//

#endregion

