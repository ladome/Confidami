﻿using System;
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
    public class SegnalazioniController : BaseController
    {
        
        private readonly PostManager _postManager;

        public SegnalazioniController()
        {
            _postManager = new PostManager();
        }

        [Route("segnalazioni")]
        public ActionResult Index()
        {
            ViewBag.Title = "Segnalazioni  - Tutte";
            return View();
        }

        [Route("segnalazioni/categoria/")]
        public ActionResult Category()
        {
            ViewBag.Title = "Segnalazioni per categoria X";
            return View("Index");
        }


        [Route("segnalazioni/inserisci")]
        public ActionResult Insert()
        {
            TempData["from"] = Request.Url;
            ViewBag.CurrentUser = CurrentUserId;
            return View(FillPostViewMoldel());
            
            //return View("Inserisci");
        }

        public ActionResult AddPost(PostViewModel postVm)
        {
            if (!ModelState.IsValid)
                return View("Index", FillPostViewMoldel());

            var post = new Post
            {
                Body = postVm.Body,
                Category = new Category { IdCategory = postVm.IdCategory },
                Title = postVm.Title,
                SlugUrl = "",
                UserId = CurrentUserId
            };

            PostManager.AddPost(post);

            //if (!HandleFileUpload(Request.Files))
            //{
            //    ModelState.AddModelError("files", "File not loaded correctly");
            //    return View("Index", FillPostViewMoldel());
            //}
            return RedirectToAction("Index");
        }


        public ActionResult Upload(HttpPostedFileWrapper file)
        {
            file.CannotBeNull("file");
            FileManager.UploadFileInTempFolder(file.InputStream, file.FileName, file.ContentType, file.ContentLength, CurrentUserId);
            return Json(false);
        }

        public JsonResult GetTempAttachMents()
        {
            return Json(FileManager.GetTempAttachMentsByUserId(CurrentUserId).Select(x => new TempAttachMentViewModel() { Name = x.FileName, Size = x.Size }), JsonRequestBehavior.AllowGet);
        }


        private PostViewModel FillPostViewMoldel()
        {
            var res = _postManager.GetAllPost();
            var posts = res.Select(x => new PostViewModelBase() { Body = x.Body, Title = x.Title, CategoryPost = x.Category.Description, IdPost = x.IdPost }).ToList();
            var categories = _postManager.GetAllCategories();
            return new PostViewModel() { Posts = posts, Categories = categories, IsAdmin = IsAdmin, ReturnUrl = Request.RawUrl };
        }


    }
}