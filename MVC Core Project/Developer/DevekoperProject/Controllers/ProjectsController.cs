using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevekoperProject.Models;
using DevekoperProject.Repositories;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace DevekoperProject.Controllers
{
    public class ProjectsController : Controller
    {
        IProjectRepo repo;

        public ProjectsController(IProjectRepo repo)
        {

            this.repo = repo;
        }
        public IActionResult Index(int page = 1, int developerCompanyId = 0)
        {
            var list = repo.GetDeveloperCompaniesList();
            list.Insert(0, new DeveloperCompany { DeveloperCompanyId = 0, DeveloperCompanyName = "All" });
            ViewBag.DeveloperCompany = list;
            int pageSize = 5;
            var data = repo.GetWithChildred();
            if (developerCompanyId > 0)
            {
                return View(data.Where(x => x.DeveloperCompanyId == developerCompanyId).ToPagedList(page, pageSize));
            }
            return View(data.ToPagedList(page, pageSize));


        }
        public IActionResult Create()
        {
            var list = repo.GetDeveloperCompaniesList();
            list.Insert(0, new DeveloperCompany { DeveloperCompanyId = 0, DeveloperCompanyName = "Select" });
            ViewBag.DeveloperCompany = list;
            return View();
        }
        public IActionResult GetProjectOptions(int id)
        {
            var data = repo.GetProjectOptions(id);
            data.Insert(0, new Project { ProjectId = 0, PlaceName = "Select" });
            return Json(data);
        }
        [HttpPost]
        //public IActionResult SaveBuildings([FromBody] Building[] buildings)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        repo.InsertBuilding(buildings);
        //        return Json(new { success = true });
        //    }
        //    return Json(new { success = false });
        //}
        [HttpPost]
        public IActionResult SaveBuildings1([FromBody] List<Building> buildings)
        {
            if (ModelState.IsValid)
            {
                repo.InsertBuilding(buildings);
                return Json(new { success = true });
            }
            return Json(new { success = false});
        }



        public IActionResult Edit(int id)
        {
            var list = repo.GetDeveloperCompaniesList();
            
            ViewBag.DeveloperCompany = list;
            var data = repo.GetWithChildren(id);
            return View(data);
        }
        [HttpPost]
        public IActionResult SaveProjects([FromBody]Project p)
        {
            if (ModelState.IsValid)
            {
                repo.Update(p);
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
        public IActionResult Delete(int id)
        {
            return View(repo.GetWithChildren(id));
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DoDelete(int id)
        {
            repo.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
