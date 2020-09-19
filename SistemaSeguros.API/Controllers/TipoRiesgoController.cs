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
    public class TipoRiesgoController : ApiController
    {
        private readonly string vccNomClase = "TipoRiesgoController";
        readonly BDSistemaSeguros db = new BDSistemaSeguros();
        #region Métodos Públicos

        [HttpGet]
        public IEnumerable<TipoRiesgo> CargarTipoRiesgos()
        {
             List<TipoRiesgo> vloListado=null;
            try
            {
                vloListado = ObtenerTipoRiesgos();
                

            }
            catch (Exception)
            {
                InternalServerError();
            }
            return vloListado;
        }

        [HttpPut]
        public IHttpActionResult RegistroTipoRiesgo(TipoRiesgo TipoRiesgo)
        {
            string vlcRespuesta = String.Empty;
            IHttpActionResult vloRespuestaApi;
            try
            {
 
                vlcRespuesta = IngresoTipoRiesgo(TipoRiesgo);

                return CreatedAtRoute("DefaultApi", new { id = TipoRiesgo.Descripcion }, TipoRiesgo);

            }
            catch (Exception)
            {
                vloRespuestaApi = InternalServerError();
            }
            return vloRespuestaApi;
        }

        [HttpPatch]
        public IHttpActionResult EditarTipoRiesgo(TipoRiesgo TipoRiesgo)
        {
            string vlcRespuesta = String.Empty;
            IHttpActionResult vloRespuestaApi;
            try
            {
 
                vlcRespuesta = ActualizarTipoRiesgo(TipoRiesgo);

                vloRespuestaApi = Ok(vlcRespuesta) ;

                return vloRespuestaApi;
            }
            catch (Exception)
            {
                vloRespuestaApi = InternalServerError();
            }
            return vloRespuestaApi;
        }

        [HttpDelete]
        public IHttpActionResult EliminarTipoRiesgo(TipoRiesgo TipoRiesgo)
        {
            string vlcRespuesta = String.Empty;
            IHttpActionResult vloRespuestaApi;
            try
            {
  
                vlcRespuesta = Eliminar(TipoRiesgo);

                vloRespuestaApi = Ok(vlcRespuesta);

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
            return db.TipoRiesgo.Count(e => e.Id == id) > 0;

        }

        /// <summary>
        /// Ingreso de usuario
        /// </summary>
        /// <param name="pvcDatos"></param>
        /// <returns></returns>
        private string IngresoTipoRiesgo(TipoRiesgo TipoRiesgo)
        {
            try
            {
 
                db.TipoRiesgo.Add(TipoRiesgo);
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
        private string Eliminar(TipoRiesgo TipoRiesgo)
        {
            try
            {
                 if (existeDato(TipoRiesgo.Id))
                {
                    TipoRiesgo usuarioEliminar = db.TipoRiesgo.SingleOrDefault(x => x.Id == TipoRiesgo.Id);

                    if (usuarioEliminar == null)
                    {
                        return "0";//error
                    }

                    db.TipoRiesgo.Remove(usuarioEliminar);
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
        private string ActualizarTipoRiesgo(TipoRiesgo TipoRiesgo)
        {
            try
            {
                 if (existeDato(TipoRiesgo.Id))
                {
                    db.Entry(TipoRiesgo).State = EntityState.Modified;
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
        /// Método para obtener la información recibida desde el TipoRiesgo.
        /// </summary>
        /// <returns>Información recibida por el TipoRiesgo</returns>
        private string ObtenerInformacion()
        {
            string vlcDatos = Request.Content.ReadAsStringAsync().Result;

            return vlcDatos;
        }
        /// <summary>
        /// Método para obtener todos los usuarios.
        /// </summary>
        /// <returns></returns>
        private List<TipoRiesgo> ObtenerTipoRiesgos()
        {

            List<TipoRiesgo> listaTipoRiesgos = new List<TipoRiesgo>();

            try
            {
                db.TipoRiesgo.ToList().ForEach(cp => listaTipoRiesgos.Add(new TipoRiesgo()
                {
                    Id = cp.Id,
                    Descripcion = cp.Descripcion
                }));
                return listaTipoRiesgos;
            }
            catch (Exception ex)
            {
                throw new ControlErrores(System.Reflection.MethodBase.GetCurrentMethod().Name, vccNomClase, ex.Message);
            }

        }

        #endregion
    }
}
