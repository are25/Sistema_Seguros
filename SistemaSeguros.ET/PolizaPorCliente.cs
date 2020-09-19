namespace SistemaSeguros.API.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PolizaPorCliente")]
    public partial class PolizaPorCliente
    {
        public int Id { get; set; }

        public int IdPoliza { get; set; }

        [Required]
        [StringLength(20)]
        public string IdCliente { get; set; }

        public int IdEstado { get; set; }

        public virtual Clientes Clientes { get; set; }

        public virtual EstadosPoliza EstadosPoliza { get; set; }

        public virtual Poliza Poliza { get; set; }
    }
}
