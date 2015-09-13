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
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using LinkedIn.Api.Client.Owin;
using LinkedIn.Api.Client.Owin.Profiles;
using Microsoft.AspNet.Identity.EntityFramework;
using LinkedIn.Api.Client.Core.Profiles;

namespace Resume.Controllers
{
    public class ProfileController : Controller
    {
        private readonly ISession db;
        public ProfileController(ISession session)
        {
            db = session;
        }   

        // GET: Profiles
        public ActionResult Index()
        {
            var profile = db.Query<Profile>().SingleOrDefault(p => p.OwnerIdentity == User.Identity.Name) ?? new Profile();
            return View(profile);
        }

        // GET: Profiles/Edit/5
        public ActionResult Edit()
        {
            var profile = db.Query<Profile>().SingleOrDefault(p => p.OwnerIdentity == User.Identity.Name) ?? new Profile();
            return View(profile);
        }

        // POST: Profiles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,Summary")] Profile profile)
        {
            if (ModelState.IsValid)
            {
                using(var tx = db.BeginTransaction())
                {
                    profile.OwnerIdentity = User.Identity.Name;
                    db.SaveOrUpdate(profile);
                    tx.Commit();
                }
                
                return RedirectToAction("Index");
            }
            return View(profile);
        }

        public async Task<ActionResult> ImportLinkedIn()
        {
            var userManger = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var user = userManger.FindById(this.User.Identity.GetUserId());
            var claim = user.Claims.SingleOrDefault(m => m.ClaimType == "LinkedIn_AccessToken");

            if(claim == null)
            {
                // Redirect to connect LinkedIn
                // Drop a session nugget to get us back here
                Session["LinkLoginRedirect"] = "/Profile/ImportLinkedIn";
                return new Resume.Controllers.AccountController.ChallengeResult("LinkedIn", Url.Action("LinkLoginCallback", "Account"), User.Identity.GetUserId());
            }

            var client = new LinkedInApiClient(HttpContext.GetOwinContext().Request, claim.ClaimValue);
            var profileApi = new LinkedInProfileApi(client);
            var liProfile = await profileApi.GetBasicProfileAsync();

            // Convert the LinkedIn api object into something eaiser to bind to the view
            var importVM = new ImportLinkedInViewModel();
            importVM.FirstName = liProfile.FirstName;
            importVM.LastName = liProfile.LastName;
            importVM.Summary = liProfile.Summary;
            importVM.Positions = liProfile.Positions.Select(p => new Position()
            {
                Company = p.Company.Name,
                StartDate = new DateTime(p.StartDate.Year ?? DateTime.Now.Year, p.StartDate.Month ?? 1, p.StartDate.Day ?? 1),
                EndDate = p.EndDate == null ? (DateTime?)null : new DateTime(p.StartDate.Year ?? DateTime.Now.Year, p.StartDate.Month ?? 1, p.StartDate.Day ?? 1)
            }).ToList();

            return View(importVM);
        }

        [HttpPost]
        public ActionResult ImportLinkedIn(ImportLinkedInViewModel importProfile)
        {
            // Update profile values
            var profile = db.Query<Profile>().SingleOrDefault(p => p.OwnerIdentity == User.Identity.Name) ?? new Profile();
            profile.FirstName = importProfile.FirstName;
            profile.LastName = importProfile.LastName;
            profile.Summary = importProfile.Summary;
            profile.OwnerIdentity = User.Identity.Name;

            // Model binder is not reading the positions
            // TODO: Convert to custom model binder
            int i = 0;
            while (true)
            {
                if (i > 99)
                    break;

                var p = string.Format("Positions[{0}]", i);
                var id = Request[p + ".Id"];
                if (id == null)
                    break;

                var position = new Position();
                position.Title = Request[p + ".Title"];
                position.Company = Request[p + ".Company"];
                position.Description = Request[p + ".Description"];
                position.StartDate = DateTime.Parse(Request[p + ".StartDate"]);
                var endDate = Request[p + ".EndDate"];
                if(endDate != null)
                    position.EndDate = DateTime.Parse(Request[p + ".EndDate"]); ;
                importProfile.Positions.Add(position);

                i++;
            }

            using(var tx = db.BeginTransaction())
            {
                db.SaveOrUpdate(profile);

                foreach(var position in importProfile.Positions)
                {
                    position.OwnerIdentity = User.Identity.Name;
                    db.Save(position);
                }

                tx.Commit();
            }

            return View("ImportLinkedInSuccess");
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
