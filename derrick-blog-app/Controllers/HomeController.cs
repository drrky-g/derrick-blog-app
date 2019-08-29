using derrick_blog_app.Models;
using System;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Web.Mvc;

namespace derrick_blog_app.Controllers
{
    [RequireHttps] //May have to change my SMTP port to the SSL one?
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
            EmailModel model = new EmailModel();

            return View(model);
        }

        //Email Contact Action
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Contact(EmailModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {

                   
                    var from = $"{model.FromEmail}-{model.FromName}<{WebConfigurationManager.AppSettings["emailto"]}>";


                    var email = new MailMessage(from, WebConfigurationManager.AppSettings["emailto"])
                    {
                        
                        Subject = model.Subject,
                        Body = $"You've got a message from {model.FromName}: <br> <hr>{model.Body}",
                        IsBodyHtml = true
                    };
                    var svc = new PersonalEmail();
                    await svc.SendAsync(email);

                    return View(new EmailModel());
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    await Task.FromResult(0);
                }
            }
            return View(model);
        }


    }
}