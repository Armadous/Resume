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
using Resume.Models;

namespace Resume.api
{
    public class ResponsibilityController : ApiController
    {
        private ResumeDb db = new ResumeDb();

        // GET api/Responsibility
        public IQueryable<Responsibility> GetResponsibilities()
        {
            return db.Responsibilities;
        }

        // GET api/Responsibility/5
        [ResponseType(typeof(Responsibility))]
        public IHttpActionResult GetResponsibility(int id)
        {
            Responsibility responsibility = db.Responsibilities.Find(id);
            if (responsibility == null)
            {
                return NotFound();
            }

            return Ok(responsibility);
        }

        // PUT api/Responsibility/5
        public IHttpActionResult PutResponsibility(int id, Responsibility responsibility)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != responsibility.ResponsibilityId)
            {
                return BadRequest();
            }

            db.Entry(responsibility).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ResponsibilityExists(id))
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

        // POST api/Responsibility
        [ResponseType(typeof(Responsibility))]
        public IHttpActionResult PostResponsibility(Responsibility responsibility)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Responsibilities.Add(responsibility);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = responsibility.ResponsibilityId }, responsibility);
        }

        // DELETE api/Responsibility/5
        [ResponseType(typeof(Responsibility))]
        public IHttpActionResult DeleteResponsibility(int id)
        {
            Responsibility responsibility = db.Responsibilities.Find(id);
            if (responsibility == null)
            {
                return NotFound();
            }

            db.Responsibilities.Remove(responsibility);
            db.SaveChanges();

            return Ok(responsibility);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ResponsibilityExists(int id)
        {
            return db.Responsibilities.Count(e => e.ResponsibilityId == id) > 0;
        }
    }
}