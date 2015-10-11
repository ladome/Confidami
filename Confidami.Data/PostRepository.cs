using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Confidami.Common.Utility;
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
                return conn.Query<int>(QueryPostStore.InserPost + " " + QueryStore.LastInsertedId,
                    new
                    {
                        idCategory = post.Category.IdCategory,
                        title = post.Title,
                        body = post.Body,
                        slugUrl = post.SlugUrl,
                        timestamp = DateTime.Now,
                        userid = post.UserId,
                        editCode = post.EditCode
                    }).SingleOrDefault();
            }
        }

        public int UpdatePost(Post post)
        {
            using (var conn = DbUtilities.Connection)
            {
                conn.Open();
                using (var transaction = conn.BeginTransaction(IsolationLevel.ReadCommitted))
                {
                    int? res;
                    try
                    {
                        res = conn.Query<int>(QueryPostStore.UpdatePost,
                        new
                        {
                            idCategory = post.Category.IdCategory,
                            title = post.Title,
                            body = post.Body,
                            idStatus = post.Status,
                            timeStamp = post.TimeStamp,
                            timestampApprovation = post.TimeStampApprovation,
                            userId = post.UserId,
                            slugUrl = post.SlugUrl,
                            editCode = post.EditCode,
                            idPost = post.IdPost
                        }, transaction: transaction).SingleOrDefault();

                        res = res +
                              conn.Query<int>(QueryPostStore.UpdateLastUserEdit,
                                  new { lastedit = DateTime.Now, idPost = post.IdPost }, transaction: transaction).SingleOrDefault();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }

                    transaction.Commit();
                    return res.GetValueOrDefault();

                }
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
                                userid = post.UserId,
                                editCode = post.EditCode
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


        public void UpdatePostWithAttachment(Post post)
        {
            using (var conn = DbUtilities.Connection)
            {
                conn.Open();
                using (var transaction = conn.BeginTransaction(IsolationLevel.ReadCommitted))
                {
                    try
                    {

                       conn.Execute(QueryPostStore.UpdatePost,
                        new
                        {
                            idCategory = post.Category.IdCategory,
                            title = post.Title,
                            body = post.Body,
                            idStatus = post.Status,
                            timeStamp = post.TimeStamp,
                            timestampApprovation = post.TimeStampApprovation,
                            userId = post.UserId,
                            slugUrl = post.SlugUrl,
                            editCode = post.EditCode,
                            idPost = post.IdPost
                        },transaction: transaction);

                        conn.Execute(QueryPostStore.UpdateLastUserEdit,
                            new { lastedit = DateTime.Now, idPost = post.IdPost }, transaction: transaction);

                        foreach (var attach in post.Attachments)
                        {
                            conn.Execute(QueryPostStore.InsertPostAttachment,
                              new
                              {
                                  idPost = post.IdPost,
                                  fileName = attach.FileName,
                                  contentType = attach.ContentType,
                                  size = attach.Size,
                                  timestamp = DateTime.Now

                              }, transaction: transaction);
                        }

                        transaction.Commit();
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
                return conn.Query<PostExtendedDb>(QueryPostStore.PostByStatus,new {idStatus = PostStatus.Approved});
            }
        }

        public IEnumerable<PostExtendedDb> GetPostsPaged(int page,int blockSize, out int numberOfPost)
        {
            using (var conn = DbUtilities.Connection)
            {
                using (var multi = conn.QueryMultiple(QueryPostStore.PostByStatusPaginated + ' '+QueryPostStore.CountAllPostsByStatus,
                    new {idStatus = PostStatus.Approved, @page = (page-1)*blockSize,blocksize=blockSize}))
                {
                    var posts = multi.Read<PostExtendedDb>();
                    numberOfPost = multi.Read<int>().Single();
                    return posts;
                }
            }
        }

        public IEnumerable<PostExtendedDb> GetPostsByKeyPaged(int page, int blockSize, string key,int category, out int numberOfPost)
        {
            using (var conn = DbUtilities.Connection)
            {
                using (var multi = conn.QueryMultiple(QueryPostStore.PostByStatusSearchKeyPaginated + ' ' + QueryPostStore.CountAllPostsByStatusAndKey,
                    new { idStatus = PostStatus.Approved, @page = (page - 1) * blockSize, blocksize = blockSize, @key="%"+key+"%",category=category }))
                {
                    var posts = multi.Read<PostExtendedDb>();
                    numberOfPost = multi.Read<int>().Single();
                    return posts;
                }
            }
        }

        public PostExtendedDb GetPostLightById(long idPost)
        {
            using (var conn = DbUtilities.Connection)
            {
                return conn.Query<PostExtendedDb>(QueryPostStore.PostByIdLight, new { idPost = idPost }).SingleOrDefault();
            }
        }

        public PostExtendedDb GetPostLightByEditCode(string editCode)
        {
            using (var conn = DbUtilities.Connection)
            {
                return conn.Query<PostExtendedDb>(QueryPostStore.PostByEditCodeLight, new { editCode = editCode }).SingleOrDefault();
            }
        }

        public EditPostInfoDb GetEditInfoByIdPost(long idPost)
        {
            using (var conn = DbUtilities.Connection)
            {
                return conn.Query<EditPostInfoDb>(QueryPostStore.EditCodeInfo, new { idPost = idPost }).SingleOrDefault();
            }
        }

        public EditPostInfoDb GetEditInfoWhitEditCodeByIdPost(long idPost)
        {
            using (var conn = DbUtilities.Connection)
            {
                return conn.Query<EditPostInfoDb>(QueryPostStore.EditCodeInfoWithEditCode, new { idPost = idPost }).SingleOrDefault();
            }
        }

        public EditPostInfoDb GetEditInfoByEditCode(string editCode)
        {
            using (var conn = DbUtilities.Connection)
            {
                return conn.Query<EditPostInfoDb>(QueryPostStore.EditCodeInfoByEditCode, new { editCode = editCode }).SingleOrDefault();
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

        public PostExtendedDbWithAttachments GetPostByEditCode(string editCode)
        {
            using (var conn = DbUtilities.Connection)
            {
                var lookup = new Dictionary<int, PostExtendedDbWithAttachments>();
                return conn.Query<PostExtendedDbWithAttachments, PostAttachments, PostExtendedDbWithAttachments>(QueryPostStore.PostByEditCode,
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
                    new { editCode = editCode }, splitOn: "IdPost").Distinct().SingleOrDefault();
            }
        }

        public IEnumerable<PostExtendedDb> GetPostsByCategory(int idCategory)
        {
            using (var conn = DbUtilities.Connection)
            {
                return conn.Query<PostExtendedDb>(QueryPostStore.PostByStatusAndCategory,new { idStatus = PostStatus.Approved, idCategory = idCategory });
            }
        }

        public IEnumerable<PostExtendedDb> GetPostsByCategoryPaged(int page, int blockSize, int idCategory,out int numberOfPost)
        {
            using (var conn = DbUtilities.Connection)
            {
                using (
                    var multi =
                        conn.QueryMultiple(QueryPostStore.PostByStatusAndCategoryPaginated + ' ' +QueryPostStore.CountAllPostsByCategoryAndStatus,
                            new { idStatus = PostStatus.Approved, idCategory = idCategory, @page = (page - 1) * blockSize, blocksize = blockSize }))
                {
                    var posts = multi.Read<PostExtendedDb>();
                    numberOfPost = multi.Read<int>().Single();
                    return posts;
                }
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

        public int InserEditInfo(long idPostEdit, string email, string secretkey)
        {
            using (var conn = DbUtilities.Connection)
            {
                return conn.Execute(QueryPostStore.InserEditInfo,
                    new { idPost = idPostEdit, email = email, secretkey = secretkey });
            }
        }

        public int UpdateVote(long idPost, int vote)
        {
            using (var conn = DbUtilities.Connection)
            {
                return conn.Execute(QueryPostStore.UpdatePostVote,
                    new { idPost = idPost, vote = vote});
            }
        }
    }

    public class CommentRepository : BaseRepository
    {
        public IEnumerable<TopCommentator> GetTopCommentators(int number)
        {
            using (var conn = DbUtilities.Connection)
            {
                return conn.Query<TopCommentator>(QueryCommentStore.UserCommentsTopN, new { topN = number });
            }
        }

        public UserDb GetUserBySocialId(string socialId)
        {
            socialId.CannotBeNull("socialId");
            using (var conn = DbUtilities.Connection)
            {
                return conn.Query<UserDb>(QueryCommentStore.UserBySocialId, new { IdSocialUserId = socialId }).SingleOrDefault();
            }
        }

        public int InsertCommentAndUser(CommentDto comment)
        {
            using (var conn = DbUtilities.Connection)
            {
                conn.Open();
                using (var transaction = conn.BeginTransaction(IsolationLevel.ReadCommitted))
                {
                    try
                    {
                        var identity = conn.Query<int>(QueryCommentStore.InsertUser + " " + QueryStore.LastInsertedId,
                            new
                            {
                                idsocialuserid = comment.UserId,
                                name = comment.Name,
                                email = comment.UserMail
                            }, transaction: transaction).SingleOrDefault();
                        ;

                        identity = InsertComment(comment, identity, transaction);

                        transaction.Commit();

                        return identity;
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }


        public bool UpdateSocialUser(UserDb user)
        {
            user.CannotBeNull("user");
            using (var conn = DbUtilities.Connection)
            {
                return conn.Query<int>(QueryCommentStore.UpdateSocialUser, new { IdSocialUserId = user.IdSocialUserId, name=user.Name,email=user.Email }).SingleOrDefault() > 0;
            }
        }


        public int InsertComment(CommentDto comment, long idUser, IDbTransaction transaction = null)
        {
                comment.CannotBeNull("comment");
                var conn = transaction != null ? transaction.Connection :  DbUtilities.Connection;

                var res = conn.Query<int>(QueryCommentStore.InsertPostComment + " " + QueryStore.LastInsertedId,
                new
                {
                    idsocialcomment = comment.IdComment,
                    idpost = comment.IdPost,
                    iduser = idUser,
                    text = comment.Comment,
                    pageurl = comment.PageUrl,
                    timestamp = DateTime.Now
                }, transaction: transaction).SingleOrDefault();

            if (transaction != null) return res;

            conn.Close();
            conn.Dispose();
            return res;

        }
    }
}
