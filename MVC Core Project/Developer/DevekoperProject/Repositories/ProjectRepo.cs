using DevekoperProject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevekoperProject.Repositories
{
    public class ProjectRepo:IProjectRepo
    {
        ProjectDbContext db;
        public ProjectRepo(ProjectDbContext db) { this.db = db; }

        public bool Delete(int id)
        {
            db.Entry(new Project { ProjectId = id }).State = EntityState.Deleted;
            return db.SaveChanges() > 0;
        }

        public List<Project> Get()
        {
            return db.Projects.ToList();
        }

        public List<DeveloperCompany> GetDeveloperCompaniesList()
        {
            return db.DeveloperCompanies.ToList();
        }

        public List<Project> GetProjectOptions(int developerCompanyId)
        {
            return db.Projects.Where(c => c.DeveloperCompanyId == developerCompanyId).ToList();
        }

        public List<Project> GetWithChildred()
        {
            return db.Projects
                .Include(x => x.DeveloperCompany)
                .Include(y => y.Buildings)
                .ToList();
        }

        public Project GetWithChildren(int id)
        {
            return db.Projects
                .Include(x => x.DeveloperCompany)
                .Include(x => x.Buildings)
                .FirstOrDefault(x => x.ProjectId == id);
        }

        public bool Insert(Project Project)
        {
            db.Projects.Add(Project);
            return db.SaveChanges() > 0;
        }

        public bool InsertBuilding(List<Building> buildings)
        {
            db.Buildings.AddRange(buildings);
            return db.SaveChanges() > 0;
        }

        public bool Update(Project Project, bool childIncluded = false)
        {
            db.Entry(Project).State = EntityState.Modified;

            var orignal = db.Projects.Include(x => x.Buildings).First(x => x.ProjectId == Project.ProjectId);
            orignal.DeveloperCompanyId = Project.DeveloperCompanyId;
            orignal.PlaceName = Project.PlaceName;


            if (Project.Buildings != null && Project.Buildings.Count > 0)
            {
                var buildings = Project.Buildings.ToArray();
                for (var i = 0; i < buildings.Length; i++)
                {
                    var temp = orignal.Buildings.FirstOrDefault(x => x.BuildingId == buildings[i].BuildingId);
                    if (temp != null)
                    {
                        temp.BuildingName = buildings[i].BuildingName;
                        temp.Address = buildings[i].Address;
                        temp.TotalFloor = buildings[i].TotalFloor;
                        temp.FlatSize = buildings[i].FlatSize;
                        temp.FlatPrice = buildings[i].FlatPrice;
                    }
                    else
                    {
                        orignal.Buildings.Add(buildings[i]);
                    }
                }
                foreach (var s in orignal.Buildings)
                {
                    var temp = Project.Buildings.FirstOrDefault(t => t.BuildingId == s.BuildingId);
                    if (temp == null)
                        db.Entry(s).State = EntityState.Deleted;
                }
            }
            return db.SaveChanges() > 0;
        }

       
    }
    
}
