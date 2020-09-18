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
            FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);
            ViewBag.Usuario = ticket.Name;
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }


        public ActionResult Usuarios()
        {
            return View();
        }

        public ActionResult Clientes()
        {
            return View();
        }

        public ActionResult Riesgo()
        {
            return View();
        }


        public ActionResult Poliza()
        {
            ViewBag.Message = "Póliza de seguros.";

            return View();
        }

        public ActionResult Cerrar()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Home");
        }

        public JsonResult CrearCookie(string Usuario)
        {
            FormsAuthentication.SetAuthCookie(Usuario, false);
            return Json(new
            {
                datosCliente = true,
            }, JsonRequestBehavior.AllowGet);

        }
    }
}