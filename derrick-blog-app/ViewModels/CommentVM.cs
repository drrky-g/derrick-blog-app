using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace derrick_blog_app.ViewModels
{
    public class CommentVM
    {
        [Required]
        public int Id { get; set; }
        public string Author { get; set; }
        [Required]
        [AllowHtml]
        public string Body { get; set; }
    }
}