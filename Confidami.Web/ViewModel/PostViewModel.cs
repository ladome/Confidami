using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web;
using Confidami.Common;
using Confidami.Common.Utility;
using Confidami.Model;
using Newtonsoft.Json;

namespace Confidami.Web.ViewModel
{
    public class PostViewModelBase
    {
        public long  IdPost { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }

        public int IdCategory { get; set; }

        public string CategoryPost { get; set; }
    }
    
    public class PostViewModel
    {
        public IEnumerable<PostViewModelBase> Posts { get; set; }

        public bool IsAdmin { get; set; }
        //public string CurrentUser { get; set; }

    }

    public class InsertPostViewModel
    {
        [Required(ErrorMessage = "Il titolo è obbligatorio")]
        [MaxLength(50)]
        public string Title { get; set; }

        [Required(ErrorMessage = "Il corpo del messaggio è obbligatorio")]
        public string Body { get; set; }

        [Range(1, 100)]
        public int IdCategory { get; set; }

        public IEnumerable<Category> Categories { get; set; }
    }

    public class TempAttachMentViewModel
    {
        private string _folder;
        public TempAttachMentViewModel(string folder)
        {
            _folder = folder;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public long Size { get; set; }

        public string FullPath
        {
            get
            {
                var fileNameThumb = String.Format("{0}{1}{2}", Path.GetFileNameWithoutExtension(Name), "_thumb",
                    Path.GetExtension(Name));
                if (Path.IsPathRooted(Config.UploadsTempFolder))
                    return Path.Combine(Config.UploadsTempFolder, _folder);
                var virtualRoot = "~/" + Config.UploadsTempFolder;
                var abosoluteUrl = VirtualPathUtility.ToAbsolute(virtualRoot);
                var a = VirtualPathUtility.Combine(VirtualPathUtility.AppendTrailingSlash(abosoluteUrl), _folder);
                var b =VirtualPathUtility.AppendTrailingSlash(a);
                return VirtualPathUtility.Combine(b, fileNameThumb);
            }
        }
    }
}