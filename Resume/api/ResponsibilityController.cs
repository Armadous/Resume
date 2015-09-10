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
    public class ResponsibilityController : ApiController
    {
        private readonly ISession db;
        public ResponsibilityController(ISession session)
        {
            db = session;
        }

        // GET api/Responsibility
        [Queryable]
        public IQueryable<Responsibility> GetResponsibilities()
        {
            return db.Query<Responsibility>().AsQueryable();
        }

        // GET api/Responsibility/5
        [ResponseType(typeof(Responsibility))]
        public IHttpActionResult GetResponsibility(int id)
        {
            Responsibility responsibility = db.Get<Responsibility>(id);
            if (responsibility == null)
            {
                return NotFound();
            }

            return Ok(responsibility);
        }

        [Authorize]
        // POST api/Responsibility
        [ResponseType(typeof(Responsibility))]
        public IHttpActionResult PostResponsibility(Responsibility responsibility)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            responsibility.Position = db.Get<Position>(responsibility.PositionId);
            responsibility.Position.Responsibilities.Add(responsibility);
            db.Save(responsibility);

            return CreatedAtRoute("DefaultApi", new { id = responsibility.Id }, responsibility);
        }

        // DELETE api/Responsibility/5
        [Authorize]
        [ResponseType(typeof(Responsibility))]
        public IHttpActionResult DeleteResponsibility(int id)
        {
            Responsibility responsibility = db.Get<Responsibility>(id);
            if (responsibility == null)
            {
                return NotFound();
            }

            // Can you delete this?
            if(responsibility.Position.OwnerIdentity != User.Identity.Name)
            {
                return BadRequest();
            }

            using(var tx = db.BeginTransaction())
            {
                db.Delete(responsibility);
                tx.Commit();
            }

            return Ok();
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
            return db.Get<Responsibility>(id) != null;
        }
    }
}