using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Confidami.Model
{
    public class Post
    {
        public Post()
        {
            Attachments = new List<PostAttachments>();
        }

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

        public List<PostAttachments> Attachments { get; set; }
        public bool HasAttachments { get { return Attachments.Any(); } }
    }

    public class PostAttachments
    {
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public long Size { get; set; }
    }

}