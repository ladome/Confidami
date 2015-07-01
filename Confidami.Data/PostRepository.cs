using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Confidami.Common;
using Dapper;

namespace Confidami.Data
{
    public class PostRepository
    {
        public bool InserPost(Post post)
        {
            using (var conn = DbUtilities.Connection)
            {
                return conn.Execute(QueryStore.InserPost,
                    new
                    {
                        idCategory = post.Category.IdCategory,
                        title = post.Title,
                        body = post.Body,
                        slugUrl = post.SlugUrl
                    }) > 0;
            }
        }

        public IEnumerable<Post> GetAllPosts()
        {
            using (var conn = DbUtilities.Connection)
            {
                return conn.Query<Post>(QueryStore.AllPosts);
            }
        }
    }

    public class CategoryRepository
    {
        public IEnumerable<Category> GetCategories()
        {
            using (var conn = DbUtilities.Connection)
            {
                return conn.Query<Category>(QueryStore.AllCategory);
            }
        }
    }
}
