using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Confidami.BL;
using Confidami.Common;
using Confidami.Common.Utility;
using Confidami.Model;
using Confidami.Web.FiltersAttribute;
using Confidami.Web.ViewModel;
using Microsoft.Ajax.Utilities;


namespace Confidami.Web.Controllers
{
    [RoutePrefix("cerca")]
    [Route("{action=Search}")]
    public class SearchController : BaseController
    {
               
        public ActionResult Search()
        {
            ViewBag.Title = "Ricerca segnalazione";
            ViewBag.Heding = "Intestazione per tag header cerca";
            return View(FillSearchModel(string.Empty, -1, new PostViewModel()));
        }

        [Route("filtro")]
        [Pagination(PageParam = "page")]
        public ActionResult SearchAction(SearchViewModel search,string page)
        {
            ViewBag.Title = "Ricerca segnalazione";
            ViewBag.Heding = "Intestazione per tag header cerca";

            if (!ModelState.IsValid)
                return View(ViewsStore.Search, FillSearchModel(search.Key, search.Category <= 0 ? -1 : search.Category, new PostViewModel()));

            TempData["key"] = search.Key;
            var count = 0;

            var postvm = FillPostViewModel(PostManager.SearchPosts(ViewBag.CurrentPage, search.Key,search.Category, out count));

            ViewBag.Count = count;
            ViewBag.NumberOfPages = (int)Math.Ceiling((decimal)count / Config.NumberOfPostPerPage);
            ViewBag.IsLastPage = count - (Config.NumberOfPostPerPage * (ViewBag.CurrentPage - 1)) < Config.NumberOfPostPerPage;

            return View(ViewsStore.Search, FillSearchModel(search.Key, search.Category <= 0 ? -1 : search.Category, postvm));
        }



        
        private SearchViewModel FillSearchModel(string key,int category, PostViewModel postvm)
        {
            var categories = PostManager.GetAllCategories();
            return new SearchViewModel(postvm) { Categories = categories,Key = key,Category = category};
        }
        
     


    }
}