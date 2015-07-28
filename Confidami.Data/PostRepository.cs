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
                        timestamp = DateTime.Now,
                        userid = post.UserId
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
                        var identity = conn.Query<int>(QueryPostStore.InserPost + " " + QueryStore.LastInsertedId,
                            new
                            {
                                idCategory = post.Category.IdCategory,
                                title = post.Title,
                                body = post.Body,
                                slugUrl = post.SlugUrl,
                                timestamp = DateTime.Now,
                                 userid = post.UserId
                            }, transaction: transaction).SingleOrDefault();


                        foreach (var attach in post.Attachments)
                        {
                            conn.Execute(QueryPostStore.InsertPostAttachment,
                              new
                              {
                                  idPost = identity,
                                  fileName = attach.FileName,
                                  contentType = attach.ContentType,
                                  size=attach.Size,
                                  timestamp=DateTime.Now
                                  
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

        public IEnumerable<PostExtendedDb> GetAllPosts()
        {
            using (var conn = DbUtilities.Connection)
            {
                var lookup = new Dictionary<int, PostExtendedDb>();
                return conn.Query<PostExtendedDb>(QueryPostStore.PostByStatus,new {idStatus = PostStatus.Approved});
            }
        }


        public PostExtendedDbWithAttachments GetPostById(long idPost)
        {
            using (var conn = DbUtilities.Connection)
            {
                var lookup = new Dictionary<int, PostExtendedDbWithAttachments>();
                return conn.Query<PostExtendedDbWithAttachments, PostAttachments, PostExtendedDbWithAttachments>(QueryPostStore.PostById,
                    (a, s) =>
                    {
                        PostExtendedDbWithAttachments attach;
                        if (!lookup.TryGetValue(a.IdPost, out attach))
                        {
                            // If the lookup doesn't contain the current category, add
                            // it and store it in `category` as well.
                            lookup.Add(a.IdPost, a);

                            attach = a;
                        }
                        if (s != null)
                            attach.Attachments.Add(s);
                        return attach;
                    },
                    new {idPost = idPost}, splitOn: "IdPost").Distinct().SingleOrDefault();
            }
        }

        public IEnumerable<PostExtendedDb> GetPostsByCategory(int idCategory)
        {
            using (var conn = DbUtilities.Connection)
            {
                return conn.Query<PostExtendedDb>(QueryPostStore.PostByStatusAndCategory,new { idStatus = PostStatus.Approved, idCategory = idCategory });
            }
        }

        public IEnumerable<PostExtendedDb> GetPostByStatus(int status)
        {
            using (var conn = DbUtilities.Connection)
            {
                return conn.Query<PostExtendedDb>(QueryPostStore.PostByStatus,new {idStatus = status});
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

        public Category GetCategory(int idcategory)
        {
            using (var conn = DbUtilities.Connection)
            {
                return conn.Query<Category>(QueryPostStore.SingleCategory, new { idcategory }).SingleOrDefault();
            }
        }
    }

    public class FileRepository : BaseRepository
    {
        public int InsertTempUpload(string userId, string fileName, string contentType, int fileSize)
        {
            using (var conn = DbUtilities.Connection)
            {
                return conn.Query<int>(QueryFileStore.InsertUploadTemp + QueryStore.LastInsertedId,
                    new {userid = userId, filename = fileName, contentType = contentType, size = fileSize,@timestamp=DateTime.Now}).SingleOrDefault();
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

        public void DeleteTempAttachment(int id)
        {
            using (var conn = DbUtilities.Connection)
            {
                conn.Execute(QueryFileStore.DeleteTempAttachment,
                    new { id = id });
            }
        }

        public IEnumerable<TempAttachMent> GetTempAttachmentsByUserId(string userId)
        {
            using (var conn = DbUtilities.Connection)
            {
                return conn.Query<TempAttachMent>(QueryFileStore.SelectUploadTempByUserId,new {userid=userId});
            }
        }

        public TempAttachMent GetTempAttachmentById(int id)
        {
            using (var conn = DbUtilities.Connection)
            {
                return conn.Query<TempAttachMent>(QueryFileStore.TempAttachmentById, new { id = id}).SingleOrDefault();
            }
        }

    }
}
