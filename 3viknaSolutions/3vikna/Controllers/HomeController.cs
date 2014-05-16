﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _3vikna.Models;
using _3vikna.Repositories;
using System.IO;
using System.ComponentModel.DataAnnotations;

namespace _3vikna.Controllers
{
    public class HomeController : Controller
    {

        SubtitlesRepo subtitleRepo = new SubtitlesRepo();
        RequestsRepo requestRepo = new RequestsRepo();
        AppDataContext db = new AppDataContext();

        [HttpGet]
        public ActionResult Index()
        {
            MainPageModelView vm = new MainPageModelView();
            vm.Req = requestRepo.GetAllByDate();
            vm.Sub = subtitleRepo.GetNewest();
            vm.Sub2 = subtitleRepo.NotFinished();

            return View(vm);
        }

        [HttpGet]
        public ActionResult RequestPage()
        {
            return View(requestRepo.SortByUpvotes());
        }

        [HttpGet]
        public ActionResult GetUpvotes()
        {
            var model = requestRepo.GetUpvotes();
            return Json(model, JsonRequestBehavior.AllowGet);
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
        public ActionResult NewRequest(int? id, FormCollection form, HttpPostedFileBase file)
        {
            List<SelectListItem> Categories = new List<SelectListItem>();
            Categories.Add(new SelectListItem { Text = "Kvikmyndir", Value = "Movies" });
            Categories.Add(new SelectListItem { Text = "Þættir", Value = "Episodes" });
            Categories.Add(new SelectListItem { Text = "Annað", Value = "Other" });
            ViewBag.Categories = Categories;

            Requests item = new Requests();
           
            if (file != null)
            {
                item.Extension = file.ContentType;
                item.ImageName = file.FileName;
                item.ImageBytes = ConvertToBytes(file);
            }
            if (form != null)
            {
                requestRepo.AddRequest(item);
                UpdateModel(item);
                requestRepo.Save();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult ScreenText()
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
        [HttpGet]
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
        public ActionResult NewScreenText(int? id, FormCollection form, HttpPostedFileBase uploadFile, HttpPostedFileBase file)
        {
            List<SelectListItem> Categories = new List<SelectListItem>();
            Categories.Add(new SelectListItem { Text = "Kvikmyndir", Value = "Movies" });
            Categories.Add(new SelectListItem { Text = "Þættir", Value = "Episodes" });
            Categories.Add(new SelectListItem { Text = "Annað", Value = "Other" });
            ViewBag.Categories = Categories;

            Subtitles item = new Subtitles();
            if (form != null)
            {
                UpdateModel(item);
            }
            string ext = Path.GetExtension(uploadFile.FileName);
            if (String.IsNullOrEmpty(ext) || !ext.Equals(".srt", StringComparison.OrdinalIgnoreCase))
            {          
                return View("Error");
            }
            if (uploadFile != null)
            {
                var reader = new StreamReader(uploadFile.InputStream);
                item.File = reader.ReadToEnd();
            }
            if (file != null)
            {
                item.Extension = file.ContentType;
                item.ImageName = file.FileName;
                item.ImageBytes = ConvertToBytes(file);
                subtitleRepo.AddSubtitle(item);
                subtitleRepo.Save();
                return RedirectToAction("Index");
            }
            else
            {
                subtitleRepo.AddSubtitle(item);
                subtitleRepo.Save();
                return RedirectToAction("Index");
            }
        }

        public byte[] ConvertToBytes(HttpPostedFileBase file)
        {
            byte[] imageBytes = null;
            BinaryReader reader = new BinaryReader(file.InputStream);
            imageBytes = reader.ReadBytes((int)file.ContentLength);
            return imageBytes;
        }

        [HttpGet]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description pagee.";

            return View();
        }

        public ActionResult Search(string searchBy, string search) //string searchBy, string search
        {
            if(searchBy == "MediaNameSub")
            {
                if(search == "")
                {
                   
                    return View("Search2",db.Subtitles.Select(x => x).ToList());
                }

                return View("Search2", db.Subtitles.Where(x => x.MediaNameSub.StartsWith(search) || search == null).ToList());
            }
                if (searchBy == "MediaName")
                {
                    if (search == "")
                    {
                        return View(db.Requests.Where(x => x.Category == "Other"));
                    }
                    return View(db.Requests.Where(x => x.MediaName.StartsWith(search) || search == null).ToList());
                }
                else if (searchBy == "Episodes")
                {
                    if (search == "")
                    {
                        return View(db.Requests.Where(x => x.Category == "Episodes"));
                    }
                    return View(db.Requests.Where(x => x.MediaName.StartsWith(search) || search == null).ToList());
                }
                else
                {
                    if (search == "")
                    {
                        return View(db.Requests.Where(x => x.Category == "Movies"));
                    }
                    return View(db.Requests.Where(x => x.MediaName.StartsWith(search) || search == null).ToList());
                }
            }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpGet]
        public ActionResult EditSub(int id)
        {
            Subtitles model = new Subtitles();
            model = subtitleRepo.GetByID(id);
            string lines = model.File;
            string[] line = lines.Split('\r');
            EditSub es = new EditSub();
            es.req = model;
            es.lines = line.ToList();
            return View(es);
        }

        [HttpGet]
        public ActionResult Alert()
        {
            return Json("You have liked this before", JsonRequestBehavior.AllowGet);
        }

        public void AddUpvotes(int id, string strUser)
        {
            Upvote item = new Upvote();
            item.ReqID = id;
            item.UserName = strUser;
            db.Upvote.Add(item);
            db.SaveChanges();
        }

        [HttpGet]
        [Authorize]
        public ActionResult upvoteInc(int id)
        {
            IEnumerable<string> usersWhoHaveLiked = requestRepo.UserHasLiked(id);
            foreach (var item in usersWhoHaveLiked)
            {
                if (User.Identity.Name == item)
                {
                    return Json(item, User.Identity.Name, JsonRequestBehavior.AllowGet);
                }
            }

            string strUser = User.Identity.Name;
            requestRepo.AddUpvotes(id, strUser);
            Requests movie = (from u in db.Requests
                              where u.ID == id
                              select u).SingleOrDefault();
            movie.UpvoteID += 1;
            db.SaveChanges();
            return RedirectToAction("RequestPage");
        }

        [Authorize]
        [HttpPost, ValidateInput(false)]
        public ActionResult EditSub(EditSub es)
        {
            int counter = 0;
            foreach (var item in es.lines)
            {
                counter++;
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
            subtitleRepo.UpdateDB(es.req.ID, es.req);
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
        }

        public ActionResult comment(int id)
        {
            var model = CommentRepository.Instance.GetComments(id);
            string name = subtitleRepo.getNameById(id);
            CommentViewModel cm = new CommentViewModel();
            cm.Com = model;
            cm.subtitleId = id;
            cm.MediaName = name; 
            return View(cm);
        }
        [Authorize]
        [HttpPost]
        public ActionResult comment(FormCollection formData, int id)
        {
            String strComment = formData["CommentText"];
            if (!String.IsNullOrEmpty(strComment))
            {
                Comment c = new Comment();
                c.CommentText = strComment;
                String strUser = User.Identity.Name;
                c.UserName = strUser;
                c.subtitleID = id;
                CommentRepository.Instance.AddComment(c);
                UpdateModel(c);
                CommentRepository.Instance.Save();
                return RedirectToAction("Comment");
            }
            else
            {
                ModelState.AddModelError("CommentText", "Comment text cannot be empty!");
                return Index();
            }
        }

        [Authorize(Roles = "Admin")] 
        public ActionResult SafeToPublish(int id)
        {
            Subtitles sub = new Subtitles();
            sub = subtitleRepo.GetByID(id);
            sub.IsFinished = true;
            UpdateModel(sub);
            subtitleRepo.Save();
            return RedirectToAction("Index");
        }
    }
}