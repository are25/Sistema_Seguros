using Newtonsoft.Json;
using SistemaSeguros.API.Models;
using SistemaSeguros.EX;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
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
        readonly BDSistemaSeguros db = new BDSistemaSeguros();
        #region Métodos Públicos
         
        [HttpGet]
        public HttpResponseMessage CargarClientes()
        {
            HttpResponseMessage vloRespuestaApi;
            List<Clientes> vloListado;
            try
            {
                vloListado = ObtenerClientes();
                string vlcRespuesta = JsonConvert.SerializeObject(vloListado);
                vloRespuestaApi = new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(vlcRespuesta) };

            }
            catch (Exception)
            {
                vloRespuestaApi = new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
            return vloRespuestaApi;
        }

        [HttpPut]
        public HttpResponseMessage RegistroCliente()
        {
            string vlcRespuesta = String.Empty;
            HttpResponseMessage vloRespuestaApi;
            try
            {
                string vlcDatos = ObtenerInformacion();//Obtener la información enviada por el cliente como JSON

                vlcRespuesta = IngresoCliente(vlcDatos);

                vloRespuestaApi = new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(vlcRespuesta) };

                return vloRespuestaApi;
            }
            catch (Exception)
            {
                vloRespuestaApi = new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
            return vloRespuestaApi;
        }

        [HttpPatch]
        public HttpResponseMessage EditarCliente()
        {
            string vlcRespuesta = String.Empty;
            HttpResponseMessage vloRespuestaApi;
            try
            {
                string vlcDatos = ObtenerInformacion();//Obtener la información enviada por el cliente como JSON

                vlcRespuesta = ActualizarCliente(vlcDatos);

                vloRespuestaApi = new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(vlcRespuesta) };

                return vloRespuestaApi;
            }
            catch (Exception)
            {
                vloRespuestaApi = new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
            return vloRespuestaApi;
        }

        [HttpDelete]
        public HttpResponseMessage EliminarCliente()
        {
            string vlcRespuesta = String.Empty;
            HttpResponseMessage vloRespuestaApi;
            try
            {
                string vlcDatos = ObtenerInformacion();//Obtener la información enviada por el cliente como JSON

                vlcRespuesta = EliminarCliente(vlcDatos);

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
            return db.Clientes.Count(e => e.IdentificacionCliente == id) > 0;

        }

        /// <summary>
        /// Ingreso de usuario
        /// </summary>
        /// <param name="pvcDatos"></param>
        /// <returns></returns>
        private string IngresoCliente(string pvcDatos)
        {
            try
            {
                Clientes vloUsuario = JsonConvert.DeserializeObject<Clientes>(pvcDatos);
                if (existeUsuario(vloUsuario.IdentificacionCliente))
                {
                    return "2";//existe
                }
                else
                {
                    db.Clientes.Add(vloUsuario);
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
        private string EliminarCliente(string pvcDatos)
        {
            try
            {
                Clientes vloUsuario = JsonConvert.DeserializeObject<Clientes>(pvcDatos);
                if (existeUsuario(vloUsuario.IdentificacionCliente))
                {
                    Clientes usuarioEliminar = db.Clientes.SingleOrDefault(x => x.IdentificacionCliente == vloUsuario.IdentificacionCliente);

                    if (usuarioEliminar == null)
                    {
                        return "0";//error
                    }

                    db.Clientes.Remove(usuarioEliminar);
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
        private string ActualizarCliente(string pvcDatos)
        {
            try
            {
                Clientes vloUsuario = JsonConvert.DeserializeObject<Clientes>(pvcDatos);
                if (existeUsuario(vloUsuario.IdentificacionCliente))
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
        private List<Clientes> ObtenerClientes()
        {
            
            List<Clientes> listaClientes = new List<Clientes>();

            try
            {
                db.Clientes.ToList().ForEach(cp => listaClientes.Add(new Clientes()
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
