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
using NHibernate;
using NHibernate.Linq;

namespace Resume.api
{
    public class ExperienceController : ApiController
    {
        private readonly ISession db;
        public ExperienceController(ISession session)
        {
            db = session;
        }   

        // GET api/Experience
        [Route("api/experience/user/{user}")]
        [Queryable]
        public IQueryable<Experience> GetExperiences(string user)
        {
            return db.Query<Experience>().Where(e => e.Responsibility.Position.OwnerIdentity == User.Identity.Name ).AsQueryable();
        }

        // GET api/Experience/5
        [ResponseType(typeof(Experience))]
        public IHttpActionResult GetExperience(int id)
        {
            Experience experience = db.Get<Experience>(id);
            if (experience == null)
            {
                return NotFound();
            }

            return Ok(experience);
        }

        //// POST api/Experience
        //[ResponseType(typeof(Experience))]
        //public IHttpActionResult PostExperience(Experience experience)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.Experiences.Add(experience);
        //    db.SaveChanges();

        //    return CreatedAtRoute("DefaultApi", new { id = experience.Id }, experience);
        //}

        //// DELETE api/Experience/5
        //[ResponseType(typeof(Experience))]
        //public IHttpActionResult DeleteExperience(int id)
        //{
        //    Experience experience = db.Experiences.Find(id);
        //    if (experience == null)
        //    {
        //        return NotFound();
        //    }

        //    db.Experiences.Remove(experience);
        //    db.SaveChanges();

        //    return Ok(experience);
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        //private bool ExperienceExists(int id)
        //{
        //    return db.Experiences.Count(e => e.Id == id) > 0;
        //}
    }
}