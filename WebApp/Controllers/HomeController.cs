using System;
using System.Web.Mvc;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            var httpSessionStateBase = HttpContext.Session;

            ViewBag.Message = "Session is null: " + httpSessionStateBase["a"];
            return View();
        }

        public ActionResult Index()
        {
            var httpSessionStateBase = HttpContext.Session;
            var random = new Random();

            httpSessionStateBase["a"] = random.Next();

            return View();
        }
    }
}