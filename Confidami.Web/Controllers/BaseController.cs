using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Confidami.BL;
using Confidami.Common;
using Confidami.Model;
using Confidami.Web.ViewModel;
using Microsoft.Owin;

namespace Confidami.Web.Controllers
{
    [HttpCookieFilter]
    [RoutePrefix("Route")]
    public class BaseController : Controller
    {
        private PostManager _postManager;
        private FileManager _fileManager;
        private Validation _validation;

        public string CurrentUserId { get; set; }

        public PostManager PostManager
        {
            get { return new PostManager();}
            set { _postManager = value; }
        }

        public FileManager FileManager
        {
            get { return new FileManager(); }
            set { _fileManager = value; }
        }

        public Validation ValidationManager
        {
            get { return new Validation(); }
            set { _validation = value; }
        }

        public bool IsAdmin { get { return User.IsInRole(RolesStore.AdminRole); } }


        public ActionResult Menu()
        {
            var cats = PostManager.GetAllCategories();
            return PartialView(ViewsStore.Menu, new Menu {Categories = cats.ToList()});
        }

        public ActionResult AdminMenu()
        {
            return PartialView(ViewsStore.MenuAdmin);
        }

        protected JsonResult CreateJsonResponse<T>(T Object, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            Response.StatusCode = (int)statusCode;
            return Json(Object, JsonRequestBehavior.AllowGet);
        }

        protected PostViewModel FillPostViewModel(IEnumerable<PostLight> posts)
        {
            var vm = new PostViewModel();
            var pbase = posts.Select(x => new PostViewModelBase()
            {
                Body = x.Body,
                Title = x.Title,
                CategoryPost = x.Category.Description,
                IdPost = x.IdPost,
                HasAttachMents = x.HasAttachments,
                CategorySlug = x.Category.Slug,
                TitleSlug = x.SlugUrl
            }).ToList();
            vm.Posts = pbase;
            return vm;
        }
    }

    public class HttpCookieFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var controller = filterContext.Controller as BaseController;
            if (controller != null)
            {
                const string cookieName = "currentId";
                var cookieItem = filterContext.RequestContext.HttpContext.Request.Cookies["currentId"];

                if (cookieItem == null)
                {
                    cookieItem = new HttpCookie(cookieName)
                    {
                        Domain = "",
                        Value = Guid.NewGuid().ToString()
                    };
                    filterContext.RequestContext.HttpContext.Response.Cookies.Add(cookieItem);
                    controller.CurrentUserId = cookieItem.Value;
                }

                controller.CurrentUserId = cookieItem.Value;
            }
            base.OnActionExecuting(filterContext);
        }
    }
}