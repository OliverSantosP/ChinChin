using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace chinchini.Models
{
    public class Status
    {
       [Key]
        public int StatusID { get; set; }
        public string Description { get; set; }

        public virtual List<Loan> Loans { get; set; }
        public virtual List<Project> Projects { get; set; }
    }
}