using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Confidami.Data
{
    public class QueryStore
    {
        public const string LastInsertedId =
            "SELECT cast(SCOPE_IDENTITY() as int) as id;";
    }

    public class QueryPostStore : QueryStore
    {
        public const string InserPost = 
            @"INSERT INTO [dbo].[tblPosts]
           ([IdCategory]
           ,[Title]
           ,[Body]
           ,[SlugUrl]
           ,[Timestamp]
           ,[UserId]
           ,[EditCode])
            VALUES (@idCategory,@title,@body,@slugUrl,@timeStamp,@userid,@editcode);";


        public const string AllPosts =
           @"SELECT [IdPost]
          ,p.[IdCategory]
          ,[Title]
          ,[Body]
          ,[SlugUrl]
          ,p.[IdStatus]
          ,st.[Description] as StatusDescription
          ,cat.[Description]
          ,[Deleted]
          ,[Timestamp]
          ,[TimestampApprovation]
           FROM [tblPosts]  p
           inner join tblpoststatus st on p.idStatus = st.idstatus
           inner join tblCategory cat on cat.idcategory = p.idcategory
           where deleted = 0 order by Timestamp desc";

        public const string AllCategory =
            @"SELECT [IdCategory],[Description],[Slug]
            FROM [tblCategory]";

        public const string SingleCategory =
            @"SELECT [IdCategory],[Description],[Slug]
                    FROM [tblCategory] where idcategory=@idcategory";

        public const string InserEditInfo =
            @"INSERT INTO [tblPostsEdit]
           ([IdPostEdit]
           ,[Email]
           ,[SecretKey])
            VALUES
           (@idPost,@email,@secretkey);";

        public const string EditCodeInfo =
            @"SELECT [IdPostEdit] as idPost
              ,[LastUserEdit]
              ,[Email]
              ,[SecretKey]
              FROM [tblPostsEdit] where idPostEdit = @idPost";

        public const string EditCodeInfoWithEditCode =
            @"SELECT [IdPostEdit]
                    ,p.idPost
                    ,[LastUserEdit]
                    ,[Email]
                    ,[SecretKey]
	                ,[EditCode]
                    FROM [tblPostsEdit] pe inner join tblPosts p on pe.idpost = p.idpost where idPostEdit = @idPost";

                public const string EditCodeInfoByEditCode =
            @"SELECT [IdPostEdit]
                    ,p.IdPost
                    ,[LastUserEdit]
                    ,[Email]
                    ,[SecretKey]
	                ,[EditCode]
                    FROM [tblPostsEdit] pe inner join tblPosts p on pe.IdPostEdit = p.idpost where editCode = @editCode";

        public const string InsertPostAttachment =
            @"INSERT INTO [tblPostsAttachments]
           ([IdPost]
           ,[FileName]
           ,[ContentType]
           ,[Size]
           ,[timestamp])
            VALUES (@idPost,@fileName,@contentType,@size,@timestamp)";

        public const string PostByStatus =
           @"SELECT p.[IdPost]
          ,p.[IdCategory]
          ,p.[userid]
          ,[Title]
          ,[Body]
          ,[SlugUrl]
          ,p.[IdStatus]
          ,st.[Description] as StatusDescription
          ,cat.[Description]
          ,cat.Slug as CatSlug
          ,p.[Deleted]
          ,p.[Timestamp]
          ,p.[TimestampApprovation]
          ,(select count(idpost) from tblPostsAttachments pa where pa.idpost = p.IdPost and pa.deleted=0) as NumberOfAttachment
           FROM [tblPosts]  p
           inner join tblpoststatus st on p.idStatus = st.idstatus
           inner join tblCategory cat on cat.idcategory = p.idcategory
           where p.deleted = 0 and p.IdStatus=@idStatus order by Timestamp desc;";

        public const string PostById =
                @"SELECT p.[IdPost]
                  ,p.[IdCategory]
                  ,p.[userid]
                  ,[Title]
                  ,[Body]
                  ,[SlugUrl]
                  ,p.[IdStatus]
                  ,st.[Description] as StatusDescription
                  ,cat.[Description]
                  ,cat.Slug as CatSlug
                  ,p.[Deleted]
                  ,p.[Timestamp]
                  ,p.[TimestampApprovation]
                  ,p.EditCode,
                  pa.IdPost,
		          pa.IdPostAttachment,
		          pa.FileName,
		          pa.ContentType,
		          pa.Size
                   FROM [tblPosts]  p
                   inner join tblpoststatus st on p.idStatus = st.idstatus
                   inner join tblCategory cat on cat.idcategory = p.idcategory
		           left outer join tblPostsAttachments pa on pa.IdPost = p.IdPost
                   where p.deleted = 0 and isnull(pa.deleted,0) = 0 and p.IdPost = @idPost;";

        public const string PostByIdLight =
                   @"SELECT p.[IdPost]
                  ,p.[IdCategory]
                  ,p.[userid]
                  ,[Title]
                  ,[Body]
                  ,[SlugUrl]
                  ,p.[IdStatus]
                  ,st.[Description] as StatusDescription
                  ,cat.[Description]
                  ,cat.Slug as CatSlug
                  ,p.[Deleted]
                  ,p.[Timestamp]
                  ,p.[TimestampApprovation]
                  ,p.EditCode
                   FROM [tblPosts]  p
                   inner join tblpoststatus st on p.idStatus = st.idstatus
                   inner join tblCategory cat on cat.idcategory = p.idcategory
                   where p.deleted = 0 and p.IdPost = @idPost;";

        public const string PostByEditCodeLight =
                   @"SELECT p.[IdPost]
                  ,p.[IdCategory]
                  ,p.[userid]
                  ,[Title]
                  ,[Body]
                  ,[SlugUrl]
                  ,p.[IdStatus]
                  ,st.[Description] as StatusDescription
                  ,cat.[Description]
                  ,cat.Slug as CatSlug
                  ,p.[Deleted]
                  ,p.[Timestamp]
                  ,p.[TimestampApprovation]
                  ,p.EditCode
                   FROM [tblPosts]  p
                   inner join tblpoststatus st on p.idStatus = st.idstatus
                   inner join tblCategory cat on cat.idcategory = p.idcategory
                   where p.deleted = 0 and p.editCode = @editCode;";

        public const string PostByEditCode =
              @"SELECT p.[IdPost]
              ,p.[IdCategory]
              ,p.[userid]
              ,[Title]
              ,[Body]
              ,[SlugUrl]
              ,p.[IdStatus]
              ,st.[Description] as StatusDescription
              ,cat.[Description]
              ,cat.Slug as CatSlug
              ,p.[Deleted]
              ,p.[Timestamp]
              ,p.[TimestampApprovation]
              ,p.EditCode
              ,pa.IdPost,
		      pa.IdPostAttachment,
		      pa.FileName,
		      pa.ContentType,
		      pa.Size
               FROM [tblPosts]  p
               inner join tblpoststatus st on p.idStatus = st.idstatus
               inner join tblCategory cat on cat.idcategory = p.idcategory
		       left outer join tblPostsAttachments pa on pa.IdPost = p.IdPost
               where p.deleted = 0 and isnull(pa.deleted,0) = 0 and p.EditCode = @editCode;";

        public const string PostByStatusAndCategory =
           @"SELECT p.[IdPost]
          ,p.[IdCategory]
          ,p.[userid]
          ,[Title]
          ,[Body]
          ,[SlugUrl]
          ,p.[IdStatus]
          ,st.[Description] as StatusDescription
          ,cat.[Description]
          ,cat.Slug as CatSlug
          ,p.[Deleted]
          ,p.[Timestamp]
          ,p.[TimestampApprovation]
          ,p.EditCode
          ,(select count(idpost) from tblPostsAttachments pa where pa.idpost = p.IdPost and pa.deleted=0) as NumberOfAttachment
           FROM [tblPosts]  p
           inner join tblpoststatus st on p.idStatus = st.idstatus
           inner join tblCategory cat on cat.idcategory = p.idcategory
           where p.deleted = 0 and p.IdStatus=@idStatus and cat.idcategory = @idcategory order by Timestamp desc;";


        public const string SetPostStatus =
           @"UPDATE tblPosts SET idStatus=@idStatus,timestampApprovation=@timestamp" +
           " WHERE idpost = @idPost;";
    }


    public class QueryFileStore
    {
        public const string InsertUploadTemp =
            "INSERT INTO [tblTempAttachments]" +
            "([UserId] ,[FileName],[ContentType],[Size],[Timestamp])" +
            "VALUES (@userId,@filename,@contenttype,@size,@timestamp);";

        public const string SelectUploadTempByUserId =
            "select IdTempAttachment as IdPostAttachment,userid,filename,contenttype,size,timestamp from tblTempAttachments where userid = @userid and deleted=0";

        public const string TempAttachmentById =
            "select IdTempAttachment as IdPostAttachment,userid,filename,contenttype,size,timestamp from tblTempAttachments where IdTempAttachment = @id and deleted=0";

        public const string DeleteInTempFolder =
            "update tblTempAttachments set deleted=1 where userid=@userid and deleted=0;";

        public const string DeleteTempAttachment =
            "update tblTempAttachments set deleted=1 where IdTempAttachment=@id and deleted=0;";

        public const string DeleteAttachment =
            "update tblPostsAttachments set deleted=1 where IdPostAttachment=@id and deleted=0;";

        public const string AttachmentsByIdPost =
            "select IdPostAttachment,idPost,filename,contenttype,size,timestamp from tblTempAttachments where idPost = @idPost and deleted=0";
    }
}
