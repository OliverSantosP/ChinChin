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
            Payment payment = null;

            //payment = Payments.LastOrDefault(new Payment { Amount = this.Quota, Loan = this });

            //if (payment.Status == "NOT PAID")
            //{

            //}

            return payment;
        }

        public void Pay()
        {
            // Validate stuff
            this.ValidateIntegrity();

            var payment = new Payment();
            payment.Amount = this.Amount;
            payment.Loan = this;

            Payments.Add(payment);
        }

        public bool ValidateIntegrity()
        {
            return true;
        }
    }
}