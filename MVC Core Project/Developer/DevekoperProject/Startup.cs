using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevekoperProject.Models;
using DevekoperProject.Repositories;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DevekoperProject
{
    public class Startup
    {
        public Startup(IConfiguration Config) { this.Config = Config; }
        public IConfiguration Config { get; set; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    this.Config.GetConnectionString("DefaultConnection")));
            services.AddDbContext<ProjectDbContext>(op => op.UseSqlServer(this.Config.GetConnectionString("DbConnection")));
            services.AddScoped<IDeveloperCompanyRepo, DeveloperCompanyRepo>();
            services.AddScoped<IProjectRepo, ProjectRepo>();

            services.AddDefaultIdentity<IdentityUser>()
                   .AddDefaultUI(UIFramework.Bootstrap4)
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

                options.LoginPath = "/Account/Login";

                options.SlidingExpiration = true;
            });

            services.AddMvc()
            .AddJsonOptions(
            options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();
            
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
