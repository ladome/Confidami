using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Confidami.Common;

namespace Confidami.Web.ViewModel
{
    public class PostViewModel
    {
        public int IdPost { get; set; }

        [Required]
        [MaxLength(50)]
        public string Title { get; set; }

        [Required]
        public string Body { get; set; }

        [Range(1,100)]
        public int IdCategory { get; set; }

        public IEnumerable<PostViewModel> Posts { get; set; }
        public IEnumerable<Category> Categories { get; set; } 
    }
}