using System.Web.Optimization;

namespace NZBDash.UI
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery.signalR-{version}.js",
                        "~/Scripts/bootstrap-notify.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js",
                        "~/Scripts/bootstrap-switch.js"
                      ));

            /* ---- Dashboard Styles ---- */

            bundles.Add(new StyleBundle("~/Content/dashboard").Include("~/Content/bootstrap.css",
                "~/Content/Dashboard/metisMenu.css",
                "~/Content/dashboard.css",
                "~/Content/Dashboard/morris.css"));

            bundles.Add(new ScriptBundle("~/bundles/dashboard").Include("~/Scripts/Dashboard/metisMenu.js",
                        "~/Scripts/Dashboard/raphael.js",
                        "~/Scripts/Dashboard/morris.js",
                        "~/Scripts/Dashboard/mocha.js",
                        "~/Scripts/Dashboard/dashboard.js",
                        "~/Scripts/Dashboard/dashboard.js"

                        ));

            bundles.Add(new ScriptBundle("~/bundles/flot").Include("~/Scripts/Dashboard/flot/jquery.flot.js",
                "~/Scripts/Dashboard/flot/jquery.flot.js"));
            /* ---- Dashboard Styles ---- */


            bundles.Add(new ScriptBundle("~/bundles/gridster").Include("~/Scripts/jquery.gridster.js"));

            bundles.Add(new StyleBundle("~/Content/gridster").Include("~/Content/jquery.gridster.css"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/font-awesome.css",
                      "~/Content/bootstrap-switch.css"));

            var ajaxGrid = new ScriptBundle("~/bundles/gridmvc").Include(
                "~/Scripts/URI.js",
                "~/Scripts/gridmvc.js",
                "~/Scripts/gridmvc-ext.js",
                "~/Scripts/ladda-bootstrap/ladda.min.js",
                "~/Scripts/ladda-bootstrap/spin.min.js");
            bundles.Add(ajaxGrid);

        }
    }
}
