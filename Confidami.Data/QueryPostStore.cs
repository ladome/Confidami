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
           ,[Timestamp])
            VALUES (@idCategory,@title,@body,@slugUrl,@timeStamp);";

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

        public const string InsertPostAttachment =
            @"INSERT INTO [tblPostsAttachments]
           ([IdPost]
           ,[FileName]
           ,[ContentType]
           ,[Size])
            VALUES (@idPost,@fileName,@contentType,@size)";

        public const string PostByStatus =
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
           where deleted = 0 and p.IdStatus=@idStatus order by Timestamp desc;";

        public const string PostByStatusAndCategory =
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
           where deleted = 0 and p.IdStatus=@idStatus and cat.idcategory = @idcategory order by Timestamp desc;";


        public const string SetPostStatus =
           @"UPDATE tblPosts SET idStatus=@idStatus,timestampApprovation=@timestamp" +
           " WHERE idpost = @idPost;";
    }


    public class QueryFileStore
    {
        public const string InsertUploadTemp =
            "INSERT INTO [tblTempAttachments]" +
            "([UserId] ,[FileName],[ContentType],[Size])" +
            "VALUES (@userId,@filename,@contenttype,@size);";

        public const string SelectUploadTempByUserId =
            "select userid,filename,contenttype,size from tblTempAttachments where userid = @userid and deleted=0";

        public const string TempAttachmentById =
            "select IdTempAttachment as Id,userid,filename,contenttype,size from tblTempAttachments where IdTempAttachment = @id and deleted=0";

        public const string DeleteInTempFolder =
            "update tblTempAttachments set deleted=1 where userid=@userid and deleted=0;";

        public const string DeleteTempAttachment =
            "update tblTempAttachments set deleted=1 where IdTempAttachment=@id and deleted=0;";
    }
}
