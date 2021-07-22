using System.Web;
using System.Web.Optimization;

namespace test_mvc
{
    public class BundleConfig
    {
        // Дополнительные сведения об объединении см. на странице https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include("~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include("~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include("~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include("~/Scripts/bootstrap.js", "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/spcontext").Include("~/Scripts/spcontext.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/bootstrap.edited.css"
                                                                 //"~/Content/bootstrap.min.css"
                                                                 , "~/Content/site.css"
                ));

            bundles.Add(new StyleBundle("~/bundles/css").Include(
                 "~/Content/bootstrap-slider.css",
               "~/Content/bootstrap.edited.css",
                //"~/Content/select2.min.css",
                "~/Content/datepicker.min.css",
                 //"~/Content/jquery.dataTables.min.css",
                 "~/Content/site.css"
               ));

            bundles.Add(new ScriptBundle("~/bundles/scripts").Include(
                "~/Scripts/jquery-{version}.js",
                "~/Scripts/bootstrap.js",
                "~/Scripts/select2.min.js",
                "~/Scripts/select2.ru.js",
                "~/Scripts/datepicker.min.js",
                "~/Scripts/bootstrap-slider.min.js",
                "~/Scripts/jquery.unobtrusive-ajax.js",
                "~/Scripts/jquery.validate.js",
                "~/Scripts/jquery.validate.unobtrusive.js",
                "~/Scripts/respond.js",
                "~/Scripts/spcontext.js",
                "~/Scripts/Utils.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval/ru", "https://cdnjs.cloudflare.com/ajax/libs/jquery-validate/1.19.1/localization/messages_ru.min.js"));

            BundleTable.EnableOptimizations = true;
            bundles.UseCdn = true;
        }
    }
}
