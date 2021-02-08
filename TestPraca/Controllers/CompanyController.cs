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
using TestPraca.Models;
using TestPraca.Helpers;
using TestPraca.Authentication;

namespace TestPraca.Controllers
{
    public class CompanyController : ApiController
    {
        private DBconn db = new DBconn();

        private ActionMethods action = new ActionMethods();


        // PUT: api/Company/5
        [BasicAuthentication]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCompany(Guid id, Company company)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != company.id)
            {
                return BadRequest();
            }

            db.Entry(company).State = EntityState.Modified;

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

        // POST: api/Company/{Company}
        [BasicAuthentication]
        [ResponseType(typeof(Guid))]
        public IHttpActionResult PostCompany(Company company)
        {
            Guid newId = action.generateUniqueId();
            company.id = newId;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            db.Companies.Add(company);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (CompanyExists(company.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            return Ok(company.id); ;
        }

        // DELETE: api/Company/5
        [BasicAuthentication]
        [ResponseType(typeof(Company))]
        public IHttpActionResult DeleteCompany(Guid id)
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

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CompanyExists(Guid id)
        {
            return db.Companies.Count(e => e.id == id) > 0;
        }
    }
}