using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Confidami.BL;
using Confidami.Model;
using Confidami.Web.ViewModel;

namespace Confidami.Web.Controllers
{
    public class HomeController : BaseController
    {
        private readonly PostManager _postManager;

        public HomeController()
        {
            _postManager = new PostManager();
        }

        public ActionResult Index()
        {
            TempData["from"] = Request.Url;
            return View(FillPostViewMoldel());
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

        public ActionResult AddPost(PostViewModel postVm)
        {
            if (!ModelState.IsValid)
                return View("Index",FillPostViewMoldel());

            var post = new Post();
            post.Body = postVm.Body;
            post.Category = new Category() {IdCategory = postVm.IdCategory};
            post.Title = postVm.Title;
            post.SlugUrl = "";

            if (Request.Files.Count > 0)
            {
                foreach (string file in Request.Files)
                {
                    var httpFile = Request.Files[file];
                    if (httpFile != null)
                    {
                        post.Attachments.Add(new PostAttachments() {FileName = httpFile.FileName,InputStream = httpFile.InputStream,ContentType = httpFile.ContentType,ContentLenght =httpFile.ContentLength});
                    }
                }
            }
            PostManager.AddPost(post);

            //if (!HandleFileUpload(Request.Files))
            //{
            //    ModelState.AddModelError("files", "File not loaded correctly");
            //    return View("Index", FillPostViewMoldel());
            //}
            return RedirectToAction("Index");
        }


        private bool HandleFileUpload(HttpFileCollectionBase files)
        {
            var isSavedSuccessfully = true;
            string fName = "";
            try
            {
                foreach (string fileName in files)
                {
                    HttpPostedFileBase file = Request.Files[fileName];
                    //Save file content goes here
                    fName = file.FileName;
                    if (file != null && file.ContentLength > 0)
                    {

                        var originalDirectory = new DirectoryInfo(string.Format("{0}Images\\WallImages", Server.MapPath(@"\")));

                        string pathString = System.IO.Path.Combine(originalDirectory.ToString(), "imagepath");

                        var fileName1 = Path.GetFileName(file.FileName);

                        bool isExists = System.IO.Directory.Exists(pathString);

                        if (!isExists)
                            System.IO.Directory.CreateDirectory(pathString);

                        var path = string.Format("{0}\\{1}", pathString, file.FileName);
                        file.SaveAs(path);

                    }

                }

            }
            catch (Exception ex)
            {
                isSavedSuccessfully = false;
            }

            return isSavedSuccessfully;
        }

        private PostViewModel FillPostViewMoldel()
        {
            var res = _postManager.GetAllPost();
            var posts = res.Select(x => new PostViewModelBase(){ Body = x.Body, Title = x.Title, CategoryPost =x.Category.Description,IdPost = x.IdPost}).ToList();
            var categories = _postManager.GetAllCategories();
            return new PostViewModel() { Posts = posts, Categories = categories, IsAdmin = IsAdmin,ReturnUrl = Request.RawUrl};
        }

    }
}