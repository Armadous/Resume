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

            // Bootstrap
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js",
                      "~/Scripts/bootstrap-datepicker.js"));

            //select2
            bundles.Add(new ScriptBundle("~/bundles/select2").Include(
                "~/Scripts/select2.min.js"));

            bundles.Add(new StyleBundle("~/css/select2").Include(
                "~/Content/css/select2.css",
                "~/Content/css/select2-bootstrap.css"));

            //TODO: Remove in favore of knockback control.
            // Knockout
            bundles.Add(new ScriptBundle("~/bundles/knockout").Include(
                "~/Scripts/knockout-3.1.0.js"));

            //TODO: Remove in favore of knockback control.
            // Backbone
            bundles.Add(new ScriptBundle("~/bundles/models").Include(
                "~/Scripts/models/*.js"));

            // Knockback (Backbone/Knockout middleman)
            bundles.Add(new ScriptBundle("~/bundles/knockback").Include(
                "~/Scripts/knockback-core-stack.js"));

            // HighCharts
            bundles.Add(new ScriptBundle("~/bundles/highcharts").Include(
                "~/Scripts/Highcharts-4.0.1/js/highcharts.js"));

            // Bootstrap and theme
            bundles.Add(new LessBundle("~/less/bootstrap").Include(
                "~/Content/bootstrap/bootstrap.less"));

            // Site Themes
            bundles.Add(new LessBundle("~/less/theme").Include(
                "~/Content/theming/Site.less"));

        }
    }
}
