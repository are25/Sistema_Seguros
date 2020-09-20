using Newtonsoft.Json;
using SistemaSeguros.API.Models;
using SistemaSeguros.EX;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace SistemaSeguros.API.Controllers
{
    [EnableCors("*", "*", "*")]
    public class ClientesController : ApiController
    {
        private readonly string vccNomClase = "CuentaClienteController";
        readonly SistemaSeguros_BD db = new SistemaSeguros_BD();
        #region Métodos Públicos

        [HttpGet]
        public IEnumerable<ClientesVM> CargarClientes()
        {
            HttpResponseMessage vloRespuestaApi;
            List<ClientesVM> vloListado=null;
            try
            {
                vloListado = ObtenerClientes();
            }
            catch (Exception)
            {
                vloRespuestaApi = new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
            return vloListado;
        }

        [HttpPut]
        public IHttpActionResult RegistroCliente(Clientes clientes)
        {
            string vlcRespuesta = String.Empty;
            IHttpActionResult vloRespuestaApi;
            try
            {
                vlcRespuesta = IngresoCliente(clientes);

                return CreatedAtRoute("DefaultApi", new { id = clientes.IdentificacionCliente }, clientes);

            }
            catch (Exception)
            {
                vloRespuestaApi = InternalServerError();
            }
            return vloRespuestaApi;
        }

        [HttpPatch]
        public IHttpActionResult EditarCliente(Clientes clientes)
        {
            string vlcRespuesta = String.Empty;
            IHttpActionResult vloRespuestaApi;
            try
            {
 
                vlcRespuesta = ActualizarCliente(clientes);

                vloRespuestaApi = Ok(vlcRespuesta);

            }
            catch (Exception)
            {
                vloRespuestaApi = InternalServerError();
            }
            return vloRespuestaApi;
        }

        [HttpDelete]
        public IHttpActionResult EliminarCliente(Clientes vloUsuario)
        {
            string vlcRespuesta = String.Empty;
            IHttpActionResult vloRespuestaApi;
            try
            {
                string vlcDatos = ObtenerInformacion();//Obtener la información enviada por el cliente como JSON

                vlcRespuesta = Eliminar(vloUsuario);

                vloRespuestaApi= Ok(vlcRespuesta);
             }
            catch (Exception)
            {
                vloRespuestaApi = InternalServerError();
            }
            return vloRespuestaApi;
        }

        #endregion
        #region Métodos privados
        private bool existeUsuario(string id)
        {
            return db.Clientes.Count(e => e.IdentificacionCliente == id) > 0;

        }

        /// <summary>
        /// Ingreso de usuario
        /// </summary>
        /// <param name="pvcDatos"></param>
        /// <returns></returns>
        private string IngresoCliente(Clientes vloUsuario)
        {
            try
            {
                if (existeUsuario(vloUsuario.IdentificacionCliente))
                {
                    return "2";//existe
                }
                else
                {
                    db.Clientes.Add(vloUsuario);
                }

                db.SaveChanges();
                return "1";

            }
            catch (Exception ex)
            {
                return "0";

                throw new ControlErrores(System.Reflection.MethodBase.GetCurrentMethod().Name, vccNomClase, ex.Message);
            }
        }

        /// <summary>
        /// Eliminación de usuario
        /// </summary>
        /// <param name="pvcDatos"></param>
        /// <returns></returns>
        private string Eliminar(Clientes vloUsuario)
        {
            try
            {
                if (existeUsuario(vloUsuario.IdentificacionCliente))
                {
                    Clientes usuarioEliminar = db.Clientes.SingleOrDefault(x => x.IdentificacionCliente == vloUsuario.IdentificacionCliente);

                    if (usuarioEliminar == null)
                    {
                        return "0";//error
                    }

                    db.Clientes.Remove(usuarioEliminar);

                    db.SaveChanges();
                    return "1";

                }
                else
                {
                    return "0";
                }
            }
            catch (Exception ex)
            {
                return "0";

                throw new ControlErrores(System.Reflection.MethodBase.GetCurrentMethod().Name, vccNomClase, ex.Message);
            }
        }
        /// <summary>
        /// Actualización de usuario
        /// </summary>
        /// <param name="pvcDatos"></param>
        /// <returns></returns>
        private string ActualizarCliente(Clientes vloUsuario)
        {
            try
            {
                if (existeUsuario(vloUsuario.IdentificacionCliente))
                {
                    db.Entry(vloUsuario).State = EntityState.Modified;

                    db.SaveChanges();
                    return "1";
                }
                else
                {
                    return "0";
                }
            }
            catch (Exception ex)
            {
                return "0";

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
        private List<ClientesVM> ObtenerClientes()
        {

            List<ClientesVM> listaClientes = new List<ClientesVM>();

            try
            {
                db.Clientes.ToList().ForEach(cp => listaClientes.Add(new ClientesVM()
                {
                    IdentificacionCliente = cp.IdentificacionCliente,
                    NombreCliente = cp.NombreCliente,
                    CorreoCliente = cp.CorreoCliente,
                    TelefonoContacto = cp.TelefonoContacto
                }));
                return listaClientes;
            }
            catch (Exception ex)
            {
                throw new ControlErrores(System.Reflection.MethodBase.GetCurrentMethod().Name, vccNomClase, ex.Message);
            }

        }

        #endregion
    }
}
