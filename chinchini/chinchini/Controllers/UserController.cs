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
            var user = new Models.ApplicationUser();
            user.Name = "Don Ramon";

            return View(user);
        }
	}
}