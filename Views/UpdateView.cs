using MiniProjectTwo.Utilities;
using Spectre.Console;
using System;

namespace MiniProjectTwo.Views
{
    class UpdateView
    {
        public static void Run()
        {
            ProductDataTable.Run();
            int UpdateProductId = AnsiConsole.Ask<int>("\n[green]Choose an product ID to update:[/]");

            Console.Clear();
            CreateOrUpdateProductForm.Run(true, UpdateProductId);

            Util.ClearConsole();

            ProductDataTable.Run();
        }
    }
}
