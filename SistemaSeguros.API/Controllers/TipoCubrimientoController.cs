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
    public class TipoCubrimientoController : ApiController
    {
        private readonly string vccNomClase = "TipoCubrimientoController";
        readonly SistemaSeguros_BD db = new SistemaSeguros_BD();
        #region Métodos Públicos

        [HttpGet]
        public IEnumerable<TipoCubrimiento> CargarTipoCubrimiento()
        {
             List<TipoCubrimiento> vloListado=null;
            try
            {
                vloListado = ObtenerTipoCubrimiento();
 
            }
            catch (Exception)
            {
                InternalServerError();
            }
            return vloListado;
        }

        [HttpPut]
        public IHttpActionResult RegistroTipoCubrimiento(TipoCubrimiento TipoCubrimiento)
        {
            string vlcRespuesta = String.Empty;
            IHttpActionResult vloRespuestaApi;
            try
            {

                vlcRespuesta = IngresoTipoCubrimiento(TipoCubrimiento);


                return CreatedAtRoute("DefaultApi", new { id = TipoCubrimiento.Descripcion }, TipoCubrimiento);
            }
            catch (Exception)
            {
                vloRespuestaApi = InternalServerError();
            }
            return vloRespuestaApi;
        }

        [HttpPatch]
        public IHttpActionResult EditarTipoCubrimiento(TipoCubrimiento TipoCubrimiento)
        {
            string vlcRespuesta = String.Empty;
            IHttpActionResult vloRespuestaApi;
            try
            {

                vlcRespuesta = ActualizarTipoCubrimiento(TipoCubrimiento);

                vloRespuestaApi = Ok(vlcRespuesta);

                return vloRespuestaApi;
            }
            catch (Exception)
            {
                vloRespuestaApi = InternalServerError();
            }
            return vloRespuestaApi;
        }

        [HttpDelete]
        public IHttpActionResult EliminarTipoCubrimiento(TipoCubrimiento TipoCubrimiento)
        {
            string vlcRespuesta = String.Empty;
            IHttpActionResult vloRespuestaApi;
            try
            {
 
                vlcRespuesta = Eliminar(TipoCubrimiento);

                vloRespuestaApi = Ok(vlcRespuesta);

                return vloRespuestaApi;
            }
            catch (Exception)
            {
                vloRespuestaApi = InternalServerError();
            }
            return vloRespuestaApi;
        }

        #endregion
        #region Métodos privados
        private bool existeDato(int id)
        {
            return db.TipoCubrimiento.Count(e => e.Id == id) > 0;

        }

        /// <summary>
        /// Ingreso de usuario
        /// </summary>
        /// <param name="pvcDatos"></param>
        /// <returns></returns>
        private string IngresoTipoCubrimiento(TipoCubrimiento vloCubrimiento)
        {
            try
            {
 
                db.TipoCubrimiento.Add(vloCubrimiento);
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
        private string Eliminar(TipoCubrimiento vloCubrimiento)
        {
            try
            {
                 if (existeDato(vloCubrimiento.Id))
                {
                    TipoCubrimiento cubrimientoEliminar = db.TipoCubrimiento.SingleOrDefault(x => x.Id == vloCubrimiento.Id);

                    if (cubrimientoEliminar == null)
                    {
                        return "0";//error
                    }

                    db.TipoCubrimiento.Remove(cubrimientoEliminar);
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
        private string ActualizarTipoCubrimiento(TipoCubrimiento vloCubrimiento)
        {
            try
            {
                 if (existeDato(vloCubrimiento.Id))
                {
                    db.Entry(vloCubrimiento).State = EntityState.Modified;
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
        /// Método para obtener la información recibida desde el TipoCubrimiento.
        /// </summary>
        /// <returns>Información recibida por el TipoCubrimiento</returns>
        private string ObtenerInformacion()
        {
            string vlcDatos = Request.Content.ReadAsStringAsync().Result;

            return vlcDatos;
        }
        /// <summary>
        /// Método para obtener todos los usuarios.
        /// </summary>
        /// <returns></returns>
        private List<TipoCubrimiento> ObtenerTipoCubrimiento()
        {

            List<TipoCubrimiento> listaTipoCubrimiento = new List<TipoCubrimiento>();

            try
            {
                db.TipoCubrimiento.ToList().ForEach(cp => listaTipoCubrimiento.Add(new TipoCubrimiento()
                {
                    Id = cp.Id,
                    Descripcion = cp.Descripcion
                }));
                return listaTipoCubrimiento;
            }
            catch (Exception ex)
            {
                throw new ControlErrores(System.Reflection.MethodBase.GetCurrentMethod().Name, vccNomClase, ex.Message);
            }

        }

        #endregion
    }
}
