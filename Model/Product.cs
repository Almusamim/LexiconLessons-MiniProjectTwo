using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MiniProjectTwo.Model
{
    class Product
    {
        public Product(Category category, Office office, string brand, string model, decimal price, DateTime purchaseDate)
        {
            Category = category;
            Office = office;
            Brand = brand;
            Model = model;
            Price = price;
            PurchaseDate = purchaseDate;
        }

        public Product()
        {
        }

        public int Id { get; set; }

        public Category Category { get; set; }
        public Office Office { get; set; }

        [Required]
        public string Brand { get; set; }

        [Required]
        public string Model { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public DateTime PurchaseDate { get; set; }

        // Product CRUD Methods
        public static Product GetSingle(int id)
        {
            using var context = new AppDbContext();
            return context.Products.SingleOrDefault(p => p.Id == id);
        }

        public static List<Product> GetAll()
        {
            using var context = new AppDbContext();
            return context.Products.Include(p => p.Category).Include(p => p.Office).ToList<Product>();
        }

        public static async void Create(Product product)
        {
            string categoryName = product.Category.Name;
            string OfficeName = product.Office.Name;

            using var context = new AppDbContext();
            product.Category = await NewOrFirstCategory(context, categoryName);
            product.Office = await NewOrFirstOffice(context, OfficeName);

            context.Products.Add(product);
            context.SaveChanges();
        }

        public static async void Update(Product product)
        {
            using var context = new AppDbContext();
            var result = context.Products.SingleOrDefault(p => p.Id == product.Id);
            if (result != null)
            {
                result.Category = await NewOrFirstCategory(context, product.Category.Name);
                result.Office = await NewOrFirstOffice(context, product.Office.Name);
                //result.Category = product.Category;
                //result.Office = product.Office;
                result.Brand = product.Brand;
                result.Model = product.Model;
                result.Price = product.Price;
                result.PurchaseDate = product.PurchaseDate;

                //context.
                //context.Entry(product).State = EntityState.Modified;
                context.Update(result);
                context.SaveChanges();
            }
        }

        public static void Delete(int id)
        {
            using var context = new AppDbContext();
            var Test = context.Products.Single(p => p.Id == id);
            context.Remove(context.Products.Single(p => p.Id == id));
            context.SaveChanges();
        }

        // To prevent category duplications in the database
        static async Task<Category> NewOrFirstCategory(AppDbContext context, string catName)
        {
            return (await context.Categories.FirstOrDefaultAsync(x => x.Name == catName)) ?? new Category(catName);
        }
        static async Task<Office> NewOrFirstOffice(AppDbContext context, string office)
        {
            return (await context.Offices.FirstOrDefaultAsync(x => x.Name == office)) ?? new Office(office);
        }

        public (string price, DateTime latestUpdate) CurrencyConvert()
        {
            var (MYR, SEK, latestUpdate) = Client.Rates();

            string price;
            switch (Office.Name)
            {
                case "Malmö":
                    price = (Price * SEK).ToString("0.## Kr");
                    break;
                case "Kuala Lumpur":
                    price = (Price * MYR).ToString("RM0.##");
                    break;
                default:
                    price = Price.ToString("$0.##");
                    break;
            }

            return (price, latestUpdate);
        }

        public string LifeSpanTxtColor()
        {
            DateTime now = DateTime.Now;
            DateTime alarmDate = now.AddMonths(-33);
            DateTime warningDate = now.AddMonths(-30);

            if (alarmDate > PurchaseDate)
            {
                return "Red";
            }
            if (warningDate > PurchaseDate)
            {
                return "Yellow";
            }

            return "White";
        }
    }
}
