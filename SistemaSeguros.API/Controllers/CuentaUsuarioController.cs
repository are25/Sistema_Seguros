using Newtonsoft.Json;
using SistemaSeguros.API.Models;
using SistemaSeguros.ET.EntidadesJS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SistemaSeguros.EX;

using System.Web.Http.Cors;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;

namespace SistemaSeguros.API.Controllers
{
    [EnableCors("*", "*", "*")]
    public class CuentaUsuarioController : ApiController
    {
        private readonly string vccNomClase = "CuentaUsuarioController";
        readonly BDSistemaSeguros db = new BDSistemaSeguros();

        #region Métodos Públicos
        [HttpPost]
        public HttpResponseMessage InicioSesion()
        {
            string vlcRespuesta = String.Empty;
            HttpResponseMessage vloRespuestaApi;
            try
            {
                string vlcDatos = ObtenerInformacion();//Obtener la información enviada por el cliente como JSON

                vlcRespuesta = ValidarInicio(vlcDatos);

                vloRespuestaApi = new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(vlcRespuesta) };

                return vloRespuestaApi;
            }
            catch (Exception)
            {
                vloRespuestaApi = new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
            return vloRespuestaApi;
        }

        [HttpGet]
        public IEnumerable<Usuarios> CargarUsuarios()
        {
            HttpResponseMessage vloRespuestaApi;
            List<Usuarios> vloListado = null;
            try
            {
                vloListado = ObtenerUsuarios();
 
            }
            catch (Exception)
            {
                vloRespuestaApi = new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
            return vloListado;
        }

        [HttpPut]
        public IHttpActionResult RegistroUsuario(Usuarios usuario)
        {
            string vlcRespuesta = String.Empty;
            IHttpActionResult vloRespuestaApi;
            try
            {
                vlcRespuesta = IngresoUsuario(usuario);

                return CreatedAtRoute("DefaultApi", new { id = usuario.Identificacion }, usuario);

             }
            catch (Exception)
            {
                vloRespuestaApi = InternalServerError();
            }
            return vloRespuestaApi;
        }

        [HttpPatch]
        public IHttpActionResult EditarUsuario(Usuarios usuario)
        {
            string vlcRespuesta = string.Empty;
            IHttpActionResult vloRespuestaApi;
            try
            {
 
                vlcRespuesta = ActualizarUsuario(usuario);

                vloRespuestaApi = Ok(vlcRespuesta);
             }
            catch (Exception)
            {
                vloRespuestaApi = InternalServerError();
            }
            return vloRespuestaApi;
        }

        [HttpDelete]
        public HttpResponseMessage EliminarUsuario(Usuarios usuarios)
        {
            string vlcRespuesta = string.Empty;
            HttpResponseMessage vloRespuestaApi;
            try
            {
 
                vlcRespuesta = Eliminar(usuarios);

                vloRespuestaApi = new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(vlcRespuesta) };

                return vloRespuestaApi;
            }
            catch (Exception)
            {
                vloRespuestaApi = new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
            return vloRespuestaApi;
        }
         
        #endregion

        #region Métodos privados
        private bool existeUsuario(string id)
        {
            return db.Usuarios.Count(e => e.Identificacion == id) > 0;

        }

        /// <summary>
        /// Ingreso de usuario
        /// </summary>
        /// <param name="pvcDatos"></param>
        /// <returns></returns>
        private string IngresoUsuario(Usuarios vloUsuario)
        {
            try
            {
                 if (existeUsuario(vloUsuario.Identificacion))
                {
                    return "2";//existe
                }
                else
                {
                    db.Usuarios.Add(vloUsuario);
                }

                try
                {
                    db.SaveChanges();
                    return "1";
                }
                catch (DbUpdateConcurrencyException)
                {
                    return "0";
                }

            }
            catch (Exception ex)
            {
                throw new ControlErrores(System.Reflection.MethodBase.GetCurrentMethod().Name, vccNomClase, ex.Message);

            }
        }

        
        /// <summary>
        /// Eliminación de usuario
        /// </summary>
        /// <param name="pvcDatos"></param>
        /// <returns></returns>
        private string Eliminar(Usuarios vloUsuario)
        {
            try
            {
                if (existeUsuario(vloUsuario.Identificacion))
                {
                    Usuarios usuarioEliminar = db.Usuarios.SingleOrDefault(x => x.Identificacion == vloUsuario.Identificacion);

                    if (usuarioEliminar == null)
                    {
                        return "0";//error
                    }

                    db.Usuarios.Remove(usuarioEliminar);
                    try
                    {
                        db.SaveChanges();
                        return "1";
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        return "0";
                    }
                }
                else
                {
                    return "0";
                }

            }
            catch (Exception ex)
            {

                throw new ControlErrores(System.Reflection.MethodBase.GetCurrentMethod().Name, vccNomClase, ex.Message);

            }
        }
        /// <summary>
        /// Actualización de usuario
        /// </summary>
        /// <param name="pvcDatos"></param>
        /// <returns></returns>
        private string ActualizarUsuario(Usuarios vloUsuario)
        {
            try
            {
                 if (existeUsuario(vloUsuario.Identificacion))
                {
                    db.Entry(vloUsuario).State = EntityState.Modified;

                    try
                    {
                        db.SaveChanges();
                        return "1";
                    }
                    catch (DbUpdateConcurrencyException)
                    {

                        return "0";

                    }
                }
                else
                {
                    return "0";
                }

            }
            catch (Exception ex)
            {

                throw new ControlErrores(System.Reflection.MethodBase.GetCurrentMethod().Name, vccNomClase, ex.Message);

            }
        }
        /// <summary>
        /// Método para validar información obtenida por pantalla.
        /// </summary>
        /// <param name="pvcDatos"></param>
        /// <returns></returns>
        private string ValidarInicio(string pvcDatos)
        {
            try
            {
                Usuario vloInicioLogin = JsonConvert.DeserializeObject<Usuario>(pvcDatos);
                List<Usuario> vloUsuario = new List<Usuario>();

                var existeUsuario = db.Usuarios
                       .Where(b => b.CorreoUsuario == vloInicioLogin.correo && b.Contrasennia == vloInicioLogin.credencial)
                       .FirstOrDefault();

                if (!String.IsNullOrEmpty(existeUsuario.Identificacion))
                {
                    return existeUsuario.NombreUsuario;
                }
                else
                {
                    return String.Empty;
                }
            }
            catch (Exception ex)
            {
                throw new ControlErrores(System.Reflection.MethodBase.GetCurrentMethod().Name, vccNomClase, ex.Message);

            }
        }
        /// <summary>
        /// Método para obtener la información recibida desde el cliente.
        /// </summary>
        /// <returns>Información recibida por el cliente</returns>
        private string ObtenerInformacion()
        {
            string vlcDatos = Request.Content.ReadAsStringAsync().Result;

            return vlcDatos;
        }
        /// <summary>
        /// Método para obtener todos los usuarios.
        /// </summary>
        /// <returns></returns>
        private List<Usuarios> ObtenerUsuarios()
        {
            List<Usuarios> listaUsuarios = new List<Usuarios>();

            db.Usuarios.ToList().ForEach(cp => listaUsuarios.Add(new Usuarios()
            {
                Identificacion = cp.Identificacion,
                NombreUsuario = cp.NombreUsuario,
                Contrasennia = cp.Contrasennia,
                CorreoUsuario = cp.CorreoUsuario
            }));
            return listaUsuarios;
        }

        #endregion
    }
}
