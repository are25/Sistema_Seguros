namespace SistemaSeguros.API.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

     public  class PolizaVM
    {
        

        public int Id { get; set; }

        
        public string Nombre { get; set; }

         public string Descripcion { get; set; }

        public int? IdTipoCubrimiento { get; set; }

        public decimal CoberturaPoliza { get; set; }

         public DateTime InicioVigencia { get; set; }

        public int PeriodoCobertura { get; set; }

         public DateTime FinVigencia { get; set; }

         public decimal PrecioPoliza { get; set; }

        public int? IdTipoRiesgo { get; set; }

       

        public List<TipoCubrimientoVM> TipoCubrimiento { get; set; }

        public List<TipoRiesgoVM> TipoRiesgo { get; set; }
    }
}
