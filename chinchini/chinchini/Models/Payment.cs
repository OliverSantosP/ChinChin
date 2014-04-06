using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace chinchini.Models
{
    public class Payment
    {
        public int PaymentID { get; set; }
        public int LoanID { get; set; }
        public float Amount { get; set; }
        public int PaymentMethodID { get; set; }
        public string Authorization { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public byte[] Timestamp { get; set; }
        public DateTime DueDate { get; set; }

        public virtual Loan Loan { get; set; }
        public virtual Status Status { get; set; }
        public virtual List<PaymentTransaction> PaymentTransactions {get;set;}
        public virtual List<PaymentDetail> PaymentDetail { get; set; }
    }
}