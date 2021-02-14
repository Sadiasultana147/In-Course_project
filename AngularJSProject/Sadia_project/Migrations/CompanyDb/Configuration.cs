namespace Sadia_Project.Migrations.CompanyDb
{
    using Sadia_Project.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Sadia_Project.Models.CompanyDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Migrations\CompanyDb";
        }

        protected override void Seed(Sadia_Project.Models.CompanyDbContext db)
        {
            Company c = new Company { CompanyName = "Apple Inc", Eshtablished = DateTime.Parse("1981-07-01").Date,
                Address = "Address", ContactEmail = "enquire@apple.com", Web = "https://www.apple.com" };
            c.Products.Add(new Product { ProductName = "iPhone", Type = ProductType.Device });
            c.Products.Add(new Product { ProductName = "MacBook", Type = ProductType.Device });
            c.Services.Add(new Service { ServiceName = "iCloud", IsContactual = true });
            db.Companies.Add(c);
            db.SaveChanges();
        }
    }
}
