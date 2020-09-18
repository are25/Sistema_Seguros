using System.Web;
using System.Web.Optimization;

namespace SistemaSeguros
{
    public class BundleConfig
    {
        // Para obtener más información sobre las uniones, visite https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Utilice la versión de desarrollo de Modernizr para desarrollar y obtener información. De este modo, estará
            // para la producción, use la herramienta de compilación disponible en https://modernizr.com para seleccionar solo las pruebas que necesite.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"
                      , "~/Content/sweetalert/sweetalert.min.js"
                      , "~/Scripts/jquery.dataTables.min.js",
                      "~/Scripts/dataTables.bootstrap.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"
                      , "~/Content/dataTables.bootstrap.min.css"));

            bundles.Add(new StyleBundle("~/Content/login").Include(
                     "~/Content/util.css",
                     "~/Content/main.css"));

            bundles.Add(new StyleBundle("~/Content/fuentes").Include(
                    "~/fonts/font-awesome-4.7.0/font-awesome.css",
                    "~/fonts/Linearicons-Free-v1.0.0/icon-font.min.css"));


            //script personalizados
            bundles.Add(new ScriptBundle("~/bundles/admin-login").Include("~/Scripts/CuentaUsuario/Login.js"));
            bundles.Add(new ScriptBundle("~/bundles/admin-usuarios").Include("~/Scripts/CuentaUsuario/Usuarios.js"));
            bundles.Add(new ScriptBundle("~/bundles/admin-clientes").Include("~/Scripts/CuentaUsuario/Clientes.js"));
            bundles.Add(new ScriptBundle("~/bundles/admin-tipoRiesgo").Include("~/Scripts/Catalogo/TipoRiesgo.js"));


        }
    }
}
