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
    public class PostRepository
    {
        public int InserPost(Post post)
        {
            using (var conn = DbUtilities.Connection)
            {
                return conn.Execute(QueryStore.InserPost,
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
                        var identity = conn.Query<int>(QueryStore.InserPost + " " + QueryStore.LastInsertedId,
                            new
                            {
                                idCategory = post.Category.IdCategory,
                                title = post.Title,
                                body = post.Body,
                                slugUrl = post.SlugUrl,
                                timestamp = DateTime.Now
                            }, transaction: transaction).SingleOrDefault();


                        foreach (var attach in post.Attachments.Select(x => x.FileName))
                        {
                            conn.Execute(QueryStore.InsertPostAttachment,
                              new
                              {
                                  idPost = identity,
                                  fileName = attach
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
                return conn.Query<PostDb>(QueryStore.PostByStatus, new { idStatus = PostStatus.Approved });
            }
        }

        public IEnumerable<PostDb> GetPostByStatus(int status)
        {
            using (var conn = DbUtilities.Connection)
            {
                return conn.Query<PostDb>(QueryStore.PostByStatus, new { idStatus = status});
            }
        }

        public bool ChangePostStatus(long idPost, PostStatus status)
        {
            using (var conn = DbUtilities.Connection)
            {
                return conn.Execute(QueryStore.SetPostStatus,
                    new
                    {
                        idPost = idPost,
                        idStatus = (int) status,
                        timeStamp = DateTime.Now
                    }) > 0;
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
