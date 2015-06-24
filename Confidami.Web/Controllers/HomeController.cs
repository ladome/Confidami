using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using Confidami.BL;
using Confidami.Common;
using Confidami.Web.ViewModel;

namespace Confidami.Web.Controllers
{
    public class HomeController : Controller
    {


        public ActionResult Index()
        {
            var res = new PostManager().GetAllPost();
            var vm = res.Select(x => new PostViewModel() {Body = x.Body, Title = x.Title}).ToList();
            return View(new PostViewModel() { Posts = vm });
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

    }
}