using MiniProjectTwo.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MiniProjectTwo
{
    class Util
    {   
        public static void PrintTxt(string txt, string color = "White")
        {
            Console.ForegroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), color, true);
            Console.Write($" {txt} ");
            Console.ResetColor();
        }

        public static void ClearConsole()
        {
            Console.Clear();
            Menu.Run();
        }

        public static void ExitApp()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n\n Press any key to exit...");
            Console.ReadKey(true);
            Environment.Exit(0);
        }

    }
}