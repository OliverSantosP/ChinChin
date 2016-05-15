using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using chinchini.Models;

namespace chinchini.Controllers
{
    public class UserController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        //
        // GET: /User/
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /User/donramon
        public ActionResult Details(string id)
        {
            // Get User profile info

            // Get User Profile
            var user = db.Users.Where(u => u.UserName == id).FirstOrDefault();

            // Get Projects backed by user
            ViewBag.projects = db.Project.Where(p => p.User.UserName == user.UserName && p.ProjectType.Description != "Donaciones").AsEnumerable();

            // Get Donations from the user
            ViewBag.donations = db.Project.Where(p => p.User.UserName == user.UserName && p.ProjectType.Description == "Donaciones").AsEnumerable();

            return View(user);
        }

        //
        // GET: /User/Projects/
        [Authorize]
        public ActionResult Projects()
        {
            var username = User.Identity.Name;

            var projects = db.Project.Include("User").Include("Loan.Payments").Include("Pitch").Include("ProjectType").Include("Status").Where(p => p.User.UserName == username).AsEnumerable();

            return View(projects);
        }
	}
}