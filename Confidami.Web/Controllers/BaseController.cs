using System;
using System.Web;
using System.Web.Mvc;
using Confidami.BL;
using Confidami.Common;
using Microsoft.Owin;

namespace Confidami.Web.Controllers
{
    [HttpCookieFilter]
    public class BaseController : Controller
    {
        private PostManager _postManager;
        private FileManager _fileManager;

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

        public bool IsAdmin { get { return User.IsInRole(RolesStore.AdminRole); } }
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
    }}