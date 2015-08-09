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
        public const string MenuAdmin = "_MainMenuAdmin";
        public const string Insert = "Insert";
        public const string AdminLayout = "~/Views/Shared/_LayoutAdmin.cshtml";
        public const string Login = "Login";
    }

    public class ControllerStore
    {
        public const string Account = "Account";
        public const string Manager = "Manager";
        public const string Home = "Home";

        //nomi fisici e routing per contents-segnalazioni
        public const string Contents = "Contents";
        public const string Segnalazioni = "segnalazioni";
        //
        public const string Search = "Search";
        public const string Newsletter = "Newsletter";
    }

    public class ActionsStore
    {
        public const string Home = "Home";
        public const string AddPost = "AddPost";
        public const string Contact = "Contact";


        public const string EditCodeView = "EditPostCode";
        public const string InserisciExtra= "inserisciextra";

        public const string Moderation = "Moderation";
        public const string Approve = "Approve";
        public const string Upload = "Upload";

        public const string Contents = "Index";
        public const string ContentsInsert = "Insert";
        public const string ContentsCategory = "Category";
        public const string FindEditCode = "FindEditCode";
        public const string EditPost = "Edit";

        public const string Search = "Index";

        public const string About = "About";
        public const string Newsletter = "Index";

    }


    public class RouteStore
    {
        public const string EditPostView = "EditPostView";
        public const string EditPostInsert = "EditPostInsert";
        public const string FindEditCodeSubmit = "FindEditPostSubmit";
    }
    public class JsonAction
    {
        public string TemAttachMents = "GetTempAttachments";
    }
}
