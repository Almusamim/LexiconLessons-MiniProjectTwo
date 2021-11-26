using MiniProjectTwo.Model;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace MiniProjectTwo
{
    class ProductService
    {
        public static void Table(string searchWord = "")
        {

            List<Product> productList = Product.GetAll().OrderBy(p => p.Office.Name).ThenBy(p => p.PurchaseDate).ToList();

            bool isSearch = !string.IsNullOrWhiteSpace(searchWord);
            if (isSearch)
            {
                searchWord = searchWord.ToLower().Trim();
                productList = productList.Where(p => p.Brand.ToLower().Contains(searchWord)
                    || p.Model.ToLower().Contains(searchWord)
                    || p.Office.Name.ToLower().Contains(searchWord)).ToList()
                ;
            }

            if (productList.Count > 0)
            {
                // Create a table
                var table = new Table()
                    .BorderColor(Color.Grey)
                    .Border(TableBorder.Rounded)
                    .AddColumns("ID", "Category", "Brand", "Model", "Purchase Date", "Office", "Price");

                // Add some rows
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

        public static void AddProudct(int id = -1)
        {
            string[] categories = Category.GetAll().Select(x => x.Name.ToString()).ToArray();
            var category = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[green]Choose a Category[/]\n[grey]Use up and down arrow to choose an option.[/]")
                    .PageSize(10)
                    .MoreChoicesText("[grey](Move up and down to reveal more categories)[/]")
                    .AddChoices(categories)
            );

            string[] offices = Office.GetAll().Select(x => x.Name.ToString()).ToArray();
            var office = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[green]Choose office Location[/]\n[grey]Use up and down arrow to choose an option.[/]")
                    .PageSize(10)
                    .MoreChoicesText("[grey](Move up and down to reveal more office locations)[/]")
                    .AddChoices(offices)
            );

            var brand = AnsiConsole.Prompt(
                new TextPrompt<string>("Brand name:")
            );

            var model = AnsiConsole.Prompt(
                new TextPrompt<string>("Model name:")
            );

            var price = AnsiConsole.Prompt(
                new TextPrompt<decimal>("Product price:")
            );

            var date = AnsiConsole.Prompt(
                new TextPrompt<DateTime>("Purschase Date:")
            );

            //Product product = new();
            Product product = new Product(
                new Category(category),
                new Office(office),
                brand,
                model,
                price,
                date
            );

            if (id > 0)
            {
                product.Id = id;
                Product.Update(product);
            }
            else
            {
                Product.Create(product);
            }

            AnsiConsole.Status()
                .Spinner(Spinner.Known.Aesthetic)
                .SpinnerStyle(Style.Parse("green bold"))
                .Start("Initiating database...", ctx =>
                {
                    // Simulate some work
                    AnsiConsole.MarkupLine("\nAdding product into database...");
                    Thread.Sleep(900);

                    AnsiConsole.MarkupLine("\nLoading the product table list...");
                    Thread.Sleep(600);
                }
            );
        }

        public static void DemoData()
        {
            bool productExist = DemoDataExist();
            if (productExist)
            {
                DateTime now = DateTime.Now;

                Product.Create(new Product(new Category("Laptop"), new Office("Malmö"), "Lenovo", "ThinkPad X1 Nano", 2400.00m, now.AddMonths(-30)));
                Product.Create(new Product(new Category("Mobile Phone"), new Office("Malmö"), "Ericsson", "T28", 100.00m, now.AddMonths(-66).AddDays(5)));
                Product.Create(new Product(new Category("Mobile Phone"), new Office("Malmö"), "Nokia", "3200", 90.00m, now.AddMonths(-66).AddDays(19)));
                Product.Create(new Product(new Category("Mobile Phone"), new Office("Dallas"), "Nokia ", "3210", 30.00m, now.AddMonths(-34)));
                Product.Create(new Product(new Category("Laptop"), new Office("Dallas"), "Framework", "Gen 1", 2900.00m, new DateTime(2020, 01, 03)));
                Product.Create(new Product(new Category("Laptop"), new Office("Dallas"), "IBM", "Thinkpad A20p", 1900.00m, now.AddMonths(-34).AddDays(4)));
                Product.Create(new Product(new Category("Laptop"), new Office("Kuala Lumpur"), "Dell", "XPS 13", 1699.00m, now.AddMonths(-31).AddDays(7)));
                Product.Create(new Product(new Category("Laptop"), new Office("Kuala Lumpur"), "Lenovo", "Thinkpad P1", 3750.90m, now.AddDays(-4)));
                Product.Create(new Product(new Category("IoT"), new Office("Kuala Lumpur"), "Raspberry", "Pi 4", 65.00m, now.AddMonths(-12).AddDays(4)));

                Util.PrintTxt("\tDemo data added.\n", "Yellow");
            }
            else
            {
                Util.PrintTxt("\n\tDemo Data Alreay Added.\n", "Red");
            }
        }

        public static bool DemoDataExist()
        {
            List<Product> list = Product.GetAll();
            return !list.Any(p => p.Model == "Thinkpad P1" && p.Brand == "Lenovo" && p.Price == 3750.90m && p.Category.Name == "Laptop" && p.Office.Name == "Kuala Lumpur");
        }

        public static void CreateCategoriesOffices()
        {
            if (Category.GetAll().Count == 0)
            {
                Category.Create(new Category("Laptop"));
                Category.Create(new Category("Mobile Phone"));
                Category.Create(new Category("IoT"));
            }
            if (Office.GetAll().Count == 0)
            {
                Office.Create(new Office("Malmö"));
                Office.Create(new Office("Dallas"));
                Office.Create(new Office("Kuala Lumpur"));
            }
        }

        public static void DeleteAll()
        {
            using var context = new AppDbContext();
            context.Categories.RemoveRange(context.Categories);
            context.Products.RemoveRange(context.Products);
            context.Offices.RemoveRange(context.Offices);
            context.SaveChanges();
        }
    }
}
