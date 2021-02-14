using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookProject.Models
{
    public class DbSeeder
    {
        public static void Seed(BookDbContext db)
        {
            Category c1 = new Category { CategoryName = "Novel" };
            c1.Books.Add(new Book { BookName = "Gitanjali ", Author = "Rabindranath Tagore", Price = 500.00M });
            c1.Books.Add(new Book { BookName = "Hajar Bachhor Dhore ", Author = "Zahir Raihan", Price = 250.00M });
            
            db.Categories.Add(c1);
            
            db.SaveChanges();
        }
    }
}
