using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
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

        [Route("{categoryName}/{id}", Name = "SingleContentRoute")]
        public ActionResult SingleContent(string categoryName,long id)
        {
            var res = PostManager.GetPost(id);
            var vm = FillinglePostViewModel(res);
            if (!string.IsNullOrEmpty(categoryName))
            {
                ViewBag.Heding = String.Format("{0}", vm.Title);
                ViewBag.CategoryName = vm.CategoryPost;

            }
            return View(vm); //segnalazioni/nome-categoria/titolo-segnalazione
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

            if (ValidationManager.FileAlreadyExists(CurrentUserId, file.FileName))
            {
                return
                    CreateJsonResponse(new BaseResponseExtended() { Success = false, Message = ValidationManager.ErrorCodes[ErrorCode.FilePresent], ErrorCode = (int)ErrorCode.FilePresent }, HttpStatusCode.BadRequest);
            }

            if (!ValidationManager.IsAdmittedExtension(file.FileName))
            {
                return
                    CreateJsonResponse(new BaseResponseExtended() { Success = false, Message = ValidationManager.ErrorCodes[ErrorCode.NotAdmittedExtension], ErrorCode = (int)ErrorCode.NotAdmittedExtension }, HttpStatusCode.BadRequest);
            }

            try
            {
                var id = FileManager.UploadFileInTempFolder(file.InputStream, file.FileName, file.ContentType, file.ContentLength, CurrentUserId);
                return CreateJsonResponse(new BaseResponseExtended() { Message = id.ToString(), Success = true });

            }
            catch (Exception ex)
            {
                return CreateJsonResponse(new BaseResponseExtended() { Message = ex.ToString(), Success = false },HttpStatusCode.InternalServerError);
            }

        }

        public JsonResult GetTempAttachMents()
        {
            return Json(FileManager.GetTempAttachMentsByUserId(CurrentUserId).Select(x => new TempAttachMentViewModel(CurrentUserId) { Name = x.FileName, Size = x.Size, Id = x.IdPostAttachment }), JsonRequestBehavior.AllowGet);
        }

        private InsertPostViewModel FillInsertModel()
        {
            var categories = PostManager.GetAllCategories();
            return new InsertPostViewModel {Categories = categories};
        }

        private PostViewModel FillPostViewModel(IEnumerable<PostLight> posts)
        {
            var vm = new PostViewModel() { IsAdmin = IsAdmin };
            var pbase = posts.Select(x => new PostViewModelBase()
            {
                Body = x.Body,
                Title = x.Title,
                CategoryPost = x.Category.Description,
                IdPost = x.IdPost,
                HasAttachMents = x.HasAttachments,
                CategorySlug = x.Category.Slug
            }).ToList();
            vm.Posts = pbase;
            return vm;
        }

        private PostViewModelSingleContent FillinglePostViewModel(Post post)
        {
            return new PostViewModelSingleContent()
            {
                Body = post.Body,
                Title = post.Title,
                CategoryPost = post.Category.Description,
                IdPost = post.IdPost,
                AttachMenents = post.Attachments.Select(x=> new PostAttachMentViewModel(post.IdPost.ToString(CultureInfo.InvariantCulture)){
                    Id = x.Id,
                    Name = x.FileName,
                    Size = x.Size
                    
                }).ToList(),
                CreationDate = post.TimeStamp
            };
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