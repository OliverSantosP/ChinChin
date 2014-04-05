using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace chinchini.Models
{
    public class PaymentMethod
    {
        public int PaymentMethodID { get; set; }
        public string Account { get; set; }
        public string Routing { get; set; }
        public string BankName { get; set; }
        public string AccountName { get; set; }

        public virtual ApplicationUser User { get; set; }
        public virtual Status Status { get; set; }
    }
}