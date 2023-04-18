using SOL_JHEREMIAS_PULACHE.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SOL_JHEREMIAS_PULACHE.Context
{
    public class AppDatabaseContext : DbContext
    {
        public AppDatabaseContext() : base("OracleDbContext")
        {

        }

        public DbSet<Libro> Libros { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<TipoUsuario> TipoUsuario { get; set; }
        public DbSet<TipoPrestamo> TiposPrestamo { get; set; }
        public DbSet<Prestamo> Prestamos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("JHERE");


            base.OnModelCreating(modelBuilder);
        }
    }
}