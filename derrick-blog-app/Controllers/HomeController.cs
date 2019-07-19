using derrick_blog_app.Models;
using System.Linq;
using System.Web.Mvc;

namespace derrick_blog_app.Controllers
{
    public class HomeController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {   //i only want to push published posts into my public facing index page (same thing i did in blog post controller)
            var publishedPosts = db.BlogPosts.Where(b => b.Published);
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}