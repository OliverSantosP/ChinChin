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


            //context.SaveChanges();
            var status = context.Status.FirstOrDefault();

            var lend1 = new Lend { Amount = 5000, AmountLeft = 2000, User_Id = user.Id, Timestamp = DateTime.Now };
            context.Lend.AddOrUpdate(l => l.LendID, lend1);

            var lend2 = new Lend { Amount = 5000, AmountLeft = 2000, User_Id = user.Id, Timestamp = DateTime.Now };
            context.Lend.AddOrUpdate(l => l.LendID, lend2);

            var pitch1 = new Pitch()
                    {
                        Body = "Lorem Ipsum es simplemente el texto de relleno de las imprentas y archivos de texto. Lorem Ipsum ha sido el texto de relleno estándar de las industrias desde el año 1500, cuando un impresor (N. del T. persona que se dedica a la imprenta) desconocido usó una galería de textos y los mezcló de tal manera que logró hacer un libro de textos especimen. No sólo sobrevivió 500 años, sino que tambien ingresó como texto de relleno en documentos electrónicos, quedando ",
                        Description = "A little bit more",
                        VideoURL = "https://www.youtube.com/watch?v=8-V-CnKcF7Q"
                    };
            context.Pitch.AddOrUpdate(p => p.PitchID, pitch1);

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
            context.Loan.AddOrUpdate(l => l.LoanID, loan1);

            context.Project.AddOrUpdate(p => p.Title,
                new Project
                {
                    Title = "This is my first Project",
                    StatusID = 1,
                    ProjectTypeID = 1,
                    Description = "This is the Oliver Description",
                    Amount = 10000,
                    Loan = loan1,
                    Pitch = pitch1,
                    User_Id = user.Id
                }
                );

            context.SaveChanges();

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
