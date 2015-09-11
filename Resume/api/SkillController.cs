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
using System.Web.Http.OData;
using NHibernate;
using NHibernate.Linq;

namespace Resume.api
{
    public class SkillController : ApiController
    {
        private readonly ISession db;
        public SkillController(ISession session)
        {
            db = session;
        }

        // GET api/Skill
        [EnableQuery]
        [Route("api/skill/user/{user}")]
        public IQueryable<Skill> GetSkills(string user)
        {
            return db.Query<Skill>().Where(s => s.OwnerIdentity == user).ToList().AsQueryable();
        }

        // GET api/Skill/5
        [ResponseType(typeof(Skill))]
        public IHttpActionResult GetSkill(int id)
        {
            Skill skill = db.Get<Skill>(id);
            if (skill == null)
            {
                return NotFound();
            }

            return Ok(skill);
        }

        // PUT api/Skill/5
        [Authorize]
        public IHttpActionResult PutSkill(int id, Skill skill)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != skill.Id)
            {
                return BadRequest();
            }

            // Ownership reassignment is not allows
            if (skill.OwnerIdentity != User.Identity.Name)
                return BadRequest();

            using(var tx = db.BeginTransaction())
            {
                db.SaveOrUpdate(skill);
                tx.Commit();
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST api/Skill
        [Authorize]
        [ResponseType(typeof(Skill))]
        public IHttpActionResult PostSkill(Skill skill)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Ownership reassignment is not allows
            if (skill.OwnerIdentity != User.Identity.Name)
                return BadRequest();

            db.Save(skill);

            return CreatedAtRoute("DefaultApi", new { id = skill.Id }, skill);
        }

        // DELETE api/Skill/5
        [Authorize]
        [ResponseType(typeof(Skill))]
        public IHttpActionResult DeleteSkill(int id)
        {
            Skill skill = db.Get<Skill>(id);
            if (skill == null)
            {
                return NotFound();
            }

            // Are you allowed to delete this?
            if(skill.OwnerIdentity != User.Identity.Name)
            {
                return BadRequest();
            }

            using(var tx = db.BeginTransaction())
            {
                db.Delete(skill);
                tx.Commit();
            }

            return Ok(skill);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SkillExists(int id)
        {
            return db.Get<Skill>(id) != null;
        }
    }
}