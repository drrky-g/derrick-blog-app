using System.Collections.Generic;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace derrick_blog_app.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.


    //Colon in class name means it inherits the properties of what its referencing
    //In this case, ApplicationUser is also inheriting the properties of IdentityUser
    public class ApplicationUser : IdentityUser
    {
        //Application User Specific Properties

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DisplayName { get; set; }

        //Virtual Navigation Section
        public virtual ICollection<Comment> Comments { get; set; }

        //this will link the user to all the comments they have made
        public ApplicationUser()
        {
            Comments = new HashSet<Comment>();
        }


        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        //this section is telling the application to use the database to find these properties

        
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        //here, we are telling the application to go to these data sets and use
        //that data to populate the relevant class locally
        public DbSet<BlogPost>BlogPosts { get; set; }
        public DbSet<Comment>Comments { get; set; }
    }
}