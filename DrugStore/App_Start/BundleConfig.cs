using System.Web.Optimization;

namespace DrugStore
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            Bundle jQuery = new ScriptBundle("~/bundles/jquery").Include("~/Scripts/jquery-{version}.js");
            Bundle jQueryUI = new ScriptBundle("~/bundles/jqueryui").Include("~/Scripts/jquery-ui-{version}.js");
            Bundle jQueryVal = new ScriptBundle("~/bundles/jqueryval").Include("~/Scripts/jquery.validate*");
            Bundle modernizr = new ScriptBundle("~/bundles/modernizr").Include("~/Scripts/modernizr-*");
            Bundle bootstrap = new ScriptBundle("~/bundles/bootstrap").Include("~/Scripts/bootstrap.js", "~/Scripts/respond.js");
            Bundle UserScripts = new ScriptBundle("~/bundles/UserScripts").Include("~/Scripts/main.js", "~/Scripts/DialogForm.js");

            Bundle css = new StyleBundle("~/Content/css").Include("~/Content/bootstrap.css", "~/Content/site.css");
            Bundle themes = new StyleBundle("~/Content/themes/base/css").Include("~/Content/themes/base/all.css");
            Bundle fontAwesome = new StyleBundle("~/Content/fontawesome").Include("~/Content/fontawesome-all.min.css");

            bundles.Add(jQuery);
            bundles.Add(jQueryUI);
            bundles.Add(jQueryVal);
            bundles.Add(modernizr);
            bundles.Add(bootstrap);
            bundles.Add(UserScripts);
            bundles.Add(css);
            bundles.Add(themes);
            bundles.Add(fontAwesome);
        }
    }
}
