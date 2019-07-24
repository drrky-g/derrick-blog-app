using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using derrick_blog_app.Models;
using derrick_blog_app.Utilities;
using PagedList;

namespace derrick_blog_app.Controllers
{
    [Authorize(Roles = "Admin")]
    public class BlogPostsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: BlogPosts


        //[Authorize(Roles ="Admin")]
        //^^^^ This makes sure that you can only see this view
        //if you have an admin role
        //IT MUST BE DIRECTLY ABOVE THE ACTION YOU WANT THE AUTHORIZATION FOR

        /*This generates a view for the admin to see all posts
         even if they arent published */
        public ActionResult AdminIndex()
        {
            return View("AdminIndex",db.BlogPosts.OrderByDescending(b => b.Created).ToList());

        }
        //.OrderByDescending(b => b.Created)
        //this will order the blogposts in decending order

        /*This index will only show posts that have a boolean
        value of true for the published property */


        //  THIS WILL EVENTUALLY BE IN HOME CONTROLLER FOR PUBLIC VIEW!!!!!!
        //------------------------------------------------------------------------
        [AllowAnonymous]
        public ActionResult Index(int? page)
        {//added int? page so that page value is passed to Index action for PagedList plugin

            int pageSize = 3;
            int pageNumber = page ?? 1;

            //set variable to only show published blog posts, ordering by most recent
            var publishedBlogPosts = db.BlogPosts.Where(b => b.Published).OrderByDescending(b => b.Created).ToPagedList(pageNumber, pageSize);

            //return the view for all published posts
            return View("Index",publishedBlogPosts);
        }

        

         
        [AllowAnonymous]
        // GET: BlogPosts/Details/5
        public ActionResult Details(string Slug)
        {
            if (String.IsNullOrWhiteSpace(Slug))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var blogPost = db.BlogPosts.FirstOrDefault(p => p.Slug == Slug);

            if (blogPost == null)
            {
                return HttpNotFound();
            }
            return View(blogPost);
        }


      
        // GET: BlogPosts/Create
        public ActionResult Create() 
        {
            return View();
        }

        // POST: BlogPosts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Bind()] is used to restrict what properties will be pulled from the database
        //the strings are the names of the properties being called for the actionresult
        public ActionResult Create([Bind(Include = "Title,Abstract,Body,Published")] BlogPost blogPost, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {   
                
                //Media setting
                if (ImageUploadValidator.IsWebFriendlyImage(image))
                {
                    var fileName = Path.GetFileName(image.FileName);
                    image.SaveAs(Path.Combine(Server.MapPath("~/Uploads/"), fileName));
                    blogPost.MediaUrl = "/Uploads/" + fileName;
                }
                
                //Slug setting
                var Slug = StringUtilities.CreateSlug(blogPost.Title);
                //sets 'Slug' to call CreateSlug static class
                if (String.IsNullOrWhiteSpace(Slug))
                {//if slug is blank, show error: invalid title, return to view
                    ModelState.AddModelError("Title", "Invalid Title");
                    return View(blogPost);
                }

                if(db.BlogPosts.Any(p => p.Slug == Slug))
                {//if slug matches any other slug in the BlogPosts db, show error: title must be unique, return to view
                    ModelState.AddModelError("Title", "The title must be unique");
                    return View(blogPost);
                }
                //sets slug to equal what the CreateSlug class creates
                blogPost.Slug = Slug;

                //This will take the date automatically instead of having the user input the day itself
                blogPost.Created = DateTimeOffset.Now;
                //tell blogpost section of db to add a new blogpost data entry
                db.BlogPosts.Add(blogPost);
                //secures that entry in the database
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //if something goes wrong, you will be returned to where you submited the blogPost data
            return View(blogPost);
        }

        // GET: BlogPosts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogPost blogPost = db.BlogPosts.Find(id);
            if (blogPost == null)
            {
                return HttpNotFound();
            }
            return View(blogPost);
        }

        // POST: BlogPosts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Abstract,Slug,Body,MediaUrl,Published,Created,Updated")] BlogPost blogPost, HttpPostedFileBase image)
        {
           

            if (ModelState.IsValid)
            {

                //Media setting
                if (ImageUploadValidator.IsWebFriendlyImage(image))
                {
                    var fileName = Path.GetFileName(image.FileName);
                    image.SaveAs(Path.Combine(Server.MapPath("~/Uploads/"), fileName));
                    blogPost.MediaUrl = "/Uploads/" + fileName;
                }

                //creating new slug during edit based off of edited title
                var newSlug = StringUtilities.CreateSlug(blogPost.Title);

                //if new slug does not equal original slug, execute code inside {}
                if (newSlug != blogPost.Slug)
                {
                    if (String.IsNullOrWhiteSpace(newSlug))
                    {//if slug is empty, return to view and inform user there is no title to make slug
                        ModelState.AddModelError("Title", "Invalid Title");
                        return View(blogPost);
                    }

                    if (db.BlogPosts.Any(p => p.Slug == newSlug))
                    {//if slug matches any other existing slugs, inform user they need a unique title to generate the slug
                        ModelState.AddModelError("Title", "The title must be unique");
                        return View(blogPost);
                    }
                    //use edited slug to generate new slug
                    blogPost.Slug = newSlug;
                }

                //trying to make it grab time if you edit a page
                blogPost.Updated = DateTimeOffset.Now;

                db.Entry(blogPost).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            
            return View(blogPost);
        }

        // GET: BlogPosts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogPost blogPost = db.BlogPosts.Find(id);
            if (blogPost == null)
            {
                return HttpNotFound();
            }
            return View(blogPost);
        }

        // POST: BlogPosts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BlogPost blogPost = db.BlogPosts.Find(id);
            db.BlogPosts.Remove(blogPost);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
