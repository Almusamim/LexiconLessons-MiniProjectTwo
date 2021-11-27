using MiniProjectTwo.Model;
using Spectre.Console;
using System.Collections.Generic;
using System.Linq;

namespace MiniProjectTwo.Utilities
{
    class ProductDataTable
    {
        public static void Run(string searchWord = "")
        {
            List<Product> productList = Product.GetAll().OrderBy(p => p.Office.Name).ThenBy(p => p.PurchaseDate).ToList();

            IsSearch(searchWord, ref productList);

            if (productList.Count > 0)
            {
                // Create a table and set the header
                Table table = TableHeader();

                // Add table rows
                TableRows(productList, table);

                // Render the table to the console
                AnsiConsole.Write(table);

                Util.PrintTxt($"Exchange Rates Updated: {productList[0].CurrencyConvert().latestUpdate} | Total Products: {productList.Count} \n\n", "DarkGray");
            }
            else
            {
                Util.PrintTxt("\n\n\t\tNO PRODUCT FOUND!\n\n", "Red");
                Util.PrintTxt("\t\tPress 'N' to add sample data.\n\n", "Yellow");
            }
        }

        private static void IsSearch(string searchWord, ref List<Product> productList)
        {
            bool isSearch = !string.IsNullOrWhiteSpace(searchWord);
            if (isSearch)
            {
                searchWord = searchWord.ToLower().Trim();
                productList = productList.Where(p => p.Brand.ToLower().Contains(searchWord)
                    || p.Model.ToLower().Contains(searchWord)
                    || p.Office.Name.ToLower().Contains(searchWord)).ToList();
            }
        }

        private static Table TableHeader()
        {
            return new Table()
                .BorderColor(Color.Grey)
                .Border(TableBorder.Rounded)
                .AddColumns("ID", "Category", "Brand", "Model", "Purchase Date", "Office", "Price");
        }

        private static void TableRows(List<Product> productList, Table table)
        {
            foreach (Product p in productList)
            {
                string txtColor = p.LifeSpanTxtColor();
                string price = p.CurrencyConvert().price;
                table.AddRow(
                    $"[{txtColor}]{p.Id}[/]",
                    $"[{txtColor}]{p.Category.Name}[/]",
                    $"[{txtColor}]{p.Brand}[/]",
                    $"[{txtColor}]{p.Model}[/]",
                    $"[{txtColor}]{p.PurchaseDate:yyyy-MM-dd}[/]",
                    $"[{txtColor}]{p.Office.Name}[/]",
                    $"[{txtColor}]{price}[/]"
                );
            }
        }
    }
}
