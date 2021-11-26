using MiniProjectTwo.Model;
using MiniProjectTwo.Utilities;
using Spectre.Console;
using System;

namespace MiniProjectTwo
{
    class Program
    {
        static void Main(string[] args)
        {
            RunApp();
        }

        private static void RunApp()
        {
            ProductService.CreateCategoriesOffices();

            Misc.LexiconHeader();

            ConsoleKeyInfo cki;
            Menu.Run();

            while (true)
            {
                cki = Console.ReadKey(false);
                Util.ClearConsole();

                // add product form
                if (cki.Key == ConsoleKey.C || cki.Key == ConsoleKey.Add)
                {
                    AddProductPage();
                }

                // Update Product
                if (cki.Key == ConsoleKey.U || cki.Key == ConsoleKey.Insert)
                {
                    UpdatePage();
                }

                // List all products in data table
                if (cki.Key == ConsoleKey.R)
                {
                    ProductService.Table();
                }

                // Delete Product
                if (cki.Key == ConsoleKey.Delete || cki.Key == ConsoleKey.D)
                {
                    DeletePage();
                }

                // Search
                if (cki.Key == ConsoleKey.S)
                {
                    SearchPage();
                }

                //Add sample data to the products
                if (cki.Key == ConsoleKey.Print || cki.Key == ConsoleKey.N)
                {
                    ProductService.DemoData();
                }

                if (cki.Key == ConsoleKey.Escape)
                {
                    Util.ExitApp();
                    break;
                }
            }
        }

        private static void AddProductPage()
        {
            ProductService.AddProudct();

            // Show data table after adding new product
            Util.ClearConsole();
            ProductService.Table();
        }

        private static void UpdatePage()
        {
            ProductService.Table();
            int UpdateProductId = AnsiConsole.Ask<int>("\n[green]Choose an product ID to update:[/]");

            Console.Clear();
            ProductService.AddProudct(UpdateProductId);

            Util.ClearConsole();
            ProductService.Table();
        }

        private static void SearchPage()
        {
            Util.PrintTxt("Search:", "Blue");
            string searchValue = Console.ReadLine();
            ProductService.Table(searchValue);
        }

        private static void DeletePage()
        {
            ProductService.Table();
            var DeleteProductId = AnsiConsole.Prompt(
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

            if (AnsiConsole.Confirm($" Are you sure you want to delete product with ID # {DeleteProductId} ?"))
            {
                Console.Clear();
                Product.Delete(DeleteProductId);
                Console.WriteLine("Test delete");
            }


            Util.ClearConsole();
            ProductService.Table();
        }
    }
}
