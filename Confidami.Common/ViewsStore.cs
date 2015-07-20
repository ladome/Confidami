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
        public const string Menu = "_MainMenu";

    }

    public class ControllerStore
    {
        public const string Account = "Account";
        public const string Manager = "Manager";
        public const string Home = "Home";
        public const string Contents = "Contents";
        public const string Search = "Search";
        public const string Newsletter = "Newsletter";
    }

    public class ActionsStore
    {
        public const string Home = "Home";
        public const string AddPost = "AddPost";
        public const string Contact = "Contact";

        public const string Moderation = "Moderation";
        public const string Approve = "Approve";
        public const string Upload = "Upload";

        public const string Contents = "Index";
        public const string ContentsInsert = "Insert";
        public const string ContentsCategory = "Category";

        public const string Search = "Index";

        public const string About = "About";
        public const string Newsletter = "Index";


        ///public static string Contact { get; set; }
    }
}
