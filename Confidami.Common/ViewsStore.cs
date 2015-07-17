using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Confidami.Common
{
    public class ViewsStore
    {
        public const string Index = "Index";
        public const string Moderation = "Moderation";

    }

    public class ControllerStore
    {
        public const string Account = "Account";
        public const string Post = "Post";
        public const string Home = "Home";        
        public const string Segnalazioni = "Segnalazioni";
        public const string Search = "Search";
        public const string Newsletter = "Newsletter";
    }

    public class ActionsStore
    {
        public const string Home = "Home";
        public const string AddPost = "AddPost";
        public const string Contact = "Contact";

        public const string Moderation = "Modera";
        public const string Approve = "Approve";
        public const string Upload = "Upload";

        public const string Segnalazioni = "Index";
        public const string SegnalazioniInsert = "Insert";
        public const string SegnalazioniCategory = "Category";        

        public const string About = "About";
        public const string Newsletter = "Index";


        ///public static string Contact { get; set; }
    }
}
