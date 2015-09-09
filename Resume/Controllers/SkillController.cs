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

namespace Resume.Controllers
{
    [Authorize]
    public class SkillController : Controller
    {
        private ResumeDb db = new ResumeDb();

        private readonly ISession _db;
        public SkillController(ISession session)
        {
            _db = session;
        }

        // GET: /Skill/
        public ActionResult Index()
        {
            return View(db.Skills.Where(s => s.OwnerIdentity == User.Identity.Name).ToList());
        }

        // GET: /Skill/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Skill skill = db.Skills.Find(id);
            if (skill == null)
            {
                return HttpNotFound();
            }
            if(skill.OwnerIdentity != User.Identity.Name)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View(skill);
        }

        // GET: /Skill/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Skill/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SkillId,Name,Description,Level,SmallIconId,MediumIconId,LargeIconId")] Skill skill)
        {
            skill.OwnerIdentity = User.Identity.Name;
            if (ModelState.IsValid)
            {
                db.Skills.Add(skill);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(skill);
        }

        // GET: /Skill/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Skill skill = db.Skills.Find(id);
            if (skill == null)
            {
                return HttpNotFound();
            }
            if (skill.OwnerIdentity != User.Identity.Name)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return View(skill);
        }

        // POST: /Skill/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SkillId,Name,Description,Level,SmallIconId,MediumIconId,LargeIconId")] Skill skill)
        {
            // Can the user update this item?
            var existingSkill = db.Skills.AsNoTracking().SingleOrDefault(s => s.Id == skill.Id);
            if (existingSkill == null)
            {
                return HttpNotFound();
            }

            if (existingSkill.OwnerIdentity != User.Identity.Name)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            skill.OwnerIdentity = existingSkill.OwnerIdentity;

            if (ModelState.IsValid)
            {
                db.Entry(skill).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(skill);
        }

        // GET: /Skill/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Skill skill = db.Skills.Find(id);
            if (skill == null)
            {
                return HttpNotFound();
            }
            if (skill.OwnerIdentity != User.Identity.Name)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View(skill);
        }

        // POST: /Skill/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Skill skill = db.Skills.Find(id);
            if (skill == null)
            {
                return HttpNotFound();
            }
            if (skill.OwnerIdentity != User.Identity.Name)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            db.Skills.Remove(skill);
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
