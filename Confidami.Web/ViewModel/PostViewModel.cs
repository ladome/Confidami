using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using Confidami.Common;
using Confidami.Common.Utility;
using Confidami.Model;
using Microsoft.Ajax.Utilities;
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
        public string CategorySlug { get; set; }
        public string TitleSlug { get; set; }
        public virtual bool HasAttachMents { get; set; }
    }

    public class PostViewModelSingleContent : PostViewModelBase
    {
        public PostViewModelSingleContent()
        {
            AttachMenents = new List<PostAttachMentViewModel>();
        }
        public List<PostAttachMentViewModel> AttachMenents { get; set; }
        public override bool HasAttachMents { get { return AttachMenents!= null && AttachMenents.Any(); } }
        public DateTime CreationDate { get; set; }

        public List<PostAttachMentViewModel> ImageFile
        {
            get { return AttachMenents.Where(x => x.IsImage).ToList(); }
        }

        public List<PostAttachMentViewModel> OtherFile
        {
            get { return AttachMenents.Where(x => !x.IsImage).ToList(); }
        }

    }
    
    public class PostViewModel
    {
        public PostViewModel()
        {
            Posts = new List<PostViewModelBase>();
        }
        public IEnumerable<PostViewModelBase> Posts { get; set; }

        //public string CurrentUser { get; set; }
    }

    public class SearchViewModel
    {
        private readonly PostViewModel _postvm;

        public SearchViewModel()
        {
            _postvm = new PostViewModel();
        }
        public SearchViewModel(PostViewModel postvm)
        {
            _postvm = postvm;
        }

        [Required(ErrorMessage = "La chiave di ricerca è richiesta")]
        public string Key { get; set; }

        public PostViewModel SearchResults
        {
            get
            {
                if (_postvm != null && !string.IsNullOrEmpty(Key))
                    _postvm.Posts.ForEach(x => { x.Body = SubStringOfSearched(x.Body, Key, 10); });
                return _postvm;
            }
        }

        public IEnumerable<Category> Categories { get; set; }

        private string SubStringOfSearched(string body, string key,int numberofchar)
        {
            var idx = body.IndexOf(key, System.StringComparison.CurrentCultureIgnoreCase);
            var lastIdx = body.LastIndexOf(key, System.StringComparison.CurrentCultureIgnoreCase);
            var startIndexCut = 0;
            var diff = idx - numberofchar;
            if (diff >= 0)
            {
                startIndexCut = diff;
            }

            return body.Substring(startIndexCut, numberofchar + key.Length);
        }
    }



    public class InsertPostViewModel
    {
        [Required(ErrorMessage = "Il titolo è obbligatorio")]
        [MaxLength(100)]
        [MinLength(5)]
        public string Title { get; set; }

        [Required(ErrorMessage = "Il corpo del messaggio è obbligatorio")]
        [MaxLength(400)]
        public string Body { get; set; }

        [Range(1, 100)]
        public int IdCategory { get; set; }

        public IEnumerable<Category> Categories { get; set; }

        public bool IsModifica { get; set; }
    }

    public class EditPostViewModel : InsertPostViewModel
    {
        public long IdPost { get; set; }
    }

    public class TempAttachMentViewModel
    {
        private readonly string _folder;
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
                var baseFolder = Config.UploadsTempFolder;
                var fileNameThumb = String.Format("{0}{1}{2}", Path.GetFileNameWithoutExtension(Name), "_thumb",
                    Path.GetExtension(Name));
                if (Path.IsPathRooted(baseFolder))
                    return Path.Combine(baseFolder, _folder);
                var virtualRoot = "~/" + baseFolder;
                var abosoluteUrl = VirtualPathUtility.ToAbsolute(virtualRoot);
                var a = VirtualPathUtility.Combine(VirtualPathUtility.AppendTrailingSlash(abosoluteUrl), _folder);
                var b =VirtualPathUtility.AppendTrailingSlash(a);
                return VirtualPathUtility.Combine(b, fileNameThumb);
            }
        }
    }

    public class PostAttachMentViewModel
    {
        private readonly string _folder;
        public PostAttachMentViewModel(string folder)
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
                var baseFolder = Config.UploadsFolder;
                var fileNameThumb = String.Format("{0}{1}{2}", Path.GetFileNameWithoutExtension(Name), "",
                    Path.GetExtension(Name));
                if (Path.IsPathRooted(baseFolder))
                    return Path.Combine(baseFolder, _folder);
                var virtualRoot = "~/" + baseFolder;
                var abosoluteUrl = VirtualPathUtility.ToAbsolute(virtualRoot);
                var a = VirtualPathUtility.Combine(VirtualPathUtility.AppendTrailingSlash(abosoluteUrl), _folder);
                var b =VirtualPathUtility.AppendTrailingSlash(a);
                return VirtualPathUtility.Combine(b, fileNameThumb);
            }
        }

        public bool IsImage
        {
            get
            {
                return
                    Config.UploadImageExtensions.Any(
                        y => y.Contains(Name.GetExtension().RemoveExtensionPoint()));
            }
        }
    }
}