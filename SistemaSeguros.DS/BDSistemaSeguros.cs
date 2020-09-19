namespace SistemaSeguros.DS
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using SistemaSeguros.API.Models;

    public partial class BDSistemaSeguros : DbContext
    {
        public BDSistemaSeguros()
            : base("name=BDSistemaSeguros")
        {
        }

        public virtual DbSet<Clientes> Clientes { get; set; }
        public virtual DbSet<EstadosPoliza> EstadosPoliza { get; set; }
        public virtual DbSet<Poliza> Poliza { get; set; }
        public virtual DbSet<PolizaPorCliente> PolizaPorCliente { get; set; }
        public virtual DbSet<TipoCubrimiento> TipoCubrimiento { get; set; }
        public virtual DbSet<TipoRiesgo> TipoRiesgo { get; set; }
        public virtual DbSet<Usuarios> Usuarios { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Clientes>()
                .Property(e => e.IdentificacionCliente)
                .IsUnicode(false);

            modelBuilder.Entity<Clientes>()
                .Property(e => e.NombreCliente)
                .IsUnicode(false);

            modelBuilder.Entity<Clientes>()
                .Property(e => e.CorreoCliente)
                .IsUnicode(false);

            modelBuilder.Entity<Clientes>()
                .Property(e => e.TelefonoContacto)
                .IsUnicode(false);

            modelBuilder.Entity<EstadosPoliza>()
                .Property(e => e.Descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<EstadosPoliza>()
                .HasMany(e => e.PolizaPorCliente)
                .WithRequired(e => e.EstadosPoliza)
                .HasForeignKey(e => e.IdEstado)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Poliza>()
                .Property(e => e.Nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Poliza>()
                .Property(e => e.Descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<PolizaPorCliente>()
                .Property(e => e.IdCliente)
                .IsUnicode(false);

            modelBuilder.Entity<TipoCubrimiento>()
                .Property(e => e.Descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<TipoCubrimiento>()
                .HasMany(e => e.Poliza)
                .WithOptional(e => e.TipoCubrimiento)
                .HasForeignKey(e => e.IdTipoCubrimiento);

            modelBuilder.Entity<TipoRiesgo>()
                .Property(e => e.Descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<TipoRiesgo>()
                .HasMany(e => e.Poliza)
                .WithOptional(e => e.TipoRiesgo)
                .HasForeignKey(e => e.IdTipoRiesgo);

            modelBuilder.Entity<Usuarios>()
                .Property(e => e.Identificacion)
                .IsUnicode(false);

            modelBuilder.Entity<Usuarios>()
                .Property(e => e.NombreUsuario)
                .IsUnicode(false);

            modelBuilder.Entity<Usuarios>()
                .Property(e => e.CorreoUsuario)
                .IsUnicode(false);

            modelBuilder.Entity<Usuarios>()
                .Property(e => e.Contrasennia)
                .IsUnicode(false);
        }
    }
}
