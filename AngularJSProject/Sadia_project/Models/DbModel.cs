using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Sadia_Project.Models
{
    public enum ProductType { Hardware = 1, Software, Device }
    public class Company
    {
        public Company()
        {
            this.Products = new HashSet<Product>();
            this.Services = new HashSet<Service>();
        }
        public int CompanyId { get; set; }
        [Required, StringLength(40)]
        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }
        [Required, Column(TypeName = "date"), Display(Name = "Working Since"),
            DataType(DataType.Date),
            DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Eshtablished { get; set; }
        [StringLength(200)]
        public string Address { get; set; }
        [Required, DataType(DataType.EmailAddress), StringLength(50), Display(Name = "Contact E-mail")]
        public string ContactEmail { get; set; }
        [DataType(DataType.Url), Display(Name = "Web Url")]
        public string Web { get; set; }
        //
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<Service> Services { get; set; }
    }
    public class Product
    {
        public int ProductId { get; set; }
        [Required, StringLength(40)]
        public string ProductName { get; set; }

        [EnumDataType(typeof(ProductType))]
        [JsonConverter(typeof(StringEnumConverter))]
        public ProductType Type { get; set; }

        [Required, ForeignKey("Company")]
        public int CompanyId { get; set; }
        //
        public virtual Company Company { get; set; }
    }
    public class Service
    {
        public int ServiceId { get; set; }
        [Required, StringLength(40)]
        public string ServiceName { get; set; }
       
        public bool IsContactual { get; set; }
        [Required, ForeignKey("Company")]
        public int CompanyId { get; set; }
        //
        public virtual Company Company { get; set; }
    }
    public class CompanyDbContext : DbContext
    {
        public CompanyDbContext()
        {
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
        }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Service> Services { get; set; }
    }
}