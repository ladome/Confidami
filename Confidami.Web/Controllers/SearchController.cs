using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Confidami.BL;
using Confidami.Common.Utility;
using Confidami.Model;
using Confidami.Web.ViewModel;
using Microsoft.Ajax.Utilities;


namespace Confidami.Web.Controllers
{
    public class SearchController : BaseController
    {
               
        [Route("cerca")]
        public ActionResult Index()
        {
            ViewBag.Title = "Ricerca segnalazione";
            ViewBag.Heding = "Intestazione per tag header cerca";
            return View();
        }

        
     


    }
}