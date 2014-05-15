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
    public class ResponsibilityController : Controller
    {
        private ResumeDb db = new ResumeDb();

        // GET: /Responsibility/
        public ActionResult Index()
        {
            var responsibilities = db.Responsibilities.Include(r => r.Position);
            return View(responsibilities.ToList());
        }

        // GET: /Responsibility/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Responsibility responsibility = db.Responsibilities.Find(id);
            if (responsibility == null)
            {
                return HttpNotFound();
            }
            return View(responsibility);
        }

        // GET: /Responsibility/Create
        public ActionResult Create()
        {
            ViewBag.PositionId = new SelectList(db.Positions, "PositionId", "Title");
            return View();
        }

        // POST: /Responsibility/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ResponsibilityId,Name,Description,Percentage,PositionId")] Responsibility responsibility)
        {
            if (ModelState.IsValid)
            {
                db.Responsibilities.Add(responsibility);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PositionId = new SelectList(db.Positions, "PositionId", "Title", responsibility.PositionId);
            return View(responsibility);
        }

        // GET: /Responsibility/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Responsibility responsibility = db.Responsibilities.Find(id);
            if (responsibility == null)
            {
                return HttpNotFound();
            }
            ViewBag.PositionId = new SelectList(db.Positions, "PositionId", "Title", responsibility.PositionId);
            return View(responsibility);
        }

        // POST: /Responsibility/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ResponsibilityId,Name,Description,Percentage,PositionId")] Responsibility responsibility)
        {
            if (ModelState.IsValid)
            {
                db.Entry(responsibility).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PositionId = new SelectList(db.Positions, "PositionId", "Title", responsibility.PositionId);
            return View(responsibility);
        }

        // GET: /Responsibility/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Responsibility responsibility = db.Responsibilities.Find(id);
            if (responsibility == null)
            {
                return HttpNotFound();
            }
            return View(responsibility);
        }

        // POST: /Responsibility/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Responsibility responsibility = db.Responsibilities.Find(id);
            db.Responsibilities.Remove(responsibility);
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
