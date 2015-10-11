using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Confidami.BL;
using Confidami.Common;
using Confidami.Model;
using Newtonsoft.Json;

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

        [Route("post/vote")]
        public JsonResult Vote(long idPost, int vote)
        {
            var res = Request.Cookies.Get("lastvote");
            List<Tuple<long, long,int>> list;
            if (res == null)
            {
                PostManager.UpdateVote(idPost, vote);
                list = new List<Tuple<long, long,int>>() { new Tuple<long, long,int>(idPost, DateTime.Now.Ticks,vote) };
            }
            else
            {
                list = GetLastVoteInfo();

                var found = list.Find(x => x.Item1 == idPost);
                if (found != null)
                {
                    var datetimefromticks = new DateTime(Convert.ToInt64(found.Item2));

                    if ((DateTime.Now - datetimefromticks).TotalDays <= 3)
                    {
                        return CreateJsonResponse(new BaseResponseExtended() { Success = false, Message = ValidationManager.ErrorCodes[ErrorCode.TooManyVotes], ErrorCode = (int)ErrorCode.TooManyVotes }, HttpStatusCode.BadRequest);
                    }

                    list.Remove(list.FirstOrDefault(x => x.Item1 == found.Item1));
                }

                var result = PostManager.UpdateVote(idPost, vote);
                if (!result.Success)
                    return CreateJsonResponse(new BaseResponseExtended() { Success = false, Message = ValidationManager.ErrorCodes[ErrorCode.Generic], ErrorCode = (int)ErrorCode.Generic }, HttpStatusCode.InternalServerError);
                list.Add(Tuple.Create(idPost, DateTime.Now.Ticks,vote));
            }

            var newValue = JsonConvert.SerializeObject(list);
            Response.SetCookie(new HttpCookie("lastvote", newValue) { Expires = DateTime.Now.AddDays(3) });
            return CreateJsonResponse("OK");
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