using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookProject.Models
{
    public class Category
    {
        public Category()
        {
            this.Books = new List<Book>();
        }
        [Display(Name = "Category ID")]
        public int CategoryId { get; set; }
        [Required, StringLength(30), Display(Name = "Category Name")]
        public string CategoryName { get; set; }
        

        //
        public virtual ICollection<Book> Books { get; set; }
    }
    public class Book
    {

        [Display(Name = "Book ID")]
        public int BookId { get; set; }
        [Required, StringLength(40), Display(Name = "Book Name")]
        public string BookName { get; set; }
        [Required, Display(Name = "Author ")]
        public string Author { get; set; }
        [Required, Column(TypeName = "money")]
        public decimal Price { get; set; }

        [Required, ForeignKey("Category"), Display(Name = "Category")]
        public int CategoryId { get; set; }
        //
        public virtual Category Category { get; set; }

    }

    public class BookDbContext : DbContext
    {
        public BookDbContext(DbContextOptions<BookDbContext> options) : base(options) { }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Book> Books { get; set; }

    }
}
