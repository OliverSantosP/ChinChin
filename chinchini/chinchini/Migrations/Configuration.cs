namespace chinchini.Migrations
{
    using System;
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
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

#if DEBUG
            var userManager = new Microsoft.AspNet.Identity.UserManager<Models.ApplicationUser>(new Microsoft.AspNet.Identity.EntityFramework.UserStore<Models.ApplicationUser>(new Models.ApplicationDbContext()))

            var testUser = new Models.ApplicationUser { Name = "Don Ramon", LastName = "Perez", Email = "ramon.perez@gmail.com" };
            //testUser.Logins.Add(new Microsoft.AspNet.Identity.EntityFramework.IdentityUserLogin { UserId = "donramon" });
            
            var result = userManager.CreateAsync(testUser, "donramon");
#endif
        }
    }
}
