
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace chinchini.Models
{
    public class Lend
    {
        [Key]
        public int LendID { get; set; }
        public float Amount { get; set; }
        public float AmountLeft { get; set; }

        public DateTime Timestamp { get; set; }

        [ForeignKey("User")]
        public string User_Id { get; set; }
        public int LoanID { get; set; }
        public virtual Loan Loan { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual Status Status { get; set; }
        public virtual List<PaymentDetail> PaymentDetails { get; set; }
    }
}
