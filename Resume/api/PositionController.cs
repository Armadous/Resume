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
    public class PositionController : ApiController
    {
        private ResumeDb db = new ResumeDb();

        // GET api/Position
        public IQueryable<Position> GetPositions()
        {
            return db.Positions;
        }

        // GET api/Position/5
        [ResponseType(typeof(Position))]
        public IHttpActionResult GetPosition(int id)
        {
            Position position = db.Positions.Find(id);
            if (position == null)
            {
                return NotFound();
            }

            return Ok(position);
        }

        // PUT api/Position/5
        public IHttpActionResult PutPosition(int id, Position position)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != position.PositionId)
            {
                return BadRequest();
            }

            db.Entry(position).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PositionExists(id))
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

        // POST api/Position
        [ResponseType(typeof(Position))]
        public IHttpActionResult PostPosition(Position position)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Positions.Add(position);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = position.PositionId }, position);
        }

        // DELETE api/Position/5
        [ResponseType(typeof(Position))]
        public IHttpActionResult DeletePosition(int id)
        {
            Position position = db.Positions.Find(id);
            if (position == null)
            {
                return NotFound();
            }

            db.Positions.Remove(position);
            db.SaveChanges();

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
            return db.Positions.Count(e => e.PositionId == id) > 0;
        }
    }
}