﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class SistemaSeguros_BD : DbContext
    {
        public SistemaSeguros_BD()
            : base("name=SistemaSeguros_BD")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Clientes> Clientes { get; set; }
        public virtual DbSet<EstadosPoliza> EstadosPoliza { get; set; }
        public virtual DbSet<Poliza> Poliza { get; set; }
        public virtual DbSet<PolizaPorCliente> PolizaPorCliente { get; set; }
        public virtual DbSet<TipoCubrimiento> TipoCubrimiento { get; set; }
        public virtual DbSet<TipoRiesgo> TipoRiesgo { get; set; }
        public virtual DbSet<Usuarios> Usuarios { get; set; }
    }
}