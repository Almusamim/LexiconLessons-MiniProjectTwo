using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace MiniProjectTwo.Model
{
    class Category
    {
        public Category(string name = "Products")
        {
            Name = name;
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        public static List<Category> GetAll()
        {
            using var context = new AppDbContext();
            return context.Categories.ToList<Category>();
        }

        public static void Create(Category category)
        {
            using var context = new AppDbContext();
            context.Categories.Add(category);
            context.SaveChanges();
        }
    }
}
