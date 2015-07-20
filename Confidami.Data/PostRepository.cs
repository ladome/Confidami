using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Confidami.Data.Entities;
using Confidami.Model;
using Dapper;

namespace Confidami.Data
{
    public class BaseRepository
    {
        
    }

    public class PostRepository : BaseRepository
    {
        public int InserPost(Post post)
        {
            using (var conn = DbUtilities.Connection)
            {
                return conn.Execute(QueryPostStore.InserPost,
                    new
                    {
                        idCategory = post.Category.IdCategory,
                        title = post.Title,
                        body = post.Body,
                        slugUrl = post.SlugUrl,
                        timestamp = DateTime.Now
                    });
            }
        }

        public int InserPostWithAttachment(Post post)
        {
            using (var conn = DbUtilities.Connection)
            {
                conn.Open();
                using (var transaction = conn.BeginTransaction(IsolationLevel.ReadCommitted))
                {
                    try
                    {
                        var identity = conn.Query<int>(QueryPostStore.InserPost + " " + QueryPostStore.LastInsertedId,
                            new
                            {
                                idCategory = post.Category.IdCategory,
                                title = post.Title,
                                body = post.Body,
                                slugUrl = post.SlugUrl,
                                timestamp = DateTime.Now
                            }, transaction: transaction).SingleOrDefault();


                        foreach (var attach in post.Attachments)
                        {
                            conn.Execute(QueryPostStore.InsertPostAttachment,
                              new
                              {
                                  idPost = identity,
                                  fileName = attach.FileName,
                                  contentType = attach.ContentType,
                                  size=attach.Size
                                  
                              },transaction:transaction);
                        }

                        transaction.Commit();

                        return identity;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw;
                    }

                }
            }

        }

        public IEnumerable<PostDb> GetAllPosts()
        {
            using (var conn = DbUtilities.Connection)
            {
                return conn.Query<PostDb>(QueryPostStore.PostByStatus, new { idStatus = PostStatus.Approved });
            }
        }

        public IEnumerable<PostDb> GetPostByStatus(int status)
        {
            using (var conn = DbUtilities.Connection)
            {
                return conn.Query<PostDb>(QueryPostStore.PostByStatus, new { idStatus = status});
            }
        }

        public bool ChangePostStatus(long idPost, PostStatus status)
        {
            using (var conn = DbUtilities.Connection)
            {
                return conn.Execute(QueryPostStore.SetPostStatus,
                    new
                    {
                        idPost = idPost,
                        idStatus = (int) status,
                        timeStamp = DateTime.Now
                    }) > 0;
            }
        }
    }

    public class CategoryRepository : BaseRepository
    {
        public IEnumerable<Category> GetCategories()
        {
            using (var conn = DbUtilities.Connection)
            {
                return conn.Query<Category>(QueryPostStore.AllCategory);
            }
        }
    }

    public class FileRepository : BaseRepository
    {
        public int InsertTempUpload(string userId, string fileName, string contentType, int fileSize)
        {
            using (var conn = DbUtilities.Connection)
            {
                return conn.Execute(QueryFileStore.InsertUploadTemp,
                    new {userid = userId, filename = fileName, contentType = contentType, size = fileSize});
            }
        }

        public void DeleteInTempFile(string userId)
        {
            using (var conn = DbUtilities.Connection)
            {
                conn.Execute(QueryFileStore.DeleteInTempFolder,
                    new { userid = userId});
            }
        }

        public IEnumerable<TempAttachMent> GetTempAttachmentsByUserId(string userId)
        {
            using (var conn = DbUtilities.Connection)
            {
                return conn.Query<TempAttachMent>(QueryFileStore.SelectUploadTempByUserId,new {userid=userId});
            }
        }

    }
}
