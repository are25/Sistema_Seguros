namespace SistemaSeguros.API.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Usuarios
    {
        [Key]
        [StringLength(20)]
        public string Identificacion { get; set; }

        [Required]
        [StringLength(50)]
        public string NombreUsuario { get; set; }

        [Required]
        [StringLength(50)]
        public string CorreoUsuario { get; set; }

        [Required]
        [StringLength(50)]
        public string Contrasennia { get; set; }
    }
}
