using System.Web;
using System.Web.Optimization;

namespace TURGWEB
{
    public class BundleConfig
    {
        // Paketleme hakkında daha fazla bilgi için lütfen https://go.microsoft.com/fwlink/?LinkId=301862 adresini ziyaret edin
        public static void RegisterBundles(BundleCollection bundles)
        {


            bundles.Add(new StyleBundle("~/basic/css").Include(
                "~/Styles/bootstrap.min.css",
                "~/Styles/mdb.min.css",
                "~/Styles/style.css"
                ));

            bundles.Add(new ScriptBundle("~/basic/js").Include(
                "~/Scripts/jquery-3.3.1.min.js",
                "~/Scripts/popper.min.js",
                "~/Scripts/bootstrap.min.js",
                "~/Scripts/mdb.min.js"
                ));


        }
    }
}
