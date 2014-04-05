using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace chinchini.Models
{
   public  class LoanType
    {
       public int LoanTypeID { get; set; }
       public string Description { get; set; }
       public float Max { get; set; }
       public float Min { get; set; }
    }
}
