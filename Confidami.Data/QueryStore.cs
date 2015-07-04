using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Confidami.Data
{
    public class QueryStore
    {
        public const string InserPost = 
            @"INSERT INTO [dbo].[tblPosts]
           ([IdCategory]
           ,[Title]
           ,[Body]
           ,[SlugUrl]
           ,[Timestamp])
            VALUES (@idCategory,@title,@body,@slugUrl,@timeStamp)";

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
            @"SELECT [IdCategory],[Description]
            FROM [tblCategory]";

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
           where deleted = 0 and p.IdStatus=@idStatus order by Timestamp desc";


        public const string SetPostStatus =
           @"UPDATE tblPosts SET idStatus=@idStatus,timestampApprovation=@timestamp" +
           " WHERE idpost = @idPost";
    }
}
