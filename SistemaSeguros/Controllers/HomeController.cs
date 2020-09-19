using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace SistemaSeguros.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string Usuario)
        {
            HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie == null)
            {
                return View("Login");
            }
            else{
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);
                ViewBag.Usuario = ticket.Name;
                return View();
            }
            
        }

        public ActionResult Login()
        {
            return View();
        }


        public ActionResult Usuarios()
        {
           return ValidarPantalla();
        }

        public ActionResult Clientes()
        {
            return ValidarPantalla();
        }

        public ActionResult Riesgo()
        {
            return ValidarPantalla();
        }

        public ActionResult Cubrimiento()
        {
            return ValidarPantalla();
        }

        public ActionResult Poliza()
        {
            ViewBag.Message = "Póliza de seguros.";

            return ValidarPantalla();
        }

        public ActionResult Cerrar()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Home");
        }

        public ActionResult ManejoPoliza()
        {
            return ValidarPantalla();
        }

        public JsonResult CrearCookie(string Usuario)
        {
            FormsAuthentication.SetAuthCookie(Usuario, false);
            return Json(new
            {
                datosCliente = true,
            }, JsonRequestBehavior.AllowGet);

        }

        private ActionResult ValidarPantalla()
        {

            HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie == null)
            {
                return View("Login");
            }
            else
            {
                return View();

            }

        }
    }
}