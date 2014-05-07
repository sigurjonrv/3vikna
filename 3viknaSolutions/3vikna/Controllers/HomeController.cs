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
        SubtitlesRepo SubtitleRepo = new SubtitlesRepo();

        RequestsRepo RequestRepo = new RequestsRepo();
        public ActionResult Index()
        {
            var model = SubtitleRepo.GetAllSubtitles();
            return View(model);
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