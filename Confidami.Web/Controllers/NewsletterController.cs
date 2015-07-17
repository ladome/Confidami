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
    public class NewsletterController : BaseController
    {

        [Route("newsletter")]
        public ActionResult Index()
        {
            ViewBag.Title = "Newsletter";
            ViewBag.Message = "Pagina iscrizione NL";
            return View();
        }

        
     


    }
}