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
    public class PositionController : ApiController
    {
        private readonly ISession db;
        public PositionController(ISession session)
        {
            db = session;
        }

        // GET api/Position
        [EnableQuery]
        [Route("api/position/user/{user}")]
        public IQueryable<Position> GetPositions(string user)
        {
            var positions = db.Query<Position>().Where(p => p.OwnerIdentity == user).FetchMany(p => p.Responsibilities).ThenFetchMany(r => r.Experiences).ThenFetch(e => e.Skill).ToList();
            return positions.AsQueryable();
        }

        // GET api/Position/5
        [ResponseType(typeof(Position))]
        public IHttpActionResult GetPosition(int id)
        {
            Position position = db.Get<Position>(id);
            if (position == null)
            {
                return NotFound();
            }

            return Ok(position);
        }

        // GET api/Position/5
        [Route("api/position/{id}/responsibilities")]
        [ResponseType(typeof(List<Responsibility>))]
        public IHttpActionResult GetResponsibilities(int id)
        {
            var position = db.Get<Position>(id);
            if (position == null)
            {
                return NotFound();
            }

            return Ok(position.Responsibilities.ToList());
        }

        // PUT api/Position/5
        public IHttpActionResult PutPosition(int id, Position position)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != position.Id)
            {
                return BadRequest();
            }

            // Ownership reassignment is not allows
            if (position.OwnerIdentity != User.Identity.Name)
                return BadRequest();

            using (var tx = db.BeginTransaction())
            {
                db.SaveOrUpdate(position);
                tx.Commit();
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST api/Position
        [ResponseType(typeof(Position))]
        public IHttpActionResult PostPosition(Position position)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Ownership reassignment is not allows
            if (position.OwnerIdentity != User.Identity.Name)
                return BadRequest();

            db.Save(position);

            return CreatedAtRoute("DefaultApi", new { id = position.Id }, position);
        }

        // DELETE api/Position/5
        [ResponseType(typeof(Position))]
        public IHttpActionResult DeletePosition(int id)
        {
            Position position = db.Get<Position>(id);
            if (position == null)
            {
                return NotFound();
            }

            // Are you allowed to delete this?
            if (position.OwnerIdentity != User.Identity.Name)
            {
                return BadRequest();
            }

            using (var tx = db.BeginTransaction())
            {
                db.Delete(position);
                tx.Commit();
            }

            return Ok(position);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PositionExists(int id)
        {
            return db.Get<Position>(id) != null;
        }
    }
}