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
           ,[SlugUrl])
            VALUES (@idCategory,@title,@body,@slugUrl)";

        public const string AllPosts =
           @"SELECT [IdPost]
          ,[IdCategory]
          ,[Title]
          ,[Body]
          ,[SlugUrl]
          ,p.[IdStatus]
          ,[Description]
          ,[Deleted]
          ,[Timestamp]
          ,[TimestampApprovation]
           FROM [tblPosts]  p
           inner join tblpoststatus st on p.idStatus = st.idstatus   
           where deleted = 0 order by Timestamp desc";

        public const string AllCategory =
            @"SELECT [IdCategory],[Description]
            FROM [tblCategory]";

    }
}
