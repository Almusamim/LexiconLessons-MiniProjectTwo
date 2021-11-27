using MiniProjectTwo.Model;
using MiniProjectTwo.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public static void Search()
        {
            Util.PrintTxt("Search:", "Blue");
            string searchValue = Console.ReadLine();
            ProductDataTable.Run(searchValue);
        }
        public static void ExitApp()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n\n Press any key to exit...");
            Console.ReadKey(true);
            Environment.Exit(0);
        }

        public static bool DemoDataExist()
        {
            List<Product> list = Product.GetAll();
            return !list.Any(p => p.Model == "Thinkpad P1" && p.Brand == "Lenovo" && p.Price == 3750.90m && p.Category.Name == "Laptop" && p.Office.Name == "Kuala Lumpur");
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

                Util.PrintTxt("\tSample data added.\n", "Yellow");
            }
            else
            {
                Util.PrintTxt("\n\tSample Data Alreay Added.\n", "Red");
            }
        }

        public static void CreateCategoriesOffices()
        {
            if (Category.GetAll().Count == 0)
            {
                Category.Create(new Category("Laptop"));
                Category.Create(new Category("Mobile Phone"));
                Category.Create(new Category("IoT"));
                Category.Create(new Category("Camera"));
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