using System.Web;
using System.Web.Optimization;

namespace School_Appln
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/ThemesJS").Include(
                    "~/Scripts/js/plugins/loaders/pace.min.js",
                    "~/Scripts/js/core/libraries/jquery.min.js",
                    "~/Scripts/js/core/libraries/bootstrap.min.js",
                    "~/Scripts/js/plugins/loaders/blockui.min.js",
                    "~/Scripts/js/plugins/visualization/d3/d3.min.js",
                    "~/Scripts/js/plugins/visualization/d3/d3_tooltip.js",
                    "~/Scripts/js/plugins/forms/styling/switchery.min.js",
                    "~/Scripts/js/plugins/forms/styling/uniform.min.js",
                    "~/Scripts/js/plugins/forms/selects/bootstrap_multiselect.js",
                    "~/Scripts/js/plugins/ui/moment/moment.min.js",
                    "~/Scripts/js/plugins/pickers/daterangepicker.js",
                    "~/Scripts/js/core/app.js"
            ));

            bundles.Add(new StyleBundle("~/Content/ThemesCSS").Include(
                    "~/Content/css/icons/icomoon/styles.css",
                    "~/Content/css/bootstrap.css",
                    "~/Content/css/core.css",
                    "~/Content/css/components.css",
                    "~/Content/css/colors.css"
                ));




            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
        }
    }
}
