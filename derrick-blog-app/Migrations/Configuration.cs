namespace derrick_blog_app.Migrations
{
    using derrick_blog_app.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<derrick_blog_app.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(derrick_blog_app.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            //this is where you add code to create admins and moderators 
            //this code runs every time you update your DB through the console (configures the database)


            //you need to add this line so you have the ability to add roles 
            //(admin, mod, etc.)
            //creates the ability to manage roles

            //create roles
            #region roleManager
            var roleManager = new RoleManager<IdentityRole>(
                new RoleStore<IdentityRole>(context));


            //if there is not a role existing yet, add an admin role
            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                roleManager.Create(new IdentityRole { Name = "Admin" });
            }

            //if there is not a role existing yet, add a moderator role
            if (!context.Roles.Any(r => r.Name == "Moderator"))
            {
                roleManager.Create(new IdentityRole { Name = "Moderator" });
            }

            #endregion

            //create users
            #region create ApplicationUsers
            //create a few users that will eventually occupy the roles of admin or moderator
            var userManager = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(context));

            //user one, Jason Twichell
            if(!context.Users.Any(u => u.Email == "JTwichell@Mailinator.com"))
            {//if there is no user with this email, create a new user
                userManager.Create(new ApplicationUser
                {//fill the ApplicationUser class with this information for this user
                    UserName = "JTwichell@Mailinator.com",
                    Email = "JTwichell@Mailinator.com",
                    FirstName = "Jason",
                    LastName = "Twichell",
                    DisplayName = "Twich"
                },/*this is the password for the user */"Abc&123!");
            }

            //user two, Gary Miller
            if(!context.Users.Any(u => u.Email == "GaryMiller@Mailinator.com"))
            {//if there is no user with this email, create a new user
                userManager.Create(new ApplicationUser
                {//fill the ApplicationUser class with this information for this user
                    UserName = "GaryMiller@Mailinator.com",
                    Email = "GaryMiller@Mailinator.com",
                    FirstName = "Gary",
                    LastName = "Miller",
                    DisplayName = "Gare"
                }, /*this is the password for the user*/ "Abc&123!");
            }

            #endregion

            //assign users to roles
            #region assign users to roles

            var userId = userManager.FindByEmail("JTwichell@Mailinator.com").Id;
            userManager.AddToRole(userId, "Admin");

            userId = userManager.FindByEmail("GaryMiller@Mailinator.com").Id;
            userManager.AddToRole(userId, "Moderator");
            #endregion 
        }
    }
}
