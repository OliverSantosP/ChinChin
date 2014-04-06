using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using chinchini.Models;

namespace chinchini.Models
{
    public class Project
    {
        public Project ()
        {
            //this.Pitch = new Pitch();
        }

        public int ProjectID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public float Amount { get; set; }

        public int StatusID { get; set; }
        public int PitchID { get; set; }
        public int ProjectTypeID { get; set; }
        public int? LoanID { get; set; }

        [ForeignKey("User")]
        public string User_Id { get; set; }
        
        public virtual Status Status { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual Pitch Pitch { get; set; }
        public virtual ProjectType ProjectType { get; set; }

        public virtual Loan Loan { get; set; }

        public static float GetLendTotal(int Id)
        {
            var db = new Models.ApplicationDbContext();
            float LendTotal = db.Project.Find(Id).Loan.Lenders.Sum(x => x.Amount);
            return LendTotal;
        }

        public static float GetPercentage(int Id)
        {
            var db = new Models.ApplicationDbContext();
            float LendTotal = db.Project.Find(Id).Loan.Lenders.Sum(x => x.Amount);
            float ProjectAmmount = db.Project.Find(Id).Amount;
            float PercentageTotal = LendTotal * 100 / ProjectAmmount;
            return PercentageTotal;
        }
    }

}