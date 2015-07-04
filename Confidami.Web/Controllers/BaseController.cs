using System.Web.Mvc;
using Confidami.BL;
using Confidami.Common;

namespace Confidami.Web.Controllers
{
    public class BaseController : Controller
    {
        private PostManager _postManager;

        public PostManager PostManager
        {
            get { return new PostManager();}
            set { _postManager = value; }
        }

        public bool IsAdmin { get { return User.IsInRole(RolesStore.AdminRole); } }
    }
}