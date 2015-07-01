using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Confidami.Common;
using Confidami.Data;

namespace Confidami.BL
{
    public class PostManager
    {
        private readonly PostRepository _postRepository;
        private readonly CategoryRepository _categoryRepository;

        public PostManager() : this(new PostRepository(), new CategoryRepository()) { }


        public PostManager(PostRepository postRepository, CategoryRepository categoryRepository)
        {
            _postRepository = postRepository;
            _categoryRepository = categoryRepository;
        }

        public BaseResponse AddPost(Post post)
        {
            _postRepository.InserPost(post);
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
            return _postRepository.GetAllPosts();
        }

        public IEnumerable<Category> GetAllCategories()
        {
            return _categoryRepository.GetCategories();
        }


    }
}
