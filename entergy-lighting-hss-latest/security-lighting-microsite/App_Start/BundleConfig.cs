using System.Web;
using System.Web.Optimization;

namespace security_lighting_microsite {
  public class BundleConfig {
    public static void RegisterBundles(BundleCollection bundles) {
      bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                  "~/scripts/lib/modernizr-*"));

      bundles.Add(new ScriptBundle("~/bundles/lib").Include(
                "~/scripts/respond.js",
                "~/scripts/lib/jquery.ba-throttle-debounce.min.js",
                "~/scripts/lib/isInViewport.min.js",
                "~/scripts/lib/easings.js",
                "~/scripts/lib/parsley.js",                
                "~/scripts/lib/bootstrap/dropdown.js"));

      bundles.Add(new ScriptBundle("~/bundles/site").Include(
                "~/scripts/site.js",
                "~/scripts/pages/index.js",
                "~/scripts/pages/fixture.js"
        ));

      bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/styles/lib/bootstrap/bootstrap.css",
                "~/styles/site.css"));
    }
  }
}
