using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace chinchini.Models
{
    public class Donation
    {
        public int DonationID { get; set; }
        public float Amount { get; set; }

        public int ProjectID { get; set; }

        [ForeignKey("User")]
        public string User_Id { get; set; }
        public virtual Project Project {get;set;}   
        public virtual ApplicationUser User{get;set;}
        
    }
}