using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace chinchini.Controllers
{
    public class UserController : Controller
    {
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
            var db = new Models.ApplicationDbContext();

            // Get User Profile
            var user = db.Users.Where(u => u.UserName == id).FirstOrDefault();

            // Get Projects backed by user
            ViewBag.projects = db.Project.Where(p => p.User.UserName == user.UserName && p.ProjectType.Description != "Donaciones").AsEnumerable();

            // Get Donations from the user
            ViewBag.donations = db.Project.Where(p => p.User.UserName == user.UserName && p.ProjectType.Description == "Donaciones").AsEnumerable();

            return View(user);
        }
	}
}