using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Resume.Models;

namespace Resume.Controllers
{
    [Authorize]
    public class PositionController : Controller
    {
        private ResumeDb db = new ResumeDb();

        // GET: /Position/
        public ActionResult Index()
        {
            return View(db.Positions.Where(p => p.OwnerIdentity == User.Identity.Name).OrderByDescending(n => n.StartDate).ToList());
        }

        // GET: /Position/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Position position = db.Positions.Find(id);
            if (position == null)
            {
                return HttpNotFound();
            }
            if(position.OwnerIdentity != User.Identity.Name)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View(position);
        }

        // GET: /Position/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Position/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Title,Description,Company,StartDate,EndDate")] Position position)
        {
            position.OwnerIdentity = User.Identity.Name;

            if (ModelState.IsValid)
            {
                db.Positions.Add(position);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(position);
        }

        // GET: /Position/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Position position = db.Positions.Find(id);
            if (position == null)
            {
                return HttpNotFound();
            }
            // Do you own this position?
            if (position.OwnerIdentity != User.Identity.Name)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View(position);
        }

        // POST: /Position/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PositionId,Title,Description,Company,StartDate,EndDate")] Position position)
        {
            // Can the user update this item?
            var existingPosition = db.Positions.AsNoTracking().SingleOrDefault(p=> p.PositionId == position.PositionId);
            if(existingPosition == null)
            {
                return HttpNotFound();
            }

            if(existingPosition.OwnerIdentity != User.Identity.Name)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            position.OwnerIdentity = existingPosition.OwnerIdentity;

            if (ModelState.IsValid)
            {
                db.Entry(position).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(position);
        }

        // GET: /Position/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Position position = db.Positions.Find(id);
            if (position == null)
            {
                return HttpNotFound();
            }
            // Do you own this position?
            if (position.OwnerIdentity != User.Identity.Name)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View(position);
        }

        // POST: /Position/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Position position = db.Positions.Find(id);
            if (position == null)
            {
                return HttpNotFound();
            }
            // Do you own this position?
            if (position.OwnerIdentity != User.Identity.Name)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            db.Positions.Remove(position);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
