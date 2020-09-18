namespace SistemaSeguros.API.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Clientes
    {
        [Key]
        [StringLength(20)]
        public string IdentificacionCliente { get; set; }

        [Required]
        [StringLength(50)]
        public string NombreCliente { get; set; }

        [StringLength(50)]
        public string CorreoCliente { get; set; }

        [Required]
        [StringLength(50)]
        public string TelefonoContacto { get; set; }
    }
}
