using System;
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
            try
            {
                MainPageViewModel viewModel = new MainPageViewModel();
                viewModel.popularRequests = requestRepo.GetAllByDate();
                viewModel.newestSubtitles = subtitleRepo.GetNewestSubtitles();
                viewModel.notFinishedSubtitles = subtitleRepo.NotFinishedSubtitles();

                return View(viewModel);
            }
            catch
            {
                return View("Error");
            }
        }

        [HttpGet]
        public ActionResult RequestPage()
        {
            try
            {
                return View(requestRepo.SortByUpvotes());
            }
            catch
            {
                return View("Error");
            }
        }

        [HttpGet]
        public ActionResult GetUpvotes()
        {
            try
            {
                var requestModel = requestRepo.GetUpvotes();
                return Json(requestModel, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return View("Error");
            }
        }

        [HttpGet]
        public ActionResult NewRequest()
        {
            try
            {
                List<SelectListItem> Categories = new List<SelectListItem>();
                Categories.Add(new SelectListItem { Text = "Kvikmyndir", Value = "Movies" });
                Categories.Add(new SelectListItem { Text = "Þættir", Value = "Episodes" });
                Categories.Add(new SelectListItem { Text = "Annað", Value = "Other" });
                ViewBag.Categories = Categories;

                return View();
            }
            catch
            {
                return View("Error");
            }
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
            if(form == null)
            {
                return View("Error");
            }
            if (form != null)
            {
                UpdateModel(item);
            }
            if (file != null)
            {
                    string fileExt = Path.GetExtension(file.FileName);
                    if (String.IsNullOrEmpty(fileExt) || fileExt.Equals(".png", StringComparison.OrdinalIgnoreCase))
                    {
                        item.Extension = file.ContentType;
                        item.ImageName = file.FileName;
                        item.ImageBytes = convert_to_bytes(file);
                    }
                    else if (String.IsNullOrEmpty(fileExt) || fileExt.Equals(".jpg", StringComparison.OrdinalIgnoreCase))
                    {
                        item.Extension = file.ContentType;
                        item.ImageName = file.FileName;
                        item.ImageBytes = convert_to_bytes(file);
                    }
                    else if (String.IsNullOrEmpty(fileExt) || fileExt.Equals(".jpeg", StringComparison.OrdinalIgnoreCase))
                    {
                        item.Extension = file.ContentType;
                        item.ImageName = file.FileName;
                        item.ImageBytes = convert_to_bytes(file);
                    }
                    else
                    {
                        return View("Error");
                    }
            }
            requestRepo.AddRequest(item);
            requestRepo.Save();
            return RedirectToAction("RequestPage");
        }

        [HttpGet]
        public ActionResult ScreenText()
        {
            try
            {
                return View(db.Subtitles);
            }
            catch
            {
                return View("Error");
            }
        }
        [Authorize]
        [HttpGet]
        public ActionResult NewScreenText()
        {
            try
            {
                List<SelectListItem> Categories = new List<SelectListItem>();
                Categories.Add(new SelectListItem { Text = "Kvikmyndir", Value = "Movies" });
                Categories.Add(new SelectListItem { Text = "Þættir", Value = "Episodes" });
                Categories.Add(new SelectListItem { Text = "Annað", Value = "Other" });
                ViewBag.Categories = Categories;

                return View();
            }
            catch
            {
                return View("Error");
            }
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
                if(uploadFile == null)
                {
                    return View("Error");
                }
                if (form != null)
                {
                    UpdateModel(item);
                }
                string uploadFileext = Path.GetExtension(uploadFile.FileName);
                if (file != null)
                {
                    string fileExt = Path.GetExtension(file.FileName);
                    if (String.IsNullOrEmpty(fileExt) || fileExt.Equals(".png", StringComparison.OrdinalIgnoreCase))
                    {
                        item.Extension = file.ContentType;
                        item.ImageName = file.FileName;
                        item.ImageBytes = convert_to_bytes(file);
                    }
                    else if (String.IsNullOrEmpty(fileExt) || fileExt.Equals(".jpg", StringComparison.OrdinalIgnoreCase))
                    {
                        item.Extension = file.ContentType;
                        item.ImageName = file.FileName;
                        item.ImageBytes = convert_to_bytes(file);
                    }
                    else if (String.IsNullOrEmpty(fileExt) || fileExt.Equals(".jpeg", StringComparison.OrdinalIgnoreCase))
                    {
                        item.Extension = file.ContentType;
                        item.ImageName = file.FileName;
                        item.ImageBytes = convert_to_bytes(file);
                    }
                    else
                    {
                        return View("Error");
                    }
                }
                if (String.IsNullOrEmpty(uploadFileext) || !uploadFileext.Equals(".srt", StringComparison.OrdinalIgnoreCase))
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
                    subtitleRepo.AddSubtitle(item);
                    subtitleRepo.Save();
                    return RedirectToAction("ScreenText");
                }
                else
                {
                    subtitleRepo.AddSubtitle(item);
                    subtitleRepo.Save();
                    return RedirectToAction("ScreenText");
                }
                //return RedirectToAction("Index");
        }

        public byte[] convert_to_bytes(HttpPostedFileBase file)
        {


            byte[] imageBytes = null;
            BinaryReader reader = new BinaryReader(file.InputStream);
            imageBytes = reader.ReadBytes((int)file.ContentLength);
            return imageBytes;

        }

        [HttpGet]
        public ActionResult About()
        {
            try
            {
                ViewBag.Message = "Your application description pagee.";

                return View();
            }
            catch
            {
                return View("Error");
            }
        }


        public ActionResult Search(string searchBy, string search, string searchBy2) //string searchBy, string search
        {
            try
            {
                if (searchBy2 == "MediaName")
                {
                    if (searchBy == "Episodes")
                    {
                        if (search == "")
                        {
                            return View(db.Requests.Where(x => x.Category == "Episodes"));
                        }
                        return View(db.Requests.Where(x => x.MediaName.StartsWith(search) || search == null).ToList());
                    }
                    else if (searchBy == "Movies")
                    {
                        if (search == "")
                        {
                            return View(db.Requests.Where(x => x.Category == "Movies"));
                        }

                        return View(db.Requests.Where(x => x.MediaName.StartsWith(search) || search == null).ToList());
                    }
                    else
                    {
                        if (searchBy == "Other")
                        {
                            if (search == "")
                            {
                                return View(db.Requests.Where(x => x.Category == "Other"));
                            }
                            return View(db.Requests.Where(x => x.MediaName.StartsWith(search) || search == null).ToList());
                        }

                    }

                }
                if (searchBy2 == "MediaNameSub")
                {
                    if (searchBy == "Episodes")
                    {
                        if (search == "")
                        {
                            return View("Search2", db.Subtitles.Where(x => x.Category == "Episodes"));
                        }
                        return View("Search2", db.Subtitles.Where(x => x.MediaNameSub.StartsWith(search) || search == null).ToList());
                    }
                    else if (searchBy == "Movies")
                    {
                        if (search == "")
                        {
                            return View("Search2", db.Subtitles.Where(x => x.Category == "Movies"));
                        }
                        return View("Search2", db.Subtitles.Where(x => x.MediaNameSub.StartsWith(search) || search == null).ToList());
                    }
                    else
                    {
                        if (searchBy == "Other")
                        {
                            if (search == "")
                            {
                                return View("Search2", db.Subtitles.Where(x => x.Category == "Other"));
                            }
                            return View("Search2", db.Subtitles.Where(x => x.MediaNameSub.StartsWith(search) || search == null).ToList());
                        }

                    }
                }
                return View("Search2", db.Subtitles.Where(x => x.Category == "Movies"));
            }
            catch
            {
                return View("Error");
            }
        }

        public ActionResult Help()
        {
            try
            {
                ViewBag.Message = "Your contact page.";

                return View();
            }
            catch
            {
                return View("Error");
            }
        }

        [HttpGet]
        public ActionResult EditSub(int? id)
        {
            try
            {
                if(id == null)
                {
                    throw new NullReferenceException();
                }
                Subtitles model = new Subtitles();
                model = subtitleRepo.GetSubtitleByID(id);
                string lines = model.File;
                string[] line = lines.Split('\r');
                EditSub es = new EditSub();
                es.editSubtitle = model;
                es.lines = line.ToList();
                return View(es);
            }
            catch
            {
                return View("Error");
            }
        }

        public void add_upvotes(int id, string strUser)
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
            try
            {
                IEnumerable<string> usersWhoHaveLiked = requestRepo.UserHasLiked(id);
                foreach (var item in usersWhoHaveLiked)
                {
                    if (User.Identity.Name == item)
                    {
                        return RedirectToAction("RequestPage");
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
            catch
            {
                return View("Error");
            }
        }

        [Authorize]
        [HttpPost, ValidateInput(false)]
        public ActionResult EditSub(EditSub es)
        {
            try
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
                    es.editSubtitle.File += stuff; // bætir inn í strenginn i gangnagrinnun,
                }
                subtitleRepo.UpdateSubtitleDB(es.editSubtitle.ID, es.editSubtitle);
                return RedirectToAction("RequestPage");
            }
            catch
            {
                return View("Error");
            }
        }

        public ActionResult Download(int? id)
        {
            try
            {
                if (id.HasValue == false)
                {
                    throw  new NullReferenceException();
                }
                string str = subtitleRepo.GetSubtitleFileFromDB(id);
                byte[] bytes = new byte[str.Length * sizeof(char)];
                System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
                string name = subtitleRepo.GetSubtitleNameById(id);
                name = name + ".srt";
                return File(bytes, "txt/srt", name);
            }
            catch (NullReferenceException ex)
            {
                return View("Error");
            }
        }


        public ActionResult Comment(int? id)
        {
            try
            {
                if(id.HasValue)
                {
                    var model = CommentRepository.Instance.GetCommentsById(id);
                    string name = subtitleRepo.GetSubtitleNameById(id);
                    if(name == null)
                    {
                        throw new Exception();
                    }
                    CommentViewModel cm = new CommentViewModel();
                    cm.comments = model;
                    cm.subtitleId = id.Value;
                    cm.MediaName = name;
                    return View(cm);
                }
                else
                {
                    throw new NullReferenceException();
                }
            }
            catch
            {
                return View("Error");
            }
        }
        [Authorize]
        [HttpPost]
        public ActionResult Comment(FormCollection formData, int id)
        {
            try
            {
                String strComment = formData["CommentText"];
                if(strComment == "")
                {
                    throw new Exception();
                }
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
            catch
            {
                return View("Error");
            }
        }

        [Authorize(Roles = "Admin")]
        public ActionResult SafeToPublish(int id)
        {
            try
            {
                Subtitles sub = new Subtitles();
                sub = subtitleRepo.GetSubtitleByID(id);
                sub.IsFinished = true;
                UpdateModel(sub);
                subtitleRepo.Save();
                return RedirectToAction("ScreenText");
            }
            catch
            {
                return View("Error");
            }
        }
    }
}