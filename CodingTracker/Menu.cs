using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingTracker
{
    internal class Menu
    {
        internal static void ShowMenu()
        {
            Console.Clear();
            bool closeApp = false;
            while (closeApp == false)
            {
                Console.WriteLine("Coding time tracker");
                Console.WriteLine("===================");
                Console.WriteLine(@"Select option:
a - Add new session
u - Update a session
d - Delete a session
v - View all sessions
q - Close application");

                string menuInput = Console.ReadLine();
                switch (menuInput.Trim().ToLower())
                {
                    case "a":

                        break;

                    case "u":

                        break;

                    case "d":

                        break;

                    case "v":

                        break;

                    case "q":
                        closeApp = true;
                        Environment.Exit(0);
                        break;

                    default:
                        Console.WriteLine("\nInvalid command - Press ENTER");
                        HelperFunctions.PressEnter();
                        Console.Clear();
                        break;
                }
            }
        }
    }
}
