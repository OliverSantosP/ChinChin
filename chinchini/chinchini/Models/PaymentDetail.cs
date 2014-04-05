using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace chinchini.Models
{
    public class PaymentDetail
    {
        public int PaymentDetailID { get; set; }
        public byte[] TimeStamp { get; set; }

        public int PaymentID { get; set; }
        public virtual Payment Payment { get; set; }
    }
}