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
        public DateTime TimeStampApprovation { get; set; }
        public virtual bool HasAttachments { get; set; }
        public string EditCode { get; set; }

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

}