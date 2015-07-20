using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
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
            post.CannotBeNull("post");

            BuildAttachAttachments(post);
            int res;

            if (!post.HasAttachments)
                res = _postRepository.InserPost(post);
            else
            {
                res = _postRepository.InserPostWithAttachment(post);
                if (res > 0) _fileManager.MoveTempInFinalFolder(post.UserId,res.ToString());
            }

            bool success = res > 0;
            return new BaseResponse { Success = success, Message = success ? "Post inserito" : "Post non inserito" };
        }

        private void BuildAttachAttachments(Post post)
        {
            post.CannotBeNull("post");
            var res =_fileManager.GetTempAttachMentsByUserId(post.UserId);
            post.Attachments =
                res.Select(x => new PostAttachments() {FileName = x.FileName, ContentType = x.ContentType, Size = x.Size}).ToList();

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
        private readonly FileRepository _fileRepository;

        public FileManager()
            : this(new FileRepository())
        {
        }

        private FileManager(FileRepository fileRepository)
        {
            _fileRepository = fileRepository;
        }


        public void UploadFileInTempFolder(Stream file, string fileName, string contentType, int contentLenght,string parentFolder = null)
        {
            parentFolder.CannotBeNull("userId");
            fileName.CannotBeNull("filename");
            file.CannotBeNull("file");

            _fileRepository.InsertTempUpload(parentFolder, fileName,contentType,contentLenght);
            UploadFileInFolder(file, fileName, contentType, contentLenght,true,parentFolder);
        }

        public void MoveTempInFinalFolder(string source,string destination)
        {
            source.CannotBeNull("source");
            source.CannotBeNull("destination");

            var pathSource = Path.Combine(FileSystem.GetTempUploadFolder(), source);
            var pathDestination = Path.Combine(FileSystem.GetFullUploadFolder(), destination);
            FileSystem.MoveFilesFolder(pathSource,pathDestination,true);

            _fileRepository.DeleteInTempFile(source);
        }



        public void UploadFileInFolder(Stream file, string fileName, string contentType, int contentLenght,bool isTmpFolder = false,string parentFolder =null)
        {
            file.CannotBeNull("file");
            fileName.CannotBeNull("fileName");

            string defaultFolfer = isTmpFolder ? Config.UploadsTempFolder : Config.UploadsFolder;
            var path = Path.IsPathRooted(defaultFolfer)? defaultFolfer: Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, defaultFolfer));

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            var dir = new DirectoryInfo(path);

            if (!string.IsNullOrEmpty(parentFolder))
            {
                path= dir.CreateSubdirectory(parentFolder).FullName;
            }

            var buffer = new byte[contentLenght];
            file.Read(buffer,0,contentLenght);
            using (var fileStream = new FileStream(Path.Combine(path,fileName),FileMode.Create))
            {
              fileStream.Write(buffer,0,contentLenght);
            }
         }

        public List<TempAttachMent> GetTempAttachMentsByUserId(string userId)
        {
            return _fileRepository.GetTempAttachmentsByUserId(userId).ToList();
        }
    }
}
