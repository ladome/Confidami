using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
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
        public string Name { get; set; }
        public long Size { get; set; }        
    }
}