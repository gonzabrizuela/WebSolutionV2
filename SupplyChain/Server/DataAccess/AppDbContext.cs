using SupplyChain.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SupplyChain.Server.DataAccess
{
    public class AppDbContext : DbContext
    {
        public virtual DbSet<Area> Area { get; set; }
        public virtual DbSet<CalendarioFestivos> CalendarioFestivos { get; set; }
        public virtual DbSet<CatOpe> CatOpe { get; set; }
        public virtual DbSet<Celdas> Celda { get; set; }
        public virtual DbSet<Compañia> Cias { get; set; }
        public virtual DbSet<Clase> Clase { get; set; }
        public virtual DbSet<Cliente> Clientes { get; set; }
        public virtual DbSet<Compra> Compras { get; set; }
        public virtual DbSet<Deposito> Deposito { get; set; }
        public virtual DbSet<Deposito> Depositos { get; set; }
        public virtual DbSet<Indic> Indic { get; set; }
        public virtual DbSet<Lineas> Lineas { get; set; }
        public virtual DbSet<Monedas> Monedas { get; set; }
        public virtual DbSet<Operario> Operario { get; set; }
        public virtual DbSet<Pais> Pais { get; set; }
        public virtual DbSet<Paradas> Paradas { get; set; }
        public virtual DbSet<Stock> Pedidos { get; set; }
        public virtual DbSet<PresAnual> PresAnual { get; set; }
        public virtual DbSet<ModeloPendientesFabricar> ModeloPendientesFabricar { get; set; }
        public virtual DbSet<ModeloAbastecimiento> ModeloAbastecimiento { get; set; }
        public virtual DbSet<Producto> Prod { get; set; }
        public virtual DbSet<Producto> Productos { get; set; }
        public virtual DbSet<Programa> Programas { get; set; }
        public virtual DbSet<Protab> Protab { get; set; }
        public virtual DbSet<Protarea> Protarea { get; set; }
        public virtual DbSet<Provincia> Provincias { get; set; }
        public virtual DbSet<Scrap> Scrap { get; set; }
        public virtual DbSet<TipoArea> TipoArea { get; set; }
        public virtual DbSet<TipoCelda> TipoCelda { get; set; }
        public virtual DbSet<TipoMat> TipoMat { get; set; }
        public virtual DbSet<TiposNoConf> TiposNoConf { get; set; }
        public virtual DbSet<Turnos> Turnos { get; set; }
        public virtual DbSet<Unidad> Unidades { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }
        public DbSet<ResumenStock> ResumenStock { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ItemAbastecimiento>().HasNoKey().ToView(null);

            //modelBuilder.Entity<Proveedor>()
            //.HasMany(p => p.Stocks).WithOne(s => s.Proveedor)
            //.HasForeignKey(c => c.CG_PROVE)
            //.IsRequired(false);

            //modelBuilder.Entity<Stock>()
            //.HasOne(p => p.Proveedor).WithOne()
            //.IsRequired(false);
        }
    }
}
