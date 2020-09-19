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
        public IEnumerable<Poliza> CargarPolizas()
        {
            List<Poliza> vloListado=null;
            try
            {
                vloListado = ObtenerPolizas();
                 

            }
            catch (Exception)
            {
                 InternalServerError();
            }
            return vloListado;
        }

        [HttpPatch]
        public IHttpActionResult EditarPolizas(Poliza Polizas)
        {
            string vlcRespuesta = String.Empty;
            IHttpActionResult vloRespuestaApi;
            try
            {
 
                vlcRespuesta = ActualizarPoliza(Polizas);

                vloRespuestaApi = Ok(vlcRespuesta);

            }
            catch (Exception)
            {
                vloRespuestaApi = InternalServerError();
            }
            return vloRespuestaApi;
        }
        
        [HttpDelete]
        public IHttpActionResult EliminarPolizas(Poliza Polizas)
        {
            string vlcRespuesta = String.Empty;
            IHttpActionResult vloRespuestaApi;
            try
            {

                vlcRespuesta = Eliminar(Polizas);

                vloRespuestaApi = Ok(vlcRespuesta);

            }
            catch (Exception)
            {
                vloRespuestaApi = InternalServerError();
            }
            return vloRespuestaApi;
        }

        [HttpPut]
        public IHttpActionResult RegistroPolizas(Poliza Polizas)
        {
            string vlcRespuesta = String.Empty;
            IHttpActionResult vloRespuestaApi;
            try
            {

                vlcRespuesta = IngresoPoliza(Polizas);
                vloRespuestaApi = Ok(vlcRespuesta);
                Polizas.Id = int.Parse(vlcRespuesta);
                return CreatedAtRoute("DefaultApi", new { id = Polizas.Nombre }, Polizas);

             }
            catch (Exception)
            {
                vloRespuestaApi = InternalServerError();
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
        private string Eliminar(Poliza vloPoliza)
        {
            try
            {

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
        private string ActualizarPoliza(Poliza vloPoliza)
        {
            try
            {
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
        private string IngresoPoliza(Poliza vloPoliza)
        {
            try
            {
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
