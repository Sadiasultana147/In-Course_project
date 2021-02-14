using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookProject.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace BookProject
{
    public class Startup
    {
        public Startup(IConfiguration Configuration)
        {
            this.Configuration = Configuration;
        }
        public IConfiguration Configuration { get; set; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<BookDbContext>(o => o.UseSqlServer(this.Configuration.GetConnectionString("bookDbString")));
            services.AddCors(options => {
                options.AddPolicy("EnableCORS", builder => {
                    builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().AllowCredentials().Build();
                });
            });
            services.AddMvc()
                .AddJsonOptions(options => {
                    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, BookDbContext db)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            if (!db.Database.CanConnect())
            {
                if (db.Database.EnsureCreated())
                {
                    DbSeeder.Seed(db);
                }
            }
            app.UseCors("EnableCORS");
            app.UseStaticFiles();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

        }
    }
}
