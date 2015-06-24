using System;

namespace Confidami.Common
{
    public class Post
    {
        
        public int IdPost { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public Category Category { get; set; }
        public string SlugUrl { get; set; }
        public PostStatus Status { get; set; }
        public string StatusDescriprion { get; set; }
        public DateTime TimeStamp { get; set; }
        public DateTime TimeStampApprovation { get; set; }
    }

    public enum PostStatus
    {
        OnApprovation = 0,
        Approved = 1,
        Rejected = 2
    }

}