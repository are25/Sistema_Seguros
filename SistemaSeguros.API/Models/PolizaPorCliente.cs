//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SistemaSeguros.API.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class PolizaPorCliente
    {
        public int Id { get; set; }
        public int IdPoliza { get; set; }
        public string IdCliente { get; set; }
        public int IdEstado { get; set; }
    
        public virtual Clientes Clientes { get; set; }
        public virtual EstadosPoliza EstadosPoliza { get; set; }
        public virtual Poliza Poliza { get; set; }
    }
}
