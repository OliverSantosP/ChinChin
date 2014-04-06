using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace chinchini.Models
{
    public class Loan
    {

        public int LoanID { get; set; }
        public float Amount { get; set; }
        public float Rate { get; set; }
        public float Quota { get; set; }
        public float Debt { get; set; }
        public int PeriodDays { get; set; }
        public DateTime DateRequested { get; set; }
        [Timestamp]
        public Byte[] LastUpdatedDate { get; set; }


        public int StatusID { get; set; }
        public virtual Status Status { get; set; }

        public int LoanTypeID { get; set; }
        public virtual LoanType LoanType { get; set; }


        public virtual List<Payment> Payments { get; set; }
        public virtual List<Lend> Lenders { get; set; }

        protected Payment CalculateNextPayment()
        {
            var payment = new Payment();

            return payment;
        }

        public Payment NextPayment()
        {
            var db = new ApplicationDbContext();
            var pending = db.Status.Where(s => s.Description == "Pendiente").FirstOrDefault();

            Payment payment = null;
            Payment newPayment = null;

            payment = Payments.OrderByDescending(p => p.DueDate).FirstOrDefault();

            if (payment == null)
            {
                newPayment = new Payment { Amount = this.Quota, DueDate = this.DateRequested.AddDays(this.PeriodDays), Loan = this, Status = pending};
            }
            else
            {
                newPayment = new Payment { Amount = this.Quota, DueDate = payment.DueDate.AddDays(this.PeriodDays), Loan = this, Status = pending};
            }

            return newPayment;
        }

        public void Pay()
        {
            var db = new ApplicationDbContext();
            var paid = db.Status.Where(s => s.Description == "Pagado").FirstOrDefault();

            // Validate stuff
            this.ValidateIntegrity();

            var lastPayment = Payments.OrderByDescending(p => p.DueDate).FirstOrDefault();
            lastPayment.Status = paid;

            var newPayment = this.NextPayment();

            this.Payments.Add(newPayment);
        }

        public bool ValidateIntegrity()
        {
            return true;
        }
    }
}