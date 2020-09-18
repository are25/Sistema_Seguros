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
        public HttpResponseMessage CargarTipoRiesgos()
        {
            HttpResponseMessage vloRespuestaApi;
            List<TipoRiesgo> vloListado;
            try
            {
                vloListado = ObtenerTipoRiesgos();
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
        public HttpResponseMessage RegistroTipoRiesgo()
        {
            string vlcRespuesta = String.Empty;
            HttpResponseMessage vloRespuestaApi;
            try
            {
                string vlcDatos = ObtenerInformacion();//Obtener la información enviada por el TipoRiesgo como JSON

                vlcRespuesta = IngresoTipoRiesgo(vlcDatos);

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
        public HttpResponseMessage EditarTipoRiesgo()
        {
            string vlcRespuesta = String.Empty;
            HttpResponseMessage vloRespuestaApi;
            try
            {
                string vlcDatos = ObtenerInformacion();//Obtener la información enviada por el TipoRiesgo como JSON

                vlcRespuesta = ActualizarTipoRiesgo(vlcDatos);

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
        public HttpResponseMessage EliminarTipoRiesgo()
        {
            string vlcRespuesta = String.Empty;
            HttpResponseMessage vloRespuestaApi;
            try
            {
                string vlcDatos = ObtenerInformacion();//Obtener la información enviada por el TipoRiesgo como JSON

                vlcRespuesta = EliminarTipoRiesgo(vlcDatos);

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
        private bool existeDato(int id)
        {
            return db.TipoRiesgo.Count(e => e.Id == id) > 0;

        }

        /// <summary>
        /// Ingreso de usuario
        /// </summary>
        /// <param name="pvcDatos"></param>
        /// <returns></returns>
        private string IngresoTipoRiesgo(string pvcDatos)
        {
            try
            {
                TipoRiesgo vloUsuario = JsonConvert.DeserializeObject<TipoRiesgo>(pvcDatos);

                db.TipoRiesgo.Add(vloUsuario);
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
        private string EliminarTipoRiesgo(string pvcDatos)
        {
            try
            {
                TipoRiesgo vloUsuario = JsonConvert.DeserializeObject<TipoRiesgo>(pvcDatos);
                if (existeDato(vloUsuario.Id))
                {
                    TipoRiesgo usuarioEliminar = db.TipoRiesgo.SingleOrDefault(x => x.Id == vloUsuario.Id);

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
        private string ActualizarTipoRiesgo(string pvcDatos)
        {
            try
            {
                TipoRiesgo vloUsuario = JsonConvert.DeserializeObject<TipoRiesgo>(pvcDatos);
                if (existeDato(vloUsuario.Id))
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
