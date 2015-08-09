using System;

namespace Confidami.Model
{
    public class TempAttachMent
    {
        public int IdPostAttachment { get; set;}
        public string UserId { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public long Size { get; set; }
        public DateTime Timestamp { get; set; }
    }

    public class AttachMent
    {
        public int IdPostAttachment { get; set; }
        public long IdPost { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public long Size { get; set; }
        public DateTime Timestamp { get; set; }
    }
}