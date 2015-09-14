using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.WebPages;
using Confidami.Common;
using Confidami.Common.Utility;
using Confidami.Model;
using Confidami.Web.FiltersAttribute;
using Confidami.Web.Helpers;
using Confidami.Web.ViewModel;
using Newtonsoft.Json;


namespace Confidami.Web.Controllers
{
    [RoutePrefix("segnalazioni")]
    public class ContentsController : BaseController
    {

        [Pagination(PageParam = "page")]
        [Route]
        [Route("categoria/{id}/{slug}", Name = "CatRoute")]
        public ActionResult Index(string page, int? id, string slug)
        {
            IEnumerable<PostLight> res = null;
            int currentPage = ViewBag.CurrentPage;

            if (!string.IsNullOrEmpty(page) && page == Constants.ViewAllPage)
            {
                res = PostManager.GetAllPost();
                return View(FillPostViewModel(res));
            }

            var count = 0;

            //Senza categoria
            if (!id.HasValue)
            {
                ViewBag.Title = "Segnalazioni";
                ViewBag.Heding = "Intestazione per tag header index segnalazioni";

                res = PostManager.GetPostsPaged(currentPage, out count);
            }
             //Con categoria
            else
            {
                ViewBag.Title = "Segnalazioni - category name" + slug;
                ViewBag.Heding = "Intestazione per tag header index segnalazioni per categoria";
                ViewBag.IdCategory = id;

                var cat = PostManager.GetCategory(id.GetValueOrDefault());
                if (cat == null)
                    return HttpNotFound();

                if (slug != cat.Slug)
                    return RedirectToRoutePermanent("CatRoute", new { id = id, slug = cat.Slug });

                res = PostManager.GetPostByCategoryPaged(currentPage, id.GetValueOrDefault(), out count);
            }

            ViewBag.Count = count;
            ViewBag.NumberOfPages = (int)Math.Ceiling((decimal)count / Config.NumberOfPostPerPage);
            ViewBag.IsLastPage = count - (Config.NumberOfPostPerPage * (currentPage)) < Config.NumberOfPostPerPage;

            return View(FillPostViewModel(res));


        }

        [Route("{categoryName}/{id:long}/{slugTitle?}", Name = "SingleContentRoute")]
        public ActionResult SingleContent(string categoryName,long id,string slugTitle)
        {
            var res = PostManager.GetPost(id);
            if (res == null)
                return HttpNotFound();

            if (slugTitle != null)
            {
                if (slugTitle != res.SlugUrl)
                    return RedirectToRoutePermanent("SingleContentRoute",
                        new {categoryName = categoryName, id = id, slugTitle = res.SlugUrl});
            }

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

            ViewBag.Acceptedfiles = new MvcHtmlString(JsonConvert.SerializeObject(Config.AcceptedExtensions));
            ViewBag.ImageFormats = new MvcHtmlString(JsonConvert.SerializeObject(Config.UploadImageExtensions));
            #endregion
            ViewBag.CurrentUserId = CurrentUserId;


            return View(FillInsertModel());
        }

        [HttpPost]
        [ValidateInput(false)]
        [Route("inserisci")]
        public ActionResult Insert(InsertPostViewModel postVm)
        {
            if (!ModelState.IsValid)
                return View(ViewsStore.Insert, FillInsertModel());

            var post = new Post
            {
                Body = Server.HtmlEncode(postVm.Body),
                Category = new Category { IdCategory = postVm.IdCategory },
                Title = Server.HtmlEncode(postVm.Title),
                UserId = CurrentUserId
            };

            var res = PostManager.AddPost(post);

            //return RedirectToAction(ActionsStore.EditCode, );
            return RedirectToRoute(RouteStore.EditPostView, new { idPost = res.Message });

        }

        [Route("codici-di-modifica",Name = "FindEditPostView")]
        public ActionResult FindEditCode()
        {
            return View(new EditPostCodeViewModel());
        }

        [Route("codici-di-modifica", Name = "FindEditPostSubmit")]
        [HttpPost]
        public ActionResult FindEditCode(EditPostCodeViewModel model)
        {
            model.CannotBeNull("EditPostCodeViewModel");
            if (!ModelState.IsValid)
                return View(model);

            var res = PostManager.CheckEditCode(model.EditCode,model.Password);
            if (!res.Success)
            {
                ModelState.AddModelError("",res.Message);
                 return View(model);
            }

            return RedirectToAction(ActionsStore.EditPost, new { id = res.PostLigh.IdPost, slugUrl = res.PostLigh.SlugUrl });
        }

