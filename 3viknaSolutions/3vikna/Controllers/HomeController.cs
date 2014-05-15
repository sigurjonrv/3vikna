using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _3vikna.Models;
using _3vikna.Repositories;
using System.IO;

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
        public ActionResult GetUpvotes()
        {
            var model = requestRepo.GetUpvotes();
            return Json(model, JsonRequestBehavior.AllowGet);
        }

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
        public ActionResult NewRequest(int? id, FormCollection form, HttpPostedFileBase uploadFile)
        {
            List<SelectListItem> Categories = new List<SelectListItem>();
            Categories.Add(new SelectListItem { Text = "Kvikmyndir", Value = "Movies" });
            Categories.Add(new SelectListItem { Text = "Þættir", Value = "Episodes" });
            Categories.Add(new SelectListItem { Text = "Annað", Value = "Other" });
            ViewBag.Categories = Categories;

            var reader = new StreamReader(uploadFile.InputStream);
            Requests item = new Requests();

            item.File = reader.ReadToEnd();
            if (form != null)
            {
                requestRepo.AddRequest(item);
                UpdateModel(item);
                requestRepo.Save();
                return RedirectToAction("Index");
            }
            else
            {
                return View("Error");
            }
        }



        public ActionResult ScreenText() //ER AD VINNA HERNA
        {
            if (User.IsInRole("Administrator"))
            {
                List<SelectListItem> Categories = new List<SelectListItem>();
                Categories.Add(new SelectListItem { Text = "Kvikmyndir", Value = "Movies" });
                Categories.Add(new SelectListItem { Text = "Þættir", Value = "Episodes" });
                Categories.Add(new SelectListItem { Text = "Annað", Value = "Other" });
                ViewBag.Categories = Categories;
                return View("NewRequest");
            }
            else
            {
                return View(db.Subtitles);
            }

        }
        [Authorize]
        public ActionResult NewScreenText()
        {
            List<SelectListItem> Categories = new List<SelectListItem>();
            Categories.Add(new SelectListItem { Text = "Kvikmyndir", Value = "Movies" });
            Categories.Add(new SelectListItem { Text = "Þættir", Value = "Episodes" });
            Categories.Add(new SelectListItem { Text = "Annað", Value = "Other" });
            ViewBag.Categories = Categories;

            return View();
        }
        [Authorize]
        [HttpPost]
        public ActionResult NewScreenText(int? id, FormCollection form, HttpPostedFileBase uploadFile)
        {
            List<SelectListItem> Categories = new List<SelectListItem>();
            Categories.Add(new SelectListItem { Text = "Kvikmyndir", Value = "Movies" });
            Categories.Add(new SelectListItem { Text = "Þættir", Value = "Episodes" });
            Categories.Add(new SelectListItem { Text = "Annað", Value = "Other" });
            ViewBag.Categories = Categories;

            Subtitles item = new Subtitles();
            if (uploadFile != null)
            {
                var reader = new StreamReader(uploadFile.InputStream);
                item.File = reader.ReadToEnd();
            }
            if (form != null)
            {
                subtitleRepo.AddSubtitle(item);
                UpdateModel(item);
                subtitleRepo.Save();
                return RedirectToAction("Index");
            }
            else
            {
                return View("Error");
            }
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
            Subtitles model = new Subtitles();
            model = subtitleRepo.GetByID(id);
            string lines = model.File;
            string[] line = lines.Split('\r');
            EditSub es = new EditSub();
            es.req = model;
            es.lines = line.ToList();
            //model.File = model.File.Replace("\n \n", "<b /> yolo <br />");
            //model.File = model.File.Replace("\n\r\n", "<br /> yolo <br />");
            //model.File = model.File.Replace("\n", "<br />");
            return View(es);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult EditSub(EditSub es)
        {

            foreach (var item in es.lines)
            {
                var stuff = item;
                if (!stuff.StartsWith(Environment.NewLine))
                {
                    if (stuff.Length == 0) // athugar hvort að koðinn sé nokkuð tóma strengur
                    {                       //og ef hann er það setur hann bil í staðinn, þarf að vera 3 á lengd.
                        stuff = stuff + "   ";
                    }
                    else if (stuff.Substring(0, 1).All(Char.IsLetter) && (stuff.Length == 1))// bætir bili fyrir aftan
                    {                                                                       //ef strengurinn nær ekki lagmarks lengd
                        stuff = stuff + " " + " ";
                    }
                    else if (stuff.Substring(0, 1).All(Char.IsLetter) && (stuff.Length == 2))//sama her
                    {
                        stuff = stuff + " ";
                    }
                    if (stuff == "1")// þetta er special-cage fyri fyrsta strenginn i skránni
                    {

                    }
                    else
                    {
                        stuff = Environment.NewLine + stuff; // bætir við \n fyrir framan hverja línu
                    }

                }
                es.req.File += stuff; // bætir inn í strenginn i gangnagrinnun,
            }
            //es.req.File = string.Join("\r", es.lines.ToArray());
            subtitleRepo.UpdateDB(es.req.ID, es.req);
            //model.File = model.File.Replace("\n \n", "<b /> yolo <br />");
            //model.File = model.File.Replace("\n\r\n", "<br /> yolo <br />");
            //model.File = model.File.Replace("\n", "<br />");
            return RedirectToAction("RequestPage");
        }

        public ActionResult Download(int id) 
        {
            string str = subtitleRepo.GetFileFromDB(id);
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            string name = subtitleRepo.getNameById(id);
            name = name + ".srt";
            return File(bytes, "txt/srt", name);
            /*var file = db.Subtitles.First(f => f.ID == id);
            return File(file.Data.ToArray(), "application/octet-stream",)*/
        }


    }
}