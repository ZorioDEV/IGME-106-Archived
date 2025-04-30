// Tyler Bye
// 4.4.2025
// Practical 2 - Browser History

using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Diagnostics.Metrics;
using System.Xml.Linq;

namespace Practical_2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Local variables needed for successful program run
            Browser myBrowser = new Browser();
            string userOption = "";

            // User input loop
            while (userOption != "7")
            {
                // Print the user's menu
                Console.WriteLine("1 to visit a webpage.              2 to move forward to the next page.");
                Console.WriteLine("3 to go back to a previous page.   4 to print your history.");
                Console.WriteLine("5 to print current page.           6 to close the browser.");
                Console.WriteLine("7 to exit the program.");

                // Get user's choice
                Console.Write("Choose one of the options: ");
                userOption = Console.ReadLine()!;

                switch (userOption)
                {
                    // Get user's webpage
                    case "1":
                        Console.Write("Enter webpage to visit: ");
                        string page = Console.ReadLine()!;
                        myBrowser.VisitPage(page);
                        break;

                    // Move forward a page
                    case "2":
                        myBrowser.MoveForward();
                        break;

                    // Move back a page
                    case "3":
                        myBrowser.MoveBackward();
                        break;

                    // Print all history
                    case "4":
                        myBrowser.PrintHistory();
                        break;

                    // Print current browsing page
                    case "5":
                        myBrowser.PrintCurrentPage();
                        break;

                    // "Close" the browser
                    case "6":
                        myBrowser.CloseBrowser();
                        break;

                    // "Close" the browser
                    case "7":
                        Console.WriteLine("Goodbye!");
                        break;

                    // Invalid input
                    default:
                        Console.WriteLine("Unrecognized input. Try again.");
                        break;
                }

                // Line break for next iteration
                Console.WriteLine();
            }
        }
    }
}
