using Newtonsoft.Json;
using SistemaSeguros.API.Models;
using SistemaSeguros.EX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace SistemaSeguros.API.Controllers
{
    [EnableCors("*", "*", "*")]

    public class EstadosController : ApiController
    {
        private readonly string vccNomClase = "PolizasController";
        readonly SistemaSeguros_BD db = new SistemaSeguros_BD();
        #region Métodos Públicos

        [HttpGet]
        public HttpResponseMessage CargarEstados()
        {
            HttpResponseMessage vloRespuestaApi;
            List<EstadosPolizaVM> vloListado;
            try
            {
                vloListado = ObtenerEstados();
                string vlcRespuesta = JsonConvert.SerializeObject(vloListado);
                vloRespuestaApi = new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(vlcRespuesta) };

            }
            catch (Exception)
            {
                vloRespuestaApi = new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
            return vloRespuestaApi;
        }

        #endregion

        #region Métodos privados
        /// <summary>
        /// Método para obtener todos los estados.
        /// </summary>
        /// <returns></returns>
        private List<EstadosPolizaVM> ObtenerEstados()
        {

            List<EstadosPolizaVM> listaEstados = new List<EstadosPolizaVM>();

            try
            {
                db.EstadosPoliza.ToList().ForEach(cp => listaEstados.Add(new EstadosPolizaVM()
                {
                    Id = cp.Id,
                    Descripcion = cp.Descripcion
                }));
                return listaEstados;
            }
            catch (Exception ex)
            {
                throw new ControlErrores(System.Reflection.MethodBase.GetCurrentMethod().Name, vccNomClase, ex.Message);
            }

        }
        #endregion
    }
}
