using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace MiniProjectTwo.Model
{
    class Office
    {
        public Office(string name)
        {
            Name = name;
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        public static List<Office> GetAll()
        {
            using var context = new AppDbContext();
            return context.Offices.ToList<Office>();
        }
        public static void Create(Office office)
        {
            using var context = new AppDbContext();
            context.Offices.Add(office);
            context.SaveChanges();
        }
    }
}
