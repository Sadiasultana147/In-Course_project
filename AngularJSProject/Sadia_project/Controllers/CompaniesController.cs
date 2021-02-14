using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Sadia_Project.Models;

namespace Sadia_Project.Controllers
{
    public class CompaniesController : ApiController
    {
        private CompanyDbContext db = new CompanyDbContext();

        // GET: api/Companies
        public IQueryable<Company> GetCompanies()
        {
            return db.Companies;
        }

        // GET: api/Companies/5
        [ResponseType(typeof(Company))]
        public IHttpActionResult GetCompany(int id)
        {
            Company company = db.Companies.Find(id);
            if (company == null)
            {
                return NotFound();
            }

            return Ok(company);
        }

        // PUT: api/Companies/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCompany(int id, Company company)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != company.CompanyId)
            {
                return BadRequest();
            }
            var ext = db.Companies.Include(x => x.Products).Include(y => y.Services).First(x => x.CompanyId == company.CompanyId);
            ext.CompanyName = company.CompanyName;
            ext.Address = company.Address;
            ext.Eshtablished = company.Eshtablished;
            ext.ContactEmail = company.ContactEmail;
            ext.Web = company.Web;
            if (company.Products != null && company.Products.Count > 0)
            {
                var prs = company.Products.ToArray();
                for (var i = 0; i < prs.Length; i++)
                {
                    var temp = ext.Products.FirstOrDefault(c => c.ProductId == prs[i].ProductId);
                    if (temp != null)
                    {
                        temp.ProductName = prs[i].ProductName;
                        temp.Type = prs[i].Type;
                    }
                    else
                    {
                        ext.Products.Add(prs[i]);
                    }
                }
                prs = ext.Products.ToArray();
                for (var i=0; i< prs.Length;i++)
                {
                    var temp = company.Products.FirstOrDefault(x => x.ProductId == prs[i].ProductId);
                    if (temp == null)
                        db.Entry(prs[i]).State = EntityState.Deleted;
                }
            }
            if (company.Services != null && company.Services.Count > 0)
            {
                var srvs = company.Services.ToArray();
                for (var i = 0; i < srvs.Length; i++)
                {
                    var temp = ext.Services.FirstOrDefault(c => c.ServiceId == srvs[i].ServiceId);
                    if (temp != null)
                    {
                        temp.ServiceName = srvs[i].ServiceName;
                        temp.IsContactual = srvs[i].IsContactual;
                    }
                    else
                    {
                        ext.Services.Add(srvs[i]);
                    }
                }
                srvs = ext.Services.ToArray();
                for (var i =0; i< srvs.Length; i++)
                {
                    var temp = company.Services.FirstOrDefault(x => x.ServiceId == srvs[i].ServiceId);
                    if (temp == null)
                        db.Entry(srvs[i]).State = EntityState.Deleted;
                }
            }
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompanyExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Companies
        [ResponseType(typeof(Company))]
        public IHttpActionResult PostCompany(Company company)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            db.Companies.Add(company);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = company.CompanyId }, company);
        }

        // DELETE: api/Companies/5
        [ResponseType(typeof(Company))]
        public IHttpActionResult DeleteCompany(int id)
        {
            Company company = db.Companies.Find(id);
            if (company == null)
            {
                return NotFound();
            }

            db.Companies.Remove(company);
            db.SaveChanges();

            return Ok(company);
        }
        /// <summary>
        /// ////////////////////////////////////////////////
        /// Custom Actions
        /// </summary>
        /// <param name="disposing"></param>
        /// 
        [Route("custom/Companies")]
        public IQueryable<Company> GetCompaniesWithRelated()
        {
            return db.Companies
                .Include("Products")
                .Include("Services");
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        [Route("custom/Companies/{id}")]
        public IHttpActionResult GetCompanyWithRelated(int id)
        {
            return Ok( db.Companies
                    .Include(x=> x.Products)
                    .Include(x => x.Services)
                    .FirstOrDefault(x=> x.CompanyId == id));
        }
        private bool CompanyExists(int id)
        {
            return db.Companies.Count(e => e.CompanyId == id) > 0;
        }
    }
}