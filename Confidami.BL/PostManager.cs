using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Confidami.BL.Mapper;
using Confidami.Data;
using Confidami.Model;

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
            var res = _postRepository.InserPost(post);
            return new BaseResponse() {Success = res, Message = res ? "Post inserito" : "Post non inserito"};
        }

        public BaseResponse ApprovePost(long idPost)
        {
            var res = _postRepository.ChangePostStatus(idPost, PostStatus.Approved);
            return new BaseResponse()
            {
                Success = res,
                Message = res ? "Post status cambiato" : "Post status non cambiato"
            };
        }

        public BaseResponse RejectPost(long idPost)
        {
            var res = _postRepository.ChangePostStatus(idPost, PostStatus.Rejected);
            return new BaseResponse()
            {
                Success = res,
                Message = res ? "Post status cambiato" : "Post status non cambiato"
            };
        }

        public BaseResponse OnApprovationPost(long idPost)
        {
            var res = _postRepository.ChangePostStatus(idPost, PostStatus.OnApprovation);
            return new BaseResponse()
            {
                Success = res,
                Message = res ? "Post status cambiato" : "Post status non cambiato"
            };
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

        public IEnumerable<Post> GetPostByStatus(PostStatus status)
        {
           return PostMapper.Map( _postRepository.GetPostByStatus((int)status));
        }

        public IEnumerable<Post> GetAllPost()
        {
            return PostMapper.Map(_postRepository.GetAllPosts());
        }

        public IEnumerable<Category> GetAllCategories()
        {
            return _categoryRepository.GetCategories();
        }


    }

}
