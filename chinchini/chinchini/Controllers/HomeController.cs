using chinchini.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace chinchini.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            

            var context = new ApplicationDbContext();

            try{
            // Delete the users
            //context.Database.ExecuteSqlCommand("DELETE FROM [AspNetUsers];");
            Models.ApplicationUser testUser = context.Users.Where(u => u.UserName == "donramon").FirstOrDefault();

            if (testUser == null)
            {
                testUser = new Models.ApplicationUser { Name = "Don Ramon", LastName = "Perez", Email = "ramon.perez@gmail.com", UserName = "donramon" };
                //testUser.Logins.Add(new Microsoft.AspNet.Identity.EntityFramework.IdentityUserLogin { UserId = "donramon" });

                var userManager = new Microsoft.AspNet.Identity.UserManager<Models.ApplicationUser>(new Microsoft.AspNet.Identity.EntityFramework.UserStore<Models.ApplicationUser>(context));

                userManager.CreateAsync(testUser, "donramon").Wait();
            }

            context.ProjectType.AddRange(new List<ProjectType>() {
                new ProjectType { Description = "MicroPrestamo" },
                new ProjectType { Description = "Prestamo" },
                new ProjectType { Description = "Donaciones" }});


            context.LoanType.AddRange(new List<LoanType>(){
               new LoanType { Description = "Type1" },
               new LoanType { Description = "Type2" },
               new LoanType { Description = "Type3" }});


            context.Status.AddRange(new List<Status>(){
              new Status { Description = "Activo" },
              new Status { Description = "Inactivo" },
              new Status { Description = "Pending" },
              new Status { Description = "Approved" },
              new Status { Description = "Rejected" },
              new Status { Description = "Test4" }});

            context.Category.AddRange(new List<Category>(){
         new Category { Name = "Agricultura" },
         new Category { Name = "Turismo" },
         new Category { Name = "Educacion" },
         new Category { Name = "Minorista" },
         new Category { Name = "Salud" }});


            //context.SaveChanges();
            var status = context.Status.FirstOrDefault();

            var lend1 = new Lend { Amount = 5000, AmountLeft = 2000, User_Id = testUser.Id, Timestamp = DateTime.Now };
            context.Lend.Add(lend1);

            var lend2 = new Lend { Amount = 5000, AmountLeft = 2000, User_Id = testUser.Id, Timestamp = DateTime.Now };
            context.Lend.Add(lend2);

            var pitch1 = new Pitch()
            {
                Body = "Lorem Ipsum es simplemente el texto de relleno de las imprentas y archivos de texto. Lorem Ipsum ha sido el texto de relleno estándar de las industrias desde el año 1500, cuando un impresor (N. del T. persona que se dedica a la imprenta) desconocido usó una galería de textos y los mezcló de tal manera que logró hacer un libro de textos especimen. No sólo sobrevivió 500 años, sino que tambien ingresó como texto de relleno en documentos electrónicos, quedando ",
                Description = "A little bit more",
                VideoURL = "https://www.youtube.com/watch?v=8-V-CnKcF7Q"
            };
            context.Pitch.Add(pitch1);

            var loan1 = new Loan()
            {
                Amount = 10000,
                DateRequested = DateTime.Now,
                Debt = 6000,
                LoanTypeID = 1,
                PeriodDays = 180,
                Rate = 0,
                Quota = 600,
                StatusID = 1,
                Lenders = new System.Collections.Generic.List<Lend>() { lend1,
                                                                                lend2}

            };
            context.Loan.Add(loan1);

            context.Project.Add(
                new Project
                {
                    Title = "This is my first Project",
                    StatusID = 1,
                    ProjectTypeID = 1,
                    Description = "This is the Oliver Description",
                    Amount = 10000,
                    Loan = loan1,
                    Pitch = pitch1,
                    User_Id = testUser.Id,
                    CategoryID = 1
                }
                );

            context.SaveChanges();
        }catch (DataException)
            {
                ViewBag.Message = "Your application description page.";
                return View();
            }
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            MigrateAndSeedDbIfSchemaIsOutdated();
            return View();
        }

        private void MigrateAndSeedDbIfSchemaIsOutdated()
        {
            // Disable initializer.
            Database.SetInitializer<ApplicationDbContext>(null);

            // Make sure database exists.
            using (var db = new ApplicationDbContext())
            {
                db.Database.Initialize(false);
            }

            var migrator = new DbMigrator(new Migrations.Configuration());

            if (migrator.GetPendingMigrations().Any())
            {
                // Run migrations and seed.
                migrator.Update();
            }
        }
    }
}