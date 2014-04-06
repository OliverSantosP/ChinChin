namespace chinchini.Migrations
{
    using chinchini.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<chinchini.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(chinchini.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //

#if DEBUG
            // Delete the users
            context.Database.ExecuteSqlCommand("DELETE FROM [AspNetUsers];");

            var testUser = new Models.ApplicationUser { Name = "Don Ramon", LastName = "Perez", Email = "ramon.perez@gmail.com", UserName = "donramon" };
            //testUser.Logins.Add(new Microsoft.AspNet.Identity.EntityFramework.IdentityUserLogin { UserId = "donramon" });

            var userManager = new Microsoft.AspNet.Identity.UserManager<Models.ApplicationUser>(new Microsoft.AspNet.Identity.EntityFramework.UserStore<Models.ApplicationUser>(context));


            userManager.CreateAsync(testUser, "donramon").Wait();
#endif



            var user = testUser;

            context.ProjectType.AddOrUpdate(pt => pt.Description,
                new ProjectType { Description = "MicroPrestamo" },
                new ProjectType { Description = "Prestamo" },
                new ProjectType { Description = "Donaciones" });


            context.LoanType.AddOrUpdate(pt => pt.Description,
               new LoanType { Description = "Type1" },
               new LoanType { Description = "Type2" },
               new LoanType { Description = "Type3" });


            context.Status.AddOrUpdate(s => s.Description,
              new Status { Description = "Activo" },
              new Status { Description = "Inactivo" },
              new Status { Description = "Test1" },
              new Status { Description = "Test2" },
              new Status { Description = "Test3" },
              new Status { Description = "Test4" });


            context.SaveChanges();
            var status = context.Status.FirstOrDefault();


            List<Lend> lenders = new List<Lend>() { new Lend { Amount=5000, AmountLeft=2000, User_Id = user.Id, Timestamp= DateTime.Now },
                                                    new Lend { Amount=5000, AmountLeft=2000, User_Id = user.Id , Timestamp= DateTime.Now}
                                                  };

            Loan l = new Loan()
            {
                Amount = 10000,
                DateRequested = DateTime.Now,
                Debt = 6000,
                LoanTypeID = 1,
                PeriodDays = 180,
                Rate = 0,
                Quota = 600,
                StatusID = 1
            };

            l.Lenders = lenders;


            Project proj = new Project()
                {
                    Title = "This is my first Project",
                    StatusID = 1,
                    ProjectTypeID = 1,
                    Description = "This is the Oliver Description",
                    Amount = 10000,
                    User_Id = user.Id
                };


            proj.Loan = l;
            proj.Pitch = new Pitch()
                    {
                        Body = "Lorem Ipsum es simplemente el texto de relleno de las imprentas y archivos de texto. Lorem Ipsum ha sido el texto de relleno est�ndar de las industrias desde el a�o 1500, cuando un impresor (N. del T. persona que se dedica a la imprenta) desconocido us� una galer�a de textos y los mezcl� de tal manera que logr� hacer un libro de textos especimen. No s�lo sobrevivi� 500 a�os, sino que tambien ingres� como texto de relleno en documentos electr�nicos, quedando ",
                        Description = "A little bit more",
                        VideoURL = "https://www.youtube.com/watch?v=8-V-CnKcF7Q"
                    };
            context.Project.AddOrUpdate(p => p.Title,
                proj
                );

            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

        }
    }
}
