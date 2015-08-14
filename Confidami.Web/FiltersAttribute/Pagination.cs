using System;
using System.Web.Mvc;
using System.Web.WebPages;
using Confidami.Common;
using Confidami.Web.Helpers;

namespace Confidami.Web.FiltersAttribute
{
    public class Pagination : ActionFilterAttribute
    {

        public string PageParam { get; set; }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.ActionParameters.ContainsKey(PageParam))
            {
                var viewBag = filterContext.Controller.ViewBag;
                var page = filterContext.ActionParameters[PageParam] as string;

                if (!string.IsNullOrEmpty(page) && page != Constants.ViewAllPage && !page.IsInt())
                    filterContext.Result = new HttpNotFoundResult();

                if (page != null && page.Trim() == string.Empty)
                    filterContext.Result = new HttpNotFoundResult();


                viewBag.NextPage = -1;
                viewBag.PreviuosPage = -1;


                int currentPage = string.IsNullOrEmpty(page) ? 1 : int.Parse(page);

                if (currentPage < 1)
                    filterContext.Result = new HttpNotFoundResult();


                viewBag.CurrentPage = currentPage;
                viewBag.NextPage = currentPage + 1;
                viewBag.PreviuosPage = currentPage - 1;
                viewBag.IsFirstPage = currentPage == 1;

            }
            base.OnActionExecuting(filterContext);
        }

    }
}