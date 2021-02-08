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
using TestPraca.Authentication;
using TestPraca.Helpers;
using TestPraca.Models;

namespace TestPraca.Controllers
{
    public class ResultsController : ApiController
    {
        private DBconn db = new DBconn();

        private ActionMethods action = new ActionMethods();   
        


        // POST: api/Results{SearchCriteria}
        [BasicAuthentication]
        [ResponseType(typeof(Results))]
        public IHttpActionResult PostEmployee(SearchCriteria search)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Results results = action.getAllSearchResults(search);            

            return Ok(results);
        }       

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EmployeeExists(Guid id)
        {
            return db.Employees.Count(e => e.Id == id) > 0;
        }
    }
}