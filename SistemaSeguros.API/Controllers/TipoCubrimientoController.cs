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
        readonly BDSistemaSeguros db = new BDSistemaSeguros();
        #region Métodos Públicos

        [HttpGet]
        public HttpResponseMessage CargarTipoCubrimiento()
        {
            HttpResponseMessage vloRespuestaApi;
            List<TipoCubrimiento> vloListado;
            try
            {
                vloListado = ObtenerTipoCubrimiento();
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
        public HttpResponseMessage RegistroTipoCubrimiento()
        {
            string vlcRespuesta = String.Empty;
            HttpResponseMessage vloRespuestaApi;
            try
            {
                string vlcDatos = ObtenerInformacion();//Obtener la información enviada por el TipoCubrimiento como JSON

                vlcRespuesta = IngresoTipoCubrimiento(vlcDatos);

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
        public HttpResponseMessage EditarTipoCubrimiento()
        {
            string vlcRespuesta = String.Empty;
            HttpResponseMessage vloRespuestaApi;
            try
            {
                string vlcDatos = ObtenerInformacion();//Obtener la información enviada por el TipoCubrimiento como JSON

                vlcRespuesta = ActualizarTipoCubrimiento(vlcDatos);

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
        public HttpResponseMessage EliminarTipoCubrimiento()
        {
            string vlcRespuesta = String.Empty;
            HttpResponseMessage vloRespuestaApi;
            try
            {
                string vlcDatos = ObtenerInformacion();//Obtener la información enviada por el TipoCubrimiento como JSON

                vlcRespuesta = EliminarTipoCubrimiento(vlcDatos);

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
            return db.TipoCubrimiento.Count(e => e.Id == id) > 0;

        }

        /// <summary>
        /// Ingreso de usuario
        /// </summary>
        /// <param name="pvcDatos"></param>
        /// <returns></returns>
        private string IngresoTipoCubrimiento(string pvcDatos)
        {
            try
            {
                TipoCubrimiento vloCubrimiento = JsonConvert.DeserializeObject<TipoCubrimiento>(pvcDatos);

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
        private string EliminarTipoCubrimiento(string pvcDatos)
        {
            try
            {
                TipoCubrimiento vloCubrimiento = JsonConvert.DeserializeObject<TipoCubrimiento>(pvcDatos);
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
        private string ActualizarTipoCubrimiento(string pvcDatos)
        {
            try
            {
                TipoCubrimiento vloCubrimiento = JsonConvert.DeserializeObject<TipoCubrimiento>(pvcDatos);
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
