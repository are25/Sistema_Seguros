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
    public class PolizasController : ApiController
    {
        private readonly string vccNomClase = "PolizasController";
        readonly BDSistemaSeguros db = new BDSistemaSeguros();
        #region Métodos Públicos

        [HttpGet]
        public HttpResponseMessage CargarPolizas()
        {
            HttpResponseMessage vloRespuestaApi;
            List<Poliza> vloListado;
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

        [HttpPatch]
        public HttpResponseMessage EditarPolizas()
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

        [HttpPut]
        public HttpResponseMessage RegistroPolizas()
        {
            string vlcRespuesta = String.Empty;
            HttpResponseMessage vloRespuestaApi;
            try
            {
                string vlcDatos = ObtenerInformacion();//Obtener la información enviada por el cliente como JSON

                vlcRespuesta = IngresoPoliza(vlcDatos);

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
        /// Eliminación de usuario
        /// </summary>
        /// <param name="pvcDatos"></param>
        /// <returns></returns>
        private string EliminarPolizas(string pvcDatos)
        {
            try
            {
                Poliza vloPoliza = JsonConvert.DeserializeObject<Poliza>(pvcDatos);

                Poliza polizaEliminar = db.Poliza.SingleOrDefault(x => x.Id == vloPoliza.Id);

                if (polizaEliminar == null)
                {
                    return "0";//error
                }

                db.Poliza.Remove(polizaEliminar);
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
        /// Actualizar póliza del sistema.
        /// </summary>
        /// <param name="pvcDatos"></param>
        /// <returns></returns>
        private string ActualizarPoliza(string pvcDatos)
        {
            try
            {
                Poliza vloPoliza = JsonConvert.DeserializeObject<Poliza>(pvcDatos);
                vloPoliza.FinVigencia = vloPoliza.InicioVigencia.AddMonths(vloPoliza.PeriodoCobertura);//sumar a la fecha final de vigencia de la póliza, los meses de cobertura.
                //Validar si riesgo es alto, que el % no sea superior a 50.
                if (vloPoliza.IdTipoRiesgo == 4)
                {
                    //verificar porcentaje
                    if (vloPoliza.CoberturaPoliza > 50)
                    {
                        return "2";//no se puede
                    }
                }
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
        private List<Poliza> ObtenerPolizas()
        {

            List<Poliza> listaPolizas = new List<Poliza>();

            try
            {
                db.Poliza.ToList().ForEach(cp => listaPolizas.Add(new Poliza()
                {
                    Id = cp.Id,
                    Nombre = cp.Nombre,
                    Descripcion = cp.Descripcion,
                    CoberturaPoliza = cp.CoberturaPoliza,
                    InicioVigencia = cp.InicioVigencia,
                    FinVigencia = cp.FinVigencia,
                    PrecioPoliza = cp.PrecioPoliza,
                    IdTipoCubrimiento = cp.IdTipoCubrimiento,
                    IdTipoRiesgo = cp.IdTipoRiesgo,
                    TipoCubrimiento = new TipoCubrimiento() { Id = cp.TipoCubrimiento.Id, Descripcion = cp.TipoCubrimiento.Descripcion },
                    TipoRiesgo = new TipoRiesgo() { Id = cp.TipoRiesgo.Id, Descripcion = cp.TipoRiesgo.Descripcion },
                    PeriodoCobertura = cp.PeriodoCobertura
                }));
                return listaPolizas;
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
        private string IngresoPoliza(string pvcDatos)
        {
            try
            {
                Poliza vloPoliza = JsonConvert.DeserializeObject<Poliza>(pvcDatos);
                vloPoliza.FinVigencia = vloPoliza.InicioVigencia.AddMonths(vloPoliza.PeriodoCobertura);//sumar a la fecha final de vigencia de la póliza, los meses de cobertura.
                //Validar si riesgo es alto, que el % no sea superior a 50.
                if(vloPoliza.IdTipoRiesgo == 4)
                {
                    //verificar porcentaje
                    if(vloPoliza.CoberturaPoliza > 50)
                    {
                        return "2";//no se puede
                    }
                } 
                db.Poliza.Add(vloPoliza);
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
