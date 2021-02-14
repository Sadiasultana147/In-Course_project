using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DevekoperProject.Models
{
    
        public class DeveloperCompany
        {
            public DeveloperCompany() { this.Projects = new List<Project>(); }
            [Display(Name = "Company ID")]
            public int DeveloperCompanyId { get; set; }
            [Required, StringLength(30), Display(Name = "Company Name")]
            public string DeveloperCompanyName { get; set; }
            [Required, DataType(DataType.Date), Column(TypeName = "date"), Display(Name = "Established Date"),
                DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
            public DateTime EstablishedDate { get; set; }

            public virtual ICollection<Project> Projects { get; set; }
        }
        public class Project
        {
            public Project()
            {
                this.Buildings = new List<Building>();
            }
            [Display(Name = "Project ID")]
            public int ProjectId { get; set; }
            [Required, StringLength(30), Display(Name = "Place Name")]
            public string PlaceName { get; set; }

            [Required, ForeignKey("DeveloperCompany"), Display(Name = "Company ID")]
            public int DeveloperCompanyId { get; set; }
            public virtual DeveloperCompany DeveloperCompany { get; set; }
            public virtual ICollection<Building> Buildings { get; set; }

        }
        public class Building
        {
            [Display(Name = "Building ID")]
            public int BuildingId { get; set; }
            [Required, StringLength(30), Display(Name = "Building Name")]
            public string BuildingName { get; set; }
            [Required, StringLength(80), Display(Name = "Address ")]
            public string Address { get; set; }
            [Required, StringLength(30), Display(Name = "Total Floor")]
            public string TotalFloor { get; set; }
            [Required, StringLength(30), Display(Name = "Flat Size")]
            public string FlatSize { get; set; }
            [Required, Display(Name = "Flat Price")]
            public decimal FlatPrice { get; set; }
            [Required, ForeignKey("Project")]
            public int ProjectId { get; set; }
            public virtual Project Project { get; set; }
        }

        public class ProjectDbContext : DbContext
        {
            public ProjectDbContext(DbContextOptions<ProjectDbContext> options) : base(options) { }
            public DbSet<DeveloperCompany> DeveloperCompanies { get; set; }
            public DbSet<Project> Projects { get; set; }
            public DbSet<Building> Buildings { get; set; }

        }
    
}
