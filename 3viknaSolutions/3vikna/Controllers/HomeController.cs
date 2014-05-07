using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _3vikna.Models;
using _3vikna.Repositories;

namespace _3vikna.Controllers
{
    public class HomeController : Controller
    {
        SubtitlesRepo repo = new SubtitlesRepo();
        public ActionResult Index()
        {
            var model = repo.GetAllSubtitles();
            return View(model);
        }
        public ActionResult Request()
        {
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