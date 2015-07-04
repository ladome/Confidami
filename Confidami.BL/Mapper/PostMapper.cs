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
        public static IEnumerable<Post> Map(IEnumerable<PostDb> post)
        {
            return post.Select(x => new Post()
            {
                Body = x.Body,
                Title = x.Title,
                Category = new Category() { Description = x.Description, IdCategory = x.IdCategory },
                SlugUrl = x.SlugUrl,
                IdPost = x.IdPost,
                Status = x.Status,
                StatusDescription = x.StatusDescription,
                TimeStamp = x.TimeStamp,
                TimeStampApprovation = x.TimeStampApprovation
            }).ToList();
        }


    }
}
