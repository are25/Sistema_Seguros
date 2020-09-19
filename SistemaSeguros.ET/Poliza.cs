namespace SistemaSeguros.API.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Poliza")]
    public partial class Poliza
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Poliza()
        {
            PolizaPorCliente = new HashSet<PolizaPorCliente>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        [StringLength(200)]
        public string Descripcion { get; set; }

        public int? IdTipoCubrimiento { get; set; }

        public decimal CoberturaPoliza { get; set; }

        [Column(TypeName = "date")]
        public DateTime InicioVigencia { get; set; }

        public int PeriodoCobertura { get; set; }

        [Column(TypeName = "date")]
        public DateTime FinVigencia { get; set; }

        [Column(TypeName = "numeric")]
        public decimal PrecioPoliza { get; set; }

        public int? IdTipoRiesgo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PolizaPorCliente> PolizaPorCliente { get; set; }

        public virtual TipoCubrimiento TipoCubrimiento { get; set; }

        public virtual TipoRiesgo TipoRiesgo { get; set; }
    }
}
