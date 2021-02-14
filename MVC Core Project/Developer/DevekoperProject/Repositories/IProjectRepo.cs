using System;
using DevekoperProject.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevekoperProject.Repositories
{
    public interface IProjectRepo
    {
        List<Project> Get();
        List<Project> GetWithChildred();

        Project GetWithChildren(int id);
        List<DeveloperCompany> GetDeveloperCompaniesList(); //for dopdown
        List<Project> GetProjectOptions(int developerCompanyId);//for dopdown
        bool Insert(Project Project);
        bool Update(Project Project, bool childIncluded = false);
        bool Delete(int id);
        bool InsertBuilding(List<Building> buildings);
    }  }