        [Route("{id:long}/{slugUrl}/modifica", Name = "EditPost")]
        [HttpGet]
        public ActionResult Edit(long id)
        {
            ViewBag.Title = "Segnalazioni - modifica";
            ViewBag.Heding = "Modifca denuncia";

            var post = PostManager.GetpostLight(id);

            return View(ViewsStore.Insert,FillEditViewModel(post));
            //todo redirigere a modifica
        }

        [Route("{id:long}/{slugUrl}/modifica", Name = "EditPostSave")]
        [HttpPost]
        public ActionResult Edit(EditPostViewModel model)
        {
            model.CannotBeNull("insertpostviewModel");
            var post = PostManager.GetpostLight(model.IdPost);

            if (!ModelState.IsValid)
            {
                return View(ViewsStore.Insert, FillEditViewModel(model));
            }
            PostManager.UpdatePost(
                new Post()
                {
                    Category = new Category() { IdCategory = model.IdCategory},
                    Title = model.Title,
                    Body = model.Body,
                    Status = post.Status,
                    TimeStamp = post.TimeStamp,
                    TimeStampApprovation = post.TimeStampApprovation,
                    UserId = post.UserId,
                    SlugUrl = post.SlugUrl,
                    EditCode = post.EditCode,
                    IdPost = post.IdPost
                }
                );
            return View(ViewsStore.Insert, FillEditViewModel(post));
        }


        [Route("{idPost}/codice-modifica", Name = "EditPostView")]
        public ActionResult EditPostCode(long idPost)
        {
            var res = PostManager.GetpostLight(idPost);
            if (res == null || res.UserId != CurrentUserId)
            {
                return HttpNotFound();
            }
            return View(model: new InsertEditPostCodeViewModel() { IdPost = res.IdPost, EditCode = res.EditCode });
        }

        [HttpPost]
        [Route("{idPost}/codice-modifica", Name = "EditPostInsert")]
        public ActionResult EditPostCode(InsertEditPostCodeViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var res = PostManager.InserEditInfo(
                new PostEdit()
                {
                    Email = model.Email,
                    IdPost = model.IdPost,
                    Password = model.Password,
                    UserId = CurrentUserId
                }
                );

            if (!res.Success)
                ModelState.AddModelError("", res.Message);
            return View("EditPostCode",model);
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
            return Json(FileManager.GetTempAttachMentsByUserId(CurrentUserId).Select(x => new TempAttachMentViewModel(CurrentUserId)
            {
                Name = x.FileName, Size = x.Size, Id = x.IdPostAttachment
            }), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAttachMentsByIdPost(long idPost)
        {
            return Json(FileManager.GetAttachMentsByIdPost(idPost).Select(x => new PostAttachMentViewModel(idPost.ToString(CultureInfo.InvariantCulture))
            {
                Name = x.FileName, Size = x.Size, Id = x.IdPostAttachment
            }), JsonRequestBehavior.AllowGet);
        }

        private InsertPostViewModel FillInsertModel()
        {
            var categories = PostManager.GetAllCategories();
            return new InsertPostViewModel {Categories = categories};
        }

        private EditPostViewModel FillEditViewModel(PostLight postLight)
        {
            #region dropzone
            ViewBag.Acceptedfiles = new MvcHtmlString(JsonConvert.SerializeObject(Config.AcceptedExtensions));
            ViewBag.ImageFormats = new MvcHtmlString(JsonConvert.SerializeObject(Config.UploadImageExtensions));
            #endregion

            ViewBag.CurrentUserId = CurrentUserId;
            ViewBag.IdPost = postLight.IdPost;

            var categories = PostManager.GetAllCategories();
            return new EditPostViewModel
            {
                Categories = categories,
                Body = Server.HtmlEncode(postLight.Body),
                IdCategory = postLight.Category.IdCategory,
                Title = postLight.Title,
                IsModifica = true,
                IdPost = postLight.IdPost
            };
        }

        private EditPostViewModel FillEditViewModel(EditPostViewModel model)
        {
            #region dropzone
            ViewBag.Acceptedfiles = new MvcHtmlString(JsonConvert.SerializeObject(Config.AcceptedExtensions));
            ViewBag.ImageFormats = new MvcHtmlString(JsonConvert.SerializeObject(Config.UploadImageExtensions));
            #endregion

            ViewBag.CurrentUserId = CurrentUserId;
            ViewBag.IdPost = model.IdPost;

            var categories = PostManager.GetAllCategories();
            return new EditPostViewModel
            {
                Categories = categories,
                Body = model.Body,
                IdCategory = model.IdCategory,
                Title = model.Title,
                IsModifica = true,
                IdPost = model.IdPost
            };
        }


        private InsertEditPostCodeViewModel FillInsertEditCodeModel(long idPost, string editCode)
        {
            return new InsertEditPostCodeViewModel {};
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