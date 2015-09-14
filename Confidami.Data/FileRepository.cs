using System;
using System.Collections.Generic;
using System.Linq;
using Confidami.Model;
using Dapper;

namespace Confidami.Data
{
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


        public void DeleteAttachment(int id)
        {
            using (var conn = DbUtilities.Connection)
            {
                conn.Execute(QueryFileStore.DeleteAttachment,
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

        public IEnumerable<AttachMent> GetAttachmentsByIdPost(long idPost)
        {
            using (var conn = DbUtilities.Connection)
            {
                return conn.Query<AttachMent>(QueryFileStore.AttachmentsByIdPost, new { idPost = idPost });
            }
        }
    }
}