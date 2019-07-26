using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace derrick_blog_app.Models
{
    public class Comment
    {
        //Classes are used the same way an object factory works in javascript
        [Required]
        public int Id { get; set; }
        [Required]
        public int BlogPostId { get; set; }
        public string AuthorId { get; set; }
        public string Body { get; set; }

        //DateTimeOffset takes time, but has it realtive to prime meridian
        //takes time zone into account
        public DateTimeOffset Created { get; set; }

        //question mark means that the value is allowed to be null.
        //for example, if you just made a post
        //and it hasnt been updated since you just posted it
        //you can ignore the update property
        public DateTimeOffset? Updated { get; set; }
        public string UpdateReason { get; set; }

        //Virtual Navigation Section

        //A mechanism to get from the comment to the blog post
        //that the comment is associated with (access another class)

        public virtual BlogPost BlogPost { get; set; }

        //Application user is a class that comes by default with the framework
        public virtual ApplicationUser Author { get; set; }
        //when calling to a property of another class, use the class name without "Id" at the end

    }
}