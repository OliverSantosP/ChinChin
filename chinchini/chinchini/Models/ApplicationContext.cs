using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace chinchini.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<Project> Project { get; set; }
        public DbSet<ProjectType> ProjectType { get; set; }
        public DbSet<Pitch> Pitch { get; set; }
        public DbSet<PitchGallery> PitchGallery { get; set; }
        public DbSet<Donation> Donation { get; set; }
        public DbSet<Loan> Loan { get; set; }
        public DbSet<LoanType> LoanType { get; set; }
        public DbSet<Lend> Lend { get; set; }
        public DbSet<PaymentMethod> PaymentMethod { get; set; }


    }
}