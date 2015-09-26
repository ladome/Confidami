using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Confidami.BL.Mapper;
using Confidami.Common;
using Confidami.Common.Utility;
using Confidami.Data;
using Confidami.Data.Entities;
using Confidami.Model;

namespace Confidami.BL
{
    public class PostManager : BaseManager
    {
        private readonly PostRepository _postRepository;
        private readonly CategoryRepository _categoryRepository;
        private readonly FileManager _fileManager;

        public PostManager() : this(new PostRepository(), new CategoryRepository(),new FileManager())
        {
        }


        public PostManager(PostRepository postRepository, CategoryRepository categoryRepository, FileManager fileManager)
        {
            _postRepository = postRepository;
            _categoryRepository = categoryRepository;
            _fileManager = fileManager;
        }

        public BaseResponse AddPost(Post post)
        {
            post.CannotBeNull("post");

            BuildAttachAttachments(post);
            int res;

            var editCode = GetRandomString();
            var founded = false;

            if (_postRepository.GetPostByEditCode(editCode) != null)
            {
                while (!founded)
                {
                    editCode = GetRandomString();
                    if (_postRepository.GetPostByEditCode(editCode) == null)
                        founded = true;
                }
            }

            post.EditCode = editCode;
            post.SlugUrl = Slug.CreateSlug(true, post.Title);


            if (!post.HasAttachments)
                res = _postRepository.InserPost(post);
            else
            {
                res = _postRepository.InserPostWithAttachment(post);
                if (res > 0) _fileManager.MoveTempInFinalFolder(post.UserId,res.ToString());
            }

            bool success = res > 0;
            return new BaseResponse { Success = success, Message = res.ToString() };
        }

        public void UpdatePost(Post post)
        {
            post.CannotBeNull("post");
            BuildAttachAttachments(post);
            post.SlugUrl = Slug.CreateSlug(true, post.Title);

            if (!post.HasAttachments)
                _postRepository.UpdatePost(post);
            else
            {
                _postRepository.UpdatePostWithAttachment(post);
                _fileManager.MoveTempInFinalFolder(post.UserId, post.IdPost.ToString());
            }
        }


        public BaseResponse InserEditInfo(PostEdit model)
        {
            model.CannotBeNull("editPost");


            if (_postRepository.GetEditInfoByIdPost(model.IdPost) != null)
                return new BaseResponse() { Message = "Info già caricate", Success = false };

            var post = _postRepository.GetPostLightById(model.IdPost);

            if (post.UserId != model.UserId)
                return new BaseResponse() {Message = "Operazione non permessa", Success = false};

            var res = _postRepository.InserEditInfo(model.IdPost, model.Email, model.Password);

            var resb = res > 0;
            return new BaseResponse
            {
                Success = resb,
                Message = resb ? "Edit info salvate" : "Edit info non salvate"
            };
        }
        private void BuildAttachAttachments(Post post)
        {
            post.CannotBeNull("post");
            var res =_fileManager.GetTempAttachMentsByUserId(post.UserId);
            post.Attachments =
                res.Select(x => new PostAttachments()
                {
                    FileName = x.FileName,
                    ContentType = x.ContentType,
                    Size = x.Size,
                }).ToList();

        }

        public BaseResponse ApprovePost(long idPost)
        {
            bool res = _postRepository.ChangePostStatus(idPost, PostStatus.Approved);
            return new BaseResponse
            {
                Success = res,
                Message = res ? "Post status cambiato" : "Post status non cambiato"
            };
        }

        public BaseResponse RejectPost(long idPost)
        {
            bool res = _postRepository.ChangePostStatus(idPost, PostStatus.Rejected);
            return new BaseResponse
            {
                Success = res,
                Message = res ? "Post status cambiato" : "Post status non cambiato"
            };
        }

        public BaseResponse OnApprovationPost(long idPost)
        {
            bool res = _postRepository.ChangePostStatus(idPost, PostStatus.OnApprovation);
            return new BaseResponse
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
            var res = _postRepository.GetPostById(idpost);
            if (res == null)
                return null;
            return PostMapper.Map(res);
        }


