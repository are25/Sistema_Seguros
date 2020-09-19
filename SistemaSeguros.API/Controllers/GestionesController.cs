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

    public class GestionesController : ApiController
    {
        private readonly string vccNomClase = "GestionesController";
        readonly BDSistemaSeguros db = new BDSistemaSeguros();
        #region Métodos Públicos
        [HttpGet]
        public HttpResponseMessage CargarPolizas()
        {
            HttpResponseMessage vloRespuestaApi;
            List<PolizaPorCliente> vloListado;
            try
            {
                vloListado = ObtenerPolizas();
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
        public HttpResponseMessage RegistroPolizaCliente()
        {
            string vlcRespuesta = String.Empty;
            HttpResponseMessage vloRespuestaApi;
            try
            {
                string vlcDatos = ObtenerInformacion();//Obtener la información enviada por el cliente como JSON

                vlcRespuesta = IngresoPolizaCliente(vlcDatos);

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
        public HttpResponseMessage EditarPoliza()
        {
            string vlcRespuesta = String.Empty;
            HttpResponseMessage vloRespuestaApi;
            try
            {
                string vlcDatos = ObtenerInformacion();//Obtener la información enviada por el cliente como JSON

                vlcRespuesta = ActualizarPoliza(vlcDatos);

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
        public HttpResponseMessage EliminarPolizas()
        {
            string vlcRespuesta = String.Empty;
            HttpResponseMessage vloRespuestaApi;
            try
            {
                string vlcDatos = ObtenerInformacion();//Obtener la información enviada por el cliente como JSON

                vlcRespuesta = EliminarPolizas(vlcDatos);

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

        #region Métodos Privados
        /// <summary>
        /// Método para obtener todos los usuarios.
        /// </summary>
        /// <returns></returns>
        private List<PolizaPorCliente> ObtenerPolizas()
        {

            List<PolizaPorCliente> listaPolizas = new List<PolizaPorCliente>();

            try
            {
                db.PolizaPorCliente.ToList().ForEach(cp => listaPolizas.Add(new PolizaPorCliente()
                {
                    Id = cp.Id,
                    IdCliente = cp.IdCliente,
                    IdEstado = cp.IdEstado,
                    IdPoliza = cp.IdPoliza,
                    Clientes = new Clientes() { IdentificacionCliente = cp.Clientes.IdentificacionCliente, NombreCliente = cp.Clientes.NombreCliente },
                    Poliza = new Poliza() { Id = cp.Poliza.Id, Descripcion = cp.Poliza.Descripcion },
                    EstadosPoliza = new EstadosPoliza() { Id = cp.EstadosPoliza.Id, Descripcion = cp.EstadosPoliza.Descripcion },

                }));
                return listaPolizas;
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
        private string EliminarPolizas(string pvcDatos)
        {
            try
            {
                PolizaPorCliente vloPoliza = JsonConvert.DeserializeObject<PolizaPorCliente>(pvcDatos);

                PolizaPorCliente polizaEliminar = db.PolizaPorCliente.SingleOrDefault(x => x.Id == vloPoliza.Id);

                if (polizaEliminar == null)
                {
                    return "0";//error
                }

                db.PolizaPorCliente.Remove(polizaEliminar);
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
        /// Método para obtener la información recibida desde el cliente.
        /// </summary>
        /// <returns>Información recibida por el cliente</returns>
        private string ObtenerInformacion()
        {
            string vlcDatos = Request.Content.ReadAsStringAsync().Result;

            return vlcDatos;
        }
        
        /// <summary>
        /// Actualizar póliza del sistema.
        /// </summary>
        /// <param name="pvcDatos"></param>
        /// <returns></returns>
        private string ActualizarPoliza(string pvcDatos)
        {
            try
            {
                PolizaPorCliente vloPoliza = JsonConvert.DeserializeObject<PolizaPorCliente>(pvcDatos);
                 
                db.Entry(vloPoliza).State = EntityState.Modified;
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
        /// Ingreso de usuario
        /// </summary>
        /// <param name="pvcDatos"></param>
        /// <returns></returns>
        private string IngresoPolizaCliente(string pvcDatos)
        {
            try
            {
                PolizaPorCliente vloPolizaCliente = JsonConvert.DeserializeObject<PolizaPorCliente>(pvcDatos);

                db.PolizaPorCliente.Add(vloPolizaCliente);

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
        #endregion
    }
}
