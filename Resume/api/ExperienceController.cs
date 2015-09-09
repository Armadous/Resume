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
    public class ExperienceController : ApiController
    {
        private ResumeDb db = new ResumeDb();

        // GET api/Experience
        [Queryable]
        public IQueryable<Experience> GetExperiences()
        {
            return db.Experiences.ToList().AsQueryable();
        }

        // GET api/Experience/5
        [ResponseType(typeof(Experience))]
        public IHttpActionResult GetExperience(int id)
        {
            Experience experience = db.Experiences.Find(id);
            if (experience == null)
            {
                return NotFound();
            }

            return Ok(experience);
        }

        // PUT api/Experience/5
        public IHttpActionResult PutExperience(int id, Experience experience)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != experience.Id)
            {
                return BadRequest();
            }

            db.Entry(experience).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExperienceExists(id))
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

        // POST api/Experience
        [ResponseType(typeof(Experience))]
        public IHttpActionResult PostExperience(Experience experience)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Experiences.Add(experience);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = experience.Id }, experience);
        }

        // DELETE api/Experience/5
        [ResponseType(typeof(Experience))]
        public IHttpActionResult DeleteExperience(int id)
        {
            Experience experience = db.Experiences.Find(id);
            if (experience == null)
            {
                return NotFound();
            }

            db.Experiences.Remove(experience);
            db.SaveChanges();

            return Ok(experience);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ExperienceExists(int id)
        {
            return db.Experiences.Count(e => e.Id == id) > 0;
        }
    }
}