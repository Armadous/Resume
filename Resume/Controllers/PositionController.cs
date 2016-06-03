using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Resume.Models;
using NHibernate;
using NHibernate.Linq;

using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using LinkedIn.Api.Client.Owin;
using LinkedIn.Api.Client.Owin.Profiles;
using System.Security.Claims;
using Microsoft.AspNet.Identity.EntityFramework;
using LinkedIn.Api.Client.Core.Profiles;
using System.Threading.Tasks;

namespace Resume.Controllers
{
    [Authorize]
    public class PositionController : Controller
    {
        private readonly ISession db;
        public PositionController(ISession session)
        {
            db = session;
        }

        // GET: /Position/
        public ActionResult Index()
        {
            return View(db.Query<Position>().Where(p => p.OwnerIdentity == User.Identity.Name).OrderByDescending(n => n.StartDate).ToList());
        }

        // GET: /Position/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Position position = db.Get<Position>(id);
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
                using(var tx = db.BeginTransaction())
                {
                    db.Save(position);
                    tx.Commit();
                }
                return RedirectToAction("Details", new { id = position.Id });
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
            Position position = db.Get<Position>(id);
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
        public ActionResult Edit([Bind(Include = "Id,PositionId,Title,Description,Company,StartDate,EndDate")] Position position)
        {
            // Can the user update this item?
            var existingPosition = db.Get<Position>(position.Id);
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
                using(var tx = db.BeginTransaction())
                {
                    // Maintain relationship
                    position.Responsibilities = existingPosition.Responsibilities;
                    db.Merge<Position>(position);
                    tx.Commit();
                }
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

            Position position = db.Get<Position>(id);
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
            Position position = db.Get<Position>(id);
            if (position == null)
            {
                return HttpNotFound();
            }
            // Do you own this position?
            if (position.OwnerIdentity != User.Identity.Name)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            using(var tx = db.BeginTransaction())
            {
                db.Delete(position);
                tx.Commit();
            }

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
