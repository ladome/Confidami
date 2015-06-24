using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Confidami.Common;
using Confidami.Data;

namespace Confidami.BL
{
    public class PostManager
    {
        private readonly PostRepository _repo;

        public PostManager() : this(new PostRepository()) { }


        public PostManager(PostRepository repo)
        {
            _repo = repo;
        }

        public BaseResponse AddPost(Post post)
        {
            _repo.InserPost(post);
            return new BaseResponse();
        }

        public BaseResponse DeletePost(long idPost)
        {
            return new BaseResponse();            
        }

        public Post GetPost(long idpost)
        {
            return new Post();
        }

        public Post GetPost(string slug)
        {
            return new Post();
        }

        public IEnumerable<Post> GetPostsByDescription(string description)
        {
            return new List<Post>();
        }

        public IEnumerable<Post> GetPostByCategory(int idCategory)
        {
            return new List<Post>();            
        }

        public IEnumerable<Post> GetAllPost()
        {
            return _repo.GetAllPosts();
        }


    }
}
