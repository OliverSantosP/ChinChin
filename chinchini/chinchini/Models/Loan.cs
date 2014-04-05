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

       
        public virtual Status Status { get; set; }

        public int ProjectID { get; set; }
        public virtual Project Project { get; set; }

        public int LoanTypeID { get; set; }
        public virtual LoanType LoanType { get; set; }

        public virtual ApplicationUser User { get; set; }

        
    }
}