using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace derrick_blog_app.Models
{
    public class BlogPost
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Abstract { get; set; }
        public string Slug { get; set; }
        public string Body { get; set; }
        public string MediaUrl { get; set; }
        public bool Published { get; set; }
        public DateTimeOffset Created { get; set; }
        public DateTimeOffset? Updated { get; set; }

        //Virtual Navigation
        
        //telling the BlogPost class to grab all comments that are related to 
        //this blog post
        public virtual ICollection<Comment> Comments { get; set; }

        //---------------------------------------------------------

        //constructor to pull comments associated with the blog post

        public BlogPost()
        {
            this.Comments = new HashSet<Comment>();
        }

    }
}