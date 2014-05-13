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

        SubtitlesRepo subtitleRepo = new SubtitlesRepo();
        RequestsRepo requestRepo = new RequestsRepo();
        AppDataContext db = new AppDataContext();
        public ActionResult Index()
        {

            MainPageModelView vm = new MainPageModelView();
            vm.Req = requestRepo.GetAllByDate();
            vm.Sub = subtitleRepo.GetNewest();
            
            /*var model = SubtitleRepo.GetAllSubtitles();
            return View(model);*/
            return View(vm);
        }
        public ActionResult RequestPage()
        {
            return View(db.Requests);
        }

        [HttpGet]
        public ActionResult NewRequest()
        {
            List<SelectListItem> Categories = new List<SelectListItem>();
            Categories.Add(new SelectListItem { Text = "Kvikmyndir", Value = "Movies" });
            Categories.Add(new SelectListItem { Text = "Þættir", Value = "Episodes" });
            Categories.Add(new SelectListItem { Text = "Annað", Value = "Other" });
            ViewBag.Categories = Categories;

            return View();
        }

        [HttpPost]
        public ActionResult NewRequest(int? id, FormCollection form)
        {
            List<SelectListItem> Categories = new List<SelectListItem>();
            Categories.Add(new SelectListItem { Text = "Kvikmyndir", Value = "Movies" });
            Categories.Add(new SelectListItem { Text = "Þættir", Value = "Episodes" });
            Categories.Add(new SelectListItem { Text = "Annað", Value = "Other" });
            ViewBag.Categories = Categories;

            Requests item = new Requests();

            requestRepo.AddRequest(item);
            UpdateModel(item);
            requestRepo.Save();
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
               return View(db.Subtitles);
           }
           
        }
        [Authorize]
        public ActionResult NewScreenText(int? id, FormCollection form)
        {
            ViewBag.Message = "Nýr Skjátexti";

            List<SelectListItem> Categories = new List<SelectListItem>();
            Categories.Add(new SelectListItem { Text = "Kvikmyndir", Value = "Movies" });
            Categories.Add(new SelectListItem { Text = "Þættir", Value = "Episodes" });
            Categories.Add(new SelectListItem { Text = "Annað", Value = "Other" });
            ViewBag.Categories = Categories;

            Subtitles item = new Subtitles();

            subtitleRepo.AddSubtitle(item);
            UpdateModel(item);
            subtitleRepo.Save();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description pagee.";

            return View();
        }

        public ActionResult Search()
        {
            ViewBag.Message = "Your application description pagee";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult EditSub(int id)
        {
            id = 133;
            Requests model = new Requests();
            model = requestRepo.GetByID(id);
            //model.File = model.File.Replace("\n \n", "<b /> yolo <br />");
            model.File = model.File.Replace("\n\r\n", "<br /> yolo <br />");
            model.File = model.File.Replace("\n", "<br />");
            return View(model);
        }

    }
}