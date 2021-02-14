using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookProject.Controllers
{
    [Produces("application/json")]
    public class CategoryDataController : Controller
    {
        BookDbContext db;
        public CategoryDataController(BookDbContext db) { this.db = db; }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult CategoriesWithBook()
        {
            var data = db.Categories.Include(x => x.Books).ToList();

            return Json(data);

        }
        [HttpGet]
        public IActionResult CategoriesWithBookById(int id)
        {
            var data = db.Categories.Include(x => x.Books).First(x => x.CategoryId == id);

            return Json(data);

        }
        [HttpPost]
        public IActionResult InsertCategoriesWithBook([FromBody]Category t)
        {
            db.Categories.Add(t);
            db.SaveChanges();

            return Json(t);

        }
        [HttpPut]
        public IActionResult UpdateCategoriesWithBook(int id, [FromBody]Category t)
        {
            var original = db.Categories.Include(x => x.Books).First(x => x.CategoryId == id);
            original.CategoryName = t.CategoryName;
            
            if (t.Books != null && t.Books.Count > 0)
            {
                var crs = t.Books.ToArray();
                for (var i = 0; i < crs.Length; i++)
                {
                    var temp = original.Books.FirstOrDefault(c => c.BookId == crs[i].BookId);
                    if (temp != null)
                    {
                        temp.BookName = crs[i].BookName;
                        temp.Author = crs[i].Author;
                        temp.Price = crs[i].Price;
                    }
                    else
                    {
                        original.Books.Add(crs[i]);
                    }
                }
                crs = original.Books.ToArray();
                for (var i = 0; i < crs.Length; i++)
                {
                    var temp = t.Books.FirstOrDefault(x => x.BookId == crs[i].BookId);
                    if (temp == null)
                        db.Entry(crs[i]).State = EntityState.Deleted;
                }
            }

            db.SaveChanges();

            return Json(t);

        }
        [HttpDelete]
        public IActionResult DeleteCategory(int id)
        {
            var original = db.Categories.First(x => x.CategoryId == id);
            db.Categories.Remove(original);
            db.SaveChanges();
            return Json(original);
        }
    }
}