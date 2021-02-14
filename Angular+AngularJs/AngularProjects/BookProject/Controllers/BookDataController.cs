using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookProject.Controllers
{
    public class BookDataController : Controller
    {
        BookDbContext db;
        public BookDataController(BookDbContext db) { this.db = db; }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult GetCategoriesForDropDown()
        {
            return Json(db.Categories.ToList());
        }
        [HttpGet]
        public IActionResult GetBooks()
        {
            return Json(db.Books.ToList());
        }
        [HttpGet]
        public IActionResult GetBookById(int id)
        {
            return Json(db.Books.First(x => x.BookId == id));
        }
        [HttpPost]
        public IActionResult InsertBook([FromBody]Book b)
        {
            db.Books.Add(b);
            db.SaveChanges();
            return Json(b);
        }
        [HttpPut]
        public IActionResult UpdateBook(int id, [FromBody] Book b)
        {
            var original = db.Books.First(x => x.BookId == id);
            original.BookName = b.BookName;
            original.Author = b.Author;
            original.Price = b.Price;
            original.CategoryId = b.CategoryId;
            db.SaveChanges();
            return Json(b);
        }
        [HttpDelete]
        public IActionResult DeleteBook(int id)
        {
            var original = db.Books.First(x => x.BookId == id);
            db.Remove(original);
            db.SaveChanges();
            return Json(original);
        }
    }
}