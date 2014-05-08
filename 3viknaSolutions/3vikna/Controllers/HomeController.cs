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
            MainPageModelView vm = new MainPageModelView();
            vm.Req = RequestRepo.GetAllByDate();
            vm.Sub = SubtitleRepo.GetAllSubs();
            
            /*var model = SubtitleRepo.GetAllSubtitles();
            return View(model);*/
            return View(vm);
        }
        public ActionResult Request()
        {
            return View();
        }

        public ActionResult NewRequest()
        {
            return View();
        }

        public ActionResult ScreenText() //ER AD VINNA HERNA
        {
           if(User.IsInRole("Administrator"))
           {
               return View("NewRequest");
           }
           else
           {
               return View();
           }
           
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description pagee.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

    }
}