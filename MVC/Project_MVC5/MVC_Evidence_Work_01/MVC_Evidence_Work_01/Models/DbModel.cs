using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MVC_Evidence_Work_01.Models
{
    public enum Category { Novel, Drama, Poem, ActionAndAdventure, Classic, DetectiveAndMystery, HistoricalFiction, LiteraryFiction }
    public class Book
    {
        [Display(Name = "Book Id")]
        public int BookId { get; set; }
        [Required, StringLength(40), Display(Name = "Book Name")]
        public string BookName { get; set; }
        [EnumDataType(typeof(Category))]
        public Category? Category { get; set; }
        [Required, Column(TypeName = "date")]
        [Display(Name = "Publish Date"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime PublishDate { get; set; }
        [Required, StringLength(150)]
        public string Picture { get; set; }
    }
    public class BookDbContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
    }
}