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
    public class HomeController : BaseController
    {
      
        public ActionResult Index()
        {            
            return View();
        }

        [Route("chi-siamo")]
        public ActionResult About()
        {            
            ViewBag.Title = "Chi siamo.....";
            return View();
        }

        [Route("contatti")]
        public ActionResult Contact()
        {
            ViewBag.Title = "Contatti....";

            return View();
        }

   

    }
}