using System.Web;
using System.Web.Optimization;

namespace Resume
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
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

            bundles.Add(new ScriptBundle("~/bundles/knockout").Include(
                "~/Scripts/knockout-3.1.0.js"));

            bundles.Add(new ScriptBundle("~/bundles/models").Include(
                "~/Scripts/models/*.js"));

            bundles.Add(new LessBundle("~/less/bootstrap").Include(
                "~/Content/bootstrap/bootstrap.less"));

            bundles.Add(new LessBundle("~/less/theme").Include(
                "~/Content/theming/Site.less"));
        }
    }
}
