using System.Web;
using System.Web.Optimization;

namespace Confidami.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));


            //Parsley validator http://parsleyjs.org/
            bundles.Add(new ScriptBundle("~/bundles/parsleyvalidator").Include(
                        "~/Scripts/parsley/parsley.min.js",
                        "~/Scripts/parsley/it.js"));

            bundles.Add(new StyleBundle("~/Content/parsleycss").Include("~/Content/parsley.css"));




            bundles.Add(new ScriptBundle("~/bundles/mainjs").Include(
                        "~/Scripts/main.js*"));


            bundles.Add(new ScriptBundle("~/bundles/dropzonescripts").Include(
                     "~/Scripts/dropzone/dropzone.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.min.js",
                      "~/Scripts/respond.js"));

            //bundles.Add(new StyleBundle("~/Content/css").Include(
            //          "~/Content/bootstrap.min.css",
            //          "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/skin/paper/bootstrap.min.css",
                      "~/Content/site.css"));



            bundles.Add(new StyleBundle("~/Content/dropzonescss").Include(
                     "~/Scripts/dropzone/basic.css",
                     "~/Scripts/dropzone/dropzone.css"));


            bundles.Add(new ScriptBundle("~/Content/raty-2.7.0/js").IncludeDirectory("~/Content/raty-2.7.0", "*.js", true));
            bundles.Add(new StyleBundle("~/Content/raty-2.7.0/css").Include("~/Content/raty-2.7.0/*.css"));
            bundles.Add(new ScriptBundle("~/Scripts/raty").Include("~/Scripts/raty.js"));


        }
    }
}
