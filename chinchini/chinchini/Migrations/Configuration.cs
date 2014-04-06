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
            //context.Database.ExecuteSqlCommand("DELETE FROM [AspNetUsers];");

            var testUser = new Models.ApplicationUser { Name = "Don Ramon", LastName = "Perez", Email = "ramon.perez@gmail.com", UserName = "donramon" };
            //testUser.Logins.Add(new Microsoft.AspNet.Identity.EntityFramework.IdentityUserLogin { UserId = "donramon" });

            var userManager = new Microsoft.AspNet.Identity.UserManager<Models.ApplicationUser>(new Microsoft.AspNet.Identity.EntityFramework.UserStore<Models.ApplicationUser>(context));


            userManager.CreateAsync(testUser, "donramon").Wait();
#endif



            

        }
    }
}
