using System;
using System.Collections.Generic;
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
using Newtonsoft.Json;


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

            var vm = FillPostViewModel(res);
            return View(vm);
        }

        [Route("categoria/{id}/{slug}",Name = "CatRoute")]
        public ActionResult Category(int id,string slug)
        {
            ViewBag.Title = "Segnalazioni - category name" + slug;
            ViewBag.Heding = "Intestazione per tag header index segnalazioni per categoria";

            var res = PostManager.GetCategory(id);
            if (res == null)
                return HttpNotFound();

            if (slug != res.Slug)
                return RedirectToRoutePermanent("CatRoute", new { id = id, slug = res.Slug });
            var resFilt = PostManager.GetPostByCategory(id);
            var vm = FillPostViewModel(resFilt);
            return View("Index",vm);
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

            #region dropzone

            ViewBag.Acceptedfiles = Config.UploadAcceptedFile;
            ViewBag.ImageFormats = new MvcHtmlString(JsonConvert.SerializeObject(Config.UploadImageExtensions));
            #endregion
            ViewBag.CurrentUserId = CurrentUserId;

            var categories = PostManager.GetAllCategories();

            return View(FillInsertModel());
        }

        [HttpPost]
        public ActionResult AddPost(InsertPostViewModel postVm)
        {
            if (!ModelState.IsValid)
                return View(ViewsStore.Insert, FillInsertModel());

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
             
            try
            {
                var id = FileManager.UploadFileInTempFolder(file.InputStream, file.FileName, file.ContentType, file.ContentLength, CurrentUserId);
                return Json(new BaseResponse() { Message = id.ToString(), Success = true }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new BaseResponse() { Message = ex.ToString(), Success = false }, JsonRequestBehavior.AllowGet);
            }

        }

        public JsonResult GetTempAttachMents()
        {
            return Json(FileManager.GetTempAttachMentsByUserId(CurrentUserId).Select(x => new TempAttachMentViewModel(CurrentUserId) { Name = x.FileName, Size = x.Size, Id = x.Id }), JsonRequestBehavior.AllowGet);
        }

        private InsertPostViewModel FillInsertModel()
        {
            var categories = PostManager.GetAllCategories();
            return new InsertPostViewModel {Categories = categories};
        }

        private PostViewModel FillPostViewModel(IEnumerable<Post> posts)
        {
            var vm = new PostViewModel() { IsAdmin = IsAdmin };
            var pbase = posts.Select(x => new PostViewModelBase() { Body = x.Body, Title = x.Title, CategoryPost = x.Category.Description, IdPost = x.IdPost }).ToList();
            vm.Posts = pbase;
            return vm;
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