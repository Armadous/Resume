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

namespace Resume.Controllers
{
    [Authorize]
    public class ExperienceController : Controller
    {
        private readonly ISession db;
        public ExperienceController(ISession session)
        {
            db = session;
        }   

        // GET: /Experience/
        public ActionResult Index()
        {
            var experiences = db.Query<Experience>().Where(e => e.Responsibility.Position.OwnerIdentity == User.Identity.Name).Include(e => e.Responsibility).Include(e => e.Skill);
            return View(experiences.ToList());
        }

        // GET: /Experience/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Experience experience = db.Get<Experience>(id);
            if (experience == null)
            {
                return HttpNotFound();
            }
            if(experience.Responsibility.Position.OwnerIdentity != User.Identity.Name)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View(experience);
        }

        // GET: /Experience/Create
        public ActionResult Create()
        {
            ViewBag.ResponsibilityId = new SelectList(db.Query<Responsibility>().Where(r => r.Position.OwnerIdentity == User.Identity.Name), "Id", "Name");
            ViewBag.SkillId = new SelectList(db.Query<Skill>().Where(s => s.OwnerIdentity == User.Identity.Name), "Id", "Name");
            return View();
        }

        // POST: /Experience/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Value,ResponsibilityId,SkillId")] Experience experience)
        {
            if (ModelState.IsValid)
            {
                experience.Responsibility = db.Get<Responsibility>(experience.ResponsibilityId);
                experience.Responsibility.Experiences.Add(experience);
                experience.Skill = db.Get<Skill>(experience.SkillId);
                experience.Skill.Experiences.Add(experience);
                db.Save(experience);

                return RedirectToAction("Index");
            }

            ViewBag.ResponsibilityId = new SelectList(db.Query<Responsibility>().Where(r => r.Position.OwnerIdentity == User.Identity.Name), "Id", "Name");
            ViewBag.SkillId = new SelectList(db.Query<Skill>().Where(s => s.OwnerIdentity == User.Identity.Name), "Id", "Name");
            return View(experience);
        }

        // GET: /Experience/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Experience experience = db.Get<Experience>(id);
            if (experience == null)
            {
                return HttpNotFound();
            }
            if (experience.Responsibility.Position.OwnerIdentity != User.Identity.Name)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ViewBag.ResponsibilityId = new SelectList(db.Query<Responsibility>().Where(r => r.Position.OwnerIdentity == User.Identity.Name), "Id", "Name", experience.ResponsibilityId);
            ViewBag.SkillId = new SelectList(db.Query<Skill>().Where(s => s.OwnerIdentity == User.Identity.Name), "Id", "Name", experience.SkillId);
            return View(experience);
        }

        // POST: /Experience/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,Value,ResponsibilityId,SkillId")] Experience experience)
        {
            // Can the user update this item?
            var existingExperience = db.Get<Experience>(experience.Id);
            if (existingExperience == null)
            {
                return HttpNotFound();
            }

            if (existingExperience.Responsibility.Position.OwnerIdentity != User.Identity.Name)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (ModelState.IsValid)
            {
                using(var tx = db.BeginTransaction())
                {
                    experience.Skill = db.Get<Skill>(experience.SkillId);
                    experience.Responsibility = db.Get<Responsibility>(experience.ResponsibilityId);
                    db.Merge<Experience>(experience);
                    tx.Commit();
                }
                return RedirectToAction("Index");
            }
            ViewBag.ResponsibilityId = new SelectList(db.Query<Responsibility>().Where(r => r.Position.OwnerIdentity == User.Identity.Name), "Id", "Name", experience.ResponsibilityId);
            ViewBag.SkillId = new SelectList(db.Query<Skill>().Where(s => s.OwnerIdentity == User.Identity.Name), "Id", "Name", experience.SkillId);
            return View(experience);
        }

        // GET: /Experience/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Experience experience = db.Get<Experience>(id);
            if (experience == null)
            {
                return HttpNotFound();
            }
            if (experience.Responsibility.Position.OwnerIdentity != User.Identity.Name)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View(experience);
        }

        // POST: /Experience/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Experience experience = db.Get<Experience>(id);
            if (experience == null)
            {
                return HttpNotFound();
            }
            if (experience.Responsibility.Position.OwnerIdentity != User.Identity.Name)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            using(var tx = db.BeginTransaction())
            {
                db.Delete(experience);
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
