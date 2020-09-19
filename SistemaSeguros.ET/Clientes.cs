namespace SistemaSeguros.API.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Clientes
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Clientes()
        {
            PolizaPorCliente = new HashSet<PolizaPorCliente>();
        }

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

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PolizaPorCliente> PolizaPorCliente { get; set; }
    }
}
