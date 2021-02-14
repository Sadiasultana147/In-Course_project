using MVC_Evidence_Work_01.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList.Mvc;
using PagedList;
using MVC_Evidence_Work_01.ViewModels;
using System.IO;
using System.Web.Hosting;

namespace MVC_Evidence_Work_01.Controllers
{   
    [Authorize]
    public class BooksController : Controller
    {
        BookDbContext db = new BookDbContext();
        [AllowAnonymous]
        public ActionResult Index(int page=1, string sort="")
        {
            var data = db.Books.Select(x => new BookVm
            {
                BookId = x.BookId,
                BookName =x.BookName,
                Category =x.Category,
                PublishDate =x.PublishDate,
                Picture = x.Picture
            });
            var sortBy = sort == "" ? "name_asc" : sort;
            ViewBag.newSort = sortBy == "name_asc" ? "name_desc" : "name_asc";
            if (sortBy == "name_asc")
                data = data.OrderBy(x => x.BookName);
            else if (sortBy == "name_desc")
                data = data.OrderByDescending(x => x.BookName);
            else
            data = data.OrderBy(x => x.BookId);
            return View(data.ToPagedList(page,5));
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Book b, HttpPostedFileBase Picture)
        {
            if (Picture != null && Picture.ContentLength > 0)
            {
                string ext = Path.GetExtension(Picture.FileName);
                string f = Guid.NewGuid() + ext;
                Picture.SaveAs(HostingEnvironment.MapPath("~/Avatars/" + f));
                b.Picture = f;
            }
           
            if (ModelState.IsValid)
            {
                db.Books.Add(b);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(b);
        }
        public ActionResult Edit(int id)
        {
            return View(db.Books.First(x=>x.BookId==id));
        }
        [HttpPost]
        public ActionResult Edit(Book b, HttpPostedFileBase Picture)
        {
            string f = "";
            if (Picture != null && Picture.ContentLength > 0)
            {
                string ext = Path.GetExtension(Picture.FileName);
                f = Guid.NewGuid() + ext;
                Picture.SaveAs(HostingEnvironment.MapPath("~/Avatars/" + f));
                b.Picture = f;
            }
            if (ModelState.IsValid)
            {
                db.Entry(b).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("Index");
        }
        public ActionResult Delete(int id)
        {
            return View(db.Books.First(x => x.BookId == id));
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DoDelete(int id)
        {
            var b = new Book { BookId = id };
            db.Entry(b).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}