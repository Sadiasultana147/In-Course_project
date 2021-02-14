using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevekoperProject.Models;
using DevekoperProject.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace DevekoperProject.Controllers
{
    [Authorize]
    public class DeveloperCompanyController : Controller
    {
        IDeveloperCompanyRepo repo;
        public DeveloperCompanyController(IDeveloperCompanyRepo repo) { this.repo = repo; }
        public IActionResult Index(int page = 1)
        {
            int size = 5;
            var data = repo.GetWithChildred()
                .ToPagedList(page, size);
            return View(data);
        }
        public IActionResult CreateDeveloperCompanyWithProject()
        {
            return View();
        }

        [HttpPost]
        public JsonResult CreateDeveloperCompanyWithProject([FromBody]DeveloperCompany DC)
        {
            if (ModelState.IsValid)
            {
                if (repo.Insert(DC))
                    return Json(new { success = true });
                else
                    return Json(new { success = false });
            }
            else
            {
                return Json(new { success = false });
            }
            //return View();
        }
        public IActionResult GetProjectAddForm()
        {
            return PartialView("_ProjectAddRowPartial");
        }
        public IActionResult EditWithProject(int id)
        {
            
            var data = repo.GetWithChildren(id);
            if (data == null)
                return NotFound();
            return View(data);
        }
        [HttpPost]
        public IActionResult EditWithProject([FromBody]DeveloperCompany DC)
        {
            if (ModelState.IsValid)
            {
                if (repo.Update(DC))
                    return Json(new { success = true });

            }
            return Json(new { success = false });

        }
        
    }
}