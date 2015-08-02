using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Confidami.Common;
using Confidami.Model;
using Confidami.Web.FiltersAttribute;
using Confidami.Web.ViewModel;
using Microsoft.Ajax.Utilities;

namespace Confidami.Web.Controllers
{
    [RoutePrefix("PannelloAdmin")]
    [Authorize(Roles = RolesStore.AdminRole)]
    public class ManagerController : BaseController
    {

        // GET: Post
        [Route("Modera")]
        public ActionResult Moderation()
        {
            var res = PostManager.GetPostByStatus(PostStatus.OnApprovation);
            return View(FillModel(res));
        }

        [HttpPost]
        [MultipleButton(Name = "action", Argument = "Approve")]
        public ActionResult Approve(long idPost)
        {
            var res = PostManager.ApprovePost(idPost);
            return RedirectToAction(ActionsStore.Moderation);
        }

        [HttpPost]
        [MultipleButton(Name = "action", Argument = "Reject")]
        public ActionResult Reject(long idPost,string returnUrl)
        {
            var res = PostManager.RejectPost(idPost);
            if (string.IsNullOrEmpty(returnUrl))
                return RedirectToAction(ActionsStore.Moderation);
            return Redirect(returnUrl);
        }

        [HttpPost]
        [MultipleButton(Name = "action", Argument = "OnApprovation")]
        public ActionResult OnApprovation(long idPost)
        {
            var res = PostManager.OnApprovationPost(idPost);
            if (Request.UrlReferrer != null) return Redirect(Request.UrlReferrer.ToString());
            return View(ViewsStore.Moderation);
        }

        private static ModerationViewModel FillModel(IEnumerable<PostLight> posts)
        {
            return new ModerationViewModel()
            {
                Posts =
                    posts.Select(
                        x =>
                            new PostViewModelBase
                            {
                                Body = x.Body,
                                CategoryPost = x.Category.Description,
                                IdCategory = x.Category.IdCategory,
                                IdPost = x.IdPost,
                                Title = x.Title
                            })
            };
        }
    }
}