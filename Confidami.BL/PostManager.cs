using System;
using System.Collections.Generic;
using System.IO;
using Confidami.BL.Mapper;
using Confidami.Common;
using Confidami.Common.Utility;
using Confidami.Data;
using Confidami.Model;

namespace Confidami.BL
{
    public class PostManager
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
            int res = 0;

            if (!post.HasAttachments)
                res = _postRepository.InserPost(post);
            else
            {
                res = _postRepository.InserPostWithAttachment(post);
                if(res > 0) post.Attachments.ForEach(x=> _fileManager.UploadFileInDefaultFolder(x.InputStream,x.FileName,x.ContentType,x.ContentLenght));
            }

            bool success = res > 0;
            return new BaseResponse { Success = success, Message = success ? "Post inserito" : "Post non inserito" };
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
            return PostMapper.Map(_postRepository.GetPostByStatus((int) status));
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


    public class FileManager
    {
        public void UploadFileInDefaultFolder(Stream file, string fileName, string contentType, int contentLenght)
        {
            file.CannotBeNull("stream");
            fileName.CannotBeNull("fileName");
            string defaultFolfer = Config.UploadsFolder;
            var path = Path.IsPathRooted(defaultFolfer)? defaultFolfer: Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, defaultFolfer));

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            var buffer = new byte[contentLenght];
            file.Read(buffer,0,contentLenght);
            using (var fileStream = new FileStream(Path.Combine(path,fileName),FileMode.Create))
            {
              fileStream.Write(buffer,0,contentLenght);
            }
            }
    }
}
