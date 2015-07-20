using System.Linq;
using System.Web;
using System.Web.Mvc;
using Confidami.BL;
using Confidami.Common;
using Confidami.Common.Utility;
using Confidami.Model;
using Confidami.Web.ViewModel;


namespace Confidami.Web.Controllers
{
    public class ContentsController : BaseController
    {

        [Route("segnalazioni")]
        public ActionResult Index()
        {
            ViewBag.Title = "Segnalazioni";
            ViewBag.Heding = "Intestazione per tag header index segnalazioni";
            //recupero tutti i post
            //TODO paginazione
            var res = PostManager.GetAllPost();
            var vm = new PostViewModel() {IsAdmin = IsAdmin};
            var posts = res.Select(x => new PostViewModelBase() { Body = x.Body, Title = x.Title, CategoryPost = x.Category.Description, IdPost = x.IdPost }).ToList();
            vm.Posts = posts;
            return View(vm);
        }

        [Route("segnalazioni/categoria/{slug}")]
        public ActionResult Category(string slug)
        {
            ViewBag.Title = "Segnalazioni - category name" + slug;
            ViewBag.Heding = "Intestazione per tag header index segnalazioni per categoria";
            return View("Index");
        }

        [Route("segnalazioni/{id}")]
        public ActionResult GetSingleContent(long id)
        {
            return View("Index"); //segnalazioni/nome-categoria/titolo-segnalazione
        }


        [Route("segnalazioni/inserisci")]
        public ActionResult Insert()
        {
            ViewBag.Title = "Segnalazioni - inserisci";
            ViewBag.Heding = "Intestazione per tag header segnalazioni inserisci";
            ViewBag.CurrentUserId = CurrentUserId;

            var categories = PostManager.GetAllCategories();

            return View(new InsertPostViewModel{Categories = categories});
        }

        [HttpPost]
        public ActionResult AddPost(InsertPostViewModel postVm)
        {
            if (!ModelState.IsValid)
                return View(ViewsStore.Insert, postVm);

            var post = new Post
            {
                Body = postVm.Body,
                Category = new Category { IdCategory = postVm.IdCategory },
                Title = postVm.Title,
                SlugUrl = "",
                UserId = CurrentUserId
            };

            PostManager.AddPost(post);

            return RedirectToAction(ActionsStore.ContentsInsert);
        }


        public ActionResult Upload(HttpPostedFileWrapper file)
        {
            file.CannotBeNull("file");
            FileManager.UploadFileInTempFolder(file.InputStream, file.FileName, file.ContentType, file.ContentLength, CurrentUserId);
            return Json(false);
        }

        public JsonResult GetTempAttachMents()
        {
            return Json(FileManager.GetTempAttachMentsByUserId(CurrentUserId).Select(x => new TempAttachMentViewModel() { Name = x.FileName, Size = x.Size }), JsonRequestBehavior.AllowGet);
        }

        //public JsonResult DeleteAttachMent(string idAttachment)
        //{
        //    return ""
        //}


    }
}