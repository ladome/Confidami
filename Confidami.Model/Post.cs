using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Confidami.Common;
using Confidami.Common.Utility;

namespace Confidami.Model
{
    public class PostLight
    {
        public string UserId { get; set; }
        public int IdPost { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public Category Category { get; set; }
        public string SlugUrl { get; set; }
        public PostStatus Status { get; set; }
        public string StatusDescription { get; set; }
        public DateTime TimeStamp { get; set; }
        public DateTime? TimeStampApprovation { get; set; }
        public virtual bool HasAttachments { get; set; }
        public string EditCode { get; set; }
        public int Votes { get; set; }

    }
    public class Post : PostLight
    {
        public Post()
        {
            Attachments = new List<PostAttachments>();
        }

        public List<PostAttachments> Attachments { get; set; }
        public new bool HasAttachments { get { return Attachments.Any(); } }
    }

    public class PostAttachments
    {
        public int Id { get; set; }
        public long IdPost { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public long Size { get; set; }
    }

    public class PostEdit
    {
        public long IdPost { get; set; }
        public DateTime? LastUserEdit { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserId { get; set; }
    }

    public class UserComments
    {
        public UserComments()
        {
            Comments = new List<PostComment>();
        }

        public long IdUser { get; set; }
        public string IdSocialUserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public IEnumerable<PostComment> Comments { get; set; } 
    }

    public class PostComment
    {
        public long IdPostComment { get; set; }
        public string IdSocialComment { get; set; }
        public long IdPost { get; set; }
        public long IdUser { get; set; }
        public string Text { get; set; }
        public string PageUrl { get; set; }
    }

    public class TopCommentator
    {
        public long IdUser { get; set; }
        public string IdSocialUserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public long NumberOfComment { get; set; }
    }


    public class CommentDto
    {
        public string IdComment { get; set; }
        public string UserId { get; set; }
        public string UserMail { get; set; }
        public string Name { get; set; }
        public string Comment { get; set; }
        public string PageUrl { get; set; }
        public string IdPost { get; set; }
    }



}