        public IEnumerable<Post> GetPostsByDescription(string description)
        {
            return new List<Post>();
        }

        public IEnumerable<PostLight> GetPostByCategory(int idCategory)
        {
            return PostMapper.Map(_postRepository.GetPostsByCategory((int)idCategory));
        }

        public IEnumerable<PostLight> GetPostByCategoryPaged(int page,int idCategory, out int count)
        {
            return PostMapper.Map(_postRepository.GetPostsByCategoryPaged(page,Config.NumberOfPostPerPage,(int)idCategory,out count));
        }

        public PostLight GetpostLight(long idPost)
        {
            var res = _postRepository.GetPostLightById(idPost);
            if (res == null)
                return null;
            return PostMapper.Map(res);
        }

        public PostLight GetpostLight(string editCode)
        {
            var res = _postRepository.GetPostLightByEditCode(editCode);
            if (res == null || res.Status != PostStatus.Approved)
                return null;
            return PostMapper.Map(res);
        }

        public CheckEditCodeResponse CheckEditCode(string editCode, string password)
        {
            editCode.CannotBeNull("editCode");
            password.CannotBeNull("password");

            var res = _postRepository.GetEditInfoByEditCode(editCode);
            if (res == null)
            {
                return new CheckEditCodeResponse()
                {
                    Success = false,
                    Message = "Nessuna denuncia associata all'edit code fornito"
                };
            }

            if (res.SecretKey != password)
            {
                return new CheckEditCodeResponse()
                {
                    Success = false,
                    Message = "Il codice segreto non corrisponde con l'editcode fornito"
                };
            }

            return new CheckEditCodeResponse() {Success = true, PostLigh = GetpostLight(editCode)};
        }

        public IEnumerable<PostLight> GetPostByStatus(PostStatus status)
        {
            return PostMapper.Map(_postRepository.GetPostByStatus((int) status));
        }


        public IEnumerable<PostLight> GetAllPost()
        {
            return PostMapper.Map(_postRepository.GetAllPosts());
        }

        public IEnumerable<PostLight> GetPostsPaged(int page, out int count)
        {
            return PostMapper.Map(_postRepository.GetPostsPaged(page,Config.NumberOfPostPerPage,out count));

        }

        public IEnumerable<PostLight> SearchPosts(int page,string key, out int count)
        {
            key.CannotBeNull("key");
            return PostMapper.Map(_postRepository.GetPostsByKeyPaged(page, Config.NumberOfPostPerPage,key, out count));

        }

        public IEnumerable<Category> GetAllCategories()
        {
            return _categoryRepository.GetCategories();
        }

        public Category GetCategory(int idCategory)
        {
            return _categoryRepository.GetCategory(idCategory);
        }
    }


    public class CommentManager : BaseManager
    {
        private readonly CommentRepository _commentRepository;

        public CommentManager() : this(new CommentRepository())
        {
        }


        public CommentManager(CommentRepository postRepository)
        {
            _commentRepository = postRepository;
        }


        public List<TopCommentator> GetTopCommentator()
        {
           return _commentRepository.GetTopCommentators(Config.NumberOfTopCommentator).ToList();
        }

        public BaseResponse InsertComment(CommentDto comment)
        {
            comment.CannotBeNull("comment");
            var user = _commentRepository.GetUserBySocialId(comment.UserId);
            int res;
            if (user == null)
            {
               res =  _commentRepository.InsertCommentAndUser(comment);
            }
            else
            {
                res = _commentRepository.InsertComment(comment, user.IdUser);
                if (string.IsNullOrEmpty(user.Email) && comment.UserMail != null)
                {
                    user.Email = comment.UserMail;
                    _commentRepository.UpdateSocialUser(user);
                }
            }

            return new BaseResponse() { Message = res > 0 ? res.ToString() : "ko", Success = res > 0 };


        }

    }
}
