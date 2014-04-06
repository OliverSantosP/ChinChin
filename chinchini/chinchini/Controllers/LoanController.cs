using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using chinchini.Models;

namespace chinchini.Controllers
{
    public class LoanController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        //
        // GET: /Loan/Details/id
        public ActionResult Details(string id)
        {
            var iid = Convert.ToInt32(id);

            var loan = db.Loan.Include("Status").Where(l => l.LoanID == iid).FirstOrDefault();

            //ViewData["next_payment"] = loan.NextPayment();

            return View(loan);
        }

        //
        // GET: /Loan/Pay/id
        public ActionResult Pay(string id)
        {
            var iid = Convert.ToInt32(id);

            var loan = db.Loan.Where(l => l.LoanID == iid).FirstOrDefault();

            ViewData["next_payment"] = loan.NextPayment();

            return View(loan);
        }

        //
        // POST: /Loan/Pay/id
        [HttpPost]
        [ActionName("Pay")]
        public ActionResult ProcessPay(string id)
        {
            var iid = Convert.ToInt32(id);

            var loan = db.Loan.Where(l => l.LoanID == iid).First();

            try
            {
                loan.Pay();
                return View("Details", loan);
            }
            catch(Exception e)
            {
                // payment error.
                return new HttpStatusCodeResult(500, "Ha ocurrido un error con el pago");
            }

            return View(loan);
        }
	}
}