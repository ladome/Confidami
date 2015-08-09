using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Confidami.Web.ViewModel
{
    public class EditPostCodeViewModel
    {
        [Required]
        public string EditCode { get; set; }

        [Required, MinLength(6, ErrorMessage = "La password deve contenere almeno 6 caratteri")]
        public string Password { get; set; }
    }

    public class InsertEditPostCodeViewModel : EditPostCodeViewModel
    {
        [Range(1, Int64.MaxValue)]
        public long IdPost { get; set; }

        [EmailAddress(ErrorMessage = "Formato email non valido")]
        public string Email { get; set; }

    }


}