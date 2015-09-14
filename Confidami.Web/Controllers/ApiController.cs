using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Confidami.BL;
using Confidami.Model;

namespace Confidami.Web.Controllers
{
    [RoutePrefix("api")]
    public class ApiController : BaseController
    {
        [Route("post/comment")]
        [HttpPost]
        public JsonResult Comment(CommentViewModel model)
        {
            var res = CommentManager.InsertComment(new CommentDto()
            {
                IdComment = model.IdComment,
                Comment = model.Comment,
                IdPost = model.IdPost,
                Name = model.Name,
                PageUrl = model.PageUrl,
                UserId = model.UserId,
                UserMail = model.UserMail
            });

            return res.Success ? CreateJsonResponse("Ok") : CreateJsonResponse("Ko", HttpStatusCode.BadRequest);
        }
    }


    public class CommentViewModel
    {
        [Required]
        public string IdComment { get; set; }
        [Required]
        public string UserId { get; set; }
        public string UserMail { get; set; }
        public string Name { get; set; }
        [Required]
        public string Comment { get; set; }
        [Required]
        public string PageUrl { get; set; }
        [Required]
        public string IdPost { get; set; }
    }
}