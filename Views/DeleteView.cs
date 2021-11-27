using MiniProjectTwo.Model;
using Spectre.Console;
using System;

namespace MiniProjectTwo.Views
{
    class DeleteView
    {
        public static void Run()
        {
            Utilities.ProductDataTable.Run();

            int DeleteProductId = SelectProductId();

            ConfirmAndDelete(DeleteProductId);

            Util.ClearConsole();
            Utilities.ProductDataTable.Run();
        }

        private static void ConfirmAndDelete(int DeleteProductId)
        {
            if (AnsiConsole.Confirm($" Are you sure you want to delete product with ID # {DeleteProductId} ?"))
            {
                Console.Clear();
                Product.Delete(DeleteProductId);
            }
        }

        private static int SelectProductId()
        {
            return AnsiConsole.Prompt(
                new TextPrompt<int>("\n[green]Choose an product ID to delete:[/]")
                    .Validate(DeleteProductId =>
                    {
                        var product = Product.GetSingle(DeleteProductId);
                        if (product is null)
                        {
                            return ValidationResult.Error("[red]ERROR: ID not found.[/]\n[yellow]Make sure to choose an existing ID[/]");
                        }

                        return ValidationResult.Success();
                    }));
        }
    }
}
