using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Confidami.Data.Entities;
using Confidami.Model;

namespace Confidami.BL.Mapper
{
    public class PostMapper
    {
        public static IEnumerable<PostLight> Map(IEnumerable<PostExtendedDb> post)
        {
            return post.Select(x => new PostLight()
            {
                Body = x.Body,
                Title = x.Title,
                Category = new Category() { Description = x.Description, IdCategory = x.IdCategory,Slug = x.CatSlug},
                SlugUrl = x.SlugUrl,
                IdPost = x.IdPost,
                Status = x.Status,
                StatusDescription = x.StatusDescription,
                TimeStamp = x.TimeStamp,
                TimeStampApprovation = x.TimeStampApprovation,
                UserId = x.UserId,
                HasAttachments = x.NumberOfAttachment > 0
            }).ToList();
        }

        public static Post Map(PostExtendedDbWithAttachments post)
        {
            return new Post()
            {
                Body = post.Body,
                Title = post.Title,
                Category = new Category() {Description = post.Description, IdCategory = post.IdCategory,Slug = post.CatSlug},
                SlugUrl = post.SlugUrl,
                IdPost = post.IdPost,
                Status = post.Status,
                StatusDescription = post.StatusDescription,
                TimeStamp = post.TimeStamp,
                TimeStampApprovation = post.TimeStampApprovation,
                Attachments = post.Attachments,
                UserId = post.UserId,
            };
        }


    }
}
