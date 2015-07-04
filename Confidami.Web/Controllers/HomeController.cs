using System.Linq;
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

        public ActionResult AddPost(PostViewModel post)
        {
            if (!ModelState.IsValid)
                return View("Index",FillPostViewMoldel());
            new PostManager().AddPost(new Post()
            {
                Body = post.Body,
                Category = new Category()
                {
                    IdCategory = post.IdCategory,
                },
                Title = post.Title,
                SlugUrl = ""
            });
            return Redirect("Index");
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