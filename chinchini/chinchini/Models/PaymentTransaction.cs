using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace chinchini.Models
{
    public class PaymentTransaction
    {
        public int PaymentTransactionID { get; set; }
        public bool Rejected { get; set; }
        public string Message { get; set; }

        public int PaymentID { get; set; }
        public virtual Payment Payment { get; set; }
    }
}
