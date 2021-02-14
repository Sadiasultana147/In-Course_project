using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevekoperProject.Models;

using Microsoft.EntityFrameworkCore;

namespace DevekoperProject.Repositories
{
    public class DeveloperCompanyRepo:IDeveloperCompanyRepo    {
        ProjectDbContext db;
        public DeveloperCompanyRepo(ProjectDbContext db)
        {
            this.db = db;
        }
       

      
       

        public bool Insert(DeveloperCompany DeveloperCompany)
        {
            db.DeveloperCompanies.Add(DeveloperCompany);
            return db.SaveChanges() > 0;
        }

        public bool Update(DeveloperCompany DeveloperCompany, bool childIncluded = false)
        {
            db.Entry(DeveloperCompany).State = EntityState.Modified;
            var orignal = db.DeveloperCompanies.Include(x => x.Projects).First(x => x.DeveloperCompanyId == DeveloperCompany.DeveloperCompanyId);
            orignal.DeveloperCompanyName = DeveloperCompany.DeveloperCompanyName;
            orignal.EstablishedDate = DeveloperCompany.EstablishedDate;

            if (DeveloperCompany.Projects != null && DeveloperCompany.Projects.Count > 0)
            {
                var projects = DeveloperCompany.Projects.ToArray();
                for (var i = 0; i < projects.Length; i++)
                {
                    var temp = orignal.Projects.FirstOrDefault(p => p.ProjectId == projects[i].ProjectId);
                    if (temp != null)
                    {
                        temp.PlaceName = projects[i].PlaceName;


                    }
                    else
                    {
                        orignal.Projects.Add(projects[i]);
                    }
                }
                foreach (var p in orignal.Projects)
                {
                    var temp = DeveloperCompany.Projects.FirstOrDefault(t => t.ProjectId == p.ProjectId);
                    if (temp == null)
                        db.Entry(p).State = EntityState.Deleted;
                }
            }
            return db.SaveChanges() > 0;

        }    
                
        public bool Delete(int id)
        {
            db.Entry(new DeveloperCompany { DeveloperCompanyId = id }).State = EntityState.Deleted;
            return db.SaveChanges() > 0;
        }

        public List<DeveloperCompany> Get()
        {
            return db.DeveloperCompanies.ToList();
        }

        public List<DeveloperCompany> GetWithChildred()
        {
            return db.DeveloperCompanies
                 .Include(x => x.Projects)
                 .ThenInclude(y => y.Buildings)
                 .ToList();
        }

        

        public DeveloperCompany GetWithChildren(int id)
        {
            return db.DeveloperCompanies
                .Include(x => x.Projects)
                .FirstOrDefault(x => x.DeveloperCompanyId == id);
        }
    }
}
