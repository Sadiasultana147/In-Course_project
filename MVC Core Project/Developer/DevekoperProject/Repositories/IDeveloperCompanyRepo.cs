using DevekoperProject.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevekoperProject.Repositories
{
    public interface IDeveloperCompanyRepo
    {
        List<DeveloperCompany> Get();
        List<DeveloperCompany> GetWithChildred();
        
        DeveloperCompany GetWithChildren(int id);
        bool Insert(DeveloperCompany DeveloperCompany);
        bool Update(DeveloperCompany DeveloperCompany, bool childIncluded = false);
        bool Delete(int id);
    }
}
