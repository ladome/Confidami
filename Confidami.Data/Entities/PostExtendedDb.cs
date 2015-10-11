using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Confidami.Model;

namespace Confidami.Data.Entities
{
    public class PostDb
    {
        public int IdPost { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public int IdCategory { get; set; }
        public string SlugUrl { get; set; }
        public PostStatus Status { get; set; }
        public DateTime TimeStamp { get; set; }
        public DateTime? TimeStampApprovation { get; set; }
    }

    public class PostExtendedDb : PostDb
    {
        public string UserId { get; set; }
        public string CatSlug { get; set; }
        public string Description { get; set; }
        public string StatusDescription { get; set; }
        public int NumberOfAttachment { get; set; }
        public string EditCode { get; set; }
        public int Votes { get; set; }
    }

    public class PostExtendedDbWithAttachments : PostExtendedDb
    {
        public PostExtendedDbWithAttachments()
        {
            Attachments = new List<PostAttachments>();
        }
        public List<PostAttachments> Attachments { get; set; }
    }

    public class EditPostInfoDb
    {
        public long IdPost { get; set; }
        public DateTime? LastUserEdit { get; set; }
        public string Email { get; set; }
        public string SecretKey { get; set; }
    }

    public class EditPostInfoWithCodeDb : EditPostInfoDb
    {
        public string EditCode { get; set; }
    }

    public class UserDb
    {
        public long IdUser { get; set; }
        public string IdSocialUserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }

}
