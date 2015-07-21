using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Confidami.BL;
using Confidami.Common;
using Confidami.Common.Utility;
using Confidami.Model;
using Confidami.Web.ViewModel;


namespace Confidami.Web.Controllers
{
    [RoutePrefix("segnalazioni")]
    public class ContentsController : BaseController
    {

        [Route]
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

        [Route("categoria/{id}/{slug}",Name = "Sluggo")]
        public ActionResult Category(int id,string slug)
        {
            ViewBag.Title = "Segnalazioni - category name" + slug;
            ViewBag.Heding = "Intestazione per tag header index segnalazioni per categoria";
            if (slug != "test")
               return RedirectToRoutePermanent("Sluggo", new {id=id,slug="test"});
            return View("Index");
        }

        [Route("segnalazioni/{categoryName}/{id}")]
        public ActionResult SingleContent(string categoryName,long id)
        {
            if (string.IsNullOrEmpty(categoryName))
                ViewBag.Heding = "Intestazione per tag header index singola segnalazione: " +id;
            else
            {
                ViewBag.Heding = "Intestazione per tag header index singola segnalazione: " + id + "  categoria: " + categoryName;
                
            }
            return View(); //segnalazioni/nome-categoria/titolo-segnalazione
        }


        [Route("inserisci")]
        public ActionResult Insert()
        {
            ViewBag.Title = "Segnalazioni - inserisci";
            ViewBag.Heding = "Intestazione per tag header segnalazioni inserisci";
            ViewBag.CurrentUserId = CurrentUserId;

            var categories = PostManager.GetAllCategories();

            return View(FillModel());
        }

        [HttpPost]
        public ActionResult AddPost(InsertPostViewModel postVm)
        {
            if (!ModelState.IsValid)
                return View(ViewsStore.Insert, FillModel());

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
            var id = FileManager.UploadFileInTempFolder(file.InputStream, file.FileName, file.ContentType, file.ContentLength, CurrentUserId);
            return Json(id);
        }

        public JsonResult GetTempAttachMents()
        {
            return Json(FileManager.GetTempAttachMentsByUserId(CurrentUserId).Select(x => new TempAttachMentViewModel() { Name = x.FileName, Size = x.Size,Id=x.Id }), JsonRequestBehavior.AllowGet);
        }

        private InsertPostViewModel FillModel()
        {
            var categories = PostManager.GetAllCategories();
            return new InsertPostViewModel {Categories = categories};
        }

        [Route("~/Contents/DeleteAttachment/{idAttachment}")]
        public ActionResult DeleteAttachMent(int idAttachment)
        {
            var res =FileManager.GetTempAttachMentById(idAttachment);
            if(res==null)
                return Json(false, JsonRequestBehavior.AllowGet);
 
            if (!IsAdmin && res.UserId != CurrentUserId)
            {
                Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                return Json("Non puoi eliminare file di un altro utente", JsonRequestBehavior.AllowGet);
            }
            FileManager.DeleteTempAttachment(res);
            return Json(true, JsonRequestBehavior.AllowGet);
        }


    }
}