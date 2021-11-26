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
            AnsiConsole.Markup("'[slowblink bold green1]C[/]' [italic]Create[/]  ");
            AnsiConsole.Markup("'[slowblink bold cyan1]R[/]' [italic]Read[/]  ");
            AnsiConsole.Markup("'[slowblink bold greenyellow]U[/]' [italic]Update[/]  ");
            AnsiConsole.Markup("'[slowblink bold red]D[/]' [italic]Delete[/]");
            Console.WriteLine("\n");


            AnsiConsole.Markup(" '[slowblink bold orchid1]S[/]' [italic orchid1]Search Product[/]  ");
            bool sampleDataExist = ProductService.DemoDataExist();
            if (sampleDataExist)
            {
                AnsiConsole.Markup("'[slowblink bold hotpink3]N[/]' [italic hotpink3]Add Sample Data[/]");
            }

            AnsiConsole.Markup("[grey] || [/]");
            AnsiConsole.Markup("'[slowblink bold red]Esc[/]' [italic red]Delete[/]");
            Console.WriteLine("\n");
        }
    }
}
