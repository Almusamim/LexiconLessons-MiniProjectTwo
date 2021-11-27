using Spectre.Console;
using System;

namespace MiniProjectTwo.Utilities
{
    class Menu
    {
        public static void Run()
        {
            Console.WriteLine();
            AnsiConsole.Markup(" [bold steelblue1_1]Press Key For:[/]  ");

            List("C", "Create", "green1");
            List("R", "Create", "cyan1");
            List("U", "Update", "greenyellow");
            List("D", "Delete", "red");

            Console.WriteLine("\n");

            List("S", "Search Product", "orchid1", "orchid1");
            bool sampleDataExist = Util.DemoDataExist();
            if (sampleDataExist)
            {
                List("N", "Add Sample Data", "hotpink3", "hotpink3");
            }

            AnsiConsole.Markup("[grey]|| [/]");
            List("Esc", "Delete", "red", "red");
            Console.WriteLine("\n");
        }

        private static void List(string key, string name, string keyColor = "white", string fontColor = "white")
        {
            AnsiConsole.Markup
            (
                $"[{fontColor}]" +
                $"'[slowblink bold {keyColor}]{key}[/]' " +
                $"[italic]{name}[/]" +
                $"[/]  "
            );
        }
    }
}
